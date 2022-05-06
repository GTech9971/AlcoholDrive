import { Component } from '@angular/core';
import { MessageDeliveryService } from './domain/service/MessageDelivery.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor(private deliveryService: MessageDeliveryService) {

  }
}
