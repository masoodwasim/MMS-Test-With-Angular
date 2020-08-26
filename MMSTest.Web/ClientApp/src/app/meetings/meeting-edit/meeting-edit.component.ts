import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MeetingsService, AttendeesModel, MeetingsModel } from '../../Services/meetings.service';
import { Observable } from 'rxjs/internal/Observable';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-meeting-edit',
  templateUrl: './meeting-edit.component.html',
  styleUrls: ['./meeting-edit.component.css']
})
export class MeetingEditComponent implements OnInit {
  public attendeeslist: AttendeesModel;
  public meetinglist: MeetingsModel;
  submitted = false;
  errorMessage: string;
  selectedItems = [];
  dropdownSettings = {};
  attendeesArray = [];
  today: Date;
  heroes$: Observable;
  txtSubject: string;
  txtDOM: string;
  txtAgenda: string;

  meetingaddform: FormGroup
  constructor(public fb: FormBuilder, private router: Router, public crudService: MeetingsService, private route: ActivatedRoute, private datePipe: DatePipe) {
    this.getAllMeetings();
    this.getAttendees();
    this.today = new Date();
  }

  ngOnInit() {
    this.meetingaddform = this.fb.group({
      subject: ['', [Validators.required, Validators.maxLength(50)]],
      agenda: ['', [Validators.required]],
      dateOfMeeting: ['', [Validators.required]],  
      attendees: ['', [Validators.required]]
       
    })

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'fullName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 10,
      allowSearchFilter: true,
      limitSelection: 10
    };


    this.heroes$ = this.route
      .queryParams
      .subscribe(params => {
        this.txtSubject = this.route.snapshot.queryParamMap.get('subject');
        var attendees = this.route.snapshot.queryParamMap.get('attendees');
        this.selectedItems = attendees.split(";");
        this.txtAgenda = this.route.snapshot.queryParamMap.get('agenda');
        let txtDOM1 = this.route.snapshot.queryParamMap.get('dateOfMeeting');
        this.txtDOM = this.datePipe.transform(txtDOM1, 'dd-MM-yyyy');
        console.log(this.txtDOM);
      });

  }
  get f() { return this.meetingaddform.controls; }


  getAttendees() {
    this.crudService.getAllAttendees().subscribe(data => this.attendeeslist = data)
  }
  getAllMeetings() {
    this.crudService.getAllMeetings().subscribe(data => this.meetinglist = data)

  }
  submitForm() {
    this.submitted = true;
     
    if (this.meetingaddform.invalid) {
      console.log("Validation failed.");
      return;
    }
    this.crudService.create(this.meetingaddform.value).subscribe(res => {
      this.router.navigate(['/meetings']);
      console.log('Meeting was created successfully');
    })
  }


  onReset() {
    this.submitted = false;
    this.meetingaddform.reset();
  }

  onItemSelect(item: any) {
  }
  onSelectAll(items: any) {
    console.log('onSelectAll', items);
  }

  OnItemDeSelect(item: any) {
    
    const index: number = this.selectedItems.indexOf(item.fullName);
    if (index !== -1) {
      this.selectedItems.splice(index, 1);
    }   



  }
}
