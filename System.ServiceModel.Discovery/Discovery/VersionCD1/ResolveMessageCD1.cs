using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000075 RID: 117
	[MessageContract(IsWrapped = false)]
	internal class ResolveMessageCD1
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00010034 File Offset: 0x0000E234
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001003C File Offset: 0x0000E23C
		[MessageBodyMember(Name = "Resolve", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public ResolveCriteriaCD1 Resolve { get; set; }
	}
}
