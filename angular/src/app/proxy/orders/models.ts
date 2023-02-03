import type { OrderStatus } from './order-status.enum';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateOrderDto {
  sku?: string;
  fullName: string;
  phoneNumber?: string;
  email: string;
  address: string;
  totalCheckOut: number;
  status: OrderStatus;
}

export interface GetOrderListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface OrderDto extends EntityDto<string> {
  sku?: string;
  fullName?: string;
  phoneNumber?: string;
  email?: string;
  address?: string;
  totalCheckOut: number;
  status: OrderStatus;
}

export interface UpdateOrderDto {
  fullName: string;
  phoneNumber?: string;
  email: string;
  address: string;
  totalCheckOut: number;
  status: OrderStatus;
}
