using System;
using System.ComponentModel;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A57 RID: 2647
	public sealed class PnrpPeerResolverBindingElement : PeerResolverBindingElement
	{
		// Token: 0x0600687D RID: 26749 RVA: 0x00186517 File Offset: 0x00184717
		public PnrpPeerResolverBindingElement()
		{
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x0018651F File Offset: 0x0018471F
		public PnrpPeerResolverBindingElement(PeerReferralPolicy referralPolicy)
		{
			this.referralPolicy = referralPolicy;
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x0018652E File Offset: 0x0018472E
		private PnrpPeerResolverBindingElement(PnrpPeerResolverBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.referralPolicy = elementToBeCloned.referralPolicy;
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x00186543 File Offset: 0x00184743
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x0018654B File Offset: 0x0018474B
		public override PeerReferralPolicy ReferralPolicy
		{
			get
			{
				return this.referralPolicy;
			}
			set
			{
				if (!PeerReferralPolicyHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(PeerReferralPolicy)));
				}
				this.referralPolicy = value;
			}
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x0018657C File Offset: 0x0018477C
		public override BindingElement Clone()
		{
			return new PnrpPeerResolverBindingElement(this);
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x00186584 File Offset: 0x00184784
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x001865B0 File Offset: 0x001847B0
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x001865DC File Offset: 0x001847DC
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006886 RID: 26758 RVA: 0x00186608 File Offset: 0x00184808
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006887 RID: 26759 RVA: 0x00186634 File Offset: 0x00184834
		public override PeerResolver CreatePeerResolver()
		{
			return new PnrpPeerResolver(this.referralPolicy);
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x00186641 File Offset: 0x00184841
		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}

		// Token: 0x04003BEE RID: 15342
		private PeerReferralPolicy referralPolicy;
	}
}
