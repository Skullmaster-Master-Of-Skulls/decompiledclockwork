using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000146 RID: 326
	public sealed class NetMsmqSecurity
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00024B20 File Offset: 0x00022D20
		public NetMsmqSecurity() : this(NetMsmqSecurityMode.Transport, null, null)
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00024B2B File Offset: 0x00022D2B
		internal NetMsmqSecurity(NetMsmqSecurityMode mode) : this(mode, null, null)
		{
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00024B36 File Offset: 0x00022D36
		private NetMsmqSecurity(NetMsmqSecurityMode mode, MsmqTransportSecurity transportSecurity, MessageSecurityOverMsmq messageSecurity)
		{
			this.mode = mode;
			this.transportSecurity = ((transportSecurity == null) ? new MsmqTransportSecurity() : transportSecurity);
			this.messageSecurity = ((messageSecurity == null) ? new MessageSecurityOverMsmq() : messageSecurity);
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00024B67 File Offset: 0x00022D67
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00024B6F File Offset: 0x00022D6F
		[DefaultValue(NetMsmqSecurityMode.Transport)]
		public NetMsmqSecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!NetMsmqSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00024B95 File Offset: 0x00022D95
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00024BB0 File Offset: 0x00022DB0
		public MsmqTransportSecurity Transport
		{
			get
			{
				if (this.transportSecurity == null)
				{
					this.transportSecurity = new MsmqTransportSecurity();
				}
				return this.transportSecurity;
			}
			set
			{
				this.transportSecurity = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00024BB9 File Offset: 0x00022DB9
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00024BD4 File Offset: 0x00022DD4
		public MessageSecurityOverMsmq Message
		{
			get
			{
				if (this.messageSecurity == null)
				{
					this.messageSecurity = new MessageSecurityOverMsmq();
				}
				return this.messageSecurity;
			}
			set
			{
				this.messageSecurity = value;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00024BDD File Offset: 0x00022DDD
		internal void ConfigureTransportSecurity(System.ServiceModel.Channels.MsmqBindingElementBase msmq)
		{
			if (this.mode == NetMsmqSecurityMode.Transport || this.mode == NetMsmqSecurityMode.Both)
			{
				msmq.MsmqTransportSecurity = this.Transport;
				return;
			}
			msmq.MsmqTransportSecurity.Disable();
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00024C09 File Offset: 0x00022E09
		internal static bool IsConfiguredTransportSecurity(MsmqTransportBindingElement msmq, out UnifiedSecurityMode mode)
		{
			if (msmq == null)
			{
				mode = UnifiedSecurityMode.None;
				return false;
			}
			if (msmq.MsmqTransportSecurity.Enabled)
			{
				mode = (UnifiedSecurityMode.Transport | UnifiedSecurityMode.Both);
			}
			else
			{
				mode = (UnifiedSecurityMode.None | UnifiedSecurityMode.Message);
			}
			return true;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00024C2B File Offset: 0x00022E2B
		internal SecurityBindingElement CreateMessageSecurity()
		{
			return this.Message.CreateSecurityBindingElement();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00024C38 File Offset: 0x00022E38
		internal static bool TryCreate(SecurityBindingElement sbe, NetMsmqSecurityMode mode, out NetMsmqSecurity security)
		{
			security = null;
			MessageSecurityOverMsmq messageSecurityOverMsmq;
			if (!MessageSecurityOverMsmq.TryCreate(sbe, out messageSecurityOverMsmq))
			{
				messageSecurityOverMsmq = null;
			}
			security = new NetMsmqSecurity(mode, null, messageSecurityOverMsmq);
			return sbe == null || SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(), sbe, false);
		}

		// Token: 0x04000B67 RID: 2919
		internal const NetMsmqSecurityMode DefaultMode = NetMsmqSecurityMode.Transport;

		// Token: 0x04000B68 RID: 2920
		private NetMsmqSecurityMode mode;

		// Token: 0x04000B69 RID: 2921
		private MsmqTransportSecurity transportSecurity;

		// Token: 0x04000B6A RID: 2922
		private MessageSecurityOverMsmq messageSecurity;
	}
}
