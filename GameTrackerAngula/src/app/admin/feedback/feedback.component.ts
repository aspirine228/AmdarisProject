import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder,Validators} from '@angular/forms';
import { Feedback } from 'src/app/_models/FeedBack/feedback';
import { FeedbackService } from 'src/app/_service/feedback.service';
import { AdminComponent } from '../admin/admin.component';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog.component';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {
  form!:FormGroup;
  loading=false;
  submitted=false;
  feedBackList!: Feedback[];
  displayedColumns!: string[]; 
  tableColumns: TableColumn[] = [
    { name: 'id', index: 'id', displayName: 'ID', useInSearch: true  },
    { name: 'userName', index: 'userName', displayName: 'UserName', useInSearch: true },
    { name: 'text', index: 'text', displayName: 'Text'},
    { name: 'grade', index: 'grade', displayName: 'Grade' },
   
    
  ];

  constructor(private formBuilder:FormBuilder,
    private feedBackService:FeedbackService, private router:Router,
    private admin:AdminComponent,public dialog: MatDialog,
    public snackBar: MatSnackBar
    ) {this.displayedColumns=this.tableColumns.map(column=>column.name); }
 
    formatLabel(value: number) {
      if (value >= 1000) {
        return Math.round(value / 1000) + 'k';
      }
  
      return value;
    }
  ngOnInit(): void {
    this.getUserFeedBacks();
    this.form=this.formBuilder.group({
      text: ['',Validators.required],
      grade: [0,[Validators.min(0), Validators.max(10)]],

    });

  }

  get f(){return this.form.controls;}
  
  onSubmit(){
    this.submitted=true;
    if(this.form.invalid){
      return;
    }
    this.loading=true;
    let feedBack:Feedback;
    feedBack=this.form.value;
    feedBack.userName=this.admin.username
    this.feedBackService.createFeedBack(feedBack).pipe(first()).subscribe(data=>{
      this.router.navigate(['./admin']);
    },
    error=>{
      this.loading=false;
    }   
  ); 
  }

  getUserFeedBacks(){
     
    this.feedBackService.getFeedBacksForUser(this.admin.id).subscribe((list:Feedback[])=>{
    this.feedBackList=list;}
    );
  }

  openDialogForDeleting(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { title: 'Dialog', message: 'Are you sure to delete this item?' }
    });
    dialogRef.disableClose = true;

    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.bool) {
        this.feedBackService.deleteUserFeedBack(id).subscribe(
          () => {
            this.getUserFeedBacks();
            dialogRef.componentInstance.bool=false;
            this.snackBar.open('The item has been deleted successfully.', 'Close', {
              duration: 1500
              
            });
          }
        );
      }
    });

  }
}
