using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200051D RID: 1309
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		// Token: 0x0600318C RID: 12684 RVA: 0x000DFC7B File Offset: 0x000DDE7B
		public BindingList()
		{
			this.Initialize();
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000DFCB3 File Offset: 0x000DDEB3
		public BindingList(IList<T> list) : base(list)
		{
			this.Initialize();
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000DFCEC File Offset: 0x000DDEEC
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T item in base.Items)
				{
					this.HookPropertyChanged(item);
				}
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000DFD68 File Offset: 0x000DDF68
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new Type[0], null) != null;
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003190 RID: 12688 RVA: 0x000DFDA8 File Offset: 0x000DDFA8
		// (remove) Token: 0x06003191 RID: 12689 RVA: 0x000DFDE4 File Offset: 0x000DDFE4
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Combine(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Remove(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000DFE20 File Offset: 0x000DE020
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			if (this.onAddingNew != null)
			{
				this.onAddingNew(this, e);
			}
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000DFE38 File Offset: 0x000DE038
		private object FireAddingNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs(null);
			this.OnAddingNew(addingNewEventArgs);
			return addingNewEventArgs.NewObject;
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06003194 RID: 12692 RVA: 0x000DFE59 File Offset: 0x000DE059
		// (remove) Token: 0x06003195 RID: 12693 RVA: 0x000DFE72 File Offset: 0x000DE072
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

		// Token: 0x06003196 RID: 12694 RVA: 0x000DFE8B File Offset: 0x000DE08B
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, e);
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000DFEA2 File Offset: 0x000DE0A2
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000DFEAA File Offset: 0x000DE0AA
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				if (this.raiseListChangedEvents != value)
				{
					this.raiseListChangedEvents = value;
				}
			}
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000DFEBC File Offset: 0x000DE0BC
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000DFEC6 File Offset: 0x000DE0C6
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000DFED0 File Offset: 0x000DE0D0
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000DFEE8 File Offset: 0x000DE0E8
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T item in base.Items)
				{
					this.UnhookPropertyChanged(item);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000DFF58 File Offset: 0x000DE158
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000DFF88 File Offset: 0x000DE188
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000DFFE5 File Offset: 0x000DE1E5
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000E001B File Offset: 0x000DE21B
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000E0042 File Offset: 0x000DE242
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000E005D File Offset: 0x000DE25D
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000E006C File Offset: 0x000DE26C
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : -1);
			return obj;
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000E0099 File Offset: 0x000DE299
		private bool AddingNewHandled
		{
			get
			{
				return this.onAddingNew != null && this.onAddingNew.GetInvocationList().Length != 0;
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000E00B4 File Offset: 0x000DE2B4
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				Type typeFromHandle = typeof(T);
				obj = SecurityUtils.SecureCreateInstance(typeFromHandle);
			}
			base.Add((T)((object)obj));
			return obj;
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000E00EA File Offset: 0x000DE2EA
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x000E010C File Offset: 0x000DE30C
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000E013A File Offset: 0x000DE33A
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000E0142 File Offset: 0x000DE342
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x000E014A File Offset: 0x000DE34A
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000E0164 File Offset: 0x000DE364
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000E016C File Offset: 0x000DE36C
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x000E0174 File Offset: 0x000DE374
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000E018E File Offset: 0x000DE38E
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000E0196 File Offset: 0x000DE396
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000E019E File Offset: 0x000DE39E
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000E01A1 File Offset: 0x000DE3A1
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x000E01A9 File Offset: 0x000DE3A9
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x000E01AC File Offset: 0x000DE3AC
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000E01B4 File Offset: 0x000DE3B4
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x000E01B7 File Offset: 0x000DE3B7
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000E01BF File Offset: 0x000DE3BF
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000E01C2 File Offset: 0x000DE3C2
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000E01CA File Offset: 0x000DE3CA
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x000E01CD File Offset: 0x000DE3CD
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000E01D5 File Offset: 0x000DE3D5
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000E01D8 File Offset: 0x000DE3D8
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000E01E2 File Offset: 0x000DE3E2
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000E01E9 File Offset: 0x000DE3E9
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000E01F1 File Offset: 0x000DE3F1
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000E01F8 File Offset: 0x000DE3F8
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000E0202 File Offset: 0x000DE402
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000E0209 File Offset: 0x000DE409
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000E020B File Offset: 0x000DE40B
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000E0210 File Offset: 0x000DE410
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this.propertyChangedEventHandler == null)
				{
					this.propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this.propertyChangedEventHandler;
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000E0254 File Offset: 0x000DE454
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this.propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.propertyChangedEventHandler;
			}
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000E0284 File Offset: 0x000DE484
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this.lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_7B;
					}
				}
				num = base.IndexOf(t);
				this.lastChangeIndex = num;
				IL_7B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this.itemTypeProperties == null)
				{
					this.itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propDesc = this.itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs e2 = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propDesc);
				this.OnListChanged(e2);
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000E0370 File Offset: 0x000DE570
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04002934 RID: 10548
		private int addNewPos = -1;

		// Token: 0x04002935 RID: 10549
		private bool raiseListChangedEvents = true;

		// Token: 0x04002936 RID: 10550
		private bool raiseItemChangedEvents;

		// Token: 0x04002937 RID: 10551
		[NonSerialized]
		private PropertyDescriptorCollection itemTypeProperties;

		// Token: 0x04002938 RID: 10552
		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		// Token: 0x04002939 RID: 10553
		[NonSerialized]
		private AddingNewEventHandler onAddingNew;

		// Token: 0x0400293A RID: 10554
		[NonSerialized]
		private ListChangedEventHandler onListChanged;

		// Token: 0x0400293B RID: 10555
		[NonSerialized]
		private int lastChangeIndex = -1;

		// Token: 0x0400293C RID: 10556
		private bool allowNew = true;

		// Token: 0x0400293D RID: 10557
		private bool allowEdit = true;

		// Token: 0x0400293E RID: 10558
		private bool allowRemove = true;

		// Token: 0x0400293F RID: 10559
		private bool userSetAllowNew;
	}
}
