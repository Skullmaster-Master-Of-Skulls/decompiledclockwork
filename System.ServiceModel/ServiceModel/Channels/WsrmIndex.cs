using System;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000947 RID: 2375
	internal abstract class WsrmIndex
	{
		// Token: 0x06005B4A RID: 23370 RVA: 0x0014EAE0 File Offset: 0x0014CCE0
		internal static ActionHeader GetAckRequestedActionHeader(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, reliableMessagingVersion, "AckRequested");
		}

		// Token: 0x06005B4B RID: 23371
		protected abstract ActionHeader GetActionHeader(string element);

		// Token: 0x06005B4C RID: 23372 RVA: 0x0014EAF0 File Offset: 0x0014CCF0
		private static ActionHeader GetActionHeader(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion, string element)
		{
			WsrmIndex wsrmIndex = null;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (addressingVersion == AddressingVersion.WSAddressingAugust2004)
				{
					if (WsrmIndex.wsAddressingAug2004WSReliableMessagingFeb2005 == null)
					{
						WsrmIndex.wsAddressingAug2004WSReliableMessagingFeb2005 = new WsrmFeb2005Index(addressingVersion);
					}
					wsrmIndex = WsrmIndex.wsAddressingAug2004WSReliableMessagingFeb2005;
				}
				else if (addressingVersion == AddressingVersion.WSAddressing10)
				{
					if (WsrmIndex.wsAddressing10WSReliableMessagingFeb2005 == null)
					{
						WsrmIndex.wsAddressing10WSReliableMessagingFeb2005 = new WsrmFeb2005Index(addressingVersion);
					}
					wsrmIndex = WsrmIndex.wsAddressing10WSReliableMessagingFeb2005;
				}
			}
			else
			{
				if (reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
				{
					throw Fx.AssertAndThrow("Reliable messaging version not supported.");
				}
				if (addressingVersion == AddressingVersion.WSAddressingAugust2004)
				{
					if (WsrmIndex.wsAddressingAug2004WSReliableMessaging11 == null)
					{
						WsrmIndex.wsAddressingAug2004WSReliableMessaging11 = new Wsrm11Index(addressingVersion);
					}
					wsrmIndex = WsrmIndex.wsAddressingAug2004WSReliableMessaging11;
				}
				else if (addressingVersion == AddressingVersion.WSAddressing10)
				{
					if (WsrmIndex.wsAddressing10WSReliableMessaging11 == null)
					{
						WsrmIndex.wsAddressing10WSReliableMessaging11 = new Wsrm11Index(addressingVersion);
					}
					wsrmIndex = WsrmIndex.wsAddressing10WSReliableMessaging11;
				}
			}
			if (wsrmIndex == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
				{
					addressingVersion
				})));
			}
			return wsrmIndex.GetActionHeader(element);
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x0014EBD0 File Offset: 0x0014CDD0
		internal static ActionHeader GetCloseSequenceActionHeader(AddressingVersion addressingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, ReliableMessagingVersion.WSReliableMessaging11, "CloseSequence");
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0014EBE2 File Offset: 0x0014CDE2
		internal static ActionHeader GetCloseSequenceResponseActionHeader(AddressingVersion addressingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, ReliableMessagingVersion.WSReliableMessaging11, "CloseSequenceResponse");
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x0014EBF4 File Offset: 0x0014CDF4
		internal static ActionHeader GetCreateSequenceActionHeader(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, reliableMessagingVersion, "CreateSequence");
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0014EC02 File Offset: 0x0014CE02
		internal static string GetCreateSequenceActionString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequence";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x0014EC2A File Offset: 0x0014CE2A
		internal static XmlDictionaryString GetCreateSequenceResponseAction(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return XD.WsrmFeb2005Dictionary.CreateSequenceResponseAction;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return DXD.Wsrm11Dictionary.CreateSequenceResponseAction;
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x0014EC5C File Offset: 0x0014CE5C
		internal static string GetCreateSequenceResponseActionString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/CreateSequenceResponse";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x0014EC84 File Offset: 0x0014CE84
		internal static string GetFaultActionString(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return addressingVersion.DefaultFaultAction;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/fault";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x0014ECAD File Offset: 0x0014CEAD
		internal static XmlDictionaryString GetNamespace(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return XD.WsrmFeb2005Dictionary.Namespace;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return DXD.Wsrm11Dictionary.Namespace;
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0014ECDF File Offset: 0x0014CEDF
		internal static string GetNamespaceString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/rm";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x0014ED07 File Offset: 0x0014CF07
		internal static ActionHeader GetSequenceAcknowledgementActionHeader(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, reliableMessagingVersion, "SequenceAcknowledgement");
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x0014ED15 File Offset: 0x0014CF15
		internal static string GetSequenceAcknowledgementActionString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/SequenceAcknowledgement";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x0014ED3D File Offset: 0x0014CF3D
		internal static MessagePartSpecification GetSignedReliabilityMessageParts(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return WsrmFeb2005Index.SignedReliabilityMessageParts;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return Wsrm11Index.SignedReliabilityMessageParts;
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0014ED65 File Offset: 0x0014CF65
		internal static ActionHeader GetTerminateSequenceActionHeader(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, reliableMessagingVersion, "TerminateSequence");
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0014ED73 File Offset: 0x0014CF73
		internal static string GetTerminateSequenceActionString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence";
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequence";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0014ED9B File Offset: 0x0014CF9B
		internal static string GetTerminateSequenceResponseActionString(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				return "http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse";
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x0014EDB5 File Offset: 0x0014CFB5
		internal static ActionHeader GetTerminateSequenceResponseActionHeader(AddressingVersion addressingVersion)
		{
			return WsrmIndex.GetActionHeader(addressingVersion, ReliableMessagingVersion.WSReliableMessaging11, "TerminateSequenceResponse");
		}

		// Token: 0x040036E9 RID: 14057
		private static WsrmFeb2005Index wsAddressingAug2004WSReliableMessagingFeb2005;

		// Token: 0x040036EA RID: 14058
		private static WsrmFeb2005Index wsAddressing10WSReliableMessagingFeb2005;

		// Token: 0x040036EB RID: 14059
		private static Wsrm11Index wsAddressingAug2004WSReliableMessaging11;

		// Token: 0x040036EC RID: 14060
		private static Wsrm11Index wsAddressing10WSReliableMessaging11;
	}
}
