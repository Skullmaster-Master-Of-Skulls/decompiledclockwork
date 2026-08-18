using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061B RID: 1563
	public class EndpointBehaviorElement : NamedServiceModelExtensionCollectionElement<BehaviorExtensionElement>
	{
		// Token: 0x06003C12 RID: 15378 RVA: 0x000E598E File Offset: 0x000E3B8E
		public EndpointBehaviorElement() : this(null)
		{
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000E5997 File Offset: 0x000E3B97
		public EndpointBehaviorElement(string name) : base("behaviorExtensions", name)
		{
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000E59A8 File Offset: 0x000E3BA8
		public override void Add(BehaviorExtensionElement element)
		{
			if (element != null)
			{
				if (element is ClearBehaviorElement || element is RemoveBehaviorElement)
				{
					base.AddItem(element);
					return;
				}
				if (!typeof(IEndpointBehavior).IsAssignableFrom(element.BehaviorType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidEndpointBehaviorType", new object[]
					{
						element.ConfigurationElementName,
						base.Name
					}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
				}
			}
			base.Add(element);
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000E5A38 File Offset: 0x000E3C38
		public override bool CanAdd(BehaviorExtensionElement element)
		{
			if (element != null)
			{
				if (element is ClearBehaviorElement || element is RemoveBehaviorElement)
				{
					return true;
				}
				if (!typeof(IEndpointBehavior).IsAssignableFrom(element.BehaviorType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidEndpointBehaviorType", new object[]
					{
						element.ConfigurationElementName,
						base.Name
					}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
				}
			}
			return base.CanAdd(element);
		}
	}
}
