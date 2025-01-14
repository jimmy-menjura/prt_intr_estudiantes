import { TestBed } from '@angular/core/testing';

import { DataEstudentsService } from './data-estudents.service';

describe('DataEstudentsService', () => {
  let service: DataEstudentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataEstudentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
