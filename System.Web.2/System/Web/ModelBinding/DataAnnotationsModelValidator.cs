using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web.Globalization;
using System.Web.Util;

namespace System.Web.ModelBinding
{
	// Token: 0x02000646 RID: 1606
	public class DataAnnotationsModelValidator : ModelValidator
	{
		// Token: 0x06004F64 RID: 20324 RVA: 0x00113B7F File Offset: 0x00111D7F
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute) : base(metadata, context)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			this.Attribute = attribute;
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x00113B9E File Offset: 0x00111D9E
		// (set) Token: 0x06004F66 RID: 20326 RVA: 0x00113BA6 File Offset: 0x00111DA6
		protected internal ValidationAttribute Attribute { get; private set; }

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x00113BB0 File Offset: 0x00111DB0
		protected internal string ErrorMessage
		{
			get
			{
				if (this.UseStringLocalizerProvider)
				{
					string localizedString = this.GetLocalizedString(this.Attribute.ErrorMessage, new object[0]);
					return localizedString ?? this.Attribute.FormatErrorMessage(base.Metadata.GetDisplayName());
				}
				return this.Attribute.FormatErrorMessage(base.Metadata.GetDisplayName());
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00113C0F File Offset: 0x00111E0F
		protected string GetLocalizedString(string name, params object[] arguments)
		{
			if (StringLocalizerProviders.DataAnnotationStringLocalizerProvider != null)
			{
				return StringLocalizerProviders.DataAnnotationStringLocalizerProvider.GetLocalizedString(Thread.CurrentThread.CurrentUICulture, name, arguments);
			}
			return null;
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06004F69 RID: 20329 RVA: 0x00113C30 File Offset: 0x00111E30
		public override bool IsRequired
		{
			get
			{
				return this.Attribute is RequiredAttribute;
			}
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00113C40 File Offset: 0x00111E40
		internal static ModelValidator Create(ModelMetadata metadata, ModelBindingExecutionContext context, ValidationAttribute attribute)
		{
			return new DataAnnotationsModelValidator(metadata, context, attribute);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x00113C4A File Offset: 0x00111E4A
		public override IEnumerable<ModelValidationResult> Validate(object container)
		{
			ValidationContext validationContext = new ValidationContext(container ?? base.Metadata.Model, null, null);
			validationContext.DisplayName = base.Metadata.GetDisplayName();
			string memberName = null;
			if (AppSettings.GetValidationMemberName)
			{
				memberName = (base.Metadata.PropertyName ?? base.Metadata.ModelType.Name);
				validationContext.MemberName = memberName;
			}
			ValidationResult validationResult = this.Attribute.GetValidationResult(base.Metadata.Model, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				yield return new ModelValidationResult
				{
					Message = this.GetValidationErrorMessage(validationResult),
					MemberName = DataAnnotationsModelValidator.GetValidationErrorMemberName(validationResult, memberName)
				};
			}
			yield break;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00113C61 File Offset: 0x00111E61
		protected virtual string GetLocalizedErrorMessage(string errorMessage)
		{
			return this.GetLocalizedString(errorMessage, new object[]
			{
				base.Metadata.GetDisplayName()
			});
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x00113C80 File Offset: 0x00111E80
		private string GetValidationErrorMessage(ValidationResult result)
		{
			string text;
			if (this.UseStringLocalizerProvider)
			{
				text = this.GetLocalizedErrorMessage(this.Attribute.ErrorMessage);
				text = (text ?? result.ErrorMessage);
			}
			else
			{
				text = result.ErrorMessage;
			}
			return text;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00113CC0 File Offset: 0x00111EC0
		private static string GetValidationErrorMemberName(ValidationResult result, string memberName)
		{
			string text = null;
			if (AppSettings.GetValidationMemberName)
			{
				text = result.MemberNames.FirstOrDefault<string>();
				if (string.Equals(text, memberName, StringComparison.Ordinal))
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00113CEF File Offset: 0x00111EEF
		private bool UseStringLocalizerProvider
		{
			get
			{
				return !string.IsNullOrEmpty(this.Attribute.ErrorMessage) && string.IsNullOrEmpty(this.Attribute.ErrorMessageResourceName) && this.Attribute.ErrorMessageResourceType == null;
			}
		}
	}
}
