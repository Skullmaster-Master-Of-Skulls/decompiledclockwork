using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000963 RID: 2403
	internal abstract class WsrmHeaderFault : WsrmFault
	{
		// Token: 0x06005D4D RID: 23885 RVA: 0x00158F6C File Offset: 0x0015716C
		protected WsrmHeaderFault(bool isSenderFault, string subcode, string faultReason, string exceptionMessage, UniqueId sequenceID, bool faultsInput, bool faultsOutput) : base(isSenderFault, subcode, faultReason, exceptionMessage)
		{
			this.subcode = subcode;
			this.sequenceID = sequenceID;
			this.faultsInput = faultsInput;
			this.faultsOutput = faultsOutput;
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00158F98 File Offset: 0x00157198
		protected WsrmHeaderFault(FaultCode code, string subcode, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion, bool faultsInput, bool faultsOutput) : this(code, subcode, reason, faultsInput, faultsOutput)
		{
			this.sequenceID = WsrmHeaderFault.ParseDetail(detailReader, reliableMessagingVersion);
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00158FB6 File Offset: 0x001571B6
		protected WsrmHeaderFault(FaultCode code, string subcode, FaultReason reason, bool faultsInput, bool faultsOutput) : base(code, subcode, reason)
		{
			this.subcode = subcode;
			this.faultsInput = faultsInput;
			this.faultsOutput = faultsOutput;
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x00158FD8 File Offset: 0x001571D8
		public bool FaultsInput
		{
			get
			{
				return this.faultsInput;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06005D51 RID: 23889 RVA: 0x00158FE0 File Offset: 0x001571E0
		public bool FaultsOutput
		{
			get
			{
				return this.faultsOutput;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06005D52 RID: 23890 RVA: 0x00158FE8 File Offset: 0x001571E8
		// (set) Token: 0x06005D53 RID: 23891 RVA: 0x00158FF0 File Offset: 0x001571F0
		public UniqueId SequenceID
		{
			get
			{
				return this.sequenceID;
			}
			protected set
			{
				this.sequenceID = value;
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x00158FFC File Offset: 0x001571FC
		private static WsrmHeaderFault CreateWsrmHeaderFault(ReliableMessagingVersion reliableMessagingVersion, FaultCode code, string subcode, FaultReason reason, XmlDictionaryReader detailReader)
		{
			if (code.IsSenderFault)
			{
				if (subcode == "InvalidAcknowledgement")
				{
					return new InvalidAcknowledgementFault(code, reason, detailReader, reliableMessagingVersion);
				}
				if (subcode == "MessageNumberRollover")
				{
					return new MessageNumberRolloverFault(code, reason, detailReader, reliableMessagingVersion);
				}
				if (subcode == "UnknownSequence")
				{
					return new UnknownSequenceFault(code, reason, detailReader, reliableMessagingVersion);
				}
				if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
				{
					if (subcode == "LastMessageNumberExceeded")
					{
						return new LastMessageNumberExceededFault(code, reason, detailReader, reliableMessagingVersion);
					}
				}
				else if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && subcode == "SequenceClosed")
				{
					return new SequenceClosedFault(code, reason, detailReader, reliableMessagingVersion);
				}
			}
			if (code.IsSenderFault || code.IsReceiverFault)
			{
				return new SequenceTerminatedFault(code, reason, detailReader, reliableMessagingVersion);
			}
			return null;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x001590B8 File Offset: 0x001572B8
		protected override FaultCode Get11Code(FaultCode code, string subcode)
		{
			return code;
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x001590BB File Offset: 0x001572BB
		protected override bool Get12HasDetail()
		{
			return true;
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x001590C0 File Offset: 0x001572C0
		private static void LookupDetailInformation(ReliableMessagingVersion reliableMessagingVersion, string subcode, out string detailName, out string detailNamespace)
		{
			detailName = null;
			detailNamespace = null;
			string namespaceString = WsrmIndex.GetNamespaceString(reliableMessagingVersion);
			bool flag = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			bool flag2 = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			if (subcode == "InvalidAcknowledgement")
			{
				detailName = "SequenceAcknowledgement";
				detailNamespace = namespaceString;
				return;
			}
			if (subcode == "MessageNumberRollover" || subcode == "SequenceTerminated" || subcode == "UnknownSequence" || (flag && subcode == "LastMessageNumberExceeded") || (flag2 && subcode == "SequenceClosed"))
			{
				detailName = "Identifier";
				detailNamespace = namespaceString;
				return;
			}
			detailName = null;
			detailNamespace = null;
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0015915C File Offset: 0x0015735C
		protected override void OnFaultMessageCreated(MessageVersion version, Message message)
		{
			if (version.Envelope == EnvelopeVersion.Soap11)
			{
				WsrmSequenceFaultHeader header = new WsrmSequenceFaultHeader(base.GetReliableMessagingVersion(), this);
				message.Headers.Add(header);
			}
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0015918F File Offset: 0x0015738F
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			WsrmUtilities.WriteIdentifier(writer, base.GetReliableMessagingVersion(), this.sequenceID);
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001591A4 File Offset: 0x001573A4
		private static UniqueId ParseDetail(XmlDictionaryReader reader, ReliableMessagingVersion reliableMessagingVersion)
		{
			UniqueId result;
			try
			{
				result = WsrmUtilities.ReadIdentifier(reader, reliableMessagingVersion);
			}
			finally
			{
				reader.Close();
			}
			return result;
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x001591D4 File Offset: 0x001573D4
		public static bool TryCreateFault11(ReliableMessagingVersion reliableMessagingVersion, Message message, MessageFault fault, int index, out WsrmHeaderFault wsrmFault)
		{
			if (index == -1)
			{
				wsrmFault = null;
				return false;
			}
			if (!fault.Code.IsSenderFault && !fault.Code.IsReceiverFault)
			{
				wsrmFault = null;
				return false;
			}
			string text = WsrmSequenceFaultHeader.GetSubcode(message.Headers.GetReaderAtHeader(index), reliableMessagingVersion);
			if (text == null)
			{
				wsrmFault = null;
				return false;
			}
			string detailName;
			string detailNamespace;
			WsrmHeaderFault.LookupDetailInformation(reliableMessagingVersion, text, out detailName, out detailNamespace);
			XmlDictionaryReader readerAtDetailContents = WsrmSequenceFaultHeader.GetReaderAtDetailContents(detailName, detailNamespace, message.Headers.GetReaderAtHeader(index), reliableMessagingVersion);
			if (readerAtDetailContents == null)
			{
				wsrmFault = null;
				return false;
			}
			wsrmFault = WsrmHeaderFault.CreateWsrmHeaderFault(reliableMessagingVersion, fault.Code, text, fault.Reason, readerAtDetailContents);
			if (wsrmFault != null)
			{
				message.Headers.UnderstoodHeaders.Add(message.Headers[index]);
				return true;
			}
			return false;
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0015928C File Offset: 0x0015748C
		public static bool TryCreateFault12(ReliableMessagingVersion reliableMessagingVersion, Message message, MessageFault fault, out WsrmHeaderFault wsrmFault)
		{
			if (!fault.Code.IsSenderFault && !fault.Code.IsReceiverFault)
			{
				wsrmFault = null;
				return false;
			}
			if (fault.Code.SubCode == null || fault.Code.SubCode.Namespace != WsrmIndex.GetNamespaceString(reliableMessagingVersion) || !fault.HasDetail)
			{
				wsrmFault = null;
				return false;
			}
			string name = fault.Code.SubCode.Name;
			XmlDictionaryReader readerAtDetailContents = fault.GetReaderAtDetailContents();
			wsrmFault = WsrmHeaderFault.CreateWsrmHeaderFault(reliableMessagingVersion, fault.Code, name, fault.Reason, readerAtDetailContents);
			return wsrmFault != null;
		}

		// Token: 0x04003785 RID: 14213
		private bool faultsInput;

		// Token: 0x04003786 RID: 14214
		private bool faultsOutput;

		// Token: 0x04003787 RID: 14215
		private UniqueId sequenceID;

		// Token: 0x04003788 RID: 14216
		private string subcode;
	}
}
