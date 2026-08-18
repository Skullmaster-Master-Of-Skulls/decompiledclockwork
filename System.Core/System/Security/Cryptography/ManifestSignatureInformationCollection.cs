using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000104 RID: 260
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManifestSignatureInformationCollection : ReadOnlyCollection<ManifestSignatureInformation>
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0001CF47 File Offset: 0x0001B147
		internal ManifestSignatureInformationCollection(IList<ManifestSignatureInformation> signatureInformation) : base(signatureInformation)
		{
		}
	}
}
