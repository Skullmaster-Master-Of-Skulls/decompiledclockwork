using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x0200014E RID: 334
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x00017AF0 File Offset: 0x00015CF0
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			List<Attribute> source = new List<Attribute>(attributes);
			DisplayColumnAttribute displayColumnAttribute = source.OfType<DisplayColumnAttribute>().FirstOrDefault<DisplayColumnAttribute>();
			DataAnnotationsModelMetadata dataAnnotationsModelMetadata = new DataAnnotationsModelMetadata(this, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute);
			HiddenInputAttribute hiddenInputAttribute = source.OfType<HiddenInputAttribute>().FirstOrDefault<HiddenInputAttribute>();
			if (hiddenInputAttribute != null)
			{
				dataAnnotationsModelMetadata.TemplateHint = "HiddenInput";
				dataAnnotationsModelMetadata.HideSurroundingHtml = !hiddenInputAttribute.DisplayValue;
			}
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
			DataTypeAttribute dataTypeAttribute = source.OfType<DataTypeAttribute>().FirstOrDefault<DataTypeAttribute>();
			DisplayFormatAttribute displayFormatAttribute = source.OfType<DisplayFormatAttribute>().FirstOrDefault<DisplayFormatAttribute>();
			DataAnnotationsModelMetadataProvider.SetFromDataTypeAndDisplayAttributes(dataAnnotationsModelMetadata, dataTypeAttribute, displayFormatAttribute);
			ScaffoldColumnAttribute scaffoldColumnAttribute = source.OfType<ScaffoldColumnAttribute>().FirstOrDefault<ScaffoldColumnAttribute>();
			if (scaffoldColumnAttribute != null)
			{
				dataAnnotationsModelMetadata.ShowForDisplay = (dataAnnotationsModelMetadata.ShowForEdit = scaffoldColumnAttribute.Scaffold);
			}
			DisplayAttribute displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			string text = null;
			if (displayAttribute != null)
			{
				dataAnnotationsModelMetadata.Description = displayAttribute.GetDescription();
				dataAnnotationsModelMetadata.ShortDisplayName = displayAttribute.GetShortName();
				dataAnnotationsModelMetadata.Watermark = displayAttribute.GetPrompt();
				dataAnnotationsModelMetadata.Order = (displayAttribute.GetOrder() ?? 10000);
				text = displayAttribute.GetName();
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

		// Token: 0x06000893 RID: 2195 RVA: 0x00017CEC File Offset: 0x00015EEC
		private static void SetFromDataTypeAndDisplayAttributes(DataAnnotationsModelMetadata result, DataTypeAttribute dataTypeAttribute, DisplayFormatAttribute displayFormatAttribute)
		{
			if (dataTypeAttribute != null)
			{
				result.DataTypeName = dataTypeAttribute.ToDataTypeName(null);
			}
			if (displayFormatAttribute == null && dataTypeAttribute != null)
			{
				displayFormatAttribute = dataTypeAttribute.DisplayFormat;
				if (displayFormatAttribute != null && dataTypeAttribute.GetType() != typeof(DataTypeAttribute))
				{
					result.HasNonDefaultEditFormat = true;
				}
			}
			else if (displayFormatAttribute != null)
			{
				result.HasNonDefaultEditFormat = true;
			}
			if (displayFormatAttribute != null)
			{
				result.NullDisplayText = displayFormatAttribute.NullDisplayText;
				result.DisplayFormatString = displayFormatAttribute.DataFormatString;
				result.ConvertEmptyStringToNull = displayFormatAttribute.ConvertEmptyStringToNull;
				result.HtmlEncode = displayFormatAttribute.HtmlEncode;
				if (displayFormatAttribute.ApplyFormatInEditMode)
				{
					result.EditFormatString = displayFormatAttribute.DataFormatString;
				}
				if (!displayFormatAttribute.HtmlEncode && string.IsNullOrWhiteSpace(result.DataTypeName))
				{
					result.DataTypeName = DataTypeUtil.HtmlTypeName;
				}
				if (string.IsNullOrEmpty(result.EditFormatString))
				{
					result.HasNonDefaultEditFormat = false;
				}
			}
		}
	}
}
