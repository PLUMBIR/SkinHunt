import { AdminService } from './../../services/admin.service';
import { Component, signal, OnInit, inject } from '@angular/core';
import { User } from '../../models/User';
import { UserCardComponent } from '../user-card/user-card.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    UserCardComponent,
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})

export class AdminComponent implements OnInit {
  adminService = inject(AdminService);
  usersList$ = signal<User[]>([]);

  ngOnInit() {
    this.adminService.getUsers()
      .subscribe((users) => {
        this.usersList$.set(users);
      });
  }
}
