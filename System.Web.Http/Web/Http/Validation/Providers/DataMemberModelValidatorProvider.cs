using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x0200018F RID: 399
	public class DataMemberModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x000224E8 File Offset: 0x000206E8
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			if (metadata.ContainerType == null || string.IsNullOrEmpty(metadata.PropertyName))
			{
				return Enumerable.Empty<ModelValidator>();
			}
			if (DataMemberModelValidatorProvider.IsRequiredDataMember(metadata.ContainerType, attributes))
			{
				return new RequiredMemberModelValidator[]
				{
					new RequiredMemberModelValidator(validatorProviders)
				};
			}
			return Enumerable.Empty<ModelValidator>();
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002253C File Offset: 0x0002073C
		internal static bool IsRequiredDataMember(Type containerType, IEnumerable<Attribute> attributes)
		{
			DataMemberAttribute dataMemberAttribute = attributes.OfType<DataMemberAttribute>().FirstOrDefault<DataMemberAttribute>();
			if (dataMemberAttribute != null)
			{
				bool flag = TypeDescriptorHelper.Get(containerType).GetAttributes().OfType<DataContractAttribute>().Any<DataContractAttribute>();
				if (flag && dataMemberAttribute.IsRequired)
				{
					return true;
				}
			}
			return false;
		}
	}
}
