using System;
using System.Net.Security;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A3 RID: 1955
	internal class CallbackContextMessageHeader : MessageHeader
	{
		// Token: 0x060049EF RID: 18927 RVA: 0x0010F650 File Offset: 0x0010D850
		public CallbackContextMessageHeader(EndpointAddress callbackAddress, AddressingVersion version)
		{
			if (callbackAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackAddress");
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			if (version != AddressingVersion.WSAddressing10)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CallbackContextOnlySupportedInWSAddressing10", new object[]
				{
					version
				})));
			}
			this.callbackAddress = callbackAddress;
			this.version = version;
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x0010F6C9 File Offset: 0x0010D8C9
		public override string Name
		{
			get
			{
				return "CallbackContext";
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x0010F6D0 File Offset: 0x0010D8D0
		public override string Namespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2008/02/context";
			}
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0010F6D8 File Offset: 0x0010D8D8
		internal static ChannelProtectionRequirements GetChannelProtectionRequirements(ProtectionLevel protectionLevel)
		{
			ChannelProtectionRequirements result;
			if (protectionLevel == ProtectionLevel.EncryptAndSign)
			{
				if (CallbackContextMessageHeader.encryptAndSignChannelProtectionRequirements == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.HeaderTypes.Add(new XmlQualifiedName("CallbackContext", "http://schemas.microsoft.com/ws/2008/02/context"));
					ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
					channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.MakeReadOnly();
					CallbackContextMessageHeader.encryptAndSignChannelProtectionRequirements = channelProtectionRequirements;
				}
				result = CallbackContextMessageHeader.encryptAndSignChannelProtectionRequirements;
			}
			else
			{
				if (protectionLevel != ProtectionLevel.Sign)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("protectionLevel"));
				}
				if (CallbackContextMessageHeader.signChannelProtectionRequirements == null)
				{
					MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
					messagePartSpecification2.HeaderTypes.Add(new XmlQualifiedName("CallbackContext", "http://schemas.microsoft.com/ws/2008/02/context"));
					ChannelProtectionRequirements channelProtectionRequirements2 = new ChannelProtectionRequirements();
					channelProtectionRequirements2.IncomingSignatureParts.AddParts(messagePartSpecification2);
					channelProtectionRequirements2.OutgoingSignatureParts.AddParts(messagePartSpecification2);
					channelProtectionRequirements2.MakeReadOnly();
					CallbackContextMessageHeader.signChannelProtectionRequirements = channelProtectionRequirements2;
				}
				result = CallbackContextMessageHeader.signChannelProtectionRequirements;
			}
			return result;
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0010F7D4 File Offset: 0x0010D9D4
		internal static CallbackContextMessageProperty ParseCallbackContextHeader(XmlReader reader, AddressingVersion version)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (version != AddressingVersion.WSAddressing10)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("CallbackContextOnlySupportedInWSAddressing10", new object[]
				{
					version
				})));
			}
			CallbackContextMessageProperty result;
			try
			{
				reader.ReadStartElement("CallbackContext", "http://schemas.microsoft.com/ws/2008/02/context");
				EndpointAddress endpointAddress = EndpointAddress.ReadFrom(version, reader, "CallbackEndpointReference", "http://schemas.microsoft.com/ws/2008/02/context");
				reader.ReadEndElement();
				result = new CallbackContextMessageProperty(endpointAddress);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("XmlFormatViolationInCallbackContextHeader"), innerException));
			}
			return result;
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0010F880 File Offset: 0x0010DA80
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			this.callbackAddress.WriteTo(this.version, writer, "CallbackEndpointReference", "http://schemas.microsoft.com/ws/2008/02/context");
		}

		// Token: 0x04002EE1 RID: 12001
		public const string CallbackContextHeaderName = "CallbackContext";

		// Token: 0x04002EE2 RID: 12002
		public const string CallbackContextHeaderNamespace = "http://schemas.microsoft.com/ws/2008/02/context";

		// Token: 0x04002EE3 RID: 12003
		public const string CallbackEndpointReference = "CallbackEndpointReference";

		// Token: 0x04002EE4 RID: 12004
		private static ChannelProtectionRequirements encryptAndSignChannelProtectionRequirements;

		// Token: 0x04002EE5 RID: 12005
		private static ChannelProtectionRequirements signChannelProtectionRequirements;

		// Token: 0x04002EE6 RID: 12006
		private EndpointAddress callbackAddress;

		// Token: 0x04002EE7 RID: 12007
		private AddressingVersion version;
	}
}
