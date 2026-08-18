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
	// Token: 0x020000AB RID: 171
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewManagerDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataViewManager : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0020E588 File Offset: 0x0020D988
		public DataViewManager() : this(null, false)
		{
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0020E5A8 File Offset: 0x0020D9A8
		public DataViewManager(DataSet dataSet) : this(dataSet, false)
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0020E5C8 File Offset: 0x0020D9C8
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

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0020E658 File Offset: 0x0020DA58
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0020E678 File Offset: 0x0020DA78
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0020E758 File Offset: 0x0020DB58
		[ResDescription("DataViewManagerTableSettingsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this.dataViewSettingsCollection;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0020E778 File Offset: 0x0020DB78
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0020E868 File Offset: 0x0020DC68
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

		// Token: 0x06000BAD RID: 2989 RVA: 0x0020E968 File Offset: 0x0020DD68
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[1];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0020E998 File Offset: 0x0020DD98
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0020E9A8 File Offset: 0x0020DDA8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0020E9B8 File Offset: 0x0020DDB8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0020E9C8 File Offset: 0x0020DDC8
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0020E9D8 File Offset: 0x0020DDD8
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0020E9E8 File Offset: 0x0020DDE8
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(new DataViewManagerListItemTypeDescriptor(this), index);
		}

		// Token: 0x1700019A RID: 410
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

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0020EA48 File Offset: 0x0020DE48
		int IList.Add(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0020EA68 File Offset: 0x0020DE68
		void IList.Clear()
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0020EA88 File Offset: 0x0020DE88
		bool IList.Contains(object value)
		{
			return value == this.item;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0020EAA8 File Offset: 0x0020DEA8
		int IList.IndexOf(object value)
		{
			if (value != this.item)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0020EAC8 File Offset: 0x0020DEC8
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0020EAE8 File Offset: 0x0020DEE8
		void IList.Remove(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0020EB08 File Offset: 0x0020DF08
		void IList.RemoveAt(int index)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0020EB28 File Offset: 0x0020DF28
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0020EB38 File Offset: 0x0020DF38
		object IBindingList.AddNew()
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0020EB58 File Offset: 0x0020DF58
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0020EB68 File Offset: 0x0020DF68
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0020EB78 File Offset: 0x0020DF78
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0020EB88 File Offset: 0x0020DF88
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0020EB98 File Offset: 0x0020DF98
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0020EBA8 File Offset: 0x0020DFA8
		bool IBindingList.IsSorted
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0020EBC8 File Offset: 0x0020DFC8
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0020EBE8 File Offset: 0x0020DFE8
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw DataViewManager.NotSupported;
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000BC7 RID: 3015 RVA: 0x0020EC08 File Offset: 0x0020E008
		// (remove) Token: 0x06000BC8 RID: 3016 RVA: 0x0020EC38 File Offset: 0x0020E038
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

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0020EC68 File Offset: 0x0020E068
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0020EC78 File Offset: 0x0020E078
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0020EC98 File Offset: 0x0020E098
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0020ECB8 File Offset: 0x0020E0B8
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0020ECC8 File Offset: 0x0020E0C8
		void IBindingList.RemoveSort()
		{
			throw DataViewManager.NotSupported;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0020ECE8 File Offset: 0x0020E0E8
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

		// Token: 0x06000BCF RID: 3023 RVA: 0x0020ED38 File Offset: 0x0020E138
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

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0020ED88 File Offset: 0x0020E188
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

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0020EDB8 File Offset: 0x0020E1B8
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

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0020EE18 File Offset: 0x0020E218
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			PropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataTablePropertyDescriptor((DataTable)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataTablePropertyDescriptor((DataTable)e.Element)) : null)));
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0020EE88 File Offset: 0x0020E288
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x0400085F RID: 2143
		private DataViewSettingCollection dataViewSettingsCollection;

		// Token: 0x04000860 RID: 2144
		private DataSet dataSet;

		// Token: 0x04000861 RID: 2145
		private DataViewManagerListItemTypeDescriptor item;

		// Token: 0x04000862 RID: 2146
		private bool locked;

		// Token: 0x04000863 RID: 2147
		internal int nViews;

		// Token: 0x04000864 RID: 2148
		private ListChangedEventHandler onListChanged;

		// Token: 0x04000865 RID: 2149
		private static NotSupportedException NotSupported = new NotSupportedException();
	}
}
