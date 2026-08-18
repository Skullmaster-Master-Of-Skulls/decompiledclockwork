using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000131 RID: 305
	[__DynamicallyInvokable]
	public sealed class BasicHttpSecurity
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x00022104 File Offset: 0x00020304
		public BasicHttpSecurity() : this(BasicHttpSecurityMode.None, new HttpTransportSecurity(), new BasicHttpMessageSecurity())
		{
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00022117 File Offset: 0x00020317
		private BasicHttpSecurity(BasicHttpSecurityMode mode, HttpTransportSecurity transportSecurity, BasicHttpMessageSecurity messageSecurity)
		{
			this.Mode = mode;
			this.transportSecurity = ((transportSecurity == null) ? new HttpTransportSecurity() : transportSecurity);
			this.messageSecurity = ((messageSecurity == null) ? new BasicHttpMessageSecurity() : messageSecurity);
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00022148 File Offset: 0x00020348
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00022150 File Offset: 0x00020350
		[__DynamicallyInvokable]
		public BasicHttpSecurityMode Mode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mode;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!BasicHttpSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00022176 File Offset: 0x00020376
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0002217E File Offset: 0x0002037E
		[__DynamicallyInvokable]
		public HttpTransportSecurity Transport
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transportSecurity;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transportSecurity = ((value == null) ? new HttpTransportSecurity() : value);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00022191 File Offset: 0x00020391
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00022199 File Offset: 0x00020399
		public BasicHttpMessageSecurity Message
		{
			get
			{
				return this.messageSecurity;
			}
			set
			{
				this.messageSecurity = ((value == null) ? new BasicHttpMessageSecurity() : value);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000221AC File Offset: 0x000203AC
		internal void EnableTransportSecurity(HttpsTransportBindingElement https)
		{
			if (this.mode == BasicHttpSecurityMode.TransportWithMessageCredential)
			{
				this.transportSecurity.ConfigureTransportProtectionOnly(https);
				return;
			}
			this.transportSecurity.ConfigureTransportProtectionAndAuthentication(https);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000221D0 File Offset: 0x000203D0
		internal static void EnableTransportSecurity(HttpsTransportBindingElement https, HttpTransportSecurity transportSecurity)
		{
			HttpTransportSecurity.ConfigureTransportProtectionAndAuthentication(https, transportSecurity);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000221D9 File Offset: 0x000203D9
		internal void EnableTransportAuthentication(HttpTransportBindingElement http)
		{
			this.transportSecurity.ConfigureTransportAuthentication(http);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000221E7 File Offset: 0x000203E7
		internal static bool IsEnabledTransportAuthentication(HttpTransportBindingElement http, HttpTransportSecurity transportSecurity)
		{
			return HttpTransportSecurity.IsConfiguredTransportAuthentication(http, transportSecurity);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000221F0 File Offset: 0x000203F0
		internal void DisableTransportAuthentication(HttpTransportBindingElement http)
		{
			this.transportSecurity.DisableTransportAuthentication(http);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000221FE File Offset: 0x000203FE
		internal SecurityBindingElement CreateMessageSecurity()
		{
			if (this.mode == BasicHttpSecurityMode.Message || this.mode == BasicHttpSecurityMode.TransportWithMessageCredential)
			{
				return this.messageSecurity.CreateMessageSecurity(this.Mode == BasicHttpSecurityMode.TransportWithMessageCredential);
			}
			return null;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00022228 File Offset: 0x00020428
		internal static bool TryCreate(SecurityBindingElement sbe, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, out BasicHttpSecurity security)
		{
			security = null;
			BasicHttpMessageSecurity basicHttpMessageSecurity = null;
			if (sbe != null)
			{
				mode &= (UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
				bool flag;
				if (!BasicHttpMessageSecurity.TryCreate(sbe, out basicHttpMessageSecurity, out flag))
				{
					return false;
				}
			}
			else
			{
				mode &= ~(UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
			}
			BasicHttpSecurityMode basicHttpSecurityMode = BasicHttpSecurityModeHelper.ToSecurityMode(mode);
			security = new BasicHttpSecurity(basicHttpSecurityMode, transportSecurity, basicHttpMessageSecurity);
			return SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(), sbe);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00022275 File Offset: 0x00020475
		internal bool InternalShouldSerialize()
		{
			return this.Mode != BasicHttpSecurityMode.None || this.ShouldSerializeMessage() || this.ShouldSerializeTransport();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002228F File Offset: 0x0002048F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessage()
		{
			return this.messageSecurity.InternalShouldSerialize();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002229C File Offset: 0x0002049C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransport()
		{
			return this.transportSecurity.InternalShouldSerialize();
		}

		// Token: 0x04000B11 RID: 2833
		internal const BasicHttpSecurityMode DefaultMode = BasicHttpSecurityMode.None;

		// Token: 0x04000B12 RID: 2834
		private BasicHttpSecurityMode mode;

		// Token: 0x04000B13 RID: 2835
		private HttpTransportSecurity transportSecurity;

		// Token: 0x04000B14 RID: 2836
		private BasicHttpMessageSecurity messageSecurity;
	}
}
