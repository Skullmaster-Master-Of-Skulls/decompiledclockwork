using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.Security.Principal;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000163 RID: 355
	public class SamlSubject
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x00035850 File Offset: 0x00033A50
		public SamlSubject()
		{
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00035863 File Offset: 0x00033A63
		public SamlSubject(string nameFormat, string nameQualifier, string name) : this(nameFormat, nameQualifier, name, null, null, null)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00035874 File Offset: 0x00033A74
		public SamlSubject(string nameFormat, string nameQualifier, string name, IEnumerable<string> confirmations, string confirmationData, SecurityKeyIdentifier securityKeyIdentifier)
		{
			if (confirmations != null)
			{
				foreach (string text in confirmations)
				{
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
						{
							XD.SamlDictionary.SubjectConfirmationMethod.Value
						}));
					}
					this.confirmationMethods.Add(text);
				}
			}
			if (this.confirmationMethods.Count == 0 && string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLSubjectRequiresNameIdentifierOrConfirmationMethod"));
			}
			if (this.confirmationMethods.Count == 0 && (confirmationData != null || securityKeyIdentifier != null))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLSubjectRequiresConfirmationMethodWhenConfirmationDataOrKeyInfoIsSpecified"));
			}
			this.name = name;
			this.nameFormat = nameFormat;
			this.nameQualifier = nameQualifier;
			this.confirmationData = confirmationData;
			this.securityKeyIdentifier = securityKeyIdentifier;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00035988 File Offset: 0x00033B88
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00035990 File Offset: 0x00033B90
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLSubjectNameIdentifierRequiresNameValue"));
				}
				this.name = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000359E3 File Offset: 0x00033BE3
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x000359EB File Offset: 0x00033BEB
		public string NameFormat
		{
			get
			{
				return this.nameFormat;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.nameFormat = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00035A16 File Offset: 0x00033C16
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00035A1E File Offset: 0x00033C1E
		public string NameQualifier
		{
			get
			{
				return this.nameQualifier;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.nameQualifier = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00035A49 File Offset: 0x00033C49
		public static string NameClaimType
		{
			get
			{
				return ClaimTypes.NameIdentifier;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00035A50 File Offset: 0x00033C50
		public IList<string> ConfirmationMethods
		{
			get
			{
				return this.confirmationMethods;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00035A58 File Offset: 0x00033C58
		internal IIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00035A60 File Offset: 0x00033C60
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00035A68 File Offset: 0x00033C68
		public string SubjectConfirmationData
		{
			get
			{
				return this.confirmationData;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.confirmationData = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00035A84 File Offset: 0x00033C84
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00035A8C File Offset: 0x00033C8C
		public SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this.securityKeyIdentifier;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.securityKeyIdentifier = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00035ACA File Offset: 0x00033CCA
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00035AD2 File Offset: 0x00033CD2
		public SecurityKey Crypto
		{
			get
			{
				return this.crypto;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.crypto = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00035B10 File Offset: 0x00033D10
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00035B18 File Offset: 0x00033D18
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				if (this.securityKeyIdentifier != null)
				{
					this.securityKeyIdentifier.MakeReadOnly();
				}
				this.confirmationMethods.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00035B48 File Offset: 0x00033D48
		private void CheckObjectValidity()
		{
			if (this.confirmationMethods.Count == 0 && string.IsNullOrEmpty(this.name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectRequiresNameIdentifierOrConfirmationMethod")));
			}
			if (this.confirmationMethods.Count == 0 && (this.confirmationData != null || this.securityKeyIdentifier != null))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectRequiresConfirmationMethodWhenConfirmationDataOrKeyInfoIsSpecified")));
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00035BC0 File Offset: 0x00033DC0
		public virtual ReadOnlyCollection<Claim> ExtractClaims()
		{
			if (this.claims == null)
			{
				this.claims = new List<Claim>();
				if (!string.IsNullOrEmpty(this.name))
				{
					this.claims.Add(new Claim(ClaimTypes.NameIdentifier, new SamlNameIdentifierClaimResource(this.name, this.nameQualifier, this.nameFormat), Rights.Identity));
					this.claims.Add(new Claim(ClaimTypes.NameIdentifier, new SamlNameIdentifierClaimResource(this.name, this.nameQualifier, this.nameFormat), Rights.PossessProperty));
				}
			}
			return this.claims.AsReadOnly();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00035C5C File Offset: 0x00033E5C
		public virtual ClaimSet ExtractSubjectKeyClaimSet(SamlSecurityTokenAuthenticator samlAuthenticator)
		{
			if (this.subjectKeyClaimset == null && this.securityKeyIdentifier != null)
			{
				if (samlAuthenticator == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlAuthenticator");
				}
				if (this.subjectToken != null)
				{
					this.subjectKeyClaimset = samlAuthenticator.ResolveClaimSet(this.subjectToken);
					this.identity = samlAuthenticator.ResolveIdentity(this.subjectToken);
					if (this.identity == null && this.subjectKeyClaimset != null)
					{
						Claim claim = null;
						using (IEnumerator<Claim> enumerator = this.subjectKeyClaimset.FindClaims(null, Rights.Identity).GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								Claim claim2 = enumerator.Current;
								claim = claim2;
							}
						}
						if (claim != null)
						{
							this.identity = SecurityUtils.CreateIdentity(claim.Resource.ToString(), base.GetType().Name);
						}
					}
				}
				if (this.subjectKeyClaimset == null)
				{
					this.subjectKeyClaimset = samlAuthenticator.ResolveClaimSet(this.securityKeyIdentifier);
					this.identity = samlAuthenticator.ResolveIdentity(this.securityKeyIdentifier);
				}
			}
			return this.subjectKeyClaimset;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00035D74 File Offset: 0x00033F74
		public virtual void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSerializer");
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			reader.MoveToContent();
			reader.Read();
			if (reader.IsStartElement(samlDictionary.NameIdentifier, samlDictionary.Namespace))
			{
				this.nameFormat = reader.GetAttribute(samlDictionary.NameIdentifierFormat, null);
				this.nameQualifier = reader.GetAttribute(samlDictionary.NameIdentifierNameQualifier, null);
				reader.MoveToContent();
				this.name = reader.ReadString();
				if (this.name == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLNameIdentifierMissingIdentifierValueOnRead")));
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			if (reader.IsStartElement(samlDictionary.SubjectConfirmation, samlDictionary.Namespace))
			{
				reader.MoveToContent();
				reader.Read();
				while (reader.IsStartElement(samlDictionary.SubjectConfirmationMethod, samlDictionary.Namespace))
				{
					string text = reader.ReadString();
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
						{
							samlDictionary.SubjectConfirmationMethod.Value
						})));
					}
					this.confirmationMethods.Add(text);
					reader.MoveToContent();
					reader.ReadEndElement();
				}
				if (this.confirmationMethods.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectConfirmationClauseMissingConfirmationMethodOnRead")));
				}
				if (reader.IsStartElement(samlDictionary.SubjectConfirmationData, samlDictionary.Namespace))
				{
					reader.MoveToContent();
					this.confirmationData = reader.ReadString();
					reader.MoveToContent();
					reader.ReadEndElement();
				}
				if (reader.IsStartElement(samlSerializer.DictionaryManager.XmlSignatureDictionary.KeyInfo, samlSerializer.DictionaryManager.XmlSignatureDictionary.Namespace))
				{
					XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
					this.securityKeyIdentifier = SamlSerializer.ReadSecurityKeyIdentifier(reader2, keyInfoSerializer);
					this.crypto = SamlSerializer.ResolveSecurityKey(this.securityKeyIdentifier, outOfBandTokenResolver);
					if (this.crypto == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlUnableToExtractSubjectKey")));
					}
					this.subjectToken = SamlSerializer.ResolveSecurityToken(this.securityKeyIdentifier, outOfBandTokenResolver);
				}
				if (this.confirmationMethods.Count == 0 && string.IsNullOrEmpty(this.name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectRequiresNameIdentifierOrConfirmationMethodOnRead")));
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00036000 File Offset: 0x00034200
		public virtual void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Subject, samlDictionary.Namespace);
			if (this.name != null)
			{
				writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.NameIdentifier, samlDictionary.Namespace);
				if (this.nameFormat != null)
				{
					writer.WriteStartAttribute(samlDictionary.NameIdentifierFormat, null);
					writer.WriteString(this.nameFormat);
					writer.WriteEndAttribute();
				}
				if (this.nameQualifier != null)
				{
					writer.WriteStartAttribute(samlDictionary.NameIdentifierNameQualifier, null);
					writer.WriteString(this.nameQualifier);
					writer.WriteEndAttribute();
				}
				writer.WriteString(this.name);
				writer.WriteEndElement();
			}
			if (this.confirmationMethods.Count > 0)
			{
				writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.SubjectConfirmation, samlDictionary.Namespace);
				foreach (string value in this.confirmationMethods)
				{
					writer.WriteElementString(samlDictionary.SubjectConfirmationMethod, samlDictionary.Namespace, value);
				}
				if (!string.IsNullOrEmpty(this.confirmationData))
				{
					writer.WriteElementString(samlDictionary.SubjectConfirmationData, samlDictionary.Namespace, this.confirmationData);
				}
				if (this.securityKeyIdentifier != null)
				{
					XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
					SamlSerializer.WriteSecurityKeyIdentifier(writer2, this.securityKeyIdentifier, keyInfoSerializer);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BE9 RID: 3049
		private readonly ImmutableCollection<string> confirmationMethods = new ImmutableCollection<string>();

		// Token: 0x04000BEA RID: 3050
		private string confirmationData;

		// Token: 0x04000BEB RID: 3051
		private SecurityKeyIdentifier securityKeyIdentifier;

		// Token: 0x04000BEC RID: 3052
		private SecurityKey crypto;

		// Token: 0x04000BED RID: 3053
		private SecurityToken subjectToken;

		// Token: 0x04000BEE RID: 3054
		private string name;

		// Token: 0x04000BEF RID: 3055
		private string nameFormat;

		// Token: 0x04000BF0 RID: 3056
		private string nameQualifier;

		// Token: 0x04000BF1 RID: 3057
		private List<Claim> claims;

		// Token: 0x04000BF2 RID: 3058
		private IIdentity identity;

		// Token: 0x04000BF3 RID: 3059
		private ClaimSet subjectKeyClaimset;

		// Token: 0x04000BF4 RID: 3060
		private bool isReadOnly;
	}
}
