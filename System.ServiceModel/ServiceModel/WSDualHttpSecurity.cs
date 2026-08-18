using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000163 RID: 355
	public sealed class WSDualHttpSecurity
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00027FB0 File Offset: 0x000261B0
		public WSDualHttpSecurity() : this(WSDualHttpSecurityMode.Message, new MessageSecurityOverHttp())
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00027FBE File Offset: 0x000261BE
		private WSDualHttpSecurity(WSDualHttpSecurityMode mode, MessageSecurityOverHttp messageSecurity)
		{
			this.mode = mode;
			this.messageSecurity = ((messageSecurity == null) ? new MessageSecurityOverHttp() : messageSecurity);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00027FDE File Offset: 0x000261DE
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00027FE6 File Offset: 0x000261E6
		public WSDualHttpSecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!WSDualHttpSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002800C File Offset: 0x0002620C
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00028014 File Offset: 0x00026214
		public MessageSecurityOverHttp Message
		{
			get
			{
				return this.messageSecurity;
			}
			set
			{
				this.messageSecurity = ((value == null) ? new MessageSecurityOverHttp() : value);
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00028027 File Offset: 0x00026227
		internal SecurityBindingElement CreateMessageSecurity()
		{
			if (this.mode == WSDualHttpSecurityMode.Message)
			{
				return this.messageSecurity.CreateSecurityBindingElement(false, true, WSDualHttpSecurity.WSDualMessageSecurityVersion);
			}
			return null;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00028048 File Offset: 0x00026248
		internal static bool TryCreate(SecurityBindingElement sbe, out WSDualHttpSecurity security)
		{
			security = null;
			if (sbe == null)
			{
				security = new WSDualHttpSecurity(WSDualHttpSecurityMode.None, null);
			}
			else
			{
				MessageSecurityOverHttp messageSecurityOverHttp;
				if (!MessageSecurityOverHttp.TryCreate<MessageSecurityOverHttp>(sbe, false, true, out messageSecurityOverHttp))
				{
					return false;
				}
				security = new WSDualHttpSecurity(WSDualHttpSecurityMode.Message, messageSecurityOverHttp);
			}
			return SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(), sbe);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002808A File Offset: 0x0002628A
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeMode() || this.ShouldSerializeMessage();
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002809C File Offset: 0x0002629C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMode()
		{
			return this.Mode != WSDualHttpSecurityMode.Message;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000280AA File Offset: 0x000262AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessage()
		{
			return this.Message.InternalShouldSerialize();
		}

		// Token: 0x04000BC3 RID: 3011
		private static readonly MessageSecurityVersion WSDualMessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;

		// Token: 0x04000BC4 RID: 3012
		internal const WSDualHttpSecurityMode DefaultMode = WSDualHttpSecurityMode.Message;

		// Token: 0x04000BC5 RID: 3013
		private WSDualHttpSecurityMode mode;

		// Token: 0x04000BC6 RID: 3014
		private MessageSecurityOverHttp messageSecurity;
	}
}
