import { Component, inject } from '@angular/core';
import {RouterModule} from '@angular/router';
import {ReactiveFormsModule, Validators} from '@angular/forms';
import {FormGroup, FormControl} from '@angular/forms';
import { DataEstudentsService } from '../../services/data-estudents.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.css'
})
export class RegistroComponent {
  service = inject(DataEstudentsService);
  route = inject(Router);
  errorEmail:Boolean = false;
  register = new FormGroup({
    name:new FormControl('',Validators.required),
    phone:new FormControl('',Validators.required),
    mail: new FormControl('',Validators.email),
    password: new FormControl('',Validators.required),
  });

  record(){
    
    let data = {
      name:this.register.controls.name.value,
      phone:this.register.controls.phone.value,
      mail:this.register.controls.mail.value,
      password:this.register.controls.password.value
    }
    this.service.saveEstudents(data,"SaveEstudents").subscribe({
      next:(response) =>{
        if(response){
          this.route.navigate(['/login'])
        }
    },error:error=>{
      console.log("error :" , error)
    }})
    
  }
  
  
}
