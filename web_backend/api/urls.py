from django.urls import path
from rest_framework.urlpatterns import format_suffix_patterns
from rest_framework.authtoken.views import obtain_auth_token
from api import views

urlpatterns = [
    path('api/auth', views.CustomObtainAuthToken.as_view()),
    path('api/notifications', views.AllNotificationView.as_view()),
    path('api/pendingnotifications', views.PendingNotificationView.as_view()),
    path('api/trackingnotifications', views.TrackingNotificationView.as_view()),
    path('api/notifications/<int:pk>', views.NotificationDetailView.as_view()),
    path('api/history', views.HistoryView.as_view()),
    path('api/accounts', views.AccountView.as_view()),
    path('api/accounts/<int:pk>', views.AccountDetailView.as_view()),
    path('api/transactions', views.TransactionView.as_view()),
    path('api/register', views.UserView.as_view()),
    path('api/summary', views.summary),
    path('api/account_summary', views.account_summary),

    # path('api/test', views.test),
    path('', views.index),
    path('login', views.index),
    path('signup', views.index)
]

urlpatterns = format_suffix_patterns(urlpatterns)

