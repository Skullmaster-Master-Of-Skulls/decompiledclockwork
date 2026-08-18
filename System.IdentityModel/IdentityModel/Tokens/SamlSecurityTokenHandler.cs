using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015F RID: 351
	public class SamlSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x000305F9 File Offset: 0x0002E7F9
		public SamlSecurityTokenHandler() : this(new SamlSecurityTokenRequirement())
		{
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00030606 File Offset: 0x0002E806
		public SamlSecurityTokenHandler(SamlSecurityTokenRequirement samlSecurityTokenRequirement)
		{
			if (samlSecurityTokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSecurityTokenRequirement");
			}
			this._samlSecurityTokenRequirement = samlSecurityTokenRequirement;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00030634 File Offset: 0x0002E834
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
				if (!(xmlElement.LocalName != "samlSecurityTokenRequirement"))
				{
					if (flag)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7026", new object[]
						{
							"samlSecurityTokenRequirement"
						}));
					}
					this._samlSecurityTokenRequirement = new SamlSecurityTokenRequirement(xmlElement);
					flag = true;
				}
			}
			if (!flag)
			{
				this._samlSecurityTokenRequirement = new SamlSecurityTokenRequirement();
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000306E8 File Offset: 0x0002E8E8
		public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			IEnumerable<SamlStatement> statements = this.CreateStatements(tokenDescriptor);
			SamlConditions conditions = this.CreateConditions(tokenDescriptor.Lifetime, tokenDescriptor.AppliesToAddress, tokenDescriptor);
			SamlAdvice advice = this.CreateAdvice(tokenDescriptor);
			string tokenIssuerName = tokenDescriptor.TokenIssuerName;
			SamlAssertion samlAssertion = this.CreateAssertion(tokenIssuerName, conditions, advice, statements);
			if (samlAssertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4013")));
			}
			samlAssertion.SigningCredentials = this.GetSigningCredentials(tokenDescriptor);
			SecurityToken securityToken = new SamlSecurityToken(samlAssertion);
			EncryptingCredentials encryptingCredentials = this.GetEncryptingCredentials(tokenDescriptor);
			if (encryptingCredentials != null)
			{
				securityToken = new EncryptedSecurityToken(securityToken, encryptingCredentials);
			}
			return securityToken;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00030790 File Offset: 0x0002E990
		protected virtual EncryptingCredentials GetEncryptingCredentials(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			EncryptingCredentials encryptingCredentials = null;
			if (tokenDescriptor.EncryptingCredentials != null)
			{
				encryptingCredentials = tokenDescriptor.EncryptingCredentials;
				if (encryptingCredentials.SecurityKey is AsymmetricSecurityKey)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4178")));
				}
			}
			return encryptingCredentials;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000269F1 File Offset: 0x00024BF1
		protected virtual SigningCredentials GetSigningCredentials(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			return tokenDescriptor.SigningCredentials;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00003459 File Offset: 0x00001659
		protected virtual SamlAdvice CreateAdvice(SecurityTokenDescriptor tokenDescriptor)
		{
			return null;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000307E9 File Offset: 0x0002E9E9
		protected virtual SamlAssertion CreateAssertion(string issuer, SamlConditions conditions, SamlAdvice advice, IEnumerable<SamlStatement> statements)
		{
			return new SamlAssertion(UniqueId.CreateRandomId(), issuer, DateTime.UtcNow, conditions, advice, statements);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000307FF File Offset: 0x0002E9FF
		public override SecurityKeyIdentifierClause CreateSecurityTokenReference(SecurityToken token, bool attached)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return token.CreateKeyIdentifierClause<SamlAssertionKeyIdentifierClause>();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003081C File Offset: 0x0002EA1C
		protected virtual SamlConditions CreateConditions(Lifetime tokenLifetime, string relyingPartyAddress, SecurityTokenDescriptor tokenDescriptor)
		{
			SamlConditions samlConditions = new SamlConditions();
			if (tokenLifetime != null)
			{
				if (tokenLifetime.Created != null)
				{
					samlConditions.NotBefore = tokenLifetime.Created.Value;
				}
				if (tokenLifetime.Expires != null)
				{
					samlConditions.NotOnOrAfter = tokenLifetime.Expires.Value;
				}
			}
			if (!string.IsNullOrEmpty(relyingPartyAddress))
			{
				samlConditions.Conditions.Add(new SamlAudienceRestrictionCondition(new Uri[]
				{
					new Uri(relyingPartyAddress)
				}));
			}
			return samlConditions;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000308A4 File Offset: 0x0002EAA4
		protected virtual IEnumerable<SamlStatement> CreateStatements(SecurityTokenDescriptor tokenDescriptor)
		{
			Collection<SamlStatement> collection = new Collection<SamlStatement>();
			SamlSubject samlSubject = this.CreateSamlSubject(tokenDescriptor);
			SamlAttributeStatement samlAttributeStatement = this.CreateAttributeStatement(samlSubject, tokenDescriptor.Subject, tokenDescriptor);
			if (samlAttributeStatement != null)
			{
				collection.Add(samlAttributeStatement);
			}
			SamlAuthenticationStatement samlAuthenticationStatement = this.CreateAuthenticationStatement(samlSubject, tokenDescriptor.AuthenticationInfo, tokenDescriptor);
			if (samlAuthenticationStatement != null)
			{
				collection.Add(samlAuthenticationStatement);
			}
			return collection;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000308F4 File Offset: 0x0002EAF4
		protected virtual SamlAuthenticationStatement CreateAuthenticationStatement(SamlSubject samlSubject, AuthenticationInformation authInfo, SecurityTokenDescriptor tokenDescriptor)
		{
			if (samlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSubject");
			}
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			if (tokenDescriptor.Subject == null)
			{
				return null;
			}
			string text = null;
			string text2 = null;
			IEnumerable<Claim> source = from c in tokenDescriptor.Subject.Claims
			where c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod"
			select c;
			if (source.Count<Claim>() > 0)
			{
				text = source.First<Claim>().Value;
			}
			source = from c in tokenDescriptor.Subject.Claims
			where c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant"
			select c;
			if (source.Count<Claim>() > 0)
			{
				text2 = source.First<Claim>().Value;
			}
			if (text == null && text2 == null)
			{
				return null;
			}
			if (text == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4270", new object[]
				{
					"AuthenticationMethod",
					"SAML11"
				}));
			}
			if (text2 == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4270", new object[]
				{
					"AuthenticationInstant",
					"SAML11"
				}));
			}
			DateTime authenticationInstant = DateTime.ParseExact(text2, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			if (authInfo == null)
			{
				return new SamlAuthenticationStatement(samlSubject, this.DenormalizeAuthenticationType(text), authenticationInstant, null, null, null);
			}
			return new SamlAuthenticationStatement(samlSubject, this.DenormalizeAuthenticationType(text), authenticationInstant, authInfo.DnsName, authInfo.Address, null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00030A64 File Offset: 0x0002EC64
		protected virtual SamlAttributeStatement CreateAttributeStatement(SamlSubject samlSubject, ClaimsIdentity subject, SecurityTokenDescriptor tokenDescriptor)
		{
			if (subject == null)
			{
				return null;
			}
			if (samlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSubject");
			}
			if (subject.Claims != null)
			{
				List<SamlAttribute> list = new List<SamlAttribute>();
				foreach (Claim claim in subject.Claims)
				{
					if (claim != null && claim.Type != "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
					{
						string type = claim.Type;
						if (!(type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant") && !(type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod"))
						{
							list.Add(this.CreateAttribute(claim, tokenDescriptor));
						}
					}
				}
				this.AddDelegateToAttributes(subject, list, tokenDescriptor);
				ICollection<SamlAttribute> collection = this.CollectAttributeValues(list);
				if (collection.Count > 0)
				{
					return new SamlAttributeStatement(samlSubject, collection);
				}
			}
			return null;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00030B40 File Offset: 0x0002ED40
		protected virtual ICollection<SamlAttribute> CollectAttributeValues(ICollection<SamlAttribute> attributes)
		{
			Dictionary<SamlAttributeKeyComparer.AttributeKey, SamlAttribute> dictionary = new Dictionary<SamlAttributeKeyComparer.AttributeKey, SamlAttribute>(attributes.Count, new SamlAttributeKeyComparer());
			foreach (SamlAttribute samlAttribute in attributes)
			{
				SamlAttribute samlAttribute2 = samlAttribute;
				if (samlAttribute2 != null)
				{
					SamlAttributeKeyComparer.AttributeKey key = new SamlAttributeKeyComparer.AttributeKey(samlAttribute2);
					if (dictionary.ContainsKey(key))
					{
						using (IEnumerator<string> enumerator2 = samlAttribute2.AttributeValues.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string item = enumerator2.Current;
								dictionary[key].AttributeValues.Add(item);
							}
							continue;
						}
					}
					dictionary.Add(key, samlAttribute2);
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00030C08 File Offset: 0x0002EE08
		protected virtual void AddDelegateToAttributes(ClaimsIdentity subject, ICollection<SamlAttribute> attributes, SecurityTokenDescriptor tokenDescriptor)
		{
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			if (subject.Actor == null)
			{
				return;
			}
			List<SamlAttribute> list = new List<SamlAttribute>();
			foreach (Claim claim in subject.Actor.Claims)
			{
				if (claim != null)
				{
					list.Add(this.CreateAttribute(claim, tokenDescriptor));
				}
			}
			this.AddDelegateToAttributes(subject.Actor, list, tokenDescriptor);
			ICollection<SamlAttribute> attributes2 = this.CollectAttributeValues(list);
			attributes.Add(this.CreateAttribute(new Claim("http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor", this.CreateXmlStringFromAttributes(attributes2), "http://www.w3.org/2001/XMLSchema#string"), tokenDescriptor));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00030CD4 File Offset: 0x0002EED4
		protected virtual SamlSubject CreateSamlSubject(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			SamlSubject samlSubject = new SamlSubject();
			Claim claim = null;
			if (tokenDescriptor.Subject != null && tokenDescriptor.Subject.Claims != null)
			{
				foreach (Claim claim2 in tokenDescriptor.Subject.Claims)
				{
					if (claim2.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
					{
						if (claim != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4139")));
						}
						claim = claim2;
					}
				}
			}
			if (claim != null)
			{
				samlSubject.Name = claim.Value;
				if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"))
				{
					samlSubject.NameFormat = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"];
				}
				if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"))
				{
					samlSubject.NameQualifier = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"];
				}
			}
			if (tokenDescriptor.Proof != null)
			{
				samlSubject.KeyIdentifier = tokenDescriptor.Proof.KeyIdentifier;
				samlSubject.ConfirmationMethods.Add(SamlConstants.HolderOfKey);
			}
			else
			{
				samlSubject.ConfirmationMethods.Add("urn:oasis:names:tc:SAML:1.0:cm:bearer");
			}
			return samlSubject;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00030E1C File Offset: 0x0002F01C
		protected virtual string CreateXmlStringFromAttributes(IEnumerable<SamlAttribute> attributes)
		{
			bool flag = false;
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
				{
					foreach (SamlAttribute samlAttribute in attributes)
					{
						if (samlAttribute != null)
						{
							if (!flag)
							{
								xmlDictionaryWriter.WriteStartElement("Actor");
								flag = true;
							}
							this.WriteAttribute(xmlDictionaryWriter, samlAttribute);
						}
					}
					if (flag)
					{
						xmlDictionaryWriter.WriteEndElement();
					}
					xmlDictionaryWriter.Flush();
				}
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00030EE4 File Offset: 0x0002F0E4
		protected virtual SamlAttribute CreateAttribute(Claim claim, SecurityTokenDescriptor tokenDescriptor)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			int num = claim.Type.LastIndexOf('/');
			if (num == 0 || num == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("claimType", SR.GetString("ID4216", new object[]
				{
					claim.Type
				}));
			}
			if (num == claim.Type.Length - 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("claimType", SR.GetString("ID4216", new object[]
				{
					claim.Type
				}));
			}
			string text = claim.Type.Substring(0, num);
			if (text.EndsWith("/", StringComparison.Ordinal))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("claim", SR.GetString("ID4213", new object[]
				{
					claim.Type
				}));
			}
			string attributeName = claim.Type.Substring(num + 1, claim.Type.Length - (num + 1));
			SamlAttribute samlAttribute = new SamlAttribute(text, attributeName, new string[]
			{
				claim.Value
			});
			if (!StringComparer.Ordinal.Equals("LOCAL AUTHORITY", claim.OriginalIssuer))
			{
				samlAttribute.OriginalIssuer = claim.OriginalIssuer;
			}
			samlAttribute.AttributeValueXsiType = claim.ValueType;
			return samlAttribute;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00031029 File Offset: 0x0002F229
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x00031059 File Offset: 0x0002F259
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				if (this._samlSecurityTokenRequirement.CertificateValidator != null)
				{
					return this._samlSecurityTokenRequirement.CertificateValidator;
				}
				if (base.Configuration != null)
				{
					return base.Configuration.CertificateValidator;
				}
				return null;
			}
			set
			{
				this._samlSecurityTokenRequirement.CertificateValidator = value;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00031068 File Offset: 0x0002F268
		protected override void DetectReplayedToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SamlSecurityToken samlSecurityToken = token as SamlSecurityToken;
			if (samlSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID1067", new object[]
				{
					token.GetType().ToString()
				}));
			}
			if (samlSecurityToken.SecurityKeys.Count != 0)
			{
				return;
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.Caches.TokenReplayCache == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4278"));
			}
			if (string.IsNullOrEmpty(samlSecurityToken.Assertion.AssertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID1063")));
			}
			StringBuilder stringBuilder = new StringBuilder();
			string key;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.NewSha256HashAlgorithm())
			{
				if (string.IsNullOrEmpty(samlSecurityToken.Assertion.Issuer))
				{
					stringBuilder.AppendFormat("{0}{1}", samlSecurityToken.Assertion.AssertionId, SamlSecurityTokenHandler._tokenTypeIdentifiers[0]);
				}
				else
				{
					stringBuilder.AppendFormat("{0}{1}{2}", samlSecurityToken.Assertion.AssertionId, samlSecurityToken.Assertion.Issuer, SamlSecurityTokenHandler._tokenTypeIdentifiers[0]);
				}
				key = Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
			}
			if (!base.Configuration.Caches.TokenReplayCache.Contains(key))
			{
				base.Configuration.Caches.TokenReplayCache.AddOrUpdate(key, token, DateTimeUtil.Add(this.GetTokenReplayCacheEntryExpirationTime(samlSecurityToken), base.Configuration.MaxClockSkew));
				return;
			}
			if (string.IsNullOrEmpty(samlSecurityToken.Assertion.Issuer))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenReplayDetectedException(SR.GetString("ID1062", new object[]
				{
					typeof(SamlSecurityToken).ToString(),
					samlSecurityToken.Assertion.AssertionId,
					""
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenReplayDetectedException(SR.GetString("ID1062", new object[]
			{
				typeof(SamlSecurityToken).ToString(),
				samlSecurityToken.Assertion.AssertionId,
				samlSecurityToken.Assertion.Issuer
			})));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000312C4 File Offset: 0x0002F4C4
		protected virtual DateTime GetTokenReplayCacheEntryExpirationTime(SamlSecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			DateTime t = DateTimeUtil.Add(DateTime.UtcNow, base.Configuration.TokenReplayCacheExpirationPeriod);
			if (DateTime.Compare(t, token.ValidTo) < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID1069", new object[]
				{
					token.ValidTo.ToString(),
					base.Configuration.TokenReplayCacheExpirationPeriod.ToString()
				})));
			}
			return token.ValidTo;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003135C File Offset: 0x0002F55C
		protected virtual void ValidateConditions(SamlConditions conditions, bool enforceAudienceRestriction)
		{
			if (conditions != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime notBefore = conditions.NotBefore;
				if (DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew) < conditions.NotBefore)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenNotYetValidException(SR.GetString("ID4222", new object[]
					{
						conditions.NotBefore,
						utcNow
					})));
				}
				DateTime notOnOrAfter = conditions.NotOnOrAfter;
				if (DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew.Negate()) >= conditions.NotOnOrAfter)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenExpiredException(SR.GetString("ID4223", new object[]
					{
						conditions.NotOnOrAfter,
						utcNow
					})));
				}
			}
			if (enforceAudienceRestriction)
			{
				if (base.Configuration == null || base.Configuration.AudienceRestriction.AllowedAudienceUris.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID1032")));
				}
				bool flag = false;
				if (conditions != null && conditions.Conditions != null)
				{
					foreach (SamlCondition samlCondition in conditions.Conditions)
					{
						SamlAudienceRestrictionCondition samlAudienceRestrictionCondition = samlCondition as SamlAudienceRestrictionCondition;
						if (samlAudienceRestrictionCondition != null)
						{
							this._samlSecurityTokenRequirement.ValidateAudienceRestriction(base.Configuration.AudienceRestriction.AllowedAudienceUris, samlAudienceRestrictionCondition.Audiences);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1035")));
				}
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003150C File Offset: 0x0002F70C
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SamlSecurityToken samlSecurityToken = token as SamlSecurityToken;
			if (samlSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID1033", new object[]
				{
					token.GetType().ToString()
				}));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				if (samlSecurityToken.Assertion == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID1034"));
				}
				TraceUtility.TraceEvent(TraceEventType.Verbose, 786438, SR.GetString("TraceValidateToken"), new SecurityTraceRecordHelper.TokenTraceRecord(token), null, null);
				if (samlSecurityToken.Assertion.SigningToken == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4220")));
				}
				this.ValidateConditions(samlSecurityToken.Assertion.Conditions, this._samlSecurityTokenRequirement.ShouldEnforceAudienceRestriction(base.Configuration.AudienceRestriction.AudienceMode, samlSecurityToken));
				if (base.Configuration.DetectReplayedTokens)
				{
					this.DetectReplayedToken(samlSecurityToken);
				}
				X509SecurityToken x509SecurityToken = samlSecurityToken.Assertion.SigningToken as X509SecurityToken;
				if (x509SecurityToken != null)
				{
					try
					{
						this.CertificateValidator.Validate(x509SecurityToken.Certificate);
					}
					catch (SecurityTokenValidationException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4257", new object[]
						{
							X509Util.GetCertificateId(x509SecurityToken.Certificate)
						}), innerException));
					}
				}
				ClaimsIdentity claimsIdentity = this.CreateClaims(samlSecurityToken);
				if (this._samlSecurityTokenRequirement.MapToWindows)
				{
					WindowsIdentity windowsIdentity = this.CreateWindowsIdentity(this.FindUpn(claimsIdentity));
					windowsIdentity.AddClaims(claimsIdentity.Claims);
					claimsIdentity = windowsIdentity;
				}
				if (base.Configuration.SaveBootstrapContext)
				{
					claimsIdentity.BootstrapContext = new BootstrapContext(token, this);
				}
				base.TraceTokenValidationSuccess(token);
				result = new List<ClaimsIdentity>(1)
				{
					claimsIdentity
				}.AsReadOnly();
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

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00031740 File Offset: 0x0002F940
		protected virtual WindowsIdentity CreateWindowsIdentity(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("upn");
			}
			WindowsIdentity windowsIdentity = new WindowsIdentity(upn);
			return new WindowsIdentity(windowsIdentity.Token, "Federation", WindowsAccountType.Normal, true);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00026C20 File Offset: 0x00024E20
		protected virtual string FindUpn(ClaimsIdentity claimsIdentity)
		{
			return ClaimsHelper.FindUpn(claimsIdentity);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003177C File Offset: 0x0002F97C
		protected virtual ClaimsIdentity CreateClaims(SamlSecurityToken samlSecurityToken)
		{
			if (samlSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSecurityToken");
			}
			if (samlSecurityToken.Assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("samlSecurityToken", SR.GetString("ID1034"));
			}
			ClaimsIdentity claimsIdentity = new ClaimsIdentity("Federation", this._samlSecurityTokenRequirement.NameClaimType, this._samlSecurityTokenRequirement.RoleClaimType);
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerNameRegistry == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4277"));
			}
			string issuerName = base.Configuration.IssuerNameRegistry.GetIssuerName(samlSecurityToken.Assertion.SigningToken, samlSecurityToken.Assertion.Issuer);
			if (string.IsNullOrEmpty(issuerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4175")));
			}
			this.ProcessStatement(samlSecurityToken.Assertion.Statements, claimsIdentity, issuerName);
			return claimsIdentity;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00031873 File Offset: 0x0002FA73
		protected virtual string DenormalizeAuthenticationType(string normalizedAuthenticationType)
		{
			return AuthenticationTypeMaps.Denormalize(normalizedAuthenticationType, AuthenticationTypeMaps.Saml);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00031880 File Offset: 0x0002FA80
		protected virtual string NormalizeAuthenticationType(string saml11AuthenticationMethod)
		{
			return AuthenticationTypeMaps.Normalize(saml11AuthenticationMethod, AuthenticationTypeMaps.Saml);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00031890 File Offset: 0x0002FA90
		protected virtual void ProcessStatement(IList<SamlStatement> statements, ClaimsIdentity subject, string issuer)
		{
			if (statements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statements");
			}
			Collection<SamlAuthenticationStatement> collection = new Collection<SamlAuthenticationStatement>();
			this.ValidateStatements(statements);
			foreach (SamlStatement samlStatement in statements)
			{
				SamlAttributeStatement samlAttributeStatement = samlStatement as SamlAttributeStatement;
				if (samlAttributeStatement != null)
				{
					this.ProcessAttributeStatement(samlAttributeStatement, subject, issuer);
				}
				else
				{
					SamlAuthenticationStatement samlAuthenticationStatement = samlStatement as SamlAuthenticationStatement;
					if (samlAuthenticationStatement != null)
					{
						collection.Add(samlAuthenticationStatement);
					}
					else
					{
						SamlAuthorizationDecisionStatement samlAuthorizationDecisionStatement = samlStatement as SamlAuthorizationDecisionStatement;
						if (samlAuthorizationDecisionStatement != null)
						{
							this.ProcessAuthorizationDecisionStatement(samlAuthorizationDecisionStatement, subject, issuer);
						}
					}
				}
			}
			foreach (SamlAuthenticationStatement samlAuthenticationStatement2 in collection)
			{
				if (samlAuthenticationStatement2 != null)
				{
					this.ProcessAuthenticationStatement(samlAuthenticationStatement2, subject, issuer);
				}
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00031978 File Offset: 0x0002FB78
		protected virtual void ProcessAttributeStatement(SamlAttributeStatement samlStatement, ClaimsIdentity subject, string issuer)
		{
			if (samlStatement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlStatement");
			}
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			this.ProcessSamlSubject(samlStatement.SamlSubject, subject, issuer);
			foreach (SamlAttribute samlAttribute in samlStatement.Attributes)
			{
				string text;
				if (string.IsNullOrEmpty(samlAttribute.Namespace))
				{
					text = samlAttribute.Name;
				}
				else
				{
					if (StringComparer.Ordinal.Equals(samlAttribute.Name, "NameIdentifier"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID4094")));
					}
					int num = samlAttribute.Namespace.LastIndexOf('/');
					if (num == -1 || num != samlAttribute.Namespace.Length - 1)
					{
						text = samlAttribute.Namespace + "/" + samlAttribute.Name;
					}
					else
					{
						text = samlAttribute.Namespace + samlAttribute.Name;
					}
				}
				if (text == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
				{
					if (subject.Actor != null)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4034"));
					}
					this.SetDelegateFromAttribute(samlAttribute, subject, issuer);
				}
				else
				{
					for (int i = 0; i < samlAttribute.AttributeValues.Count; i++)
					{
						if (!StringComparer.Ordinal.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", text) || SamlSecurityTokenHandler.GetClaim(subject, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") == null)
						{
							string originalIssuer = issuer;
							SamlAttribute samlAttribute2 = samlAttribute;
							if (samlAttribute2 != null && samlAttribute2.OriginalIssuer != null)
							{
								originalIssuer = samlAttribute2.OriginalIssuer;
							}
							string valueType = "http://www.w3.org/2001/XMLSchema#string";
							if (samlAttribute2 != null)
							{
								valueType = samlAttribute2.AttributeValueXsiType;
							}
							subject.AddClaim(new Claim(text, samlAttribute.AttributeValues[i], valueType, issuer, originalIssuer));
						}
					}
				}
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00031B58 File Offset: 0x0002FD58
		private static Claim GetClaim(ClaimsIdentity subject, string claimType)
		{
			foreach (Claim claim in subject.Claims)
			{
				if (StringComparer.Ordinal.Equals(claimType, claim.Type))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00031BB8 File Offset: 0x0002FDB8
		protected virtual void ProcessSamlSubject(SamlSubject samlSubject, ClaimsIdentity subject, string issuer)
		{
			if (samlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSubject");
			}
			if (SamlSecurityTokenHandler.GetClaim(subject, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") == null && !string.IsNullOrEmpty(samlSubject.Name))
			{
				Claim claim = new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", samlSubject.Name, "http://www.w3.org/2001/XMLSchema#string", issuer);
				if (samlSubject.NameFormat != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"] = samlSubject.NameFormat;
				}
				if (samlSubject.NameQualifier != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"] = samlSubject.NameQualifier;
				}
				subject.AddClaim(claim);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00031C50 File Offset: 0x0002FE50
		protected virtual void ProcessAuthenticationStatement(SamlAuthenticationStatement samlStatement, ClaimsIdentity subject, string issuer)
		{
			if (samlStatement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlStatement");
			}
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			this.ProcessSamlSubject(samlStatement.SamlSubject, subject, issuer);
			subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", this.NormalizeAuthenticationType(samlStatement.AuthenticationMethod), "http://www.w3.org/2001/XMLSchema#string", issuer));
			subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(samlStatement.AuthenticationInstant.ToUniversalTime(), DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime", issuer));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ProcessAuthorizationDecisionStatement(SamlAuthorizationDecisionStatement samlStatement, ClaimsIdentity subject, string issuer)
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00031CE4 File Offset: 0x0002FEE4
		protected virtual void SetDelegateFromAttribute(SamlAttribute attribute, ClaimsIdentity subject, string issuer)
		{
			if (subject == null || attribute == null || attribute.AttributeValues == null || attribute.AttributeValues.Count < 1)
			{
				return;
			}
			Collection<Claim> collection = new Collection<Claim>();
			SamlAttribute samlAttribute = null;
			foreach (string text in attribute.AttributeValues)
			{
				if (text != null && text.Length > 0)
				{
					using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(text), BoundedXmlDictionaryReaderQuotas.Quotas))
					{
						xmlDictionaryReader.MoveToContent();
						xmlDictionaryReader.ReadStartElement("Actor");
						while (xmlDictionaryReader.IsStartElement("saml:Attribute"))
						{
							SamlAttribute samlAttribute2 = this.ReadAttribute(xmlDictionaryReader);
							if (samlAttribute2 != null)
							{
								string text2 = string.IsNullOrEmpty(samlAttribute2.Namespace) ? samlAttribute2.Name : (samlAttribute2.Namespace + "/" + samlAttribute2.Name);
								if (text2 == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
								{
									if (samlAttribute != null)
									{
										throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4034"));
									}
									samlAttribute = samlAttribute2;
								}
								else
								{
									string valueType = "http://www.w3.org/2001/XMLSchema#string";
									string text3 = null;
									SamlAttribute samlAttribute3 = samlAttribute2;
									if (samlAttribute3 != null)
									{
										valueType = samlAttribute3.AttributeValueXsiType;
										text3 = samlAttribute3.OriginalIssuer;
									}
									for (int i = 0; i < samlAttribute2.AttributeValues.Count; i++)
									{
										Claim item;
										if (string.IsNullOrEmpty(text3))
										{
											item = new Claim(text2, samlAttribute2.AttributeValues[i], valueType, issuer);
										}
										else
										{
											item = new Claim(text2, samlAttribute2.AttributeValues[i], valueType, issuer, text3);
										}
										collection.Add(item);
									}
								}
							}
						}
						xmlDictionaryReader.ReadEndElement();
					}
				}
			}
			subject.Actor = new ClaimsIdentity(collection, "Federation");
			this.SetDelegateFromAttribute(samlAttribute, subject.Actor, issuer);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00031EF8 File Offset: 0x000300F8
		public override bool CanReadToken(XmlReader reader)
		{
			return reader != null && reader.IsStartElement("Assertion", SamlConstants.Namespace);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00031F10 File Offset: 0x00030110
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4275"));
			}
			SamlSecurityTokenHandler.t_currentAssertionDepth = 0;
			KeyInfo.ResetReadDepth();
			System.IdentityModel.Tokens.KeyInfoSerializer.ResetReadDepth();
			SamlAssertion samlAssertion = this.ReadAssertion(reader);
			SecurityToken signingToken;
			this.TryResolveIssuerToken(samlAssertion, base.Configuration.IssuerTokenResolver, out signingToken);
			samlAssertion.SigningToken = signingToken;
			return new SamlSecurityToken(samlAssertion);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00031F8C File Offset: 0x0003018C
		protected virtual SamlAction ReadAction(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Action", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"Action",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			string attribute = reader.GetAttribute("Namespace", null);
			reader.MoveToContent();
			string text = reader.ReadString();
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4073")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return new SamlAction(text, attribute);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00032054 File Offset: 0x00030254
		protected virtual void WriteAction(XmlWriter writer, SamlAction action)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("action");
			}
			writer.WriteStartElement("saml", "Action", SamlConstants.Namespace);
			if (!string.IsNullOrEmpty(action.Namespace))
			{
				writer.WriteAttributeString("Namespace", null, action.Namespace);
			}
			writer.WriteString(action.Action);
			writer.WriteEndElement();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000320D0 File Offset: 0x000302D0
		protected virtual SamlAdvice ReadAdvice(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Advice", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"Advice",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return new SamlAdvice();
			}
			reader.MoveToContent();
			reader.Read();
			Collection<string> collection = new Collection<string>();
			Collection<SamlAssertion> collection2 = new Collection<SamlAssertion>();
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement("AssertionIDReference", SamlConstants.Namespace))
				{
					collection.Add(reader.ReadString());
					reader.ReadEndElement();
				}
				else if (reader.IsStartElement("Assertion", SamlConstants.Namespace))
				{
					SamlAssertion item = this.ReadAssertion(reader);
					collection2.Add(item);
				}
				else
				{
					TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8005", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}), new object[0]);
					reader.Skip();
				}
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return new SamlAdvice(collection, collection2);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00032218 File Offset: 0x00030418
		protected virtual void WriteAdvice(XmlWriter writer, SamlAdvice advice)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (advice == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("advice");
			}
			writer.WriteStartElement("saml", "Advice", SamlConstants.Namespace);
			if (advice.AssertionIdReferences.Count > 0)
			{
				foreach (string value in advice.AssertionIdReferences)
				{
					if (string.IsNullOrEmpty(value))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4079")));
					}
					writer.WriteElementString("saml", "AssertionIDReference", SamlConstants.Namespace, value);
				}
			}
			if (advice.Assertions.Count > 0)
			{
				foreach (SamlAssertion assertion in advice.Assertions)
				{
					this.WriteAssertion(writer, assertion);
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00032330 File Offset: 0x00030530
		protected virtual SamlAssertion ReadAssertion(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4275"));
			}
			SamlAssertion samlAssertion = new SamlAssertion();
			EnvelopedSignatureReader envelopedSignatureReader = new EnvelopedSignatureReader(reader, new SamlSecurityTokenHandler.WrappedSerializer(this, samlAssertion), base.Configuration.IssuerTokenResolver, false, true, false);
			SamlSecurityTokenHandler.t_currentAssertionDepth++;
			SamlAssertion result;
			try
			{
				if (!LocalAppContextSwitches.AllowUnlimitedXmlRecursion && SamlSecurityTokenHandler.t_currentAssertionDepth > 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4125"), new InvalidOperationException(SR.GetString("ID4194", new object[]
					{
						SamlSecurityTokenHandler.t_currentAssertionDepth,
						8
					}))));
				}
				if (!envelopedSignatureReader.IsStartElement("Assertion", SamlConstants.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
					{
						"Assertion",
						SamlConstants.Namespace,
						envelopedSignatureReader.LocalName,
						envelopedSignatureReader.NamespaceURI
					})));
				}
				string attribute = envelopedSignatureReader.GetAttribute("MajorVersion", null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4075", new object[]
					{
						"MajorVersion"
					})));
				}
				int num = XmlConvert.ToInt32(attribute);
				attribute = envelopedSignatureReader.GetAttribute("MinorVersion", null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4075", new object[]
					{
						"MinorVersion"
					})));
				}
				int num2 = XmlConvert.ToInt32(attribute);
				if (num != SamlConstants.MajorVersionValue || num2 != SamlConstants.MinorVersionValue)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4076", new object[]
					{
						num,
						num2,
						SamlConstants.MajorVersionValue,
						SamlConstants.MinorVersionValue
					})));
				}
				attribute = envelopedSignatureReader.GetAttribute("AssertionID", null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4075", new object[]
					{
						"AssertionID"
					})));
				}
				if (!XmlUtil.IsValidXmlIDValue(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4077", new object[]
					{
						attribute
					})));
				}
				samlAssertion.AssertionId = attribute;
				attribute = envelopedSignatureReader.GetAttribute("Issuer", null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4075", new object[]
					{
						"Issuer"
					})));
				}
				samlAssertion.Issuer = attribute;
				attribute = envelopedSignatureReader.GetAttribute("IssueInstant", null);
				if (!string.IsNullOrEmpty(attribute))
				{
					samlAssertion.IssueInstant = DateTime.ParseExact(attribute, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
				}
				envelopedSignatureReader.MoveToContent();
				envelopedSignatureReader.Read();
				if (envelopedSignatureReader.IsStartElement("Conditions", SamlConstants.Namespace))
				{
					samlAssertion.Conditions = this.ReadConditions(envelopedSignatureReader);
				}
				if (envelopedSignatureReader.IsStartElement("Advice", SamlConstants.Namespace))
				{
					samlAssertion.Advice = this.ReadAdvice(envelopedSignatureReader);
				}
				while (envelopedSignatureReader.IsStartElement())
				{
					samlAssertion.Statements.Add(this.ReadStatement(envelopedSignatureReader));
				}
				if (samlAssertion.Statements.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4078")));
				}
				envelopedSignatureReader.MoveToContent();
				envelopedSignatureReader.ReadEndElement();
				samlAssertion.SigningCredentials = envelopedSignatureReader.SigningCredentials;
				samlAssertion.CaptureSourceData(envelopedSignatureReader);
				result = samlAssertion;
			}
			finally
			{
				SamlSecurityTokenHandler.t_currentAssertionDepth--;
			}
			return result;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0003270C File Offset: 0x0003090C
		protected virtual void WriteAssertion(XmlWriter writer, SamlAssertion assertion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			if (assertion != null && assertion.CanWriteSourceData)
			{
				assertion.WriteSourceData(writer);
				return;
			}
			if (assertion.SigningCredentials != null)
			{
				writer = new EnvelopedSignatureWriter(writer, assertion.SigningCredentials, assertion.AssertionId, new SamlSecurityTokenHandler.WrappedSerializer(this, assertion));
			}
			writer.WriteStartElement("saml", "Assertion", SamlConstants.Namespace);
			writer.WriteAttributeString("MajorVersion", null, Convert.ToString(SamlConstants.MajorVersionValue, CultureInfo.InvariantCulture));
			writer.WriteAttributeString("MinorVersion", null, Convert.ToString(SamlConstants.MinorVersionValue, CultureInfo.InvariantCulture));
			writer.WriteAttributeString("AssertionID", null, assertion.AssertionId);
			writer.WriteAttributeString("Issuer", null, assertion.Issuer);
			writer.WriteAttributeString("IssueInstant", null, assertion.IssueInstant.ToUniversalTime().ToString(DateTimeFormats.Generated, CultureInfo.InvariantCulture));
			if (assertion.Conditions != null)
			{
				this.WriteConditions(writer, assertion.Conditions);
			}
			if (assertion.Advice != null)
			{
				this.WriteAdvice(writer, assertion.Advice);
			}
			for (int i = 0; i < assertion.Statements.Count; i++)
			{
				this.WriteStatement(writer, assertion.Statements[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0003286C File Offset: 0x00030A6C
		protected virtual SamlConditions ReadConditions(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlConditions samlConditions = new SamlConditions();
			string attribute = reader.GetAttribute("NotBefore", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				samlConditions.NotBefore = DateTime.ParseExact(attribute, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			}
			attribute = reader.GetAttribute("NotOnOrAfter", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				samlConditions.NotOnOrAfter = DateTime.ParseExact(attribute, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			}
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return samlConditions;
			}
			reader.ReadStartElement();
			while (reader.IsStartElement())
			{
				samlConditions.Conditions.Add(this.ReadCondition(reader));
			}
			reader.ReadEndElement();
			return samlConditions;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0003293C File Offset: 0x00030B3C
		protected virtual void WriteConditions(XmlWriter writer, SamlConditions conditions)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (conditions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("conditions");
			}
			writer.WriteStartElement("saml", "Conditions", SamlConstants.Namespace);
			if (conditions.NotBefore != DateTimeUtil.GetMinValue(DateTimeKind.Utc) && conditions.NotBefore != SamlSecurityTokenHandler.WCFMinValue)
			{
				writer.WriteAttributeString("NotBefore", null, conditions.NotBefore.ToUniversalTime().ToString(DateTimeFormats.Generated, DateTimeFormatInfo.InvariantInfo));
			}
			if (conditions.NotOnOrAfter != DateTimeUtil.GetMaxValue(DateTimeKind.Utc) && conditions.NotOnOrAfter != SamlSecurityTokenHandler.WCFMaxValue)
			{
				writer.WriteAttributeString("NotOnOrAfter", null, conditions.NotOnOrAfter.ToUniversalTime().ToString(DateTimeFormats.Generated, DateTimeFormatInfo.InvariantInfo));
			}
			for (int i = 0; i < conditions.Conditions.Count; i++)
			{
				this.WriteCondition(writer, conditions.Conditions[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00032A58 File Offset: 0x00030C58
		protected virtual SamlCondition ReadCondition(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement("AudienceRestrictionCondition", SamlConstants.Namespace))
			{
				return this.ReadAudienceRestrictionCondition(reader);
			}
			if (reader.IsStartElement("DoNotCacheCondition", SamlConstants.Namespace))
			{
				return this.ReadDoNotCacheCondition(reader);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4080", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00032AE0 File Offset: 0x00030CE0
		protected virtual void WriteCondition(XmlWriter writer, SamlCondition condition)
		{
			if (condition == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("condition");
			}
			SamlAudienceRestrictionCondition samlAudienceRestrictionCondition = condition as SamlAudienceRestrictionCondition;
			if (samlAudienceRestrictionCondition != null)
			{
				this.WriteAudienceRestrictionCondition(writer, samlAudienceRestrictionCondition);
				return;
			}
			SamlDoNotCacheCondition samlDoNotCacheCondition = condition as SamlDoNotCacheCondition;
			if (samlDoNotCacheCondition != null)
			{
				this.WriteDoNotCacheCondition(writer, samlDoNotCacheCondition);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4081", new object[]
			{
				condition.GetType()
			})));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00032B50 File Offset: 0x00030D50
		protected virtual SamlAudienceRestrictionCondition ReadAudienceRestrictionCondition(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("AudienceRestrictionCondition", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"AudienceRestrictionCondition",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			reader.ReadStartElement();
			SamlAudienceRestrictionCondition samlAudienceRestrictionCondition = new SamlAudienceRestrictionCondition();
			while (reader.IsStartElement())
			{
				if (!reader.IsStartElement("Audience", SamlConstants.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
					{
						"Audience",
						SamlConstants.Namespace,
						reader.LocalName,
						reader.NamespaceURI
					})));
				}
				string text = reader.ReadString();
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4083")));
				}
				samlAudienceRestrictionCondition.Audiences.Add(new Uri(text, UriKind.RelativeOrAbsolute));
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			if (samlAudienceRestrictionCondition.Audiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4084")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlAudienceRestrictionCondition;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00032CB4 File Offset: 0x00030EB4
		protected virtual void WriteAudienceRestrictionCondition(XmlWriter writer, SamlAudienceRestrictionCondition condition)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (condition == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("condition");
			}
			if (condition.Audiences == null || condition.Audiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4269")));
			}
			writer.WriteStartElement("saml", "AudienceRestrictionCondition", SamlConstants.Namespace);
			for (int i = 0; i < condition.Audiences.Count; i++)
			{
				writer.WriteElementString("Audience", SamlConstants.Namespace, condition.Audiences[i].OriginalString);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00032D68 File Offset: 0x00030F68
		protected virtual SamlDoNotCacheCondition ReadDoNotCacheCondition(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("DoNotCacheCondition", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"DoNotCacheCondition",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			SamlDoNotCacheCondition result = new SamlDoNotCacheCondition();
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return result;
			}
			reader.MoveToContent();
			reader.ReadStartElement();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00032E10 File Offset: 0x00031010
		protected virtual void WriteDoNotCacheCondition(XmlWriter writer, SamlDoNotCacheCondition condition)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (condition == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("condition");
			}
			writer.WriteStartElement("saml", "DoNotCacheCondition", SamlConstants.Namespace);
			writer.WriteEndElement();
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00032E60 File Offset: 0x00031060
		protected virtual SamlStatement ReadStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement("AuthenticationStatement", SamlConstants.Namespace))
			{
				return this.ReadAuthenticationStatement(reader);
			}
			if (reader.IsStartElement("AttributeStatement", SamlConstants.Namespace))
			{
				return this.ReadAttributeStatement(reader);
			}
			if (reader.IsStartElement("AuthorizationDecisionStatement", SamlConstants.Namespace))
			{
				return this.ReadAuthorizationDecisionStatement(reader);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4085", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00032F00 File Offset: 0x00031100
		protected virtual void WriteStatement(XmlWriter writer, SamlStatement statement)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (statement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			SamlAuthenticationStatement samlAuthenticationStatement = statement as SamlAuthenticationStatement;
			if (samlAuthenticationStatement != null)
			{
				this.WriteAuthenticationStatement(writer, samlAuthenticationStatement);
				return;
			}
			SamlAuthorizationDecisionStatement samlAuthorizationDecisionStatement = statement as SamlAuthorizationDecisionStatement;
			if (samlAuthorizationDecisionStatement != null)
			{
				this.WriteAuthorizationDecisionStatement(writer, samlAuthorizationDecisionStatement);
				return;
			}
			SamlAttributeStatement samlAttributeStatement = statement as SamlAttributeStatement;
			if (samlAttributeStatement != null)
			{
				this.WriteAttributeStatement(writer, samlAttributeStatement);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4086", new object[]
			{
				statement.GetType()
			})));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00032F94 File Offset: 0x00031194
		protected virtual SamlSubject ReadSubject(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Subject", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"Subject",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			SamlSubject samlSubject = new SamlSubject();
			reader.ReadStartElement("Subject", SamlConstants.Namespace);
			if (reader.IsStartElement("NameIdentifier", SamlConstants.Namespace))
			{
				samlSubject.NameFormat = reader.GetAttribute("Format", null);
				samlSubject.NameQualifier = reader.GetAttribute("NameQualifier", null);
				reader.MoveToContent();
				samlSubject.Name = reader.ReadElementString();
				if (string.IsNullOrEmpty(samlSubject.Name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4087")));
				}
			}
			if (reader.IsStartElement("SubjectConfirmation", SamlConstants.Namespace))
			{
				reader.ReadStartElement();
				while (reader.IsStartElement("ConfirmationMethod", SamlConstants.Namespace))
				{
					string text = reader.ReadElementString();
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4088")));
					}
					samlSubject.ConfirmationMethods.Add(text);
				}
				if (samlSubject.ConfirmationMethods.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4088")));
				}
				if (reader.IsStartElement("SubjectConfirmationData", SamlConstants.Namespace))
				{
					samlSubject.SubjectConfirmationData = reader.ReadElementString();
				}
				if (reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#"))
				{
					samlSubject.KeyIdentifier = this.ReadSubjectKeyInfo(reader);
					SecurityKey securityKey = this.ResolveSubjectKeyIdentifier(samlSubject.KeyIdentifier);
					if (securityKey != null)
					{
						samlSubject.Crypto = securityKey;
					}
					else
					{
						samlSubject.Crypto = new SecurityKeyElement(samlSubject.KeyIdentifier, base.Configuration.ServiceTokenResolver);
					}
				}
				if (samlSubject.ConfirmationMethods.Count == 0 && string.IsNullOrEmpty(samlSubject.Name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4089")));
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlSubject;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000331DC File Offset: 0x000313DC
		protected virtual void WriteSubject(XmlWriter writer, SamlSubject subject)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			writer.WriteStartElement("saml", "Subject", SamlConstants.Namespace);
			if (!string.IsNullOrEmpty(subject.Name))
			{
				writer.WriteStartElement("saml", "NameIdentifier", SamlConstants.Namespace);
				if (!string.IsNullOrEmpty(subject.NameFormat))
				{
					writer.WriteAttributeString("Format", null, subject.NameFormat);
				}
				if (subject.NameQualifier != null)
				{
					writer.WriteAttributeString("NameQualifier", null, subject.NameQualifier);
				}
				writer.WriteString(subject.Name);
				writer.WriteEndElement();
			}
			if (subject.ConfirmationMethods.Count > 0)
			{
				writer.WriteStartElement("saml", "SubjectConfirmation", SamlConstants.Namespace);
				foreach (string value in subject.ConfirmationMethods)
				{
					writer.WriteElementString("ConfirmationMethod", SamlConstants.Namespace, value);
				}
				if (!string.IsNullOrEmpty(subject.SubjectConfirmationData))
				{
					writer.WriteElementString("SubjectConfirmationData", SamlConstants.Namespace, subject.SubjectConfirmationData);
				}
				if (subject.KeyIdentifier != null)
				{
					this.WriteSubjectKeyInfo(writer, subject.KeyIdentifier);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00033348 File Offset: 0x00031548
		protected virtual SecurityKeyIdentifier ReadSubjectKeyInfo(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (this.KeyInfoSerializer.CanReadKeyIdentifier(reader))
			{
				return this.KeyInfoSerializer.ReadKeyIdentifier(reader);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4090")));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0003339C File Offset: 0x0003159C
		protected virtual void WriteSubjectKeyInfo(XmlWriter writer, SecurityKeyIdentifier subjectSki)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (subjectSki == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectSki");
			}
			if (this.KeyInfoSerializer.CanWriteKeyIdentifier(subjectSki))
			{
				this.KeyInfoSerializer.WriteKeyIdentifier(writer, subjectSki);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("subjectSki", SR.GetString("ID4091", new object[]
			{
				subjectSki.GetType()
			}));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00033414 File Offset: 0x00031614
		protected virtual SamlAttributeStatement ReadAttributeStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("AttributeStatement", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"AttributeStatement",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			reader.ReadStartElement();
			SamlAttributeStatement samlAttributeStatement = new SamlAttributeStatement();
			if (!reader.IsStartElement("Subject", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4092")));
			}
			samlAttributeStatement.SamlSubject = this.ReadSubject(reader);
			while (reader.IsStartElement() && reader.IsStartElement("Attribute", SamlConstants.Namespace))
			{
				samlAttributeStatement.Attributes.Add(this.ReadAttribute(reader));
			}
			if (samlAttributeStatement.Attributes.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4093")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlAttributeStatement;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00033530 File Offset: 0x00031730
		protected virtual void WriteAttributeStatement(XmlWriter writer, SamlAttributeStatement statement)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (statement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			writer.WriteStartElement("saml", "AttributeStatement", SamlConstants.Namespace);
			this.WriteSubject(writer, statement.SamlSubject);
			for (int i = 0; i < statement.Attributes.Count; i++)
			{
				this.WriteAttribute(writer, statement.Attributes[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000335B4 File Offset: 0x000317B4
		protected virtual SamlAttribute ReadAttribute(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlAttribute samlAttribute = new SamlAttribute();
			samlAttribute.Name = reader.GetAttribute("AttributeName", null);
			if (string.IsNullOrEmpty(samlAttribute.Name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4094")));
			}
			samlAttribute.Namespace = reader.GetAttribute("AttributeNamespace", null);
			if (string.IsNullOrEmpty(samlAttribute.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4095")));
			}
			string attribute = reader.GetAttribute("OriginalIssuer", "http://schemas.xmlsoap.org/ws/2009/09/identity/claims");
			if (attribute == null)
			{
				attribute = reader.GetAttribute("OriginalIssuer", "http://schemas.microsoft.com/ws/2008/06/identity");
			}
			if (attribute == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4252")));
			}
			samlAttribute.OriginalIssuer = attribute;
			reader.MoveToContent();
			reader.Read();
			while (reader.IsStartElement("AttributeValue", SamlConstants.Namespace))
			{
				string text = null;
				string text2 = null;
				string attribute2 = reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
				if (!string.IsNullOrEmpty(attribute2))
				{
					if (attribute2.IndexOf(":", StringComparison.Ordinal) == -1)
					{
						text = reader.LookupNamespace(string.Empty);
						text2 = attribute2;
					}
					else if (attribute2.IndexOf(":", StringComparison.Ordinal) > 0 && attribute2.IndexOf(":", StringComparison.Ordinal) < attribute2.Length - 1)
					{
						string prefix = attribute2.Substring(0, attribute2.IndexOf(":", StringComparison.Ordinal));
						text = reader.LookupNamespace(prefix);
						text2 = attribute2.Substring(attribute2.IndexOf(":", StringComparison.Ordinal) + 1);
					}
				}
				if (text != null && text2 != null)
				{
					samlAttribute.AttributeValueXsiType = text + "#" + text2;
				}
				if (reader.IsEmptyElement)
				{
					reader.Read();
					samlAttribute.AttributeValues.Add(string.Empty);
				}
				else
				{
					samlAttribute.AttributeValues.Add(this.ReadAttributeValue(reader, samlAttribute));
				}
			}
			if (samlAttribute.AttributeValues.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4212")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlAttribute;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000337EC File Offset: 0x000319EC
		protected virtual string ReadAttributeValue(XmlReader reader, SamlAttribute attribute)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			reader.ReadStartElement("AttributeValue", SamlConstants.Namespace);
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				text2 += reader.Value;
				reader.Read();
			}
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element)
			{
				while (reader.NodeType == XmlNodeType.Element)
				{
					text += reader.ReadOuterXml();
					reader.MoveToContent();
				}
			}
			else
			{
				text = text2;
				text += reader.ReadContentAsString();
			}
			reader.ReadEndElement();
			return text;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00033890 File Offset: 0x00031A90
		protected virtual void WriteAttribute(XmlWriter writer, SamlAttribute attribute)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (attribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attribute");
			}
			writer.WriteStartElement("saml", "Attribute", SamlConstants.Namespace);
			writer.WriteAttributeString("AttributeName", null, attribute.Name);
			writer.WriteAttributeString("AttributeNamespace", null, attribute.Namespace);
			if (attribute != null && attribute.OriginalIssuer != null)
			{
				writer.WriteAttributeString("OriginalIssuer", "http://schemas.xmlsoap.org/ws/2009/09/identity/claims", attribute.OriginalIssuer);
			}
			string text = null;
			string text2 = null;
			if (attribute != null && !StringComparer.Ordinal.Equals(attribute.AttributeValueXsiType, "http://www.w3.org/2001/XMLSchema#string"))
			{
				int num = attribute.AttributeValueXsiType.IndexOf('#');
				text = attribute.AttributeValueXsiType.Substring(0, num);
				text2 = attribute.AttributeValueXsiType.Substring(num + 1);
			}
			for (int i = 0; i < attribute.AttributeValues.Count; i++)
			{
				if (attribute.AttributeValues[i] == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4096")));
				}
				writer.WriteStartElement("saml", "AttributeValue", SamlConstants.Namespace);
				if (text != null && text2 != null)
				{
					writer.WriteAttributeString("xmlns", "tn", null, text);
					writer.WriteAttributeString("type", "http://www.w3.org/2001/XMLSchema-instance", "tn:" + text2);
				}
				this.WriteAttributeValue(writer, attribute.AttributeValues[i], attribute);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00028EE8 File Offset: 0x000270E8
		protected virtual void WriteAttributeValue(XmlWriter writer, string value, SamlAttribute attribute)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteString(value);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00033A1C File Offset: 0x00031C1C
		protected virtual SamlAuthenticationStatement ReadAuthenticationStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("AuthenticationStatement", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"AuthenticationStatement",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			SamlAuthenticationStatement samlAuthenticationStatement = new SamlAuthenticationStatement();
			string attribute = reader.GetAttribute("AuthenticationInstant", null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4097")));
			}
			samlAuthenticationStatement.AuthenticationInstant = DateTime.ParseExact(attribute, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			samlAuthenticationStatement.AuthenticationMethod = reader.GetAttribute("AuthenticationMethod", null);
			if (string.IsNullOrEmpty(samlAuthenticationStatement.AuthenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4098")));
			}
			reader.MoveToContent();
			reader.Read();
			if (reader.IsStartElement("Subject", SamlConstants.Namespace))
			{
				samlAuthenticationStatement.SamlSubject = this.ReadSubject(reader);
				if (reader.IsStartElement("SubjectLocality", SamlConstants.Namespace))
				{
					samlAuthenticationStatement.DnsAddress = reader.GetAttribute("DNSAddress", null);
					samlAuthenticationStatement.IPAddress = reader.GetAttribute("IPAddress", null);
					if (reader.IsEmptyElement)
					{
						reader.MoveToContent();
						reader.Read();
					}
					else
					{
						reader.MoveToContent();
						reader.Read();
						reader.ReadEndElement();
					}
				}
				while (reader.IsStartElement())
				{
					if (!reader.IsStartElement("AuthorityBinding", SamlConstants.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
						{
							"AuthorityBinding",
							SamlConstants.Namespace,
							reader.LocalName,
							reader.NamespaceURI
						})));
					}
					samlAuthenticationStatement.AuthorityBindings.Add(this.ReadAuthorityBinding(reader));
				}
				reader.MoveToContent();
				reader.ReadEndElement();
				return samlAuthenticationStatement;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4099")));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00033C4C File Offset: 0x00031E4C
		protected virtual void WriteAuthenticationStatement(XmlWriter writer, SamlAuthenticationStatement statement)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (statement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			writer.WriteStartElement("saml", "AuthenticationStatement", SamlConstants.Namespace);
			writer.WriteAttributeString("AuthenticationMethod", null, statement.AuthenticationMethod);
			writer.WriteAttributeString("AuthenticationInstant", null, XmlConvert.ToString(statement.AuthenticationInstant.ToUniversalTime(), DateTimeFormats.Generated));
			this.WriteSubject(writer, statement.SamlSubject);
			if (statement.IPAddress != null || statement.DnsAddress != null)
			{
				writer.WriteStartElement("saml", "SubjectLocality", SamlConstants.Namespace);
				if (statement.IPAddress != null)
				{
					writer.WriteAttributeString("IPAddress", null, statement.IPAddress);
				}
				if (statement.DnsAddress != null)
				{
					writer.WriteAttributeString("DNSAddress", null, statement.DnsAddress);
				}
				writer.WriteEndElement();
			}
			for (int i = 0; i < statement.AuthorityBindings.Count; i++)
			{
				this.WriteAuthorityBinding(writer, statement.AuthorityBindings[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00033D68 File Offset: 0x00031F68
		protected virtual SamlAuthorityBinding ReadAuthorityBinding(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlAuthorityBinding samlAuthorityBinding = new SamlAuthorityBinding();
			string attribute = reader.GetAttribute("AuthorityKind", null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4200")));
			}
			string[] array = attribute.Split(new char[]
			{
				':'
			});
			if (array.Length > 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4201", new object[]
				{
					attribute
				})));
			}
			string prefix;
			string name;
			if (array.Length == 2)
			{
				prefix = array[0];
				name = array[1];
			}
			else
			{
				prefix = string.Empty;
				name = array[0];
			}
			string ns = reader.LookupNamespace(prefix);
			samlAuthorityBinding.AuthorityKind = new XmlQualifiedName(name, ns);
			samlAuthorityBinding.Binding = reader.GetAttribute("Binding", null);
			if (string.IsNullOrEmpty(samlAuthorityBinding.Binding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4202")));
			}
			samlAuthorityBinding.Location = reader.GetAttribute("Location", null);
			if (string.IsNullOrEmpty(samlAuthorityBinding.Location))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4203")));
			}
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
			}
			else
			{
				reader.MoveToContent();
				reader.Read();
				reader.ReadEndElement();
			}
			return samlAuthorityBinding;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00033ECC File Offset: 0x000320CC
		protected virtual void WriteAuthorityBinding(XmlWriter writer, SamlAuthorityBinding authorityBinding)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (authorityBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			writer.WriteStartElement("saml", "AuthorityBinding", SamlConstants.Namespace);
			string text = null;
			if (!string.IsNullOrEmpty(authorityBinding.AuthorityKind.Namespace))
			{
				writer.WriteAttributeString(string.Empty, "xmlns", null, authorityBinding.AuthorityKind.Namespace);
				text = writer.LookupPrefix(authorityBinding.AuthorityKind.Namespace);
			}
			writer.WriteStartAttribute("AuthorityKind", null);
			if (string.IsNullOrEmpty(text))
			{
				writer.WriteString(authorityBinding.AuthorityKind.Name);
			}
			else
			{
				writer.WriteString(text + ":" + authorityBinding.AuthorityKind.Name);
			}
			writer.WriteEndAttribute();
			writer.WriteAttributeString("Location", null, authorityBinding.Location);
			writer.WriteAttributeString("Binding", null, authorityBinding.Binding);
			writer.WriteEndElement();
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00033FCC File Offset: 0x000321CC
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
			SamlSecurityToken samlSecurityToken = token as SamlSecurityToken;
			if (samlSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4217", new object[]
				{
					token.GetType(),
					typeof(SamlSecurityToken)
				})));
			}
			this.WriteAssertion(writer, samlSecurityToken.Assertion);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003404C File Offset: 0x0003224C
		protected virtual SamlAuthorizationDecisionStatement ReadAuthorizationDecisionStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("AuthorizationDecisionStatement", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"AuthorizationDecisionStatement",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			SamlAuthorizationDecisionStatement samlAuthorizationDecisionStatement = new SamlAuthorizationDecisionStatement();
			samlAuthorizationDecisionStatement.Resource = reader.GetAttribute("Resource", null);
			if (string.IsNullOrEmpty(samlAuthorizationDecisionStatement.Resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4205")));
			}
			string attribute = reader.GetAttribute("Decision", null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4204")));
			}
			if (attribute.Equals(SamlAccessDecision.Deny.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				samlAuthorizationDecisionStatement.AccessDecision = SamlAccessDecision.Deny;
			}
			else if (attribute.Equals(SamlAccessDecision.Permit.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				samlAuthorizationDecisionStatement.AccessDecision = SamlAccessDecision.Permit;
			}
			else
			{
				samlAuthorizationDecisionStatement.AccessDecision = SamlAccessDecision.Indeterminate;
			}
			reader.MoveToContent();
			reader.Read();
			if (!reader.IsStartElement("Subject", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4206")));
			}
			samlAuthorizationDecisionStatement.SamlSubject = this.ReadSubject(reader);
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement("Action", SamlConstants.Namespace))
				{
					samlAuthorizationDecisionStatement.SamlActions.Add(this.ReadAction(reader));
				}
				else
				{
					if (!reader.IsStartElement("Evidence", SamlConstants.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4208", new object[]
						{
							reader.LocalName,
							reader.NamespaceURI
						})));
					}
					if (samlAuthorizationDecisionStatement.Evidence != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4207")));
					}
					samlAuthorizationDecisionStatement.Evidence = this.ReadEvidence(reader);
				}
			}
			if (samlAuthorizationDecisionStatement.SamlActions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4209")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlAuthorizationDecisionStatement;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000342A0 File Offset: 0x000324A0
		protected virtual void WriteAuthorizationDecisionStatement(XmlWriter writer, SamlAuthorizationDecisionStatement statement)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (statement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			writer.WriteStartElement("saml", "AuthorizationDecisionStatement", SamlConstants.Namespace);
			writer.WriteAttributeString("Decision", null, statement.AccessDecision.ToString());
			writer.WriteAttributeString("Resource", null, statement.Resource);
			this.WriteSubject(writer, statement.SamlSubject);
			foreach (SamlAction action in statement.SamlActions)
			{
				this.WriteAction(writer, action);
			}
			if (statement.Evidence != null)
			{
				this.WriteEvidence(writer, statement.Evidence);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00034384 File Offset: 0x00032584
		protected virtual SamlEvidence ReadEvidence(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Evidence", SamlConstants.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4082", new object[]
				{
					"Evidence",
					SamlConstants.Namespace,
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			SamlEvidence samlEvidence = new SamlEvidence();
			reader.ReadStartElement();
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement("AssertionIDReference", SamlConstants.Namespace))
				{
					samlEvidence.AssertionIdReferences.Add(reader.ReadElementString());
				}
				else
				{
					if (!reader.IsStartElement("Assertion", SamlConstants.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4210", new object[]
						{
							reader.LocalName,
							reader.NamespaceURI
						})));
					}
					samlEvidence.Assertions.Add(this.ReadAssertion(reader));
				}
			}
			if (samlEvidence.AssertionIdReferences.Count == 0 && samlEvidence.Assertions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4211")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
			return samlEvidence;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000344D0 File Offset: 0x000326D0
		protected virtual void WriteEvidence(XmlWriter writer, SamlEvidence evidence)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (evidence == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("evidence");
			}
			writer.WriteStartElement("saml", "Evidence", SamlConstants.Namespace);
			for (int i = 0; i < evidence.AssertionIdReferences.Count; i++)
			{
				writer.WriteElementString("saml", "AssertionIDReference", SamlConstants.Namespace, evidence.AssertionIdReferences[i]);
			}
			for (int j = 0; j < evidence.Assertions.Count; j++)
			{
				this.WriteAssertion(writer, evidence.Assertions[j]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00034580 File Offset: 0x00032780
		protected virtual SecurityKey ResolveSubjectKeyIdentifier(SecurityKeyIdentifier subjectKeyIdentifier)
		{
			if (subjectKeyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectKeyIdentifier");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.ServiceTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4276"));
			}
			SecurityKey result = null;
			foreach (SecurityKeyIdentifierClause keyIdentifierClause in subjectKeyIdentifier)
			{
				if (base.Configuration.ServiceTokenResolver.TryResolveSecurityKey(keyIdentifierClause, out result))
				{
					return result;
				}
			}
			if (subjectKeyIdentifier.CanCreateKey)
			{
				return subjectKeyIdentifier.CreateKey();
			}
			return null;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00034638 File Offset: 0x00032838
		protected virtual SecurityToken ResolveIssuerToken(SamlAssertion assertion, SecurityTokenResolver issuerResolver)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			if (issuerResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerResolver");
			}
			SecurityToken result;
			if (this.TryResolveIssuerToken(assertion, issuerResolver, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4220")));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00034694 File Offset: 0x00032894
		protected virtual bool TryResolveIssuerToken(SamlAssertion assertion, SecurityTokenResolver issuerResolver, out SecurityToken token)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			if (assertion.SigningCredentials == null || assertion.SigningCredentials.SigningKeyIdentifier == null || issuerResolver == null)
			{
				token = null;
				return false;
			}
			SecurityKeyIdentifier signingKeyIdentifier = assertion.SigningCredentials.SigningKeyIdentifier;
			if (signingKeyIdentifier.Count < 2 || LocalAppContextSwitches.ProcessMultipleSecurityKeyIdentifierClauses)
			{
				return issuerResolver.TryResolveToken(signingKeyIdentifier, out token);
			}
			return issuerResolver.TryResolveToken(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				signingKeyIdentifier[0]
			}), out token);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00034714 File Offset: 0x00032914
		protected virtual SecurityKeyIdentifier ReadSigningKeyInfo(XmlReader reader, SamlAssertion assertion)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SecurityKeyIdentifier securityKeyIdentifier;
			if (this.KeyInfoSerializer.CanReadKeyIdentifier(reader))
			{
				securityKeyIdentifier = this.KeyInfoSerializer.ReadKeyIdentifier(reader);
			}
			else
			{
				KeyInfo keyInfo = new KeyInfo(this.KeyInfoSerializer);
				keyInfo.ReadXml(XmlDictionaryReader.CreateDictionaryReader(reader));
				securityKeyIdentifier = keyInfo.KeyIdentifier;
			}
			if (securityKeyIdentifier.Count == 0)
			{
				return new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					new SamlSecurityKeyIdentifierClause(assertion)
				});
			}
			return securityKeyIdentifier;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00034790 File Offset: 0x00032990
		protected virtual void WriteSigningKeyInfo(XmlWriter writer, SecurityKeyIdentifier signingKeyIdentifier)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (signingKeyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signingKeyIdentifier");
			}
			if (this.KeyInfoSerializer.CanWriteKeyIdentifier(signingKeyIdentifier))
			{
				this.KeyInfoSerializer.WriteKeyIdentifier(writer, signingKeyIdentifier);
				return;
			}
			throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4221", new object[]
			{
				signingKeyIdentifier
			}));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000347F8 File Offset: 0x000329F8
		private void ValidateStatements(IList<SamlStatement> statements)
		{
			if (statements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statements");
			}
			List<SamlSubject> list = new List<SamlSubject>();
			foreach (SamlStatement samlStatement in statements)
			{
				if (samlStatement is SamlAttributeStatement)
				{
					list.Add((samlStatement as SamlAttributeStatement).SamlSubject);
				}
				if (samlStatement is SamlAuthenticationStatement)
				{
					list.Add((samlStatement as SamlAuthenticationStatement).SamlSubject);
				}
				if (samlStatement is SamlAuthorizationDecisionStatement)
				{
					list.Add((samlStatement as SamlAuthorizationDecisionStatement).SamlSubject);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			string name = list[0].Name;
			string nameFormat = list[0].NameFormat;
			string nameQualifier = list[0].NameQualifier;
			foreach (SamlSubject samlSubject in list)
			{
				if (!StringComparer.Ordinal.Equals(samlSubject.Name, name) || !StringComparer.Ordinal.Equals(samlSubject.NameFormat, nameFormat) || !StringComparer.Ordinal.Equals(samlSubject.NameQualifier, nameQualifier))
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4225", new object[]
					{
						samlSubject
					}));
				}
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00034968 File Offset: 0x00032B68
		public override string[] GetTokenTypeIdentifiers()
		{
			return SamlSecurityTokenHandler._tokenTypeIdentifiers;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00034970 File Offset: 0x00032B70
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x000349E4 File Offset: 0x00032BE4
		public SecurityTokenSerializer KeyInfoSerializer
		{
			get
			{
				if (this._keyInfoSerializer == null)
				{
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						if (this._keyInfoSerializer == null)
						{
							SecurityTokenHandlerCollection securityTokenHandlerCollection = (base.ContainingCollection != null) ? base.ContainingCollection : SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
							this._keyInfoSerializer = new SecurityTokenSerializerAdapter(securityTokenHandlerCollection);
						}
					}
				}
				return this._keyInfoSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._keyInfoSerializer = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00034A00 File Offset: 0x00032C00
		public override Type TokenType
		{
			get
			{
				return typeof(SamlSecurityToken);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00034A0C File Offset: 0x00032C0C
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00034A14 File Offset: 0x00032C14
		public SamlSecurityTokenRequirement SamlSecurityTokenRequirement
		{
			get
			{
				return this._samlSecurityTokenRequirement;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._samlSecurityTokenRequirement = value;
			}
		}

		// Token: 0x04000BD2 RID: 3026
		public const string Namespace = "urn:oasis:names:tc:SAML:1.0";

		// Token: 0x04000BD3 RID: 3027
		public const string BearerConfirmationMethod = "urn:oasis:names:tc:SAML:1.0:cm:bearer";

		// Token: 0x04000BD4 RID: 3028
		public const string UnspecifiedAuthenticationMethod = "urn:oasis:names:tc:SAML:1.0:am:unspecified";

		// Token: 0x04000BD5 RID: 3029
		public const string Assertion = "urn:oasis:names:tc:SAML:1.0:assertion";

		// Token: 0x04000BD6 RID: 3030
		private const string Attribute = "saml:Attribute";

		// Token: 0x04000BD7 RID: 3031
		private const string Actor = "Actor";

		// Token: 0x04000BD8 RID: 3032
		private const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		// Token: 0x04000BD9 RID: 3033
		private static DateTime WCFMinValue = new DateTime(DateTime.MinValue.Ticks + 864000000000L, DateTimeKind.Utc);

		// Token: 0x04000BDA RID: 3034
		private static DateTime WCFMaxValue = new DateTime(DateTime.MaxValue.Ticks - 864000000000L, DateTimeKind.Utc);

		// Token: 0x04000BDB RID: 3035
		private static string[] _tokenTypeIdentifiers = new string[]
		{
			"urn:oasis:names:tc:SAML:1.0:assertion",
			"http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1"
		};

		// Token: 0x04000BDC RID: 3036
		private SamlSecurityTokenRequirement _samlSecurityTokenRequirement;

		// Token: 0x04000BDD RID: 3037
		private SecurityTokenSerializer _keyInfoSerializer;

		// Token: 0x04000BDE RID: 3038
		private const int MaxAssertionNestingDepth = 8;

		// Token: 0x04000BDF RID: 3039
		[ThreadStatic]
		private static int t_currentAssertionDepth;

		// Token: 0x04000BE0 RID: 3040
		private object _syncObject = new object();

		// Token: 0x0200026E RID: 622
		private class WrappedSerializer : SecurityTokenSerializer
		{
			// Token: 0x06001285 RID: 4741 RVA: 0x0005072E File Offset: 0x0004E92E
			public WrappedSerializer(SamlSecurityTokenHandler parent, SamlAssertion assertion)
			{
				if (parent == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
				}
				this._parent = parent;
				this._assertion = assertion;
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
			{
				return false;
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x00002434 File Offset: 0x00000634
			protected override bool CanReadKeyIdentifierCore(XmlReader reader)
			{
				return true;
			}

			// Token: 0x06001288 RID: 4744 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanReadTokenCore(XmlReader reader)
			{
				return false;
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return false;
			}

			// Token: 0x0600128A RID: 4746 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
			{
				return false;
			}

			// Token: 0x0600128B RID: 4747 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteTokenCore(SecurityToken token)
			{
				return false;
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x00050757 File Offset: 0x0004E957
			protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
			{
				return this._parent.ReadSigningKeyInfo(reader, this._assertion);
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x0600128F RID: 4751 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x0005076B File Offset: 0x0004E96B
			protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
			{
				this._parent.WriteSigningKeyInfo(writer, keyIdentifier);
			}

			// Token: 0x06001291 RID: 4753 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x0400110E RID: 4366
			private SamlSecurityTokenHandler _parent;

			// Token: 0x0400110F RID: 4367
			private SamlAssertion _assertion;
		}
	}
}
