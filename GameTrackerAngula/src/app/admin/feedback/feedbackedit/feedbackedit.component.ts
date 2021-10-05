import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FeedbackService } from 'src/app/_service/feedback.service';
import { Feedback } from 'src/app/_models/FeedBack/feedback';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';



@Component({
  selector: 'app-feedbackedit',
  templateUrl: './feedbackedit.component.html',
  styleUrls: ['./feedbackedit.component.css']
})
export class FeedbackeditComponent implements OnInit {
  public pageTitle!: string;
  public feedBackForm!:FormGroup;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private feedBackService: FeedbackService,) { }

  ngOnInit(): void {
    
    let objectId: number;
    this.route.params.subscribe(params => {
      objectId = +params['id'];
     
        this.getFeedBack(objectId);
        this.pageTitle = 'Edit FeedBack:';
      
    });

    this.feedBackForm = this.formBuilder.group({
      id: [objectId!, [Validators.required]],
      text: ['', [Validators.required]],
      grade: [0, [Validators.min(0), Validators.max(10)]]
    });
  }
  getFeedBack(id: number): void {
    this.feedBackService.getFeedBack(id).subscribe((feedBack: Feedback) => {
      this.feedBackForm.patchValue({
        ...feedBack
      });
    });
  }

  saveFeedBack(): void {
    if (this.feedBackForm.dirty && this.feedBackForm.valid) {
       const feedBackToSave: Feedback = {
         ...this.feedBackForm.value
       };

       this.feedBackService.updateUserFeedBack(feedBackToSave).subscribe(
         () => this.onSaveComplete()
       );
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.feedBackForm.reset();
    this.router.navigate(['/admin/feedback']);
  }
}
