using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace System.Web.ModelBinding
{
	// Token: 0x02000644 RID: 1604
	public class DataAnnotationsModelMetadata : ModelMetadata
	{
		// Token: 0x06004F5F RID: 20319 RVA: 0x001137FF File Offset: 0x001119FF
		public DataAnnotationsModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute) : base(provider, containerType, modelAccessor, modelType, propertyName)
		{
			this._displayColumnAttribute = displayColumnAttribute;
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00113818 File Offset: 0x00111A18
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

		// Token: 0x06004F61 RID: 20321 RVA: 0x00113898 File Offset: 0x00111A98
		private static void ValidateDisplayColumnAttribute(DisplayColumnAttribute displayColumnAttribute, PropertyInfo displayColumnProperty, Type modelType)
		{
			if (displayColumnProperty == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("DataAnnotationsModelMetadataProvider_UnknownProperty"), new object[]
				{
					modelType.FullName,
					displayColumnAttribute.DisplayColumn
				}));
			}
			if (displayColumnProperty.GetGetMethod() == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("DataAnnotationsModelMetadataProvider_UnreadableProperty"), new object[]
				{
					modelType.FullName,
					displayColumnAttribute.DisplayColumn
				}));
			}
		}

		// Token: 0x04002A72 RID: 10866
		private DisplayColumnAttribute _displayColumnAttribute;
	}
}
