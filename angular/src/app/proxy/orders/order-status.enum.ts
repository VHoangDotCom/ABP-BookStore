import { mapEnumToOptions } from '@abp/ng.core';

export enum OrderStatus {
  PENDING = 0,
  AVAILABLE = 1,
  PROCESSING = 2,
  COMPLETED = 3,
  CANCELED = 4,
  REFUND = 5,
  COMPLAINT = 6,
}

export const orderStatusOptions = mapEnumToOptions(OrderStatus);
