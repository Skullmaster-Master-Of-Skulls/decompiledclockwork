using System;
using System.Collections.ObjectModel;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000ED RID: 237
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngPropertyCollection : Collection<CngProperty>
	{
	}
}
