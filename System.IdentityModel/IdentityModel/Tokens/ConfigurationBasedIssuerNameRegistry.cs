using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Diagnostics.Application;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000116 RID: 278
	public class ConfigurationBasedIssuerNameRegistry : IssuerNameRegistry
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0001FF00 File Offset: 0x0001E100
		public override void LoadCustomConfiguration(XmlNodeList customConfiguration)
		{
			if (customConfiguration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("customConfiguration");
			}
			List<XmlElement> xmlElements = XmlUtil.GetXmlElements(customConfiguration);
			if (xmlElements.Count != 1)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7019", new object[]
				{
					typeof(ConfigurationBasedIssuerNameRegistry).Name
				}));
			}
			XmlElement xmlElement = xmlElements[0];
			if (!StringComparer.Ordinal.Equals(xmlElement.LocalName, "trustedIssuers"))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7002", new object[]
				{
					xmlElement.LocalName,
					"trustedIssuers"
				}));
			}
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null)
				{
					if (StringComparer.Ordinal.Equals(xmlElement2.LocalName, "add"))
					{
						XmlNode namedItem = xmlElement2.Attributes.GetNamedItem("thumbprint");
						XmlNode namedItem2 = xmlElement2.Attributes.GetNamedItem("name");
						if (xmlElement2.Attributes.Count > 2 || namedItem == null)
						{
							throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7010", new object[]
							{
								string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
								{
									xmlElement.LocalName,
									xmlElement2.LocalName
								}),
								string.Format(CultureInfo.InvariantCulture, "{0} and {1}", new object[]
								{
									"thumbprint",
									"name"
								})
							}));
						}
						string text = namedItem.Value;
						text = text.Replace(" ", "");
						string value = (namedItem2 == null || string.IsNullOrEmpty(namedItem2.Value)) ? string.Empty : string.Intern(namedItem2.Value);
						this._configuredTrustedIssuers.Add(text, value);
					}
					else if (StringComparer.Ordinal.Equals(xmlElement2.LocalName, "remove"))
					{
						if (xmlElement2.Attributes.Count != 1 || !StringComparer.Ordinal.Equals(xmlElement2.Attributes[0].LocalName, "thumbprint"))
						{
							throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7010", new object[]
							{
								string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
								{
									xmlElement.LocalName,
									xmlElement2.LocalName
								}),
								"thumbprint"
							}));
						}
						string text2 = xmlElement2.Attributes.GetNamedItem("thumbprint").Value;
						text2 = text2.Replace(" ", "");
						this._configuredTrustedIssuers.Remove(text2);
					}
					else
					{
						if (!StringComparer.Ordinal.Equals(xmlElement2.LocalName, "clear"))
						{
							throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7002", new object[]
							{
								xmlElement.LocalName,
								xmlElement2.LocalName
							}));
						}
						this._configuredTrustedIssuers.Clear();
					}
				}
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0002023C File Offset: 0x0001E43C
		public override string GetIssuerName(SecurityToken securityToken)
		{
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityToken");
			}
			X509SecurityToken x509SecurityToken = securityToken as X509SecurityToken;
			if (x509SecurityToken != null)
			{
				string thumbprint = x509SecurityToken.Certificate.Thumbprint;
				if (this._configuredTrustedIssuers.ContainsKey(thumbprint))
				{
					string text = this._configuredTrustedIssuers[thumbprint];
					text = (string.IsNullOrEmpty(text) ? x509SecurityToken.Certificate.Subject : text);
					if (TD.GetIssuerNameSuccessIsEnabled())
					{
						TD.GetIssuerNameSuccess(EventTraceActivity.GetFromThreadOrCreate(false), text, securityToken.Id);
					}
					return text;
				}
			}
			if (TD.GetIssuerNameFailureIsEnabled())
			{
				TD.GetIssuerNameFailure(EventTraceActivity.GetFromThreadOrCreate(false), securityToken.Id);
			}
			return null;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000202D8 File Offset: 0x0001E4D8
		public IDictionary<string, string> ConfiguredTrustedIssuers
		{
			get
			{
				return this._configuredTrustedIssuers;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000202E0 File Offset: 0x0001E4E0
		public void AddTrustedIssuer(string certificateThumbprint, string name)
		{
			if (string.IsNullOrEmpty(certificateThumbprint))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("certificateThumbprint");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("name");
			}
			if (this._configuredTrustedIssuers.ContainsKey(certificateThumbprint))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4265", new object[]
				{
					certificateThumbprint
				}));
			}
			certificateThumbprint = certificateThumbprint.Replace(" ", "");
			this._configuredTrustedIssuers.Add(certificateThumbprint, name);
		}

		// Token: 0x04000ACF RID: 2767
		private Dictionary<string, string> _configuredTrustedIssuers = new Dictionary<string, string>(new ConfigurationBasedIssuerNameRegistry.ThumbprintKeyComparer());

		// Token: 0x0200025D RID: 605
		private class ThumbprintKeyComparer : IEqualityComparer<string>
		{
			// Token: 0x0600125D RID: 4701 RVA: 0x000501EC File Offset: 0x0004E3EC
			public bool Equals(string x, string y)
			{
				return StringComparer.OrdinalIgnoreCase.Equals(x, y);
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x000501FA File Offset: 0x0004E3FA
			public int GetHashCode(string obj)
			{
				return obj.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
			}
		}
	}
}
