import { importExpr } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit,ViewChild } from '@angular/core';
import { GamerService } from 'src/app/_service/gamer.service';
import {  MatPaginator } from '@angular/material/paginator';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import {  MatSort } from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { AdminComponent } from '../../admin/admin.component';
import { Gamer } from 'src/app/_models/Gamer/gamer';

@Component({
  selector: 'app-gamers-list',
  templateUrl: './gamers-list.component.html',
  styleUrls: ['./gamers-list.component.css']
})   
export class GamersListComponent implements OnInit {
  gamerlist!:Gamer[];
  displayedColumns!: string[]; 
  tableColumns: TableColumn[] = [
    { name: 'id', index: 'id', displayName: 'ID' },
    { name: 'gamesPlayed', index: 'gamesPlayed', displayName: 'Played', useInSearch: true },
    { name: 'phoneNumber', index: 'phoneNumber', displayName: 'Phone', useInSearch: true },
    { name: 'wallet', index: 'wallet', displayName: 'Wallet' },
    { name: 'name', index: 'name', displayName: 'Name' },
    { name: 'email', index: 'email', displayName: 'Email' },
    
  ];
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false}) sort!: MatSort;

  constructor(private gamerSerivce: GamerService,
    private admin:AdminComponent) {
    this.displayedColumns=this.tableColumns.map(column=>column.name);
   }

  ngOnInit(): void {
    if(this.admin.roleName=='admin')
    {
      this.getGamers();
    }
    
    if(this.admin.roleName=='company')
    {
      this.getGamersForCompany();
    }
   
  }

  getGamers(){
    this.gamerSerivce.getGamers().subscribe((list: Gamer[]) => {
        
      this.gamerlist = list ;

    });
  }
  getGamersForCompany(){
    this.gamerSerivce.getGamersForCompany(this.admin.username).subscribe((list: Gamer[]) => {
        
      this.gamerlist = list ;

    });
  }
  
}
