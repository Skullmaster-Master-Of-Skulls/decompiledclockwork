using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200065B RID: 1627
	public class MexNamedPipeBindingCollectionElement : MexBindingBindingCollectionElement<CustomBinding, MexNamedPipeBindingElement>
	{
		// Token: 0x06003EAC RID: 16044 RVA: 0x000EE8CB File Offset: 0x000ECACB
		internal static MexNamedPipeBindingCollectionElement GetBindingCollectionElement()
		{
			return (MexNamedPipeBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("mexNamedPipeBinding");
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x000EE8DC File Offset: 0x000ECADC
		protected internal override Binding GetDefault()
		{
			return MetadataExchangeBindings.GetBindingForScheme(Uri.UriSchemeNetPipe);
		}
	}
}
