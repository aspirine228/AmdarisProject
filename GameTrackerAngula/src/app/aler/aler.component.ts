import { Component, OnInit ,OnDestroy} from '@angular/core';
import { Subscription } from 'rxjs';

import { AlertService } from '../_service/alert.service';

@Component({
  selector: 'app-aler',
  templateUrl: './aler.component.html',
  styleUrls: ['./aler.component.css']
})
export class AlerComponent implements OnInit,OnDestroy {
private subscription! : Subscription;
message: any;
  constructor(private alertService:AlertService) { }

  ngOnInit(): void {
    this.subscription=this.alertService.getAlert()
    .subscribe(message=>{
      switch (message && message.type){
        case 'success':
          message.cssClass='aler alert-success';
          break;
          case 'error':
            message.cssClass = 'aler aler-danger';
            break;
      }
      this.message=message;
    });
  }
ngOnDestroy(){
  this.subscription.unsubscribe();
}
}
