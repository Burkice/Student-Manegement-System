import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { LoginService } from './login.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatInputModule, MatCardModule, ReactiveFormsModule,MatButton],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  builder = inject(FormBuilder);
  router=inject(Router);
  loginForm = this.builder.group({
    userName: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(private loginService:LoginService,
  ){}

  onLogin(){
    const userName=this.loginForm.value.userName!;
    const password=this.loginForm.value.password!;
    this.loginService.login(userName,password).subscribe((result)=>{
       console.log(result);
       localStorage.setItem("token",result.token);
       this.router.navigateByUrl('/students');
       
    });
  }
}
