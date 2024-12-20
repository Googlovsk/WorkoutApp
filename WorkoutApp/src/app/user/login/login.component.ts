import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Route, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styles: ``
})
export class LoginComponent {
  isSubmitted:boolean = false;
  form:FormGroup;

  constructor(public formbulder:FormBuilder, private service:AuthService, private router:Router, private toastr:ToastrService){
    this.form = this.formbulder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

 

  onSubmit(){
    this.isSubmitted = true;
    if (this.form.valid){
      this.service.singin(this.form.value).subscribe({
        next:(res:any)=>{
          localStorage.setItem('token',res.token);
          this.router.navigateByUrl('/dashboard');
        },
        error:err=>{
          if(err.status==400){
            this.toastr.error('Некорректный логин или пароль', 'Ошибка')
          }
          else{
            console.log('error during login:\n', err)
          }
        }
      })
    }
    else{

    }
  }

  hasDisplayableError(controlName: string):Boolean{
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched))
  }
}
