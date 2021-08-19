import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Injectable()
export class ListformatInterceptor implements HttpInterceptor {

  users : any;
  listx : Array<any> = [];
  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      filter(x=> x instanceof HttpResponse && request.url.includes('users'), console.log(request.url.includes('users'))),
      map((event : any)=>event.clone({body: event.body})

      )
     /* map((event : any)=> 
      {
        this.users = event.body; 
        for(let item of this.users)
        {
          console.log(item.userName);
          this.listx.push(item.userName);
          
         
  
        }
        console.log(this.listx);
      
        return event;
      }
          
        )*/
    );
  }
}
