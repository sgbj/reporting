import { HttpClient, HttpClientModule } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { EditorModule } from '@tinymce/tinymce-angular';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
    EditorModule,
  ],
})
export class AppComponent {
  title = 'angularapp';
  reports = [
    { name: 'Inventory Report', url: '/api/inventory' },
    { name: 'Sales Report', url: '/api/sales' },
  ];
  reportControl = new FormControl<any>(null, Validators.required);
  htmlControl = new FormControl('');

  constructor(private http: HttpClient) {}

  onSubmit() {
    this.http
      .get(this.reportControl.value.url, { responseType: 'text' })
      .subscribe((value) => this.htmlControl.setValue(value));
  }
}
