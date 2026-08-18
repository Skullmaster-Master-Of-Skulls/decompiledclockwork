using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000133 RID: 307
	public class ValidatableObjectAdapter : ModelValidator
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00015A20 File Offset: 0x00013C20
		public ValidatableObjectAdapter(ModelMetadata metadata, ControllerContext context) : base(metadata, context)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00015A2C File Offset: 0x00013C2C
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
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ValidatableObjectAdapter_IncompatibleType, new object[]
				{
					typeof(IValidatableObject).FullName,
					model.GetType().FullName
				}));
			}
			ValidationContext validationContext = new ValidationContext(validatableObject, null, null);
			return this.ConvertResults(validatableObject.Validate(validationContext));
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00015D94 File Offset: 0x00013F94
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
