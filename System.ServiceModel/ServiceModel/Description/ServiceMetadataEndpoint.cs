using System;
using System.Diagnostics;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F3 RID: 1011
	[DebuggerDisplay("Address={address}")]
	[DebuggerDisplay("Name={name}")]
	public class ServiceMetadataEndpoint : ServiceEndpoint
	{
		// Token: 0x06002617 RID: 9751 RVA: 0x000898B9 File Offset: 0x00087AB9
		public ServiceMetadataEndpoint() : this(MetadataExchangeBindings.CreateMexHttpBinding(), null)
		{
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000898C7 File Offset: 0x00087AC7
		public ServiceMetadataEndpoint(EndpointAddress address) : this(MetadataExchangeBindings.CreateMexHttpBinding(), address)
		{
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000898D5 File Offset: 0x00087AD5
		public ServiceMetadataEndpoint(Binding binding, EndpointAddress address) : base(ServiceMetadataBehavior.MexContract, binding, address)
		{
			base.IsSystemEndpoint = true;
		}
	}
}
