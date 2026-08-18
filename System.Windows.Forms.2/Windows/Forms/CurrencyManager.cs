using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000176 RID: 374
	public class CurrencyManager : BindingManagerBase
	{
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x060013A3 RID: 5027 RVA: 0x00041721 File Offset: 0x0003F921
		// (remove) Token: 0x060013A4 RID: 5028 RVA: 0x0004173A File Offset: 0x0003F93A
		[SRCategory("CatData")]
		public event ItemChangedEventHandler ItemChanged
		{
			add
			{
				this.onItemChanged = (ItemChangedEventHandler)Delegate.Combine(this.onItemChanged, value);
			}
			remove
			{
				this.onItemChanged = (ItemChangedEventHandler)Delegate.Remove(this.onItemChanged, value);
			}
		}

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x060013A5 RID: 5029 RVA: 0x00041753 File Offset: 0x0003F953
		// (remove) Token: 0x060013A6 RID: 5030 RVA: 0x0004176C File Offset: 0x0003F96C
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

		// Token: 0x060013A7 RID: 5031 RVA: 0x00041785 File Offset: 0x0003F985
		internal CurrencyManager(object dataSource)
		{
			this.SetDataSource(dataSource);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000417B8 File Offset: 0x0003F9B8
		internal bool AllowAdd
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowNew;
				}
				return this.list != null && !this.list.IsReadOnly && !this.list.IsFixedSize;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004180A File Offset: 0x0003FA0A
		internal bool AllowEdit
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowEdit;
				}
				return this.list != null && !this.list.IsReadOnly;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00041844 File Offset: 0x0003FA44
		internal bool AllowRemove
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowRemove;
				}
				return this.list != null && !this.list.IsReadOnly && !this.list.IsFixedSize;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00041896 File Offset: 0x0003FA96
		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return this.list.Count;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000418AD File Offset: 0x0003FAAD
		public override object Current
		{
			get
			{
				return this[this.Position];
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000418BB File Offset: 0x0003FABB
		internal override Type BindType
		{
			get
			{
				return ListBindingHelper.GetListItemType(this.List);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000418C8 File Offset: 0x0003FAC8
		internal override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000418D0 File Offset: 0x0003FAD0
		internal override void SetDataSource(object dataSource)
		{
			if (this.dataSource == dataSource)
			{
				return;
			}
			this.Release();
			this.dataSource = dataSource;
			this.list = null;
			this.finalType = null;
			object obj = dataSource;
			if (obj is Array)
			{
				this.finalType = obj.GetType();
				obj = (Array)obj;
			}
			if (obj is IListSource)
			{
				obj = ((IListSource)obj).GetList();
			}
			if (obj is IList)
			{
				if (this.finalType == null)
				{
					this.finalType = obj.GetType();
				}
				this.list = (IList)obj;
				this.WireEvents(this.list);
				if (this.list.Count > 0)
				{
					this.listposition = 0;
				}
				else
				{
					this.listposition = -1;
				}
				this.OnItemChanged(this.resetEvent);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				this.UpdateIsBinding();
				return;
			}
			if (obj == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			throw new ArgumentException(SR.GetString("ListManagerSetDataSource", new object[]
			{
				obj.GetType().FullName
			}), "dataSource");
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000419E5 File Offset: 0x0003FBE5
		internal override bool IsBinding
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000419ED File Offset: 0x0003FBED
		internal bool ShouldBind
		{
			get
			{
				return this.shouldBind;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000419F5 File Offset: 0x0003FBF5
		public IList List
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000419FD File Offset: 0x0003FBFD
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00041A08 File Offset: 0x0003FC08
		public override int Position
		{
			get
			{
				return this.listposition;
			}
			set
			{
				if (this.listposition == -1)
				{
					return;
				}
				if (value < 0)
				{
					value = 0;
				}
				int count = this.list.Count;
				if (value >= count)
				{
					value = count - 1;
				}
				this.ChangeRecordState(value, this.listposition != value, true, true, false);
			}
		}

		// Token: 0x17000478 RID: 1144
		internal object this[int index]
		{
			get
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException(SR.GetString("ListManagerNoValue", new object[]
					{
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				return this.list[index];
			}
			set
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException(SR.GetString("ListManagerNoValue", new object[]
					{
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.list[index] = value;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		public override void AddNew()
		{
			IBindingList bindingList = this.list as IBindingList;
			if (bindingList != null)
			{
				bindingList.AddNew();
				this.ChangeRecordState(this.list.Count - 1, this.Position != this.list.Count - 1, this.Position != this.list.Count - 1, true, true);
				return;
			}
			throw new NotSupportedException(SR.GetString("CurrencyManagerCantAddNew"));
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00041B74 File Offset: 0x0003FD74
		public override void CancelCurrentEdit()
		{
			if (this.Count > 0)
			{
				object obj = (this.Position >= 0 && this.Position < this.list.Count) ? this.list[this.Position] : null;
				IEditableObject editableObject = obj as IEditableObject;
				if (editableObject != null)
				{
					editableObject.CancelEdit();
				}
				ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
				if (cancelAddNew != null)
				{
					cancelAddNew.CancelNew(this.Position);
				}
				this.OnItemChanged(new ItemChangedEventArgs(this.Position));
				if (this.Position != -1)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.Position));
				}
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00041C18 File Offset: 0x0003FE18
		private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChange, bool pullData)
		{
			if (newPosition == -1 && this.list.Count == 0)
			{
				if (this.listposition != -1)
				{
					this.listposition = -1;
					this.OnPositionChanged(EventArgs.Empty);
				}
				return;
			}
			if ((newPosition < 0 || newPosition >= this.Count) && this.IsBinding)
			{
				throw new IndexOutOfRangeException(SR.GetString("ListManagerBadPosition"));
			}
			int num = this.listposition;
			if (endCurrentEdit)
			{
				this.inChangeRecordState = true;
				try
				{
					this.EndCurrentEdit();
				}
				finally
				{
					this.inChangeRecordState = false;
				}
			}
			if (validating && pullData)
			{
				this.CurrencyManager_PullData();
			}
			this.listposition = Math.Min(newPosition, this.Count - 1);
			if (validating)
			{
				this.OnCurrentChanged(EventArgs.Empty);
			}
			bool flag = num != this.listposition;
			if (flag && firePositionChange)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00041CF8 File Offset: 0x0003FEF8
		protected void CheckEmpty()
		{
			if (this.dataSource == null || this.list == null || this.list.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("ListManagerEmptyList"));
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00041D28 File Offset: 0x0003FF28
		private bool CurrencyManager_PushData()
		{
			if (this.pullingData)
			{
				return false;
			}
			int num = this.listposition;
			if (this.lastGoodKnownRow == -1)
			{
				try
				{
					base.PushData();
				}
				catch (Exception e)
				{
					base.OnDataError(e);
					this.FindGoodRow();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			else
			{
				try
				{
					base.PushData();
				}
				catch (Exception e2)
				{
					base.OnDataError(e2);
					this.listposition = this.lastGoodKnownRow;
					base.PushData();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			return num != this.listposition;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		private bool CurrencyManager_PullData()
		{
			bool result = true;
			this.pullingData = true;
			try
			{
				base.PullData(out result);
			}
			finally
			{
				this.pullingData = false;
			}
			return result;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00041E0C File Offset: 0x0004000C
		public override void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00041E1C File Offset: 0x0004001C
		public override void EndCurrentEdit()
		{
			if (this.Count > 0)
			{
				bool flag = this.CurrencyManager_PullData();
				if (flag)
				{
					object obj = (this.Position >= 0 && this.Position < this.list.Count) ? this.list[this.Position] : null;
					IEditableObject editableObject = obj as IEditableObject;
					if (editableObject != null)
					{
						editableObject.EndEdit();
					}
					ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
					if (cancelAddNew != null)
					{
						cancelAddNew.EndNew(this.Position);
					}
				}
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00041E98 File Offset: 0x00040098
		private void FindGoodRow()
		{
			int count = this.list.Count;
			int i = 0;
			while (i < count)
			{
				this.listposition = i;
				try
				{
					base.PushData();
				}
				catch (Exception e)
				{
					base.OnDataError(e);
					goto IL_31;
				}
				goto IL_29;
				IL_31:
				i++;
				continue;
				IL_29:
				this.listposition = i;
				return;
			}
			this.SuspendBinding();
			throw new InvalidOperationException(SR.GetString("DataBindingPushDataException"));
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00041F04 File Offset: 0x00040104
		internal void SetSort(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				((IBindingList)this.list).ApplySort(property, sortDirection);
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00041F37 File Offset: 0x00040137
		internal PropertyDescriptor GetSortProperty()
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				return ((IBindingList)this.list).SortProperty;
			}
			return null;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00041F6A File Offset: 0x0004016A
		internal ListSortDirection GetSortDirection()
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				return ((IBindingList)this.list).SortDirection;
			}
			return ListSortDirection.Ascending;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00041FA0 File Offset: 0x000401A0
		internal int Find(PropertyDescriptor property, object key, bool keepIndex)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (property != null && this.list is IBindingList && ((IBindingList)this.list).SupportsSearching)
			{
				return ((IBindingList)this.list).Find(property, key);
			}
			if (property != null)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					object value = property.GetValue(this.list[i]);
					if (key.Equals(value))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00042028 File Offset: 0x00040228
		internal override string GetListName()
		{
			if (this.list is ITypedList)
			{
				return ((ITypedList)this.list).GetListName(null);
			}
			return this.finalType.Name;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00042054 File Offset: 0x00040254
		protected internal override string GetListName(ArrayList listAccessors)
		{
			if (this.list is ITypedList)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(array, 0);
				return ((ITypedList)this.list).GetListName(array);
			}
			return "";
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00042099 File Offset: 0x00040299
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListItemProperties(this.list, listAccessors);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0001FDAB File Offset: 0x0001DFAB
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000420A8 File Offset: 0x000402A8
		private void List_ListChanged(object sender, ListChangedEventArgs e)
		{
			ListChangedEventArgs listChangedEventArgs;
			if (e.ListChangedType == ListChangedType.ItemMoved && e.OldIndex < 0)
			{
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemAdded, e.NewIndex, e.OldIndex);
			}
			else if (e.ListChangedType == ListChangedType.ItemMoved && e.NewIndex < 0)
			{
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemDeleted, e.OldIndex, e.NewIndex);
			}
			else
			{
				listChangedEventArgs = e;
			}
			int num = this.listposition;
			this.UpdateLastGoodKnownRow(listChangedEventArgs);
			this.UpdateIsBinding();
			if (this.list.Count == 0)
			{
				this.listposition = -1;
				if (num != -1)
				{
					this.OnPositionChanged(EventArgs.Empty);
					this.OnCurrentChanged(EventArgs.Empty);
				}
				if (listChangedEventArgs.ListChangedType == ListChangedType.Reset && e.NewIndex == -1)
				{
					this.OnItemChanged(this.resetEvent);
				}
				if (listChangedEventArgs.ListChangedType == ListChangedType.ItemDeleted)
				{
					this.OnItemChanged(this.resetEvent);
				}
				if (e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted || e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
				{
					this.OnMetaDataChanged(EventArgs.Empty);
				}
				this.OnListChanged(listChangedEventArgs);
				return;
			}
			this.suspendPushDataInCurrentChanged = true;
			try
			{
				switch (listChangedEventArgs.ListChangedType)
				{
				case ListChangedType.Reset:
					if (this.listposition == -1 && this.list.Count > 0)
					{
						this.ChangeRecordState(0, true, false, true, false);
					}
					else
					{
						this.ChangeRecordState(Math.Min(this.listposition, this.list.Count - 1), true, false, true, false);
					}
					this.UpdateIsBinding(false);
					this.OnItemChanged(this.resetEvent);
					break;
				case ListChangedType.ItemAdded:
					if (listChangedEventArgs.NewIndex <= this.listposition && this.listposition < this.list.Count - 1)
					{
						this.ChangeRecordState(this.listposition + 1, true, true, this.listposition != this.list.Count - 2, false);
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						if (this.listposition == this.list.Count - 1)
						{
							this.OnPositionChanged(EventArgs.Empty);
						}
					}
					else
					{
						if (listChangedEventArgs.NewIndex == this.listposition && this.listposition == this.list.Count - 1 && this.listposition != -1)
						{
							this.OnCurrentItemChanged(EventArgs.Empty);
						}
						if (this.listposition == -1)
						{
							this.ChangeRecordState(0, false, false, true, false);
						}
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
					}
					break;
				case ListChangedType.ItemDeleted:
					if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.ChangeRecordState(Math.Min(this.listposition, this.Count - 1), true, false, true, false);
						this.OnItemChanged(this.resetEvent);
					}
					else if (listChangedEventArgs.NewIndex < this.listposition)
					{
						this.ChangeRecordState(this.listposition - 1, true, false, true, false);
						this.OnItemChanged(this.resetEvent);
					}
					else
					{
						this.OnItemChanged(this.resetEvent);
					}
					break;
				case ListChangedType.ItemMoved:
					if (listChangedEventArgs.OldIndex == this.listposition)
					{
						this.ChangeRecordState(listChangedEventArgs.NewIndex, true, this.Position > -1 && this.Position < this.list.Count, true, false);
					}
					else if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.ChangeRecordState(listChangedEventArgs.OldIndex, true, this.Position > -1 && this.Position < this.list.Count, true, false);
					}
					this.OnItemChanged(this.resetEvent);
					break;
				case ListChangedType.ItemChanged:
					if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.OnCurrentItemChanged(EventArgs.Empty);
					}
					this.OnItemChanged(new ItemChangedEventArgs(listChangedEventArgs.NewIndex));
					break;
				case ListChangedType.PropertyDescriptorAdded:
				case ListChangedType.PropertyDescriptorDeleted:
				case ListChangedType.PropertyDescriptorChanged:
					this.lastGoodKnownRow = -1;
					if (this.listposition == -1 && this.list.Count > 0)
					{
						this.ChangeRecordState(0, true, false, true, false);
					}
					else if (this.listposition > this.list.Count - 1)
					{
						this.ChangeRecordState(this.list.Count - 1, true, false, true, false);
					}
					this.OnMetaDataChanged(EventArgs.Empty);
					break;
				}
				this.OnListChanged(listChangedEventArgs);
			}
			finally
			{
				this.suspendPushDataInCurrentChanged = false;
			}
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x060013C9 RID: 5065 RVA: 0x000424F0 File Offset: 0x000406F0
		// (remove) Token: 0x060013CA RID: 5066 RVA: 0x00042509 File Offset: 0x00040709
		[SRCategory("CatData")]
		public event EventHandler MetaDataChanged
		{
			add
			{
				this.onMetaDataChangedHandler = (EventHandler)Delegate.Combine(this.onMetaDataChangedHandler, value);
			}
			remove
			{
				this.onMetaDataChangedHandler = (EventHandler)Delegate.Remove(this.onMetaDataChangedHandler, value);
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00042524 File Offset: 0x00040724
		protected internal override void OnCurrentChanged(EventArgs e)
		{
			if (!this.inChangeRecordState)
			{
				int num = this.lastGoodKnownRow;
				bool flag = false;
				if (!this.suspendPushDataInCurrentChanged)
				{
					flag = this.CurrencyManager_PushData();
				}
				if (this.Count > 0)
				{
					object obj = this.list[this.Position];
					if (obj is IEditableObject)
					{
						((IEditableObject)obj).BeginEdit();
					}
				}
				try
				{
					if (!flag || (flag && num != -1))
					{
						if (this.onCurrentChangedHandler != null)
						{
							this.onCurrentChangedHandler(this, e);
						}
						if (this.onCurrentItemChangedHandler != null)
						{
							this.onCurrentItemChangedHandler(this, e);
						}
					}
				}
				catch (Exception e2)
				{
					base.OnDataError(e2);
				}
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000425D4 File Offset: 0x000407D4
		protected internal override void OnCurrentItemChanged(EventArgs e)
		{
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, e);
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000425EC File Offset: 0x000407EC
		protected virtual void OnItemChanged(ItemChangedEventArgs e)
		{
			bool flag = false;
			if ((e.Index == this.listposition || (e.Index == -1 && this.Position < this.Count)) && !this.inChangeRecordState)
			{
				flag = this.CurrencyManager_PushData();
			}
			try
			{
				if (this.onItemChanged != null)
				{
					this.onItemChanged(this, e);
				}
			}
			catch (Exception e2)
			{
				base.OnDataError(e2);
			}
			if (flag)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00042670 File Offset: 0x00040870
		private void OnListChanged(ListChangedEventArgs e)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, e);
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00042687 File Offset: 0x00040887
		protected internal void OnMetaDataChanged(EventArgs e)
		{
			if (this.onMetaDataChangedHandler != null)
			{
				this.onMetaDataChangedHandler(this, e);
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000426A0 File Offset: 0x000408A0
		protected virtual void OnPositionChanged(EventArgs e)
		{
			try
			{
				if (this.onPositionChangedHandler != null)
				{
					this.onPositionChangedHandler(this, e);
				}
			}
			catch (Exception e2)
			{
				base.OnDataError(e2);
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000426E0 File Offset: 0x000408E0
		public void Refresh()
		{
			if (this.list.Count > 0)
			{
				if (this.listposition >= this.list.Count)
				{
					this.lastGoodKnownRow = -1;
					this.listposition = 0;
				}
			}
			else
			{
				this.listposition = -1;
			}
			this.List_ListChanged(this.list, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00042738 File Offset: 0x00040938
		internal void Release()
		{
			this.UnwireEvents(this.list);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00042748 File Offset: 0x00040948
		public override void ResumeBinding()
		{
			this.lastGoodKnownRow = -1;
			try
			{
				if (!this.shouldBind)
				{
					this.shouldBind = true;
					this.listposition = ((this.list != null && this.list.Count != 0) ? 0 : -1);
					this.UpdateIsBinding();
				}
			}
			catch
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
				throw;
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000427B4 File Offset: 0x000409B4
		public override void SuspendBinding()
		{
			this.lastGoodKnownRow = -1;
			if (this.shouldBind)
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000427D2 File Offset: 0x000409D2
		internal void UnwireEvents(IList list)
		{
			if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
			{
				((IBindingList)list).ListChanged -= this.List_ListChanged;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00042800 File Offset: 0x00040A00
		protected override void UpdateIsBinding()
		{
			this.UpdateIsBinding(true);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004280C File Offset: 0x00040A0C
		private void UpdateIsBinding(bool raiseItemChangedEvent)
		{
			bool flag = this.list != null && this.list.Count > 0 && this.shouldBind && this.listposition != -1;
			if (this.list != null && this.bound != flag)
			{
				this.bound = flag;
				int num = flag ? 0 : -1;
				this.ChangeRecordState(num, this.bound, this.Position != num, true, false);
				int count = base.Bindings.Count;
				for (int i = 0; i < count; i++)
				{
					base.Bindings[i].UpdateIsBinding();
				}
				if (raiseItemChangedEvent)
				{
					this.OnItemChanged(this.resetEvent);
				}
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000428BC File Offset: 0x00040ABC
		private void UpdateLastGoodKnownRow(ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				this.lastGoodKnownRow = -1;
				return;
			case ListChangedType.ItemAdded:
				if (e.NewIndex <= this.lastGoodKnownRow && this.lastGoodKnownRow < this.List.Count - 1)
				{
					this.lastGoodKnownRow++;
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (e.NewIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = -1;
					return;
				}
				break;
			case ListChangedType.ItemMoved:
				if (e.OldIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = e.NewIndex;
					return;
				}
				break;
			case ListChangedType.ItemChanged:
				if (e.NewIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = -1;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004296B File Offset: 0x00040B6B
		internal void WireEvents(IList list)
		{
			if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
			{
				((IBindingList)list).ListChanged += this.List_ListChanged;
			}
		}

		// Token: 0x04000948 RID: 2376
		private object dataSource;

		// Token: 0x04000949 RID: 2377
		private IList list;

		// Token: 0x0400094A RID: 2378
		private bool bound;

		// Token: 0x0400094B RID: 2379
		private bool shouldBind = true;

		// Token: 0x0400094C RID: 2380
		protected int listposition = -1;

		// Token: 0x0400094D RID: 2381
		private int lastGoodKnownRow = -1;

		// Token: 0x0400094E RID: 2382
		private bool pullingData;

		// Token: 0x0400094F RID: 2383
		private bool inChangeRecordState;

		// Token: 0x04000950 RID: 2384
		private bool suspendPushDataInCurrentChanged;

		// Token: 0x04000951 RID: 2385
		private ItemChangedEventHandler onItemChanged;

		// Token: 0x04000952 RID: 2386
		private ListChangedEventHandler onListChanged;

		// Token: 0x04000953 RID: 2387
		private ItemChangedEventArgs resetEvent = new ItemChangedEventArgs(-1);

		// Token: 0x04000954 RID: 2388
		private EventHandler onMetaDataChangedHandler;

		// Token: 0x04000955 RID: 2389
		protected Type finalType;
	}
}
