using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000645 RID: 1605
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider
	{
		// Token: 0x06004F62 RID: 20322 RVA: 0x00113920 File Offset: 0x00111B20
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			List<Attribute> source = new List<Attribute>(attributes);
			DisplayColumnAttribute displayColumnAttribute = source.OfType<DisplayColumnAttribute>().FirstOrDefault<DisplayColumnAttribute>();
			DataAnnotationsModelMetadata dataAnnotationsModelMetadata = new DataAnnotationsModelMetadata(this, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute);
			IEnumerable<UIHintAttribute> source2 = source.OfType<UIHintAttribute>();
			UIHintAttribute uihintAttribute;
			if ((uihintAttribute = source2.FirstOrDefault((UIHintAttribute a) => string.Equals(a.PresentationLayer, "MVC", StringComparison.OrdinalIgnoreCase))) == null)
			{
				uihintAttribute = source2.FirstOrDefault((UIHintAttribute a) => string.IsNullOrEmpty(a.PresentationLayer));
			}
			UIHintAttribute uihintAttribute2 = uihintAttribute;
			if (uihintAttribute2 != null)
			{
				dataAnnotationsModelMetadata.TemplateHint = uihintAttribute2.UIHint;
			}
			DataTypeAttribute dataTypeAttribute = source.OfType<DataTypeAttribute>().FirstOrDefault<DataTypeAttribute>();
			if (dataTypeAttribute != null)
			{
				dataAnnotationsModelMetadata.DataTypeName = dataTypeAttribute.ToDataTypeName(null);
			}
			EditableAttribute editableAttribute = attributes.OfType<EditableAttribute>().FirstOrDefault<EditableAttribute>();
			if (editableAttribute != null)
			{
				dataAnnotationsModelMetadata.IsReadOnly = !editableAttribute.AllowEdit;
			}
			else
			{
				ReadOnlyAttribute readOnlyAttribute = source.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
				if (readOnlyAttribute != null)
				{
					dataAnnotationsModelMetadata.IsReadOnly = readOnlyAttribute.IsReadOnly;
				}
			}
			DisplayFormatAttribute displayFormatAttribute = source.OfType<DisplayFormatAttribute>().FirstOrDefault<DisplayFormatAttribute>();
			if (displayFormatAttribute == null && dataTypeAttribute != null)
			{
				displayFormatAttribute = dataTypeAttribute.DisplayFormat;
			}
			if (displayFormatAttribute != null)
			{
				dataAnnotationsModelMetadata.NullDisplayText = displayFormatAttribute.NullDisplayText;
				dataAnnotationsModelMetadata.DisplayFormatString = displayFormatAttribute.DataFormatString;
				dataAnnotationsModelMetadata.ConvertEmptyStringToNull = displayFormatAttribute.ConvertEmptyStringToNull;
				if (displayFormatAttribute.ApplyFormatInEditMode)
				{
					dataAnnotationsModelMetadata.EditFormatString = displayFormatAttribute.DataFormatString;
				}
				if (!displayFormatAttribute.HtmlEncode && string.IsNullOrWhiteSpace(dataAnnotationsModelMetadata.DataTypeName))
				{
					dataAnnotationsModelMetadata.DataTypeName = DataTypeUtil.HtmlTypeName;
				}
			}
			ScaffoldColumnAttribute scaffoldColumnAttribute = source.OfType<ScaffoldColumnAttribute>().FirstOrDefault<ScaffoldColumnAttribute>();
			if (scaffoldColumnAttribute != null)
			{
				dataAnnotationsModelMetadata.ShowForDisplay = (dataAnnotationsModelMetadata.ShowForEdit = scaffoldColumnAttribute.Scaffold);
			}
			DisplayAttribute displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			string text = null;
			if (displayAttribute != null)
			{
				DisplayAttributeAdapter displayAttributeAdapter = new DisplayAttributeAdapter(displayAttribute);
				dataAnnotationsModelMetadata.Description = displayAttributeAdapter.GetDescription();
				dataAnnotationsModelMetadata.ShortDisplayName = displayAttributeAdapter.GetShortName();
				dataAnnotationsModelMetadata.Watermark = displayAttributeAdapter.GetPrompt();
				dataAnnotationsModelMetadata.Order = (displayAttributeAdapter.GetOrder() ?? 10000);
				text = displayAttributeAdapter.GetName();
			}
			if (text != null)
			{
				dataAnnotationsModelMetadata.DisplayName = text;
			}
			else
			{
				DisplayNameAttribute displayNameAttribute = source.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
				if (displayNameAttribute != null)
				{
					dataAnnotationsModelMetadata.DisplayName = displayNameAttribute.DisplayName;
				}
			}
			RequiredAttribute requiredAttribute = source.OfType<RequiredAttribute>().FirstOrDefault<RequiredAttribute>();
			if (requiredAttribute != null)
			{
				dataAnnotationsModelMetadata.IsRequired = true;
			}
			return dataAnnotationsModelMetadata;
		}
	}
}
