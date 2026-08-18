using System;
using System.ComponentModel;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAC RID: 3244
	public class PropertyDescriptorFieldInfo : PropertyFieldInfo, IPropertyMetadataReader
	{
		// Token: 0x06007988 RID: 31112 RVA: 0x001BECCC File Offset: 0x001BCECC
		public PropertyDescriptorFieldInfo(PropertyDescriptor propertyDescriptor)
		{
			if (propertyDescriptor == null)
			{
				throw new ArgumentNullException("propertyDescriptor");
			}
			this.propertyDescriptor = propertyDescriptor;
			base.DisplayName = this.GetValueForDisplayName();
			base.DataType = this.propertyDescriptor.PropertyType;
			base.Name = this.propertyDescriptor.Name;
			base.AllowedRoles = FieldRoles.All;
			base.AutoGenerateField = this.GetValueForAutoGenerateField();
		}

		// Token: 0x06007989 RID: 31113 RVA: 0x001BED38 File Offset: 0x001BCF38
		private string GetValueForDisplayName()
		{
			string valueForDisplayNameFromAttributes = this.GetValueForDisplayNameFromAttributes();
			if (valueForDisplayNameFromAttributes == null)
			{
				return this.propertyDescriptor.Name;
			}
			return valueForDisplayNameFromAttributes;
		}

		// Token: 0x0600798A RID: 31114 RVA: 0x001BED5C File Offset: 0x001BCF5C
		private string GetValueForDisplayNameFromAttributes()
		{
			if (this.propertyDescriptor.Attributes == null)
			{
				return null;
			}
			DisplayNameAttribute displayNameAttribute = this.propertyDescriptor.Attributes.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			if (displayNameAttribute != null)
			{
				return displayNameAttribute.DisplayName;
			}
			return null;
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x001BED9C File Offset: 0x001BCF9C
		private bool GetValueForAutoGenerateField()
		{
			bool? valueForAutoGenerateFieldFromAttributes = this.GetValueForAutoGenerateFieldFromAttributes();
			return valueForAutoGenerateFieldFromAttributes == null || valueForAutoGenerateFieldFromAttributes.Value;
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x001BEDC4 File Offset: 0x001BCFC4
		private bool? GetValueForAutoGenerateFieldFromAttributes()
		{
			if (this.propertyDescriptor.Attributes == null)
			{
				return null;
			}
			BrowsableAttribute browsableAttribute = this.propertyDescriptor.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
			if (browsableAttribute != null)
			{
				return new bool?(browsableAttribute.Browsable);
			}
			return null;
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x001BEE18 File Offset: 0x001BD018
		public override object GetValue(object item)
		{
			object value = this.propertyDescriptor.GetValue(item);
			if (value != DBNull.Value)
			{
				return value;
			}
			return null;
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x001BEE3D File Offset: 0x001BD03D
		public override void SetValue(object item, object fieldValue)
		{
			this.propertyDescriptor.SetValue(item, fieldValue);
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x001BEE4C File Offset: 0x001BD04C
		string IPropertyMetadataReader.GetValueForDisplayName()
		{
			return this.GetValueForDisplayName();
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x001BEE54 File Offset: 0x001BD054
		bool IPropertyMetadataReader.GetValueForAutoGenerateField()
		{
			return this.GetValueForAutoGenerateField();
		}

		// Token: 0x04002142 RID: 8514
		private PropertyDescriptor propertyDescriptor;
	}
}
