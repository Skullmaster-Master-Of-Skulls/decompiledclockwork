using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200012B RID: 299
	public sealed class BasicHttpsSecurity
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00021CBE File Offset: 0x0001FEBE
		public BasicHttpsSecurity() : this(BasicHttpsSecurityMode.Transport, new HttpTransportSecurity(), new BasicHttpMessageSecurity())
		{
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00021CD4 File Offset: 0x0001FED4
		private BasicHttpsSecurity(BasicHttpsSecurityMode mode, HttpTransportSecurity transportSecurity, BasicHttpMessageSecurity messageSecurity)
		{
			if (!BasicHttpsSecurityModeHelper.IsDefined(mode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("mode"));
			}
			HttpTransportSecurity transport = (transportSecurity == null) ? new HttpTransportSecurity() : transportSecurity;
			BasicHttpMessageSecurity message = (messageSecurity == null) ? new BasicHttpMessageSecurity() : messageSecurity;
			BasicHttpSecurityMode mode2 = BasicHttpsSecurityModeHelper.ToBasicHttpSecurityMode(mode);
			this.basicHttpSecurity = new BasicHttpSecurity
			{
				Mode = mode2,
				Transport = transport,
				Message = message
			};
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00021D43 File Offset: 0x0001FF43
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00021D55 File Offset: 0x0001FF55
		public BasicHttpsSecurityMode Mode
		{
			get
			{
				return BasicHttpsSecurityModeHelper.ToBasicHttpsSecurityMode(this.basicHttpSecurity.Mode);
			}
			set
			{
				if (!BasicHttpsSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.basicHttpSecurity.Mode = BasicHttpsSecurityModeHelper.ToBasicHttpSecurityMode(value);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00021D85 File Offset: 0x0001FF85
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00021D92 File Offset: 0x0001FF92
		public HttpTransportSecurity Transport
		{
			get
			{
				return this.basicHttpSecurity.Transport;
			}
			set
			{
				this.basicHttpSecurity.Transport = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00021DAD File Offset: 0x0001FFAD
		public BasicHttpMessageSecurity Message
		{
			get
			{
				return this.basicHttpSecurity.Message;
			}
			set
			{
				this.basicHttpSecurity.Message = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00021DBB File Offset: 0x0001FFBB
		internal BasicHttpSecurity BasicHttpSecurity
		{
			get
			{
				return this.basicHttpSecurity;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00021DC3 File Offset: 0x0001FFC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessage()
		{
			return this.basicHttpSecurity.ShouldSerializeMessage();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransport()
		{
			return this.basicHttpSecurity.ShouldSerializeTransport();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		internal static BasicHttpSecurity ToBasicHttpSecurity(BasicHttpsSecurity basicHttpsSecurity)
		{
			return new BasicHttpSecurity
			{
				Message = basicHttpsSecurity.Message,
				Transport = basicHttpsSecurity.Transport,
				Mode = BasicHttpsSecurityModeHelper.ToBasicHttpSecurityMode(basicHttpsSecurity.Mode)
			};
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00021E20 File Offset: 0x00020020
		internal static BasicHttpsSecurity ToBasicHttpsSecurity(BasicHttpSecurity basicHttpSecurity)
		{
			return new BasicHttpsSecurity
			{
				Message = basicHttpSecurity.Message,
				Transport = basicHttpSecurity.Transport,
				Mode = BasicHttpsSecurityModeHelper.ToBasicHttpsSecurityMode(basicHttpSecurity.Mode)
			};
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00021E5D File Offset: 0x0002005D
		internal static void EnableTransportSecurity(HttpsTransportBindingElement https, HttpTransportSecurity transportSecurity)
		{
			BasicHttpSecurity.EnableTransportSecurity(https, transportSecurity);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00021E66 File Offset: 0x00020066
		internal static bool IsEnabledTransportAuthentication(HttpTransportBindingElement http, HttpTransportSecurity transportSecurity)
		{
			return BasicHttpSecurity.IsEnabledTransportAuthentication(http, transportSecurity);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00021E6F File Offset: 0x0002006F
		internal void EnableTransportSecurity(HttpsTransportBindingElement https)
		{
			this.basicHttpSecurity.EnableTransportSecurity(https);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00021E7D File Offset: 0x0002007D
		internal void EnableTransportAuthentication(HttpTransportBindingElement http)
		{
			this.basicHttpSecurity.EnableTransportAuthentication(http);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00021E8B File Offset: 0x0002008B
		internal void DisableTransportAuthentication(HttpTransportBindingElement http)
		{
			this.basicHttpSecurity.DisableTransportAuthentication(http);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00021E99 File Offset: 0x00020099
		internal SecurityBindingElement CreateMessageSecurity()
		{
			return this.basicHttpSecurity.CreateMessageSecurity();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00021EA6 File Offset: 0x000200A6
		internal bool InternalShouldSerialize()
		{
			return this.Mode != BasicHttpsSecurityMode.Transport || this.ShouldSerializeMessage() || this.ShouldSerializeTransport();
		}

		// Token: 0x04000B06 RID: 2822
		internal const BasicHttpsSecurityMode DefaultMode = BasicHttpsSecurityMode.Transport;

		// Token: 0x04000B07 RID: 2823
		private BasicHttpSecurity basicHttpSecurity;
	}
}
