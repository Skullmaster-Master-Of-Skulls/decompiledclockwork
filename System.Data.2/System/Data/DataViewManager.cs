using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000DA RID: 218
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewManagerDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataViewManager : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00077FF4 File Offset: 0x000773F4
		public DataViewManager() : this(null, false)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0007800C File Offset: 0x0007740C
		public DataViewManager(DataSet dataSet) : this(dataSet, false)
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00078024 File Offset: 0x00077424
		internal DataViewManager(DataSet dataSet, bool locked)
		{
			GC.SuppressFinalize(this);
			this.dataSet = dataSet;
			if (this.dataSet != null)
			{
				this.dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this.dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
			}
			this.locked = locked;
			this.item = new DataViewManagerListItemTypeDescriptor(this);
			this.dataViewSettingsCollection = new DataViewSettingCollection(this);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000780A8 File Offset: 0x000774A8
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x000780BC File Offset: 0x000774BC
		[ResDescription("DataViewManagerDataSetDescr")]
		[DefaultValue(null)]
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.SetFailed("DataSet to null");
				}
				if (this.locked)
				{
					throw ExceptionBuilder.SetDataSetFailed();
				}
				if (this.dataSet != null)
				{
					if (this.nViews > 0)
					{
						throw ExceptionBuilder.CanNotSetDataSet();
					}
					this.dataSet.Tables.CollectionChanged -= this.TableCollectionChanged;
					this.dataSet.Relations.CollectionChanged -= this.RelationCollectionChanged;
				}
				this.dataSet = value;
				this.dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this.dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
				this.dataViewSettingsCollection = new DataViewSettingCollection(this);
				this.item.Reset();
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00078190 File Offset: 0x00077590
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResDescription("DataViewManagerTableSettingsDescr")]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this.dataViewSettingsCollection;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000781A4 File Offset: 0x000775A4
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0007828C File Offset: 0x0007768C
		public string DataViewSettingCollectionString
		{
			get
			{
				if (this.dataSet == null)
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<DataViewSettingCollectionString>");
				foreach (object obj in this.dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataViewSetting dataViewSetting = this.dataViewSettingsCollection[dataTable];
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} Sort=\"{1}\" RowFilter=\"{2}\" RowStateFilter=\"{3}\"/>", new object[]
					{
						dataTable.EncodedTableName,
						dataViewSetting.Sort,
						dataViewSetting.RowFilter,
						dataViewSetting.RowStateFilter
					});
				}
				stringBuilder.Append("</DataViewSettingCollectionString>");
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					return;
				}
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(value));
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
				xmlTextReader.Read();
				if (xmlTextReader.Name != "DataViewSettingCollectionString")
				{
					throw ExceptionBuilder.SetFailed("DataViewSettingCollectionString");
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.NodeType == XmlNodeType.Element)
					{
						string tableName = XmlConvert.DecodeName(xmlTextReader.LocalName);
						if (xmlTextReader.MoveToAttribute("Sort"))
						{
							this.dataViewSettingsCollection[tableName].Sort = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowFilter"))
						{
							this.dataViewSettingsCollection[tableName].RowFilter = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowStateFilter"))
						{
							this.dataViewSettingsCollection[tableName].RowStateFilter = (DataViewRowState)Enum.Parse(typeof(DataViewRowState), xmlTextReader.Value);
						}
					}
				}
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00078384 File Offset: 0x00077784
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[1];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000783A8 File Offset: 0x000777A8
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000783B8 File Offset: 0x000777B8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x000783C8 File Offset: 0x000777C8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x000783D8 File Offset: 0x000777D8
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000783E8 File Offset: 0x000777E8
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000783F8 File Offset: 0x000777F8
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(new DataViewManagerListItemTypeDescriptor(this), index);
		}

		// Token: 0x1700022E RID: 558
		object IList.this[int index]
		{
			get
			{
				return this.item;
			}
			set
			{
				throw ExceptionBuilder.CannotModifyCollection();
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0007843C File Offset: 0x0007783C
		int IList.Add(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00078450 File Offset: 0x00077850
		void IList.Clear()
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00078464 File Offset: 0x00077864
		bool IList.Contains(object value)
		{
			return value == this.item;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0007847C File Offset: 0x0007787C
		int IList.IndexOf(object value)
		{
			if (value != this.item)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00078498 File Offset: 0x00077898
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000784AC File Offset: 0x000778AC
		void IList.Remove(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000784C0 File Offset: 0x000778C0
		void IList.RemoveAt(int index)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000784D4 File Offset: 0x000778D4
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000784E4 File Offset: 0x000778E4
		object IBindingList.AddNew()
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000784F8 File Offset: 0x000778F8
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00078508 File Offset: 0x00077908
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00078518 File Offset: 0x00077918
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00078528 File Offset: 0x00077928
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00078538 File Offset: 0x00077938
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00078548 File Offset: 0x00077948
		bool IBindingList.IsSorted
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0007855C File Offset: 0x0007795C
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00078570 File Offset: 0x00077970
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000ECE RID: 3790 RVA: 0x00078584 File Offset: 0x00077984
		// (remove) Token: 0x06000ECF RID: 3791 RVA: 0x000785A8 File Offset: 0x000779A8
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

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000785CC File Offset: 0x000779CC
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000785DC File Offset: 0x000779DC
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000785F0 File Offset: 0x000779F0
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00078604 File Offset: 0x00077A04
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00078614 File Offset: 0x00077A14
		void IBindingList.RemoveSort()
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00078628 File Offset: 0x00077A28
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return dataSet.DataSetName;
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.TableName;
			}
			return string.Empty;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0007866C File Offset: 0x00077A6C
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return ((ICustomTypeDescriptor)new DataViewManagerListItemTypeDescriptor(this)).GetProperties();
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.GetPropertyDescriptorCollection(null);
			}
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000786B8 File Offset: 0x00077AB8
		public DataView CreateDataView(DataTable table)
		{
			if (this.dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this);
			return dataView;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000786E4 File Offset: 0x00077AE4
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			try
			{
				if (this.onListChanged != null)
				{
					this.onListChanged(this, e);
				}
			}
			catch (Exception e2)
			{
				if (!ADP.IsCatchableExceptionType(e2))
				{
					throw;
				}
				ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00078738 File Offset: 0x00077B38
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			PropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataTablePropertyDescriptor((DataTable)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataTablePropertyDescriptor((DataTable)e.Element)) : null)));
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000787A4 File Offset: 0x00077BA4
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x0400043B RID: 1083
		private DataViewSettingCollection dataViewSettingsCollection;

		// Token: 0x0400043C RID: 1084
		private DataSet dataSet;

		// Token: 0x0400043D RID: 1085
		private DataViewManagerListItemTypeDescriptor item;

		// Token: 0x0400043E RID: 1086
		private bool locked;

		// Token: 0x0400043F RID: 1087
		internal int nViews;

		// Token: 0x04000440 RID: 1088
		private ListChangedEventHandler onListChanged;

		// Token: 0x04000441 RID: 1089
		private static NotSupportedException NotSupported = new NotSupportedException();
	}
}
