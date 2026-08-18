using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Charting
{
	// Token: 0x020016F8 RID: 5880
	public class ProductsList : CollectionBase, IBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x0600E450 RID: 58448 RVA: 0x0032B0CC File Offset: 0x003292CC
		public void LoadProducts()
		{
			((IList)this).Add(new Product(0, "Cars", 10000));
			((IList)this).Add(new Product(1, "Bikes", 15000));
			((IList)this).Add(new Product(2, "Trailers", 5000));
			this.OnListChanged(this.resetEvent);
		}

		// Token: 0x170045AF RID: 17839
		public Product this[int index]
		{
			get
			{
				return (Product)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600E453 RID: 58451 RVA: 0x0032B14E File Offset: 0x0032934E
		public int Add(Product value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600E454 RID: 58452 RVA: 0x0032B15C File Offset: 0x0032935C
		public Product AddNew()
		{
			return (Product)((IBindingList)this).AddNew();
		}

		// Token: 0x0600E455 RID: 58453 RVA: 0x0032B169 File Offset: 0x00329369
		public void Remove(Product value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600E456 RID: 58454 RVA: 0x0032B177 File Offset: 0x00329377
		protected virtual void OnListChanged(ListChangedEventArgs ev)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, ev);
			}
		}

		// Token: 0x0600E457 RID: 58455 RVA: 0x0032B18E File Offset: 0x0032938E
		protected override void OnClear()
		{
		}

		// Token: 0x0600E458 RID: 58456 RVA: 0x0032B190 File Offset: 0x00329390
		protected override void OnClearComplete()
		{
			this.OnListChanged(this.resetEvent);
		}

		// Token: 0x0600E459 RID: 58457 RVA: 0x0032B19E File Offset: 0x0032939E
		protected override void OnInsertComplete(int index, object value)
		{
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		// Token: 0x0600E45A RID: 58458 RVA: 0x0032B1AD File Offset: 0x003293AD
		protected override void OnRemoveComplete(int index, object value)
		{
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		// Token: 0x0600E45B RID: 58459 RVA: 0x0032B1BC File Offset: 0x003293BC
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
		}

		// Token: 0x0600E45C RID: 58460 RVA: 0x0032B1D0 File Offset: 0x003293D0
		internal void ProductChanged(Product inc)
		{
			int newIndex = base.List.IndexOf(inc);
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex));
		}

		// Token: 0x170045B0 RID: 17840
		// (get) Token: 0x0600E45D RID: 58461 RVA: 0x0032B1F7 File Offset: 0x003293F7
		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170045B1 RID: 17841
		// (get) Token: 0x0600E45E RID: 58462 RVA: 0x0032B1FA File Offset: 0x003293FA
		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170045B2 RID: 17842
		// (get) Token: 0x0600E45F RID: 58463 RVA: 0x0032B1FD File Offset: 0x003293FD
		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170045B3 RID: 17843
		// (get) Token: 0x0600E460 RID: 58464 RVA: 0x0032B200 File Offset: 0x00329400
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170045B4 RID: 17844
		// (get) Token: 0x0600E461 RID: 58465 RVA: 0x0032B203 File Offset: 0x00329403
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170045B5 RID: 17845
		// (get) Token: 0x0600E462 RID: 58466 RVA: 0x0032B206 File Offset: 0x00329406
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x0600E463 RID: 58467 RVA: 0x0032B209 File Offset: 0x00329409
		// (remove) Token: 0x0600E464 RID: 58468 RVA: 0x0032B222 File Offset: 0x00329422
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		// Token: 0x0600E465 RID: 58469 RVA: 0x0032B23C File Offset: 0x0032943C
		object IBindingList.AddNew()
		{
			Product product = new Product(base.Count);
			base.List.Add(product);
			return product;
		}

		// Token: 0x170045B6 RID: 17846
		// (get) Token: 0x0600E466 RID: 58470 RVA: 0x0032B263 File Offset: 0x00329463
		bool IBindingList.IsSorted
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170045B7 RID: 17847
		// (get) Token: 0x0600E467 RID: 58471 RVA: 0x0032B26A File Offset: 0x0032946A
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170045B8 RID: 17848
		// (get) Token: 0x0600E468 RID: 58472 RVA: 0x0032B271 File Offset: 0x00329471
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600E469 RID: 58473 RVA: 0x0032B278 File Offset: 0x00329478
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600E46A RID: 58474 RVA: 0x0032B27F File Offset: 0x0032947F
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600E46B RID: 58475 RVA: 0x0032B286 File Offset: 0x00329486
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600E46C RID: 58476 RVA: 0x0032B28D File Offset: 0x0032948D
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600E46D RID: 58477 RVA: 0x0032B294 File Offset: 0x00329494
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040041EB RID: 16875
		private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x040041EC RID: 16876
		private ListChangedEventHandler onListChanged;
	}
}
