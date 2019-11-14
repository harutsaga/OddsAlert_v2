from rest_framework import serializers
from django.contrib import auth
from django.db import models
from .models import Notification, Account, Transaction

class NotificationSerializer(serializers.ModelSerializer):
    account_name = serializers.SerializerMethodField()
    
    def get_account_name(self, obj):
        if obj.account is not None:
            return obj.account.name
        else:
            return ''

    class Meta:
        model = Notification
        fields = [field.name for field in model._meta.fields]
        fields.append('account_name')

class AccountSerializer(serializers.ModelSerializer):
    class Meta:
        model = Account
        fields = '__all__'

class TransactionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Transaction
        fields = '__all__'

class UserSerializer(serializers.ModelSerializer):
    def create(self, validated_data):
        user = auth.get_user_model().objects.create(
            username=validated_data['username']
        )
        user.set_password(validated_data['password'])
        user.save()
        return user

    class Meta:
        model = auth.get_user_model()
        fields = ('id', 'username', 'password')

class SummarySerializer(serializers.Serializer):
    account = serializers.CharField()
    total_bets= serializers.IntegerField()
    total_wins= serializers.IntegerField()
    turnover= serializers.DecimalField(max_digits=20, decimal_places=2)
    returns= serializers.DecimalField(max_digits=20, decimal_places=2)
    profit= serializers.DecimalField(max_digits=20, decimal_places=2)
    strike_rate= serializers.DecimalField(max_digits=20, decimal_places=2)
    pot= serializers.DecimalField(max_digits=20, decimal_places=2)
    pending_bets= serializers.IntegerField()
    pending_turnover= serializers.DecimalField(max_digits=20, decimal_places=2)
    cur_position= serializers.DecimalField(max_digits=20, decimal_places=2)

class AccountSummarySerializer(serializers.Serializer):
    account = serializers.CharField()
    opening_balance = serializers.DecimalField(max_digits=20, decimal_places=2)
    deposit = serializers.DecimalField(max_digits=20, decimal_places=2)
    withdrawl = serializers.DecimalField(max_digits=20, decimal_places=2)
    adjustments = serializers.DecimalField(max_digits=20, decimal_places=2)
    closing_balance = serializers.DecimalField(max_digits=20, decimal_places=2)
    actual_closing_balance = serializers.DecimalField(max_digits=20, decimal_places=2)
    movement = serializers.DecimalField(max_digits=20, decimal_places=2)