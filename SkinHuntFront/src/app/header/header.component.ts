import { AuthService } from './../services/auth.service';
import {MdbCollapseModule} from 'mdb-angular-ui-kit/collapse';
import {MdbRippleModule} from 'mdb-angular-ui-kit/ripple';
import { MdbDropdownModule} from 'mdb-angular-ui-kit/dropdown';
import {ChangeDetectionStrategy, Component, computed, inject, signal} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SignInComponent} from './sign-in/sign-in.component';
import {MdbModalModule, MdbModalRef, MdbModalService} from 'mdb-angular-ui-kit/modal';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    MdbCollapseModule,
    MdbRippleModule,
    MdbDropdownModule,
    MdbModalModule,
    RouterModule,
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class HeaderComponent {
  signInModalRef: MdbModalRef<SignInComponent> | null = null;

  router: Router = inject(Router);

  userAvatar$ = computed(() => {
    if (this.authService.user$()?.avatar) {
      return this.authService.user$()?.avatar;
    }

    return "../../../assets/unAuthAvatar.jpg";
  });

  adminMenu$ = computed(() => {
    const roles = this.authService.user$()?.roles;

    return roles?.includes('admin')
  });

  userBalance$ = computed(() => {
    return this.authService.user$()?.balance;
  });

  constructor (
    private modalService: MdbModalService,
    public authService: AuthService
  ) { }

  openSignInModal() {
    this.signInModalRef = this.modalService.open(SignInComponent, {
      modalClass: 'modal-dialog-centered'
    })
  }

  logOutHandler() {
    this.authService.logOut();
    this.router.navigate(['']);
  }
}
