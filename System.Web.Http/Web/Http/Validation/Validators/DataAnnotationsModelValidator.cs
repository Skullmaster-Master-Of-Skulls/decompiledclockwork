using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x02000196 RID: 406
	public class DataAnnotationsModelValidator : ModelValidator
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x00022E0C File Offset: 0x0002100C
		public DataAnnotationsModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders, ValidationAttribute attribute) : base(validatorProviders)
		{
			if (attribute == null)
			{
				throw Error.ArgumentNull("attribute");
			}
			this.Attribute = attribute;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00022E2A File Offset: 0x0002102A
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00022E32 File Offset: 0x00021032
		protected internal ValidationAttribute Attribute { get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00022E3B File Offset: 0x0002103B
		public override bool IsRequired
		{
			get
			{
				return this.Attribute is RequiredAttribute;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00022E4C File Offset: 0x0002104C
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			string displayName = metadata.GetDisplayName();
			ValidationContext validationContext = new ValidationContext(container ?? metadata.Model)
			{
				DisplayName = displayName,
				MemberName = displayName
			};
			ValidationResult validationResult = this.Attribute.GetValidationResult(metadata.Model, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				string text = validationResult.MemberNames.FirstOrDefault<string>();
				if (string.Equals(text, displayName, StringComparison.Ordinal))
				{
					text = null;
				}
				ModelValidationResult modelValidationResult = new ModelValidationResult
				{
					Message = validationResult.ErrorMessage,
					MemberName = text
				};
				return new ModelValidationResult[]
				{
					modelValidationResult
				};
			}
			return Enumerable.Empty<ModelValidationResult>();
		}
	}
}
