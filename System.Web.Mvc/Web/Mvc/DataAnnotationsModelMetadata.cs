using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200013C RID: 316
	public class DataAnnotationsModelMetadata : ModelMetadata
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x000167C3 File Offset: 0x000149C3
		public DataAnnotationsModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute) : base(provider, containerType, modelAccessor, modelType, propertyName)
		{
			this._displayColumnAttribute = displayColumnAttribute;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000167DC File Offset: 0x000149DC
		protected override string GetSimpleDisplayText()
		{
			if (base.Model != null && this._displayColumnAttribute != null && !string.IsNullOrEmpty(this._displayColumnAttribute.DisplayColumn))
			{
				PropertyInfo property = base.ModelType.GetProperty(this._displayColumnAttribute.DisplayColumn, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				DataAnnotationsModelMetadata.ValidateDisplayColumnAttribute(this._displayColumnAttribute, property, base.ModelType);
				object value = property.GetValue(base.Model, new object[0]);
				if (value != null)
				{
					return value.ToString();
				}
			}
			return base.GetSimpleDisplayText();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001685C File Offset: 0x00014A5C
		private static void ValidateDisplayColumnAttribute(DisplayColumnAttribute displayColumnAttribute, PropertyInfo displayColumnProperty, Type modelType)
		{
			if (displayColumnProperty == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.DataAnnotationsModelMetadataProvider_UnknownProperty, new object[]
				{
					modelType.FullName,
					displayColumnAttribute.DisplayColumn
				}));
			}
			if (displayColumnProperty.GetGetMethod() == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.DataAnnotationsModelMetadataProvider_UnreadableProperty, new object[]
				{
					modelType.FullName,
					displayColumnAttribute.DisplayColumn
				}));
			}
		}

		// Token: 0x04000243 RID: 579
		private DisplayColumnAttribute _displayColumnAttribute;
	}
}
