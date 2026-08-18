using System;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000948 RID: 2376
	internal class Wsrm11Index : WsrmIndex
	{
		// Token: 0x06005B5E RID: 23390 RVA: 0x0014EDCF File Offset: 0x0014CFCF
		internal Wsrm11Index(AddressingVersion addressingVersion)
		{
			this.addressingVersion = addressingVersion;
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06005B5F RID: 23391 RVA: 0x0014EDE0 File Offset: 0x0014CFE0
		internal static MessagePartSpecification SignedReliabilityMessageParts
		{
			get
			{
				if (Wsrm11Index.signedReliabilityMessageParts == null)
				{
					XmlQualifiedName[] headerTypes = new XmlQualifiedName[]
					{
						new XmlQualifiedName("Sequence", "http://docs.oasis-open.org/ws-rx/wsrm/200702"),
						new XmlQualifiedName("SequenceAcknowledgement", "http://docs.oasis-open.org/ws-rx/wsrm/200702"),
						new XmlQualifiedName("AckRequested", "http://docs.oasis-open.org/ws-rx/wsrm/200702"),
						new XmlQualifiedName("UsesSequenceSTR", "http://docs.oasis-open.org/ws-rx/wsrm/200702")
					};
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(headerTypes);
					messagePartSpecification.MakeReadOnly();
					Wsrm11Index.signedReliabilityMessageParts = messagePartSpecification;
				}
				return Wsrm11Index.signedReliabilityMessageParts;
			}
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0014EE5C File Offset: 0x0014D05C
		protected override ActionHeader GetActionHeader(string element)
		{
			Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
			if (element == "AckRequested")
			{
				if (this.ackRequestedActionHeader == null)
				{
					this.ackRequestedActionHeader = ActionHeader.Create(wsrm11Dictionary.AckRequestedAction, this.addressingVersion);
				}
				return this.ackRequestedActionHeader;
			}
			if (element == "CreateSequence")
			{
				if (this.createSequenceActionHeader == null)
				{
					this.createSequenceActionHeader = ActionHeader.Create(wsrm11Dictionary.CreateSequenceAction, this.addressingVersion);
				}
				return this.createSequenceActionHeader;
			}
			if (element == "SequenceAcknowledgement")
			{
				if (this.sequenceAcknowledgementActionHeader == null)
				{
					this.sequenceAcknowledgementActionHeader = ActionHeader.Create(wsrm11Dictionary.SequenceAcknowledgementAction, this.addressingVersion);
				}
				return this.sequenceAcknowledgementActionHeader;
			}
			if (element == "TerminateSequence")
			{
				if (this.terminateSequenceActionHeader == null)
				{
					this.terminateSequenceActionHeader = ActionHeader.Create(wsrm11Dictionary.TerminateSequenceAction, this.addressingVersion);
				}
				return this.terminateSequenceActionHeader;
			}
			if (element == "TerminateSequenceResponse")
			{
				if (this.terminateSequenceResponseActionHeader == null)
				{
					this.terminateSequenceResponseActionHeader = ActionHeader.Create(wsrm11Dictionary.TerminateSequenceResponseAction, this.addressingVersion);
				}
				return this.terminateSequenceResponseActionHeader;
			}
			if (element == "CloseSequence")
			{
				if (this.closeSequenceActionHeader == null)
				{
					this.closeSequenceActionHeader = ActionHeader.Create(wsrm11Dictionary.CloseSequenceAction, this.addressingVersion);
				}
				return this.closeSequenceActionHeader;
			}
			if (element == "CloseSequenceResponse")
			{
				if (this.closeSequenceResponseActionHeader == null)
				{
					this.closeSequenceResponseActionHeader = ActionHeader.Create(wsrm11Dictionary.CloseSequenceResponseAction, this.addressingVersion);
				}
				return this.closeSequenceResponseActionHeader;
			}
			throw Fx.AssertAndThrow("Element not supported.");
		}

		// Token: 0x040036ED RID: 14061
		private static MessagePartSpecification signedReliabilityMessageParts;

		// Token: 0x040036EE RID: 14062
		private ActionHeader ackRequestedActionHeader;

		// Token: 0x040036EF RID: 14063
		private AddressingVersion addressingVersion;

		// Token: 0x040036F0 RID: 14064
		private ActionHeader closeSequenceActionHeader;

		// Token: 0x040036F1 RID: 14065
		private ActionHeader closeSequenceResponseActionHeader;

		// Token: 0x040036F2 RID: 14066
		private ActionHeader createSequenceActionHeader;

		// Token: 0x040036F3 RID: 14067
		private ActionHeader sequenceAcknowledgementActionHeader;

		// Token: 0x040036F4 RID: 14068
		private ActionHeader terminateSequenceActionHeader;

		// Token: 0x040036F5 RID: 14069
		private ActionHeader terminateSequenceResponseActionHeader;
	}
}
