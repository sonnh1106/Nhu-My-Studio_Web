import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/core/services/baseService/base.service';
import { EventEntity } from 'src/app/data/entity/Event';

@Injectable()
export class EventService extends BaseService<EventEntity> {
    
}
