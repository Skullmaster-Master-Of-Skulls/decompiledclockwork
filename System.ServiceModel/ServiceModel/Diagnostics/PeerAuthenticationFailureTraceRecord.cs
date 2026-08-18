using System;
using System.IdentityModel.Claims;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA8 RID: 2728
	internal class PeerAuthenticationFailureTraceRecord : PeerSecurityTraceRecord
	{
		// Token: 0x06006C09 RID: 27657 RVA: 0x00193A00 File Offset: 0x00191C00
		public PeerAuthenticationFailureTraceRecord(string meshId, string remoteAddress, ClaimSet claimSet, Exception e) : base(meshId, remoteAddress, claimSet, e)
		{
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x00193A0D File Offset: 0x00191C0D
		public PeerAuthenticationFailureTraceRecord(string meshId, string remoteAddress) : base(meshId, remoteAddress, null, null)
		{
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06006C0B RID: 27659 RVA: 0x00193A19 File Offset: 0x00191C19
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerAuthenticationTraceRecord";
			}
		}
	}
}
