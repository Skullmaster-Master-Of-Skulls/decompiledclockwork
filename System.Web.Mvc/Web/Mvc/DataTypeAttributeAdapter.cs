using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000077 RID: 119
	internal class DataTypeAttributeAdapter : DataAnnotationsModelValidator
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000B266 File Offset: 0x00009466
		public DataTypeAttributeAdapter(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute, string ruleName) : base(metadata, context, attribute)
		{
			if (string.IsNullOrEmpty(ruleName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "ruleName");
			}
			this.RuleName = ruleName;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000B292 File Offset: 0x00009492
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000B29A File Offset: 0x0000949A
		public string RuleName { get; set; }

		// Token: 0x060003BF RID: 959 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			yield return new ModelClientValidationRule
			{
				ValidationType = this.RuleName,
				ErrorMessage = base.ErrorMessage
			};
			yield break;
		}
	}
}
