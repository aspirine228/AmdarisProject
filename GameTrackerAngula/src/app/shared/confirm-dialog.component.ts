import { Component, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogActions } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogContent } from '@angular/material/dialog';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDialogClose } from '@angular/material/dialog';


@Component({
    selector: 'confirm-dialog',
    template: `
        <div class="confirm-dialog">
        <h1 class="confirm-dialog-title">{{data && data.title? data.title: 'Dialog'}}</h1>
        <h1  class="confirm-dialog-content">
        Are you sure to do this?
        </h1 >
      
        <div mat-dialog-actions>
  <button mat-button (click)="onClick()">No Thanks</button>
  <button mat-button (click)="onClickTrue()" cdkFocusInitial>Ok</button>
        </div>
  
        </div>`
    ,
    styles: [
        `
        .confirm-dialog {
            min-width: 350px;
            font-family: sans-serif;
            }
        .confirm-dialog-title {
            margin-top:0px;
        }
        .confirm-dialog-content {
            padding-top:10px; 
            padding-bottom:20px;
        }
        .confirm-dialog-action {
            justify-content: center;
        }
        `
    ]

})

export class ConfirmDialogComponent {
    // dialogActions: string;
    public readonly ACTION_YES: string = "YES";
    public readonly ACTION_NO: string = "NO";
    public readonly ACTION_CANCEL: string = "CANCELED";
    public readonly ACTION_IGNORE: string = "IGNORED";
    public readonly ACTION_OK: string = "OK";
    public readonly ACTION_CLOSE: string = "CLOSED";
    public readonly ACTION_CONFIRM: string = "CONFIRMED";
 public bool=false;
    constructor( @Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ConfirmDialogComponent>) {
      

    }
    onClick():void{
        this.dialogRef.close();
    }
    onClickTrue():void{
       this.bool=true;
        this.dialogRef.close();
    }    
    
}