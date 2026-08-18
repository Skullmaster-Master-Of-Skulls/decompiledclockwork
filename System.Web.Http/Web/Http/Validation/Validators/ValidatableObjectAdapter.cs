using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x02000195 RID: 405
	public class ValidatableObjectAdapter : ModelValidator
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x00022A99 File Offset: 0x00020C99
		public ValidatableObjectAdapter(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
		{
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00022AA4 File Offset: 0x00020CA4
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			object model = metadata.Model;
			if (model == null)
			{
				return Enumerable.Empty<ModelValidationResult>();
			}
			IValidatableObject validatableObject = model as IValidatableObject;
			if (validatableObject == null)
			{
				throw Error.InvalidOperation(SRResources.ValidatableObjectAdapter_IncompatibleType, new object[]
				{
					model.GetType()
				});
			}
			ValidationContext validationContext = new ValidationContext(validatableObject, null, null);
			return this.ConvertResults(validatableObject.Validate(validationContext));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00022DE8 File Offset: 0x00020FE8
		private IEnumerable<ModelValidationResult> ConvertResults(IEnumerable<ValidationResult> results)
		{
			foreach (ValidationResult result in results)
			{
				if (result != ValidationResult.Success)
				{
					if (result.MemberNames == null || !result.MemberNames.Any<string>())
					{
						yield return new ModelValidationResult
						{
							Message = result.ErrorMessage
						};
					}
					else
					{
						foreach (string memberName in result.MemberNames)
						{
							yield return new ModelValidationResult
							{
								Message = result.ErrorMessage,
								MemberName = memberName
							};
						}
					}
				}
			}
			yield break;
		}
	}
}
