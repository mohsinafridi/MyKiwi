import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  values: any;
  constructor(private router: Router) { }

  ngOnInit() {
    // this.getValues();
  }

  registerToggle() {
    this.registerMode = true;
  }

cancelRegisterMode(registerMode: boolean) {
       this.registerMode = registerMode;
}
goToLearnMore() {
  this.router.navigate(['learnmore']);
}
}
