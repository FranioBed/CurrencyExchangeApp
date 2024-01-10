import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface CurrencyExchangeRate {
  name: string;
  code: string;
  rate: number;
  effectiveDate: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public rates: CurrencyExchangeRate[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCurrencyExchangeRates();
  }

  getCurrencyExchangeRates() {
    this.http.get<CurrencyExchangeRate[]>('/currencyexchangerate').subscribe(
      (result) => {
        this.rates = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'nbpcurrencyexchange.client';
}
