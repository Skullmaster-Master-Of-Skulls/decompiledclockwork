using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x0200007A RID: 122
	internal class FileExtensionsAttributeAdapter : DataAnnotationsModelValidator<FileExtensionsAttribute>
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000B3C1 File Offset: 0x000095C1
		public FileExtensionsAttributeAdapter(ModelMetadata metadata, ControllerContext context, FileExtensionsAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			ModelClientValidationRule rule = new ModelClientValidationRule
			{
				ValidationType = "extension",
				ErrorMessage = base.ErrorMessage
			};
			rule.ValidationParameters["extension"] = base.Attribute.Extensions;
			yield return rule;
			yield break;
		}
	}
}
