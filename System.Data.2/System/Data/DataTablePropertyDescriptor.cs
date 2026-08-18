using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D4 RID: 212
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00073B7C File Offset: 0x00072F7C
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00073B90 File Offset: 0x00072F90
		internal DataTablePropertyDescriptor(DataTable dataTable) : base(dataTable.TableName, null)
		{
			this.table = dataTable;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00073BB4 File Offset: 0x00072FB4
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00073BCC File Offset: 0x00072FCC
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00073BDC File Offset: 0x00072FDC
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00073BF4 File Offset: 0x00072FF4
		public override bool Equals(object other)
		{
			if (other is DataTablePropertyDescriptor)
			{
				DataTablePropertyDescriptor dataTablePropertyDescriptor = (DataTablePropertyDescriptor)other;
				return dataTablePropertyDescriptor.Table == this.Table;
			}
			return false;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00073C20 File Offset: 0x00073020
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00073C38 File Offset: 0x00073038
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00073C48 File Offset: 0x00073048
		public override object GetValue(object component)
		{
			DataViewManagerListItemTypeDescriptor dataViewManagerListItemTypeDescriptor = (DataViewManagerListItemTypeDescriptor)component;
			return dataViewManagerListItemTypeDescriptor.GetDataView(this.table);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00073C68 File Offset: 0x00073068
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00073C78 File Offset: 0x00073078
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00073C88 File Offset: 0x00073088
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x04000405 RID: 1029
		private DataTable table;
	}
}
