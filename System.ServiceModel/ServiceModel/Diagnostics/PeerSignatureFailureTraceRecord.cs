using System;
using System.IdentityModel.Claims;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA9 RID: 2729
	internal class PeerSignatureFailureTraceRecord : PeerSecurityTraceRecord
	{
		// Token: 0x06006C0C RID: 27660 RVA: 0x00193A20 File Offset: 0x00191C20
		public PeerSignatureFailureTraceRecord(string meshId, Uri via, ClaimSet claimSet, Exception exception) : base(meshId, null, claimSet, exception)
		{
			this.via = via;
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06006C0D RID: 27661 RVA: 0x00193A34 File Offset: 0x00191C34
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/PeerSignatureFailureTraceRecord";
			}
		}

		// Token: 0x06006C0E RID: 27662 RVA: 0x00193A3B File Offset: 0x00191C3B
		internal override void WriteTo(XmlWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteElementString("Via", this.via.ToString());
		}

		// Token: 0x04003EAB RID: 16043
		private Uri via;
	}
}
