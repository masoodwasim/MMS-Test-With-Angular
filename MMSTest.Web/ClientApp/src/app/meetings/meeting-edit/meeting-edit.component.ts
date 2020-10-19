import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MeetingsService, AttendeesModel, MeetingsModel } from '../../Services/meetings.service';
import { Observable } from 'rxjs/internal/Observable';
import { DatePipe } from '@angular/common';
import { meeting } from '../../models/Meeting';


@Component({
  selector: 'app-meeting-edit',
  templateUrl: './meeting-edit.component.html',
  styleUrls: ['./meeting-edit.component.css']
})
export class MeetingEditComponent implements OnInit {
  public attendeeslist: AttendeesModel[];
  meetinglist: MeetingsModel;
  submitted = false;
  errorMessage: string;
  selectedItems = [];
  dropdownSettings = {};
  attendeesArray = [];
  today: Date;
  heroes$: Observable;
  subject: string;
  dateofmeeting: string;
  agenda: string;
  meetingaddform: FormGroup
  Id: number;
  FormHeader: string = "Add New ";

  public objattendees: []=[];

  constructor(public fb: FormBuilder, private router: Router, public crudService: MeetingsService, private route: ActivatedRoute, private datePipe: DatePipe) {
    this.getAttendees();
    this.today = new Date();
  }

  ngOnInit() {
    this.meetingaddform = this.fb.group({
      Id:[''],
      subject: ['', [Validators.required, Validators.maxLength(50)]],
      agenda: ['', [Validators.required]],
      dateOfMeeting: ['', [Validators.required]],
      attendees: ['', [Validators.required]]
       
    }) 

    this.heroes$ = this.route
      .queryParams
      .subscribe(params => {
        var IdParam = this.route.snapshot.queryParamMap.get('id');
        console.log(IdParam);
        this.Id = +IdParam;
        if (this.Id > 0) {
          this.getMeetingDetails(); 
        } 
        else {
          this.subject ="";
          this.agenda = ""; 
        }
      });

  }
  get f() { return this.meetingaddform.controls; }


  getAttendees() {
    this.crudService.getAllAttendees().subscribe(data => this.attendeeslist = data);
    
    //console.log("Attendee List" + data);
  }
 
  getMeetingDetails() { 
    this.crudService.getMeetingDetails(this.Id)
      .subscribe(data => {
        this.subject = data.subject;
        this.agenda = data.agenda; 
        this.Id = data.id;
        var attendees = data.attendees;
        console.log('attendees' + attendees);
      //  this.selectedItems = attendees.split(";");
        this.attendeesArray = attendees.split(";");
        this.dateofmeeting = this.datePipe.transform(data.dateOfMeeting, 'yyyy-MM-dd');
        this.FormHeader = "Edit"

        for (var attendee in this.attendeesArray) {

          console.log('Array Started' + this.attendeesArray[attendee]);

          for (var item in this.attendeeslist) {

            console.log('Fullname' + this.attendeeslist[item].fullName);

            if (this.attendeeslist[item].fullName == this.attendeesArray[attendee]) {

              this.selectedItems.push(this.attendeeslist[item]);

              console.log(this.attendeeslist[item].fullName);
            }
          }
        }
        for (var item in this.selectedItems) {
          console.log('Array objattendees- Fullname' + this.selectedItems[item].fullName);
          console.log('Array objattendees - ID' + this.selectedItems[item].id);
        }
        console.log(JSON.stringify(this.selectedItems));

        console.log(JSON.stringify(this.attendeeslist));
        

      });
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
   
  }
  submitForm() {
    this.submitted = true;

    if (this.meetingaddform.invalid) {
      alert("Validation failed.");
      return;
    }
    if (this.Id <= 0) {
      this.crudService.create(this.meetingaddform.value).subscribe(res => {
        this.router.navigate(['/meetings']);
        alert('Meeting was created successfully');
      })
    } else {
      this.crudService.update(this.meetingaddform.value).subscribe(res => {
        this.router.navigate(['/meetings']);
        alert('Meeting was updated successfully');
      })
        }
  }
  
  ResetValues() {
    this.subject = "";
    this.agenda = "";
    this.dateofmeeting = "";
    this.Id = -1;
    this.FormHeader = "Add New"
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
 
