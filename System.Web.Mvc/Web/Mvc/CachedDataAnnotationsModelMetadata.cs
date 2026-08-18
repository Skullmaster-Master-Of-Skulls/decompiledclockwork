using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000071 RID: 113
	public class CachedDataAnnotationsModelMetadata : CachedModelMetadata<CachedDataAnnotationsMetadataAttributes>
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0000AAC1 File Offset: 0x00008CC1
		public CachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor) : base(prototype, modelAccessor)
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AACB File Offset: 0x00008CCB
		public CachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, IEnumerable<Attribute> attributes) : base(provider, containerType, modelType, propertyName, new CachedDataAnnotationsMetadataAttributes(attributes.ToArray<Attribute>()))
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		protected override bool ComputeConvertEmptyStringToNull()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeConvertEmptyStringToNull();
			}
			return base.PrototypeCache.DisplayFormat.ConvertEmptyStringToNull;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000AB0C File Offset: 0x00008D0C
		protected override string ComputeDataTypeName()
		{
			if (base.PrototypeCache.DataType != null)
			{
				return base.PrototypeCache.DataType.ToDataTypeName(null);
			}
			if (base.PrototypeCache.DisplayFormat != null && !base.PrototypeCache.DisplayFormat.HtmlEncode)
			{
				return DataTypeUtil.HtmlTypeName;
			}
			return base.ComputeDataTypeName();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AB63 File Offset: 0x00008D63
		protected override string ComputeDescription()
		{
			if (base.PrototypeCache.Display == null)
			{
				return base.ComputeDescription();
			}
			return base.PrototypeCache.Display.GetDescription();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000AB89 File Offset: 0x00008D89
		protected override string ComputeDisplayFormatString()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeDisplayFormatString();
			}
			return base.PrototypeCache.DisplayFormat.DataFormatString;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		protected override string ComputeDisplayName()
		{
			string text = null;
			if (base.PrototypeCache.Display != null)
			{
				text = base.PrototypeCache.Display.GetName();
			}
			if (text == null && base.PrototypeCache.DisplayName != null)
			{
				text = base.PrototypeCache.DisplayName.DisplayName;
			}
			return text ?? base.ComputeDisplayName();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000AC09 File Offset: 0x00008E09
		protected override string ComputeEditFormatString()
		{
			if (base.PrototypeCache.DisplayFormat != null && base.PrototypeCache.DisplayFormat.ApplyFormatInEditMode)
			{
				this._isEditFormatStringFromCache = true;
				return base.PrototypeCache.DisplayFormat.DataFormatString;
			}
			return base.ComputeEditFormatString();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AC48 File Offset: 0x00008E48
		protected override bool ComputeHasNonDefaultEditFormat()
		{
			if (!string.IsNullOrEmpty(this.EditFormatString) && this._isEditFormatStringFromCache)
			{
				if (base.PrototypeCache.DataType == null)
				{
					return true;
				}
				if (base.PrototypeCache.DataType.DisplayFormat != base.PrototypeCache.DisplayFormat)
				{
					return true;
				}
				if (base.PrototypeCache.DataType.GetType() != typeof(DataTypeAttribute))
				{
					return true;
				}
			}
			return base.ComputeHasNonDefaultEditFormat();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		protected override bool ComputeHideSurroundingHtml()
		{
			if (base.PrototypeCache.HiddenInput == null)
			{
				return base.ComputeHideSurroundingHtml();
			}
			return !base.PrototypeCache.HiddenInput.DisplayValue;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000ACEA File Offset: 0x00008EEA
		protected override bool ComputeHtmlEncode()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeHtmlEncode();
			}
			return base.PrototypeCache.DisplayFormat.HtmlEncode;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000AD10 File Offset: 0x00008F10
		protected override bool ComputeIsReadOnly()
		{
			if (base.PrototypeCache.Editable != null)
			{
				return !base.PrototypeCache.Editable.AllowEdit;
			}
			if (base.PrototypeCache.ReadOnly != null)
			{
				return base.PrototypeCache.ReadOnly.IsReadOnly;
			}
			return base.ComputeIsReadOnly();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AD62 File Offset: 0x00008F62
		protected override bool ComputeIsRequired()
		{
			return base.PrototypeCache.Required != null || base.ComputeIsRequired();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AD79 File Offset: 0x00008F79
		protected override string ComputeNullDisplayText()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeNullDisplayText();
			}
			return base.PrototypeCache.DisplayFormat.NullDisplayText;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		protected override int ComputeOrder()
		{
			int? num = null;
			if (base.PrototypeCache.Display != null)
			{
				num = base.PrototypeCache.Display.GetOrder();
			}
			int? num2 = num;
			if (num2 == null)
			{
				return base.ComputeOrder();
			}
			return num2.GetValueOrDefault();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000ADEC File Offset: 0x00008FEC
		protected override string ComputeShortDisplayName()
		{
			if (base.PrototypeCache.Display == null)
			{
				return base.ComputeShortDisplayName();
			}
			return base.PrototypeCache.Display.GetShortName();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AE12 File Offset: 0x00009012
		protected override bool ComputeShowForDisplay()
		{
			if (base.PrototypeCache.ScaffoldColumn == null)
			{
				return base.ComputeShowForDisplay();
			}
			return base.PrototypeCache.ScaffoldColumn.Scaffold;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AE38 File Offset: 0x00009038
		protected override bool ComputeShowForEdit()
		{
			if (base.PrototypeCache.ScaffoldColumn == null)
			{
				return base.ComputeShowForEdit();
			}
			return base.PrototypeCache.ScaffoldColumn.Scaffold;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AE60 File Offset: 0x00009060
		protected override string ComputeSimpleDisplayText()
		{
			if (base.Model != null && base.PrototypeCache.DisplayColumn != null && !string.IsNullOrEmpty(base.PrototypeCache.DisplayColumn.DisplayColumn))
			{
				PropertyInfo property = base.ModelType.GetProperty(base.PrototypeCache.DisplayColumn.DisplayColumn, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				CachedDataAnnotationsModelMetadata.ValidateDisplayColumnAttribute(base.PrototypeCache.DisplayColumn, property, base.ModelType);
				object value = property.GetValue(base.Model, new object[0]);
				if (value != null)
				{
					return value.ToString();
				}
			}
			return base.ComputeSimpleDisplayText();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AEF1 File Offset: 0x000090F1
		protected override string ComputeTemplateHint()
		{
			if (base.PrototypeCache.UIHint != null)
			{
				return base.PrototypeCache.UIHint.UIHint;
			}
			if (base.PrototypeCache.HiddenInput != null)
			{
				return "HiddenInput";
			}
			return base.ComputeTemplateHint();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AF2A File Offset: 0x0000912A
		protected override string ComputeWatermark()
		{
			if (base.PrototypeCache.Display == null)
			{
				return base.ComputeWatermark();
			}
			return base.PrototypeCache.Display.GetPrompt();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000AF50 File Offset: 0x00009150
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

		// Token: 0x04000103 RID: 259
		private bool _isEditFormatStringFromCache;
	}
}
