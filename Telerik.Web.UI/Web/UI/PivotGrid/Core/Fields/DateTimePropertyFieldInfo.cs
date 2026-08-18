using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B7 RID: 1719
	public class DateTimePropertyFieldInfo : PropertyFieldInfo
	{
		// Token: 0x06003DDC RID: 15836 RVA: 0x000C721C File Offset: 0x000C541C
		public DateTimePropertyFieldInfo(PropertyFieldInfo propertyInfo, DateTimeStep step, string propertyName)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.propertyInfo = propertyInfo;
			this.DateTimeStep = step;
			this.PropertyName = propertyName;
			IPropertyMetadataReader propertyMetadataReader = propertyInfo as IPropertyMetadataReader;
			if (propertyMetadataReader != null)
			{
				base.DisplayName = propertyMetadataReader.GetValueForDisplayName();
				base.AutoGenerateField = propertyMetadataReader.GetValueForAutoGenerateField();
			}
			base.DataType = typeof(DateTime);
			base.Name = this.propertyInfo.Name;
			base.AllowedRoles = FieldRoles.All;
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000C729D File Offset: 0x000C549D
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x000C72A5 File Offset: 0x000C54A5
		public DateTimeStep DateTimeStep { get; set; }

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000C72AE File Offset: 0x000C54AE
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x000C72B6 File Offset: 0x000C54B6
		public string PropertyName { get; set; }

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000C72C0 File Offset: 0x000C54C0
		public override bool Equals(object obj)
		{
			DateTimePropertyFieldInfo dateTimePropertyFieldInfo = obj as DateTimePropertyFieldInfo;
			return base.Equals(obj) && dateTimePropertyFieldInfo.DateTimeStep == this.DateTimeStep;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000C72ED File Offset: 0x000C54ED
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.DateTimeStep.GetHashCode() * 8699;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000C730C File Offset: 0x000C550C
		public override object GetValue(object item)
		{
			return this.propertyInfo.GetValue(item);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000C731A File Offset: 0x000C551A
		public override void SetValue(object item, object fieldValue)
		{
			this.propertyInfo.SetValue(item, fieldValue);
		}

		// Token: 0x04001093 RID: 4243
		private PropertyFieldInfo propertyInfo;
	}
}
