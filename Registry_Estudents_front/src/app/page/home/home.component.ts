import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { DataEstudentsService } from '../../services/data-estudents.service';
import { Router ,RouterModule} from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { CourseByEstudent } from '../../models/CourseByEstudent.model';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  course:CourseByEstudent[] =[] 
  route = inject(Router);
  dataService = inject(DataEstudentsService)
  platformId = inject(PLATFORM_ID);
  ngOnInit(){
    this.getCourseByEstudent();
  }
  getCourseByEstudent(){
      
      if(isPlatformBrowser(this.platformId)){ 
        
        const {id} = JSON.parse(sessionStorage.getItem("user") || '""')
        console.log("id", id)
        this.dataService.getCourseByEstudent("getcourseByEstudent",id).subscribe({
          next:(data:any)=>{
            this.course = data;
        },
        error:(error)=>{
          console.log(error)
        }
      })
    }
    }
    irAcurso(id:number){
      this.route.navigate([`/courses/${id}`])
    }
    eliminarCurso(idEstudents:any,idCourse:any){
      alert("seguro quieres eliminar el curso?");
      this.dataService.deleteCourseById("deleteCourseById",idEstudents,idCourse).subscribe({
        next:(data:any)=>{
          console.log("data: ", data)
          this.getCourseByEstudent();
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
