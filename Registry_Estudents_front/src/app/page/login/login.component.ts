import { Component, inject, OnInit } from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {ReactiveFormsModule, Validators} from '@angular/forms';
import {FormGroup, FormControl} from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Logon } from '../../models/Logon.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  
  authService = inject(AuthService);
  route = inject(Router);
  login = new FormGroup({
    mail: new FormControl('',Validators.email),
    password: new FormControl('',Validators.required),
  });
  ngOnInit(): void {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("user");
  }
  logon(){
    let data = {
      mail:this.login.controls.mail.value,
      password:this.login.controls.password.value
    }
    this.authService.LogonEstudents(data,"logonEstudents").subscribe({
      next:(response:Logon) =>{
        const {isSucces,user,token} = response;
        if(isSucces){
          sessionStorage.setItem("token",token);
          sessionStorage.setItem("user",JSON.stringify(user))
          this.route.navigate(['/registercourse'])
        }else {
          alert("Correo y/o contraseña incorrectos");
        }
    },error:error=>{
      console.log("error :" , error)
    }})
  }
}
