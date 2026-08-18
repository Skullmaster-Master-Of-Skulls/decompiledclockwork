using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CA RID: 458
	public class PeerResolverSettings
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00036404 File Offset: 0x00034604
		public PeerResolverSettings()
		{
			this.customSettings = new PeerCustomResolverSettings();
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00036417 File Offset: 0x00034617
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x0003641F File Offset: 0x0003461F
		public PeerResolverMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!PeerResolverModeHelper.IsDefined(value))
				{
					PeerExceptionHelper.ThrowArgument_InvalidResolverMode(value);
				}
				this.mode = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00036436 File Offset: 0x00034636
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0003643E File Offset: 0x0003463E
		public PeerReferralPolicy ReferralPolicy
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

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0003646F File Offset: 0x0003466F
		public PeerCustomResolverSettings Custom
		{
			get
			{
				return this.customSettings;
			}
		}

		// Token: 0x0400179E RID: 6046
		private PeerReferralPolicy referralPolicy;

		// Token: 0x0400179F RID: 6047
		private PeerResolverMode mode;

		// Token: 0x040017A0 RID: 6048
		private PeerCustomResolverSettings customSettings;
	}
}
