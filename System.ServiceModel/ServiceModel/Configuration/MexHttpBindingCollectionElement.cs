using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062A RID: 1578
	public class MexHttpBindingCollectionElement : MexBindingBindingCollectionElement<WSHttpBinding, MexHttpBindingElement>
	{
		// Token: 0x06003C7A RID: 15482 RVA: 0x000E6DBA File Offset: 0x000E4FBA
		internal static MexHttpBindingCollectionElement GetBindingCollectionElement()
		{
			return (MexHttpBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("mexHttpBinding");
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000E6DCB File Offset: 0x000E4FCB
		protected internal override Binding GetDefault()
		{
			return MetadataExchangeBindings.GetBindingForScheme(Uri.UriSchemeHttp);
		}
	}
}
