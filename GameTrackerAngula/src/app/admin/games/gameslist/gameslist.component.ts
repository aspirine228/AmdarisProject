import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { GameService } from 'src/app/_service/game.service';
import { MaterialModule } from 'src/app/material/material.module';
import { merge, Observable } from 'rxjs';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { Game } from 'src/app/_models/Game/game';
import { DataSource } from '@angular/cdk/table';
import { PagedResult } from 'src/app/_infrastructure/models/PagedResult';
import {  MatDialog } from '@angular/material/dialog';
import {  MatSnackBar } from '@angular/material/snack-bar';
import {  MatPaginator } from '@angular/material/paginator';
import {  MatSort } from '@angular/material/sort';
import { PaginatedRequest } from 'src/app/_infrastructure/models/PaginatedRequest';
import { Filter } from 'src/app/_infrastructure/models/Filter';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import { RequestFilters } from 'src/app/_infrastructure/models/RequestFilters';
import { FilterLogicalOperators } from 'src/app/_infrastructure/models/FilterLogicalOperators';
import { AdminComponent } from '../../admin/admin.component';


@Component({
  selector: 'app-gameslist',
  templateUrl: './gameslist.component.html',
  styleUrls: ['./gameslist.component.css']
})
export class GameslistComponent implements OnInit {
  pagedGames!: PagedResult<Game>;
  testGame!:Game;
  public gameForm!:FormGroup;
  tableColumns: TableColumn[] = [
    { name: 'timePlayed', index: 'timePlayed', displayName: 'When' },
    { name: 'phoneNumber', index: 'phoneNumber', displayName: 'Phone', useInSearch: true },
    { name: 'try1', index: 'try1', displayName: 'Fist Try' },
    { name: 'try2', index: 'try2', displayName: 'Second Try' },
    { name: 'scenario', index: 'scenario', displayName: 'Scenario' },
    { name: 'prize', index: 'prize', displayName: 'Prize' }
  ];

  displayedColumns!: string[];

  searchInput = new FormControl('');
  filterForm!: FormGroup;
  gamelist!:Game[];
  list2!:Observable<Game>;
  requestFilters!: RequestFilters;

  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false}) sort!: MatSort;
  
  constructor(private gameService:GameService,
    private formBuilder:FormBuilder,
    public dialog:MatDialog,
    private admin:AdminComponent,
    
    public snackBar:MatSnackBar
    ) {
      this.displayedColumns=this.tableColumns.map(column=>column.name);
      this.filterForm=this.formBuilder.group({
        timePlayed:[Date],
        phoneNumber:[''],
        try1:[0],
        try2:[0],
        scenario:[''],
        prize:[''],
      });
      
     }

     ngOnInit() {
     
    if(this.admin.roleName=='admin')
    {
    this.loadGamesFromApi();
    }
    if(this.admin.roleName=='gamer')
    {
      this.loadGamesForUser();
    }
    if(this.admin.roleName=='company')
    {
      this.loadGamesForCompany();
    }


    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

   merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
   
     this.loadGamesFromApiPaged();
  
     });
  }

  loadGamesFromApiPaged() {
    const paginatedRequest = new PaginatedRequest(this.paginator, this.sort, this.requestFilters);
    this.gameService.getGamesPaged(paginatedRequest)
      .subscribe((pagedGames: PagedResult<Game>) => {
        this.pagedGames = pagedGames ;
      });
  }

  loadGamesFromApi() {  
    this.gameService.getGames()
      .subscribe((list: Game[]) => {        
        this.gamelist = list ;
      });
  }

  loadGamesForUser() {  
    this.gameService.getGamesForUser(this.admin.phoneNumber)
      .subscribe((list: Game[]) => {        
        this.gamelist = list ;
      }); 
  }

  loadGamesForCompany() {  
    this.gameService.getGamesForCompany(this.admin.username)
      .subscribe((list: Game[]) => {        
        this.gamelist = list ;
      }); 
  }




  resetGrid() {
    this.requestFilters = {filters: [], logicalOperator: FilterLogicalOperators.And};
    this.loadGamesFromApiPaged();
  }
  applySearch() {
    this.createFiltersFromSearchInput();
    this.loadGamesFromApiPaged();
  }
  filterBooksFromForm() {
    this.createFiltersFromForm();
    this.loadGamesFromApiPaged();
  }
  private createFiltersFromForm() {
    if (this.filterForm.value) {
      const filters: Filter[] = [];

      Object.keys(this.filterForm.controls).forEach(key => {
        const controlValue = this.filterForm.controls[key].value;
        if (controlValue) {
          const foundTableColumn = this.tableColumns.find(tableColumn => tableColumn.name === key);
          const filter: Filter = { path : foundTableColumn!.index, value : controlValue };
          filters.push(filter);
        }
      });

      this.requestFilters = {
        logicalOperator: FilterLogicalOperators.And,
        filters
      };
    }
  }

  private createFiltersFromSearchInput() {
    const filterValue = this.searchInput.value.trim();
    if (filterValue) {
      const filters: Filter[] = [];
      this.tableColumns.forEach(column => {
        if (column.useInSearch) {
          const filter: Filter = { path : column.index, value : filterValue };
          filters.push(filter);
        }
      });
      this.requestFilters = {
        logicalOperator: FilterLogicalOperators.Or,
        filters
      };
    } else {
      this.resetGrid();
    }
  }
}
