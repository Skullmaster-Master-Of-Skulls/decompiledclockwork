using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000166 RID: 358
	public sealed class WSHttpSecurity
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x000280CE File Offset: 0x000262CE
		public WSHttpSecurity() : this(SecurityMode.Message, WSHttpSecurity.GetDefaultHttpTransportSecurity(), new NonDualMessageSecurityOverHttp())
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000280E1 File Offset: 0x000262E1
		internal WSHttpSecurity(SecurityMode mode, HttpTransportSecurity transportSecurity, NonDualMessageSecurityOverHttp messageSecurity)
		{
			this.mode = mode;
			this.transportSecurity = ((transportSecurity == null) ? WSHttpSecurity.GetDefaultHttpTransportSecurity() : transportSecurity);
			this.messageSecurity = ((messageSecurity == null) ? new NonDualMessageSecurityOverHttp() : messageSecurity);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00028114 File Offset: 0x00026314
		internal static HttpTransportSecurity GetDefaultHttpTransportSecurity()
		{
			return new HttpTransportSecurity
			{
				ClientCredentialType = HttpClientCredentialType.Windows
			};
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002812F File Offset: 0x0002632F
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00028137 File Offset: 0x00026337
		public SecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!SecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002815D File Offset: 0x0002635D
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00028165 File Offset: 0x00026365
		public HttpTransportSecurity Transport
		{
			get
			{
				return this.transportSecurity;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.transportSecurity = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00028186 File Offset: 0x00026386
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002818E File Offset: 0x0002638E
		public NonDualMessageSecurityOverHttp Message
		{
			get
			{
				return this.messageSecurity;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.messageSecurity = value;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000281AF File Offset: 0x000263AF
		internal void ApplyTransportSecurity(HttpsTransportBindingElement https)
		{
			if (this.mode == SecurityMode.TransportWithMessageCredential)
			{
				this.transportSecurity.ConfigureTransportProtectionOnly(https);
				return;
			}
			this.transportSecurity.ConfigureTransportProtectionAndAuthentication(https);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000281D3 File Offset: 0x000263D3
		internal static void ApplyTransportSecurity(HttpsTransportBindingElement transport, HttpTransportSecurity transportSecurity)
		{
			HttpTransportSecurity.ConfigureTransportProtectionAndAuthentication(transport, transportSecurity);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000281DC File Offset: 0x000263DC
		internal SecurityBindingElement CreateMessageSecurity(bool isReliableSessionEnabled, MessageSecurityVersion version)
		{
			if (this.mode == SecurityMode.Message || this.mode == SecurityMode.TransportWithMessageCredential)
			{
				return this.messageSecurity.CreateSecurityBindingElement(this.Mode == SecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, version);
			}
			return null;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00028208 File Offset: 0x00026408
		internal static bool TryCreate(SecurityBindingElement sbe, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSessionEnabled, out WSHttpSecurity security)
		{
			security = null;
			NonDualMessageSecurityOverHttp nonDualMessageSecurityOverHttp = null;
			SecurityMode securityMode;
			if (sbe != null)
			{
				mode &= (UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
				securityMode = SecurityModeHelper.ToSecurityMode(mode);
				if (!MessageSecurityOverHttp.TryCreate<NonDualMessageSecurityOverHttp>(sbe, securityMode == SecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, out nonDualMessageSecurityOverHttp))
				{
					return false;
				}
			}
			else
			{
				mode &= ~(UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
				securityMode = SecurityModeHelper.ToSecurityMode(mode);
			}
			security = new WSHttpSecurity(securityMode, transportSecurity, nonDualMessageSecurityOverHttp);
			return true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00028257 File Offset: 0x00026457
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeMode() || this.ShouldSerializeMessage() || this.ShouldSerializeTransport();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00028271 File Offset: 0x00026471
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMode()
		{
			return this.Mode != SecurityMode.Message;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002827F File Offset: 0x0002647F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessage()
		{
			return this.Message.InternalShouldSerialize();
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002828C File Offset: 0x0002648C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransport()
		{
			return this.Transport.ClientCredentialType != HttpClientCredentialType.Windows || this.Transport.ShouldSerializeProxyCredentialType() || this.Transport.ShouldSerializeRealm();
		}

		// Token: 0x04000BCA RID: 3018
		internal const SecurityMode DefaultMode = SecurityMode.Message;

		// Token: 0x04000BCB RID: 3019
		private SecurityMode mode;

		// Token: 0x04000BCC RID: 3020
		private HttpTransportSecurity transportSecurity;

		// Token: 0x04000BCD RID: 3021
		private NonDualMessageSecurityOverHttp messageSecurity;
	}
}
