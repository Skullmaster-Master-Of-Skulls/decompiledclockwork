using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006F RID: 111
	[MessageContract(IsWrapped = false)]
	internal class ProbeMessageCD1
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000FE84 File Offset: 0x0000E084
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000FE8C File Offset: 0x0000E08C
		[MessageBodyMember(Name = "Probe", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public FindCriteriaCD1 Probe { get; set; }
	}
}
