using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A11 RID: 2577
	internal class PeerMessageFilter
	{
		// Token: 0x060065F8 RID: 26104 RVA: 0x0017BCC5 File Offset: 0x00179EC5
		public PeerMessageFilter(Uri via) : this(via, null)
		{
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x0017BCCF File Offset: 0x00179ECF
		public PeerMessageFilter(Uri via, EndpointAddress to)
		{
			this.via = via;
			if (to != null)
			{
				this.actingAs = to.Uri;
			}
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x0017BCF4 File Offset: 0x00179EF4
		public bool Match(Uri peerVia, Uri toCond)
		{
			return !(peerVia == null) && Uri.Compare(this.via, peerVia, UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0 && (!(this.actingAs != null) || Uri.Compare(this.actingAs, toCond, UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x04003AD3 RID: 15059
		private Uri via;

		// Token: 0x04003AD4 RID: 15060
		private Uri actingAs;
	}
}
