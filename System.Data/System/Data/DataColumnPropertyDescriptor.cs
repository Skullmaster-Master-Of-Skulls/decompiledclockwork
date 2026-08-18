using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x0200006C RID: 108
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x001ECEF8 File Offset: 0x001EC2F8
		internal DataColumnPropertyDescriptor(DataColumn dataColumn) : base(dataColumn.ColumnName, null)
		{
			this.column = dataColumn;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x001ECF28 File Offset: 0x001EC328
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x001ECF88 File Offset: 0x001EC388
		internal DataColumn Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x001ECFA8 File Offset: 0x001EC3A8
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x001ECFC8 File Offset: 0x001EC3C8
		public override bool IsReadOnly
		{
			get
			{
				return this.column.ReadOnly;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x001ECFE8 File Offset: 0x001EC3E8
		public override Type PropertyType
		{
			get
			{
				return this.column.DataType;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x001ED008 File Offset: 0x001EC408
		public override bool Equals(object other)
		{
			if (other is DataColumnPropertyDescriptor)
			{
				DataColumnPropertyDescriptor dataColumnPropertyDescriptor = (DataColumnPropertyDescriptor)other;
				return dataColumnPropertyDescriptor.Column == this.Column;
			}
			return false;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x001ED038 File Offset: 0x001EC438
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x001ED058 File Offset: 0x001EC458
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.column));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x001ED0A8 File Offset: 0x001EC4A8
		public override object GetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			return dataRowView.GetColumnValue(this.column);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x001ED0C8 File Offset: 0x001EC4C8
		public override void ResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			dataRowView.SetColumnValue(this.column, DBNull.Value);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x001ED0F8 File Offset: 0x001EC4F8
		public override void SetValue(object component, object value)
		{
			DataRowView dataRowView = (DataRowView)component;
			dataRowView.SetColumnValue(this.column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x001ED128 File Offset: 0x001EC528
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x001ED138 File Offset: 0x001EC538
		public override bool IsBrowsable
		{
			get
			{
				return this.column.ColumnMapping != MappingType.Hidden && base.IsBrowsable;
			}
		}

		// Token: 0x04000718 RID: 1816
		private DataColumn column;
	}
}
