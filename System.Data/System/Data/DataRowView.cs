using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000092 RID: 146
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x001F7E68 File Offset: 0x001F7268
		internal DataRowView(DataView dataView, DataRow row)
		{
			this.dataView = dataView;
			this._row = row;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x001F7E98 File Offset: 0x001F7298
		public override bool Equals(object other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x001F7EB8 File Offset: 0x001F72B8
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x001F7ED8 File Offset: 0x001F72D8
		public DataView DataView
		{
			get
			{
				return this.dataView;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x001F7EF8 File Offset: 0x001F72F8
		internal int ObjectID
		{
			get
			{
				return this._row.ObjectID;
			}
		}

		// Token: 0x17000101 RID: 257
		public object this[int ndx]
		{
			get
			{
				return this.Row[ndx, this.RowVersionDefault];
			}
			set
			{
				if (!this.dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(this.dataView.Table.Columns[ndx], value);
			}
		}

		// Token: 0x17000102 RID: 258
		public object this[string property]
		{
			get
			{
				DataColumn dataColumn = this.dataView.Table.Columns[property];
				if (dataColumn != null)
				{
					return this.Row[dataColumn, this.RowVersionDefault];
				}
				if (this.dataView.Table.DataSet != null && this.dataView.Table.DataSet.Relations.Contains(property))
				{
					return this.CreateChildView(property);
				}
				throw ExceptionBuilder.PropertyNotFound(property, this.dataView.Table.TableName);
			}
			set
			{
				DataColumn dataColumn = this.dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.SetFailed(property);
				}
				if (!this.dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(dataColumn, value);
			}
		}

		// Token: 0x17000103 RID: 259
		string IDataErrorInfo.this[string colName]
		{
			get
			{
				return this.Row.GetColumnError(colName);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x001F8098 File Offset: 0x001F7498
		string IDataErrorInfo.Error
		{
			get
			{
				return this.Row.RowError;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x001F80B8 File Offset: 0x001F74B8
		public DataRowVersion RowVersion
		{
			get
			{
				return this.RowVersionDefault & (DataRowVersion)(-1025);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x001F80D8 File Offset: 0x001F74D8
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this.dataView.RowStateFilter);
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x001F8108 File Offset: 0x001F7508
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x001F8128 File Offset: 0x001F7528
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x001F8148 File Offset: 0x001F7548
		internal void SetColumnValue(DataColumn column, object value)
		{
			if (this.delayBeginEdit)
			{
				this.delayBeginEdit = false;
				this.Row.BeginEdit();
			}
			if (DataRowVersion.Original == this.RowVersionDefault)
			{
				throw ExceptionBuilder.SetFailed(column.ColumnName);
			}
			this.Row[column] = value;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x001F8198 File Offset: 0x001F7598
		public DataView CreateChildView(DataRelation relation)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			int record = this.GetRecord();
			object[] keyValues = relation.ParentKey.GetKeyValues(record);
			RelatedView relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x001F8218 File Offset: 0x001F7618
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(this.DataView.Table.ChildRelations[relationName]);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x001F8248 File Offset: 0x001F7648
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x001F8268 File Offset: 0x001F7668
		public void BeginEdit()
		{
			this.delayBeginEdit = true;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x001F8288 File Offset: 0x001F7688
		public void CancelEdit()
		{
			DataRow row = this.Row;
			if (this.IsNew)
			{
				this.dataView.FinishAddNew(false);
			}
			else
			{
				row.CancelEdit();
			}
			this.delayBeginEdit = false;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x001F82C8 File Offset: 0x001F76C8
		public void EndEdit()
		{
			if (this.IsNew)
			{
				this.dataView.FinishAddNew(true);
			}
			else
			{
				this.Row.EndEdit();
			}
			this.delayBeginEdit = false;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x001F8308 File Offset: 0x001F7708
		public bool IsNew
		{
			get
			{
				return this._row == this.dataView.addNewRow;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x001F8328 File Offset: 0x001F7728
		public bool IsEdit
		{
			get
			{
				return this.Row.HasVersion(DataRowVersion.Proposed) || this.delayBeginEdit;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x001F8358 File Offset: 0x001F7758
		public void Delete()
		{
			this.dataView.Delete(this.Row);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x001F8378 File Offset: 0x001F7778
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x001F8398 File Offset: 0x001F7798
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x001F83A8 File Offset: 0x001F77A8
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x001F83B8 File Offset: 0x001F77B8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x001F83C8 File Offset: 0x001F77C8
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x001F83D8 File Offset: 0x001F77D8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x001F83E8 File Offset: 0x001F77E8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x001F83F8 File Offset: 0x001F77F8
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x001F8418 File Offset: 0x001F7818
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x001F8438 File Offset: 0x001F7838
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x001F8458 File Offset: 0x001F7858
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this.dataView.Table == null)
			{
				return DataRowView.zeroPropertyDescriptorCollection;
			}
			return this.dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x001F8498 File Offset: 0x001F7898
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000860 RID: 2144 RVA: 0x001F84A8 File Offset: 0x001F78A8
		// (remove) Token: 0x06000861 RID: 2145 RVA: 0x001F84D8 File Offset: 0x001F78D8
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this.onPropertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this.onPropertyChanged, value);
			}
			remove
			{
				this.onPropertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this.onPropertyChanged, value);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x001F8508 File Offset: 0x001F7908
		internal void RaisePropertyChangedEvent(string propName)
		{
			if (this.onPropertyChanged != null)
			{
				this.onPropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}

		// Token: 0x04000793 RID: 1939
		private readonly DataView dataView;

		// Token: 0x04000794 RID: 1940
		private readonly DataRow _row;

		// Token: 0x04000795 RID: 1941
		private bool delayBeginEdit;

		// Token: 0x04000796 RID: 1942
		private static PropertyDescriptorCollection zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);

		// Token: 0x04000797 RID: 1943
		private PropertyChangedEventHandler onPropertyChanged;
	}
}
