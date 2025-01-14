import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: 'login',
        loadComponent: () => import('./page/login/login.component').then( m => m.LoginComponent)
      },
      {
        path: 'registro',
        loadComponent: () => import('./page/register/registro.component').then( m => m.RegistroComponent)
      },
      {
        path: 'home',
        loadComponent: () => import('./page/home/home.component').then( m => m.HomeComponent)
      },
      {
        path: 'registercourse',
        loadComponent: () => import('./page/register-course/register-course.component').then( m => m.RegisterCourseComponent)
      },
      {
        path: 'courses/:id',
        loadComponent: () => import('./page/courses/courses.component').then( m => m.CoursesComponent)
      },
];
