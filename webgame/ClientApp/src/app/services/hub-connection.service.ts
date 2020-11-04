import { Injectable, Inject } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { ChartModel } from '../../model/ChartModel';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HubConnectionService {
  public data: String;

  private _hubConnection: signalR.HubConnection

  private readonly METHOD_NAME = 'BroadcastData'
  private readonly METHOD_NAME_TWO = 'broadcastdata2'

  private path: string
  private readonly PATH_FOR_HUB: string = 'data-to-server'

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.path = baseUrl + this.PATH_FOR_HUB
    console.log('path', this.path)
  }

  public startConnection = () => {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.path)
      .build();

    this._hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  // запрос с сервера
  public hubConnectionSubscription(): Observable<String> {

    const sbj = new Subject<String>()

    this._hubConnection.on(this.METHOD_NAME, (data: String) => {
      sbj.next(data)
    })

    return sbj
  }

  /**
   * Отправка данных на серве
   * @param dataForServer данные для сервера
   */
  public dataToServer(dataForServer: String) {
    this._hubConnection.invoke(this.METHOD_NAME, dataForServer)
      .catch(err => console.log(err));
  }

  // запрос с сервера
  public hubConnection2Subscription(): Observable<String> {

    const sbj = new Subject<String>()

    this._hubConnection.on(this.METHOD_NAME_TWO, (data: String) => {
      sbj.next(data)
    })

    return sbj
  }

  /**
   * Отправка данных на серве
   * @param dataForServer данные для сервера
   */
  public data2ToServer(dataForServer: String) {
    this._hubConnection.invoke(this.METHOD_NAME, dataForServer)
      .catch(err => console.log(err));
  }

}
