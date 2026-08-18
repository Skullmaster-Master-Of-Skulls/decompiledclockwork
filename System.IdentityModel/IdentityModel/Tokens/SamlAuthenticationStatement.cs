using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000153 RID: 339
	public class SamlAuthenticationStatement : SamlSubjectStatement
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0002E560 File Offset: 0x0002C760
		public SamlAuthenticationStatement()
		{
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002E5A8 File Offset: 0x0002C7A8
		public SamlAuthenticationStatement(SamlSubject samlSubject, string authenticationMethod, DateTime authenticationInstant, string dnsAddress, string ipAddress, IEnumerable<SamlAuthorityBinding> authorityBindings) : base(samlSubject)
		{
			if (string.IsNullOrEmpty(authenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("authenticationMethod", SR.GetString("SAMLAuthenticationStatementMissingAuthenticationMethod"));
			}
			this.authenticationMethod = authenticationMethod;
			this.authenticationInstant = authenticationInstant.ToUniversalTime();
			this.dnsAddress = dnsAddress;
			this.ipAddress = ipAddress;
			if (authorityBindings != null)
			{
				foreach (SamlAuthorityBinding samlAuthorityBinding in authorityBindings)
				{
					if (samlAuthorityBinding == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
						{
							XD.SamlDictionary.Assertion.Value
						}));
					}
					this.authorityBindings.Add(samlAuthorityBinding);
				}
			}
			this.CheckObjectValidity();
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002E6B8 File Offset: 0x0002C8B8
		public DateTime AuthenticationInstant
		{
			get
			{
				return this.authenticationInstant;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.authenticationInstant = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002E6E3 File Offset: 0x0002C8E3
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		public string AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					this.authenticationMethod = XD.SamlDictionary.UnspecifiedAuthenticationMethod.Value;
					return;
				}
				this.authenticationMethod = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002E740 File Offset: 0x0002C940
		public static string ClaimType
		{
			get
			{
				return ClaimTypes.Authentication;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002E747 File Offset: 0x0002C947
		public IList<SamlAuthorityBinding> AuthorityBindings
		{
			get
			{
				return this.authorityBindings;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002E74F File Offset: 0x0002C94F
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0002E757 File Offset: 0x0002C957
		public string DnsAddress
		{
			get
			{
				return this.dnsAddress;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.dnsAddress = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002E782 File Offset: 0x0002C982
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0002E78A File Offset: 0x0002C98A
		public string IPAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.ipAddress = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002E7B5 File Offset: 0x0002C9B5
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
		public override void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				foreach (SamlAuthorityBinding samlAuthorityBinding in this.authorityBindings)
				{
					samlAuthorityBinding.MakeReadOnly();
				}
				this.authorityBindings.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002E828 File Offset: 0x0002CA28
		protected override void AddClaimsToList(IList<Claim> claims)
		{
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claims");
			}
			claims.Add(new Claim(ClaimTypes.Authentication, new SamlAuthenticationClaimResource(this.authenticationInstant, this.authenticationMethod, this.dnsAddress, this.ipAddress, this.authorityBindings), Rights.PossessProperty));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002E880 File Offset: 0x0002CA80
		private void CheckObjectValidity()
		{
			if (base.SamlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectStatementRequiresSubject")));
			}
			if (string.IsNullOrEmpty(this.authenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthenticationStatementMissingAuthenticationMethod")));
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		public override void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			string attribute = reader.GetAttribute(samlDictionary.AuthenticationInstant, null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthenticationStatementMissingAuthenticationInstanceOnRead")));
			}
			this.authenticationInstant = DateTime.ParseExact(attribute, SamlConstants.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			this.authenticationMethod = reader.GetAttribute(samlDictionary.AuthenticationMethod, null);
			if (string.IsNullOrEmpty(this.authenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthenticationStatementMissingAuthenticationMethodOnRead")));
			}
			reader.MoveToContent();
			reader.Read();
			if (reader.IsStartElement(samlDictionary.Subject, samlDictionary.Namespace))
			{
				SamlSubject samlSubject = new SamlSubject();
				samlSubject.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
				base.SamlSubject = samlSubject;
				if (reader.IsStartElement(samlDictionary.SubjectLocality, samlDictionary.Namespace))
				{
					this.dnsAddress = reader.GetAttribute(samlDictionary.SubjectLocalityDNSAddress, null);
					this.ipAddress = reader.GetAttribute(samlDictionary.SubjectLocalityIPAddress, null);
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
					if (!reader.IsStartElement(samlDictionary.AuthorityBinding, samlDictionary.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
						{
							samlDictionary.AuthenticationStatement
						})));
					}
					SamlAuthorityBinding samlAuthorityBinding = new SamlAuthorityBinding();
					samlAuthorityBinding.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
					this.authorityBindings.Add(samlAuthorityBinding);
				}
				reader.MoveToContent();
				reader.ReadEndElement();
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthenticationStatementMissingSubject")));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		public override void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			this.CheckObjectValidity();
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AuthenticationStatement, samlDictionary.Namespace);
			writer.WriteStartAttribute(samlDictionary.AuthenticationMethod, null);
			writer.WriteString(this.authenticationMethod);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(samlDictionary.AuthenticationInstant, null);
			writer.WriteString(this.authenticationInstant.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
			writer.WriteEndAttribute();
			base.SamlSubject.WriteXml(writer, samlSerializer, keyInfoSerializer);
			if (this.ipAddress != null || this.dnsAddress != null)
			{
				writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.SubjectLocality, samlDictionary.Namespace);
				if (this.ipAddress != null)
				{
					writer.WriteStartAttribute(samlDictionary.SubjectLocalityIPAddress, null);
					writer.WriteString(this.ipAddress);
					writer.WriteEndAttribute();
				}
				if (this.dnsAddress != null)
				{
					writer.WriteStartAttribute(samlDictionary.SubjectLocalityDNSAddress, null);
					writer.WriteString(this.dnsAddress);
					writer.WriteEndAttribute();
				}
				writer.WriteEndElement();
			}
			for (int i = 0; i < this.authorityBindings.Count; i++)
			{
				this.authorityBindings[i].WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BAE RID: 2990
		private DateTime authenticationInstant = DateTime.UtcNow.ToUniversalTime();

		// Token: 0x04000BAF RID: 2991
		private string authenticationMethod = XD.SamlDictionary.UnspecifiedAuthenticationMethod.Value;

		// Token: 0x04000BB0 RID: 2992
		private readonly ImmutableCollection<SamlAuthorityBinding> authorityBindings = new ImmutableCollection<SamlAuthorityBinding>();

		// Token: 0x04000BB1 RID: 2993
		private string dnsAddress;

		// Token: 0x04000BB2 RID: 2994
		private string ipAddress;

		// Token: 0x04000BB3 RID: 2995
		private bool isReadOnly;
	}
}
