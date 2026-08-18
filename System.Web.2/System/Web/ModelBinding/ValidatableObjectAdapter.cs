using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200066D RID: 1645
	public class ValidatableObjectAdapter : ModelValidator
	{
		// Token: 0x06005056 RID: 20566 RVA: 0x001155E0 File Offset: 0x001137E0
		public ValidatableObjectAdapter(ModelMetadata metadata, ModelBindingExecutionContext context) : base(metadata, context)
		{
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x001155EC File Offset: 0x001137EC
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			object model = base.Metadata.Model;
			if (model == null)
			{
				return Enumerable.Empty<ModelValidationResult>();
			}
			IValidatableObject validatableObject = model as IValidatableObject;
			if (validatableObject == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("ValidatableObjectAdapter_IncompatibleType"), new object[]
				{
					typeof(IValidatableObject).FullName,
					model.GetType().FullName
				}));
			}
			ValidationContext validationContext = new ValidationContext(validatableObject, null, null);
			return this.ConvertResults(validatableObject.Validate(validationContext));
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0011566E File Offset: 0x0011386E
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
						IEnumerator<string> enumerator2 = null;
					}
				}
				result = null;
			}
			IEnumerator<ValidationResult> enumerator = null;
			yield break;
			yield break;
		}
	}
}
