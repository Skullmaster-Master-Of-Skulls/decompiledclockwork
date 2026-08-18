using System;
using System.Configuration;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C5 RID: 1733
	public class ServiceBehaviorElement : NamedServiceModelExtensionCollectionElement<BehaviorExtensionElement>
	{
		// Token: 0x06004327 RID: 17191 RVA: 0x000FDC7C File Offset: 0x000FBE7C
		public ServiceBehaviorElement() : this(null)
		{
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000FDC85 File Offset: 0x000FBE85
		public ServiceBehaviorElement(string name) : base("behaviorExtensions", name)
		{
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000FDC94 File Offset: 0x000FBE94
		public override void Add(BehaviorExtensionElement element)
		{
			if (element != null)
			{
				if (element is ClearBehaviorElement || element is RemoveBehaviorElement)
				{
					base.AddItem(element);
					return;
				}
				if (!typeof(IServiceBehavior).IsAssignableFrom(element.BehaviorType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceBehaviorType", new object[]
					{
						element.ConfigurationElementName,
						base.Name
					}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
				}
			}
			base.Add(element);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000FDD24 File Offset: 0x000FBF24
		public override bool CanAdd(BehaviorExtensionElement element)
		{
			if (element != null)
			{
				if (element is ClearBehaviorElement || element is RemoveBehaviorElement)
				{
					return true;
				}
				if (!typeof(IServiceBehavior).IsAssignableFrom(element.BehaviorType))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceBehaviorType", new object[]
					{
						element.ConfigurationElementName,
						base.Name
					}), element.ElementInformation.Source, element.ElementInformation.LineNumber));
				}
			}
			return base.CanAdd(element);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x000FDDAC File Offset: 0x000FBFAC
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}
	}
}
