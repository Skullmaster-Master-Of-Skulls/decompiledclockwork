using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	public class SessionSecurityToken : SecurityToken, ISerializable
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x0003839D File Offset: 0x0003659D
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal) : this(claimsPrincipal, null)
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000383A7 File Offset: 0x000365A7
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, TimeSpan lifetime) : this(claimsPrincipal, null, new DateTime?(DateTime.UtcNow), new DateTime?(DateTimeUtil.AddNonNegative(DateTime.UtcNow, lifetime)))
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000383CB File Offset: 0x000365CB
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, string context) : this(claimsPrincipal, context, new DateTime?(DateTime.UtcNow), new DateTime?(DateTimeUtil.AddNonNegative(DateTime.UtcNow, SessionSecurityTokenHandler.DefaultTokenLifetime)))
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000383F3 File Offset: 0x000365F3
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, string context, DateTime? validFrom, DateTime? validTo) : this(claimsPrincipal, new UniqueId(), context, string.Empty, validFrom, validTo, null)
		{
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003840B File Offset: 0x0003660B
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, string context, string endpointId, DateTime? validFrom, DateTime? validTo) : this(claimsPrincipal, new UniqueId(), context, endpointId, validFrom, validTo, null)
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00038420 File Offset: 0x00036620
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, UniqueId contextId, string context, string endpointId, TimeSpan lifetime, SymmetricSecurityKey key) : this(claimsPrincipal, contextId, context, endpointId, DateTime.UtcNow, lifetime, key)
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00038436 File Offset: 0x00036636
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, UniqueId contextId, string context, string endpointId, DateTime validFrom, TimeSpan lifetime, SymmetricSecurityKey key) : this(claimsPrincipal, contextId, context, endpointId, new DateTime?(validFrom), new DateTime?(DateTimeUtil.AddNonNegative(validFrom, lifetime)), key)
		{
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003845C File Offset: 0x0003665C
		public SessionSecurityToken(ClaimsPrincipal claimsPrincipal, UniqueId contextId, string context, string endpointId, DateTime? validFrom, DateTime? validTo, SymmetricSecurityKey key) : this(claimsPrincipal, contextId, UniqueId.CreateUniqueId(), context, (key == null) ? null : key.GetSymmetricKey(), endpointId, validFrom, validTo, null, validFrom, validTo, null, null)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00038494 File Offset: 0x00036694
		internal SessionSecurityToken(ClaimsPrincipal claimsPrincipal, UniqueId contextId, string id, string context, byte[] key, string endpointId, DateTime? validFrom, DateTime? validTo, UniqueId keyGeneration, DateTime? keyEffectiveTime, DateTime? keyExpirationTime, SctAuthorizationPolicy sctAuthorizationPolicy, Uri securityContextSecurityTokenWrapperSecureConversationVersion)
		{
			if (claimsPrincipal == null || claimsPrincipal.Identities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimsPrincipal");
			}
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			DateTime dateTime;
			if (validFrom != null)
			{
				dateTime = DateTimeUtil.ToUniversalTime(validFrom.Value);
			}
			else
			{
				dateTime = DateTime.UtcNow;
			}
			DateTime dateTime2;
			if (validTo != null)
			{
				dateTime2 = DateTimeUtil.ToUniversalTime(validTo.Value);
			}
			else
			{
				dateTime2 = DateTimeUtil.Add(dateTime, SessionSecurityTokenHandler.DefaultTokenLifetime);
			}
			if (dateTime >= dateTime2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("validFrom"));
			}
			if (dateTime2 < DateTime.UtcNow)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("validTo"));
			}
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointId");
			}
			if (keyEffectiveTime == null)
			{
				keyEffectiveTime = new DateTime?(dateTime);
			}
			if (keyExpirationTime == null)
			{
				keyExpirationTime = new DateTime?(dateTime2);
			}
			if (keyEffectiveTime.Value > keyExpirationTime.Value || keyEffectiveTime.Value < dateTime)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keyEffectiveTime"));
			}
			if (keyExpirationTime.Value > dateTime2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keyExpirationTime"));
			}
			if (securityContextSecurityTokenWrapperSecureConversationVersion == null)
			{
				this._secureConversationVersion = WSSecureConversation13Constants.NamespaceUri;
			}
			else
			{
				this._isSecurityContextSecurityTokenWrapper = true;
				this._secureConversationVersion = securityContextSecurityTokenWrapperSecureConversationVersion;
			}
			if (key == null)
			{
				key = CryptoHelper.KeyGenerator.GenerateSymmetricKey(128);
			}
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointId");
			}
			this._claimsPrincipal = claimsPrincipal;
			this._contextId = contextId;
			this._id = id;
			this._context = context;
			this._securityKeys = new ReadOnlyCollection<SecurityKey>(new SecurityKey[]
			{
				new InMemorySymmetricSecurityKey(key)
			});
			this._endpointId = endpointId;
			this._validFrom = validFrom.Value;
			this._validTo = validTo.Value;
			this._keyGeneration = keyGeneration;
			this._keyEffectiveTime = keyEffectiveTime.Value;
			this._keyExpirationTime = keyExpirationTime.Value;
			this._sctAuthorizationPolicy = sctAuthorizationPolicy;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000386C0 File Offset: 0x000368C0
		protected SessionSecurityToken(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				return;
			}
			byte[] array = (byte[])info.GetValue("SessionToken", typeof(byte[]));
			if (array == null || array.Length == 0)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4272"));
			}
			SessionDictionary instance = SessionDictionary.Instance;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(array, 0, array.Length, instance, XmlDictionaryReaderQuotas.Max, null, null))
			{
				bool isSecurityContextSecurityTokenWrapper = false;
				bool isPersistent = true;
				bool isReferenceMode = false;
				string context2 = string.Empty;
				if (xmlDictionaryReader.IsStartElement(instance.SecurityContextToken, instance.EmptyString))
				{
					isSecurityContextSecurityTokenWrapper = true;
				}
				else
				{
					if (!xmlDictionaryReader.IsStartElement(instance.SessionToken, instance.EmptyString))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
						{
							instance.SecurityContextToken.Value,
							xmlDictionaryReader.Name
						})));
					}
					if (xmlDictionaryReader.GetAttribute(instance.PersistentTrue, instance.EmptyString) == null)
					{
						isPersistent = false;
					}
					if (xmlDictionaryReader.GetAttribute(instance.ReferenceModeTrue, instance.EmptyString) != null)
					{
						isReferenceMode = true;
					}
					xmlDictionaryReader.ReadFullStartElement();
					xmlDictionaryReader.MoveToContent();
					if (xmlDictionaryReader.IsStartElement(instance.Context, instance.EmptyString))
					{
						context2 = xmlDictionaryReader.ReadElementContentAsString();
					}
				}
				string text = xmlDictionaryReader.ReadElementString();
				if (text != "1")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4232", new object[]
					{
						text,
						"1"
					})));
				}
				string a = xmlDictionaryReader.ReadElementString();
				Uri namespaceUri;
				if (a == "http://schemas.xmlsoap.org/ws/2005/02/sc")
				{
					namespaceUri = WSSecureConversationFeb2005Constants.NamespaceUri;
				}
				else
				{
					if (!(a == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4232", new object[]
						{
							text,
							"1"
						})));
					}
					namespaceUri = WSSecureConversation13Constants.NamespaceUri;
				}
				string text2 = null;
				if (xmlDictionaryReader.IsStartElement(instance.Id, instance.EmptyString))
				{
					text2 = xmlDictionaryReader.ReadElementString();
				}
				if (string.IsNullOrEmpty(text2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4239", new object[]
					{
						instance.Id.Value
					})));
				}
				if (!xmlDictionaryReader.IsStartElement(instance.ContextId, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.ContextId.Value,
						xmlDictionaryReader.Name
					})));
				}
				UniqueId contextId = xmlDictionaryReader.ReadElementContentAsUniqueId();
				if (!xmlDictionaryReader.IsStartElement(instance.Key, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.Key.Value,
						xmlDictionaryReader.Name
					})));
				}
				byte[] symmetricKey = xmlDictionaryReader.ReadElementContentAsBase64();
				UniqueId keyGeneration = null;
				if (xmlDictionaryReader.IsStartElement(instance.KeyGeneration, instance.EmptyString))
				{
					keyGeneration = xmlDictionaryReader.ReadElementContentAsUniqueId();
				}
				if (!xmlDictionaryReader.IsStartElement(instance.EffectiveTime, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.EffectiveTime.Value,
						xmlDictionaryReader.Name
					})));
				}
				DateTime validFrom = new DateTime(XmlUtil.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				if (!xmlDictionaryReader.IsStartElement(instance.ExpiryTime, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.ExpiryTime.Value,
						xmlDictionaryReader.Name
					})));
				}
				DateTime validTo = new DateTime(XmlUtil.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				if (!xmlDictionaryReader.IsStartElement(instance.KeyEffectiveTime, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.KeyEffectiveTime.Value,
						xmlDictionaryReader.Name
					})));
				}
				DateTime keyEffectiveTime = new DateTime(XmlUtil.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				if (!xmlDictionaryReader.IsStartElement(instance.KeyExpiryTime, instance.EmptyString))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						instance.KeyExpiryTime.Value,
						xmlDictionaryReader.Name
					})));
				}
				DateTime keyExpirationTime = new DateTime(XmlUtil.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				ClaimsPrincipal claimsPrincipal = null;
				if (xmlDictionaryReader.IsStartElement(instance.ClaimsPrincipal, instance.EmptyString))
				{
					claimsPrincipal = this.ReadPrincipal(xmlDictionaryReader, instance);
				}
				SctAuthorizationPolicy sctAuthorizationPolicy = null;
				if (xmlDictionaryReader.IsStartElement(instance.SctAuthorizationPolicy, instance.EmptyString))
				{
					xmlDictionaryReader.ReadStartElement(instance.SctAuthorizationPolicy, instance.EmptyString);
					System.IdentityModel.Claims.Claim claim = this.DeserializeSysClaim(xmlDictionaryReader);
					xmlDictionaryReader.ReadEndElement();
					sctAuthorizationPolicy = new SctAuthorizationPolicy(claim);
				}
				string endpointId = null;
				if (xmlDictionaryReader.IsStartElement(instance.EndpointId, instance.EmptyString))
				{
					endpointId = xmlDictionaryReader.ReadElementContentAsString();
				}
				xmlDictionaryReader.ReadEndElement();
				this._claimsPrincipal = claimsPrincipal;
				this._contextId = contextId;
				this._id = text2;
				this._context = context2;
				this._securityKeys = new ReadOnlyCollection<SecurityKey>(new SecurityKey[]
				{
					new InMemorySymmetricSecurityKey(symmetricKey)
				});
				this._endpointId = endpointId;
				this._validFrom = validFrom;
				this._validTo = validTo;
				this._keyGeneration = keyGeneration;
				this._keyEffectiveTime = keyEffectiveTime;
				this._keyExpirationTime = keyExpirationTime;
				this._isSecurityContextSecurityTokenWrapper = isSecurityContextSecurityTokenWrapper;
				this._secureConversationVersion = namespaceUri;
				this._sctAuthorizationPolicy = sctAuthorizationPolicy;
				this._isPersistent = isPersistent;
				this._isReferenceMode = isReferenceMode;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00038C58 File Offset: 0x00036E58
		public ClaimsPrincipal ClaimsPrincipal
		{
			get
			{
				return this._claimsPrincipal;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00038C60 File Offset: 0x00036E60
		public string Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00038C68 File Offset: 0x00036E68
		public UniqueId ContextId
		{
			get
			{
				return this._contextId;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00038C70 File Offset: 0x00036E70
		public string EndpointId
		{
			get
			{
				return this._endpointId;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00038C78 File Offset: 0x00036E78
		internal bool IsSecurityContextSecurityTokenWrapper
		{
			get
			{
				return this._isSecurityContextSecurityTokenWrapper;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00038C80 File Offset: 0x00036E80
		public DateTime KeyEffectiveTime
		{
			get
			{
				return this._keyEffectiveTime;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00038C88 File Offset: 0x00036E88
		public DateTime KeyExpirationTime
		{
			get
			{
				return this._keyExpirationTime;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00038C90 File Offset: 0x00036E90
		public UniqueId KeyGeneration
		{
			get
			{
				return this._keyGeneration;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00038C98 File Offset: 0x00036E98
		public override string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00038CA0 File Offset: 0x00036EA0
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00038CA8 File Offset: 0x00036EA8
		public bool IsPersistent
		{
			get
			{
				return this._isPersistent;
			}
			set
			{
				this._isPersistent = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00038CB1 File Offset: 0x00036EB1
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00038CB9 File Offset: 0x00036EB9
		public bool IsReferenceMode
		{
			get
			{
				return this._isReferenceMode;
			}
			set
			{
				this._isReferenceMode = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00038CC2 File Offset: 0x00036EC2
		internal SctAuthorizationPolicy SctAuthorizationPolicy
		{
			get
			{
				return this._sctAuthorizationPolicy;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00038CCA File Offset: 0x00036ECA
		public Uri SecureConversationVersion
		{
			get
			{
				return this._secureConversationVersion;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00038CD2 File Offset: 0x00036ED2
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this._securityKeys;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00038CDA File Offset: 0x00036EDA
		public override DateTime ValidFrom
		{
			get
			{
				return this._validFrom;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00038CE2 File Offset: 0x00036EE2
		public override DateTime ValidTo
		{
			get
			{
				return this._validTo;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00038CEC File Offset: 0x00036EEC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			SessionDictionary instance = SessionDictionary.Instance;
			using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, instance))
			{
				if (this.IsSecurityContextSecurityTokenWrapper)
				{
					xmlDictionaryWriter.WriteStartElement(instance.SecurityContextToken, instance.EmptyString);
				}
				else
				{
					xmlDictionaryWriter.WriteStartElement(instance.SessionToken, instance.EmptyString);
					if (this.IsPersistent)
					{
						xmlDictionaryWriter.WriteAttributeString(instance.PersistentTrue, instance.EmptyString, "");
					}
					if (this.IsReferenceMode)
					{
						xmlDictionaryWriter.WriteAttributeString(instance.ReferenceModeTrue, instance.EmptyString, "");
					}
					if (!string.IsNullOrEmpty(this.Context))
					{
						xmlDictionaryWriter.WriteElementString(instance.Context, instance.EmptyString, this.Context);
					}
				}
				xmlDictionaryWriter.WriteStartElement(instance.Version, instance.EmptyString);
				xmlDictionaryWriter.WriteValue("1");
				xmlDictionaryWriter.WriteEndElement();
				xmlDictionaryWriter.WriteElementString(instance.SecureConversationVersion, instance.EmptyString, this.SecureConversationVersion.AbsoluteUri);
				xmlDictionaryWriter.WriteElementString(instance.Id, instance.EmptyString, this.Id);
				XmlUtil.WriteElementStringAsUniqueId(xmlDictionaryWriter, instance.ContextId, instance.EmptyString, this.ContextId.ToString());
				byte[] symmetricKey = ((SymmetricSecurityKey)this.SecurityKeys[0]).GetSymmetricKey();
				xmlDictionaryWriter.WriteStartElement(instance.Key, instance.EmptyString);
				xmlDictionaryWriter.WriteBase64(symmetricKey, 0, symmetricKey.Length);
				xmlDictionaryWriter.WriteEndElement();
				if (this.KeyGeneration != null)
				{
					XmlUtil.WriteElementStringAsUniqueId(xmlDictionaryWriter, instance.KeyGeneration, instance.EmptyString, this.KeyGeneration.ToString());
				}
				XmlUtil.WriteElementContentAsInt64(xmlDictionaryWriter, instance.EffectiveTime, instance.EmptyString, this.ValidFrom.ToUniversalTime().Ticks);
				XmlUtil.WriteElementContentAsInt64(xmlDictionaryWriter, instance.ExpiryTime, instance.EmptyString, this.ValidTo.ToUniversalTime().Ticks);
				XmlUtil.WriteElementContentAsInt64(xmlDictionaryWriter, instance.KeyEffectiveTime, instance.EmptyString, this.KeyEffectiveTime.ToUniversalTime().Ticks);
				XmlUtil.WriteElementContentAsInt64(xmlDictionaryWriter, instance.KeyExpiryTime, instance.EmptyString, this.KeyExpirationTime.ToUniversalTime().Ticks);
				this.WritePrincipal(xmlDictionaryWriter, instance, this.ClaimsPrincipal);
				if (this.SctAuthorizationPolicy != null)
				{
					xmlDictionaryWriter.WriteStartElement(instance.SctAuthorizationPolicy, instance.EmptyString);
					System.IdentityModel.Claims.Claim claim = ((DefaultClaimSet)((IAuthorizationPolicy)this.SctAuthorizationPolicy).Issuer)[0];
					this.SerializeSysClaim(claim, xmlDictionaryWriter);
					xmlDictionaryWriter.WriteEndElement();
				}
				xmlDictionaryWriter.WriteElementString(instance.EndpointId, instance.EmptyString, this.EndpointId);
				xmlDictionaryWriter.WriteEndElement();
				xmlDictionaryWriter.Flush();
				info.AddValue("SessionToken", memoryStream.ToArray());
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00038FCC File Offset: 0x000371CC
		private ClaimsPrincipal ReadPrincipal(XmlDictionaryReader dictionaryReader, SessionDictionary dictionary)
		{
			if (dictionaryReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryReader");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			ClaimsPrincipal claimsPrincipal = null;
			Collection<ClaimsIdentity> collection = new Collection<ClaimsIdentity>();
			dictionaryReader.MoveToContent();
			if (dictionaryReader.IsStartElement(dictionary.ClaimsPrincipal, dictionary.EmptyString))
			{
				dictionaryReader.ReadFullStartElement();
				this.ReadIdentities(dictionaryReader, dictionary, collection);
				dictionaryReader.ReadEndElement();
			}
			WindowsIdentity windowsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity in collection)
			{
				windowsIdentity = (claimsIdentity as WindowsIdentity);
				if (windowsIdentity != null)
				{
					claimsPrincipal = new WindowsPrincipal(windowsIdentity);
					break;
				}
			}
			if (claimsPrincipal != null)
			{
				collection.Remove(windowsIdentity);
			}
			else if (collection.Count > 0)
			{
				claimsPrincipal = new ClaimsPrincipal();
			}
			if (claimsPrincipal != null)
			{
				claimsPrincipal.AddIdentities(collection);
			}
			return claimsPrincipal;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000390AC File Offset: 0x000372AC
		private void ReadIdentities(XmlDictionaryReader dictionaryReader, SessionDictionary dictionary, Collection<ClaimsIdentity> identities)
		{
			if (dictionaryReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryReader");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (identities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identities");
			}
			dictionaryReader.MoveToContent();
			if (dictionaryReader.IsStartElement(dictionary.Identities, dictionary.EmptyString))
			{
				dictionaryReader.ReadFullStartElement();
				while (dictionaryReader.IsStartElement(dictionary.Identity, dictionary.EmptyString))
				{
					identities.Add(this.ReadIdentity(dictionaryReader, dictionary));
				}
				dictionaryReader.ReadEndElement();
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00039140 File Offset: 0x00037340
		private ClaimsIdentity ReadIdentity(XmlDictionaryReader dictionaryReader, SessionDictionary dictionary)
		{
			if (dictionaryReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryReader");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			dictionaryReader.MoveToContent();
			ClaimsIdentity claimsIdentity = null;
			if (!dictionaryReader.IsStartElement(dictionary.Identity, dictionary.EmptyString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID3007", new object[]
				{
					dictionaryReader.LocalName,
					dictionaryReader.NamespaceURI
				})));
			}
			string attribute = dictionaryReader.GetAttribute(dictionary.NameClaimType, dictionary.EmptyString);
			string attribute2 = dictionaryReader.GetAttribute(dictionary.RoleClaimType, dictionary.EmptyString);
			string attribute3 = dictionaryReader.GetAttribute(dictionary.WindowsLogonName, dictionary.EmptyString);
			string attribute4 = dictionaryReader.GetAttribute(dictionary.AuthenticationType, dictionary.EmptyString);
			if (string.IsNullOrEmpty(attribute3))
			{
				claimsIdentity = new ClaimsIdentity(attribute4, attribute, attribute2);
			}
			else
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity(this.GetUpn(attribute3));
				claimsIdentity = new WindowsIdentity(windowsIdentity.Token, attribute4);
			}
			claimsIdentity.Label = dictionaryReader.GetAttribute(dictionary.Label, dictionary.EmptyString);
			dictionaryReader.ReadFullStartElement();
			if (dictionaryReader.IsStartElement(dictionary.ClaimCollection, dictionary.EmptyString))
			{
				dictionaryReader.ReadStartElement();
				Collection<System.Security.Claims.Claim> claims = new Collection<System.Security.Claims.Claim>();
				this.ReadClaims(dictionaryReader, dictionary, claims);
				claimsIdentity.AddClaims(claims);
				dictionaryReader.ReadEndElement();
			}
			if (dictionaryReader.IsStartElement(dictionary.Actor, dictionary.EmptyString))
			{
				dictionaryReader.ReadStartElement();
				claimsIdentity.Actor = this.ReadIdentity(dictionaryReader, dictionary);
				dictionaryReader.ReadEndElement();
			}
			if (dictionaryReader.IsStartElement(dictionary.BootstrapToken, dictionary.EmptyString))
			{
				dictionaryReader.ReadStartElement();
				byte[] buffer = dictionaryReader.ReadContentAsBase64();
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					claimsIdentity.BootstrapContext = (BootstrapContext)binaryFormatter.Deserialize(memoryStream);
				}
				dictionaryReader.ReadEndElement();
			}
			dictionaryReader.ReadEndElement();
			return claimsIdentity;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00039338 File Offset: 0x00037538
		private string GetUpn(string windowsLogonName)
		{
			if (string.IsNullOrEmpty(windowsLogonName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsLogonName");
			}
			int num = windowsLogonName.IndexOf('\\');
			if (num >= 0 && num != 0 && num != windowsLogonName.Length - 1)
			{
				string text = windowsLogonName.Substring(0, num + 1);
				string str = windowsLogonName.Substring(num + 1);
				Dictionary<string, string> domainNameMap = SessionSecurityToken.DomainNameMap;
				string text2;
				bool flag2;
				lock (domainNameMap)
				{
					flag2 = SessionSecurityToken.DomainNameMap.TryGetValue(text, out text2);
				}
				if (!flag2)
				{
					uint capacity = 50U;
					StringBuilder stringBuilder = new StringBuilder((int)capacity);
					if (!NativeMethods.TranslateName(text, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != 122)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4248", new object[]
							{
								windowsLogonName
							}), new Win32Exception(lastWin32Error)));
						}
						stringBuilder = new StringBuilder((int)capacity);
						if (!NativeMethods.TranslateName(text, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
						{
							lastWin32Error = Marshal.GetLastWin32Error();
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4248", new object[]
							{
								windowsLogonName
							}), new Win32Exception(lastWin32Error)));
						}
					}
					stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
					text2 = stringBuilder.ToString();
					Dictionary<string, string> domainNameMap2 = SessionSecurityToken.DomainNameMap;
					lock (domainNameMap2)
					{
						if (SessionSecurityToken.DomainNameMap.Count >= 50)
						{
							if (SessionSecurityToken.rnd == null)
							{
								SessionSecurityToken.rnd = new Random((int)DateTime.Now.Ticks);
							}
							int num2 = SessionSecurityToken.rnd.Next() % SessionSecurityToken.DomainNameMap.Count;
							foreach (string key in SessionSecurityToken.DomainNameMap.Keys)
							{
								if (num2 <= 0)
								{
									SessionSecurityToken.DomainNameMap.Remove(key);
									break;
								}
								num2--;
							}
						}
						SessionSecurityToken.DomainNameMap[text] = text2;
					}
				}
				return str + "@" + text2;
			}
			if (SessionSecurityToken.IsPossibleUpn(windowsLogonName))
			{
				return windowsLogonName;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4248", new object[]
			{
				windowsLogonName
			})));
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000395A4 File Offset: 0x000377A4
		private void ReadClaims(XmlDictionaryReader dictionaryReader, SessionDictionary dictionary, Collection<System.Security.Claims.Claim> claims)
		{
			if (dictionaryReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryReader");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claims");
			}
			while (dictionaryReader.IsStartElement(dictionary.Claim, dictionary.EmptyString))
			{
				System.Security.Claims.Claim claim = new System.Security.Claims.Claim(dictionaryReader.GetAttribute(dictionary.Type, dictionary.EmptyString), dictionaryReader.GetAttribute(dictionary.Value, dictionary.EmptyString), dictionaryReader.GetAttribute(dictionary.ValueType, dictionary.EmptyString), dictionaryReader.GetAttribute(dictionary.Issuer, dictionary.EmptyString), dictionaryReader.GetAttribute(dictionary.OriginalIssuer, dictionary.EmptyString));
				dictionaryReader.ReadFullStartElement();
				if (dictionaryReader.IsStartElement(dictionary.ClaimProperties, dictionary.EmptyString))
				{
					this.ReadClaimProperties(dictionaryReader, dictionary, claim.Properties);
				}
				dictionaryReader.ReadEndElement();
				claims.Add(claim);
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003969C File Offset: 0x0003789C
		private void ReadClaimProperties(XmlDictionaryReader dictionaryReader, SessionDictionary dictionary, IDictionary<string, string> properties)
		{
			if (dictionaryReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryReader");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			dictionaryReader.ReadStartElement();
			while (dictionaryReader.IsStartElement(dictionary.ClaimProperty, dictionary.EmptyString))
			{
				string attribute = dictionaryReader.GetAttribute(dictionary.ClaimPropertyName, dictionary.EmptyString);
				string attribute2 = dictionaryReader.GetAttribute(dictionary.ClaimPropertyValue, dictionary.EmptyString);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4249")));
				}
				if (string.IsNullOrEmpty(attribute2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4250")));
				}
				properties.Add(new KeyValuePair<string, string>(attribute, attribute2));
				dictionaryReader.ReadFullStartElement();
				dictionaryReader.ReadEndElement();
			}
			dictionaryReader.ReadEndElement();
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00039790 File Offset: 0x00037990
		private void WritePrincipal(XmlDictionaryWriter dictionaryWriter, SessionDictionary dictionary, ClaimsPrincipal principal)
		{
			if (dictionaryWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryWriter");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (principal == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("principal");
			}
			dictionaryWriter.WriteStartElement(dictionary.ClaimsPrincipal, dictionary.EmptyString);
			if (principal.Identities != null)
			{
				this.WriteIdentities(dictionaryWriter, dictionary, principal.Identities);
			}
			dictionaryWriter.WriteEndElement();
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00039804 File Offset: 0x00037A04
		private void WriteIdentities(XmlDictionaryWriter dictionaryWriter, SessionDictionary dictionary, IEnumerable<ClaimsIdentity> identities)
		{
			if (dictionaryWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryWriter");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (identities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identities");
			}
			dictionaryWriter.WriteStartElement(dictionary.Identities, dictionary.EmptyString);
			foreach (ClaimsIdentity identity in identities)
			{
				this.WriteIdentity(dictionaryWriter, dictionary, identity);
			}
			dictionaryWriter.WriteEndElement();
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000398A0 File Offset: 0x00037AA0
		private void WriteIdentity(XmlDictionaryWriter dictionaryWriter, SessionDictionary dictionary, ClaimsIdentity identity)
		{
			if (dictionaryWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryWriter");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			dictionaryWriter.WriteStartElement(dictionary.Identity, dictionary.EmptyString);
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				dictionaryWriter.WriteAttributeString(dictionary.WindowsLogonName, dictionary.EmptyString, windowsIdentity.Name);
			}
			if (!string.IsNullOrEmpty(identity.AuthenticationType))
			{
				dictionaryWriter.WriteAttributeString(dictionary.AuthenticationType, dictionary.EmptyString, identity.AuthenticationType);
			}
			if (!string.IsNullOrEmpty(identity.Label))
			{
				dictionaryWriter.WriteAttributeString(dictionary.Label, dictionary.EmptyString, identity.Label);
			}
			if (identity.NameClaimType != null)
			{
				dictionaryWriter.WriteAttributeString(dictionary.NameClaimType, dictionary.EmptyString, identity.NameClaimType);
			}
			if (identity.RoleClaimType != null)
			{
				dictionaryWriter.WriteAttributeString(dictionary.RoleClaimType, dictionary.EmptyString, identity.RoleClaimType);
			}
			if (identity.Claims != null)
			{
				dictionaryWriter.WriteStartElement(dictionary.ClaimCollection, dictionary.EmptyString);
				IEnumerable<System.Security.Claims.Claim> claims = identity.Claims;
				SessionSecurityToken.OutboundClaimsFilter outboundClaimsFilter;
				if (windowsIdentity != null)
				{
					outboundClaimsFilter = ((System.Security.Claims.Claim c) => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid" || (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" && c.Issuer == "LOCAL AUTHORITY" && c.ValueType == "http://www.w3.org/2001/XMLSchema#string"));
				}
				else
				{
					outboundClaimsFilter = null;
				}
				this.WriteClaims(dictionaryWriter, dictionary, claims, outboundClaimsFilter);
				dictionaryWriter.WriteEndElement();
			}
			if (identity.Actor != null)
			{
				dictionaryWriter.WriteStartElement(dictionary.Actor, dictionary.EmptyString);
				this.WriteIdentity(dictionaryWriter, dictionary, identity.Actor);
				dictionaryWriter.WriteEndElement();
			}
			if (identity.BootstrapContext != null)
			{
				dictionaryWriter.WriteStartElement(dictionary.BootstrapToken, dictionary.EmptyString);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, identity.BootstrapContext);
					byte[] array = memoryStream.ToArray();
					dictionaryWriter.WriteBase64(array, 0, array.Length);
				}
				dictionaryWriter.WriteEndElement();
			}
			dictionaryWriter.WriteEndElement();
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00039A94 File Offset: 0x00037C94
		private void WriteClaims(XmlDictionaryWriter dictionaryWriter, SessionDictionary dictionary, IEnumerable<System.Security.Claims.Claim> claims, SessionSecurityToken.OutboundClaimsFilter outboundClaimsFilter)
		{
			if (dictionaryWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryWriter");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claims");
			}
			foreach (System.Security.Claims.Claim claim in claims)
			{
				if (claim != null && (outboundClaimsFilter == null || !outboundClaimsFilter(claim)))
				{
					dictionaryWriter.WriteStartElement(dictionary.Claim, dictionary.EmptyString);
					if (!string.IsNullOrEmpty(claim.Issuer))
					{
						dictionaryWriter.WriteAttributeString(dictionary.Issuer, dictionary.EmptyString, claim.Issuer);
					}
					if (!string.IsNullOrEmpty(claim.OriginalIssuer))
					{
						dictionaryWriter.WriteAttributeString(dictionary.OriginalIssuer, dictionary.EmptyString, claim.OriginalIssuer);
					}
					dictionaryWriter.WriteAttributeString(dictionary.Type, dictionary.EmptyString, claim.Type);
					dictionaryWriter.WriteAttributeString(dictionary.Value, dictionary.EmptyString, claim.Value);
					dictionaryWriter.WriteAttributeString(dictionary.ValueType, dictionary.EmptyString, claim.ValueType);
					if (claim.Properties != null && claim.Properties.Count > 0)
					{
						this.WriteClaimProperties(dictionaryWriter, dictionary, claim.Properties);
					}
					dictionaryWriter.WriteEndElement();
				}
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00039BFC File Offset: 0x00037DFC
		private void WriteClaimProperties(XmlDictionaryWriter dictionaryWriter, SessionDictionary dictionary, IDictionary<string, string> properties)
		{
			if (dictionaryWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryWriter");
			}
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			if (properties.Count > 0)
			{
				dictionaryWriter.WriteStartElement(dictionary.ClaimProperties, dictionary.EmptyString);
				foreach (KeyValuePair<string, string> keyValuePair in properties)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key) && !string.IsNullOrEmpty(keyValuePair.Value))
					{
						dictionaryWriter.WriteStartElement(dictionary.ClaimProperty, dictionary.EmptyString);
						dictionaryWriter.WriteAttributeString(dictionary.ClaimPropertyName, dictionary.EmptyString, keyValuePair.Key);
						dictionaryWriter.WriteAttributeString(dictionary.ClaimPropertyValue, dictionary.EmptyString, keyValuePair.Value);
						dictionaryWriter.WriteEndElement();
					}
				}
				dictionaryWriter.WriteEndElement();
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00039D00 File Offset: 0x00037F00
		private void SerializeSysClaim(System.IdentityModel.Claims.Claim claim, XmlDictionaryWriter writer)
		{
			SessionDictionary instance = SessionDictionary.Instance;
			if (claim == null)
			{
				writer.WriteElementString(instance.NullValue, instance.EmptyString, string.Empty);
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Sid.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.WindowsSidClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				SessionSecurityToken.SerializeSid((SecurityIdentifier)claim.Resource, instance, writer);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.DenyOnlySid.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.DenyOnlySidClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				SessionSecurityToken.SerializeSid((SecurityIdentifier)claim.Resource, instance, writer);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.X500DistinguishedName.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.X500DistinguishedNameClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				byte[] rawData = ((X500DistinguishedName)claim.Resource).RawData;
				writer.WriteBase64(rawData, 0, rawData.Length);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Thumbprint.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.X509ThumbprintClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				byte[] array = (byte[])claim.Resource;
				writer.WriteBase64(array, 0, array.Length);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Name.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.NameClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Dns.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.DnsClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Rsa.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.RsaClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString(((RSA)claim.Resource).ToXmlString(false));
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Email.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.MailAddressClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString(((MailAddress)claim.Resource).Address);
				writer.WriteEndElement();
				return;
			}
			if (claim == System.IdentityModel.Claims.Claim.System)
			{
				writer.WriteElementString(instance.SystemClaim, instance.EmptyString, string.Empty);
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Hash.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.HashClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				byte[] array2 = (byte[])claim.Resource;
				writer.WriteBase64(array2, 0, array2.Length);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Spn.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.SpnClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Upn.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.UpnClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString((string)claim.Resource);
				writer.WriteEndElement();
				return;
			}
			if (System.IdentityModel.Claims.ClaimTypes.Uri.Equals(claim.ClaimType))
			{
				writer.WriteStartElement(instance.UrlClaim, instance.EmptyString);
				SessionSecurityToken.WriteRightAttribute(claim, instance, writer);
				writer.WriteString(((Uri)claim.Resource).AbsoluteUri);
				writer.WriteEndElement();
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4290", new object[]
			{
				claim
			})));
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003A0CC File Offset: 0x000382CC
		private System.IdentityModel.Claims.Claim DeserializeSysClaim(XmlDictionaryReader reader)
		{
			SessionDictionary instance = SessionDictionary.Instance;
			if (reader.IsStartElement(instance.NullValue, instance.EmptyString))
			{
				reader.ReadElementString();
				return null;
			}
			if (reader.IsStartElement(instance.WindowsSidClaim, instance.EmptyString))
			{
				string right = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				byte[] binaryForm = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Sid, new SecurityIdentifier(binaryForm, 0), right);
			}
			if (reader.IsStartElement(instance.DenyOnlySidClaim, instance.EmptyString))
			{
				string right2 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				byte[] binaryForm2 = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.DenyOnlySid, new SecurityIdentifier(binaryForm2, 0), right2);
			}
			if (reader.IsStartElement(instance.X500DistinguishedNameClaim, instance.EmptyString))
			{
				string right3 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				byte[] encodedDistinguishedName = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.X500DistinguishedName, new X500DistinguishedName(encodedDistinguishedName), right3);
			}
			if (reader.IsStartElement(instance.X509ThumbprintClaim, instance.EmptyString))
			{
				string right4 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				byte[] resource = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Thumbprint, resource, right4);
			}
			if (reader.IsStartElement(instance.NameClaim, instance.EmptyString))
			{
				string right5 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string resource2 = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Name, resource2, right5);
			}
			if (reader.IsStartElement(instance.DnsClaim, instance.EmptyString))
			{
				string right6 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string resource3 = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Dns, resource3, right6);
			}
			if (reader.IsStartElement(instance.RsaClaim, instance.EmptyString))
			{
				string right7 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string xmlString = reader.ReadString();
				reader.ReadEndElement();
				RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
				rsacryptoServiceProvider.FromXmlString(xmlString);
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Rsa, rsacryptoServiceProvider, right7);
			}
			if (reader.IsStartElement(instance.MailAddressClaim, instance.EmptyString))
			{
				string right8 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string address = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Email, new MailAddress(address), right8);
			}
			if (reader.IsStartElement(instance.SystemClaim, instance.EmptyString))
			{
				reader.ReadElementString();
				return System.IdentityModel.Claims.Claim.System;
			}
			if (reader.IsStartElement(instance.HashClaim, instance.EmptyString))
			{
				string right9 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				byte[] resource4 = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Hash, resource4, right9);
			}
			if (reader.IsStartElement(instance.SpnClaim, instance.EmptyString))
			{
				string right10 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string resource5 = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Spn, resource5, right10);
			}
			if (reader.IsStartElement(instance.UpnClaim, instance.EmptyString))
			{
				string right11 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string resource6 = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Upn, resource6, right11);
			}
			if (reader.IsStartElement(instance.UrlClaim, instance.EmptyString))
			{
				string right12 = SessionSecurityToken.ReadRightAttribute(reader, instance);
				reader.ReadStartElement();
				string uriString = reader.ReadString();
				reader.ReadEndElement();
				return new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Uri, new Uri(uriString), right12);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4289", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003A474 File Offset: 0x00038674
		private static void SerializeSid(SecurityIdentifier sid, SessionDictionary dictionary, XmlDictionaryWriter writer)
		{
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			writer.WriteBase64(array, 0, array.Length);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003A4A0 File Offset: 0x000386A0
		private static string ReadRightAttribute(XmlDictionaryReader reader, SessionDictionary dictionary)
		{
			string attribute = reader.GetAttribute(dictionary.Right, dictionary.EmptyString);
			if (!string.IsNullOrEmpty(attribute))
			{
				return attribute;
			}
			return Rights.PossessProperty;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003A4CF File Offset: 0x000386CF
		private static void WriteRightAttribute(System.IdentityModel.Claims.Claim claim, SessionDictionary dictionary, XmlDictionaryWriter writer)
		{
			if (Rights.PossessProperty.Equals(claim.Right))
			{
				return;
			}
			writer.WriteAttributeString(dictionary.Right, dictionary.EmptyString, claim.Right);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003A4FC File Offset: 0x000386FC
		private static bool IsPossibleUpn(string name)
		{
			int num = name.IndexOf('@');
			return name.Length >= 3 && num >= 0 && num != 0 && num != name.Length - 1;
		}

		// Token: 0x04000C6E RID: 3182
		private const string SupportedVersion = "1";

		// Token: 0x04000C6F RID: 3183
		private const string tokenKey = "SessionToken";

		// Token: 0x04000C70 RID: 3184
		private const string WindowsSecurityTokenStubElementName = "WindowsSecurityTokenStub";

		// Token: 0x04000C71 RID: 3185
		private static Dictionary<string, string> DomainNameMap = new Dictionary<string, string>(50, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C72 RID: 3186
		private const int MaxDomainNameMapSize = 50;

		// Token: 0x04000C73 RID: 3187
		private static Random rnd = new Random();

		// Token: 0x04000C74 RID: 3188
		private string _context;

		// Token: 0x04000C75 RID: 3189
		private bool _isPersistent;

		// Token: 0x04000C76 RID: 3190
		private ClaimsPrincipal _claimsPrincipal;

		// Token: 0x04000C77 RID: 3191
		private SctAuthorizationPolicy _sctAuthorizationPolicy;

		// Token: 0x04000C78 RID: 3192
		private string _endpointId;

		// Token: 0x04000C79 RID: 3193
		private bool _isReferenceMode;

		// Token: 0x04000C7A RID: 3194
		private bool _isSecurityContextSecurityTokenWrapper;

		// Token: 0x04000C7B RID: 3195
		private string _id;

		// Token: 0x04000C7C RID: 3196
		private UniqueId _contextId;

		// Token: 0x04000C7D RID: 3197
		private UniqueId _keyGeneration;

		// Token: 0x04000C7E RID: 3198
		private DateTime _keyEffectiveTime;

		// Token: 0x04000C7F RID: 3199
		private DateTime _keyExpirationTime;

		// Token: 0x04000C80 RID: 3200
		private Uri _secureConversationVersion;

		// Token: 0x04000C81 RID: 3201
		private DateTime _validFrom;

		// Token: 0x04000C82 RID: 3202
		private DateTime _validTo;

		// Token: 0x04000C83 RID: 3203
		private ReadOnlyCollection<SecurityKey> _securityKeys;

		// Token: 0x02000271 RID: 625
		// (Invoke) Token: 0x06001297 RID: 4759
		private delegate bool OutboundClaimsFilter(System.Security.Claims.Claim claim);
	}
}
