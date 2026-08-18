using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000043 RID: 67
	public class DataAnnotationsModelValidator : ModelValidator
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00006454 File Offset: 0x00004654
		public DataAnnotationsModelValidator(ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute) : base(metadata, context)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this.Attribute = attribute;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006473 File Offset: 0x00004673
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000647B File Offset: 0x0000467B
		protected internal ValidationAttribute Attribute { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006484 File Offset: 0x00004684
		protected internal string ErrorMessage
		{
			get
			{
				return this.Attribute.FormatErrorMessage(base.Metadata.GetDisplayName());
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000649C File Offset: 0x0000469C
		public override bool IsRequired
		{
			get
			{
				return this.Attribute is RequiredAttribute;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000064AC File Offset: 0x000046AC
		internal static ModelValidator Create(ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute)
		{
			return new DataAnnotationsModelValidator(metadata, context, attribute);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000064B8 File Offset: 0x000046B8
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			IEnumerable<ModelClientValidationRule> enumerable = base.GetClientValidationRules();
			IClientValidatable clientValidatable = this.Attribute as IClientValidatable;
			if (clientValidatable != null)
			{
				enumerable = enumerable.Concat(clientValidatable.GetClientValidationRules(base.Metadata, base.ControllerContext));
			}
			return enumerable;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000064F8 File Offset: 0x000046F8
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			string text = base.Metadata.PropertyName ?? base.Metadata.ModelType.Name;
			ValidationContext validationContext = new ValidationContext(container ?? base.Metadata.Model)
			{
				DisplayName = base.Metadata.GetDisplayName(),
				MemberName = text
			};
			ValidationResult validationResult = this.Attribute.GetValidationResult(base.Metadata.Model, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				string text2 = validationResult.MemberNames.FirstOrDefault<string>();
				if (string.Equals(text2, text, StringComparison.Ordinal))
				{
					text2 = null;
				}
				ModelValidationResult modelValidationResult = new ModelValidationResult
				{
					Message = validationResult.ErrorMessage,
					MemberName = text2
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
