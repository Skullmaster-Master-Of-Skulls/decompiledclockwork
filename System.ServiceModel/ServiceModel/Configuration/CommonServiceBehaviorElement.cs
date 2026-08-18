using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000608 RID: 1544
	public class CommonServiceBehaviorElement : ServiceModelExtensionCollectionElement<BehaviorExtensionElement>
	{
		// Token: 0x06003B70 RID: 15216 RVA: 0x000E3927 File Offset: 0x000E1B27
		public CommonServiceBehaviorElement() : base("behaviorExtensions")
		{
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000E3934 File Offset: 0x000E1B34
		public override void Add(BehaviorExtensionElement element)
		{
			if (element != null && !typeof(IServiceBehavior).IsAssignableFrom(element.BehaviorType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidCommonServiceBehaviorType", new object[]
				{
					element.ConfigurationElementName,
					typeof(IServiceBehavior).FullName
				}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
			}
			base.Add(element);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000E39B4 File Offset: 0x000E1BB4
		public override bool CanAdd(BehaviorExtensionElement element)
		{
			if (element != null && !typeof(IServiceBehavior).IsAssignableFrom(element.BehaviorType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidCommonServiceBehaviorType", new object[]
				{
					element.ConfigurationElementName,
					typeof(IServiceBehavior).FullName
				}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
			}
			return base.CanAdd(element);
		}
	}
}
