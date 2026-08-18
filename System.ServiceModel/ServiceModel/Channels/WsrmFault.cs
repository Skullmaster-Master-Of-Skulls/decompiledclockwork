using System;
using System.Globalization;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000961 RID: 2401
	internal abstract class WsrmFault : MessageFault
	{
		// Token: 0x06005D37 RID: 23863 RVA: 0x00158CEC File Offset: 0x00156EEC
		protected WsrmFault(bool isSenderFault, string subcode, string faultReason, string exceptionMessage)
		{
			if (isSenderFault)
			{
				this.code = new FaultCode("Sender", "");
			}
			else
			{
				this.code = new FaultCode("Receiver", "");
			}
			this.subcode = subcode;
			this.reason = new FaultReason(faultReason, CultureInfo.CurrentCulture);
			this.exceptionMessage = exceptionMessage;
			this.isRemote = false;
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x00158D55 File Offset: 0x00156F55
		protected WsrmFault(FaultCode code, string subcode, FaultReason reason)
		{
			this.code = code;
			this.subcode = subcode;
			this.reason = reason;
			this.isRemote = true;
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06005D39 RID: 23865 RVA: 0x00158D79 File Offset: 0x00156F79
		public override FaultCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06005D3A RID: 23866 RVA: 0x00158D81 File Offset: 0x00156F81
		public override bool HasDetail
		{
			get
			{
				return this.hasDetail;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06005D3B RID: 23867 RVA: 0x00158D89 File Offset: 0x00156F89
		public bool IsRemote
		{
			get
			{
				return this.isRemote;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x00158D91 File Offset: 0x00156F91
		public override FaultReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x00158D99 File Offset: 0x00156F99
		public string Subcode
		{
			get
			{
				return this.subcode;
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x00158DA4 File Offset: 0x00156FA4
		public virtual CommunicationException CreateException()
		{
			string text;
			if (this.IsRemote)
			{
				text = FaultException.GetSafeReasonText(this.reason);
				text = SR.GetString("WsrmFaultReceived", new object[]
				{
					text
				});
			}
			else
			{
				if (this.exceptionMessage == null)
				{
					throw Fx.AssertAndThrow("Exception message must not be accessed unless set.");
				}
				text = this.exceptionMessage;
			}
			if (this.code.IsSenderFault)
			{
				return new ProtocolException(text);
			}
			return new CommunicationException(text);
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00158E10 File Offset: 0x00157010
		public static CommunicationException CreateException(WsrmFault fault)
		{
			return fault.CreateException();
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x00158E18 File Offset: 0x00157018
		public Message CreateMessage(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			this.SetReliableMessagingVersion(reliableMessagingVersion);
			string faultActionString = WsrmIndex.GetFaultActionString(messageVersion.Addressing, reliableMessagingVersion);
			if (messageVersion.Envelope == EnvelopeVersion.Soap11)
			{
				this.code = this.Get11Code(this.code, this.subcode);
			}
			else
			{
				if (messageVersion.Envelope != EnvelopeVersion.Soap12)
				{
					throw Fx.AssertAndThrow("Unsupported MessageVersion.");
				}
				if (this.code.SubCode == null)
				{
					FaultCode subCode = new FaultCode(this.subcode, WsrmIndex.GetNamespaceString(reliableMessagingVersion));
					this.code = new FaultCode(this.code.Name, this.code.Namespace, subCode);
				}
				this.hasDetail = this.Get12HasDetail();
			}
			Message message = Message.CreateMessage(messageVersion, this, faultActionString);
			this.OnFaultMessageCreated(messageVersion, message);
			return message;
		}

		// Token: 0x06005D41 RID: 23873
		protected abstract FaultCode Get11Code(FaultCode code, string subcode);

		// Token: 0x06005D42 RID: 23874
		protected abstract bool Get12HasDetail();

		// Token: 0x06005D43 RID: 23875 RVA: 0x00158ED9 File Offset: 0x001570D9
		protected string GetExceptionMessage()
		{
			if (this.exceptionMessage == null)
			{
				throw Fx.AssertAndThrow("Exception message must not be accessed unless set.");
			}
			return this.exceptionMessage;
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x00158EF4 File Offset: 0x001570F4
		protected ReliableMessagingVersion GetReliableMessagingVersion()
		{
			if (this.reliableMessagingVersion == null)
			{
				throw Fx.AssertAndThrow("Reliable messaging version must not be accessed unless set.");
			}
			return this.reliableMessagingVersion;
		}

		// Token: 0x06005D45 RID: 23877
		protected abstract void OnFaultMessageCreated(MessageVersion version, Message message);

		// Token: 0x06005D46 RID: 23878 RVA: 0x00158F0F File Offset: 0x0015710F
		protected void SetReliableMessagingVersion(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == null)
			{
				throw Fx.AssertAndThrow("Reliable messaging version cannot be set to null.");
			}
			if (this.reliableMessagingVersion != null)
			{
				throw Fx.AssertAndThrow("Reliable messaging version must not be set twice.");
			}
			this.reliableMessagingVersion = reliableMessagingVersion;
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x00158F39 File Offset: 0x00157139
		internal void WriteDetail(XmlDictionaryWriter writer)
		{
			this.OnWriteDetailContents(writer);
		}

		// Token: 0x0400377E RID: 14206
		private FaultCode code;

		// Token: 0x0400377F RID: 14207
		private string exceptionMessage;

		// Token: 0x04003780 RID: 14208
		private bool hasDetail;

		// Token: 0x04003781 RID: 14209
		private bool isRemote;

		// Token: 0x04003782 RID: 14210
		private FaultReason reason;

		// Token: 0x04003783 RID: 14211
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x04003784 RID: 14212
		private string subcode;
	}
}
