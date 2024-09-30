import { AuthService } from './../../services/auth.service';
import { Component, ViewChild, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import {MdbFormsModule} from 'mdb-angular-ui-kit/forms';
import { MdbTabsComponent, MdbTabsModule } from 'mdb-angular-ui-kit/tabs';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { MdbValidationModule } from 'mdb-angular-ui-kit/validation';

export  const  PasswordValidator:  ValidatorFn  = (control:AbstractControl):  ValidationErrors|  null  =>{
  const  password  =  control.get('passwordSignUp');
  const  confirmpassword  =  control.get('confirmPasswordSignUp');

  if (password  &&  confirmpassword  &&  password.value  !==  confirmpassword.value){
    return { passwordMismatch :  true }
  }

  return  null;
}

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [
    CommonModule,
    MdbFormsModule,
    MdbTabsModule,
    MdbValidationModule,
    ReactiveFormsModule,
  ],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})

export class SignInComponent {

  @ViewChild('tabs') tabs!: MdbTabsComponent;

  errorLogIn$ = signal<boolean>(false);

  signInForm = new FormGroup({
    email: new FormControl<string>('', { validators: [Validators.required, Validators.email], updateOn: 'change' }),
    password: new FormControl<string>('', { validators: [Validators.required, Validators.minLength(8)], updateOn: 'change' }),
  });

  signUpForm = new FormGroup({
    email: new FormControl<string>('', { validators: [Validators.required, Validators.email], updateOn: 'change' }),
    password: new FormControl<string>('', { validators: [Validators.required, Validators.minLength(8)], updateOn: 'change' }),
    phoneNumber: new FormControl<string>('', { validators: [Validators.required, Validators.minLength(13), Validators.maxLength(13)], updateOn: 'change' })
  },
    {
      validators: PasswordValidator,
    });

  constructor(
    public modalRef: MdbModalRef<SignInComponent>, 
    private readonly authService : AuthService
   ) {
  }

  get SignInEmail() {
    return this.signInForm.controls.email;
  }

  get SignInPassword() {
    return this.signInForm.controls.password;
  }

  get SignUpEmail() {
    return this.signUpForm.controls.email;
  }

  get SignUpPassword() {
    return this.signUpForm.controls.password;
  }

  get SignUpPhoneNumber() {
    return this.signUpForm.controls.phoneNumber;
  }

  setActiveTab() {
    this.tabs.setActiveTab(1);
  }

  onSubmitSignIn() {
    const command = {
      email: this.SignInEmail.value!,
      password: this.SignInPassword.value!,
    };

    this.authService.signIn(command).subscribe((result) => {
      if (result) {
        this.modalRef.close();
      }
      else{
        this.errorLogIn$.set(true);
      }
    });
  }

  onSubmitSignUp() {
    const command = {
      email: this.SignUpEmail.value!,
      password: this.SignUpPassword.value!,
      phoneNumber: this.SignUpPhoneNumber.value!
    };

    this.authService.signUp(command).subscribe((result) => {
      if (result) {
        this.modalRef.close();
      }
      else{
        this.errorLogIn$.set(true);
      }
    });
  }
}
