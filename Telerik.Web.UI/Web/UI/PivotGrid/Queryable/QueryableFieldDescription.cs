using System;
using System.Reflection;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D6E RID: 3438
	public sealed class QueryableFieldDescription : PropertyFieldInfo, IPropertyMetadataReader
	{
		// Token: 0x06008035 RID: 32821 RVA: 0x001D512C File Offset: 0x001D332C
		public QueryableFieldDescription(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.propertyInfo = propertyInfo;
			base.PreferredRole = FieldInfoHelper.GetRoleForType(propertyInfo.PropertyType);
			base.Name = this.propertyInfo.Name;
			base.DataType = this.propertyInfo.PropertyType;
			base.DisplayName = AttributeHelper.GetValueForDisplayName(propertyInfo);
			base.AllowedRoles = FieldRoles.All;
			base.AutoGenerateField = AttributeHelper.GetValueForAutoGenerateField(propertyInfo);
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x001D51AD File Offset: 0x001D33AD
		public override object GetValue(object item)
		{
			return this.propertyInfo.GetValue(item, null);
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x001D51BC File Offset: 0x001D33BC
		public override void SetValue(object item, object fieldValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x001D51C3 File Offset: 0x001D33C3
		string IPropertyMetadataReader.GetValueForDisplayName()
		{
			return AttributeHelper.GetValueForDisplayName(this.propertyInfo);
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x001D51D0 File Offset: 0x001D33D0
		bool IPropertyMetadataReader.GetValueForAutoGenerateField()
		{
			return AttributeHelper.GetValueForAutoGenerateField(this.propertyInfo);
		}

		// Token: 0x04002342 RID: 9026
		private PropertyInfo propertyInfo;
	}
}
