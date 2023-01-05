import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ButtonEventRs } from '../button-event-rs';
import { RowAttrPckt } from '../row-attr-pckt';
import { TableContent } from '../table-content.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

export class SearchComponent {
  @Output() triggerReset = new EventEmitter<any>();
  @Output() triggerSearch = new EventEmitter<any>();
  displayedColumns: string[] = ['attribute', 'conditions'];
  addableContent = '+';
  buttonLabelSearch = 'Search';
  buttonLabelReset = 'Reset';
  dataSource = Object.values(TableContent).map((v) => JSON.parse(v));
  httpSearchData: RowAttrPckt[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  rsEvent(pckt: ButtonEventRs) {
    if (pckt.reset) {
      this.triggerReset.emit();
    }
    else if (pckt.search) {
      this.triggerSearch.emit();
    }
  }

  collectRowElements(conds: RowAttrPckt) {
    this.httpSearchData.push(conds);
    if (this.httpSearchData.length === this.dataSource.length) {
      this.postFilters();
      this.router.navigate(['/quadruplexes'], { queryParams: { r: 'search' } });
    }
  }

  postFilters() {
    this.http.post('http://localhost:5000/api/Search/PostFilters',
      this.httpSearchData);
    this.httpSearchData.splice(0);
  }
}
