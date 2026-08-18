using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel
{
	// Token: 0x0200016F RID: 367
	public abstract class PeerResolver
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000ADE RID: 2782
		public abstract bool CanShareReferrals { get; }

		// Token: 0x06000ADF RID: 2783
		public abstract object Register(string meshId, PeerNodeAddress nodeAddress, TimeSpan timeout);

		// Token: 0x06000AE0 RID: 2784
		public abstract ReadOnlyCollection<PeerNodeAddress> Resolve(string meshId, int maxAddresses, TimeSpan timeout);

		// Token: 0x06000AE1 RID: 2785
		public abstract void Unregister(object registrationId, TimeSpan timeout);

		// Token: 0x06000AE2 RID: 2786
		public abstract void Update(object registrationId, PeerNodeAddress updatedNodeAddress, TimeSpan timeout);

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000287C0 File Offset: 0x000269C0
		public virtual void Initialize(EndpointAddress address, Binding binding, ClientCredentials credentials, PeerReferralPolicy referralPolicy)
		{
		}
	}
}
