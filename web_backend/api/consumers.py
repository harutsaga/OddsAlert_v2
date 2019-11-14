from channels.generic.websocket import WebsocketConsumer
import json
from asgiref.sync import async_to_sync
from channels.generic.websocket import AsyncWebsocketConsumer, JsonWebsocketConsumer

### Websocket Event Consumer ###
class EventConsumer(JsonWebsocketConsumer):
    # connect
    def connect(self):
        async_to_sync(self.channel_layer.group_add)('events', self.channel_name)
        self.accept()

    # disconnect
    def disconnect(self, close_code):
        async_to_sync(self.channel_layer.group_discard)(
            'events',
            self.channel_name
        )
        self.close()

    # send event
    def events_alarm(self, event):
        self.send_json(event)

    # event received
    def receive_json(self, content, **kwargs):
        self.send_json(content)
