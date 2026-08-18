using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000673 RID: 1651
	[ConfigurationCollection(typeof(PolicyImporterElement), AddItemName = "extension")]
	public sealed class PolicyImporterElementCollection : ServiceModelEnhancedConfigurationElementCollection<PolicyImporterElement>
	{
		// Token: 0x06003F58 RID: 16216 RVA: 0x000F0635 File Offset: 0x000EE835
		public PolicyImporterElementCollection() : base("extension")
		{
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000F0644 File Offset: 0x000EE844
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			PolicyImporterElement policyImporterElement = (PolicyImporterElement)element;
			return policyImporterElement.Type;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000F0674 File Offset: 0x000EE874
		internal void SetDefaults()
		{
			base.Add(new PolicyImporterElement(typeof(PrivacyNoticeBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(UseManagedPresentationBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(TransactionFlowBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(ReliableSessionBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(SecurityBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(CompositeDuplexBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(OneWayBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(MessageEncodingBindingElementImporter)));
			base.Add(new PolicyImporterElement(typeof(TransportBindingElementImporter)));
			base.Add(new PolicyImporterElement("System.ServiceModel.Channels.UdpTransportImporter, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
		}
	}
}
