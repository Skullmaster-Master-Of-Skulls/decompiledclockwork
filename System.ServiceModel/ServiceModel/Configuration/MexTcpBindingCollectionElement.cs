using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000665 RID: 1637
	public class MexTcpBindingCollectionElement : MexBindingBindingCollectionElement<CustomBinding, MexTcpBindingElement>
	{
		// Token: 0x06003EF7 RID: 16119 RVA: 0x000EF573 File Offset: 0x000ED773
		internal static MexTcpBindingCollectionElement GetBindingCollectionElement()
		{
			return (MexTcpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("mexTcpBinding");
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000EF584 File Offset: 0x000ED784
		protected internal override Binding GetDefault()
		{
			return MetadataExchangeBindings.GetBindingForScheme(Uri.UriSchemeNetTcp);
		}
	}
}
