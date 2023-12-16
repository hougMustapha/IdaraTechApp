import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/shared/models/account/user';
import { take } from 'rxjs';
import { RegisterWithExternal } from 'src/app/shared/models/account/registerWithExternal';

@Component({
  selector: 'app-register-with-third-party',
  templateUrl: './register-with-third-party.component.html',
  styleUrls: ['./register-with-third-party.component.css']
})
export class RegisterWithThirdPartyComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;
  provider: string | null = null;
  access_token : string | null = null;
  userId: string | null = null;
  errorMessages: string[] = [];
  
  constructor(private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.accountService.user$.pipe(take(1)).subscribe({
      next: (user: User | null) => {
        if (user) {
          this.router.navigateByUrl("/");
        } else {
          this.activatedRoute.queryParamMap.subscribe({
            next: (params: any) => {
              this.provider = this.activatedRoute.snapshot.paramMap.get('provider');
              this.access_token = params.get('access_token');
              this.userId = params.get('userId');

              if (this.provider && this.access_token && this.userId && 
                (this.provider === 'facebook' || this.provider === 'google')) {
                  this.initializeForm();
                } else {
                  this.router.navigateByUrl('/account/register');
                }
            }
          })
        }
      }
    })
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      firstname: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(15)]],
      lastname: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(15)]]
    });
  }

  register() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.registerForm.valid && this.userId && this.access_token && this.provider) {
      const firstName = this.registerForm.get('firstname')?.value;
      const lastName = this.registerForm.get('lastname')?.value;

      const model = new RegisterWithExternal(firstName, lastName, this.userId, this.access_token, this.provider);
      this.accountService.registerWithThirdParty(model).subscribe({
        next: response => {
          this.router.navigateByUrl('/');
        }, error: error => {
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
          } else {
            this.errorMessages.push(error.error);
          }
        }
      })
    }
  }

}
