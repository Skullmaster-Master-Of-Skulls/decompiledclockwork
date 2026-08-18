using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000330 RID: 816
	internal class RequestSecurityToken : BodyWriter
	{
		// Token: 0x06001D3E RID: 7486 RVA: 0x0006CF72 File Offset: 0x0006B172
		public RequestSecurityToken() : this(SecurityStandardsManager.DefaultInstance)
		{
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0006CF7F File Offset: 0x0006B17F
		public RequestSecurityToken(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer) : this(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer))
		{
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0006CF90 File Offset: 0x0006B190
		public RequestSecurityToken(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer, XmlElement requestSecurityTokenXml, string context, string tokenType, string requestType, int keySize, SecurityKeyIdentifierClause renewTarget, SecurityKeyIdentifierClause closeTarget) : this(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer), requestSecurityTokenXml, context, tokenType, requestType, keySize, renewTarget, closeTarget)
		{
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0006CFB8 File Offset: 0x0006B1B8
		public RequestSecurityToken(XmlElement requestSecurityTokenXml, string context, string tokenType, string requestType, int keySize, SecurityKeyIdentifierClause renewTarget, SecurityKeyIdentifierClause closeTarget) : this(SecurityStandardsManager.DefaultInstance, requestSecurityTokenXml, context, tokenType, requestType, keySize, renewTarget, closeTarget)
		{
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0006CFDC File Offset: 0x0006B1DC
		internal RequestSecurityToken(SecurityStandardsManager standardsManager, XmlElement rstXml, string context, string tokenType, string requestType, int keySize, SecurityKeyIdentifierClause renewTarget, SecurityKeyIdentifierClause closeTarget)
		{
			this.thisLock = new object();
			base..ctor(true);
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			if (rstXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstXml");
			}
			this.rstXml = rstXml;
			this.context = context;
			this.tokenType = tokenType;
			this.keySize = keySize;
			this.requestType = requestType;
			this.renewTarget = renewTarget;
			this.closeTarget = closeTarget;
			this.isReceiver = true;
			this.isReadOnly = true;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0006D071 File Offset: 0x0006B271
		internal RequestSecurityToken(SecurityStandardsManager standardsManager) : this(standardsManager, true)
		{
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0006D07C File Offset: 0x0006B27C
		internal RequestSecurityToken(SecurityStandardsManager standardsManager, bool isBuffered)
		{
			this.thisLock = new object();
			base..ctor(isBuffered);
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			this.requestType = this.standardsManager.TrustDriver.RequestTypeIssue;
			this.requestProperties = null;
			this.isReceiver = false;
			this.isReadOnly = false;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0006D0E8 File Offset: 0x0006B2E8
		public ChannelBinding GetChannelBinding()
		{
			if (this.message == null)
			{
				return null;
			}
			ChannelBindingMessageProperty channelBindingMessageProperty = null;
			ChannelBindingMessageProperty.TryGet(this.message, out channelBindingMessageProperty);
			ChannelBinding result = null;
			if (channelBindingMessageProperty != null)
			{
				result = channelBindingMessageProperty.ChannelBinding;
			}
			return result;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0006D11C File Offset: 0x0006B31C
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x0006D124 File Offset: 0x0006B324
		public Message Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0006D12D File Offset: 0x0006B32D
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x0006D135 File Offset: 0x0006B335
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

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0006D160 File Offset: 0x0006B360
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0006D168 File Offset: 0x0006B368
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0006D193 File Offset: 0x0006B393
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0006D19C File Offset: 0x0006B39C
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

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0006D1F5 File Offset: 0x0006B3F5
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0006D1FD File Offset: 0x0006B3FD
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0006D205 File Offset: 0x0006B405
		public RequestSecurityToken.OnGetBinaryNegotiationCallback OnGetBinaryNegotiation
		{
			get
			{
				return this.onGetBinaryNegotiation;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.onGetBinaryNegotiation = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0006D230 File Offset: 0x0006B430
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0006D268 File Offset: 0x0006B468
		public IEnumerable<XmlElement> RequestProperties
		{
			get
			{
				if (this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemNotAvailableInDeserializedRST", new object[]
					{
						"RequestProperties"
					})));
				}
				return this.requestProperties;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value != null)
				{
					int num = 0;
					Collection<XmlElement> collection = new Collection<XmlElement>();
					foreach (XmlElement xmlElement in value)
					{
						if (xmlElement == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "value[{0}]", new object[]
							{
								num
							})));
						}
						collection.Add(xmlElement);
						num++;
					}
					this.requestProperties = collection;
					return;
				}
				this.requestProperties = null;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0006D324 File Offset: 0x0006B524
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0006D32C File Offset: 0x0006B52C
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.requestType = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0006D36A File Offset: 0x0006B56A
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0006D372 File Offset: 0x0006B572
		public SecurityKeyIdentifierClause RenewTarget
		{
			get
			{
				return this.renewTarget;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.renewTarget = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0006D39D File Offset: 0x0006B59D
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0006D3A5 File Offset: 0x0006B5A5
		public SecurityKeyIdentifierClause CloseTarget
		{
			get
			{
				return this.closeTarget;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.closeTarget = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0006D3D0 File Offset: 0x0006B5D0
		public XmlElement RequestSecurityTokenXml
		{
			get
			{
				if (!this.isReceiver)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ItemAvailableInDeserializedRSTOnly", new object[]
					{
						"RequestSecurityTokenXml"
					})));
				}
				return this.rstXml;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0006D408 File Offset: 0x0006B608
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0006D410 File Offset: 0x0006B610
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
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.standardsManager = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0006D45E File Offset: 0x0006B65E
		internal bool IsReceiver
		{
			get
			{
				return this.isReceiver;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0006D466 File Offset: 0x0006B666
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

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0006D49E File Offset: 0x0006B69E
		internal DataContractSerializer AppliesToSerializer
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

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0006D4D6 File Offset: 0x0006B6D6
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

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0006D50E File Offset: 0x0006B70E
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006D516 File Offset: 0x0006B716
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

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006D554 File Offset: 0x0006B754
		internal BinaryNegotiation GetBinaryNegotiation()
		{
			if (this.isReceiver)
			{
				return this.standardsManager.TrustDriver.GetBinaryNegotiation(this);
			}
			if (this.negotiationData == null && this.onGetBinaryNegotiation != null)
			{
				this.onGetBinaryNegotiation(this.GetChannelBinding());
			}
			return this.negotiationData;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006D5A2 File Offset: 0x0006B7A2
		public SecurityToken GetRequestorEntropy()
		{
			return this.GetRequestorEntropy(null);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0006D5AB File Offset: 0x0006B7AB
		internal SecurityToken GetRequestorEntropy(SecurityTokenResolver resolver)
		{
			if (this.isReceiver)
			{
				return this.standardsManager.TrustDriver.GetEntropy(this, resolver);
			}
			return this.entropyToken;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0006D5CE File Offset: 0x0006B7CE
		public void SetRequestorEntropy(byte[] entropy)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.entropyToken = ((entropy != null) ? new NonceToken(entropy) : null);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0006D604 File Offset: 0x0006B804
		internal void SetRequestorEntropy(WrappedKeySecurityToken entropyToken)
		{
			if (this.IsReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			this.entropyToken = entropyToken;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0006D630 File Offset: 0x0006B830
		public void SetAppliesTo<T>(T appliesTo, DataContractSerializer serializer)
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

		// Token: 0x06001D68 RID: 7528 RVA: 0x0006D6A0 File Offset: 0x0006B8A0
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

		// Token: 0x06001D69 RID: 7529 RVA: 0x0006D6F0 File Offset: 0x0006B8F0
		public T GetAppliesTo<T>()
		{
			return this.GetAppliesTo<T>(DataContractSerializerDefaults.CreateSerializer(typeof(T), int.MaxValue));
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0006D70C File Offset: 0x0006B90C
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

		// Token: 0x06001D6B RID: 7531 RVA: 0x0006D747 File Offset: 0x0006B947
		private void OnWriteTo(XmlWriter writer)
		{
			if (this.isReceiver)
			{
				this.rstXml.WriteTo(writer);
				return;
			}
			this.standardsManager.TrustDriver.WriteRequestSecurityToken(this, writer);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006D770 File Offset: 0x0006B970
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

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006D834 File Offset: 0x0006BA34
		public static RequestSecurityToken CreateFrom(XmlReader reader)
		{
			return RequestSecurityToken.CreateFrom(SecurityStandardsManager.DefaultInstance, reader);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0006D841 File Offset: 0x0006BA41
		public static RequestSecurityToken CreateFrom(XmlReader reader, MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer)
		{
			return RequestSecurityToken.CreateFrom(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer), reader);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0006D850 File Offset: 0x0006BA50
		internal static RequestSecurityToken CreateFrom(SecurityStandardsManager standardsManager, XmlReader reader)
		{
			return standardsManager.TrustDriver.CreateRequestSecurityToken(reader);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0006D85E File Offset: 0x0006BA5E
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.isReadOnly = true;
				if (this.requestProperties != null)
				{
					this.requestProperties = new ReadOnlyCollection<XmlElement>(this.requestProperties);
				}
				this.OnMakeReadOnly();
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0006D88E File Offset: 0x0006BA8E
		protected internal virtual void OnWriteCustomAttributes(XmlWriter writer)
		{
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0006D890 File Offset: 0x0006BA90
		protected internal virtual void OnWriteCustomElements(XmlWriter writer)
		{
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0006D892 File Offset: 0x0006BA92
		protected internal virtual void OnMakeReadOnly()
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006D894 File Offset: 0x0006BA94
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x04001E03 RID: 7683
		private string context;

		// Token: 0x04001E04 RID: 7684
		private string tokenType;

		// Token: 0x04001E05 RID: 7685
		private string requestType;

		// Token: 0x04001E06 RID: 7686
		private SecurityToken entropyToken;

		// Token: 0x04001E07 RID: 7687
		private BinaryNegotiation negotiationData;

		// Token: 0x04001E08 RID: 7688
		private XmlElement rstXml;

		// Token: 0x04001E09 RID: 7689
		private IList<XmlElement> requestProperties;

		// Token: 0x04001E0A RID: 7690
		private byte[] cachedWriteBuffer;

		// Token: 0x04001E0B RID: 7691
		private int cachedWriteBufferLength;

		// Token: 0x04001E0C RID: 7692
		private int keySize;

		// Token: 0x04001E0D RID: 7693
		private Message message;

		// Token: 0x04001E0E RID: 7694
		private SecurityKeyIdentifierClause renewTarget;

		// Token: 0x04001E0F RID: 7695
		private SecurityKeyIdentifierClause closeTarget;

		// Token: 0x04001E10 RID: 7696
		private RequestSecurityToken.OnGetBinaryNegotiationCallback onGetBinaryNegotiation;

		// Token: 0x04001E11 RID: 7697
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001E12 RID: 7698
		private bool isReceiver;

		// Token: 0x04001E13 RID: 7699
		private bool isReadOnly;

		// Token: 0x04001E14 RID: 7700
		private object appliesTo;

		// Token: 0x04001E15 RID: 7701
		private DataContractSerializer appliesToSerializer;

		// Token: 0x04001E16 RID: 7702
		private Type appliesToType;

		// Token: 0x04001E17 RID: 7703
		private object thisLock;

		// Token: 0x02000B7B RID: 2939
		// (Invoke) Token: 0x060072C8 RID: 29384
		public delegate void OnGetBinaryNegotiationCallback(ChannelBinding channelBinding);
	}
}
