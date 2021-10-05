import { Component, OnInit } from '@angular/core';
import {  MatPaginator } from '@angular/material/paginator';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import {  MatSort } from '@angular/material/sort';
import { CompanyService } from 'src/app/_service/company.service';
import { Company } from 'src/app/_models/Company/company';

@Component({
  selector: 'app-companies-list',
  templateUrl: './companies-list.component.html',
  styleUrls: ['./companies-list.component.css']
})
export class CompaniesListComponent implements OnInit {
 
  companylist!:Company[];
  displayedColumns!: string[]; 
  tableColumns: TableColumn[] = [
    { name: 'id', index: 'id', displayName: 'ID' },
    { name: 'companyName', index: 'companyName', displayName: 'Company Name', useInSearch: true },
    { name: 'contractStart', index: 'contractStart', displayName: 'Contract Starts', useInSearch: true },
    { name: 'contractEnd', index: 'contractEnd', displayName: 'Contract Ends' },  
  ];
  constructor(private companySerice:CompanyService) { 
    this.displayedColumns=this.tableColumns.map(column=>column.name);
  }

  ngOnInit(): void {
    this.getCompanies();
  }
  getCompanies(){
    this.companySerice.getCompanies().subscribe((list: Company[]) => {
        
      this.companylist = list ;

    });
  }
}
