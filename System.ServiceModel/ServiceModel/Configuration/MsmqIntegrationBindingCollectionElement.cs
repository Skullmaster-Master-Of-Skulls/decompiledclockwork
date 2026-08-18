using System;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000642 RID: 1602
	public class MsmqIntegrationBindingCollectionElement : StandardBindingCollectionElement<MsmqIntegrationBinding, MsmqIntegrationBindingElement>
	{
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000EBB74 File Offset: 0x000E9D74
		internal static MsmqIntegrationBindingCollectionElement GetBindingCollectionElement()
		{
			return (MsmqIntegrationBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("msmqIntegrationBinding");
		}
	}
}
