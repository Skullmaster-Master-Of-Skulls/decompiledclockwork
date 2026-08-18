using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000C7 RID: 199
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x000631C4 File Offset: 0x000625C4
		internal DataRowView(DataView dataView, DataRow row)
		{
			this.dataView = dataView;
			this._row = row;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000631E8 File Offset: 0x000625E8
		public override bool Equals(object other)
		{
			return this == other;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000631FC File Offset: 0x000625FC
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00063214 File Offset: 0x00062614
		public DataView DataView
		{
			get
			{
				return this.dataView;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00063228 File Offset: 0x00062628
		internal int ObjectID
		{
			get
			{
				return this._row.ObjectID;
			}
		}

		// Token: 0x170001A2 RID: 418
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

		// Token: 0x170001A3 RID: 419
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

		// Token: 0x170001A4 RID: 420
		string IDataErrorInfo.this[string colName]
		{
			get
			{
				return this.Row.GetColumnError(colName);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000633A0 File Offset: 0x000627A0
		string IDataErrorInfo.Error
		{
			get
			{
				return this.Row.RowError;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000633B8 File Offset: 0x000627B8
		public DataRowVersion RowVersion
		{
			get
			{
				return this.RowVersionDefault & (DataRowVersion)(-1025);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000633D4 File Offset: 0x000627D4
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this.dataView.RowStateFilter);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000633F8 File Offset: 0x000627F8
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00063418 File Offset: 0x00062818
		internal bool HasRecord()
		{
			return this.Row.HasVersion(this.RowVersionDefault);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00063438 File Offset: 0x00062838
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00063458 File Offset: 0x00062858
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

		// Token: 0x06000B87 RID: 2951 RVA: 0x000634A8 File Offset: 0x000628A8
		public DataView CreateChildView(DataRelation relation, bool followParent)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			RelatedView relatedView;
			if (!followParent)
			{
				int record = this.GetRecord();
				object[] keyValues = relation.ParentKey.GetKeyValues(record);
				relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			}
			else
			{
				relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
			}
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00063538 File Offset: 0x00062938
		public DataView CreateChildView(DataRelation relation)
		{
			return this.CreateChildView(relation, false);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00063550 File Offset: 0x00062950
		public DataView CreateChildView(string relationName, bool followParent)
		{
			return this.CreateChildView(this.DataView.Table.ChildRelations[relationName], followParent);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0006357C File Offset: 0x0006297C
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(relationName, false);
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00063594 File Offset: 0x00062994
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000635A8 File Offset: 0x000629A8
		public void BeginEdit()
		{
			this.delayBeginEdit = true;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000635BC File Offset: 0x000629BC
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

		// Token: 0x06000B8E RID: 2958 RVA: 0x000635F4 File Offset: 0x000629F4
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

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0006362C File Offset: 0x00062A2C
		public bool IsNew
		{
			get
			{
				return this._row == this.dataView.addNewRow;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0006364C File Offset: 0x00062A4C
		public bool IsEdit
		{
			get
			{
				return this.Row.HasVersion(DataRowVersion.Proposed) || this.delayBeginEdit;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00063674 File Offset: 0x00062A74
		public void Delete()
		{
			this.dataView.Delete(this.Row);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00063694 File Offset: 0x00062A94
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000636A8 File Offset: 0x00062AA8
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000636B8 File Offset: 0x00062AB8
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000636C8 File Offset: 0x00062AC8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000636D8 File Offset: 0x00062AD8
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000636E8 File Offset: 0x00062AE8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000636F8 File Offset: 0x00062AF8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00063708 File Offset: 0x00062B08
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0006371C File Offset: 0x00062B1C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00063730 File Offset: 0x00062B30
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00063744 File Offset: 0x00062B44
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this.dataView.Table == null)
			{
				return DataRowView.zeroPropertyDescriptorCollection;
			}
			return this.dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00063778 File Offset: 0x00062B78
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000B9E RID: 2974 RVA: 0x00063788 File Offset: 0x00062B88
		// (remove) Token: 0x06000B9F RID: 2975 RVA: 0x000637AC File Offset: 0x00062BAC
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

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000637D0 File Offset: 0x00062BD0
		internal void RaisePropertyChangedEvent(string propName)
		{
			if (this.onPropertyChanged != null)
			{
				this.onPropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}

		// Token: 0x04000373 RID: 883
		private readonly DataView dataView;

		// Token: 0x04000374 RID: 884
		private readonly DataRow _row;

		// Token: 0x04000375 RID: 885
		private bool delayBeginEdit;

		// Token: 0x04000376 RID: 886
		private static PropertyDescriptorCollection zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);

		// Token: 0x04000377 RID: 887
		private PropertyChangedEventHandler onPropertyChanged;
	}
}
