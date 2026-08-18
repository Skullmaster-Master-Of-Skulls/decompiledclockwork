using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000145 RID: 325
	public class NetMsmqBinding : MsmqBindingBase
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x0002457E File Offset: 0x0002277E
		public NetMsmqBinding()
		{
			this.Initialize();
			this.security = new NetMsmqSecurity();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00024597 File Offset: 0x00022797
		public NetMsmqBinding(string configurationName)
		{
			this.Initialize();
			this.security = new NetMsmqSecurity();
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000245B8 File Offset: 0x000227B8
		public NetMsmqBinding(NetMsmqSecurityMode securityMode)
		{
			if (!NetMsmqSecurityModeHelper.IsDefined(securityMode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("mode", (int)securityMode, typeof(NetMsmqSecurityMode)));
			}
			this.Initialize();
			this.security = new NetMsmqSecurity(securityMode);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00024605 File Offset: 0x00022805
		private NetMsmqBinding(NetMsmqSecurity security)
		{
			this.Initialize();
			this.security = security;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0002461A File Offset: 0x0002281A
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0002462C File Offset: 0x0002282C
		[DefaultValue(QueueTransferProtocol.Native)]
		public QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return (this.transport as MsmqTransportBindingElement).QueueTransferProtocol;
			}
			set
			{
				(this.transport as MsmqTransportBindingElement).QueueTransferProtocol = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0002463F File Offset: 0x0002283F
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0002464C File Offset: 0x0002284C
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.encoding.ReaderQuotas;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				value.CopyTo(this.encoding.ReaderQuotas);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00024672 File Offset: 0x00022872
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0002467A File Offset: 0x0002287A
		public NetMsmqSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				this.security = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00024683 File Offset: 0x00022883
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002468A File Offset: 0x0002288A
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00024697 File Offset: 0x00022897
		[DefaultValue(524288L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return this.transport.MaxBufferPoolSize;
			}
			set
			{
				this.transport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000246A5 File Offset: 0x000228A5
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x000246B7 File Offset: 0x000228B7
		internal int MaxPoolSize
		{
			get
			{
				return (this.transport as MsmqTransportBindingElement).MaxPoolSize;
			}
			set
			{
				(this.transport as MsmqTransportBindingElement).MaxPoolSize = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x000246CA File Offset: 0x000228CA
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x000246DC File Offset: 0x000228DC
		[DefaultValue(false)]
		public bool UseActiveDirectory
		{
			get
			{
				return (this.transport as MsmqTransportBindingElement).UseActiveDirectory;
			}
			set
			{
				(this.transport as MsmqTransportBindingElement).UseActiveDirectory = value;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000246F0 File Offset: 0x000228F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return this.ReaderQuotas.MaxArrayLength != 16384 || this.ReaderQuotas.MaxBytesPerRead != 4096 || this.ReaderQuotas.MaxDepth != 32 || this.ReaderQuotas.MaxNameTableCharCount != 16384 || this.ReaderQuotas.MaxStringContentLength != 8192;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00024760 File Offset: 0x00022960
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.security.Mode != NetMsmqSecurityMode.Transport || (this.security.Transport.MsmqAuthenticationMode != MsmqAuthenticationMode.WindowsDomain || this.security.Transport.MsmqEncryptionAlgorithm != MsmqEncryptionAlgorithm.RC4Stream || this.security.Transport.MsmqSecureHashAlgorithm != MsmqDefaults.MsmqSecureHashAlgorithm || this.security.Transport.MsmqProtectionLevel != ProtectionLevel.Sign) || (this.security.Message.AlgorithmSuite != MsmqDefaults.MessageSecurityAlgorithmSuite || this.security.Message.ClientCredentialType != MessageCredentialType.Windows);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000247FB File Offset: 0x000229FB
		private void Initialize()
		{
			this.transport = new MsmqTransportBindingElement();
			this.encoding = new BinaryMessageEncodingBindingElement();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00024814 File Offset: 0x00022A14
		private void InitializeFrom(MsmqTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding)
		{
			base.CustomDeadLetterQueue = transport.CustomDeadLetterQueue;
			base.DeadLetterQueue = transport.DeadLetterQueue;
			base.Durable = transport.Durable;
			base.ExactlyOnce = transport.ExactlyOnce;
			base.MaxReceivedMessageSize = transport.MaxReceivedMessageSize;
			base.ReceiveRetryCount = transport.ReceiveRetryCount;
			base.MaxRetryCycles = transport.MaxRetryCycles;
			base.ReceiveErrorHandling = transport.ReceiveErrorHandling;
			base.RetryCycleDelay = transport.RetryCycleDelay;
			base.TimeToLive = transport.TimeToLive;
			base.UseSourceJournal = transport.UseSourceJournal;
			base.UseMsmqTracing = transport.UseMsmqTracing;
			base.ReceiveContextEnabled = transport.ReceiveContextEnabled;
			this.QueueTransferProtocol = transport.QueueTransferProtocol;
			this.MaxBufferPoolSize = transport.MaxBufferPoolSize;
			this.UseActiveDirectory = transport.UseActiveDirectory;
			base.ValidityDuration = transport.ValidityDuration;
			this.ReaderQuotas = encoding.ReaderQuotas;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000248F9 File Offset: 0x00022AF9
		private bool IsBindingElementsMatch(MsmqTransportBindingElement transport, MessageEncodingBindingElement encoding)
		{
			return this.GetTransport().IsMatch(transport) && this.encoding.IsMatch(encoding);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002491C File Offset: 0x00022B1C
		private void ApplyConfiguration(string configurationName)
		{
			NetMsmqBindingCollectionElement bindingCollectionElement = NetMsmqBindingCollectionElement.GetBindingCollectionElement();
			NetMsmqBindingElement netMsmqBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (netMsmqBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netMsmqBinding"
				})));
			}
			netMsmqBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00024974 File Offset: 0x00022B74
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			SecurityBindingElement securityBindingElement = this.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			bindingElementCollection.Add(this.encoding);
			bindingElementCollection.Add(this.GetTransport());
			return bindingElementCollection.Clone();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000249B8 File Offset: 0x00022BB8
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 3)
			{
				return false;
			}
			SecurityBindingElement sbe = null;
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = null;
			MsmqTransportBindingElement msmqTransportBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is SecurityBindingElement)
				{
					sbe = (bindingElement as SecurityBindingElement);
				}
				else if (bindingElement is TransportBindingElement)
				{
					msmqTransportBindingElement = (bindingElement as MsmqTransportBindingElement);
				}
				else
				{
					if (!(bindingElement is MessageEncodingBindingElement))
					{
						return false;
					}
					binaryMessageEncodingBindingElement = (bindingElement as BinaryMessageEncodingBindingElement);
				}
			}
			UnifiedSecurityMode mode;
			if (!NetMsmqBinding.IsValidTransport(msmqTransportBindingElement, out mode))
			{
				return false;
			}
			if (binaryMessageEncodingBindingElement == null)
			{
				return false;
			}
			NetMsmqSecurity netMsmqSecurity;
			if (!NetMsmqBinding.TryCreateSecurity(sbe, mode, out netMsmqSecurity))
			{
				return false;
			}
			NetMsmqBinding netMsmqBinding = new NetMsmqBinding(netMsmqSecurity);
			netMsmqBinding.InitializeFrom(msmqTransportBindingElement, binaryMessageEncodingBindingElement);
			if (!netMsmqBinding.IsBindingElementsMatch(msmqTransportBindingElement, binaryMessageEncodingBindingElement))
			{
				return false;
			}
			binding = netMsmqBinding;
			return true;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00024A98 File Offset: 0x00022C98
		private SecurityBindingElement CreateMessageSecurity()
		{
			if (this.security.Mode == NetMsmqSecurityMode.Message || this.security.Mode == NetMsmqSecurityMode.Both)
			{
				return this.security.CreateMessageSecurity();
			}
			return null;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00024AC4 File Offset: 0x00022CC4
		private static bool TryCreateSecurity(SecurityBindingElement sbe, UnifiedSecurityMode mode, out NetMsmqSecurity security)
		{
			if (sbe != null)
			{
				mode &= (UnifiedSecurityMode.Message | UnifiedSecurityMode.Both);
			}
			else
			{
				mode &= ~(UnifiedSecurityMode.Message | UnifiedSecurityMode.Both);
			}
			NetMsmqSecurityMode mode2 = NetMsmqSecurityModeHelper.ToSecurityMode(mode);
			return NetMsmqSecurity.TryCreate(sbe, mode2, out security);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00024AF6 File Offset: 0x00022CF6
		private System.ServiceModel.Channels.MsmqBindingElementBase GetTransport()
		{
			this.security.ConfigureTransportSecurity(this.transport);
			return this.transport;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00024B0F File Offset: 0x00022D0F
		private static bool IsValidTransport(MsmqTransportBindingElement msmq, out UnifiedSecurityMode mode)
		{
			mode = (UnifiedSecurityMode)0;
			return msmq != null && NetMsmqSecurity.IsConfiguredTransportSecurity(msmq, out mode);
		}

		// Token: 0x04000B65 RID: 2917
		private BinaryMessageEncodingBindingElement encoding;

		// Token: 0x04000B66 RID: 2918
		private NetMsmqSecurity security;
	}
}
