using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000A0 RID: 160
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00209918 File Offset: 0x00208D18
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00209938 File Offset: 0x00208D38
		internal DataTablePropertyDescriptor(DataTable dataTable) : base(dataTable.TableName, null)
		{
			this.table = dataTable;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00209968 File Offset: 0x00208D68
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00209988 File Offset: 0x00208D88
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00209998 File Offset: 0x00208D98
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x002099B8 File Offset: 0x00208DB8
		public override bool Equals(object other)
		{
			if (other is DataTablePropertyDescriptor)
			{
				DataTablePropertyDescriptor dataTablePropertyDescriptor = (DataTablePropertyDescriptor)other;
				return dataTablePropertyDescriptor.Table == this.Table;
			}
			return false;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x002099E8 File Offset: 0x00208DE8
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00209A08 File Offset: 0x00208E08
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00209A18 File Offset: 0x00208E18
		public override object GetValue(object component)
		{
			DataViewManagerListItemTypeDescriptor dataViewManagerListItemTypeDescriptor = (DataViewManagerListItemTypeDescriptor)component;
			return dataViewManagerListItemTypeDescriptor.GetDataView(this.table);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00209A38 File Offset: 0x00208E38
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00209A48 File Offset: 0x00208E48
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00209A58 File Offset: 0x00208E58
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x04000827 RID: 2087
		private DataTable table;
	}
}
