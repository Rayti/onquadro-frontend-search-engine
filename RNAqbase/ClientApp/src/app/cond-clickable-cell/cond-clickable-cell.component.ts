import { Component, Input, EventEmitter, Output, SimpleChanges, SimpleChange } from '@angular/core';
import { CondCommPckt } from '../cond-comm-pckt';
import { Condition } from '../condition';
import { RowCommPckt } from '../row-comm-pckt';

@Component({
  selector: 'app-cond-clickable-cell',
  templateUrl: './cond-clickable-cell.component.html',
  styleUrls: ['./cond-clickable-cell.component.css']
})
export class CondClickableCellComponent {
  @Input('ID') attrID: string;
  @Input() condData: Condition;
  @Input() eventReceiver: RowCommPckt;
  @Output() clicked = new EventEmitter<CondCommPckt>();
  isClicked: boolean;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['eventReceiver']) {
      this.eventRecv(this.eventReceiver);
    }
  }

  ngOnInit() {
    this.isClicked = false;
    if (this.condData.condition == 'any') {
      this.clickEvent();
    }
  }

  eventRecv(pckt: RowCommPckt) {
    if (pckt.clickInvoker != '') {
      if (pckt.clickInvoker == 'row') {
        if (pckt.eventReceiver == this.condData.condition) {
          this.isClicked = true;
        }
        else if (pckt.eventReceiver != this.condData.condition) {
          this.isClicked = false;
        }
      }
      else {
        if ((pckt.eventReceiver == 'any') && (this.condData.condition == 'any')) {
          this.isClicked = false;
        }
        else if ((pckt.clickInvoker == 'any') && (this.condData.condition != 'any')) {
          this.isClicked = false;
        }
        else if ((pckt.clickInvoker != this.condData.condition) && (pckt.typeOfRow == 'radioSelect')) {
          this.isClicked = false;
        }
      }
    }
  }

  clickEvent() {
    if (this.isClicked) {
      this.isClicked = false;
      const pckt = <CondCommPckt>{ clickInvoker: this.condData.condition, clicked: false };
      this.clicked.emit(pckt);
    }
    else {
      this.isClicked = true;
      const pckt = <CondCommPckt>{ clickInvoker: this.condData.condition, clicked: true };
      this.clicked.emit(pckt);
    }
  }
}
