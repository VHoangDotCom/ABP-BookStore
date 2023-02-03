import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { OrderService, OrderDto, orderStatusOptions } from '@proxy/orders';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class OrderComponent implements OnInit {
  order = { items: [], totalCount: 0 } as PagedResultDto<OrderDto>;

  isModalOpen = false;

  form: FormGroup;

  orderStatuses = orderStatusOptions;

  selectedOrder = {} as OrderDto;

  constructor(
    public readonly list: ListService,
    private orderService: OrderService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const orderStreamCreator = (query) => this.orderService.getList(query);

    this.list.hookToQuery(orderStreamCreator).subscribe((response) => {
      this.order = response;
    });
  }

  createOrder() {
    this.selectedOrder = {} as OrderDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editOrder(id: string) {
    this.orderService.get(id).subscribe((order) => {
      this.selectedOrder = order;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      fullName: [this.selectedOrder.fullName || '', Validators.required],
      phoneNumber: [this.selectedOrder.phoneNumber || '', Validators.required],
      email: [this.selectedOrder.email || null, Validators.required],
      address: [this.selectedOrder.address || null, Validators.required],
      totalCheckout: [this.selectedOrder.totalCheckOut || '', Validators.required],
      status: [this.selectedOrder.status || '', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedOrder.id) {
      this.orderService
        .update(this.selectedOrder.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.orderService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
            this.orderService.delete(id).subscribe(() => this.list.get());
          }
	    });
  }
}
