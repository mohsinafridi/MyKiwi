import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  constructor(private authService: AuthService) { }

 // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registeration done.');
    }, error  => {
      console.log('registeration Error');
    });
  }

 cancel() {
    this.cancelRegister.emit(false);
  }

}
