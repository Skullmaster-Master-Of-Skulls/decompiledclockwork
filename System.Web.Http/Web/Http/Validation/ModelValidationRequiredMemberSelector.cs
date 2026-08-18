using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x02000186 RID: 390
	public sealed class ModelValidationRequiredMemberSelector : IRequiredMemberSelector
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x0002172B File Offset: 0x0001F92B
		public ModelValidationRequiredMemberSelector(ModelMetadataProvider metadataProvider, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (metadataProvider == null)
			{
				throw Error.ArgumentNull("metadataProvider");
			}
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			this._metadataProvider = metadataProvider;
			this._validatorProviders = validatorProviders.ToList<ModelValidatorProvider>();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00021770 File Offset: 0x0001F970
		public bool IsRequiredMember(MemberInfo member)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			if (this._validatorProviders == null || !this._validatorProviders.Any<ModelValidatorProvider>())
			{
				return false;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null || propertyInfo.GetGetMethod() == null)
			{
				return false;
			}
			ModelMetadata metadataForProperty = this._metadataProvider.GetMetadataForProperty(() => null, member.DeclaringType, member.Name);
			if (metadataForProperty.ModelType.IsNullable())
			{
				return false;
			}
			IEnumerable<ModelValidator> validators = metadataForProperty.GetValidators(this._validatorProviders);
			return validators.Any((ModelValidator validator) => validator.IsRequired);
		}

		// Token: 0x040002FE RID: 766
		private readonly ModelMetadataProvider _metadataProvider;

		// Token: 0x040002FF RID: 767
		private readonly List<ModelValidatorProvider> _validatorProviders;
	}
}
