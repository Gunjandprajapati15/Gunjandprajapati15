import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LinkedinService } from '../services/linkedin.service';
import { Observable } from 'rxjs';
import { Post } from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class PostListResolverService implements Resolve<any> {

  constructor(private linkedinService: LinkedinService) { }

  /* resolver created for page will not display until load all the post  */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Post[]> {
    return this.linkedinService.listPosts();
  }


}
