using System;
using System.Configuration;
using System.Net;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066D RID: 1645
	internal class PeerTransportListenAddressValidator : ConfigurationValidatorBase
	{
		// Token: 0x06003F34 RID: 16180 RVA: 0x000F0122 File Offset: 0x000EE322
		public override bool CanValidate(Type type)
		{
			return type == typeof(IPAddress);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x000F0134 File Offset: 0x000EE334
		public override void Validate(object value)
		{
			PeerValidateHelper.ValidateListenIPAddress(value as IPAddress);
		}
	}
}
