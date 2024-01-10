import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve rates from the server', () => {
    const mockRates = [
      { name: 'Euro', code: 'EUR', rate: 4.5 , effectiveDate: '2024-01-01' },
      { name: 'funt szterling', code: 'GBP', rate: 5.0, effectiveDate: '2024-01-01' }
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('/currencyexchangerate');
    expect(req.request.method).toEqual('GET');
    req.flush(mockRates);

    expect(component.rates).toEqual(mockRates);
  });
});
