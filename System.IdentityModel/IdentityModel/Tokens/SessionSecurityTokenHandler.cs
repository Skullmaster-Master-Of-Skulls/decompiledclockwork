using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Selectors;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.ServiceModel.Security;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000182 RID: 386
	public class SessionSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x0003A702 File Offset: 0x00038902
		public SessionSecurityTokenHandler() : this(SessionSecurityTokenHandler.DefaultCookieTransforms)
		{
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003A70F File Offset: 0x0003890F
		public SessionSecurityTokenHandler(ReadOnlyCollection<CookieTransform> transforms) : this(transforms, SessionSecurityTokenHandler.DefaultLifetime)
		{
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003A720 File Offset: 0x00038920
		public SessionSecurityTokenHandler(ReadOnlyCollection<CookieTransform> transforms, TimeSpan tokenLifetime)
		{
			if (transforms == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transforms");
			}
			if (tokenLifetime <= TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID0016")));
			}
			this._transforms = transforms;
			this._tokenLifetime = tokenLifetime;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003A788 File Offset: 0x00038988
		public override void LoadCustomConfiguration(XmlNodeList customConfigElements)
		{
			if (customConfigElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("customConfigElements");
			}
			List<XmlElement> xmlElements = XmlUtil.GetXmlElements(customConfigElements);
			bool flag = false;
			foreach (XmlElement xmlElement in xmlElements)
			{
				if (StringComparer.Ordinal.Equals(xmlElement.LocalName, "sessionTokenRequirement"))
				{
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7026", new object[]
						{
							"sessionTokenRequirement"
						})));
					}
					this._tokenLifetime = SessionSecurityTokenHandler.DefaultLifetime;
					foreach (object obj in xmlElement.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (!StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "lifetime"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7004", new object[]
							{
								xmlAttribute.LocalName,
								xmlElement.LocalName
							})));
						}
						TimeSpan defaultLifetime = SessionSecurityTokenHandler.DefaultLifetime;
						if (!TimeSpan.TryParse(xmlAttribute.Value, out defaultLifetime))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7017", new object[]
							{
								xmlAttribute.Value
							})));
						}
						if (defaultLifetime < TimeSpan.Zero)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7018")));
						}
						this._tokenLifetime = defaultLifetime;
					}
					flag = true;
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0003A968 File Offset: 0x00038B68
		public virtual string CookieElementName
		{
			get
			{
				return "Cookie";
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003A96F File Offset: 0x00038B6F
		public virtual string CookieNamespace
		{
			get
			{
				return "http://schemas.microsoft.com/ws/2006/05/security";
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003A978 File Offset: 0x00038B78
		protected virtual byte[] ApplyTransforms(byte[] cookie, bool outbound)
		{
			byte[] array = cookie;
			if (this.Transforms == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4296")));
			}
			if (outbound)
			{
				for (int i = 0; i < this._transforms.Count; i++)
				{
					array = this._transforms[i].Encode(array);
				}
			}
			else
			{
				for (int j = this._transforms.Count; j > 0; j--)
				{
					array = this._transforms[j - 1].Decode(array);
				}
			}
			return array;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003AA03 File Offset: 0x00038C03
		public override bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("SecurityContextToken", "http://schemas.xmlsoap.org/ws/2005/02/sc") || reader.IsStartElement("SecurityContextToken", "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4272")));
			}
			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(tokenDescriptor.Subject);
			if (base.Configuration.SaveBootstrapContext)
			{
				SecurityTokenHandlerCollection securityTokenHandlerCollection = this.CreateBootstrapTokenHandlerCollection();
				if (!securityTokenHandlerCollection.CanWriteToken(tokenDescriptor.Token))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4010", new object[]
					{
						tokenDescriptor.Token.GetType().ToString()
					})));
				}
				(claimsPrincipal.Identities as ReadOnlyCollection<ClaimsIdentity>)[0].BootstrapContext = new BootstrapContext(tokenDescriptor.Token, securityTokenHandlerCollection[tokenDescriptor.Token.GetType()]);
			}
			DateTime value = (tokenDescriptor.Lifetime.Created != null) ? tokenDescriptor.Lifetime.Created.Value : DateTime.UtcNow;
			DateTime value2 = (tokenDescriptor.Lifetime.Expires != null) ? tokenDescriptor.Lifetime.Expires.Value : (DateTime.UtcNow + SessionSecurityTokenHandler.DefaultTokenLifetime);
			return new SessionSecurityToken(claimsPrincipal, null, new DateTime?(value), new DateTime?(value2));
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003AB94 File Offset: 0x00038D94
		public virtual SessionSecurityToken CreateSessionSecurityToken(ClaimsPrincipal principal, string context, string endpointId, DateTime validFrom, DateTime validTo)
		{
			if (principal == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("principal");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4272")));
			}
			return new SessionSecurityToken(principal, context, endpointId, new DateTime?(validFrom), new DateTime?(validTo));
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public static TimeSpan DefaultTokenLifetime
		{
			get
			{
				return SessionSecurityTokenHandler.DefaultLifetime;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003ABF4 File Offset: 0x00038DF4
		public virtual SecurityToken ReadToken(byte[] token, SecurityTokenResolver tokenResolver)
		{
			SecurityToken result;
			using (XmlReader xmlReader = XmlDictionaryReader.CreateTextReader(token, XmlDictionaryReaderQuotas.Max))
			{
				result = this.ReadToken(xmlReader, tokenResolver);
			}
			return result;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003AC34 File Offset: 0x00038E34
		public override SecurityToken ReadToken(XmlReader reader)
		{
			return this.ReadToken(reader, EmptySecurityTokenResolver.Instance);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003AC44 File Offset: 0x00038E44
		public override SecurityToken ReadToken(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (tokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolver");
			}
			UniqueId uniqueId = null;
			UniqueId uniqueId2 = null;
			SecurityToken securityToken = null;
			SessionDictionary instance = SessionDictionary.Instance;
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			string ns;
			string localname;
			string localname2;
			if (xmlDictionaryReader.IsStartElement("SecurityContextToken", "http://schemas.xmlsoap.org/ws/2005/02/sc"))
			{
				ns = "http://schemas.xmlsoap.org/ws/2005/02/sc";
				localname = "Identifier";
				localname2 = "Instance";
			}
			else
			{
				if (!xmlDictionaryReader.IsStartElement("SecurityContextToken", "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
					{
						"SecurityContextToken",
						xmlDictionaryReader.Name
					})));
				}
				ns = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512";
				localname = "Identifier";
				localname2 = "Instance";
			}
			string attribute = xmlDictionaryReader.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			xmlDictionaryReader.ReadFullStartElement();
			if (!xmlDictionaryReader.IsStartElement(localname, ns))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4230", new object[]
				{
					"Identifier",
					xmlDictionaryReader.Name
				})));
			}
			uniqueId = xmlDictionaryReader.ReadElementContentAsUniqueId();
			if (uniqueId == null || string.IsNullOrEmpty(uniqueId.ToString()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4242")));
			}
			if (xmlDictionaryReader.IsStartElement(localname2, ns))
			{
				uniqueId2 = xmlDictionaryReader.ReadElementContentAsUniqueId();
			}
			if (xmlDictionaryReader.IsStartElement(this.CookieElementName, this.CookieNamespace))
			{
				SecurityToken securityToken2 = null;
				SecurityContextKeyIdentifierClause keyIdentifierClause;
				if (uniqueId2 == null)
				{
					keyIdentifierClause = new SecurityContextKeyIdentifierClause(uniqueId);
				}
				else
				{
					keyIdentifierClause = new SecurityContextKeyIdentifierClause(uniqueId, uniqueId2);
				}
				tokenResolver.TryResolveToken(keyIdentifierClause, out securityToken2);
				if (securityToken2 != null)
				{
					securityToken = securityToken2;
					xmlDictionaryReader.Skip();
				}
				else
				{
					byte[] array = xmlDictionaryReader.ReadElementContentAsBase64();
					if (array == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4237")));
					}
					byte[] buffer = this.ApplyTransforms(array, false);
					using (MemoryStream memoryStream = new MemoryStream(buffer))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						securityToken = (binaryFormatter.Deserialize(memoryStream) as SecurityToken);
					}
					SessionSecurityToken sessionSecurityToken = securityToken as SessionSecurityToken;
					if (sessionSecurityToken != null && sessionSecurityToken.ContextId != uniqueId)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4229", new object[]
						{
							sessionSecurityToken.ContextId,
							uniqueId
						})));
					}
					if (sessionSecurityToken != null && sessionSecurityToken.Id != attribute)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4227", new object[]
						{
							sessionSecurityToken.Id,
							attribute
						})));
					}
				}
			}
			else
			{
				SecurityToken securityToken3 = null;
				SecurityContextKeyIdentifierClause keyIdentifierClause2;
				if (uniqueId2 == null)
				{
					keyIdentifierClause2 = new SecurityContextKeyIdentifierClause(uniqueId);
				}
				else
				{
					keyIdentifierClause2 = new SecurityContextKeyIdentifierClause(uniqueId, uniqueId2);
				}
				tokenResolver.TryResolveToken(keyIdentifierClause2, out securityToken3);
				if (securityToken3 != null)
				{
					securityToken = securityToken3;
				}
			}
			xmlDictionaryReader.ReadEndElement();
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4243")));
			}
			return securityToken;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0003AF70 File Offset: 0x00039170
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0003AF78 File Offset: 0x00039178
		public virtual TimeSpan TokenLifetime
		{
			get
			{
				return this._tokenLifetime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0016"));
				}
				this._tokenLifetime = value;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003AFA8 File Offset: 0x000391A8
		private SecurityTokenHandlerCollection CreateBootstrapTokenHandlerCollection()
		{
			return base.ContainingCollection ?? SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003AFC6 File Offset: 0x000391C6
		public override string[] GetTokenTypeIdentifiers()
		{
			return new string[]
			{
				"http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecureConversation",
				"http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct",
				"http://schemas.xmlsoap.org/ws/2005/02/sc/sct"
			};
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0003AFE6 File Offset: 0x000391E6
		public override Type TokenType
		{
			get
			{
				return typeof(SessionSecurityToken);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0003AFF2 File Offset: 0x000391F2
		public ReadOnlyCollection<CookieTransform> Transforms
		{
			get
			{
				return this._transforms;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003AFFA File Offset: 0x000391FA
		protected void SetTransforms(IEnumerable<CookieTransform> transforms)
		{
			this._transforms = new List<CookieTransform>(transforms).AsReadOnly();
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0003B010 File Offset: 0x00039210
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SessionSecurityToken sessionSecurityToken = token as SessionSecurityToken;
			if (sessionSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4292", new object[]
				{
					token.GetType().ToString(),
					base.GetType().ToString()
				})));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				if (DiagnosticUtility.ShouldTrace(TraceEventType.Verbose))
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 786438, SR.GetString("TraceValidateToken"), new SecurityTraceRecordHelper.TokenTraceRecord(token), null, null);
				}
				this.ValidateSession(sessionSecurityToken);
				base.TraceTokenValidationSuccess(token);
				List<ClaimsIdentity> list = new List<ClaimsIdentity>(1);
				list.AddRange(sessionSecurityToken.ClaimsPrincipal.Identities);
				result = list.AsReadOnly();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.TraceTokenValidationFailure(token, ex.Message);
				throw ex;
			}
			return result;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003B0F8 File Offset: 0x000392F8
		public virtual ReadOnlyCollection<ClaimsIdentity> ValidateToken(SessionSecurityToken token, string endpointId)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointId");
			}
			if (!string.IsNullOrEmpty(token.EndpointId) && token.EndpointId != endpointId)
			{
				string @string = SR.GetString("ID4291", new object[]
				{
					token
				});
				base.TraceTokenValidationFailure(token, @string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(@string));
			}
			return this.ValidateToken(token);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003B17C File Offset: 0x0003937C
		protected virtual void ValidateSession(SessionSecurityToken securityToken)
		{
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityToken");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4274")));
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew);
			DateTime t2 = DateTimeUtil.Add(utcNow, -base.Configuration.MaxClockSkew);
			if (securityToken.ValidFrom > t)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenNotYetValidException(SR.GetString("ID4255", new object[]
				{
					securityToken.ValidTo,
					securityToken.ValidFrom,
					utcNow
				})));
			}
			if (securityToken.ValidTo < t2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenExpiredException(SR.GetString("ID4255", new object[]
				{
					securityToken.ValidTo,
					securityToken.ValidFrom,
					utcNow
				})));
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003B294 File Offset: 0x00039494
		public virtual byte[] WriteToken(SessionSecurityToken sessionToken)
		{
			if (sessionToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sessionToken");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
				{
					this.WriteToken(xmlWriter, sessionToken);
					xmlWriter.Flush();
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003B30C File Offset: 0x0003950C
		public override void WriteToken(XmlWriter writer, SecurityToken token)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SessionSecurityToken sessionSecurityToken = token as SessionSecurityToken;
			if (sessionSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4046", new object[]
				{
					token,
					this.TokenType
				})));
			}
			string ns;
			string localName;
			string localName2;
			string localName3;
			if (sessionSecurityToken.SecureConversationVersion == WSSecureConversationFeb2005Constants.NamespaceUri)
			{
				ns = "http://schemas.xmlsoap.org/ws/2005/02/sc";
				localName = "SecurityContextToken";
				localName2 = "Identifier";
				localName3 = "Instance";
			}
			else
			{
				if (!(sessionSecurityToken.SecureConversationVersion == WSSecureConversation13Constants.NamespaceUri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4050")));
				}
				ns = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512";
				localName = "SecurityContextToken";
				localName2 = "Identifier";
				localName3 = "Instance";
			}
			SessionDictionary instance = SessionDictionary.Instance;
			XmlDictionaryWriter xmlDictionaryWriter;
			if (writer is XmlDictionaryWriter)
			{
				xmlDictionaryWriter = (XmlDictionaryWriter)writer;
			}
			else
			{
				xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			}
			xmlDictionaryWriter.WriteStartElement(localName, ns);
			if (sessionSecurityToken.Id != null)
			{
				xmlDictionaryWriter.WriteAttributeString("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", sessionSecurityToken.Id);
			}
			xmlDictionaryWriter.WriteElementString(localName2, ns, sessionSecurityToken.ContextId.ToString());
			if (sessionSecurityToken.KeyGeneration != null)
			{
				xmlDictionaryWriter.WriteStartElement(localName3, ns);
				xmlDictionaryWriter.WriteValue(sessionSecurityToken.KeyGeneration);
				xmlDictionaryWriter.WriteEndElement();
			}
			if (!sessionSecurityToken.IsReferenceMode)
			{
				xmlDictionaryWriter.WriteStartElement(this.CookieElementName, this.CookieNamespace);
				byte[] array;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, token);
					array = memoryStream.ToArray();
				}
				array = this.ApplyTransforms(array, true);
				xmlDictionaryWriter.WriteBase64(array, 0, array.Length);
				xmlDictionaryWriter.WriteEndElement();
			}
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.Flush();
		}

		// Token: 0x04000C88 RID: 3208
		private const string DefaultCookieElementName = "Cookie";

		// Token: 0x04000C89 RID: 3209
		private const string DefaultCookieNamespace = "http://schemas.microsoft.com/ws/2006/05/security";

		// Token: 0x04000C8A RID: 3210
		private const string SecureConversationTokenIdentifier = "http://schemas.microsoft.com/ws/2006/05/servicemodel/tokens/SecureConversation";

		// Token: 0x04000C8B RID: 3211
		public static readonly TimeSpan DefaultLifetime = TimeSpan.FromHours(10.0);

		// Token: 0x04000C8C RID: 3212
		public static readonly ReadOnlyCollection<CookieTransform> DefaultCookieTransforms = new List<CookieTransform>(new CookieTransform[]
		{
			new DeflateCookieTransform(),
			new ProtectedDataCookieTransform()
		}).AsReadOnly();

		// Token: 0x04000C8D RID: 3213
		private TimeSpan _tokenLifetime = SessionSecurityTokenHandler.DefaultLifetime;

		// Token: 0x04000C8E RID: 3214
		private ReadOnlyCollection<CookieTransform> _transforms;
	}
}
