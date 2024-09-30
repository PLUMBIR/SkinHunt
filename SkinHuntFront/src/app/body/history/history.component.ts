import { Component, OnInit, inject, signal } from '@angular/core';
import { SideNavComponent } from '../side-nav/side-nav.component';
import { TransactionModel } from '../../models/transactionModel';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
    selector: 'app-history',
    standalone: true,
    templateUrl: './history.component.html',
    styleUrl: './history.component.scss',
    imports: [
        SideNavComponent,
    ]
})
export class HistoryComponent implements OnInit{
  userService = inject(UserService);
  authService = inject(AuthService);

  transactionsList$ = signal<TransactionModel[]>([]);

  ngOnInit() {
    const userId = this.authService.user$()?.id;

    if (userId) {
      this.userService.getTransactions(userId)
        .subscribe((res) => {
          this.transactionsList$.set(res.map((item) => {
            return {
              ...item,
              createdDate: new Date(item.createdDate),
            }
          }));
        });
    }
  }
}
