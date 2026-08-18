using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000075 RID: 117
	internal class WsrmFeb2005Dictionary
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		public WsrmFeb2005Dictionary(ServiceModelDictionary dictionary)
		{
			this.Identifier = dictionary.CreateString("Identifier", 15);
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm", 16);
			this.SequenceAcknowledgement = dictionary.CreateString("SequenceAcknowledgement", 23);
			this.AcknowledgementRange = dictionary.CreateString("AcknowledgementRange", 24);
			this.Upper = dictionary.CreateString("Upper", 25);
			this.Lower = dictionary.CreateString("Lower", 26);
			this.BufferRemaining = dictionary.CreateString("BufferRemaining", 27);
			this.NETNamespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2006/05/rm", 28);
			this.SequenceAcknowledgementAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement", 29);
			this.Sequence = dictionary.CreateString("Sequence", 31);
			this.MessageNumber = dictionary.CreateString("MessageNumber", 32);
			this.AckRequested = dictionary.CreateString("AckRequested", 328);
			this.AckRequestedAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested", 329);
			this.AcksTo = dictionary.CreateString("AcksTo", 330);
			this.Accept = dictionary.CreateString("Accept", 331);
			this.CreateSequence = dictionary.CreateString("CreateSequence", 332);
			this.CreateSequenceAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence", 333);
			this.CreateSequenceRefused = dictionary.CreateString("CreateSequenceRefused", 334);
			this.CreateSequenceResponse = dictionary.CreateString("CreateSequenceResponse", 335);
			this.CreateSequenceResponseAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse", 336);
			this.Expires = dictionary.CreateString("Expires", 55);
			this.FaultCode = dictionary.CreateString("FaultCode", 337);
			this.InvalidAcknowledgement = dictionary.CreateString("InvalidAcknowledgement", 338);
			this.LastMessage = dictionary.CreateString("LastMessage", 339);
			this.LastMessageAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage", 340);
			this.LastMessageNumberExceeded = dictionary.CreateString("LastMessageNumberExceeded", 341);
			this.MessageNumberRollover = dictionary.CreateString("MessageNumberRollover", 342);
			this.Nack = dictionary.CreateString("Nack", 343);
			this.NETPrefix = dictionary.CreateString("netrm", 344);
			this.Offer = dictionary.CreateString("Offer", 345);
			this.Prefix = dictionary.CreateString("r", 346);
			this.SequenceFault = dictionary.CreateString("SequenceFault", 347);
			this.SequenceTerminated = dictionary.CreateString("SequenceTerminated", 348);
			this.TerminateSequence = dictionary.CreateString("TerminateSequence", 349);
			this.TerminateSequenceAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence", 350);
			this.UnknownSequence = dictionary.CreateString("UnknownSequence", 351);
			this.ConnectionLimitReached = dictionary.CreateString("ConnectionLimitReached", 480);
		}

		// Token: 0x04000663 RID: 1635
		public XmlDictionaryString Identifier;

		// Token: 0x04000664 RID: 1636
		public XmlDictionaryString Namespace;

		// Token: 0x04000665 RID: 1637
		public XmlDictionaryString SequenceAcknowledgement;

		// Token: 0x04000666 RID: 1638
		public XmlDictionaryString AcknowledgementRange;

		// Token: 0x04000667 RID: 1639
		public XmlDictionaryString Upper;

		// Token: 0x04000668 RID: 1640
		public XmlDictionaryString Lower;

		// Token: 0x04000669 RID: 1641
		public XmlDictionaryString BufferRemaining;

		// Token: 0x0400066A RID: 1642
		public XmlDictionaryString NETNamespace;

		// Token: 0x0400066B RID: 1643
		public XmlDictionaryString SequenceAcknowledgementAction;

		// Token: 0x0400066C RID: 1644
		public XmlDictionaryString Sequence;

		// Token: 0x0400066D RID: 1645
		public XmlDictionaryString MessageNumber;

		// Token: 0x0400066E RID: 1646
		public XmlDictionaryString AckRequested;

		// Token: 0x0400066F RID: 1647
		public XmlDictionaryString AckRequestedAction;

		// Token: 0x04000670 RID: 1648
		public XmlDictionaryString AcksTo;

		// Token: 0x04000671 RID: 1649
		public XmlDictionaryString Accept;

		// Token: 0x04000672 RID: 1650
		public XmlDictionaryString CreateSequence;

		// Token: 0x04000673 RID: 1651
		public XmlDictionaryString CreateSequenceAction;

		// Token: 0x04000674 RID: 1652
		public XmlDictionaryString CreateSequenceRefused;

		// Token: 0x04000675 RID: 1653
		public XmlDictionaryString CreateSequenceResponse;

		// Token: 0x04000676 RID: 1654
		public XmlDictionaryString CreateSequenceResponseAction;

		// Token: 0x04000677 RID: 1655
		public XmlDictionaryString Expires;

		// Token: 0x04000678 RID: 1656
		public XmlDictionaryString FaultCode;

		// Token: 0x04000679 RID: 1657
		public XmlDictionaryString InvalidAcknowledgement;

		// Token: 0x0400067A RID: 1658
		public XmlDictionaryString LastMessage;

		// Token: 0x0400067B RID: 1659
		public XmlDictionaryString LastMessageAction;

		// Token: 0x0400067C RID: 1660
		public XmlDictionaryString LastMessageNumberExceeded;

		// Token: 0x0400067D RID: 1661
		public XmlDictionaryString MessageNumberRollover;

		// Token: 0x0400067E RID: 1662
		public XmlDictionaryString Nack;

		// Token: 0x0400067F RID: 1663
		public XmlDictionaryString NETPrefix;

		// Token: 0x04000680 RID: 1664
		public XmlDictionaryString Offer;

		// Token: 0x04000681 RID: 1665
		public XmlDictionaryString Prefix;

		// Token: 0x04000682 RID: 1666
		public XmlDictionaryString SequenceFault;

		// Token: 0x04000683 RID: 1667
		public XmlDictionaryString SequenceTerminated;

		// Token: 0x04000684 RID: 1668
		public XmlDictionaryString TerminateSequence;

		// Token: 0x04000685 RID: 1669
		public XmlDictionaryString TerminateSequenceAction;

		// Token: 0x04000686 RID: 1670
		public XmlDictionaryString UnknownSequence;

		// Token: 0x04000687 RID: 1671
		public XmlDictionaryString ConnectionLimitReached;
	}
}
