using System;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F4 RID: 756
	public class ChannelProtectionRequirements
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x0005E433 File Offset: 0x0005C633
		public ChannelProtectionRequirements()
		{
			this.incomingSignatureParts = new ScopedMessagePartSpecification();
			this.incomingEncryptionParts = new ScopedMessagePartSpecification();
			this.outgoingSignatureParts = new ScopedMessagePartSpecification();
			this.outgoingEncryptionParts = new ScopedMessagePartSpecification();
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005E467 File Offset: 0x0005C667
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005E470 File Offset: 0x0005C670
		public ChannelProtectionRequirements(ChannelProtectionRequirements other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("other"));
			}
			this.incomingSignatureParts = new ScopedMessagePartSpecification(other.incomingSignatureParts);
			this.incomingEncryptionParts = new ScopedMessagePartSpecification(other.incomingEncryptionParts);
			this.outgoingSignatureParts = new ScopedMessagePartSpecification(other.outgoingSignatureParts);
			this.outgoingEncryptionParts = new ScopedMessagePartSpecification(other.outgoingEncryptionParts);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005E4E0 File Offset: 0x0005C6E0
		internal ChannelProtectionRequirements(ChannelProtectionRequirements other, ProtectionLevel newBodyProtectionLevel)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("other"));
			}
			this.incomingSignatureParts = new ScopedMessagePartSpecification(other.incomingSignatureParts, newBodyProtectionLevel > ProtectionLevel.None);
			this.incomingEncryptionParts = new ScopedMessagePartSpecification(other.incomingEncryptionParts, newBodyProtectionLevel == ProtectionLevel.EncryptAndSign);
			this.outgoingSignatureParts = new ScopedMessagePartSpecification(other.outgoingSignatureParts, newBodyProtectionLevel > ProtectionLevel.None);
			this.outgoingEncryptionParts = new ScopedMessagePartSpecification(other.outgoingEncryptionParts, newBodyProtectionLevel == ProtectionLevel.EncryptAndSign);
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0005E55F File Offset: 0x0005C75F
		public ScopedMessagePartSpecification IncomingSignatureParts
		{
			get
			{
				return this.incomingSignatureParts;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0005E567 File Offset: 0x0005C767
		public ScopedMessagePartSpecification IncomingEncryptionParts
		{
			get
			{
				return this.incomingEncryptionParts;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0005E56F File Offset: 0x0005C76F
		public ScopedMessagePartSpecification OutgoingSignatureParts
		{
			get
			{
				return this.outgoingSignatureParts;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0005E577 File Offset: 0x0005C777
		public ScopedMessagePartSpecification OutgoingEncryptionParts
		{
			get
			{
				return this.outgoingEncryptionParts;
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005E57F File Offset: 0x0005C77F
		public void Add(ChannelProtectionRequirements protectionRequirements)
		{
			this.Add(protectionRequirements, false);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005E58C File Offset: 0x0005C78C
		public void Add(ChannelProtectionRequirements protectionRequirements, bool channelScopeOnly)
		{
			if (protectionRequirements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("protectionRequirements"));
			}
			if (protectionRequirements.incomingSignatureParts != null)
			{
				this.incomingSignatureParts.AddParts(protectionRequirements.incomingSignatureParts.ChannelParts);
			}
			if (protectionRequirements.incomingEncryptionParts != null)
			{
				this.incomingEncryptionParts.AddParts(protectionRequirements.incomingEncryptionParts.ChannelParts);
			}
			if (protectionRequirements.outgoingSignatureParts != null)
			{
				this.outgoingSignatureParts.AddParts(protectionRequirements.outgoingSignatureParts.ChannelParts);
			}
			if (protectionRequirements.outgoingEncryptionParts != null)
			{
				this.outgoingEncryptionParts.AddParts(protectionRequirements.outgoingEncryptionParts.ChannelParts);
			}
			if (!channelScopeOnly)
			{
				ChannelProtectionRequirements.AddActionParts(this.incomingSignatureParts, protectionRequirements.incomingSignatureParts);
				ChannelProtectionRequirements.AddActionParts(this.incomingEncryptionParts, protectionRequirements.incomingEncryptionParts);
				ChannelProtectionRequirements.AddActionParts(this.outgoingSignatureParts, protectionRequirements.outgoingSignatureParts);
				ChannelProtectionRequirements.AddActionParts(this.outgoingEncryptionParts, protectionRequirements.outgoingEncryptionParts);
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0005E670 File Offset: 0x0005C870
		private static void AddActionParts(ScopedMessagePartSpecification to, ScopedMessagePartSpecification from)
		{
			foreach (string action in from.Actions)
			{
				MessagePartSpecification parts;
				if (from.TryGetParts(action, true, out parts))
				{
					to.AddParts(parts, action);
				}
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.incomingSignatureParts.MakeReadOnly();
				this.incomingEncryptionParts.MakeReadOnly();
				this.outgoingSignatureParts.MakeReadOnly();
				this.outgoingEncryptionParts.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0005E70C File Offset: 0x0005C90C
		public ChannelProtectionRequirements CreateInverse()
		{
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			channelProtectionRequirements.Add(this, true);
			channelProtectionRequirements.incomingSignatureParts = new ScopedMessagePartSpecification(this.OutgoingSignatureParts);
			channelProtectionRequirements.outgoingSignatureParts = new ScopedMessagePartSpecification(this.IncomingSignatureParts);
			channelProtectionRequirements.incomingEncryptionParts = new ScopedMessagePartSpecification(this.OutgoingEncryptionParts);
			channelProtectionRequirements.outgoingEncryptionParts = new ScopedMessagePartSpecification(this.IncomingEncryptionParts);
			return channelProtectionRequirements;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0005E76C File Offset: 0x0005C96C
		internal static ChannelProtectionRequirements CreateFromContract(ContractDescription contract, ISecurityCapabilities bindingElement, bool isForClient)
		{
			return ChannelProtectionRequirements.CreateFromContract(contract, bindingElement.SupportedRequestProtectionLevel, bindingElement.SupportedResponseProtectionLevel, isForClient);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0005E784 File Offset: 0x0005C984
		private static MessagePartSpecification UnionMessagePartSpecifications(ScopedMessagePartSpecification actionParts)
		{
			MessagePartSpecification messagePartSpecification = new MessagePartSpecification(false);
			foreach (string action in actionParts.Actions)
			{
				MessagePartSpecification messagePartSpecification2;
				if (actionParts.TryGetParts(action, out messagePartSpecification2))
				{
					if (messagePartSpecification2.IsBodyIncluded)
					{
						messagePartSpecification.IsBodyIncluded = true;
					}
					foreach (XmlQualifiedName xmlQualifiedName in messagePartSpecification2.HeaderTypes)
					{
						if (!messagePartSpecification.IsHeaderIncluded(xmlQualifiedName.Name, xmlQualifiedName.Namespace))
						{
							messagePartSpecification.HeaderTypes.Add(xmlQualifiedName);
						}
					}
				}
			}
			return messagePartSpecification;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0005E84C File Offset: 0x0005CA4C
		internal static ChannelProtectionRequirements CreateFromContractAndUnionResponseProtectionRequirements(ContractDescription contract, ISecurityCapabilities bindingElement, bool isForClient)
		{
			ChannelProtectionRequirements channelProtectionRequirements = ChannelProtectionRequirements.CreateFromContract(contract, bindingElement.SupportedRequestProtectionLevel, bindingElement.SupportedResponseProtectionLevel, isForClient);
			ChannelProtectionRequirements channelProtectionRequirements2 = new ChannelProtectionRequirements();
			channelProtectionRequirements2.OutgoingEncryptionParts.AddParts(ChannelProtectionRequirements.UnionMessagePartSpecifications(channelProtectionRequirements.OutgoingEncryptionParts), "*");
			channelProtectionRequirements2.OutgoingSignatureParts.AddParts(ChannelProtectionRequirements.UnionMessagePartSpecifications(channelProtectionRequirements.OutgoingSignatureParts), "*");
			channelProtectionRequirements.IncomingEncryptionParts.CopyTo(channelProtectionRequirements2.IncomingEncryptionParts);
			channelProtectionRequirements.IncomingSignatureParts.CopyTo(channelProtectionRequirements2.IncomingSignatureParts);
			return channelProtectionRequirements2;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0005E8CC File Offset: 0x0005CACC
		internal static ChannelProtectionRequirements CreateFromContract(ContractDescription contract, ProtectionLevel defaultRequestProtectionLevel, ProtectionLevel defaultResponseProtectionLevel, bool isForClient)
		{
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contract"));
			}
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			ProtectionLevel protectionLevel;
			ProtectionLevel protectionLevel2;
			if (contract.HasProtectionLevel)
			{
				protectionLevel = contract.ProtectionLevel;
				protectionLevel2 = contract.ProtectionLevel;
			}
			else
			{
				protectionLevel = defaultRequestProtectionLevel;
				protectionLevel2 = defaultResponseProtectionLevel;
			}
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				ProtectionLevel protectionLevel3;
				ProtectionLevel protectionLevel4;
				if (operationDescription.HasProtectionLevel)
				{
					protectionLevel3 = operationDescription.ProtectionLevel;
					protectionLevel4 = operationDescription.ProtectionLevel;
				}
				else
				{
					protectionLevel3 = protectionLevel;
					protectionLevel4 = protectionLevel2;
				}
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					ProtectionLevel protectionLevel5;
					if (messageDescription.HasProtectionLevel)
					{
						protectionLevel5 = messageDescription.ProtectionLevel;
					}
					else if (messageDescription.Direction == MessageDirection.Input)
					{
						protectionLevel5 = protectionLevel3;
					}
					else
					{
						protectionLevel5 = protectionLevel4;
					}
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
					foreach (MessageHeaderDescription header in messageDescription.Headers)
					{
						ChannelProtectionRequirements.AddHeaderProtectionRequirements(header, messagePartSpecification, messagePartSpecification2, protectionLevel5);
					}
					ProtectionLevel protectionLevel6;
					if (messageDescription.Body.Parts.Count > 0)
					{
						protectionLevel6 = ProtectionLevel.None;
					}
					else if (messageDescription.Body.ReturnValue != null)
					{
						if (!messageDescription.Body.ReturnValue.GetType().Equals(typeof(MessagePartDescription)))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OnlyBodyReturnValuesSupported")));
						}
						MessagePartDescription returnValue = messageDescription.Body.ReturnValue;
						protectionLevel6 = (returnValue.HasProtectionLevel ? returnValue.ProtectionLevel : protectionLevel5);
					}
					else
					{
						protectionLevel6 = protectionLevel5;
					}
					if (messageDescription.Body.Parts.Count > 0)
					{
						foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
						{
							ProtectionLevel v = messagePartDescription.HasProtectionLevel ? messagePartDescription.ProtectionLevel : protectionLevel5;
							protectionLevel6 = ProtectionLevelHelper.Max(protectionLevel6, v);
							if (protectionLevel6 == ProtectionLevel.EncryptAndSign)
							{
								break;
							}
						}
					}
					if (protectionLevel6 != ProtectionLevel.None)
					{
						messagePartSpecification.IsBodyIncluded = true;
						if (protectionLevel6 == ProtectionLevel.EncryptAndSign)
						{
							messagePartSpecification2.IsBodyIncluded = true;
						}
					}
					if (messageDescription.Direction == MessageDirection.Input)
					{
						channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification, messageDescription.Action);
						channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification2, messageDescription.Action);
					}
					else
					{
						channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification, messageDescription.Action);
						channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, messageDescription.Action);
					}
				}
				if (operationDescription.Faults != null)
				{
					if (operationDescription.IsServerInitiated())
					{
						ChannelProtectionRequirements.AddFaultProtectionRequirements(operationDescription.Faults, channelProtectionRequirements, protectionLevel3, true);
					}
					else
					{
						ChannelProtectionRequirements.AddFaultProtectionRequirements(operationDescription.Faults, channelProtectionRequirements, protectionLevel4, false);
					}
				}
			}
			return channelProtectionRequirements;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005EC1C File Offset: 0x0005CE1C
		private static void AddHeaderProtectionRequirements(MessageHeaderDescription header, MessagePartSpecification signedParts, MessagePartSpecification encryptedParts, ProtectionLevel defaultProtectionLevel)
		{
			ProtectionLevel protectionLevel = header.HasProtectionLevel ? header.ProtectionLevel : defaultProtectionLevel;
			if (protectionLevel != ProtectionLevel.None)
			{
				XmlQualifiedName item = new XmlQualifiedName(header.Name, header.Namespace);
				signedParts.HeaderTypes.Add(item);
				if (protectionLevel == ProtectionLevel.EncryptAndSign)
				{
					encryptedParts.HeaderTypes.Add(item);
				}
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0005EC6C File Offset: 0x0005CE6C
		private static void AddFaultProtectionRequirements(FaultDescriptionCollection faults, ChannelProtectionRequirements requirements, ProtectionLevel defaultProtectionLevel, bool addToIncoming)
		{
			if (faults == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("faults"));
			}
			if (requirements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("requirements"));
			}
			foreach (FaultDescription faultDescription in faults)
			{
				MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
				MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
				ProtectionLevel protectionLevel = faultDescription.HasProtectionLevel ? faultDescription.ProtectionLevel : defaultProtectionLevel;
				if (protectionLevel != ProtectionLevel.None)
				{
					messagePartSpecification.IsBodyIncluded = true;
					if (protectionLevel == ProtectionLevel.EncryptAndSign)
					{
						messagePartSpecification2.IsBodyIncluded = true;
					}
				}
				if (addToIncoming)
				{
					requirements.IncomingSignatureParts.AddParts(messagePartSpecification, faultDescription.Action);
					requirements.IncomingEncryptionParts.AddParts(messagePartSpecification2, faultDescription.Action);
				}
				else
				{
					requirements.OutgoingSignatureParts.AddParts(messagePartSpecification, faultDescription.Action);
					requirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, faultDescription.Action);
				}
			}
		}

		// Token: 0x04001C96 RID: 7318
		private ScopedMessagePartSpecification incomingSignatureParts;

		// Token: 0x04001C97 RID: 7319
		private ScopedMessagePartSpecification incomingEncryptionParts;

		// Token: 0x04001C98 RID: 7320
		private ScopedMessagePartSpecification outgoingSignatureParts;

		// Token: 0x04001C99 RID: 7321
		private ScopedMessagePartSpecification outgoingEncryptionParts;

		// Token: 0x04001C9A RID: 7322
		private bool isReadOnly;
	}
}
