import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Component, Inject } from "@angular/core";
import { Observable } from "rxjs";

export class MeetingsService {
  public meetings: MeetingsModel[];
  public attendees: AttendeesModel[];
  appURL: string = "";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':'application/json'
    })

  }
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<MeetingsModel[]>(baseUrl + 'meetings').subscribe(result => {
  //    this.meetings = result;
  //  }, error => console.error(error));
  //}
  constructor(private httpobj: HttpClient, @Inject('BASE_URL') _baseurl: string) {
    this.appURL = _baseurl;
  }
  getAllMeetings(): Observable<MeetingsModel> {
    return this.httpobj.get<MeetingsModel>(this.appURL + 'meetings');

  }
  getAllAttendees(): Observable<AttendeesModel> {
    return this.httpobj.get<AttendeesModel>(this.appURL + 'attendees');

  }
  //getAttendees(): Observable<AttendeesModel[]> {
  //  return this.httpobj.get(this.appURL + 'attendees')  
  //    .map(res => res.json())
  //    .catch(this.handleError);
  //}

  create(meeting): Observable<MeetingsModel> {
    console.log(JSON.stringify(meeting));

    return this.httpobj.post<MeetingsModel>(this.appURL + 'meetings', JSON.stringify(meeting), this.httpOptions).pipe()
  }

  //private handleError(error: Response | any) {
  //  // In a real world app, we might use a remote logging infrastructure  
  //  let errMsg: string;
  //  if (error instanceof Response) {
  //    const body = error.json() || '';
  //    const err = body.toString() || JSON.stringify(body);
  //    errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
  //  } else {
  //    errMsg = error.message ? error.message : error.toString();
  //  }
  //  console.error(errMsg);
  //  return Observable.throw(errMsg);
  //}  

}
export class  MeetingsModel {
  id: number;
  subject: string;
  agenda: string;
  dateOfMeeting: string;
  attendees: string;
}

export class AttendeesModel {
  //id: number;
  //firstName: string;
  //lastName: string;
  //isActive: boolean;
  fullName: string;
}
