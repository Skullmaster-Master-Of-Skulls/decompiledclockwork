using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066E RID: 1646
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class PeerTransportListenAddressValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000F0141 File Offset: 0x000EE341
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new PeerTransportListenAddressValidator();
			}
		}
	}
}
