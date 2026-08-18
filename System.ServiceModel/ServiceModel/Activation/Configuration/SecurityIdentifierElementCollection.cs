using System;
using System.Configuration;
using System.Security.Principal;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D7 RID: 1495
	[ConfigurationCollection(typeof(SecurityIdentifierElement))]
	public sealed class SecurityIdentifierElementCollection : ServiceModelConfigurationElementCollection<SecurityIdentifierElement>
	{
		// Token: 0x06003A08 RID: 14856 RVA: 0x000DFE4C File Offset: 0x000DE04C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			SecurityIdentifierElement securityIdentifierElement = (SecurityIdentifierElement)element;
			return securityIdentifierElement.SecurityIdentifier.Value;
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000DFE80 File Offset: 0x000DE080
		internal void SetDefaultIdentifiers()
		{
			if (Iis7Helper.IisVersion >= 7)
			{
				base.Add(new SecurityIdentifierElement(new SecurityIdentifier("S-1-5-32-568")));
			}
			base.Add(new SecurityIdentifierElement(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null)));
			base.Add(new SecurityIdentifierElement(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null)));
			base.Add(new SecurityIdentifierElement(new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null)));
			base.Add(new SecurityIdentifierElement(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null)));
		}
	}
}
