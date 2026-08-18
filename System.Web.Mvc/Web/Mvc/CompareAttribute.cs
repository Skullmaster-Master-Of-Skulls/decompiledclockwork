using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000082 RID: 130
	[Obsolete("The recommended alternative is to use the System.ComponentModel.DataAnnotations.CompareAttribute type, which has the same functionality as this type.")]
	[AttributeUsage(AttributeTargets.Property)]
	public class CompareAttribute : ValidationAttribute, IClientValidatable
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000B6D9 File Offset: 0x000098D9
		public CompareAttribute(string otherProperty) : base(MvcResources.CompareAttribute_MustMatch)
		{
			if (otherProperty == null)
			{
				throw new ArgumentNullException("otherProperty");
			}
			this.OtherProperty = otherProperty;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000B6FB File Offset: 0x000098FB
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000B703 File Offset: 0x00009903
		public string OtherProperty { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B70C File Offset: 0x0000990C
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000B714 File Offset: 0x00009914
		public string OtherPropertyDisplayName { get; internal set; }

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B720 File Offset: 0x00009920
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
			{
				name,
				this.OtherPropertyDisplayName ?? this.OtherProperty
			});
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B774 File Offset: 0x00009974
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);
			if (property == null)
			{
				return new ValidationResult(string.Format(CultureInfo.CurrentCulture, MvcResources.CompareAttribute_UnknownProperty, new object[]
				{
					this.OtherProperty
				}));
			}
			object value2 = property.GetValue(validationContext.ObjectInstance, null);
			if (!object.Equals(value, value2))
			{
				if (this.OtherPropertyDisplayName == null)
				{
					this.OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => validationContext.ObjectInstance, validationContext.ObjectType, this.OtherProperty).GetDisplayName();
				}
				return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
			}
			return null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B84C File Offset: 0x00009A4C
		public static string FormatPropertyForClientValidation(string property)
		{
			if (property == null)
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "property");
			}
			return "*." + property;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000BA28 File Offset: 0x00009C28
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			if (metadata.ContainerType != null && this.OtherPropertyDisplayName == null)
			{
				this.OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, this.OtherProperty).GetDisplayName();
			}
			yield return new ModelClientValidationEqualToRule(this.FormatErrorMessage(metadata.GetDisplayName()), CompareAttribute.FormatPropertyForClientValidation(this.OtherProperty));
			yield break;
		}
	}
}
