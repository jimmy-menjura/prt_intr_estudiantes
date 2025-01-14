import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { DataEstudentsService } from '../../services/data-estudents.service';
import { Router, RouterModule } from '@angular/router';
import { CourseByEstudent } from '../../models/CourseByEstudent.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css'
})
export class CoursesComponent implements OnInit{
  courseById:CourseByEstudent[] = [];
  route = inject(Router);
  router = inject(ActivatedRoute);
  dataService = inject(DataEstudentsService)
  id:any
  platformId = inject(PLATFORM_ID);
  ngOnInit(){
    this.getCourseById();
  }
  getCourseById(){
        this.id = this.router.snapshot.paramMap.get('id');
        console.log("id", this.id)
        this.dataService.getCourseById("getcourseById",parseInt(this.id)).subscribe({
          next:(data:any)=>{
            this.courseById = data;
            console.log(this.courseById)
        },
        error:(error)=>{
          console.log(error)
        }
      })
    }
    logout(){
      sessionStorage.removeItem("token");
      sessionStorage.removeItem("user");
      this.route.navigate([`/login`]);
    }
}
