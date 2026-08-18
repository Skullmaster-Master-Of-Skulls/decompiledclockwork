using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000977 RID: 2423
	internal sealed class WsrmAcknowledgmentInfo : WsrmHeaderInfo
	{
		// Token: 0x06005DDA RID: 24026 RVA: 0x0015AD68 File Offset: 0x00158F68
		private WsrmAcknowledgmentInfo(UniqueId sequenceID, SequenceRangeCollection ranges, bool final, int bufferRemaining, MessageHeaderInfo header) : base(header)
		{
			this.sequenceID = sequenceID;
			this.ranges = ranges;
			this.final = final;
			this.bufferRemaining = bufferRemaining;
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x0015AD8F File Offset: 0x00158F8F
		public int BufferRemaining
		{
			get
			{
				return this.bufferRemaining;
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0015AD97 File Offset: 0x00158F97
		public bool Final
		{
			get
			{
				return this.final;
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x0015AD9F File Offset: 0x00158F9F
		public SequenceRangeCollection Ranges
		{
			get
			{
				return this.ranges;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x0015ADA7 File Offset: 0x00158FA7
		public UniqueId SequenceID
		{
			get
			{
				return this.sequenceID;
			}
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x0015ADB0 File Offset: 0x00158FB0
		internal static void ReadAck(ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader, out UniqueId sequenceId, out SequenceRangeCollection rangeCollection, out bool final)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			reader.ReadStartElement(wsrmFeb2005Dictionary.SequenceAcknowledgement, @namespace);
			reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			sequenceId = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			bool allowZero = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			rangeCollection = SequenceRangeCollection.Empty;
			while (reader.IsStartElement(wsrmFeb2005Dictionary.AcknowledgementRange, @namespace))
			{
				reader.MoveToAttribute("Lower");
				long num = WsrmUtilities.ReadSequenceNumber(reader, allowZero);
				reader.MoveToAttribute("Upper");
				long num2 = WsrmUtilities.ReadSequenceNumber(reader, allowZero);
				if (num < 0L || num > num2 || (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && num == 0L && num2 > 0L) || (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && num == 0L))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidSequenceRange", new object[]
					{
						num,
						num2
					})));
				}
				rangeCollection = rangeCollection.MergeWith(new SequenceRange(num, num2));
				reader.MoveToElement();
				WsrmUtilities.ReadEmptyElement(reader);
			}
			bool flag = rangeCollection.Count > 0;
			final = false;
			bool flag2 = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			if (flag2)
			{
				Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
				if (reader.IsStartElement(wsrm11Dictionary.None, @namespace))
				{
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
						{
							reader.Name,
							reader.NodeType,
							wsrmFeb2005Dictionary.SequenceAcknowledgement
						})));
					}
					WsrmUtilities.ReadEmptyElement(reader);
					flag = true;
				}
				if (reader.IsStartElement(wsrm11Dictionary.Final, @namespace))
				{
					if (!flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
						{
							reader.Name,
							reader.NodeType,
							wsrmFeb2005Dictionary.SequenceAcknowledgement
						})));
					}
					WsrmUtilities.ReadEmptyElement(reader);
					final = true;
				}
			}
			bool flag3 = false;
			while (reader.IsStartElement(wsrmFeb2005Dictionary.Nack, @namespace))
			{
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
					{
						reader.Name,
						reader.NodeType,
						"Body"
					})));
				}
				reader.ReadStartElement();
				WsrmUtilities.ReadSequenceNumber(reader, true);
				reader.ReadEndElement();
				flag3 = true;
			}
			if (!flag && !flag3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
				{
					reader.Name,
					reader.NodeType,
					"Body"
				})));
			}
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x0015B054 File Offset: 0x00159254
		public static WsrmAcknowledgmentInfo ReadHeader(ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader, MessageHeaderInfo header)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			UniqueId uniqueId;
			SequenceRangeCollection sequenceRangeCollection;
			bool flag;
			WsrmAcknowledgmentInfo.ReadAck(reliableMessagingVersion, reader, out uniqueId, out sequenceRangeCollection, out flag);
			int num = -1;
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement(wsrmFeb2005Dictionary.BufferRemaining, XD.WsrmFeb2005Dictionary.NETNamespace))
				{
					if (num != -1)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
						{
							reader.Name,
							reader.NodeType,
							"Body"
						})));
					}
					reader.ReadStartElement();
					num = reader.ReadContentAsInt();
					reader.ReadEndElement();
					if (num < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidBufferRemaining", new object[]
						{
							num
						})));
					}
				}
				else
				{
					if (reader.IsStartElement(wsrmFeb2005Dictionary.AcknowledgementRange, @namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
						{
							reader.Name,
							reader.NodeType,
							"Body"
						})));
					}
					if (reader.IsStartElement(wsrmFeb2005Dictionary.Nack, @namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
						{
							reader.Name,
							reader.NodeType,
							"Body"
						})));
					}
					if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
					{
						Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
						if (reader.IsStartElement(wsrm11Dictionary.None, @namespace) || reader.IsStartElement(wsrm11Dictionary.Final, @namespace))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
							{
								reader.Name,
								reader.NodeType,
								wsrmFeb2005Dictionary.SequenceAcknowledgement
							})));
						}
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
			return new WsrmAcknowledgmentInfo(uniqueId, sequenceRangeCollection, flag, num, header);
		}

		// Token: 0x040037B6 RID: 14262
		private int bufferRemaining;

		// Token: 0x040037B7 RID: 14263
		private bool final;

		// Token: 0x040037B8 RID: 14264
		private SequenceRangeCollection ranges;

		// Token: 0x040037B9 RID: 14265
		private UniqueId sequenceID;
	}
}
