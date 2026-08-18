using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062C RID: 1580
	public class MexHttpsBindingCollectionElement : MexBindingBindingCollectionElement<WSHttpBinding, MexHttpsBindingElement>
	{
		// Token: 0x06003C7F RID: 15487 RVA: 0x000E6DF1 File Offset: 0x000E4FF1
		internal static MexHttpsBindingCollectionElement GetBindingCollectionElement()
		{
			return (MexHttpsBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("mexHttpsBinding");
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000E6E02 File Offset: 0x000E5002
		protected internal override Binding GetDefault()
		{
			return MetadataExchangeBindings.GetBindingForScheme(Uri.UriSchemeHttps);
		}
	}
}
