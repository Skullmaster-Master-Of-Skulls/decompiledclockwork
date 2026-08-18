using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000607 RID: 1543
	public class CommonEndpointBehaviorElement : ServiceModelExtensionCollectionElement<BehaviorExtensionElement>
	{
		// Token: 0x06003B6D RID: 15213 RVA: 0x000E3818 File Offset: 0x000E1A18
		public CommonEndpointBehaviorElement() : base("behaviorExtensions")
		{
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000E3828 File Offset: 0x000E1A28
		public override void Add(BehaviorExtensionElement element)
		{
			if (element != null && !typeof(IEndpointBehavior).IsAssignableFrom(element.BehaviorType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidCommonEndpointBehaviorType", new object[]
				{
					element.ConfigurationElementName,
					typeof(IEndpointBehavior).FullName
				}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
			}
			base.Add(element);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000E38A8 File Offset: 0x000E1AA8
		public override bool CanAdd(BehaviorExtensionElement element)
		{
			if (element != null && !typeof(IEndpointBehavior).IsAssignableFrom(element.BehaviorType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidCommonEndpointBehaviorType", new object[]
				{
					element.ConfigurationElementName,
					typeof(IEndpointBehavior).FullName
				}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
			}
			return base.CanAdd(element);
		}
	}
}
