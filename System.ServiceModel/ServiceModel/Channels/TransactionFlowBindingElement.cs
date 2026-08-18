using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A5D RID: 2653
	public sealed class TransactionFlowBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x060068C7 RID: 26823 RVA: 0x001874B0 File Offset: 0x001856B0
		public TransactionFlowBindingElement() : this(true, TransactionFlowDefaults.TransactionProtocol)
		{
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x001874BE File Offset: 0x001856BE
		public TransactionFlowBindingElement(TransactionProtocol transactionProtocol) : this(true, transactionProtocol)
		{
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x001874C8 File Offset: 0x001856C8
		internal TransactionFlowBindingElement(bool transactions) : this(transactions, TransactionFlowDefaults.TransactionProtocol)
		{
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x001874D8 File Offset: 0x001856D8
		internal TransactionFlowBindingElement(bool transactions, TransactionProtocol transactionProtocol)
		{
			this.transactions = transactions;
			this.issuedTokens = (transactions ? TransactionFlowOption.Allowed : TransactionFlowOption.NotAllowed);
			if (!TransactionProtocol.IsDefined(transactionProtocol))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTransactionFlowProtocolValue", new object[]
				{
					transactionProtocol.ToString()
				}));
			}
			this.transactionProtocol = transactionProtocol;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x00187534 File Offset: 0x00185734
		private TransactionFlowBindingElement(TransactionFlowBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.transactions = elementToBeCloned.transactions;
			this.issuedTokens = elementToBeCloned.issuedTokens;
			if (!TransactionProtocol.IsDefined(elementToBeCloned.transactionProtocol))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTransactionFlowProtocolValue", new object[]
				{
					elementToBeCloned.transactionProtocol.ToString()
				}));
			}
			this.transactionProtocol = elementToBeCloned.transactionProtocol;
			this.AllowWildcardAction = elementToBeCloned.AllowWildcardAction;
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x060068CC RID: 26828 RVA: 0x001875AE File Offset: 0x001857AE
		// (set) Token: 0x060068CD RID: 26829 RVA: 0x001875B6 File Offset: 0x001857B6
		internal bool Transactions
		{
			get
			{
				return this.transactions;
			}
			set
			{
				this.transactions = value;
				this.issuedTokens = (value ? TransactionFlowOption.Allowed : TransactionFlowOption.NotAllowed);
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x060068CE RID: 26830 RVA: 0x001875CC File Offset: 0x001857CC
		// (set) Token: 0x060068CF RID: 26831 RVA: 0x001875D4 File Offset: 0x001857D4
		internal TransactionFlowOption IssuedTokens
		{
			get
			{
				return this.issuedTokens;
			}
			set
			{
				TransactionFlowBindingElement.ValidateOption(value);
				this.issuedTokens = value;
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x001875E3 File Offset: 0x001857E3
		public override BindingElement Clone()
		{
			return new TransactionFlowBindingElement(this);
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x001875EC File Offset: 0x001857EC
		private bool IsFlowEnabled(Dictionary<DirectionalAction, TransactionFlowOption> dictionary)
		{
			if (this.issuedTokens != TransactionFlowOption.NotAllowed)
			{
				return true;
			}
			if (!this.transactions)
			{
				return false;
			}
			foreach (TransactionFlowOption transactionFlowOption in dictionary.Values)
			{
				if (transactionFlowOption != TransactionFlowOption.NotAllowed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x00187658 File Offset: 0x00185858
		internal bool IsFlowEnabled(ContractDescription contract)
		{
			if (this.issuedTokens != TransactionFlowOption.NotAllowed)
			{
				return true;
			}
			if (!this.transactions)
			{
				return false;
			}
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				TransactionFlowAttribute transactionFlowAttribute = operationDescription.Behaviors.Find<TransactionFlowAttribute>();
				if (transactionFlowAttribute != null && transactionFlowAttribute.Transactions != TransactionFlowOption.NotAllowed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x060068D3 RID: 26835 RVA: 0x001876D0 File Offset: 0x001858D0
		// (set) Token: 0x060068D4 RID: 26836 RVA: 0x001876D8 File Offset: 0x001858D8
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return this.transactionProtocol;
			}
			set
			{
				if (!TransactionProtocol.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.transactionProtocol = value;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060068D5 RID: 26837 RVA: 0x001876FE File Offset: 0x001858FE
		// (set) Token: 0x060068D6 RID: 26838 RVA: 0x00187706 File Offset: 0x00185906
		[DefaultValue(false)]
		public bool AllowWildcardAction { get; set; }

		// Token: 0x060068D7 RID: 26839 RVA: 0x0018770F File Offset: 0x0018590F
		internal static void ValidateOption(TransactionFlowOption opt)
		{
			if (!TransactionFlowOptionHelper.IsDefined(opt))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("TransactionFlowBadOption")));
			}
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x00187733 File Offset: 0x00185933
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransactionProtocol()
		{
			return this.TransactionProtocol != TransactionProtocol.Default;
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x00187748 File Offset: 0x00185948
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			return (typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestSessionChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel)) && context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x0018781C File Offset: 0x00185A1C
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = this.GetDictionary(context);
			if (!this.IsFlowEnabled(dictionary))
			{
				return context.BuildInnerChannelFactory<TChannel>();
			}
			if (this.issuedTokens == TransactionFlowOption.NotAllowed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransactionFlowRequiredIssuedTokens")));
			}
			return new TransactionChannelFactory<TChannel>(this.transactionProtocol, context, dictionary, this.AllowWildcardAction)
			{
				FlowIssuedTokens = this.IssuedTokens
			};
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x001878D0 File Offset: 0x00185AD0
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			if (!context.CanBuildInnerChannelListener<TChannel>())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = this.GetDictionary(context);
			if (!this.IsFlowEnabled(dictionary))
			{
				return context.BuildInnerChannelListener<TChannel>();
			}
			if (this.issuedTokens == TransactionFlowOption.NotAllowed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransactionFlowRequiredIssuedTokens")));
			}
			IChannelListener<TChannel> innerListener = context.BuildInnerChannelListener<TChannel>();
			return new TransactionChannelListener<TChannel>(this.transactionProtocol, context.Binding, dictionary, innerListener)
			{
				FlowIssuedTokens = this.IssuedTokens
			};
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0018798C File Offset: 0x00185B8C
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return context.CanBuildInnerChannelListener<TChannel>() && (typeof(TChannel) == typeof(IInputChannel) || typeof(TChannel) == typeof(IReplyChannel) || typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IInputSessionChannel) || typeof(TChannel) == typeof(IReplySessionChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel));
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x00187A48 File Offset: 0x00185C48
		private Dictionary<DirectionalAction, TransactionFlowOption> GetDictionary(BindingContext context)
		{
			Dictionary<DirectionalAction, TransactionFlowOption> dictionary = context.BindingParameters.Find<Dictionary<DirectionalAction, TransactionFlowOption>>();
			if (dictionary == null)
			{
				dictionary = new Dictionary<DirectionalAction, TransactionFlowOption>();
			}
			return dictionary;
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x00187A6C File Offset: 0x00185C6C
		internal static MessagePartSpecification GetIssuedTokenHeaderSpecification(SecurityStandardsManager standardsManager)
		{
			if (standardsManager.TrustDriver.IsIssuedTokensSupported)
			{
				return new MessagePartSpecification(new XmlQualifiedName[]
				{
					new XmlQualifiedName(standardsManager.TrustDriver.IssuedTokensHeaderName, standardsManager.TrustDriver.IssuedTokensHeaderNamespace)
				});
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TrustDriverVersionDoesNotSupportIssuedTokens")));
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x00187AD0 File Offset: 0x00185CD0
		public override T GetProperty<T>(BindingContext context)
		{
			if (!(typeof(T) == typeof(ChannelProtectionRequirements)))
			{
				return context.GetInnerProperty<T>();
			}
			ChannelProtectionRequirements protectionRequirements = this.GetProtectionRequirements();
			if (protectionRequirements != null)
			{
				protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
				return (T)((object)protectionRequirements);
			}
			return (T)((object)context.GetInnerProperty<ChannelProtectionRequirements>());
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x00187B30 File Offset: 0x00185D30
		private ChannelProtectionRequirements GetProtectionRequirements()
		{
			if (this.Transactions || this.IssuedTokens != TransactionFlowOption.NotAllowed)
			{
				ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
				if (this.Transactions)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(new XmlQualifiedName[]
					{
						new XmlQualifiedName("CoordinationContext", "http://schemas.xmlsoap.org/ws/2004/10/wscoor"),
						new XmlQualifiedName("CoordinationContext", "http://docs.oasis-open.org/ws-tx/wscoor/2006/06"),
						new XmlQualifiedName("OleTxTransaction", "http://schemas.microsoft.com/ws/2006/02/tx/oletx")
					});
					messagePartSpecification.MakeReadOnly();
					channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification);
				}
				if (this.IssuedTokens != TransactionFlowOption.NotAllowed)
				{
					MessagePartSpecification issuedTokenHeaderSpecification = TransactionFlowBindingElement.GetIssuedTokenHeaderSpecification(SecurityStandardsManager.DefaultInstance);
					issuedTokenHeaderSpecification.MakeReadOnly();
					channelProtectionRequirements.IncomingSignatureParts.AddParts(issuedTokenHeaderSpecification);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(issuedTokenHeaderSpecification);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(issuedTokenHeaderSpecification);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(issuedTokenHeaderSpecification);
				}
				MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification(true);
				messagePartSpecification2.MakeReadOnly();
				channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification2, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault");
				channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault");
				return channelProtectionRequirements;
			}
			return null;
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x00187C54 File Offset: 0x00185E54
		private XmlElement GetAssertion(XmlDocument doc, TransactionFlowOption option, string prefix, string name, string ns, string policyNs)
		{
			if (doc == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("doc");
			}
			XmlElement xmlElement = null;
			switch (option)
			{
			case TransactionFlowOption.Allowed:
			{
				xmlElement = doc.CreateElement(prefix, name, ns);
				XmlAttribute xmlAttribute = doc.CreateAttribute("wsp", "Optional", policyNs);
				xmlAttribute.Value = "true";
				xmlElement.Attributes.Append(xmlAttribute);
				if (this.transactionProtocol == TransactionProtocol.OleTransactions || this.transactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004)
				{
					XmlAttribute xmlAttribute2 = doc.CreateAttribute("wsp1", "Optional", "http://schemas.xmlsoap.org/ws/2002/12/policy");
					xmlAttribute2.Value = "true";
					xmlElement.Attributes.Append(xmlAttribute2);
				}
				break;
			}
			case TransactionFlowOption.Mandatory:
				xmlElement = doc.CreateElement(prefix, name, ns);
				break;
			}
			return xmlElement;
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x00187D1C File Offset: 0x00185F1C
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			TransactionFlowBindingElement transactionFlowBindingElement = context.BindingElements.Find<TransactionFlowBindingElement>();
			if (transactionFlowBindingElement == null || !transactionFlowBindingElement.Transactions)
			{
				return;
			}
			XmlDocument doc = new XmlDocument();
			XmlElement xmlElement = null;
			foreach (OperationDescription operationDescription in context.Contract.Operations)
			{
				TransactionFlowAttribute transactionFlowAttribute = operationDescription.Behaviors.Find<TransactionFlowAttribute>();
				TransactionFlowOption option = (transactionFlowAttribute == null) ? TransactionFlowOption.NotAllowed : transactionFlowAttribute.Transactions;
				if (transactionFlowBindingElement.TransactionProtocol == TransactionProtocol.OleTransactions)
				{
					xmlElement = this.GetAssertion(doc, option, "oletx", "OleTxAssertion", "http://schemas.microsoft.com/ws/2006/02/tx/oletx", exporter.PolicyVersion.Namespace);
				}
				else if (transactionFlowBindingElement.TransactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004)
				{
					xmlElement = this.GetAssertion(doc, option, "wsat", "ATAssertion", "http://schemas.xmlsoap.org/ws/2004/10/wsat", exporter.PolicyVersion.Namespace);
				}
				else if (transactionFlowBindingElement.TransactionProtocol == TransactionProtocol.WSAtomicTransaction11)
				{
					xmlElement = this.GetAssertion(doc, option, "wsat", "ATAssertion", "http://docs.oasis-open.org/ws-tx/wsat/2006/06", exporter.PolicyVersion.Namespace);
				}
				if (xmlElement != null)
				{
					context.GetOperationBindingAssertions(operationDescription).Add(xmlElement);
				}
			}
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x00187E7C File Offset: 0x0018607C
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			TransactionFlowBindingElement transactionFlowBindingElement = b as TransactionFlowBindingElement;
			return transactionFlowBindingElement != null && this.transactions == transactionFlowBindingElement.transactions && this.issuedTokens == transactionFlowBindingElement.issuedTokens && this.transactionProtocol == transactionFlowBindingElement.transactionProtocol;
		}

		// Token: 0x04003C10 RID: 15376
		private bool transactions;

		// Token: 0x04003C11 RID: 15377
		private TransactionFlowOption issuedTokens;

		// Token: 0x04003C12 RID: 15378
		private TransactionProtocol transactionProtocol;
	}
}
