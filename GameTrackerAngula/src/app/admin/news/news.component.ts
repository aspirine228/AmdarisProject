import { Component, OnInit } from '@angular/core';
import { NewsService } from 'src/app/_service/news.service';
import { News } from 'src/app/_models/News/news';
import { AdminComponent } from '../admin/admin.component';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog.component';
import { FormGroup, FormBuilder,Validators} from '@angular/forms';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {
  form!:FormGroup;
  loading=false;
  submitted=false;
  displayedColumns!: string[]; 
  tableColumns: TableColumn[] = [
    { name: 'id', index: 'id', displayName: 'ID', useInSearch: true  },
    { name: 'title', index: 'title', displayName: 'Title', useInSearch: true },
    { name: 'text', index: 'text', displayName: 'Text'},
    
    
  ];

  newsList!:News[];
  constructor(private newsService:NewsService, private formBuilder:FormBuilder,private router:Router,
    private admin:AdminComponent,public dialog: MatDialog,
    public snackBar: MatSnackBar
    ) {this.displayedColumns=this.tableColumns.map(column=>column.name); }

  ngOnInit(): void {
    this.getNews();
    this.form=this.formBuilder.group({
      title: ['',Validators.required],
      text: ['',Validators.required],

    });
  }


  getNews(){
  this.newsService.getNews().subscribe((list: News[]) => {
        
    this.newsList = list ;

  });;
  }

  get f(){return this.form.controls;}
  
  onSubmit(){
    this.submitted=true;
    if(this.form.invalid){
      return;
    }

    this.loading=true;
    let news:News;
    news=this.form.value;
  
    this.newsService.createNews(news).pipe(first()).subscribe(data=>{
      this.router.navigate(['./admin']);
    },
    error=>{
      this.loading=false;
    }   
  ); 
  }

  openDialogForDeleting(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { title: 'Dialog', message: 'Are you sure to delete this item?' }
    });
    dialogRef.disableClose = true;

    dialogRef.afterClosed().subscribe(result => {
      if (dialogRef.componentInstance.bool) {
        this.newsService.deleteNews(id).subscribe(
          () => {
            this.getNews();
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
