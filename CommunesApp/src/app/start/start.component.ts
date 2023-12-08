import { Component, OnInit } from '@angular/core';
import { StartService } from './start.service';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css']
})
export class StartComponent implements OnInit{
  message: string | undefined;

  constructor(private startService: StartService) {}

  ngOnInit(): void {
    this.startService.getStarters().subscribe({
      next: (respose: any) => this.message = respose.value.message,
      error: error => console.log(error)
    })
  }

}
