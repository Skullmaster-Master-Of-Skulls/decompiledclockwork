using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200015C RID: 348
	public sealed class WSFederationHttpSecurity
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x00026A6F File Offset: 0x00024C6F
		public WSFederationHttpSecurity() : this(WSFederationHttpSecurityMode.Message, new FederatedMessageSecurityOverHttp())
		{
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00026A7D File Offset: 0x00024C7D
		private WSFederationHttpSecurity(WSFederationHttpSecurityMode mode, FederatedMessageSecurityOverHttp messageSecurity)
		{
			this.mode = mode;
			this.messageSecurity = ((messageSecurity == null) ? new FederatedMessageSecurityOverHttp() : messageSecurity);
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00026A9D File Offset: 0x00024C9D
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00026AA5 File Offset: 0x00024CA5
		public WSFederationHttpSecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!WSFederationHttpSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00026ACB File Offset: 0x00024CCB
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x00026AD3 File Offset: 0x00024CD3
		public FederatedMessageSecurityOverHttp Message
		{
			get
			{
				return this.messageSecurity;
			}
			set
			{
				this.messageSecurity = value;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00026ADC File Offset: 0x00024CDC
		internal SecurityBindingElement CreateMessageSecurity(bool isReliableSessionEnabled, MessageSecurityVersion version)
		{
			if (this.mode == WSFederationHttpSecurityMode.Message || this.mode == WSFederationHttpSecurityMode.TransportWithMessageCredential)
			{
				return this.messageSecurity.CreateSecurityBindingElement(this.Mode == WSFederationHttpSecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, version);
			}
			return null;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00026B08 File Offset: 0x00024D08
		internal static bool TryCreate(SecurityBindingElement sbe, WSFederationHttpSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSessionEnabled, MessageSecurityVersion version, out WSFederationHttpSecurity security)
		{
			security = null;
			FederatedMessageSecurityOverHttp federatedMessageSecurityOverHttp = null;
			if (sbe == null)
			{
				mode = WSFederationHttpSecurityMode.None;
			}
			else
			{
				mode &= (WSFederationHttpSecurityMode)3;
				if (!FederatedMessageSecurityOverHttp.TryCreate(sbe, mode == WSFederationHttpSecurityMode.TransportWithMessageCredential, isReliableSessionEnabled, version, out federatedMessageSecurityOverHttp))
				{
					return false;
				}
			}
			security = new WSFederationHttpSecurity(mode, federatedMessageSecurityOverHttp);
			return true;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00026B46 File Offset: 0x00024D46
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeMode() || this.ShouldSerializeMessage();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00026B58 File Offset: 0x00024D58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMode()
		{
			return this.Mode != WSFederationHttpSecurityMode.Message;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00026B66 File Offset: 0x00024D66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessage()
		{
			return this.Message.InternalShouldSerialize();
		}

		// Token: 0x04000BA5 RID: 2981
		internal const WSFederationHttpSecurityMode DefaultMode = WSFederationHttpSecurityMode.Message;

		// Token: 0x04000BA6 RID: 2982
		private WSFederationHttpSecurityMode mode;

		// Token: 0x04000BA7 RID: 2983
		private FederatedMessageSecurityOverHttp messageSecurity;
	}
}
