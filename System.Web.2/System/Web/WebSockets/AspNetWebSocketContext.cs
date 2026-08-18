using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;

namespace System.Web.WebSockets
{
	// Token: 0x020001B9 RID: 441
	public abstract class AspNetWebSocketContext : WebSocketContext
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string AnonymousID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpApplicationStateBase Application
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string ApplicationPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Cache Cache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpClientCertificate ClientCertificate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00047F79 File Offset: 0x00046179
		public static int ConnectionCount
		{
			get
			{
				return AspNetWebSocketManager.Current.ActiveSocketCount;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override CookieCollection CookieCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string FilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsClientConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsDebuggingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override bool IsLocal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override bool IsSecureConnection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IDictionary Items
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual WindowsIdentity LogonUserIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override string Origin
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PathInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ProfileBase Profile
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RawUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override Uri RequestUri
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override string SecWebSocketKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override string SecWebSocketVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpServerUtilityBase Server
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual NameValueCollection ServerVariables
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual DateTime Timestamp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Uri UrlReferrer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override IPrincipal User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserAgent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserHostAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UserHostName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string[] UserLanguages
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override WebSocket WebSocket
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
