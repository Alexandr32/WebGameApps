import { Component, OnInit, Inject } from '@angular/core';
import { HubConnectionService } from '../services/hub-connection.service';
import { HttpClient } from '@angular/common/http';
import { ConnectionServiceService } from '../services/connection.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  constructor(
    public signalRService: HubConnectionService,
    public connectionServiceService: ConnectionServiceService,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string) {

  }

  ngOnInit() {

    this.signalRService.startConnection();

    // Подписка на изменение
    this.signalRService
      .hubConnectionSubscription()
      .subscribe((data: String) => {

        console.log('hubConnectionSubscription', data)

      }, error => {
          console.error(error)
      });

    this.signalRService.hubConnection2Subscription()
      .subscribe((data: String) => {

        console.log('hubConnection2Subscription', data)

      }, error => {
        console.log(error)
      })

  }

  // Запрос с сервера 
  public startHttpRequest = () => {

    this.connectionServiceService.startHttpRequest()
      .subscribe(res => {

      })
  }

  public chartClicked = (event) => {
    this.signalRService.dataToServer("Привет");
  }


}
