using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000600 RID: 1536
	[ConfigurationCollection(typeof(ClaimTypeElement))]
	public sealed class ClaimTypeElementCollection : ServiceModelConfigurationElementCollection<ClaimTypeElement>
	{
		// Token: 0x06003B36 RID: 15158 RVA: 0x000E2E00 File Offset: 0x000E1000
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ClaimTypeElement claimTypeElement = (ClaimTypeElement)element;
			return claimTypeElement.ClaimType;
		}
	}
}
