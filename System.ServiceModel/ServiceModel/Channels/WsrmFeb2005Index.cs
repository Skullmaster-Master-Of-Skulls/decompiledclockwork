using System;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000949 RID: 2377
	internal class WsrmFeb2005Index : WsrmIndex
	{
		// Token: 0x06005B61 RID: 23393 RVA: 0x0014EFDE File Offset: 0x0014D1DE
		internal WsrmFeb2005Index(AddressingVersion addressingVersion)
		{
			this.addressingVersion = addressingVersion;
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0014EFF0 File Offset: 0x0014D1F0
		internal static MessagePartSpecification SignedReliabilityMessageParts
		{
			get
			{
				if (WsrmFeb2005Index.signedReliabilityMessageParts == null)
				{
					XmlQualifiedName[] headerTypes = new XmlQualifiedName[]
					{
						new XmlQualifiedName("Sequence", "http://schemas.xmlsoap.org/ws/2005/02/rm"),
						new XmlQualifiedName("SequenceAcknowledgement", "http://schemas.xmlsoap.org/ws/2005/02/rm"),
						new XmlQualifiedName("AckRequested", "http://schemas.xmlsoap.org/ws/2005/02/rm")
					};
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(headerTypes);
					messagePartSpecification.MakeReadOnly();
					WsrmFeb2005Index.signedReliabilityMessageParts = messagePartSpecification;
				}
				return WsrmFeb2005Index.signedReliabilityMessageParts;
			}
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0014F05C File Offset: 0x0014D25C
		protected override ActionHeader GetActionHeader(string element)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			if (element == "AckRequested")
			{
				if (this.ackRequestedActionHeader == null)
				{
					this.ackRequestedActionHeader = ActionHeader.Create(wsrmFeb2005Dictionary.AckRequestedAction, this.addressingVersion);
				}
				return this.ackRequestedActionHeader;
			}
			if (element == "CreateSequence")
			{
				if (this.createSequenceActionHeader == null)
				{
					this.createSequenceActionHeader = ActionHeader.Create(wsrmFeb2005Dictionary.CreateSequenceAction, this.addressingVersion);
				}
				return this.createSequenceActionHeader;
			}
			if (element == "SequenceAcknowledgement")
			{
				if (this.sequenceAcknowledgementActionHeader == null)
				{
					this.sequenceAcknowledgementActionHeader = ActionHeader.Create(wsrmFeb2005Dictionary.SequenceAcknowledgementAction, this.addressingVersion);
				}
				return this.sequenceAcknowledgementActionHeader;
			}
			if (element == "TerminateSequence")
			{
				if (this.terminateSequenceActionHeader == null)
				{
					this.terminateSequenceActionHeader = ActionHeader.Create(wsrmFeb2005Dictionary.TerminateSequenceAction, this.addressingVersion);
				}
				return this.terminateSequenceActionHeader;
			}
			throw Fx.AssertAndThrow("Element not supported.");
		}

		// Token: 0x040036F6 RID: 14070
		private static MessagePartSpecification signedReliabilityMessageParts;

		// Token: 0x040036F7 RID: 14071
		private ActionHeader ackRequestedActionHeader;

		// Token: 0x040036F8 RID: 14072
		private AddressingVersion addressingVersion;

		// Token: 0x040036F9 RID: 14073
		private ActionHeader createSequenceActionHeader;

		// Token: 0x040036FA RID: 14074
		private ActionHeader sequenceAcknowledgementActionHeader;

		// Token: 0x040036FB RID: 14075
		private ActionHeader terminateSequenceActionHeader;
	}
}
