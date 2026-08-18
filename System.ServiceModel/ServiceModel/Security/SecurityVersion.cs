using System;
using System.IdentityModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000284 RID: 644
	[__DynamicallyInvokable]
	public abstract class SecurityVersion
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x00043CAF File Offset: 0x00041EAF
		internal SecurityVersion(XmlDictionaryString headerName, XmlDictionaryString headerNamespace, XmlDictionaryString headerPrefix)
		{
			this.headerName = headerName;
			this.headerNamespace = headerNamespace;
			this.headerPrefix = headerPrefix;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00043CCC File Offset: 0x00041ECC
		internal XmlDictionaryString HeaderName
		{
			get
			{
				return this.headerName;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00043CD4 File Offset: 0x00041ED4
		internal XmlDictionaryString HeaderNamespace
		{
			get
			{
				return this.headerNamespace;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00043CDC File Offset: 0x00041EDC
		internal XmlDictionaryString HeaderPrefix
		{
			get
			{
				return this.headerPrefix;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001285 RID: 4741
		internal abstract XmlDictionaryString FailedAuthenticationFaultCode { get; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001286 RID: 4742
		internal abstract XmlDictionaryString InvalidSecurityTokenFaultCode { get; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001287 RID: 4743
		internal abstract XmlDictionaryString InvalidSecurityFaultCode { get; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00043CE4 File Offset: 0x00041EE4
		internal virtual bool SupportsSignatureConfirmation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00043CE7 File Offset: 0x00041EE7
		[__DynamicallyInvokable]
		public static SecurityVersion WSSecurity10
		{
			[__DynamicallyInvokable]
			get
			{
				return SecurityVersion.SecurityVersion10.Instance;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00043CEE File Offset: 0x00041EEE
		[__DynamicallyInvokable]
		public static SecurityVersion WSSecurity11
		{
			[__DynamicallyInvokable]
			get
			{
				return SecurityVersion.SecurityVersion11.Instance;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00043CF5 File Offset: 0x00041EF5
		internal static SecurityVersion Default
		{
			get
			{
				return SecurityVersion.WSSecurity11;
			}
		}

		// Token: 0x0600128C RID: 4748
		internal abstract ReceiveSecurityHeader CreateReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction, int headerIndex);

		// Token: 0x0600128D RID: 4749
		internal abstract SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction);

		// Token: 0x0600128E RID: 4750 RVA: 0x00043CFC File Offset: 0x00041EFC
		internal bool DoesMessageContainSecurityHeader(Message message)
		{
			return message.Headers.FindHeader(this.HeaderName.Value, this.HeaderNamespace.Value) >= 0;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00043D25 File Offset: 0x00041F25
		internal int FindIndexOfSecurityHeader(Message message, string[] actors)
		{
			return message.Headers.FindHeader(this.HeaderName.Value, this.HeaderNamespace.Value, actors);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00043D49 File Offset: 0x00041F49
		internal virtual bool IsReaderAtSignatureConfirmation(XmlDictionaryReader reader)
		{
			return false;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00043D4C File Offset: 0x00041F4C
		internal virtual ISignatureValueSecurityElement ReadSignatureConfirmation(XmlDictionaryReader reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SignatureConfirmationNotSupported")));
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00043D68 File Offset: 0x00041F68
		internal ReceiveSecurityHeader TryCreateReceiveSecurityHeader(Message message, string actor, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
		{
			int num = message.Headers.FindHeader(this.HeaderName.Value, this.HeaderNamespace.Value, new string[]
			{
				actor
			});
			if (num < 0 && string.IsNullOrEmpty(actor))
			{
				num = message.Headers.FindHeader(this.HeaderName.Value, this.HeaderNamespace.Value, message.Version.Envelope.UltimateDestinationActorValues);
			}
			if (num < 0)
			{
				return null;
			}
			MessageHeaderInfo messageHeaderInfo = message.Headers[num];
			return this.CreateReceiveSecurityHeader(message, messageHeaderInfo.Actor, messageHeaderInfo.MustUnderstand, messageHeaderInfo.Relay, standardsManager, algorithmSuite, direction, num);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00043E11 File Offset: 0x00042011
		internal virtual void WriteSignatureConfirmation(XmlDictionaryWriter writer, string id, byte[] signatureConfirmation)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SignatureConfirmationNotSupported")));
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00043E2C File Offset: 0x0004202C
		internal void WriteStartHeader(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(this.HeaderPrefix.Value, this.HeaderName, this.HeaderNamespace);
		}

		// Token: 0x040019F6 RID: 6646
		private readonly XmlDictionaryString headerName;

		// Token: 0x040019F7 RID: 6647
		private readonly XmlDictionaryString headerNamespace;

		// Token: 0x040019F8 RID: 6648
		private readonly XmlDictionaryString headerPrefix;

		// Token: 0x02000B20 RID: 2848
		private class SecurityVersion10 : SecurityVersion
		{
			// Token: 0x06006FAA RID: 28586 RVA: 0x0019E4F6 File Offset: 0x0019C6F6
			protected SecurityVersion10() : base(XD.SecurityJan2004Dictionary.Security, XD.SecurityJan2004Dictionary.Namespace, XD.SecurityJan2004Dictionary.Prefix)
			{
			}

			// Token: 0x17001A08 RID: 6664
			// (get) Token: 0x06006FAB RID: 28587 RVA: 0x0019E51C File Offset: 0x0019C71C
			public static SecurityVersion.SecurityVersion10 Instance
			{
				get
				{
					return SecurityVersion.SecurityVersion10.instance;
				}
			}

			// Token: 0x17001A09 RID: 6665
			// (get) Token: 0x06006FAC RID: 28588 RVA: 0x0019E523 File Offset: 0x0019C723
			internal override XmlDictionaryString FailedAuthenticationFaultCode
			{
				get
				{
					return XD.SecurityJan2004Dictionary.FailedAuthenticationFaultCode;
				}
			}

			// Token: 0x17001A0A RID: 6666
			// (get) Token: 0x06006FAD RID: 28589 RVA: 0x0019E52F File Offset: 0x0019C72F
			internal override XmlDictionaryString InvalidSecurityTokenFaultCode
			{
				get
				{
					return XD.SecurityJan2004Dictionary.InvalidSecurityTokenFaultCode;
				}
			}

			// Token: 0x17001A0B RID: 6667
			// (get) Token: 0x06006FAE RID: 28590 RVA: 0x0019E53B File Offset: 0x0019C73B
			internal override XmlDictionaryString InvalidSecurityFaultCode
			{
				get
				{
					return XD.SecurityJan2004Dictionary.InvalidSecurityFaultCode;
				}
			}

			// Token: 0x06006FAF RID: 28591 RVA: 0x0019E547 File Offset: 0x0019C747
			internal override SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
			{
				return new WSSecurityOneDotZeroSendSecurityHeader(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, direction);
			}

			// Token: 0x06006FB0 RID: 28592 RVA: 0x0019E559 File Offset: 0x0019C759
			internal override ReceiveSecurityHeader CreateReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction, int headerIndex)
			{
				return new WSSecurityOneDotZeroReceiveSecurityHeader(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, headerIndex, direction);
			}

			// Token: 0x06006FB1 RID: 28593 RVA: 0x0019E56D File Offset: 0x0019C76D
			public override string ToString()
			{
				return "WSSecurity10";
			}

			// Token: 0x04003FDE RID: 16350
			private static readonly SecurityVersion.SecurityVersion10 instance = new SecurityVersion.SecurityVersion10();
		}

		// Token: 0x02000B21 RID: 2849
		private sealed class SecurityVersion11 : SecurityVersion.SecurityVersion10
		{
			// Token: 0x06006FB3 RID: 28595 RVA: 0x0019E580 File Offset: 0x0019C780
			private SecurityVersion11()
			{
			}

			// Token: 0x17001A0C RID: 6668
			// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x0019E588 File Offset: 0x0019C788
			public new static SecurityVersion.SecurityVersion11 Instance
			{
				get
				{
					return SecurityVersion.SecurityVersion11.instance;
				}
			}

			// Token: 0x17001A0D RID: 6669
			// (get) Token: 0x06006FB5 RID: 28597 RVA: 0x0019E58F File Offset: 0x0019C78F
			internal override bool SupportsSignatureConfirmation
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006FB6 RID: 28598 RVA: 0x0019E592 File Offset: 0x0019C792
			internal override ReceiveSecurityHeader CreateReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction, int headerIndex)
			{
				return new WSSecurityOneDotOneReceiveSecurityHeader(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, headerIndex, direction);
			}

			// Token: 0x06006FB7 RID: 28599 RVA: 0x0019E5A6 File Offset: 0x0019C7A6
			internal override SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction)
			{
				return new WSSecurityOneDotOneSendSecurityHeader(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, direction);
			}

			// Token: 0x06006FB8 RID: 28600 RVA: 0x0019E5B8 File Offset: 0x0019C7B8
			internal override bool IsReaderAtSignatureConfirmation(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(XD.SecurityXXX2005Dictionary.SignatureConfirmation, XD.SecurityXXX2005Dictionary.Namespace);
			}

			// Token: 0x06006FB9 RID: 28601 RVA: 0x0019E5D4 File Offset: 0x0019C7D4
			internal override ISignatureValueSecurityElement ReadSignatureConfirmation(XmlDictionaryReader reader)
			{
				reader.MoveToStartElement(XD.SecurityXXX2005Dictionary.SignatureConfirmation, XD.SecurityXXX2005Dictionary.Namespace);
				bool isEmptyElement = reader.IsEmptyElement;
				string requiredNonEmptyAttribute = XmlHelper.GetRequiredNonEmptyAttribute(reader, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				byte[] requiredBase64Attribute = XmlHelper.GetRequiredBase64Attribute(reader, XD.SecurityXXX2005Dictionary.ValueAttribute, null);
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					reader.ReadEndElement();
				}
				return new SignatureConfirmationElement(requiredNonEmptyAttribute, requiredBase64Attribute, this);
			}

			// Token: 0x06006FBA RID: 28602 RVA: 0x0019E648 File Offset: 0x0019C848
			internal override void WriteSignatureConfirmation(XmlDictionaryWriter writer, string id, byte[] signature)
			{
				if (id == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
				}
				if (signature == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signature");
				}
				writer.WriteStartElement(XD.SecurityXXX2005Dictionary.Prefix.Value, XD.SecurityXXX2005Dictionary.SignatureConfirmation, XD.SecurityXXX2005Dictionary.Namespace);
				writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, id);
				writer.WriteStartAttribute(XD.SecurityXXX2005Dictionary.ValueAttribute, null);
				writer.WriteBase64(signature, 0, signature.Length);
				writer.WriteEndAttribute();
				writer.WriteEndElement();
			}

			// Token: 0x06006FBB RID: 28603 RVA: 0x0019E6F6 File Offset: 0x0019C8F6
			public override string ToString()
			{
				return "WSSecurity11";
			}

			// Token: 0x04003FDF RID: 16351
			private static readonly SecurityVersion.SecurityVersion11 instance = new SecurityVersion.SecurityVersion11();
		}
	}
}
