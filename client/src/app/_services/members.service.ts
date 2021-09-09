import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  BaseUrl = environment.apiUrl;
  members : Member[] =[];
  

  constructor(private http : HttpClient) { }
  getMembers(){
    if(this.members.length > 0)
    {
      console.log(this.members +"THis is beofr if");
      return of(this.members);
    } 
    return this.http.get<Member[]>(this.BaseUrl + 'users').pipe(
      map(members =>{
        members.forEach(element => {
          console.log(element.username +"THis is after if");
        });
        this.members = members;
        
        return this.members;
      })
    );
  }

  getMember(username :string){
    const member = this.members.find(x=>x.username === username);
    if(member !== undefined) 
    {
      console.log('member'+member.username);
      return of (member);
    }
    return this.http.get<Member>(this.BaseUrl + 'users/' + username);
  }

  updateMember(member:Member){
    return this.http.put(this.BaseUrl+'users',member).pipe(
      map(()=>{
        const index = this.members.indexOf(member);
        this.members[index] = member;
         }
      ));
  }
}
