using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Configuration;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000160 RID: 352
	public class SamlSecurityTokenRequirement
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00034A9C File Offset: 0x00032C9C
		public SamlSecurityTokenRequirement()
		{
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00034ABC File Offset: 0x00032CBC
		public SamlSecurityTokenRequirement(XmlElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			if (element.LocalName != "samlSecurityTokenRequirement")
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7000", new object[]
				{
					"samlSecurityTokenRequirement",
					element.LocalName
				}));
			}
			bool flag = false;
			X509RevocationMode revocationMode = SamlSecurityTokenRequirement.DefaultRevocationMode;
			X509CertificateValidationMode x509CertificateValidationMode = SamlSecurityTokenRequirement.DefaultValidationMode;
			StoreLocation trustedStoreLocation = SamlSecurityTokenRequirement.DefaultStoreLocation;
			string text = null;
			foreach (object obj in element.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "mapToWindows"))
				{
					bool mapToWindows = false;
					if (!bool.TryParse(xmlAttribute.Value, out mapToWindows))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7022", new object[]
						{
							xmlAttribute.Value
						}));
					}
					this.MapToWindows = mapToWindows;
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "issuerCertificateValidator"))
				{
					text = xmlAttribute.Value.ToString();
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "issuerCertificateRevocationMode"))
				{
					flag = true;
					string x = xmlAttribute.Value.ToString();
					if (StringComparer.OrdinalIgnoreCase.Equals(x, "NoCheck"))
					{
						revocationMode = X509RevocationMode.NoCheck;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(x, "Offline"))
					{
						revocationMode = X509RevocationMode.Offline;
					}
					else
					{
						if (!StringComparer.OrdinalIgnoreCase.Equals(x, "Online"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
							{
								xmlAttribute.LocalName,
								element.LocalName
							})));
						}
						revocationMode = X509RevocationMode.Online;
					}
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "issuerCertificateValidationMode"))
				{
					flag = true;
					string x2 = xmlAttribute.Value.ToString();
					if (StringComparer.OrdinalIgnoreCase.Equals(x2, "ChainTrust"))
					{
						x509CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "PeerOrChainTrust"))
					{
						x509CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "PeerTrust"))
					{
						x509CertificateValidationMode = X509CertificateValidationMode.PeerTrust;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "None"))
					{
						x509CertificateValidationMode = X509CertificateValidationMode.None;
					}
					else
					{
						if (!StringComparer.OrdinalIgnoreCase.Equals(x2, "Custom"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
							{
								xmlAttribute.LocalName,
								element.LocalName
							})));
						}
						x509CertificateValidationMode = X509CertificateValidationMode.Custom;
					}
				}
				else
				{
					if (!StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "issuerCertificateTrustedStoreLocation"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7004", new object[]
						{
							xmlAttribute.LocalName,
							element.LocalName
						})));
					}
					flag = true;
					string x3 = xmlAttribute.Value.ToString();
					if (StringComparer.OrdinalIgnoreCase.Equals(x3, "CurrentUser"))
					{
						trustedStoreLocation = StoreLocation.CurrentUser;
					}
					else
					{
						if (!StringComparer.OrdinalIgnoreCase.Equals(x3, "LocalMachine"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
							{
								xmlAttribute.LocalName,
								element.LocalName
							})));
						}
						trustedStoreLocation = StoreLocation.LocalMachine;
					}
				}
			}
			List<XmlElement> xmlElements = XmlUtil.GetXmlElements(element.ChildNodes);
			foreach (XmlElement xmlElement in xmlElements)
			{
				if (StringComparer.Ordinal.Equals(xmlElement.LocalName, "nameClaimType"))
				{
					if (xmlElement.Attributes.Count != 1 || !StringComparer.Ordinal.Equals(xmlElement.Attributes[0].LocalName, "value"))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7001", new object[]
						{
							string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
							{
								element.LocalName,
								xmlElement.LocalName
							}),
							"value"
						}));
					}
					this.NameClaimType = xmlElement.Attributes[0].Value;
				}
				else
				{
					if (!StringComparer.Ordinal.Equals(xmlElement.LocalName, "roleClaimType"))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7002", new object[]
						{
							xmlElement.LocalName,
							"samlSecurityTokenRequirement"
						}));
					}
					if (xmlElement.Attributes.Count != 1 || !StringComparer.Ordinal.Equals(xmlElement.Attributes[0].LocalName, "value"))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7001", new object[]
						{
							string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
							{
								element.LocalName,
								xmlElement.LocalName
							}),
							"value"
						}));
					}
					this.RoleClaimType = xmlElement.Attributes[0].Value;
				}
			}
			if (x509CertificateValidationMode != X509CertificateValidationMode.Custom)
			{
				if (flag)
				{
					this._certificateValidator = X509Util.CreateCertificateValidator(x509CertificateValidationMode, revocationMode, trustedStoreLocation);
				}
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7028"));
			}
			Type type = Type.GetType(text, true);
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID7007", new object[]
				{
					type
				}));
			}
			this._certificateValidator = CustomTypeElement.Resolve<X509CertificateValidator>(new CustomTypeElement(type));
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000350D0 File Offset: 0x000332D0
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x000350D8 File Offset: 0x000332D8
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				return this._certificateValidator;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._certificateValidator = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x000350F4 File Offset: 0x000332F4
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x000350FC File Offset: 0x000332FC
		public string NameClaimType
		{
			get
			{
				return this._nameClaimType;
			}
			set
			{
				this._nameClaimType = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00035105 File Offset: 0x00033305
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0003510D File Offset: 0x0003330D
		public string RoleClaimType
		{
			get
			{
				return this._roleClaimType;
			}
			set
			{
				this._roleClaimType = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00035116 File Offset: 0x00033316
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0003511E File Offset: 0x0003331E
		public bool MapToWindows
		{
			get
			{
				return this._mapToWindows;
			}
			set
			{
				this._mapToWindows = value;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00035128 File Offset: 0x00033328
		public virtual bool ShouldEnforceAudienceRestriction(AudienceUriMode audienceUriMode, SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			switch (audienceUriMode)
			{
			case AudienceUriMode.Never:
				return false;
			case AudienceUriMode.Always:
				return true;
			case AudienceUriMode.BearerKeyOnly:
				return token.SecurityKeys == null || token.SecurityKeys.Count == 0;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4025", new object[]
				{
					audienceUriMode
				})));
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000351A4 File Offset: 0x000333A4
		public virtual void ValidateAudienceRestriction(IList<Uri> allowedAudienceUris, IList<Uri> tokenAudiences)
		{
			if (allowedAudienceUris == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("allowedAudienceUris");
			}
			if (tokenAudiences == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenAudiences");
			}
			if (tokenAudiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1036")));
			}
			if (allowedAudienceUris.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1043")));
			}
			bool flag = false;
			foreach (Uri uri in tokenAudiences)
			{
				if (uri != null)
				{
					Uri item;
					if (uri.IsAbsoluteUri)
					{
						item = new Uri(uri.GetLeftPart(UriPartial.Path));
					}
					else
					{
						Uri uri2 = new Uri("http://www.example.com");
						Uri uri3 = new Uri(uri2, uri);
						item = uri2.MakeRelativeUri(new Uri(uri3.GetLeftPart(UriPartial.Path)));
					}
					if (allowedAudienceUris.Contains(item))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return;
			}
			if (1 == tokenAudiences.Count || null != tokenAudiences[0])
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1038", new object[]
				{
					tokenAudiences[0].OriginalString
				})));
			}
			StringBuilder stringBuilder = new StringBuilder(SR.GetString("ID8007"));
			bool flag2 = true;
			foreach (Uri uri4 in tokenAudiences)
			{
				if (uri4 != null)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(uri4.OriginalString);
				}
			}
			TraceUtility.TraceString(TraceEventType.Error, stringBuilder.ToString(), new object[0]);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1037")));
		}

		// Token: 0x04000BE1 RID: 3041
		private static X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04000BE2 RID: 3042
		private static X509CertificateValidationMode DefaultValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04000BE3 RID: 3043
		private static StoreLocation DefaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04000BE4 RID: 3044
		private string _nameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000BE5 RID: 3045
		private string _roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04000BE6 RID: 3046
		private bool _mapToWindows;

		// Token: 0x04000BE7 RID: 3047
		private X509CertificateValidator _certificateValidator;
	}
}
