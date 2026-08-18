using System;
using System.Data;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B6 RID: 1718
	internal class DataRowFieldInfo : PropertyFieldInfo
	{
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000C714C File Offset: 0x000C534C
		public DataRowFieldInfo(DataColumn dataColumn)
		{
			if (dataColumn == null)
			{
				throw new ArgumentNullException("dataColumn");
			}
			this.fieldDescriptor = dataColumn;
			base.DisplayName = this.fieldDescriptor.Caption;
			base.DataType = this.fieldDescriptor.DataType;
			base.Name = this.fieldDescriptor.ColumnName;
			base.AllowedRoles = FieldRoles.All;
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000C71B0 File Offset: 0x000C53B0
		public override object GetValue(object item)
		{
			DataRow dataRow = item as DataRow;
			if (dataRow == null)
			{
				throw new ArgumentException("GetValue requires a valud DataRow instance");
			}
			object obj = dataRow[this.fieldDescriptor];
			if (obj != DBNull.Value)
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000C71EC File Offset: 0x000C53EC
		public override void SetValue(object item, object fieldValue)
		{
			DataRow dataRow = item as DataRow;
			if (dataRow == null)
			{
				throw new ArgumentException("GetValue requires a valud DataRow instance");
			}
			dataRow[this.fieldDescriptor] = fieldValue;
		}

		// Token: 0x04001092 RID: 4242
		private DataColumn fieldDescriptor;
	}
}
