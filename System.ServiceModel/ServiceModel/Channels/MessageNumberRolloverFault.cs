using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000966 RID: 2406
	internal sealed class MessageNumberRolloverFault : WsrmHeaderFault
	{
		// Token: 0x06005D62 RID: 23906 RVA: 0x0015941C File Offset: 0x0015761C
		public MessageNumberRolloverFault(UniqueId sequenceID) : base(true, "MessageNumberRollover", SR.GetString("MessageNumberRolloverFaultReason"), SR.GetString("MessageNumberRollover"), sequenceID, true, true)
		{
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00159444 File Offset: 0x00157644
		public MessageNumberRolloverFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "MessageNumberRollover", reason, true, true)
		{
			try
			{
				base.SequenceID = WsrmUtilities.ReadIdentifier(detailReader, reliableMessagingVersion);
				if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
				{
					detailReader.ReadStartElement(DXD.Wsrm11Dictionary.MaxMessageNumber, WsrmIndex.GetNamespace(reliableMessagingVersion));
					string s = detailReader.ReadContentAsString();
					ulong num;
					if (!ulong.TryParse(s, out num) || num <= 0UL)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidSequenceNumber", new object[]
						{
							num
						})));
					}
					detailReader.ReadEndElement();
				}
			}
			finally
			{
				detailReader.Close();
			}
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001594EC File Offset: 0x001576EC
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			ReliableMessagingVersion reliableMessagingVersion = base.GetReliableMessagingVersion();
			WsrmUtilities.WriteIdentifier(writer, reliableMessagingVersion, base.SequenceID);
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				writer.WriteStartElement("r", DXD.Wsrm11Dictionary.MaxMessageNumber, WsrmIndex.GetNamespace(reliableMessagingVersion));
				writer.WriteValue(long.MaxValue);
				writer.WriteEndElement();
			}
		}
	}
}
