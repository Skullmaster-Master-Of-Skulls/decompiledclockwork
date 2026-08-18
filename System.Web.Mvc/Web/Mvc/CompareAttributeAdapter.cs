using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000075 RID: 117
	internal class CompareAttributeAdapter : DataAnnotationsModelValidator<CompareAttribute>
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000B013 File Offset: 0x00009213
		public CompareAttributeAdapter(ModelMetadata metadata, ControllerContext context, CompareAttribute attribute) : base(metadata, context, new CompareAttributeAdapter.CompareAttributeWrapper(attribute, metadata))
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B10C File Offset: 0x0000930C
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			yield return new ModelClientValidationEqualToRule(base.ErrorMessage, CompareAttributeAdapter.FormatPropertyForClientValidation(base.Attribute.OtherProperty));
			yield break;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B129 File Offset: 0x00009329
		private static string FormatPropertyForClientValidation(string property)
		{
			return "*." + property;
		}

		// Token: 0x02000076 RID: 118
		private sealed class CompareAttributeWrapper : CompareAttribute
		{
			// Token: 0x060003BA RID: 954 RVA: 0x0000B14C File Offset: 0x0000934C
			public CompareAttributeWrapper(CompareAttribute attribute, ModelMetadata metadata) : base(attribute.OtherProperty)
			{
				this._otherPropertyDisplayName = attribute.OtherPropertyDisplayName;
				if (this._otherPropertyDisplayName == null && metadata.ContainerType != null)
				{
					this._otherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, attribute.OtherProperty).GetDisplayName();
				}
				if (this._otherPropertyDisplayName == null)
				{
					this._otherPropertyDisplayName = attribute.OtherProperty;
				}
				if (!string.IsNullOrEmpty(attribute.ErrorMessage) || !string.IsNullOrEmpty(attribute.ErrorMessageResourceName) || attribute.ErrorMessageResourceType != null)
				{
					base.ErrorMessage = attribute.ErrorMessage;
					base.ErrorMessageResourceName = attribute.ErrorMessageResourceName;
					base.ErrorMessageResourceType = attribute.ErrorMessageResourceType;
				}
			}

			// Token: 0x060003BB RID: 955 RVA: 0x0000B234 File Offset: 0x00009434
			public override string FormatErrorMessage(string name)
			{
				return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
				{
					name,
					this._otherPropertyDisplayName
				});
			}

			// Token: 0x04000104 RID: 260
			private readonly string _otherPropertyDisplayName;
		}
	}
}
