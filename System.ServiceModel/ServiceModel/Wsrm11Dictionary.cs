using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000043 RID: 67
	internal class Wsrm11Dictionary
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000AB28 File Offset: 0x00008D28
		public Wsrm11Dictionary(XmlDictionary dictionary)
		{
			this.AckRequestedAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/AckRequested");
			this.CloseSequence = dictionary.Add("CloseSequence");
			this.CloseSequenceAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequence");
			this.CloseSequenceResponse = dictionary.Add("CloseSequenceResponse");
			this.CloseSequenceResponseAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse");
			this.CreateSequenceAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequence");
			this.CreateSequenceResponseAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequenceResponse");
			this.DiscardFollowingFirstGap = dictionary.Add("DiscardFollowingFirstGap");
			this.Endpoint = dictionary.Add("Endpoint");
			this.FaultAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/fault");
			this.Final = dictionary.Add("Final");
			this.IncompleteSequenceBehavior = dictionary.Add("IncompleteSequenceBehavior");
			this.LastMsgNumber = dictionary.Add("LastMsgNumber");
			this.MaxMessageNumber = dictionary.Add("MaxMessageNumber");
			this.Namespace = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702");
			this.NoDiscard = dictionary.Add("NoDiscard");
			this.None = dictionary.Add("None");
			this.SequenceAcknowledgementAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/SequenceAcknowledgement");
			this.SequenceClosed = dictionary.Add("SequenceClosed");
			this.TerminateSequenceAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequence");
			this.TerminateSequenceResponse = dictionary.Add("TerminateSequenceResponse");
			this.TerminateSequenceResponseAction = dictionary.Add("http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse");
			this.UsesSequenceSSL = dictionary.Add("UsesSequenceSSL");
			this.UsesSequenceSTR = dictionary.Add("UsesSequenceSTR");
			this.WsrmRequired = dictionary.Add("WsrmRequired");
		}

		// Token: 0x040001EE RID: 494
		public XmlDictionaryString AckRequestedAction;

		// Token: 0x040001EF RID: 495
		public XmlDictionaryString CloseSequence;

		// Token: 0x040001F0 RID: 496
		public XmlDictionaryString CloseSequenceAction;

		// Token: 0x040001F1 RID: 497
		public XmlDictionaryString CloseSequenceResponse;

		// Token: 0x040001F2 RID: 498
		public XmlDictionaryString CloseSequenceResponseAction;

		// Token: 0x040001F3 RID: 499
		public XmlDictionaryString CreateSequenceAction;

		// Token: 0x040001F4 RID: 500
		public XmlDictionaryString CreateSequenceResponseAction;

		// Token: 0x040001F5 RID: 501
		public XmlDictionaryString DiscardFollowingFirstGap;

		// Token: 0x040001F6 RID: 502
		public XmlDictionaryString Endpoint;

		// Token: 0x040001F7 RID: 503
		public XmlDictionaryString FaultAction;

		// Token: 0x040001F8 RID: 504
		public XmlDictionaryString Final;

		// Token: 0x040001F9 RID: 505
		public XmlDictionaryString IncompleteSequenceBehavior;

		// Token: 0x040001FA RID: 506
		public XmlDictionaryString LastMsgNumber;

		// Token: 0x040001FB RID: 507
		public XmlDictionaryString MaxMessageNumber;

		// Token: 0x040001FC RID: 508
		public XmlDictionaryString Namespace;

		// Token: 0x040001FD RID: 509
		public XmlDictionaryString NoDiscard;

		// Token: 0x040001FE RID: 510
		public XmlDictionaryString None;

		// Token: 0x040001FF RID: 511
		public XmlDictionaryString SequenceAcknowledgementAction;

		// Token: 0x04000200 RID: 512
		public XmlDictionaryString SequenceClosed;

		// Token: 0x04000201 RID: 513
		public XmlDictionaryString TerminateSequenceAction;

		// Token: 0x04000202 RID: 514
		public XmlDictionaryString TerminateSequenceResponse;

		// Token: 0x04000203 RID: 515
		public XmlDictionaryString TerminateSequenceResponseAction;

		// Token: 0x04000204 RID: 516
		public XmlDictionaryString UsesSequenceSSL;

		// Token: 0x04000205 RID: 517
		public XmlDictionaryString UsesSequenceSTR;

		// Token: 0x04000206 RID: 518
		public XmlDictionaryString WsrmRequired;
	}
}
