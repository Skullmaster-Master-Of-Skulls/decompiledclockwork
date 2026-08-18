using System;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A22 RID: 2594
	public abstract class PeerResolverBindingElement : BindingElement
	{
		// Token: 0x06006727 RID: 26407 RVA: 0x001817BA File Offset: 0x0017F9BA
		protected PeerResolverBindingElement()
		{
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x001817C2 File Offset: 0x0017F9C2
		protected PeerResolverBindingElement(PeerResolverBindingElement other) : base(other)
		{
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06006729 RID: 26409
		// (set) Token: 0x0600672A RID: 26410
		public abstract PeerReferralPolicy ReferralPolicy { get; set; }

		// Token: 0x0600672B RID: 26411
		public abstract PeerResolver CreatePeerResolver();
	}
}
