using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000905 RID: 2309
	public abstract class MsmqBindingElementBase : TransportBindingElement, ITransactedBindingElement, IWsdlExportExtension, IPolicyExportExtension, ITransportPolicyImport
	{
		// Token: 0x060057F3 RID: 22515 RVA: 0x00143634 File Offset: 0x00141834
		internal MsmqBindingElementBase()
		{
			this.customDeadLetterQueue = null;
			this.deadLetterQueue = DeadLetterQueue.System;
			this.durable = true;
			this.exactlyOnce = true;
			this.maxRetryCycles = 2;
			this.receiveContextEnabled = true;
			this.receiveErrorHandling = ReceiveErrorHandling.Fault;
			this.receiveRetryCount = 5;
			this.retryCycleDelay = MsmqDefaults.RetryCycleDelay;
			this.timeToLive = MsmqDefaults.TimeToLive;
			this.msmqTransportSecurity = new MsmqTransportSecurity();
			this.useMsmqTracing = false;
			this.useSourceJournal = false;
			this.ReceiveContextSettings = new MsmqReceiveContextSettings();
		}

		// Token: 0x060057F4 RID: 22516 RVA: 0x001436BC File Offset: 0x001418BC
		internal MsmqBindingElementBase(MsmqBindingElementBase elementToBeCloned) : base(elementToBeCloned)
		{
			this.customDeadLetterQueue = elementToBeCloned.customDeadLetterQueue;
			this.deadLetterQueue = elementToBeCloned.deadLetterQueue;
			this.durable = elementToBeCloned.durable;
			this.exactlyOnce = elementToBeCloned.exactlyOnce;
			this.maxRetryCycles = elementToBeCloned.maxRetryCycles;
			this.msmqTransportSecurity = new MsmqTransportSecurity(elementToBeCloned.MsmqTransportSecurity);
			this.receiveContextEnabled = elementToBeCloned.ReceiveContextEnabled;
			this.receiveErrorHandling = elementToBeCloned.receiveErrorHandling;
			this.receiveRetryCount = elementToBeCloned.receiveRetryCount;
			this.retryCycleDelay = elementToBeCloned.retryCycleDelay;
			this.timeToLive = elementToBeCloned.timeToLive;
			this.useMsmqTracing = elementToBeCloned.useMsmqTracing;
			this.useSourceJournal = elementToBeCloned.useSourceJournal;
			this.ReceiveContextSettings = elementToBeCloned.ReceiveContextSettings;
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x0014377D File Offset: 0x0014197D
		// (set) Token: 0x060057F6 RID: 22518 RVA: 0x00143785 File Offset: 0x00141985
		internal IReceiveContextSettings ReceiveContextSettings { get; set; }

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060057F7 RID: 22519
		internal abstract MsmqUri.IAddressTranslator AddressTranslator { get; }

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060057F8 RID: 22520 RVA: 0x0014378E File Offset: 0x0014198E
		// (set) Token: 0x060057F9 RID: 22521 RVA: 0x00143796 File Offset: 0x00141996
		public Uri CustomDeadLetterQueue
		{
			get
			{
				return this.customDeadLetterQueue;
			}
			set
			{
				this.customDeadLetterQueue = value;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x0014379F File Offset: 0x0014199F
		// (set) Token: 0x060057FB RID: 22523 RVA: 0x001437A7 File Offset: 0x001419A7
		public DeadLetterQueue DeadLetterQueue
		{
			get
			{
				return this.deadLetterQueue;
			}
			set
			{
				if (!DeadLetterQueueHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.deadLetterQueue = value;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060057FC RID: 22524 RVA: 0x001437CD File Offset: 0x001419CD
		// (set) Token: 0x060057FD RID: 22525 RVA: 0x001437D5 File Offset: 0x001419D5
		public bool Durable
		{
			get
			{
				return this.durable;
			}
			set
			{
				this.durable = value;
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060057FE RID: 22526 RVA: 0x001437DE File Offset: 0x001419DE
		public bool TransactedReceiveEnabled
		{
			get
			{
				return this.exactlyOnce;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060057FF RID: 22527 RVA: 0x001437E6 File Offset: 0x001419E6
		// (set) Token: 0x06005800 RID: 22528 RVA: 0x001437EE File Offset: 0x001419EE
		public bool ExactlyOnce
		{
			get
			{
				return this.exactlyOnce;
			}
			set
			{
				this.exactlyOnce = value;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x001437F7 File Offset: 0x001419F7
		// (set) Token: 0x06005802 RID: 22530 RVA: 0x001437FF File Offset: 0x001419FF
		public int ReceiveRetryCount
		{
			get
			{
				return this.receiveRetryCount;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("MsmqNonNegativeArgumentExpected")));
				}
				this.receiveRetryCount = value;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x00143831 File Offset: 0x00141A31
		// (set) Token: 0x06005804 RID: 22532 RVA: 0x00143839 File Offset: 0x00141A39
		public int MaxRetryCycles
		{
			get
			{
				return this.maxRetryCycles;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("MsmqNonNegativeArgumentExpected")));
				}
				this.maxRetryCycles = value;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x0014386B File Offset: 0x00141A6B
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x00143873 File Offset: 0x00141A73
		public MsmqTransportSecurity MsmqTransportSecurity
		{
			get
			{
				return this.msmqTransportSecurity;
			}
			internal set
			{
				this.msmqTransportSecurity = value;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x0014387C File Offset: 0x00141A7C
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x00143884 File Offset: 0x00141A84
		public bool ReceiveContextEnabled
		{
			get
			{
				return this.receiveContextEnabled;
			}
			set
			{
				this.receiveContextEnabled = value;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x0014388D File Offset: 0x00141A8D
		// (set) Token: 0x0600580A RID: 22538 RVA: 0x00143895 File Offset: 0x00141A95
		public ReceiveErrorHandling ReceiveErrorHandling
		{
			get
			{
				return this.receiveErrorHandling;
			}
			set
			{
				if (!ReceiveErrorHandlingHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.receiveErrorHandling = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x001438BB File Offset: 0x00141ABB
		// (set) Token: 0x0600580C RID: 22540 RVA: 0x001438C4 File Offset: 0x00141AC4
		public TimeSpan RetryCycleDelay
		{
			get
			{
				return this.retryCycleDelay;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.retryCycleDelay = value;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x00143937 File Offset: 0x00141B37
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x00143940 File Offset: 0x00141B40
		public TimeSpan TimeToLive
		{
			get
			{
				return this.timeToLive;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.timeToLive = value;
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x001439B3 File Offset: 0x00141BB3
		// (set) Token: 0x06005810 RID: 22544 RVA: 0x001439BB File Offset: 0x00141BBB
		public bool UseMsmqTracing
		{
			get
			{
				return this.useMsmqTracing;
			}
			set
			{
				this.useMsmqTracing = value;
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x001439C4 File Offset: 0x00141BC4
		// (set) Token: 0x06005812 RID: 22546 RVA: 0x001439CC File Offset: 0x00141BCC
		public bool UseSourceJournal
		{
			get
			{
				return this.useSourceJournal;
			}
			set
			{
				this.useSourceJournal = value;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x001439D5 File Offset: 0x00141BD5
		// (set) Token: 0x06005814 RID: 22548 RVA: 0x001439E4 File Offset: 0x00141BE4
		public TimeSpan ValidityDuration
		{
			get
			{
				return this.ReceiveContextSettings.ValidityDuration;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				((MsmqReceiveContextSettings)this.ReceiveContextSettings).SetValidityDuration(value);
			}
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x00143A64 File Offset: 0x00141C64
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return default(T);
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new MsmqBindingElementBase.BindingDeliveryCapabilitiesHelper());
			}
			if (typeof(T) == typeof(IReceiveContextSettings))
			{
				if (this.ExactlyOnce && this.ReceiveContextEnabled)
				{
					return (T)((object)this.ReceiveContextSettings);
				}
				return default(T);
			}
			else
			{
				if (typeof(T) == typeof(ITransactedBindingElement))
				{
					return (T)((object)this);
				}
				return base.GetProperty<T>(context);
			}
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x00143B39 File Offset: 0x00141D39
		private static bool FindAssertion(ICollection<XmlElement> assertions, string name)
		{
			return PolicyConversionContext.FindAssertion(assertions, name, "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq", true) != null;
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x00143B4C File Offset: 0x00141D4C
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
			XmlDocument xmlDocument = new XmlDocument();
			ICollection<XmlElement> bindingAssertions = context.GetBindingAssertions();
			if (!this.Durable)
			{
				bindingAssertions.Add(xmlDocument.CreateElement("msmq", "MsmqVolatile", "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq"));
			}
			if (!this.ExactlyOnce)
			{
				bindingAssertions.Add(xmlDocument.CreateElement("msmq", "MsmqBestEffort", "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq"));
			}
			if (context.Contract.SessionMode == SessionMode.Required)
			{
				bindingAssertions.Add(xmlDocument.CreateElement("msmq", "MsmqSession", "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq"));
			}
			if (this.MsmqTransportSecurity.MsmqProtectionLevel != ProtectionLevel.None)
			{
				bindingAssertions.Add(xmlDocument.CreateElement("msmq", "Authenticated", "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq"));
				if (this.MsmqTransportSecurity.MsmqAuthenticationMode == MsmqAuthenticationMode.WindowsDomain)
				{
					bindingAssertions.Add(xmlDocument.CreateElement("msmq", "WindowsDomain", "http://schemas.microsoft.com/ws/06/2004/mspolicy/msmq"));
				}
			}
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(context.BindingElements, out flag);
			if (flag && messageEncodingBindingElement is IPolicyExportExtension)
			{
				((IPolicyExportExtension)messageEncodingBindingElement).ExportPolicy(exporter, context);
			}
			WsdlExporter.WSAddressingHelper.AddWSAddressingAssertion(exporter, context, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x00143C88 File Offset: 0x00141E88
		void ITransportPolicyImport.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			ICollection<XmlElement> bindingAssertions = policyContext.GetBindingAssertions();
			if (MsmqBindingElementBase.FindAssertion(bindingAssertions, "MsmqVolatile"))
			{
				this.Durable = false;
			}
			if (MsmqBindingElementBase.FindAssertion(bindingAssertions, "MsmqBestEffort"))
			{
				this.ExactlyOnce = false;
			}
			if (MsmqBindingElementBase.FindAssertion(bindingAssertions, "MsmqSession"))
			{
				policyContext.Contract.SessionMode = SessionMode.Required;
			}
			if (!MsmqBindingElementBase.FindAssertion(bindingAssertions, "Authenticated"))
			{
				this.MsmqTransportSecurity.MsmqProtectionLevel = ProtectionLevel.None;
				this.MsmqTransportSecurity.MsmqAuthenticationMode = MsmqAuthenticationMode.None;
				return;
			}
			this.MsmqTransportSecurity.MsmqProtectionLevel = ProtectionLevel.Sign;
			if (MsmqBindingElementBase.FindAssertion(bindingAssertions, "WindowsDomain"))
			{
				this.MsmqTransportSecurity.MsmqAuthenticationMode = MsmqAuthenticationMode.WindowsDomain;
				return;
			}
			this.MsmqTransportSecurity.MsmqAuthenticationMode = MsmqAuthenticationMode.Certificate;
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x00143D35 File Offset: 0x00141F35
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x00143D37 File Offset: 0x00141F37
		internal virtual string WsdlTransportUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x00143D3C File Offset: 0x00141F3C
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(endpointContext, out flag);
			TransportBindingElement.ExportWsdlEndpoint(exporter, endpointContext, this.WsdlTransportUri, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x00143D6C File Offset: 0x00141F6C
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(BindingElementCollection bindingElements, out bool createdNew)
		{
			createdNew = false;
			MessageEncodingBindingElement messageEncodingBindingElement = bindingElements.Find<MessageEncodingBindingElement>();
			if (messageEncodingBindingElement == null)
			{
				createdNew = true;
				messageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			}
			return messageEncodingBindingElement;
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x00143D90 File Offset: 0x00141F90
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(WsdlEndpointConversionContext endpointContext, out bool createdNew)
		{
			BindingElementCollection bindingElements = endpointContext.Endpoint.Binding.CreateBindingElements();
			return this.FindMessageEncodingBindingElement(bindingElements, out createdNew);
		}

		// Token: 0x0400360B RID: 13835
		private Uri customDeadLetterQueue;

		// Token: 0x0400360C RID: 13836
		private DeadLetterQueue deadLetterQueue;

		// Token: 0x0400360D RID: 13837
		private bool durable;

		// Token: 0x0400360E RID: 13838
		private bool exactlyOnce;

		// Token: 0x0400360F RID: 13839
		private int maxRetryCycles;

		// Token: 0x04003610 RID: 13840
		private ReceiveErrorHandling receiveErrorHandling;

		// Token: 0x04003611 RID: 13841
		private int receiveRetryCount;

		// Token: 0x04003612 RID: 13842
		private TimeSpan retryCycleDelay;

		// Token: 0x04003613 RID: 13843
		private TimeSpan timeToLive;

		// Token: 0x04003614 RID: 13844
		private MsmqTransportSecurity msmqTransportSecurity;

		// Token: 0x04003615 RID: 13845
		private bool useMsmqTracing;

		// Token: 0x04003616 RID: 13846
		private bool useSourceJournal;

		// Token: 0x04003617 RID: 13847
		private bool receiveContextEnabled;

		// Token: 0x02000DAF RID: 3503
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x06007F2C RID: 32556 RVA: 0x001D92F9 File Offset: 0x001D74F9
			internal BindingDeliveryCapabilitiesHelper()
			{
			}

			// Token: 0x17001C55 RID: 7253
			// (get) Token: 0x06007F2D RID: 32557 RVA: 0x001D9301 File Offset: 0x001D7501
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001C56 RID: 7254
			// (get) Token: 0x06007F2E RID: 32558 RVA: 0x001D9304 File Offset: 0x001D7504
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return true;
				}
			}
		}
	}
}
