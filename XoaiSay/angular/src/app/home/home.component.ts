import { CommonModule, CurrencyPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService, RestService } from '@abp/ng.core';
import { firstValueFrom } from 'rxjs';

interface ProductDto {
  id: string;
  name: string;
  description: string;
  price: number;
  weightGrams: number;
  imageUrl: string;
  stockQuantity: number;
}

interface PagedResultDto<T> {
  items: T[];
  totalCount: number;
}

interface CreatePurchaseOrderDto {
  productId: string;
  customerName: string;
  customerEmail: string;
  phoneNumber: string;
  shippingAddress: string;
  notes?: string | null;
  quantity: number;
}

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, ReactiveFormsModule, CurrencyPipe, RouterLink]
})
export class HomeComponent implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly restService = inject(RestService);
  private readonly fb = inject(FormBuilder);

  readonly isLoading = signal(true);
  readonly isSubmitting = signal(false);
  readonly products = signal<ProductDto[]>([]);
  readonly loadError = signal<string | null>(null);
  readonly orderSuccess = signal<boolean>(false);
  readonly orderError = signal<string | null>(null);
  readonly trackByProductId = (_: number, item: ProductDto) => item.id;

  readonly orderForm = this.fb.nonNullable.group({
    productId: ['', Validators.required],
    quantity: [1, [Validators.required, Validators.min(1), Validators.max(100)]],
    customerName: ['', [Validators.required, Validators.maxLength(128)]],
    customerEmail: ['', [Validators.required, Validators.email, Validators.maxLength(128)]],
    phoneNumber: ['', [Validators.required, Validators.maxLength(32)]],
    shippingAddress: ['', [Validators.required, Validators.maxLength(256)]],
    notes: ['']
  });

  readonly selectedProduct = computed(() => {
    const productId = this.orderForm.controls.productId.value;
    return this.products().find(product => product.id === productId) ?? null;
  });

  readonly computedTotal = computed(() => {
    const product = this.selectedProduct();
    const quantity = this.orderForm.controls.quantity.value;
    return product ? product.price * quantity : 0;
  });

  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  ngOnInit(): void {
    void this.loadProducts();
  }

  async loadProducts(): Promise<void> {
    this.isLoading.set(true);
    this.loadError.set(null);

    try {
      const response = (await firstValueFrom(
        this.restService.request<PagedResultDto<ProductDto>, unknown>({
          method: 'GET',
          url: '/api/store/products',
          params: {
            MaxResultCount: 50,
            Sorting: 'name'
          }
        })
      )) as PagedResultDto<ProductDto>;

      if (response?.items?.length) {
        this.products.set(response.items);
        this.orderForm.controls.productId.setValue(response.items[0].id);
      } else {
        this.products.set([]);
        this.loadError.set('Chưa có sản phẩm nào được cấu hình.');
      }
    } catch (error) {
      console.error('Failed to load products', error);
      this.loadError.set('Không thể tải danh sách sản phẩm. Vui lòng thử lại sau.');
    } finally {
      this.isLoading.set(false);
    }
  }

  async submitOrder(): Promise<void> {
    this.orderSuccess.set(false);
    this.orderError.set(null);

    if (this.orderForm.invalid || !this.selectedProduct()) {
      this.orderForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);

    const payload: CreatePurchaseOrderDto = {
      ...this.orderForm.getRawValue(),
      notes: this.orderForm.controls.notes.value?.trim() || null
    };

    try {
      await firstValueFrom(
        this.restService.request<unknown, unknown>({
          method: 'POST',
          url: '/api/store/orders',
          body: payload
        })
      );

      this.orderSuccess.set(true);
      this.orderForm.reset({
        productId: this.orderForm.controls.productId.value,
        quantity: 1,
        customerName: '',
        customerEmail: '',
        phoneNumber: '',
        shippingAddress: '',
        notes: ''
      });
    } catch (error: any) {
      console.error('Order submission failed', error);
      const message = error?.error?.error?.message ?? 'Không thể gửi đơn hàng. Vui lòng thử lại.';
      this.orderError.set(message);
    } finally {
      this.isSubmitting.set(false);
    }
  }

  login(): void {
    this.authService.navigateToLogin();
  }
}

