import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NewsService } from 'src/app/_service/news.service';
import { News } from 'src/app/_models/News/news';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-newsedit',
  templateUrl: './newsedit.component.html',
  styleUrls: ['./newsedit.component.css']
})
export class NewseditComponent implements OnInit {

  public pageTitle!: string;
  public newsForm!:FormGroup;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private newsService: NewsService,) { }

  ngOnInit(): void {
    
    let objectId: number;
    this.route.params.subscribe(params => {
      objectId = +params['id'];
     
        this.getNews(objectId);
        this.pageTitle = 'Edit News:';
      
    });

    this.newsForm = this.formBuilder.group({
      id: [objectId!, [Validators.required]],
      title: ['', [Validators.required]],
      text: ['', [Validators.required]],

    });
  }
  getNews(id: number): void {
    this.newsService.getCurrentNews(id).subscribe((news: News) => {
      this.newsForm.patchValue({
        ...news
      });
    });
  }

  saveNews(): void {
    if (this.newsForm.dirty && this.newsForm.valid) {
       const newsToSave: News = {
         ...this.newsForm.value
       };

       this.newsService.updateNews(newsToSave).subscribe(
         () => this.onSaveComplete()
       );
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.newsForm.reset();
    this.router.navigate(['/admin/feedback']);
  }

}
