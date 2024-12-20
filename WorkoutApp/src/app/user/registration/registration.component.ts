import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators} from '@angular/forms'
import { FirstKeyPipe } from '../../shared/pipes/first-key.pipe';
import { AuthService } from '../../shared/services/auth.service';
import { error, log } from 'console';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe, RouterLink],
  templateUrl: './registration.component.html',
  styles: ``
})
export class RegistrationComponent {
  form: FormGroup;
  isSubmitted:boolean = false;
  
  constructor(public formBuilder: FormBuilder, private service:AuthService, private toastr:ToastrService) 
    {
    this.form = this.formBuilder.group(
      {
        fullName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        phone: ['', Validators.required],
        password: ['', 
          [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(/(?=.*[^a-zA-Z0-9 ])/)
          ]
        ],
        confirmPassword: [''],
      },
      {validators:this.passwordMatchValidator}
    );
  }

  passwordMatchValidator: ValidatorFn = (control: AbstractControl) => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (password && confirmPassword && password.value != confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    } else {
      confirmPassword?.setErrors(null);
    }
    return null;
  };
  

  onSubmit(){
    this.isSubmitted = true;
    if(this.form.valid){
      const formData = this.form.getRawValue();
      this.service.createUser(formData)
      .subscribe({
        next:(res:any)=>{
          if(res.succeeded){
            this.form.reset();
            this.isSubmitted = false;
            this.toastr.success('Успешная регистрация!',)
          }
        },
        error:err=>{
          if(err.error.errors){
            err.error.errors.forEach((x:any) => {
              switch(x.code){
                case 'DuplicateEmail':
                  this.toastr.error('Этот адрес электронной почты уже занят, введите другой')
                  break;
                case 'DuplicateUserName':
                  break;
                default:
                  this.toastr.error('Что-то пошло не так. Свяжитесь с разработчиком')
                  console.log(x);
                  break;
  
              }
            });
          } else{
            console.log('error:', err)
          }
        }
      })
    }
  }
  hasDisplayableError(controlName: string):Boolean{
    const control = this.form.get(controlName);
    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched))
  }
}
