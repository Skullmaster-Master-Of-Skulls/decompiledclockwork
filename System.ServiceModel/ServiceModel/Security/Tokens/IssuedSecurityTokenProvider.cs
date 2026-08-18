using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.ComIntegration;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200039A RID: 922
	public class IssuedSecurityTokenProvider : SecurityTokenProvider, ICommunicationObject
	{
		// Token: 0x06002228 RID: 8744 RVA: 0x0007D289 File Offset: 0x0007B489
		public IssuedSecurityTokenProvider() : this(null)
		{
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0007D292 File Offset: 0x0007B492
		internal IssuedSecurityTokenProvider(SafeFreeCredentials credentialsHandle)
		{
			this.federatedTokenProvider = new IssuedSecurityTokenProvider.CoreFederatedTokenProvider(credentialsHandle);
			this.messageSecurityVersion = MessageSecurityVersion.Default;
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600222A RID: 8746 RVA: 0x0007D2B1 File Offset: 0x0007B4B1
		// (remove) Token: 0x0600222B RID: 8747 RVA: 0x0007D2BF File Offset: 0x0007B4BF
		public event EventHandler Closed
		{
			add
			{
				this.federatedTokenProvider.Closed += value;
			}
			remove
			{
				this.federatedTokenProvider.Closed -= value;
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600222C RID: 8748 RVA: 0x0007D2CD File Offset: 0x0007B4CD
		// (remove) Token: 0x0600222D RID: 8749 RVA: 0x0007D2DB File Offset: 0x0007B4DB
		public event EventHandler Closing
		{
			add
			{
				this.federatedTokenProvider.Closing += value;
			}
			remove
			{
				this.federatedTokenProvider.Closing -= value;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600222E RID: 8750 RVA: 0x0007D2E9 File Offset: 0x0007B4E9
		// (remove) Token: 0x0600222F RID: 8751 RVA: 0x0007D2F7 File Offset: 0x0007B4F7
		public event EventHandler Faulted
		{
			add
			{
				this.federatedTokenProvider.Faulted += value;
			}
			remove
			{
				this.federatedTokenProvider.Faulted -= value;
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002230 RID: 8752 RVA: 0x0007D305 File Offset: 0x0007B505
		// (remove) Token: 0x06002231 RID: 8753 RVA: 0x0007D313 File Offset: 0x0007B513
		public event EventHandler Opened
		{
			add
			{
				this.federatedTokenProvider.Opened += value;
			}
			remove
			{
				this.federatedTokenProvider.Opened -= value;
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002232 RID: 8754 RVA: 0x0007D321 File Offset: 0x0007B521
		// (remove) Token: 0x06002233 RID: 8755 RVA: 0x0007D32F File Offset: 0x0007B52F
		public event EventHandler Opening
		{
			add
			{
				this.federatedTokenProvider.Opening += value;
			}
			remove
			{
				this.federatedTokenProvider.Opening -= value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0007D33D File Offset: 0x0007B53D
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x0007D34A File Offset: 0x0007B54A
		public Binding IssuerBinding
		{
			get
			{
				return this.federatedTokenProvider.IssuerBinding;
			}
			set
			{
				this.federatedTokenProvider.IssuerBinding = value;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x0007D358 File Offset: 0x0007B558
		public KeyedByTypeCollection<IEndpointBehavior> IssuerChannelBehaviors
		{
			get
			{
				return this.federatedTokenProvider.IssuerChannelBehaviors;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0007D365 File Offset: 0x0007B565
		public Collection<XmlElement> TokenRequestParameters
		{
			get
			{
				return this.federatedTokenProvider.RequestProperties;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0007D372 File Offset: 0x0007B572
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x0007D37F File Offset: 0x0007B57F
		public EndpointAddress IssuerAddress
		{
			get
			{
				return this.federatedTokenProvider.IssuerAddress;
			}
			set
			{
				this.federatedTokenProvider.IssuerAddress = value;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0007D38D File Offset: 0x0007B58D
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x0007D39A File Offset: 0x0007B59A
		public EndpointAddress TargetAddress
		{
			get
			{
				return this.federatedTokenProvider.TargetAddress;
			}
			set
			{
				this.federatedTokenProvider.TargetAddress = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007D3A8 File Offset: 0x0007B5A8
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x0007D3B5 File Offset: 0x0007B5B5
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return this.federatedTokenProvider.KeyEntropyMode;
			}
			set
			{
				this.federatedTokenProvider.KeyEntropyMode = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x0007D3C3 File Offset: 0x0007B5C3
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x0007D3D0 File Offset: 0x0007B5D0
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.federatedTokenProvider.IdentityVerifier;
			}
			set
			{
				this.federatedTokenProvider.IdentityVerifier = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0007D3DE File Offset: 0x0007B5DE
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x0007D3EB File Offset: 0x0007B5EB
		public bool CacheIssuedTokens
		{
			get
			{
				return this.federatedTokenProvider.CacheServiceTokens;
			}
			set
			{
				this.federatedTokenProvider.CacheServiceTokens = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0007D3F9 File Offset: 0x0007B5F9
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0007D406 File Offset: 0x0007B606
		public TimeSpan MaxIssuedTokenCachingTime
		{
			get
			{
				return this.federatedTokenProvider.MaxServiceTokenCachingTime;
			}
			set
			{
				this.federatedTokenProvider.MaxServiceTokenCachingTime = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0007D414 File Offset: 0x0007B614
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x0007D41C File Offset: 0x0007B61C
		public MessageSecurityVersion MessageSecurityVersion
		{
			get
			{
				return this.messageSecurityVersion;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.messageSecurityVersion = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0007D43D File Offset: 0x0007B63D
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x0007D445 File Offset: 0x0007B645
		public SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this.securityTokenSerializer;
			}
			set
			{
				this.securityTokenSerializer = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0007D44E File Offset: 0x0007B64E
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x0007D45B File Offset: 0x0007B65B
		public SecurityAlgorithmSuite SecurityAlgorithmSuite
		{
			get
			{
				return this.federatedTokenProvider.SecurityAlgorithmSuite;
			}
			set
			{
				this.federatedTokenProvider.SecurityAlgorithmSuite = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0007D469 File Offset: 0x0007B669
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x0007D476 File Offset: 0x0007B676
		public int IssuedTokenRenewalThresholdPercentage
		{
			get
			{
				return this.federatedTokenProvider.ServiceTokenValidityThresholdPercentage;
			}
			set
			{
				this.federatedTokenProvider.ServiceTokenValidityThresholdPercentage = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0007D484 File Offset: 0x0007B684
		public CommunicationState State
		{
			get
			{
				return this.federatedTokenProvider.State;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0007D491 File Offset: 0x0007B691
		public virtual TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0007D498 File Offset: 0x0007B698
		public virtual TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0007D49F File Offset: 0x0007B69F
		public override bool SupportsTokenCancellation
		{
			get
			{
				return this.federatedTokenProvider.SupportsTokenCancellation;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007D4AC File Offset: 0x0007B6AC
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0007D4B9 File Offset: 0x0007B6B9
		internal ChannelParameterCollection ChannelParameters
		{
			get
			{
				return this.federatedTokenProvider.ChannelParameters;
			}
			set
			{
				this.federatedTokenProvider.ChannelParameters = value;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0007D4C7 File Offset: 0x0007B6C7
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x0007D4CF File Offset: 0x0007B6CF
		internal SecurityTokenHandlerCollectionManager TokenHandlerCollectionManager
		{
			get
			{
				return this.tokenHandlerCollectionManager;
			}
			set
			{
				this.tokenHandlerCollectionManager = value;
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0007D4D8 File Offset: 0x0007B6D8
		public void Abort()
		{
			this.federatedTokenProvider.Abort();
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0007D4E5 File Offset: 0x0007B6E5
		public void Close()
		{
			this.federatedTokenProvider.Close();
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0007D4F2 File Offset: 0x0007B6F2
		public void Close(TimeSpan timeout)
		{
			this.federatedTokenProvider.Close(timeout);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0007D500 File Offset: 0x0007B700
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.federatedTokenProvider.BeginClose(callback, state);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0007D50F File Offset: 0x0007B70F
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.federatedTokenProvider.BeginClose(timeout, callback, state);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0007D51F File Offset: 0x0007B71F
		public void EndClose(IAsyncResult result)
		{
			this.federatedTokenProvider.EndClose(result);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0007D52D File Offset: 0x0007B72D
		private void OnOpenCore()
		{
			if (this.securityTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TokenSerializerNotSetonFederationProvider")));
			}
			this.federatedTokenProvider.StandardsManager = new SecurityStandardsManager(this.messageSecurityVersion, this.securityTokenSerializer);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0007D56D File Offset: 0x0007B76D
		public void Open()
		{
			this.OnOpenCore();
			this.federatedTokenProvider.Open();
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0007D580 File Offset: 0x0007B780
		public void Open(TimeSpan timeout)
		{
			this.OnOpenCore();
			this.federatedTokenProvider.Open(timeout);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0007D594 File Offset: 0x0007B794
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			this.OnOpenCore();
			return this.federatedTokenProvider.BeginOpen(callback, state);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007D5A9 File Offset: 0x0007B7A9
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpenCore();
			return this.federatedTokenProvider.BeginOpen(timeout, callback, state);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0007D5BF File Offset: 0x0007B7BF
		public void EndOpen(IAsyncResult result)
		{
			this.federatedTokenProvider.EndOpen(result);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0007D5CD File Offset: 0x0007B7CD
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0007D5D5 File Offset: 0x0007B7D5
		protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.federatedTokenProvider.BeginGetToken(timeout, callback, state);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0007D5E5 File Offset: 0x0007B7E5
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return this.federatedTokenProvider.GetToken(timeout);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0007D5F3 File Offset: 0x0007B7F3
		protected override SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			return this.federatedTokenProvider.EndGetToken(result);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0007D604 File Offset: 0x0007B804
		internal void SetupActAsOnBehalfOfParameters(FederatedClientCredentialsParameters actAsOnBehalfOfParameters)
		{
			if (actAsOnBehalfOfParameters == null)
			{
				return;
			}
			if (actAsOnBehalfOfParameters.IssuedSecurityToken != null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("AuthFailed"));
			}
			if (actAsOnBehalfOfParameters.OnBehalfOf != null)
			{
				if (this.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					if (this.TokenRequestParameterExists("OnBehalfOf", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("DuplicateFederatedClientCredentialsParameters", new object[]
						{
							"OnBehalfOf"
						}));
					}
					this.TokenRequestParameters.Add(this.CreateXmlTokenElement(actAsOnBehalfOfParameters.OnBehalfOf, "trust", "OnBehalfOf", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", "OnBehalfOf"));
				}
				else
				{
					if (this.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrustFeb2005)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedTrustVersion", new object[]
						{
							this.MessageSecurityVersion.TrustVersion.Namespace
						})));
					}
					if (this.TokenRequestParameterExists("OnBehalfOf", "http://schemas.xmlsoap.org/ws/2005/02/trust"))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("DuplicateFederatedClientCredentialsParameters", new object[]
						{
							"OnBehalfOf"
						}));
					}
					this.TokenRequestParameters.Add(this.CreateXmlTokenElement(actAsOnBehalfOfParameters.OnBehalfOf, "t", "OnBehalfOf", "http://schemas.xmlsoap.org/ws/2005/02/trust", "OnBehalfOf"));
				}
			}
			if (actAsOnBehalfOfParameters.ActAs != null)
			{
				if (this.TokenRequestParameterExists("ActAs", "http://docs.oasis-open.org/ws-sx/ws-trust/200802"))
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("DuplicateFederatedClientCredentialsParameters", new object[]
					{
						"ActAs"
					}));
				}
				this.TokenRequestParameters.Add(this.CreateXmlTokenElement(actAsOnBehalfOfParameters.ActAs, "tr", "ActAs", "http://docs.oasis-open.org/ws-sx/ws-trust/200802", "ActAs"));
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007D7B0 File Offset: 0x0007B9B0
		private bool TokenRequestParameterExists(string localName, string xmlNamespace)
		{
			foreach (XmlElement xmlElement in this.TokenRequestParameters)
			{
				if (xmlElement.LocalName == localName && xmlElement.NamespaceURI == xmlNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0007D81C File Offset: 0x0007BA1C
		private XmlElement CreateXmlTokenElement(SecurityToken token, string prefix, string name, string ns, string usage)
		{
			Stream stream = new MemoryStream();
			using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, false))
			{
				xmlDictionaryWriter.WriteStartElement(prefix, name, ns);
				this.WriteToken(xmlDictionaryWriter, token, usage);
				xmlDictionaryWriter.WriteEndElement();
				xmlDictionaryWriter.Flush();
			}
			stream.Seek(0L, SeekOrigin.Begin);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(new XmlTextReader(stream)
			{
				DtdProcessing = DtdProcessing.Prohibit
			});
			stream.Close();
			return xmlDocument.DocumentElement;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		private void WriteToken(XmlWriter xmlWriter, SecurityToken token, string usage)
		{
			SecurityTokenHandlerCollection securityTokenHandlerCollection;
			if (this.tokenHandlerCollectionManager.ContainsKey(usage))
			{
				securityTokenHandlerCollection = this.tokenHandlerCollectionManager[usage];
			}
			else
			{
				securityTokenHandlerCollection = this.tokenHandlerCollectionManager[""];
			}
			if (securityTokenHandlerCollection != null && securityTokenHandlerCollection.CanWriteToken(token))
			{
				securityTokenHandlerCollection.WriteToken(xmlWriter, token);
				return;
			}
			this.SecurityTokenSerializer.WriteToken(xmlWriter, token);
		}

		// Token: 0x04001F9C RID: 8092
		private IssuedSecurityTokenProvider.CoreFederatedTokenProvider federatedTokenProvider;

		// Token: 0x04001F9D RID: 8093
		private MessageSecurityVersion messageSecurityVersion;

		// Token: 0x04001F9E RID: 8094
		private SecurityTokenSerializer securityTokenSerializer;

		// Token: 0x04001F9F RID: 8095
		private SecurityTokenHandlerCollectionManager tokenHandlerCollectionManager;

		// Token: 0x02000B9B RID: 2971
		private class CoreFederatedTokenProvider : IssuanceTokenProviderBase<IssuedSecurityTokenProvider.FederatedTokenProviderState>
		{
			// Token: 0x0600736E RID: 29550 RVA: 0x001AE9A8 File Offset: 0x001ACBA8
			public CoreFederatedTokenProvider(SafeFreeCredentials credentialsHandle)
			{
				this.credentialsHandle = credentialsHandle;
				this.channelBehaviors = new KeyedByTypeCollection<IEndpointBehavior>();
				this.addTargetServiceAppliesTo = true;
				this.keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
			}

			// Token: 0x17001AC3 RID: 6851
			// (get) Token: 0x0600736F RID: 29551 RVA: 0x001AEA07 File Offset: 0x001ACC07
			// (set) Token: 0x06007370 RID: 29552 RVA: 0x001AEA0F File Offset: 0x001ACC0F
			public Binding IssuerBinding
			{
				get
				{
					return this.issuerBinding;
				}
				set
				{
					base.CommunicationObject.ThrowIfDisposedOrImmutable();
					this.issuerBinding = value;
				}
			}

			// Token: 0x17001AC4 RID: 6852
			// (get) Token: 0x06007371 RID: 29553 RVA: 0x001AEA23 File Offset: 0x001ACC23
			public Collection<XmlElement> RequestProperties
			{
				get
				{
					return this.requestProperties;
				}
			}

			// Token: 0x17001AC5 RID: 6853
			// (get) Token: 0x06007372 RID: 29554 RVA: 0x001AEA2B File Offset: 0x001ACC2B
			// (set) Token: 0x06007373 RID: 29555 RVA: 0x001AEA33 File Offset: 0x001ACC33
			public SecurityKeyEntropyMode KeyEntropyMode
			{
				get
				{
					return this.keyEntropyMode;
				}
				set
				{
					base.CommunicationObject.ThrowIfDisposedOrImmutable();
					SecurityKeyEntropyModeHelper.Validate(value);
					this.keyEntropyMode = value;
				}
			}

			// Token: 0x17001AC6 RID: 6854
			// (get) Token: 0x06007374 RID: 29556 RVA: 0x001AEA4D File Offset: 0x001ACC4D
			// (set) Token: 0x06007375 RID: 29557 RVA: 0x001AEA55 File Offset: 0x001ACC55
			public IdentityVerifier IdentityVerifier
			{
				get
				{
					return this.identityVerifier;
				}
				set
				{
					base.CommunicationObject.ThrowIfDisposedOrImmutable();
					this.identityVerifier = value;
				}
			}

			// Token: 0x17001AC7 RID: 6855
			// (get) Token: 0x06007376 RID: 29558 RVA: 0x001AEA69 File Offset: 0x001ACC69
			// (set) Token: 0x06007377 RID: 29559 RVA: 0x001AEA71 File Offset: 0x001ACC71
			public ChannelParameterCollection ChannelParameters
			{
				get
				{
					return this.channelParameters;
				}
				set
				{
					base.CommunicationObject.ThrowIfDisposedOrImmutable();
					this.channelParameters = value;
				}
			}

			// Token: 0x17001AC8 RID: 6856
			// (get) Token: 0x06007378 RID: 29560 RVA: 0x001AEA85 File Offset: 0x001ACC85
			public KeyedByTypeCollection<IEndpointBehavior> IssuerChannelBehaviors
			{
				get
				{
					return this.channelBehaviors;
				}
			}

			// Token: 0x17001AC9 RID: 6857
			// (get) Token: 0x06007379 RID: 29561 RVA: 0x001AEA8D File Offset: 0x001ACC8D
			public override XmlDictionaryString RequestSecurityTokenAction
			{
				get
				{
					return base.StandardsManager.TrustDriver.RequestSecurityTokenAction;
				}
			}

			// Token: 0x17001ACA RID: 6858
			// (get) Token: 0x0600737A RID: 29562 RVA: 0x001AEA9F File Offset: 0x001ACC9F
			public override XmlDictionaryString RequestSecurityTokenResponseAction
			{
				get
				{
					return base.StandardsManager.TrustDriver.RequestSecurityTokenResponseAction;
				}
			}

			// Token: 0x17001ACB RID: 6859
			// (get) Token: 0x0600737B RID: 29563 RVA: 0x001AEAB1 File Offset: 0x001ACCB1
			protected override MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x17001ACC RID: 6860
			// (get) Token: 0x0600737C RID: 29564 RVA: 0x001AEAB9 File Offset: 0x001ACCB9
			protected override bool RequiresManualReplyAddressing
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600737D RID: 29565 RVA: 0x001AEABC File Offset: 0x001ACCBC
			private bool TryGetKeyType(out SecurityKeyType keyType)
			{
				if (this.requestProperties != null)
				{
					for (int i = 0; i < this.requestProperties.Count; i++)
					{
						if (base.StandardsManager.TrustDriver.TryParseKeyTypeElement(this.requestProperties[i], out keyType))
						{
							return true;
						}
					}
				}
				keyType = SecurityKeyType.SymmetricKey;
				return false;
			}

			// Token: 0x0600737E RID: 29566 RVA: 0x001AEB0C File Offset: 0x001ACD0C
			private bool TryGetKeySize(out int keySize)
			{
				if (this.requestProperties != null)
				{
					for (int i = 0; i < this.requestProperties.Count; i++)
					{
						if (base.StandardsManager.TrustDriver.TryParseKeySizeElement(this.requestProperties[i], out keySize))
						{
							return true;
						}
					}
				}
				keySize = 0;
				return false;
			}

			// Token: 0x0600737F RID: 29567 RVA: 0x001AEB5C File Offset: 0x001ACD5C
			public override void OnOpen(TimeSpan timeout)
			{
				if (base.IssuerAddress == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StsAddressNotSet", new object[]
					{
						base.TargetAddress
					})));
				}
				if (this.IssuerBinding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StsBindingNotSet", new object[]
					{
						base.IssuerAddress
					})));
				}
				if (base.SecurityAlgorithmSuite == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityAlgorithmSuiteNotSet", new object[]
					{
						typeof(IssuedSecurityTokenProvider)
					})));
				}
				this.channelFactory = base.StandardsManager.TrustDriver.CreateFederationProxy(base.IssuerAddress, this.IssuerBinding, this.IssuerChannelBehaviors);
				this.messageVersion = this.IssuerBinding.MessageVersion;
				for (int i = 0; i < this.requestProperties.Count; i++)
				{
					if (base.StandardsManager.TrustDriver.IsAppliesTo(this.requestProperties[i].LocalName, this.requestProperties[i].NamespaceURI))
					{
						this.addTargetServiceAppliesTo = false;
						break;
					}
				}
				this.isKeyTypePresentInRstProperties = this.TryGetKeyType(out this.keyType);
				if (!this.isKeyTypePresentInRstProperties)
				{
					this.keyType = SecurityKeyType.SymmetricKey;
				}
				this.isKeySizePresentInRstProperties = this.TryGetKeySize(out this.keySize);
				if (!this.isKeySizePresentInRstProperties && this.keyType != SecurityKeyType.BearerKey)
				{
					this.keySize = ((this.keyType == SecurityKeyType.SymmetricKey) ? base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength : this.defaultPublicKeySize);
				}
				base.OnOpen(timeout);
			}

			// Token: 0x06007380 RID: 29568 RVA: 0x001AED00 File Offset: 0x001ACF00
			public override void OnOpening()
			{
				base.OnOpening();
				if (this.credentialsHandle == null)
				{
					if (this.IssuerBinding == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StsBindingNotSet", new object[]
						{
							base.IssuerAddress
						})));
					}
					this.credentialsHandle = SecurityUtils.GetCredentialsHandle(this.IssuerBinding, this.IssuerChannelBehaviors);
					this.ownCredentialsHandle = true;
				}
			}

			// Token: 0x06007381 RID: 29569 RVA: 0x001AED6A File Offset: 0x001ACF6A
			public override void OnAbort()
			{
				if (this.channelFactory != null && this.channelFactory.State == CommunicationState.Opened)
				{
					this.channelFactory.Abort();
					this.channelFactory = null;
				}
				this.CleanUpRsaSecurityTokenCache();
				this.FreeCredentialsHandle();
				base.OnAbort();
			}

			// Token: 0x06007382 RID: 29570 RVA: 0x001AEDA8 File Offset: 0x001ACFA8
			public override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.channelFactory != null && this.channelFactory.State == CommunicationState.Opened)
				{
					this.channelFactory.Close(timeoutHelper.RemainingTime());
					this.channelFactory = null;
					this.CleanUpRsaSecurityTokenCache();
					this.FreeCredentialsHandle();
					base.OnClose(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x06007383 RID: 29571 RVA: 0x001AEE05 File Offset: 0x001AD005
			private void FreeCredentialsHandle()
			{
				if (this.credentialsHandle != null)
				{
					if (this.ownCredentialsHandle)
					{
						this.credentialsHandle.Close();
					}
					this.credentialsHandle = null;
				}
			}

			// Token: 0x06007384 RID: 29572 RVA: 0x001AEE29 File Offset: 0x001AD029
			protected override bool WillInitializeChannelFactoriesCompleteSynchronously(EndpointAddress target)
			{
				return this.channelFactory.State != CommunicationState.Opened;
			}

			// Token: 0x06007385 RID: 29573 RVA: 0x001AEE3C File Offset: 0x001AD03C
			protected override void InitializeChannelFactories(EndpointAddress target, TimeSpan timeout)
			{
				if (this.channelFactory.State == CommunicationState.Created)
				{
					this.channelFactory.Open(timeout);
				}
			}

			// Token: 0x06007386 RID: 29574 RVA: 0x001AEE57 File Offset: 0x001AD057
			protected override IAsyncResult BeginInitializeChannelFactories(EndpointAddress target, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (this.channelFactory.State == CommunicationState.Created)
				{
					return this.channelFactory.BeginOpen(timeout, callback, state);
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007387 RID: 29575 RVA: 0x001AEE7E File Offset: 0x001AD07E
			protected override void EndInitializeChannelFactories(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				this.channelFactory.EndOpen(result);
			}

			// Token: 0x06007388 RID: 29576 RVA: 0x001AEE9C File Offset: 0x001AD09C
			protected override IRequestChannel CreateClientChannel(EndpointAddress target, Uri via)
			{
				IRequestChannel requestChannel = this.channelFactory.CreateChannel(base.IssuerAddress);
				if (this.channelParameters != null)
				{
					this.channelParameters.PropagateChannelParameters(requestChannel);
				}
				if (this.ownCredentialsHandle)
				{
					ChannelParameterCollection property = requestChannel.GetProperty<ChannelParameterCollection>();
					if (property != null)
					{
						property.Add(new SspiIssuanceChannelParameter(true, this.credentialsHandle));
					}
				}
				this.ReplaceSspiIssuanceChannelParameter(requestChannel.GetProperty<ChannelParameterCollection>(), new SspiIssuanceChannelParameter(true, this.credentialsHandle));
				return requestChannel;
			}

			// Token: 0x06007389 RID: 29577 RVA: 0x001AEF0C File Offset: 0x001AD10C
			private void ReplaceSspiIssuanceChannelParameter(ChannelParameterCollection channelParameters, SspiIssuanceChannelParameter sicp)
			{
				if (channelParameters != null)
				{
					for (int i = 0; i < channelParameters.Count; i++)
					{
						if (channelParameters[i] is SspiIssuanceChannelParameter)
						{
							channelParameters.RemoveAt(i);
						}
					}
					channelParameters.Add(sicp);
				}
			}

			// Token: 0x0600738A RID: 29578 RVA: 0x001AEF49 File Offset: 0x001AD149
			protected override bool CreateNegotiationStateCompletesSynchronously(EndpointAddress target, Uri via)
			{
				return true;
			}

			// Token: 0x0600738B RID: 29579 RVA: 0x001AEF4C File Offset: 0x001AD14C
			protected override IAsyncResult BeginCreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult<IssuedSecurityTokenProvider.FederatedTokenProviderState>(this.CreateNegotiationState(target, via, timeout), callback, state);
			}

			// Token: 0x0600738C RID: 29580 RVA: 0x001AEF60 File Offset: 0x001AD160
			protected override IssuedSecurityTokenProvider.FederatedTokenProviderState EndCreateNegotiationState(IAsyncResult result)
			{
				return CompletedAsyncResult<IssuedSecurityTokenProvider.FederatedTokenProviderState>.End(result);
			}

			// Token: 0x0600738D RID: 29581 RVA: 0x001AEF68 File Offset: 0x001AD168
			protected override IssuedSecurityTokenProvider.FederatedTokenProviderState CreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout)
			{
				if (this.keyType == SecurityKeyType.SymmetricKey || this.keyType == SecurityKeyType.BearerKey)
				{
					byte[] array;
					if (this.KeyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy || this.KeyEntropyMode == SecurityKeyEntropyMode.ClientEntropy)
					{
						array = new byte[this.keySize / 8];
						CryptoHelper.FillRandomBytes(array);
					}
					else
					{
						array = null;
					}
					return new IssuedSecurityTokenProvider.FederatedTokenProviderState(array);
				}
				if (this.keyType == SecurityKeyType.AsymmetricKey)
				{
					return new IssuedSecurityTokenProvider.FederatedTokenProviderState(this.CreateAndCacheRsaSecurityToken());
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x0600738E RID: 29582 RVA: 0x001AEFDC File Offset: 0x001AD1DC
			protected override BodyWriter GetFirstOutgoingMessageBody(IssuedSecurityTokenProvider.FederatedTokenProviderState negotiationState, out MessageProperties messageProperties)
			{
				messageProperties = null;
				RequestSecurityToken requestSecurityToken = new RequestSecurityToken(base.StandardsManager);
				if (this.addTargetServiceAppliesTo)
				{
					if (this.MessageVersion.Addressing == AddressingVersion.WSAddressing10)
					{
						requestSecurityToken.SetAppliesTo<EndpointAddress10>(EndpointAddress10.FromEndpointAddress(negotiationState.TargetAddress), DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddress10), int.MaxValue));
					}
					else
					{
						if (this.MessageVersion.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								this.MessageVersion.Addressing
							})));
						}
						requestSecurityToken.SetAppliesTo<EndpointAddressAugust2004>(EndpointAddressAugust2004.FromEndpointAddress(negotiationState.TargetAddress), DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddressAugust2004), int.MaxValue));
					}
				}
				requestSecurityToken.Context = negotiationState.Context;
				if (!this.isKeySizePresentInRstProperties)
				{
					requestSecurityToken.KeySize = this.keySize;
				}
				Collection<XmlElement> collection = new Collection<XmlElement>();
				if (this.requestProperties != null)
				{
					for (int i = 0; i < this.requestProperties.Count; i++)
					{
						collection.Add(this.requestProperties[i]);
					}
				}
				if (!this.isKeyTypePresentInRstProperties)
				{
					XmlElement item = base.StandardsManager.TrustDriver.CreateKeyTypeElement(this.keyType);
					collection.Insert(0, item);
				}
				if (this.keyType == SecurityKeyType.SymmetricKey)
				{
					byte[] requestorEntropy = negotiationState.GetRequestorEntropy();
					requestSecurityToken.SetRequestorEntropy(requestorEntropy);
				}
				else if (this.keyType == SecurityKeyType.AsymmetricKey)
				{
					RsaKeyIdentifierClause rsaKeyIdentifierClause = new RsaKeyIdentifierClause(negotiationState.Rsa);
					SecurityKeyIdentifier keyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
					{
						rsaKeyIdentifierClause
					});
					collection.Add(base.StandardsManager.TrustDriver.CreateUseKeyElement(keyIdentifier, base.StandardsManager));
					RsaSecurityTokenParameters rsaSecurityTokenParameters = new RsaSecurityTokenParameters();
					rsaSecurityTokenParameters.InclusionMode = SecurityTokenInclusionMode.Never;
					rsaSecurityTokenParameters.RequireDerivedKeys = false;
					SupportingTokenSpecification item2 = new SupportingTokenSpecification(negotiationState.RsaSecurityToken, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, SecurityTokenAttachmentMode.Endorsing, rsaSecurityTokenParameters);
					messageProperties = new MessageProperties();
					SecurityMessageProperty securityMessageProperty = new SecurityMessageProperty();
					securityMessageProperty.OutgoingSupportingTokens.Add(item2);
					messageProperties.Security = securityMessageProperty;
				}
				if (this.keyType == SecurityKeyType.SymmetricKey && this.KeyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy)
				{
					collection.Add(base.StandardsManager.TrustDriver.CreateComputedKeyAlgorithmElement(base.StandardsManager.TrustDriver.ComputedKeyAlgorithm));
				}
				requestSecurityToken.RequestProperties = collection;
				requestSecurityToken.MakeReadOnly();
				return requestSecurityToken;
			}

			// Token: 0x0600738F RID: 29583 RVA: 0x001AF21C File Offset: 0x001AD41C
			protected ReadOnlyCollection<IAuthorizationPolicy> GetServiceAuthorizationPolicies(AcceleratedTokenProviderState negotiationState)
			{
				EndpointIdentity endpointIdentity;
				if (this.identityVerifier.TryGetIdentity(negotiationState.TargetAddress, out endpointIdentity))
				{
					List<Claim> list = new List<Claim>(1);
					list.Add(endpointIdentity.IdentityClaim);
					return new List<IAuthorizationPolicy>(1)
					{
						new UnconditionalPolicy(SecurityUtils.CreateIdentity(endpointIdentity.IdentityClaim.Resource.ToString()), new DefaultClaimSet(ClaimSet.System, list))
					}.AsReadOnly();
				}
				return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}

			// Token: 0x06007390 RID: 29584 RVA: 0x001AF290 File Offset: 0x001AD490
			protected override BodyWriter GetNextOutgoingMessageBody(Message incomingMessage, IssuedSecurityTokenProvider.FederatedTokenProviderState negotiationState)
			{
				IssuanceTokenProviderBase<IssuedSecurityTokenProvider.FederatedTokenProviderState>.ThrowIfFault(incomingMessage, base.IssuerAddress);
				if ((base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005 && incomingMessage.Headers.Action != base.StandardsManager.TrustDriver.RequestSecurityTokenResponseAction.Value) || (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13 && incomingMessage.Headers.Action != base.StandardsManager.TrustDriver.RequestSecurityTokenResponseFinalAction.Value) || incomingMessage.Headers.Action == null)
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidActionForNegotiationMessage", new object[]
					{
						incomingMessage.Headers.Action
					})), incomingMessage);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse = null;
				XmlDictionaryReader readerAtBodyContents = incomingMessage.GetReaderAtBodyContents();
				using (readerAtBodyContents)
				{
					if (base.StandardsManager.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrustFeb2005)
					{
						if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
						{
							RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = base.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(readerAtBodyContents);
							using (IEnumerator<RequestSecurityTokenResponse> enumerator = requestSecurityTokenResponseCollection.RstrCollection.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									RequestSecurityTokenResponse requestSecurityTokenResponse2 = enumerator.Current;
									if (requestSecurityTokenResponse != null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MoreThanOneRSTRInRSTRC")));
									}
									requestSecurityTokenResponse = requestSecurityTokenResponse2;
								}
								goto IL_182;
							}
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					requestSecurityTokenResponse = base.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponse(readerAtBodyContents);
					IL_182:
					incomingMessage.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (requestSecurityTokenResponse.Context != negotiationState.Context)
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("BadSecurityNegotiationContext")), incomingMessage);
				}
				GenericXmlSecurityToken issuedToken;
				if (this.keyType == SecurityKeyType.SymmetricKey || this.keyType == SecurityKeyType.BearerKey)
				{
					ReadOnlyCollection<IAuthorizationPolicy> serviceAuthorizationPolicies = this.GetServiceAuthorizationPolicies(negotiationState);
					byte[] requestorEntropy = negotiationState.GetRequestorEntropy();
					issuedToken = requestSecurityTokenResponse.GetIssuedToken(null, null, this.KeyEntropyMode, requestorEntropy, null, serviceAuthorizationPolicies, this.keySize, this.keyType == SecurityKeyType.BearerKey);
				}
				else
				{
					if (this.keyType != SecurityKeyType.AsymmetricKey)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					issuedToken = requestSecurityTokenResponse.GetIssuedToken(null, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, negotiationState.Rsa);
				}
				negotiationState.SetServiceToken(issuedToken);
				return null;
			}

			// Token: 0x17001ACD RID: 6861
			// (get) Token: 0x06007391 RID: 29585 RVA: 0x001AF4F4 File Offset: 0x001AD6F4
			protected override bool IsMultiLegNegotiation
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007392 RID: 29586 RVA: 0x001AF4F8 File Offset: 0x001AD6F8
			private RsaSecurityToken CreateAndCacheRsaSecurityToken()
			{
				RsaSecurityToken rsaSecurityToken;
				if (IssuedSecurityTokenProvider.CoreFederatedTokenProvider.MaxRsaSecurityTokenCacheSize >= 0 && this.IsImpersonatedContext())
				{
					rsaSecurityToken = RsaSecurityToken.CreateSafeRsaSecurityToken(this.keySize);
					if (IssuedSecurityTokenProvider.CoreFederatedTokenProvider.MaxRsaSecurityTokenCacheSize <= 0)
					{
						return rsaSecurityToken;
					}
					List<RsaSecurityToken> obj = this.rsaSecurityTokens;
					lock (obj)
					{
						if (this.rsaSecurityTokens.Count >= IssuedSecurityTokenProvider.CoreFederatedTokenProvider.MaxRsaSecurityTokenCacheSize)
						{
							this.rsaSecurityTokens.RemoveAt(0);
						}
						this.rsaSecurityTokens.Add(rsaSecurityToken);
						return rsaSecurityToken;
					}
				}
				rsaSecurityToken = new RsaSecurityToken(new RSACryptoServiceProvider(this.keySize));
				return rsaSecurityToken;
			}

			// Token: 0x06007393 RID: 29587 RVA: 0x001AF594 File Offset: 0x001AD794
			private void CleanUpRsaSecurityTokenCache()
			{
				List<RsaSecurityToken> obj = this.rsaSecurityTokens;
				lock (obj)
				{
					for (int i = 0; i < this.rsaSecurityTokens.Count; i++)
					{
						this.rsaSecurityTokens[i].Dispose();
					}
					this.rsaSecurityTokens.Clear();
				}
			}

			// Token: 0x06007394 RID: 29588 RVA: 0x001AF600 File Offset: 0x001AD800
			private bool IsImpersonatedContext()
			{
				SafeCloseHandle safeCloseHandle = null;
				if (System.ServiceModel.ComIntegration.SafeNativeMethods.OpenCurrentThreadToken(System.ServiceModel.ComIntegration.SafeNativeMethods.GetCurrentThread(), TokenAccessLevels.Query, true, out safeCloseHandle))
				{
					safeCloseHandle.Close();
					return true;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
				if (lastWin32Error == 1008)
				{
					return false;
				}
				ErrorBehavior.ThrowAndCatch(new Win32Exception(lastWin32Error));
				return true;
			}

			// Token: 0x06007395 RID: 29589 RVA: 0x001AF649 File Offset: 0x001AD849
			protected override void ValidateKeySize(GenericXmlSecurityToken issuedToken)
			{
				if (this.keyType == SecurityKeyType.BearerKey)
				{
					return;
				}
				base.ValidateKeySize(issuedToken);
			}

			// Token: 0x0400415F RID: 16735
			internal const SecurityKeyEntropyMode defaultKeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;

			// Token: 0x04004160 RID: 16736
			private static int MaxRsaSecurityTokenCacheSize = 1024;

			// Token: 0x04004161 RID: 16737
			private IChannelFactory<IRequestChannel> channelFactory;

			// Token: 0x04004162 RID: 16738
			private Binding issuerBinding;

			// Token: 0x04004163 RID: 16739
			private KeyedByTypeCollection<IEndpointBehavior> channelBehaviors;

			// Token: 0x04004164 RID: 16740
			private Collection<XmlElement> requestProperties = new Collection<XmlElement>();

			// Token: 0x04004165 RID: 16741
			private IdentityVerifier identityVerifier = IdentityVerifier.CreateDefault();

			// Token: 0x04004166 RID: 16742
			private bool addTargetServiceAppliesTo;

			// Token: 0x04004167 RID: 16743
			private SecurityKeyEntropyMode keyEntropyMode;

			// Token: 0x04004168 RID: 16744
			private SecurityKeyType keyType;

			// Token: 0x04004169 RID: 16745
			private bool isKeyTypePresentInRstProperties;

			// Token: 0x0400416A RID: 16746
			private int keySize;

			// Token: 0x0400416B RID: 16747
			private bool isKeySizePresentInRstProperties;

			// Token: 0x0400416C RID: 16748
			private int defaultPublicKeySize = 1024;

			// Token: 0x0400416D RID: 16749
			private MessageVersion messageVersion;

			// Token: 0x0400416E RID: 16750
			private ChannelParameterCollection channelParameters;

			// Token: 0x0400416F RID: 16751
			private readonly List<RsaSecurityToken> rsaSecurityTokens = new List<RsaSecurityToken>();

			// Token: 0x04004170 RID: 16752
			private SafeFreeCredentials credentialsHandle;

			// Token: 0x04004171 RID: 16753
			private bool ownCredentialsHandle;
		}

		// Token: 0x02000B9C RID: 2972
		private class FederatedTokenProviderState : AcceleratedTokenProviderState
		{
			// Token: 0x06007397 RID: 29591 RVA: 0x001AF668 File Offset: 0x001AD868
			public FederatedTokenProviderState(byte[] entropy) : base(entropy)
			{
			}

			// Token: 0x06007398 RID: 29592 RVA: 0x001AF671 File Offset: 0x001AD871
			public FederatedTokenProviderState(RsaSecurityToken rsaToken) : base(null)
			{
				this.rsaToken = rsaToken;
			}

			// Token: 0x17001ACE RID: 6862
			// (get) Token: 0x06007399 RID: 29593 RVA: 0x001AF681 File Offset: 0x001AD881
			public RSA Rsa
			{
				get
				{
					return this.rsaToken.Rsa;
				}
			}

			// Token: 0x17001ACF RID: 6863
			// (get) Token: 0x0600739A RID: 29594 RVA: 0x001AF68E File Offset: 0x001AD88E
			public RsaSecurityToken RsaSecurityToken
			{
				get
				{
					return this.rsaToken;
				}
			}

			// Token: 0x04004172 RID: 16754
			private RsaSecurityToken rsaToken;
		}
	}
}
