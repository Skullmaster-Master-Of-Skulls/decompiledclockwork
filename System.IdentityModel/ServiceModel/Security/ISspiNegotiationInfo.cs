using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000C RID: 12
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal interface ISspiNegotiationInfo
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95
		ISspiNegotiation SspiNegotiation { get; }
	}
}
