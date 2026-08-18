using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000AB RID: 171
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0005BD5C File Offset: 0x0005B15C
		internal DataColumnPropertyDescriptor(DataColumn dataColumn) : base(dataColumn.ColumnName, null)
		{
			this.column = dataColumn;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0005BD80 File Offset: 0x0005B180
		public override AttributeCollection Attributes
		{
			get
			{
				if (typeof(IList).IsAssignableFrom(this.PropertyType))
				{
					Attribute[] array = new Attribute[base.Attributes.Count + 1];
					base.Attributes.CopyTo(array, 0);
					array[array.Length - 1] = new ListBindableAttribute(false);
					return new AttributeCollection(array);
				}
				return base.Attributes;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0005BDE0 File Offset: 0x0005B1E0
		internal DataColumn Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0005BDF4 File Offset: 0x0005B1F4
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0005BE0C File Offset: 0x0005B20C
		public override bool IsReadOnly
		{
			get
			{
				return this.column.ReadOnly;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0005BE24 File Offset: 0x0005B224
		public override Type PropertyType
		{
			get
			{
				return this.column.DataType;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0005BE3C File Offset: 0x0005B23C
		public override bool Equals(object other)
		{
			if (other is DataColumnPropertyDescriptor)
			{
				DataColumnPropertyDescriptor dataColumnPropertyDescriptor = (DataColumnPropertyDescriptor)other;
				return dataColumnPropertyDescriptor.Column == this.Column;
			}
			return false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0005BE68 File Offset: 0x0005B268
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0005BE80 File Offset: 0x0005B280
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.column));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0005BECC File Offset: 0x0005B2CC
		public override object GetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			return dataRowView.GetColumnValue(this.column);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0005BEEC File Offset: 0x0005B2EC
		public override void ResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			dataRowView.SetColumnValue(this.column, DBNull.Value);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0005BF14 File Offset: 0x0005B314
		public override void SetValue(object component, object value)
		{
			DataRowView dataRowView = (DataRowView)component;
			dataRowView.SetColumnValue(this.column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0005BF44 File Offset: 0x0005B344
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0005BF54 File Offset: 0x0005B354
		public override bool IsBrowsable
		{
			get
			{
				return this.column.ColumnMapping != MappingType.Hidden && base.IsBrowsable;
			}
		}

		// Token: 0x04000326 RID: 806
		private DataColumn column;
	}
}
