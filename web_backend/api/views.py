from rest_framework import status
from rest_framework.decorators import api_view, permission_classes
from rest_framework.authtoken.views import ObtainAuthToken
from rest_framework.authtoken.models import Token
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework import permissions
from .models import Notification, Account, Transaction
from .serializers import NotificationSerializer, AccountSerializer, TransactionSerializer, UserSerializer, SummarySerializer, AccountSummarySerializer
from django.db.models import Q
from django.contrib import auth
from dateutil import parser
from django.shortcuts import render
import datetime

from .channel_util import send_alert

def index(request):
    return render(request, 'index.html')

class CustomObtainAuthToken(ObtainAuthToken):
    def post(self, request, *args, **kwargs):
        response = super(CustomObtainAuthToken, self).post(request, *args, **kwargs)
        token = Token.objects.get(key=response.data['token'])
        return Response({'token': token.key, 'id': token.user_id})

class HistoryView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        records =[x for x in Notification.objects.filter(~Q(status=0)).order_by('-datetime') if x.account is not None and x.account.owner==request.user]
        serial = NotificationSerializer(records, many = True)
        return Response(serial.data)

class PendingNotificationView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        try:
            records = Notification.objects.filter(status=0).order_by('-datetime')
            serial = NotificationSerializer(records, many = True)
            return Response(serial.data)
        except Notification.DoesNotExist:
            records = None
            return Response(status = status.HTTP_204_NO_CONTENT)

class TrackingNotificationView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        try:
            records =[x for x in Notification.objects.filter(status=1) if x.account is not None]
            serial = NotificationSerializer(records, many = True)
            return Response(serial.data)
        except Notification.DoesNotExist:
            records = None
            return Response(status = status.HTTP_204_NO_CONTENT)


class AllNotificationView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        try:
            records = Notification.objects.order_by('-datetime')
            serial = NotificationSerializer(records, many = True)
            return Response(serial.data)
        except Notification.DoesNotExist:
            records = None
            return Response(status = status.HTTP_204_NO_CONTENT)
        

    def post(self, request, format = None):
        serial = NotificationSerializer(data = request.data)
        if serial.is_valid():
            serial.save()

            # new notification
            send_alert({
                "type": "events.alarm",
                "content" : 'update_notification',
                'alert': True
            })
            return Response(serial.data, status = status.HTTP_201_CREATED)
        
        return Response(serial.errors, status = status.HTTP_400_BAD_REQUEST)

class NotificationDetailView(APIView):
    permission_classes = (permissions.IsAuthenticated,)

    def get(self, request, pk, format = None):
        record = Notification.objects.get(pk=pk)
        serial = NotificationSerializer(record)
        return Response(serial.data)

    def put(self, request, pk, format = None):
        id = request.data.get('id')
        if id is not None:
            request.data.pop('id')
        Notification.objects.filter(pk=pk).update(**request.data)
        record = Notification.objects.get(pk=pk)
        serial = NotificationSerializer(record)

        send_alert({
            "type": "events.alarm",
            "content" : 'update_notification',
            'alert': False
        })
        return Response(serial.data)
       
    def delete(self, request, pk, format = None):
        record = Notification.objects.get(pk=pk)
        record.status = 3
        record.save()
        serial = NotificationSerializer(record)

        send_alert({
            "type": "events.alarm",
            "content" : 'update_notification',
            'alert': False
        })
        return Response(serial.data)

class AccountView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        records = Account.objects.all().filter(owner=request.user)
        serial = AccountSerializer(records, many=True)
        return Response(serial.data)

    def post(self, request, format = None):
        account_data = request.data
        account_data['owner'] = request.user.id
        serial = AccountSerializer(data = account_data)
        if serial.is_valid():
            serial.save()
            return Response(serial.data, status = status.HTTP_201_CREATED)
        return Response(serial.errors, status = status.HTTP_400_BAD_REQUEST)

    def delete(self, request, format = None):
        records = Account.objects.all().filter(owner=request.user)
        for account in records:
            account.delete()
        return Response(status = status.HTTP_204_NO_CONTENT)

class AccountDetailView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, pk, format = None):
        account = Account.objects.get(pk=pk)
        serial = AccountSerializer(account)
        return Response(serial.data)

    def put(self, request, pk, format=None):
        account = Account.objects.get(pk)
        serializer = AccountSerializer(account, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    def delete(self, request, pk, format = None):
        account = Account.objects.get(pk=pk)
        if account is not None:
            account.delete()
        return Response(status.HTTP_204_NO_CONTENT)

class TransactionView(APIView):
    permission_classes = (permissions.IsAuthenticated,)
    def get(self, request, format = None):
        records = Transaction.objects.all()
        serial = TransactionSerializer(records, many = True)
        return Response(serial.data)

    def post(self, request, format = None):
        serial = TransactionSerializer(data = request.data)
        if serial.is_valid():
            serial.save()
            return Response(serial.data, status = status.HTTP_201_CREATED)
        return Response(serial.errors, status = status.HTTP_400_BAD_REQUEST)

class UserView(APIView):
    def post(self, request, format = None):
        serial = UserSerializer(data = request.data)
        if serial.is_valid():
            serial.save()
            return Response(serial.data, status = status.HTTP_201_CREATED)
        
        return Response(serial.errors, status = status.HTTP_400_BAD_REQUEST)

@api_view(['POST'])
@permission_classes((permissions.IsAuthenticated, ))
def summary(request):
    if request.method == 'POST':
        start_dt = parser.parse(request.data.get('start_date'))
        end_dt =  parser.parse(request.data.get('end_date'))

        user = request.user
        summary_data = []
        g_total_bets = 0
        g_total_wins = 0
        g_turnover = 0
        g_returns = 0
        g_pending_bets = 0
        g_pending_turnover = 0
        for acc in user.accounts.all():
            resulted = acc.dismissed_notification.filter(status=2, datetime__gte = start_dt, datetime__lte = end_dt)
            pending = acc.dismissed_notification.filter(status=1, datetime__gte = start_dt, datetime__lte = end_dt)

            total_bets = len(resulted)
            total_wins = len([y for y in resulted if y.result == "Win"])
            turnover = sum(x.bet_amount for x in resulted)
            returns = sum(x.bet_amount * x.price_taken for x in [y for y in resulted if y.result == "Win"])
            profit = returns - turnover
            strike_rate = 100.0 * total_wins / total_bets if total_bets !=0 else 0
            pot = 100.0 * profit / turnover if turnover !=0 else 0
            pending_bets = len(pending)
            pending_turnover = sum(x.bet_amount for x in pending)
            cur_position = profit - pending_turnover

            g_total_bets += total_bets
            g_total_wins += total_wins
            g_turnover += turnover
            g_returns += returns
            g_pending_bets += pending_bets
            g_pending_turnover += pending_turnover

            summary_data.append({
                'account': acc.name,
                'total_bets':total_bets,
                'total_wins':total_wins,
                'turnover':turnover,
                'returns':returns,
                'profit':profit,
                'strike_rate':strike_rate,
                'pot':pot,
                'pending_bets':pending_bets,
                'pending_turnover':pending_turnover,
                'cur_position':cur_position
            })
            
        g_profit = g_returns - g_turnover
        g_strike_rate = 100.0 * g_total_wins / g_total_bets if g_total_bets !=0 else 0
        g_pot = 100.0 * g_profit / g_turnover if g_turnover !=0 else 0
        g_cur_position = g_profit - g_pending_turnover
        summary_data.insert(0, {
                'account':'',
                'total_bets':g_total_bets,
                'total_wins':g_total_wins,
                'turnover':g_turnover,
                'returns':g_returns,
                'profit':g_profit,
                'strike_rate':g_strike_rate,
                'pot':g_pot,
                'pending_bets':g_pending_bets,
                'pending_turnover':g_pending_turnover,
                'cur_position':g_cur_position
            })

        serializer = SummarySerializer(summary_data, many=True)
        return Response(serializer.data)

@api_view(['POST'])
@permission_classes((permissions.IsAuthenticated, ))
def account_summary(request):
    if request.method == 'POST':
        start_dt = parser.parse(request.data.get('start_date'))
        end_dt =  parser.parse(request.data.get('end_date'))
        user = request.user
        summary_data = []

        g_opening_balance = 0
        g_deposit = 0
        g_withdrawl = 0
        g_adjustments = 0
        g_closing_balance = 0
        g_actual_closing_balance = 0
        g_movement = 0

        for acc in user.accounts.all():
            trans = acc.transaction.filter(date__gte = start_dt, date__lte = end_dt)
            prev = acc.transaction.filter(date__lte = start_dt - datetime.timedelta(seconds = 1 ), action='Actual').order_by('-date')
            if prev is not None and len(prev) > 0:
                opening_balance = prev[0].amount
            else:
                opening_balance = 0

            deposit = sum(x.amount for x in trans if x.action=='Deposit')
            withdrawl = -sum(x.amount for x in trans if x.action=='Withdraw')
            adjustments = sum(x.amount for x in trans if x.action=='Adjust')
            closing_balance = opening_balance + deposit - withdrawl + adjustments

            cur = acc.transaction.filter(date__gte = start_dt, date__lte = end_dt, action='Actual').order_by('-date')
            if cur is not None and len(cur) > 0:
                actual_closing_balance = cur[0].amount
            else:
                actual_closing_balance = 0

            movement = - closing_balance + actual_closing_balance

            g_opening_balance += opening_balance
            g_deposit += deposit
            g_withdrawl += withdrawl
            g_adjustments += adjustments
            g_closing_balance += closing_balance
            g_actual_closing_balance += actual_closing_balance
            g_movement += movement

            summary_data.append({
                'account': acc.name,
                'opening_balance':opening_balance,
                'deposit':deposit,
                'withdrawl':withdrawl,
                'adjustments':adjustments,
                'closing_balance':closing_balance,
                'actual_closing_balance':actual_closing_balance,
                'movement':movement,
            })
            
        summary_data.insert(0, {
                'account': '',
                'opening_balance':g_opening_balance,
                'deposit':g_deposit,
                'withdrawl':g_withdrawl,
                'adjustments':g_adjustments,
                'closing_balance':g_closing_balance,
                'actual_closing_balance':g_actual_closing_balance,
                'movement':g_movement,
            })

        serializer = AccountSummarySerializer(summary_data, many=True)
        return Response(serializer.data)
