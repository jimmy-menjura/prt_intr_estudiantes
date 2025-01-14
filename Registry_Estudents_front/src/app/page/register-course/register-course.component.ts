import { Component, inject, PLATFORM_ID } from '@angular/core';
import { DataEstudentsService } from '../../services/data-estudents.service';
import { CourseByTeacher } from '../../models/CourseByTeacher.model';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-register-course',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './register-course.component.html',
  styleUrl: './register-course.component.css'
})
export class RegisterCourseComponent {
  // course:CourseByTeacher[]=[] ;
  course:any[]=[] ;
  register:FormGroup;
  platformId = inject(PLATFORM_ID);
  route = inject(Router);
  fb = inject(FormBuilder);
  dataService = inject(DataEstudentsService)
  validateData:Boolean = false;
  constructor(){
    this.register = this.fb.group({
      courses:this.fb.array([])
      });
  }
  ngOnInit(){
    this.getCourseByEstudent();
  }

  getCourseByEstudent(){
    if(isPlatformBrowser(this.platformId)){ 
      const {id} = JSON.parse(sessionStorage.getItem("user") || '""')
      console.log("id", id)
      this.dataService.getCountCourseByEstudent("getcourseRegisterByEstudent",id).subscribe({
        next:(data:any)=>{
          if(data >= 3){
            this.route.navigate(['/home'])
            this.validateData = false;
          }
          if(data === 2){
            alert("Aun puede registrar otra materia")
            this.validateData = true;
            this.getCourseByTeacher()
          }
          else {
            this.validateData = true
          this.getCourseByTeacher()
        }
      },
      error:(error)=>{
        console.log(error)
      }
    })
    }
  }
  getCourseByTeacher(){
    this.dataService.getCourseByTeacher("getcourseByTeacher").subscribe({
      next:(data)=>{
        this.course = this.formatCourseData(data); 
        this.course.forEach(course => this.agregarCurso(course));
      },
      error:(error)=>{
        console.log(error)
      }
    })
  }
  record(){
      const cursosSeleccionados = this.register.value.courses.filter((course:any) => course.selected);   
    this.dataService.saveCourse(cursosSeleccionados,"saveCourseByEstudents").subscribe({
      next:(response) =>{
        if(response){
          let successResponse = response.filter((rta:any,i:any)=>response[i] === 'Registro exitoso')
          let failedResponse = response.filter((rta:any,i:any)=>response[i] === 'Ya todos los cursos han sido registrados')
          let warningResponse = response.filter((rta:any,i:any)=>response[i] === 'No puedes tener mas de dos profesores en una materia')
          if(successResponse.length >= 1)
            alert(`Registraste: ${successResponse.length}`)
          if(failedResponse.length >= 1){
            alert(failedResponse[0])
          }
          if(warningResponse.length >= 1)
            alert(`No pudiste registrar un curso:`)
          this.route.navigate(['/home'])
        }
    },error:error=>{
      console.log("error :" , error)
    }})
  }
  
  validateCheckboxes(formArray: FormArray) {  
    const checkedCount = formArray.controls.filter(control => control.value).length;  
    return checkedCount <= 3 ? null : { maxChecked: true }; 
  }  
  get courses(): FormArray {  
    return this.register.get('courses') as FormArray;  
  }  

  formatCourseData(data: any[]): any[] {  
    return data.map(item => ({  
      id:item.id,
      course:item.course,
      teacher:item.teacher,
      idCourse: item.idCourse,  
      idTeacher: item.idTeacher,  
      noCredits: item.noCredits,
      idNoCredits: item.idNoCredits  
    }));  
  }  

  agregarCurso(cours:any): void {  
    if(isPlatformBrowser(this.platformId)){ 
      const {id} = JSON.parse(sessionStorage.getItem("user") || '""')
      console.log("id", id)
    this.courses.push(this.fb.group({ 
      id:[cours.id], 
      course:[cours.course],
      teacher:[cours.teacher],
      idCourse: [cours.idCourse],  
      idTeacher: [cours.idTeacher],  
      NoCredits: [cours.noCredits],
      idNoCredits: [cours.idNoCredits],
      idEstudents: id,
      selected:false
    }));  
  }
  }  


}
