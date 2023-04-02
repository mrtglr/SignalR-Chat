import { Component } from '@angular/core';
import { ChatService, MessageDto } from './shared/chat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client';

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe((receivedObj: MessageDto) => { this.addToInbox(receivedObj); });
  }

  msgDto: MessageDto = new MessageDto();
  msgInboxArray: MessageDto[] = [];

  send(): void {
    if (this.msgDto) {
      if (this.msgDto.user.length == 0 || this.msgDto.msgText.length == 0) {
        window.alert("Both fields are required.");
        return;
      } else {
        this.chatService.broadcastMessage(this.msgDto);
      }
    }
  }

  addToInbox(obj: MessageDto) {
    let newObj = new MessageDto();
    newObj.user = obj.user;
    newObj.msgText = obj.msgText;
    this.msgInboxArray.push(newObj);

  }
}
