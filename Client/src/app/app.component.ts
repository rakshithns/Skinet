import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Client';

  constructor(private basketService: BasketService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadcurrentUser();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(response => {
        console.log('Initialised');
      }, error => {
        console.log(error);
      });
    }
  }

  loadcurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log('user loaded');
      }, error => {
        console.log(error);
      });
  }
}
