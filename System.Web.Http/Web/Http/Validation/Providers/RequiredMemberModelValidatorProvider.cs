using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Metadata;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x02000191 RID: 401
	public class RequiredMemberModelValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x000229CE File Offset: 0x00020BCE
		public RequiredMemberModelValidatorProvider(IRequiredMemberSelector requiredMemberSelector)
		{
			this._requiredMemberSelector = requiredMemberSelector;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000229E0 File Offset: 0x00020BE0
		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			string propertyName = metadata.PropertyName;
			if (propertyName != null)
			{
				PropertyInfo property = metadata.ContainerType.GetProperty(propertyName);
				if (this._requiredMemberSelector.IsRequiredMember(property))
				{
					return new ModelValidator[]
					{
						new RequiredMemberModelValidator(validatorProviders)
					};
				}
			}
			return Enumerable.Empty<ModelValidator>();
		}

		// Token: 0x0400030C RID: 780
		private IRequiredMemberSelector _requiredMemberSelector;
	}
}
