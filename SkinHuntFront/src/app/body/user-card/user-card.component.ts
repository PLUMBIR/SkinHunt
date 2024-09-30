import { Component, input } from '@angular/core';
import { User } from '../../models/User';

@Component({
  selector: 'user-card',
  standalone: true,
  imports: [],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.scss'
})
export class UserCardComponent {
  user = input.required<User>();
}
