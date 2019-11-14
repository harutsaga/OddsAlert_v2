from django.db import models

# Create your models here.
class Notification(models.Model):
    event_id = models.CharField(max_length=255, blank=True)
    datetime = models.DateTimeField()
    time_to_jump = models.CharField(max_length=255, blank=True)
    degree = models.IntegerField()
    state = models.CharField(max_length=255, blank=True)
    venue = models.CharField(max_length=255, blank=True)
    race_number = models.IntegerField()
    horse_number = models.IntegerField()
    horse_name = models.CharField(max_length=255, blank=True)
    previous_price = models.FloatField()
    current_price = models.FloatField()
    bookie = models.CharField(max_length=255, blank=True)
    suggested_stake = models.FloatField()
    max_profit = models.FloatField()
    top_price_1 = models.CharField(max_length=255, blank=True)
    top_price_2 = models.CharField(max_length=255, blank=True)
    top_price_3 = models.CharField(max_length=255, blank=True)
    top_price_4 = models.CharField(max_length=255, blank=True)

    account = models.ForeignKey('Account', related_name='dismissed_notification', on_delete = models.SET_NULL, blank=True, null = True)
    price_taken = models.FloatField()
    bet_amount = models.FloatField()
    BF_SP = models.FloatField()

    result = models.CharField(max_length=255, default="")
    status = models.IntegerField() # 0 : Undismissed, Nothing , 1 : Dismissed, Pending, 2 : Dismissed, Resulted, 3 : Deleted

class Transaction(models.Model):
    amount = models.FloatField()
    account = models.ForeignKey('Account', related_name='transaction', on_delete = models.CASCADE)
    action = models.CharField(max_length=255, blank=True)
    date = models.DateTimeField()

class Account(models.Model):
    name = models.CharField(max_length=255, blank=False)
    created = models.DateTimeField(auto_now_add=True)
    owner = models.ForeignKey('auth.User', related_name='accounts', on_delete = models.CASCADE, null = True)
