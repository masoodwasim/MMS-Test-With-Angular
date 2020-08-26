import { Component, OnInit } from '@angular/core';
import { MeetingsModel, MeetingsService } from '../Services/meetings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-meetings',
  templateUrl: './meetings.component.html',
  styleUrls: ['./meetings.component.css']
})
export class MeetingsComponent implements OnInit {

  public meetinglist: MeetingsModel;
   
  constructor(private serviceMeetings: MeetingsService, private router: Router) { 
    this.getAllMeetings();
  }
  
  ngOnInit() {
  }
  getAllMeetings() {
    this.serviceMeetings.getAllMeetings().subscribe(data => this.meetinglist = data)

  }
  editMeeting(meeting: any) {
    this.router.navigate(['/meeting-edit'], { queryParams: meeting });
    meeting.editable = !meeting.editable;
    console.log(meeting);
  }
}
