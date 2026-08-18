using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000331 RID: 817
	internal class RequestSecurityTokenResponse : BodyWriter
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x0006D89D File Offset: 0x0006BA9D
		public RequestSecurityTokenResponse() : this(SecurityStandardsManager.DefaultInstance)
		{
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0006D8AA File Offset: 0x0006BAAA
		public RequestSecurityTokenResponse(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer) : this(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer))
		{
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006D8BC File Offset: 0x0006BABC
		public RequestSecurityTokenResponse(XmlElement requestSecurityTokenResponseXml, string context, string tokenType, int keySize, SecurityKeyIdentifierClause requestedAttachedReference, SecurityKeyIdentifierClause requestedUnattachedReference, bool computeKey, DateTime validFrom, DateTime validTo, bool isRequestedTokenClosed) : this(SecurityStandardsManager.DefaultInstance, requestSecurityTokenResponseXml, context, tokenType, keySize, requestedAttachedReference, requestedUnattachedReference, computeKey, validFrom, validTo, isRequestedTokenClosed, null)
		{
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0006D8E8 File Offset: 0x0006BAE8
		public RequestSecurityTokenResponse(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer, XmlElement requestSecurityTokenResponseXml, string context, string tokenType, int keySize, SecurityKeyIdentifierClause requestedAttachedReference, SecurityKeyIdentifierClause requestedUnattachedReference, bool computeKey, DateTime validFrom, DateTime validTo, bool isRequestedTokenClosed) : this(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer), requestSecurityTokenResponseXml, context, tokenType, keySize, requestedAttachedReference, requestedUnattachedReference, computeKey, validFrom, validTo, isRequestedTokenClosed, null)
		{
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0006D918 File Offset: 0x0006BB18
		internal RequestSecurityTokenResponse(SecurityStandardsManager standardsManager)
		{
			this.thisLock = new object();
			base..ctor(true);
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			this.effectiveTime = SecurityUtils.MinUtcDateTime;
			this.expirationTime = SecurityUtils.MaxUtcDateTime;
			this.isRequestedTokenClosed = false;
			this.isLifetimeSet = false;
			this.isReceiver = false;
			this.isReadOnly = false;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0006D988 File Offset: 0x0006BB88
		internal RequestSecurityTokenResponse(SecurityStandardsManager standardsManager, XmlElement rstrXml, string context, string tokenType, int keySize, SecurityKeyIdentifierClause requestedAttachedReference, SecurityKeyIdentifierClause requestedUnattachedReference, bool computeKey, DateTime validFrom, DateTime validTo, bool isRequestedTokenClosed, XmlBuffer issuedTokenBuffer)
		{
			this.thisLock = new object();
			base..ctor(true);
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			if (rstrXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstrXml");
			}
			this.rstrXml = rstrXml;
			this.context = context;
			this.tokenType = tokenType;
			this.keySize = keySize;
			this.requestedAttachedReference = requestedAttachedReference;
			this.requestedUnattachedReference = requestedUnattachedReference;
			this.computeKey = computeKey;
			this.effectiveTime = validFrom.ToUniversalTime();
			this.expirationTime = validTo.ToUniversalTime();
			this.isLifetimeSet = true;
			this.isRequestedTokenClosed = isRequestedTokenClosed;
			this.issuedTokenBuffer = issuedTokenBuffer;
			this.isReceiver = true;
			this.isReadOnly = true;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0006DA4E File Offset: 0x0006BC4E
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x0006DA56 File Offset: 0x0006BC56
		public string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.context = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0006DA81 File Offset: 0x0006BC81
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0006DA89 File Offset: 0x0006BC89
		public string TokenType
		{
			get
			{
				return this.tokenType;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.tokenType = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0006DAB4 File Offset: 0x0006BCB4
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0006DABC File Offset: 0x0006BCBC
		public SecurityKeyIdentifierClause RequestedAttachedReference
		{
			get
			{
				return this.requestedAttachedReference;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.requestedAttachedReference = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0006DAE7 File Offset: 0x0006BCE7
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0006DAEF File Offset: 0x0006BCEF
		public SecurityKeyIdentifierClause RequestedUnattachedReference
		{
			get
			{
				return this.requestedUnattachedReference;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.requestedUnattachedReference = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0006DB1A File Offset: 0x0006BD1A
		public DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0006DB22 File Offset: 0x0006BD22
		public DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0006DB2A File Offset: 0x0006BD2A
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0006DB32 File Offset: 0x0006BD32
		public bool ComputeKey
		{
			get
			{
				return this.computeKey;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.computeKey = value;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0006DB5D File Offset: 0x0006BD5D
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0006DB68 File Offset: 0x0006BD68
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeNonNegative")));
				}
				this.keySize = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0006DBC1 File Offset: 0x0006BDC1
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0006DBC9 File Offset: 0x0006BDC9
		public bool IsRequestedTokenClosed
		{
			get
			{
				return this.isRequestedTokenClosed;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.isRequestedTokenClosed = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0006DBFC File Offset: 0x0006BDFC
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x0006DC04 File Offset: 0x0006BE04
		internal bool IsReceiver
		{
			get
			{
				return this.isReceiver;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0006DC0C File Offset: 0x0006BE0C
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0006DC14 File Offset: 0x0006BE14
		internal SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.standardsManager = ((value != null) ? value : SecurityStandardsManager.DefaultInstance);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0006DC49 File Offset: 0x0006BE49
		public SecurityToken EntropyToken
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRSTR", new object[]
					{
						"EntropyToken"
					})));
				}
				return this.entropyToken;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0006DC81 File Offset: 0x0006BE81
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0006DCB9 File Offset: 0x0006BEB9
		public SecurityToken RequestedSecurityToken
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRSTR", new object[]
					{
						"IssuedToken"
					})));
				}
				return this.issuedToken;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.issuedToken = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0006DD1C File Offset: 0x0006BF1C
		public SecurityToken RequestedProofToken
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRSTR", new object[]
					{
						"ProofToken"
					})));
				}
				return this.proofToken;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.proofToken = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0006DD47 File Offset: 0x0006BF47
		public XmlElement RequestSecurityTokenResponseXml
		{
			get
			{
				if (!this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemAvailableInDeserializedRSTROnly", new object[]
					{
						"RequestSecurityTokenXml"
					})));
				}
				return this.rstrXml;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0006DD7F File Offset: 0x0006BF7F
		internal object AppliesTo
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRST", new object[]
					{
						"AppliesTo"
					})));
				}
				return this.appliesTo;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0006DDB7 File Offset: 0x0006BFB7
		internal XmlObjectSerializer AppliesToSerializer
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRST", new object[]
					{
						"AppliesToSerializer"
					})));
				}
				return this.appliesToSerializer;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0006DDEF File Offset: 0x0006BFEF
		internal Type AppliesToType
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRST", new object[]
					{
						"AppliesToType"
					})));
				}
				return this.appliesToType;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0006DE27 File Offset: 0x0006C027
		internal bool IsLifetimeSet
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRSTR", new object[]
					{
						"IsLifetimeSet"
					})));
				}
				return this.isLifetimeSet;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0006DE5F File Offset: 0x0006C05F
		internal XmlBuffer IssuedTokenBuffer
		{
			get
			{
				return this.issuedTokenBuffer;
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0006DE67 File Offset: 0x0006C067
		public void SetIssuerEntropy(byte[] issuerEntropy)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.entropyToken = ((issuerEntropy != null) ? new NonceToken(issuerEntropy) : null);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0006DE9D File Offset: 0x0006C09D
		internal void SetIssuerEntropy(WrappedKeySecurityToken issuerEntropy)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.entropyToken = issuerEntropy;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0006DEC8 File Offset: 0x0006C0C8
		public SecurityToken GetIssuerEntropy()
		{
			return this.GetIssuerEntropy(null);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0006DED1 File Offset: 0x0006C0D1
		internal SecurityToken GetIssuerEntropy(SecurityTokenResolver resolver)
		{
			if (this.isReceiver)
			{
				return this.standardsManager.TrustDriver.GetEntropy(this, resolver);
			}
			return this.entropyToken;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
		public void SetLifetime(DateTime validFrom, DateTime validTo)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			if (validFrom.ToUniversalTime() > validTo.ToUniversalTime())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("EffectiveGreaterThanExpiration"));
			}
			this.effectiveTime = validFrom.ToUniversalTime();
			this.expirationTime = validTo.ToUniversalTime();
			this.isLifetimeSet = true;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0006DF70 File Offset: 0x0006C170
		public void SetAppliesTo<T>(T appliesTo, XmlObjectSerializer serializer)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			if (appliesTo != null && serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			this.appliesTo = appliesTo;
			this.appliesToSerializer = serializer;
			this.appliesToType = typeof(T);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		public void GetAppliesToQName(out string localName, out string namespaceUri)
		{
			if (!this.isReceiver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemAvailableInDeserializedRSTOnly", new object[]
				{
					"MatchesAppliesTo"
				})));
			}
			this.standardsManager.TrustDriver.GetAppliesToQName(this, out localName, out namespaceUri);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0006E030 File Offset: 0x0006C230
		public T GetAppliesTo<T>()
		{
			return this.GetAppliesTo<T>(DataContractSerializerDefaults.CreateSerializer(typeof(T), int.MaxValue));
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0006E04C File Offset: 0x0006C24C
		public T GetAppliesTo<T>(XmlObjectSerializer serializer)
		{
			if (!this.isReceiver)
			{
				return (T)((object)this.appliesTo);
			}
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			return this.standardsManager.TrustDriver.GetAppliesTo<T>(this, serializer);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006E087 File Offset: 0x0006C287
		internal void SetBinaryNegotiation(BinaryNegotiation negotiation)
		{
			if (negotiation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("negotiation");
			}
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.negotiationData = negotiation;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0006E0C5 File Offset: 0x0006C2C5
		internal BinaryNegotiation GetBinaryNegotiation()
		{
			if (this.isReceiver)
			{
				return this.standardsManager.TrustDriver.GetBinaryNegotiation(this);
			}
			return this.negotiationData;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0006E0E8 File Offset: 0x0006C2E8
		internal void SetAuthenticator(byte[] authenticator)
		{
			if (authenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authenticator");
			}
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.authenticator = DiagnosticUtility.Utility.AllocateByteArray(authenticator.Length);
			Buffer.BlockCopy(authenticator, 0, this.authenticator, 0, authenticator.Length);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0006E150 File Offset: 0x0006C350
		internal byte[] GetAuthenticator()
		{
			if (this.isReceiver)
			{
				return this.standardsManager.TrustDriver.GetAuthenticator(this);
			}
			if (this.authenticator == null)
			{
				return null;
			}
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(this.authenticator.Length);
			Buffer.BlockCopy(this.authenticator, 0, array, 0, this.authenticator.Length);
			return array;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006E1AB File Offset: 0x0006C3AB
		private void OnWriteTo(XmlWriter w)
		{
			if (this.isReceiver)
			{
				this.rstrXml.WriteTo(w);
				return;
			}
			this.standardsManager.TrustDriver.WriteRequestSecurityTokenResponse(this, w);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.IsReadOnly)
			{
				if (this.cachedWriteBuffer == null)
				{
					MemoryStream memoryStream = new MemoryStream();
					using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, XD.Dictionary))
					{
						this.OnWriteTo(xmlDictionaryWriter);
						xmlDictionaryWriter.Flush();
						memoryStream.Flush();
						memoryStream.Seek(0L, SeekOrigin.Begin);
						this.cachedWriteBuffer = memoryStream.GetBuffer();
						this.cachedWriteBufferLength = (int)memoryStream.Length;
					}
				}
				writer.WriteNode(XmlDictionaryReader.CreateBinaryReader(this.cachedWriteBuffer, 0, this.cachedWriteBufferLength, XD.Dictionary, XmlDictionaryReaderQuotas.Max), false);
				return;
			}
			this.OnWriteTo(writer);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0006E298 File Offset: 0x0006C498
		public static RequestSecurityTokenResponse CreateFrom(XmlReader reader)
		{
			return RequestSecurityTokenResponse.CreateFrom(SecurityStandardsManager.DefaultInstance, reader);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0006E2A5 File Offset: 0x0006C4A5
		public static RequestSecurityTokenResponse CreateFrom(XmlReader reader, MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer)
		{
			return RequestSecurityTokenResponse.CreateFrom(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer), reader);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006E2B4 File Offset: 0x0006C4B4
		internal static RequestSecurityTokenResponse CreateFrom(SecurityStandardsManager standardsManager, XmlReader reader)
		{
			return standardsManager.TrustDriver.CreateRequestSecurityTokenResponse(reader);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006E2C2 File Offset: 0x0006C4C2
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0006E2CB File Offset: 0x0006C4CB
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.isReadOnly = true;
				this.OnMakeReadOnly();
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0006E2E4 File Offset: 0x0006C4E4
		public GenericXmlSecurityToken GetIssuedToken(SecurityTokenResolver resolver, IList<SecurityTokenAuthenticator> allowedAuthenticators, SecurityKeyEntropyMode keyEntropyMode, byte[] requestorEntropy, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			return this.GetIssuedToken(resolver, allowedAuthenticators, keyEntropyMode, requestorEntropy, expectedTokenType, authorizationPolicies, 0, false);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0006E304 File Offset: 0x0006C504
		public virtual GenericXmlSecurityToken GetIssuedToken(SecurityTokenResolver resolver, IList<SecurityTokenAuthenticator> allowedAuthenticators, SecurityKeyEntropyMode keyEntropyMode, byte[] requestorEntropy, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, int defaultKeySize, bool isBearerKeyType)
		{
			if (!this.isReceiver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemAvailableInDeserializedRSTROnly", new object[]
				{
					"GetIssuedToken"
				})));
			}
			return this.standardsManager.TrustDriver.GetIssuedToken(this, resolver, allowedAuthenticators, keyEntropyMode, requestorEntropy, expectedTokenType, authorizationPolicies, defaultKeySize, isBearerKeyType);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0006E360 File Offset: 0x0006C560
		public virtual GenericXmlSecurityToken GetIssuedToken(string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, RSA clientKey)
		{
			if (!this.isReceiver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemAvailableInDeserializedRSTROnly", new object[]
				{
					"GetIssuedToken"
				})));
			}
			return this.standardsManager.TrustDriver.GetIssuedToken(this, expectedTokenType, authorizationPolicies, clientKey);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0006E3B1 File Offset: 0x0006C5B1
		protected internal virtual void OnWriteCustomAttributes(XmlWriter writer)
		{
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0006E3B3 File Offset: 0x0006C5B3
		protected internal virtual void OnWriteCustomElements(XmlWriter writer)
		{
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0006E3B5 File Offset: 0x0006C5B5
		protected virtual void OnMakeReadOnly()
		{
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0006E3B8 File Offset: 0x0006C5B8
		public static byte[] ComputeCombinedKey(byte[] requestorEntropy, byte[] issuerEntropy, int keySizeInBits)
		{
			if (requestorEntropy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestorEntropy");
			}
			if (issuerEntropy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerEntropy");
			}
			if (keySizeInBits < RequestSecurityTokenResponse.minSaneKeySizeInBits || keySizeInBits > RequestSecurityTokenResponse.maxSaneKeySizeInBits)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidKeySizeSpecifiedInNegotiation", new object[]
				{
					keySizeInBits,
					RequestSecurityTokenResponse.minSaneKeySizeInBits,
					RequestSecurityTokenResponse.maxSaneKeySizeInBits
				})));
			}
			Psha1DerivedKeyGenerator psha1DerivedKeyGenerator = new Psha1DerivedKeyGenerator(requestorEntropy);
			return psha1DerivedKeyGenerator.GenerateDerivedKey(new byte[0], issuerEntropy, keySizeInBits, 0);
		}

		// Token: 0x04001E18 RID: 7704
		private static int minSaneKeySizeInBits = 64;

		// Token: 0x04001E19 RID: 7705
		private static int maxSaneKeySizeInBits = 131072;

		// Token: 0x04001E1A RID: 7706
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001E1B RID: 7707
		private string context;

		// Token: 0x04001E1C RID: 7708
		private int keySize;

		// Token: 0x04001E1D RID: 7709
		private bool computeKey;

		// Token: 0x04001E1E RID: 7710
		private string tokenType;

		// Token: 0x04001E1F RID: 7711
		private SecurityKeyIdentifierClause requestedAttachedReference;

		// Token: 0x04001E20 RID: 7712
		private SecurityKeyIdentifierClause requestedUnattachedReference;

		// Token: 0x04001E21 RID: 7713
		private SecurityToken issuedToken;

		// Token: 0x04001E22 RID: 7714
		private SecurityToken proofToken;

		// Token: 0x04001E23 RID: 7715
		private SecurityToken entropyToken;

		// Token: 0x04001E24 RID: 7716
		private BinaryNegotiation negotiationData;

		// Token: 0x04001E25 RID: 7717
		private XmlElement rstrXml;

		// Token: 0x04001E26 RID: 7718
		private DateTime effectiveTime;

		// Token: 0x04001E27 RID: 7719
		private DateTime expirationTime;

		// Token: 0x04001E28 RID: 7720
		private bool isLifetimeSet;

		// Token: 0x04001E29 RID: 7721
		private byte[] authenticator;

		// Token: 0x04001E2A RID: 7722
		private bool isReceiver;

		// Token: 0x04001E2B RID: 7723
		private bool isReadOnly;

		// Token: 0x04001E2C RID: 7724
		private byte[] cachedWriteBuffer;

		// Token: 0x04001E2D RID: 7725
		private int cachedWriteBufferLength;

		// Token: 0x04001E2E RID: 7726
		private bool isRequestedTokenClosed;

		// Token: 0x04001E2F RID: 7727
		private object appliesTo;

		// Token: 0x04001E30 RID: 7728
		private XmlObjectSerializer appliesToSerializer;

		// Token: 0x04001E31 RID: 7729
		private Type appliesToType;

		// Token: 0x04001E32 RID: 7730
		private object thisLock;

		// Token: 0x04001E33 RID: 7731
		private XmlBuffer issuedTokenBuffer;
	}
}
