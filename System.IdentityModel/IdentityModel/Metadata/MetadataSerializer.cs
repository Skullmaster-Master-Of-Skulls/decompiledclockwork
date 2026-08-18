using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Configuration;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FF RID: 255
	public class MetadataSerializer
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0001AC0B File Offset: 0x00018E0B
		public MetadataSerializer() : this(new KeyInfoSerializer(true))
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001AC1C File Offset: 0x00018E1C
		public MetadataSerializer(SecurityTokenSerializer tokenSerializer)
		{
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer");
			}
			this._tokenSerializer = tokenSerializer;
			this.TrustedStoreLocation = IdentityConfiguration.DefaultTrustedStoreLocation;
			this.CertificateValidationMode = IdentityConfiguration.DefaultCertificateValidationMode;
			this.RevocationMode = IdentityConfiguration.DefaultRevocationMode;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001AC75 File Offset: 0x00018E75
		protected virtual ApplicationServiceDescriptor CreateApplicationServiceInstance()
		{
			return new ApplicationServiceDescriptor();
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001AC7C File Offset: 0x00018E7C
		protected virtual ContactPerson CreateContactPersonInstance()
		{
			return new ContactPerson();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001AC83 File Offset: 0x00018E83
		protected virtual ProtocolEndpoint CreateProtocolEndpointInstance()
		{
			return new ProtocolEndpoint();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001AC8A File Offset: 0x00018E8A
		protected virtual EntitiesDescriptor CreateEntitiesDescriptorInstance()
		{
			return new EntitiesDescriptor();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001AC91 File Offset: 0x00018E91
		protected virtual EntityDescriptor CreateEntityDescriptorInstance()
		{
			return new EntityDescriptor();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001AC98 File Offset: 0x00018E98
		protected virtual IdentityProviderSingleSignOnDescriptor CreateIdentityProviderSingleSignOnDescriptorInstance()
		{
			return new IdentityProviderSingleSignOnDescriptor();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001AC9F File Offset: 0x00018E9F
		protected virtual IndexedProtocolEndpoint CreateIndexedProtocolEndpointInstance()
		{
			return new IndexedProtocolEndpoint();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001ACA6 File Offset: 0x00018EA6
		protected virtual KeyDescriptor CreateKeyDescriptorInstance()
		{
			return new KeyDescriptor();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001ACAD File Offset: 0x00018EAD
		protected virtual LocalizedName CreateLocalizedNameInstance()
		{
			return new LocalizedName();
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		protected virtual LocalizedUri CreateLocalizedUriInstance()
		{
			return new LocalizedUri();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001ACBB File Offset: 0x00018EBB
		protected virtual Organization CreateOrganizationInstance()
		{
			return new Organization();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		protected virtual SecurityTokenServiceDescriptor CreateSecurityTokenServiceDescriptorInstance()
		{
			return new SecurityTokenServiceDescriptor();
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001ACC9 File Offset: 0x00018EC9
		protected virtual ServiceProviderSingleSignOnDescriptor CreateServiceProviderSingleSignOnDescriptorInstance()
		{
			return new ServiceProviderSingleSignOnDescriptor();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		private static ContactType GetContactPersonType(string conactType, out bool found)
		{
			if (conactType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("conactType");
			}
			found = true;
			if (StringComparer.Ordinal.Equals(conactType, "unspecified"))
			{
				return ContactType.Unspecified;
			}
			if (StringComparer.Ordinal.Equals(conactType, "administrative"))
			{
				return ContactType.Administrative;
			}
			if (StringComparer.Ordinal.Equals(conactType, "billing"))
			{
				return ContactType.Billing;
			}
			if (StringComparer.Ordinal.Equals(conactType, "other"))
			{
				return ContactType.Other;
			}
			if (StringComparer.Ordinal.Equals(conactType, "support"))
			{
				return ContactType.Support;
			}
			if (StringComparer.Ordinal.Equals(conactType, "technical"))
			{
				return ContactType.Technical;
			}
			found = false;
			return ContactType.Unspecified;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001AD70 File Offset: 0x00018F70
		private static KeyType GetKeyDescriptorType(string keyType)
		{
			if (keyType == null)
			{
				return KeyType.Unspecified;
			}
			if (StringComparer.Ordinal.Equals(keyType, "encryption"))
			{
				return KeyType.Encryption;
			}
			if (StringComparer.Ordinal.Equals(keyType, "signing"))
			{
				return KeyType.Signing;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
			{
				"use",
				keyType
			})));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		protected virtual ApplicationServiceDescriptor ReadApplicationServiceDescriptor(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			ApplicationServiceDescriptor applicationServiceDescriptor = this.CreateApplicationServiceInstance();
			this.ReadWebServiceDescriptorAttributes(reader, applicationServiceDescriptor);
			this.ReadCustomAttributes<ApplicationServiceDescriptor>(reader, applicationServiceDescriptor);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("ApplicationServiceEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706"))
					{
						isEmptyElement = reader.IsEmptyElement;
						reader.ReadStartElement();
						if (!isEmptyElement && reader.IsStartElement())
						{
							EndpointReference item = EndpointReference.ReadFrom(reader);
							applicationServiceDescriptor.Endpoints.Add(item);
							reader.ReadEndElement();
						}
					}
					else if (reader.IsStartElement("PassiveRequestorEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706"))
					{
						isEmptyElement = reader.IsEmptyElement;
						reader.ReadStartElement();
						if (!isEmptyElement && reader.IsStartElement())
						{
							EndpointReference item2 = EndpointReference.ReadFrom(reader);
							applicationServiceDescriptor.PassiveRequestorEndpoints.Add(item2);
							reader.ReadEndElement();
						}
					}
					else if (!this.ReadWebServiceDescriptorElement(reader, applicationServiceDescriptor) && !this.ReadCustomElement<ApplicationServiceDescriptor>(reader, applicationServiceDescriptor))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return applicationServiceDescriptor;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001AEE0 File Offset: 0x000190E0
		protected virtual ContactPerson ReadContactPerson(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			ContactPerson contactPerson = this.CreateContactPersonInstance();
			string attribute = reader.GetAttribute("contactType", null);
			bool flag = false;
			contactPerson.Type = MetadataSerializer.GetContactPersonType(attribute, out flag);
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3201", new object[]
				{
					typeof(ContactType),
					attribute
				})));
			}
			this.ReadCustomAttributes<ContactPerson>(reader, contactPerson);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("Company", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						contactPerson.Company = reader.ReadElementContentAsString("Company", "urn:oasis:names:tc:SAML:2.0:metadata");
					}
					else if (reader.IsStartElement("GivenName", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						contactPerson.GivenName = reader.ReadElementContentAsString("GivenName", "urn:oasis:names:tc:SAML:2.0:metadata");
					}
					else if (reader.IsStartElement("SurName", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						contactPerson.Surname = reader.ReadElementContentAsString("SurName", "urn:oasis:names:tc:SAML:2.0:metadata");
					}
					else if (reader.IsStartElement("EmailAddress", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						string text = reader.ReadElementContentAsString("EmailAddress", "urn:oasis:names:tc:SAML:2.0:metadata");
						if (!string.IsNullOrEmpty(text))
						{
							contactPerson.EmailAddresses.Add(text);
						}
					}
					else if (reader.IsStartElement("TelephoneNumber", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						string text2 = reader.ReadElementContentAsString("TelephoneNumber", "urn:oasis:names:tc:SAML:2.0:metadata");
						if (!string.IsNullOrEmpty(text2))
						{
							contactPerson.TelephoneNumbers.Add(text2);
						}
					}
					else if (!this.ReadCustomElement<ContactPerson>(reader, contactPerson))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return contactPerson;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ReadCustomAttributes<T>(XmlReader reader, T target)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00002D09 File Offset: 0x00000F09
		protected virtual bool ReadCustomElement<T>(XmlReader reader, T target)
		{
			return false;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001B099 File Offset: 0x00019299
		protected virtual void ReadCustomRoleDescriptor(string xsiType, XmlReader reader, EntityDescriptor entityDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID3274", new object[]
			{
				xsiType
			}), new object[0]);
			reader.Skip();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001B0D4 File Offset: 0x000192D4
		protected virtual DisplayClaim ReadDisplayClaim(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			string attribute = reader.GetAttribute("Uri", null);
			if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"ClaimType",
					attribute
				})));
			}
			DisplayClaim displayClaim = new DisplayClaim(attribute);
			bool optional = true;
			string attribute2 = reader.GetAttribute("Optional");
			if (!string.IsNullOrEmpty(attribute2))
			{
				try
				{
					optional = XmlConvert.ToBoolean(attribute2.ToLowerInvariant());
				}
				catch (FormatException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"Optional",
						attribute2
					})));
				}
			}
			displayClaim.Optional = optional;
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("DisplayName", "http://docs.oasis-open.org/wsfed/authorization/200706"))
					{
						displayClaim.DisplayTag = reader.ReadElementContentAsString("DisplayName", "http://docs.oasis-open.org/wsfed/authorization/200706");
					}
					else if (reader.IsStartElement("Description", "http://docs.oasis-open.org/wsfed/authorization/200706"))
					{
						displayClaim.Description = reader.ReadElementContentAsString("Description", "http://docs.oasis-open.org/wsfed/authorization/200706");
					}
					else
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return displayClaim;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001B224 File Offset: 0x00019424
		protected virtual EntitiesDescriptor ReadEntitiesDescriptor(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			MetadataSerializer.t_entitiesDescriptorDepth++;
			EntitiesDescriptor result;
			try
			{
				if (MetadataSerializer.t_entitiesDescriptorDepth > 8 && !LocalAppContextSwitches.AllowUnlimitedXmlRecursion)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID4194", new object[]
					{
						MetadataSerializer.t_entitiesDescriptorDepth,
						8
					})));
				}
				EntitiesDescriptor entitiesDescriptor = this.CreateEntitiesDescriptorInstance();
				EnvelopedSignatureReader envelopedSignatureReader = new EnvelopedSignatureReader(reader, this.SecurityTokenSerializer, tokenResolver, false, false, true);
				string attribute = envelopedSignatureReader.GetAttribute("Name", null);
				if (!string.IsNullOrEmpty(attribute))
				{
					entitiesDescriptor.Name = attribute;
				}
				this.ReadCustomAttributes<EntitiesDescriptor>(envelopedSignatureReader, entitiesDescriptor);
				bool isEmptyElement = envelopedSignatureReader.IsEmptyElement;
				envelopedSignatureReader.ReadStartElement();
				if (!isEmptyElement)
				{
					while (envelopedSignatureReader.IsStartElement())
					{
						if (envelopedSignatureReader.IsStartElement("EntityDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
						{
							entitiesDescriptor.ChildEntities.Add(this.ReadEntityDescriptor(envelopedSignatureReader, tokenResolver));
						}
						else if (envelopedSignatureReader.IsStartElement("EntitiesDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
						{
							entitiesDescriptor.ChildEntityGroups.Add(this.ReadEntitiesDescriptor(envelopedSignatureReader, tokenResolver));
						}
						else if (!envelopedSignatureReader.TryReadSignature() && !this.ReadCustomElement<EntitiesDescriptor>(envelopedSignatureReader, entitiesDescriptor))
						{
							envelopedSignatureReader.Skip();
						}
					}
					envelopedSignatureReader.ReadEndElement();
				}
				entitiesDescriptor.SigningCredentials = envelopedSignatureReader.SigningCredentials;
				if (entitiesDescriptor.SigningCredentials != null)
				{
					this.ValidateSigningCredential(entitiesDescriptor.SigningCredentials);
				}
				if (entitiesDescriptor.ChildEntityGroups.Count == 0 && entitiesDescriptor.ChildEntities.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3200", new object[]
					{
						"EntityDescriptor"
					})));
				}
				foreach (EntityDescriptor entityDescriptor in entitiesDescriptor.ChildEntities)
				{
					if (!string.IsNullOrEmpty(entityDescriptor.FederationId) && !StringComparer.Ordinal.Equals(entityDescriptor.FederationId, entitiesDescriptor.Name))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
						{
							"FederationID",
							entityDescriptor.FederationId
						})));
					}
				}
				result = entitiesDescriptor;
			}
			finally
			{
				MetadataSerializer.t_entitiesDescriptorDepth--;
			}
			return result;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001B48C File Offset: 0x0001968C
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001B494 File Offset: 0x00019694
		public X509CertificateValidationMode CertificateValidationMode { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001B49D File Offset: 0x0001969D
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001B4A5 File Offset: 0x000196A5
		public X509RevocationMode RevocationMode { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001B4AE File Offset: 0x000196AE
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001B4B6 File Offset: 0x000196B6
		public StoreLocation TrustedStoreLocation { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001B4BF File Offset: 0x000196BF
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001B4C7 File Offset: 0x000196C7
		public X509CertificateValidator CertificateValidator { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001B4D0 File Offset: 0x000196D0
		public List<string> TrustedIssuers
		{
			get
			{
				return this._trustedIssuers;
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001B4D8 File Offset: 0x000196D8
		protected virtual void ValidateSigningCredential(SigningCredentials signingCredentials)
		{
			if (signingCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signingCredentials");
			}
			if (this.CertificateValidationMode != X509CertificateValidationMode.Custom)
			{
				this.CertificateValidator = X509Util.CreateCertificateValidator(this.CertificateValidationMode, this.RevocationMode, this.TrustedStoreLocation);
			}
			else if (this.CertificateValidationMode == X509CertificateValidationMode.Custom && this.CertificateValidator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4280")));
			}
			X509Certificate2 metadataSigningCertificate = this.GetMetadataSigningCertificate(signingCredentials.SigningKeyIdentifier);
			this.ValidateIssuer(metadataSigningCertificate);
			this.CertificateValidator.Validate(metadataSigningCertificate);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ValidateIssuer(X509Certificate2 signingCertificate)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001B56C File Offset: 0x0001976C
		protected virtual X509Certificate2 GetMetadataSigningCertificate(SecurityKeyIdentifier ski)
		{
			if (ski == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ski");
			}
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = null;
			if (ski.TryFind<X509RawDataKeyIdentifierClause>(out x509RawDataKeyIdentifierClause))
			{
				return new X509Certificate2(x509RawDataKeyIdentifierClause.GetX509RawData());
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID8029")));
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001B5C0 File Offset: 0x000197C0
		protected virtual EntityDescriptor ReadEntityDescriptor(XmlReader inputReader, SecurityTokenResolver tokenResolver)
		{
			if (inputReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputReader");
			}
			EntityDescriptor entityDescriptor = this.CreateEntityDescriptorInstance();
			EnvelopedSignatureReader envelopedSignatureReader = new EnvelopedSignatureReader(inputReader, this.SecurityTokenSerializer, tokenResolver, false, false, true);
			string attribute = envelopedSignatureReader.GetAttribute("entityID", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				entityDescriptor.EntityId = new EntityId(attribute);
			}
			string attribute2 = envelopedSignatureReader.GetAttribute("FederationID", "http://docs.oasis-open.org/wsfed/federation/200706");
			if (!string.IsNullOrEmpty(attribute2))
			{
				entityDescriptor.FederationId = attribute2;
			}
			this.ReadCustomAttributes<EntityDescriptor>(envelopedSignatureReader, entityDescriptor);
			bool isEmptyElement = envelopedSignatureReader.IsEmptyElement;
			envelopedSignatureReader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (envelopedSignatureReader.IsStartElement())
				{
					if (envelopedSignatureReader.IsStartElement("SPSSODescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						entityDescriptor.RoleDescriptors.Add(this.ReadServiceProviderSingleSignOnDescriptor(envelopedSignatureReader));
					}
					else if (envelopedSignatureReader.IsStartElement("IDPSSODescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						entityDescriptor.RoleDescriptors.Add(this.ReadIdentityProviderSingleSignOnDescriptor(envelopedSignatureReader));
					}
					else if (envelopedSignatureReader.IsStartElement("RoleDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						string attribute3 = envelopedSignatureReader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
						if (string.IsNullOrEmpty(attribute3))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID0001", new object[]
							{
								"xsi:type",
								"RoleDescriptor"
							})));
						}
						int num = attribute3.IndexOf(":", 0, StringComparison.Ordinal);
						if (num < 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3207", new object[]
							{
								"xsi:type",
								"RoleDescriptor",
								attribute3
							})));
						}
						string text = attribute3.Substring(0, num);
						string text2 = envelopedSignatureReader.LookupNamespace(text);
						if (string.IsNullOrEmpty(text2))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
							{
								text,
								text2
							})));
						}
						if (!StringComparer.Ordinal.Equals(text2, "http://docs.oasis-open.org/wsfed/federation/200706"))
						{
							this.ReadCustomRoleDescriptor(attribute3, envelopedSignatureReader, entityDescriptor);
						}
						else if (StringComparer.Ordinal.Equals(attribute3, text + ":ApplicationServiceType"))
						{
							entityDescriptor.RoleDescriptors.Add(this.ReadApplicationServiceDescriptor(envelopedSignatureReader));
						}
						else
						{
							if (!StringComparer.Ordinal.Equals(attribute3, text + ":SecurityTokenServiceType"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3207", new object[]
								{
									"xsi:type",
									"RoleDescriptor",
									attribute3
								})));
							}
							entityDescriptor.RoleDescriptors.Add(this.ReadSecurityTokenServiceDescriptor(envelopedSignatureReader));
						}
					}
					else if (envelopedSignatureReader.IsStartElement("Organization", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						entityDescriptor.Organization = this.ReadOrganization(envelopedSignatureReader);
					}
					else if (envelopedSignatureReader.IsStartElement("ContactPerson", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						entityDescriptor.Contacts.Add(this.ReadContactPerson(envelopedSignatureReader));
					}
					else if (!envelopedSignatureReader.TryReadSignature() && !this.ReadCustomElement<EntityDescriptor>(envelopedSignatureReader, entityDescriptor))
					{
						envelopedSignatureReader.Skip();
					}
				}
				envelopedSignatureReader.ReadEndElement();
			}
			entityDescriptor.SigningCredentials = envelopedSignatureReader.SigningCredentials;
			if (entityDescriptor.SigningCredentials != null)
			{
				this.ValidateSigningCredential(entityDescriptor.SigningCredentials);
			}
			return entityDescriptor;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		protected virtual IdentityProviderSingleSignOnDescriptor ReadIdentityProviderSingleSignOnDescriptor(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			IdentityProviderSingleSignOnDescriptor identityProviderSingleSignOnDescriptor = this.CreateIdentityProviderSingleSignOnDescriptorInstance();
			this.ReadSingleSignOnDescriptorAttributes(reader, identityProviderSingleSignOnDescriptor);
			this.ReadCustomAttributes<IdentityProviderSingleSignOnDescriptor>(reader, identityProviderSingleSignOnDescriptor);
			string attribute = reader.GetAttribute("WantAuthnRequestsSigned");
			if (!string.IsNullOrEmpty(attribute))
			{
				try
				{
					identityProviderSingleSignOnDescriptor.WantAuthenticationRequestsSigned = XmlConvert.ToBoolean(attribute.ToLowerInvariant());
				}
				catch (FormatException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"WantAuthnRequestsSigned",
						attribute
					})));
				}
			}
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("SingleSignOnService", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						ProtocolEndpoint item = this.ReadProtocolEndpoint(reader);
						identityProviderSingleSignOnDescriptor.SingleSignOnServices.Add(item);
					}
					else if (reader.IsStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						identityProviderSingleSignOnDescriptor.SupportedAttributes.Add(this.ReadAttribute(reader));
					}
					else if (!this.ReadSingleSignOnDescriptorElement(reader, identityProviderSingleSignOnDescriptor) && !this.ReadCustomElement<IdentityProviderSingleSignOnDescriptor>(reader, identityProviderSingleSignOnDescriptor))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return identityProviderSingleSignOnDescriptor;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001BA14 File Offset: 0x00019C14
		protected virtual IndexedProtocolEndpoint ReadIndexedProtocolEndpoint(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			IndexedProtocolEndpoint indexedProtocolEndpoint = this.CreateIndexedProtocolEndpointInstance();
			string attribute = reader.GetAttribute("Binding", null);
			Uri binding;
			if (!UriUtil.TryCreateValidUri(attribute, UriKind.RelativeOrAbsolute, out binding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"Binding",
					attribute
				})));
			}
			indexedProtocolEndpoint.Binding = binding;
			string attribute2 = reader.GetAttribute("Location", null);
			Uri location;
			if (!UriUtil.TryCreateValidUri(attribute2, UriKind.RelativeOrAbsolute, out location))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"Location",
					attribute2
				})));
			}
			indexedProtocolEndpoint.Location = location;
			string attribute3 = reader.GetAttribute("index", null);
			int index;
			if (!int.TryParse(attribute3, out index))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"index",
					attribute3
				})));
			}
			indexedProtocolEndpoint.Index = index;
			string attribute4 = reader.GetAttribute("ResponseLocation", null);
			if (!string.IsNullOrEmpty(attribute4))
			{
				Uri responseLocation;
				if (!UriUtil.TryCreateValidUri(attribute4, UriKind.RelativeOrAbsolute, out responseLocation))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"ResponseLocation",
						attribute4
					})));
				}
				indexedProtocolEndpoint.ResponseLocation = responseLocation;
			}
			string attribute5 = reader.GetAttribute("isDefault", null);
			if (!string.IsNullOrEmpty(attribute5))
			{
				try
				{
					indexedProtocolEndpoint.IsDefault = new bool?(XmlConvert.ToBoolean(attribute5.ToLowerInvariant()));
				}
				catch (FormatException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"isDefault",
						attribute5
					})));
				}
			}
			this.ReadCustomAttributes<IndexedProtocolEndpoint>(reader, indexedProtocolEndpoint);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (!this.ReadCustomElement<IndexedProtocolEndpoint>(reader, indexedProtocolEndpoint))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return indexedProtocolEndpoint;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001BC24 File Offset: 0x00019E24
		protected virtual KeyDescriptor ReadKeyDescriptor(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			KeyDescriptor keyDescriptor = this.CreateKeyDescriptorInstance();
			string attribute = reader.GetAttribute("use", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				keyDescriptor.Use = MetadataSerializer.GetKeyDescriptorType(attribute);
			}
			this.ReadCustomAttributes<KeyDescriptor>(reader, keyDescriptor);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#"))
					{
						keyDescriptor.KeyInfo = this.SecurityTokenSerializer.ReadKeyIdentifier(reader);
					}
					else if (reader.IsStartElement("EncryptionMethod", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						string attribute2 = reader.GetAttribute("Algorithm");
						if (!string.IsNullOrEmpty(attribute2) && UriUtil.CanCreateValidUri(attribute2, UriKind.Absolute))
						{
							keyDescriptor.EncryptionMethods.Add(new EncryptionMethod(new Uri(attribute2)));
						}
						isEmptyElement = reader.IsEmptyElement;
						reader.ReadStartElement("EncryptionMethod", "urn:oasis:names:tc:SAML:2.0:metadata");
						if (!isEmptyElement)
						{
							while (reader.IsStartElement())
							{
								if (!this.ReadCustomElement<KeyDescriptor>(reader, keyDescriptor))
								{
									reader.Skip();
								}
							}
							reader.ReadEndElement();
						}
					}
					else if (!this.ReadCustomElement<KeyDescriptor>(reader, keyDescriptor))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			if (keyDescriptor.KeyInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3200", new object[]
				{
					"KeyInfo"
				})));
			}
			return keyDescriptor;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001BD88 File Offset: 0x00019F88
		protected virtual LocalizedName ReadLocalizedName(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			LocalizedName localizedName = this.CreateLocalizedNameInstance();
			string attribute = reader.GetAttribute("lang", "http://www.w3.org/XML/1998/namespace");
			try
			{
				localizedName.Language = CultureInfo.GetCultureInfo(attribute);
			}
			catch (ArgumentNullException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"lang",
					"null"
				})));
			}
			catch (ArgumentException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"lang",
					attribute
				})));
			}
			this.ReadCustomAttributes<LocalizedName>(reader, localizedName);
			bool isEmptyElement = reader.IsEmptyElement;
			string name = reader.Name;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				localizedName.Name = reader.ReadContentAsString();
				while (reader.IsStartElement())
				{
					if (!this.ReadCustomElement<LocalizedName>(reader, localizedName))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			if (string.IsNullOrEmpty(localizedName.Name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3200", new object[]
				{
					name
				})));
			}
			return localizedName;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		protected virtual LocalizedUri ReadLocalizedUri(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			LocalizedUri localizedUri = this.CreateLocalizedUriInstance();
			string attribute = reader.GetAttribute("lang", "http://www.w3.org/XML/1998/namespace");
			try
			{
				localizedUri.Language = CultureInfo.GetCultureInfo(attribute);
			}
			catch (ArgumentNullException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"lang",
					"null"
				})));
			}
			catch (ArgumentException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"lang",
					attribute
				})));
			}
			this.ReadCustomAttributes<LocalizedUri>(reader, localizedUri);
			bool isEmptyElement = reader.IsEmptyElement;
			string name = reader.Name;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				string text = reader.ReadContentAsString();
				Uri uri;
				if (!UriUtil.TryCreateValidUri(text, UriKind.RelativeOrAbsolute, out uri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						name,
						text
					})));
				}
				localizedUri.Uri = uri;
				while (reader.IsStartElement())
				{
					if (!this.ReadCustomElement<LocalizedUri>(reader, localizedUri))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			if (localizedUri.Uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3200", new object[]
				{
					name
				})));
			}
			return localizedUri;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001C040 File Offset: 0x0001A240
		public MetadataBase ReadMetadata(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, XmlDictionaryReaderQuotas.Max);
			return this.ReadMetadata(reader);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C073 File Offset: 0x0001A273
		public MetadataBase ReadMetadata(XmlReader reader)
		{
			return this.ReadMetadata(reader, EmptySecurityTokenResolver.Instance);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001C084 File Offset: 0x0001A284
		public MetadataBase ReadMetadata(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (tokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolver");
			}
			MetadataSerializer.t_entitiesDescriptorDepth = 0;
			KeyInfo.ResetReadDepth();
			KeyInfoSerializer.ResetReadDepth();
			if (!(reader is XmlDictionaryReader))
			{
				reader = XmlDictionaryReader.CreateDictionaryReader(reader);
			}
			return this.ReadMetadataCore(reader, tokenResolver);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		protected virtual MetadataBase ReadMetadataCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (tokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolver");
			}
			MetadataBase result;
			if (reader.IsStartElement("EntitiesDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				result = this.ReadEntitiesDescriptor(reader, tokenResolver);
			}
			else
			{
				if (!reader.IsStartElement("EntityDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3260")));
				}
				result = this.ReadEntityDescriptor(reader, tokenResolver);
			}
			return result;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C16C File Offset: 0x0001A36C
		protected virtual Organization ReadOrganization(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			Organization organization = this.CreateOrganizationInstance();
			this.ReadCustomAttributes<Organization>(reader, organization);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("OrganizationName", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						organization.Names.Add(this.ReadLocalizedName(reader));
					}
					else if (reader.IsStartElement("OrganizationDisplayName", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						organization.DisplayNames.Add(this.ReadLocalizedName(reader));
					}
					else if (reader.IsStartElement("OrganizationURL", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						organization.Urls.Add(this.ReadLocalizedUri(reader));
					}
					else if (!this.ReadCustomElement<Organization>(reader, organization))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return organization;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001C248 File Offset: 0x0001A448
		protected virtual ProtocolEndpoint ReadProtocolEndpoint(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			ProtocolEndpoint protocolEndpoint = this.CreateProtocolEndpointInstance();
			string attribute = reader.GetAttribute("Binding", null);
			Uri binding;
			if (!UriUtil.TryCreateValidUri(attribute, UriKind.RelativeOrAbsolute, out binding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"Binding",
					attribute
				})));
			}
			protocolEndpoint.Binding = binding;
			string attribute2 = reader.GetAttribute("Location", null);
			Uri location;
			if (!UriUtil.TryCreateValidUri(attribute2, UriKind.RelativeOrAbsolute, out location))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"Location",
					attribute2
				})));
			}
			protocolEndpoint.Location = location;
			string attribute3 = reader.GetAttribute("ResponseLocation", null);
			if (!string.IsNullOrEmpty(attribute3))
			{
				Uri responseLocation;
				if (!UriUtil.TryCreateValidUri(attribute3, UriKind.RelativeOrAbsolute, out responseLocation))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"ResponseLocation",
						attribute3
					})));
				}
				protocolEndpoint.ResponseLocation = responseLocation;
			}
			this.ReadCustomAttributes<ProtocolEndpoint>(reader, protocolEndpoint);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (!this.ReadCustomElement<ProtocolEndpoint>(reader, protocolEndpoint))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return protocolEndpoint;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C39C File Offset: 0x0001A59C
		protected virtual void ReadRoleDescriptorAttributes(XmlReader reader, RoleDescriptor roleDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			if (roleDescriptor.ProtocolsSupported == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.ProtocolsSupported");
			}
			string attribute = reader.GetAttribute("validUntil", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				DateTime validUntil;
				if (!DateTime.TryParse(attribute, out validUntil))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"validUntil",
						attribute
					})));
				}
				roleDescriptor.ValidUntil = validUntil;
			}
			string attribute2 = reader.GetAttribute("errorURL", null);
			if (!string.IsNullOrEmpty(attribute2))
			{
				Uri errorUrl;
				if (!UriUtil.TryCreateValidUri(attribute2, UriKind.RelativeOrAbsolute, out errorUrl))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"errorURL",
						attribute2
					})));
				}
				roleDescriptor.ErrorUrl = errorUrl;
			}
			string attribute3 = reader.GetAttribute("protocolSupportEnumeration", null);
			if (string.IsNullOrEmpty(attribute3))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
				{
					"protocolSupportEnumeration",
					attribute3
				})));
			}
			foreach (string text in attribute3.Split(new char[]
			{
				' '
			}))
			{
				string text2 = text.Trim();
				if (!string.IsNullOrEmpty(text2))
				{
					roleDescriptor.ProtocolsSupported.Add(new Uri(text2));
				}
			}
			this.ReadCustomAttributes<RoleDescriptor>(reader, roleDescriptor);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001C528 File Offset: 0x0001A728
		protected virtual bool ReadRoleDescriptorElement(XmlReader reader, RoleDescriptor roleDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			if (roleDescriptor.Contacts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.Contacts");
			}
			if (roleDescriptor.Keys == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.Keys");
			}
			if (reader.IsStartElement("Organization", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				roleDescriptor.Organization = this.ReadOrganization(reader);
				return true;
			}
			if (reader.IsStartElement("KeyDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				roleDescriptor.Keys.Add(this.ReadKeyDescriptor(reader));
				return true;
			}
			if (reader.IsStartElement("ContactPerson", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				roleDescriptor.Contacts.Add(this.ReadContactPerson(reader));
				return true;
			}
			return this.ReadCustomElement<RoleDescriptor>(reader, roleDescriptor);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001C600 File Offset: 0x0001A800
		protected virtual SecurityTokenServiceDescriptor ReadSecurityTokenServiceDescriptor(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SecurityTokenServiceDescriptor securityTokenServiceDescriptor = this.CreateSecurityTokenServiceDescriptorInstance();
			this.ReadWebServiceDescriptorAttributes(reader, securityTokenServiceDescriptor);
			this.ReadCustomAttributes<SecurityTokenServiceDescriptor>(reader, securityTokenServiceDescriptor);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("SecurityTokenServiceEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706"))
					{
						isEmptyElement = reader.IsEmptyElement;
						reader.ReadStartElement();
						if (!isEmptyElement && reader.IsStartElement())
						{
							EndpointReference item = EndpointReference.ReadFrom(reader);
							securityTokenServiceDescriptor.SecurityTokenServiceEndpoints.Add(item);
							reader.ReadEndElement();
						}
					}
					else if (reader.IsStartElement("PassiveRequestorEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706"))
					{
						isEmptyElement = reader.IsEmptyElement;
						reader.ReadStartElement();
						if (!isEmptyElement && reader.IsStartElement())
						{
							EndpointReference item2 = EndpointReference.ReadFrom(reader);
							securityTokenServiceDescriptor.PassiveRequestorEndpoints.Add(item2);
							reader.ReadEndElement();
						}
					}
					else if (!this.ReadWebServiceDescriptorElement(reader, securityTokenServiceDescriptor) && !this.ReadCustomElement<SecurityTokenServiceDescriptor>(reader, securityTokenServiceDescriptor))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return securityTokenServiceDescriptor;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001C708 File Offset: 0x0001A908
		protected virtual ServiceProviderSingleSignOnDescriptor ReadServiceProviderSingleSignOnDescriptor(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			ServiceProviderSingleSignOnDescriptor serviceProviderSingleSignOnDescriptor = this.CreateServiceProviderSingleSignOnDescriptorInstance();
			string attribute = reader.GetAttribute("AuthnRequestsSigned");
			if (!string.IsNullOrEmpty(attribute))
			{
				try
				{
					serviceProviderSingleSignOnDescriptor.AuthenticationRequestsSigned = XmlConvert.ToBoolean(attribute.ToLowerInvariant());
				}
				catch (FormatException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"AuthnRequestsSigned",
						attribute
					})));
				}
			}
			string attribute2 = reader.GetAttribute("WantAssertionsSigned");
			if (!string.IsNullOrEmpty(attribute2))
			{
				try
				{
					serviceProviderSingleSignOnDescriptor.WantAssertionsSigned = XmlConvert.ToBoolean(attribute2.ToLowerInvariant());
				}
				catch (FormatException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
					{
						"WantAssertionsSigned",
						attribute2
					})));
				}
			}
			this.ReadSingleSignOnDescriptorAttributes(reader, serviceProviderSingleSignOnDescriptor);
			this.ReadCustomAttributes<ServiceProviderSingleSignOnDescriptor>(reader, serviceProviderSingleSignOnDescriptor);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("AssertionConsumerService", "urn:oasis:names:tc:SAML:2.0:metadata"))
					{
						IndexedProtocolEndpoint indexedProtocolEndpoint = this.ReadIndexedProtocolEndpoint(reader);
						serviceProviderSingleSignOnDescriptor.AssertionConsumerServices.Add(indexedProtocolEndpoint.Index, indexedProtocolEndpoint);
					}
					else if (!this.ReadSingleSignOnDescriptorElement(reader, serviceProviderSingleSignOnDescriptor) && !this.ReadCustomElement<ServiceProviderSingleSignOnDescriptor>(reader, serviceProviderSingleSignOnDescriptor))
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
			return serviceProviderSingleSignOnDescriptor;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001C870 File Offset: 0x0001AA70
		protected virtual void ReadSingleSignOnDescriptorAttributes(XmlReader reader, SingleSignOnDescriptor roleDescriptor)
		{
			this.ReadRoleDescriptorAttributes(reader, roleDescriptor);
			this.ReadCustomAttributes<SingleSignOnDescriptor>(reader, roleDescriptor);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001C884 File Offset: 0x0001AA84
		protected virtual bool ReadSingleSignOnDescriptorElement(XmlReader reader, SingleSignOnDescriptor singleSignOnDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (singleSignOnDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ssoDescriptor");
			}
			if (this.ReadRoleDescriptorElement(reader, singleSignOnDescriptor))
			{
				return true;
			}
			if (reader.IsStartElement("ArtifactResolutionService", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				IndexedProtocolEndpoint indexedProtocolEndpoint = this.ReadIndexedProtocolEndpoint(reader);
				singleSignOnDescriptor.ArtifactResolutionServices.Add(indexedProtocolEndpoint.Index, indexedProtocolEndpoint);
				return true;
			}
			if (reader.IsStartElement("SingleLogoutService", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				singleSignOnDescriptor.SingleLogoutServices.Add(this.ReadProtocolEndpoint(reader));
				return true;
			}
			if (!reader.IsStartElement("NameIDFormat", "urn:oasis:names:tc:SAML:2.0:metadata"))
			{
				return this.ReadCustomElement<SingleSignOnDescriptor>(reader, singleSignOnDescriptor);
			}
			string uriString = reader.ReadElementContentAsString("NameIDFormat", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (!UriUtil.CanCreateValidUri(uriString, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID0014", new object[]
				{
					"NameIDFormat"
				})));
			}
			singleSignOnDescriptor.NameIdentifierFormats.Add(new Uri(uriString));
			return true;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001C988 File Offset: 0x0001AB88
		protected virtual void ReadWebServiceDescriptorAttributes(XmlReader reader, WebServiceDescriptor roleDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			this.ReadRoleDescriptorAttributes(reader, roleDescriptor);
			string attribute = reader.GetAttribute("ServiceDisplayName", null);
			if (!string.IsNullOrEmpty(attribute))
			{
				roleDescriptor.ServiceDisplayName = attribute;
			}
			string attribute2 = reader.GetAttribute("ServiceDescription", null);
			if (!string.IsNullOrEmpty(attribute2))
			{
				roleDescriptor.ServiceDescription = attribute2;
			}
			this.ReadCustomAttributes<WebServiceDescriptor>(reader, roleDescriptor);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001CA04 File Offset: 0x0001AC04
		public virtual bool ReadWebServiceDescriptorElement(XmlReader reader, WebServiceDescriptor roleDescriptor)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			if (roleDescriptor.TargetScopes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.TargetScopes");
			}
			if (roleDescriptor.ClaimTypesOffered == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.TargetScopes");
			}
			if (roleDescriptor.TokenTypesOffered == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.TokenTypesOffered");
			}
			if (this.ReadRoleDescriptorElement(reader, roleDescriptor))
			{
				return true;
			}
			if (reader.IsStartElement("TargetScopes", "http://docs.oasis-open.org/wsfed/federation/200706"))
			{
				bool isEmptyElement = reader.IsEmptyElement;
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement())
					{
						roleDescriptor.TargetScopes.Add(EndpointReference.ReadFrom(reader));
					}
					reader.ReadEndElement();
				}
				return true;
			}
			if (reader.IsStartElement("ClaimTypesOffered", "http://docs.oasis-open.org/wsfed/federation/200706"))
			{
				bool isEmptyElement2 = reader.IsEmptyElement;
				reader.ReadStartElement();
				if (!isEmptyElement2)
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("ClaimType", "http://docs.oasis-open.org/wsfed/authorization/200706"))
						{
							roleDescriptor.ClaimTypesOffered.Add(this.ReadDisplayClaim(reader));
						}
						else
						{
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				return true;
			}
			if (reader.IsStartElement("ClaimTypesRequested", "http://docs.oasis-open.org/wsfed/federation/200706"))
			{
				bool isEmptyElement3 = reader.IsEmptyElement;
				reader.ReadStartElement();
				if (!isEmptyElement3)
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("ClaimType", "http://docs.oasis-open.org/wsfed/authorization/200706"))
						{
							roleDescriptor.ClaimTypesRequested.Add(this.ReadDisplayClaim(reader));
						}
						else
						{
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				return true;
			}
			if (reader.IsStartElement("TokenTypesOffered", "http://docs.oasis-open.org/wsfed/federation/200706"))
			{
				bool isEmptyElement4 = reader.IsEmptyElement;
				reader.ReadStartElement("TokenTypesOffered", "http://docs.oasis-open.org/wsfed/federation/200706");
				if (!isEmptyElement4)
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("TokenType", "http://docs.oasis-open.org/wsfed/federation/200706"))
						{
							string attribute = reader.GetAttribute("Uri", null);
							Uri item;
							if (!UriUtil.TryCreateValidUri(attribute, UriKind.Absolute, out item))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3202", new object[]
								{
									"TokenType",
									attribute
								})));
							}
							roleDescriptor.TokenTypesOffered.Add(item);
							isEmptyElement4 = reader.IsEmptyElement;
							reader.ReadStartElement();
							if (!isEmptyElement4)
							{
								reader.ReadEndElement();
							}
						}
						else
						{
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				return true;
			}
			return this.ReadCustomElement<WebServiceDescriptor>(reader, roleDescriptor);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001CC66 File Offset: 0x0001AE66
		public SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this._tokenSerializer;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001CC70 File Offset: 0x0001AE70
		protected virtual void WriteApplicationServiceDescriptor(XmlWriter writer, ApplicationServiceDescriptor appService)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (appService == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("appService");
			}
			if (appService.Endpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("appService.Endpoints");
			}
			if (appService.PassiveRequestorEndpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("appService.PassiveRequestorEndpoints");
			}
			writer.WriteStartElement("RoleDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "fed:ApplicationServiceType");
			writer.WriteAttributeString("xmlns", "fed", null, "http://docs.oasis-open.org/wsfed/federation/200706");
			this.WriteWebServiceDescriptorAttributes(writer, appService);
			this.WriteCustomAttributes<ApplicationServiceDescriptor>(writer, appService);
			this.WriteWebServiceDescriptorElements(writer, appService);
			foreach (EndpointReference endpointReference in appService.Endpoints)
			{
				writer.WriteStartElement("ApplicationServiceEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706");
				endpointReference.WriteTo(writer);
				writer.WriteEndElement();
			}
			foreach (EndpointReference endpointReference2 in appService.PassiveRequestorEndpoints)
			{
				writer.WriteStartElement("PassiveRequestorEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706");
				endpointReference2.WriteTo(writer);
				writer.WriteEndElement();
			}
			this.WriteCustomElements<ApplicationServiceDescriptor>(writer, appService);
			writer.WriteEndElement();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		protected virtual void WriteContactPerson(XmlWriter writer, ContactPerson contactPerson)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (contactPerson == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contactPerson");
			}
			if (contactPerson.EmailAddresses == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contactPerson.EmailAddresses");
			}
			if (contactPerson.TelephoneNumbers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contactPerson.TelephoneNumbers");
			}
			writer.WriteStartElement("ContactPerson", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (contactPerson.Type == ContactType.Unspecified)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"contactType"
				})));
			}
			writer.WriteAttributeString("contactType", null, contactPerson.Type.ToString().ToLowerInvariant());
			this.WriteCustomAttributes<ContactPerson>(writer, contactPerson);
			if (!string.IsNullOrEmpty(contactPerson.Company))
			{
				writer.WriteElementString("Company", "urn:oasis:names:tc:SAML:2.0:metadata", contactPerson.Company);
			}
			if (!string.IsNullOrEmpty(contactPerson.GivenName))
			{
				writer.WriteElementString("GivenName", "urn:oasis:names:tc:SAML:2.0:metadata", contactPerson.GivenName);
			}
			if (!string.IsNullOrEmpty(contactPerson.Surname))
			{
				writer.WriteElementString("SurName", "urn:oasis:names:tc:SAML:2.0:metadata", contactPerson.Surname);
			}
			foreach (string value in contactPerson.EmailAddresses)
			{
				writer.WriteElementString("EmailAddress", "urn:oasis:names:tc:SAML:2.0:metadata", value);
			}
			foreach (string value2 in contactPerson.TelephoneNumbers)
			{
				writer.WriteElementString("TelephoneNumber", "urn:oasis:names:tc:SAML:2.0:metadata", value2);
			}
			this.WriteCustomElements<ContactPerson>(writer, contactPerson);
			writer.WriteEndElement();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void WriteCustomAttributes<T>(XmlWriter writer, T source)
		{
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void WriteCustomElements<T>(XmlWriter writer, T source)
		{
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		protected virtual void WriteProtocolEndpoint(XmlWriter writer, ProtocolEndpoint endpoint, XmlQualifiedName element)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			writer.WriteStartElement(element.Name, element.Namespace);
			if (endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"Binding"
				})));
			}
			writer.WriteAttributeString("Binding", null, endpoint.Binding.IsAbsoluteUri ? endpoint.Binding.AbsoluteUri : endpoint.Binding.ToString());
			if (endpoint.Location == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"Location"
				})));
			}
			writer.WriteAttributeString("Location", null, endpoint.Location.IsAbsoluteUri ? endpoint.Location.AbsoluteUri : endpoint.Location.ToString());
			if (endpoint.ResponseLocation != null)
			{
				writer.WriteAttributeString("ResponseLocation", null, endpoint.ResponseLocation.IsAbsoluteUri ? endpoint.ResponseLocation.AbsoluteUri : endpoint.ResponseLocation.ToString());
			}
			this.WriteCustomAttributes<ProtocolEndpoint>(writer, endpoint);
			this.WriteCustomElements<ProtocolEndpoint>(writer, endpoint);
			writer.WriteEndElement();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001D144 File Offset: 0x0001B344
		protected virtual void WriteDisplayClaim(XmlWriter writer, DisplayClaim claim)
		{
			writer.WriteStartElement("auth", "ClaimType", "http://docs.oasis-open.org/wsfed/authorization/200706");
			if (string.IsNullOrEmpty(claim.ClaimType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"ClaimType"
				})));
			}
			if (!UriUtil.CanCreateValidUri(claim.ClaimType, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID0014", new object[]
				{
					claim.ClaimType
				})));
			}
			writer.WriteAttributeString("Uri", claim.ClaimType);
			if (claim.WriteOptionalAttribute)
			{
				writer.WriteAttributeString("Optional", XmlConvert.ToString(claim.Optional));
			}
			if (!string.IsNullOrEmpty(claim.DisplayTag))
			{
				writer.WriteElementString("auth", "DisplayName", "http://docs.oasis-open.org/wsfed/authorization/200706", claim.DisplayTag);
			}
			if (!string.IsNullOrEmpty(claim.Description))
			{
				writer.WriteElementString("auth", "Description", "http://docs.oasis-open.org/wsfed/authorization/200706", claim.Description);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001D258 File Offset: 0x0001B458
		protected virtual void WriteEntitiesDescriptor(XmlWriter inputWriter, EntitiesDescriptor entitiesDescriptor)
		{
			if (inputWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputWriter");
			}
			if (entitiesDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entitiesDescriptor");
			}
			if (entitiesDescriptor.ChildEntities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entitiesDescriptor.ChildEntities");
			}
			if (entitiesDescriptor.ChildEntityGroups == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entitiesDescriptor.ChildEntityGroups");
			}
			string text = "_" + Guid.NewGuid().ToString();
			XmlWriter xmlWriter = inputWriter;
			EnvelopedSignatureWriter envelopedSignatureWriter = null;
			if (entitiesDescriptor.SigningCredentials != null)
			{
				envelopedSignatureWriter = new EnvelopedSignatureWriter(inputWriter, entitiesDescriptor.SigningCredentials, text, this.SecurityTokenSerializer);
				xmlWriter = envelopedSignatureWriter;
			}
			xmlWriter.WriteStartElement("EntitiesDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			xmlWriter.WriteAttributeString("ID", null, text);
			if (entitiesDescriptor.ChildEntities.Count == 0 && entitiesDescriptor.ChildEntityGroups.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"EntitiesDescriptor"
				})));
			}
			foreach (EntityDescriptor entityDescriptor in entitiesDescriptor.ChildEntities)
			{
				if (!string.IsNullOrEmpty(entityDescriptor.FederationId) && !StringComparer.Ordinal.Equals(entityDescriptor.FederationId, entitiesDescriptor.Name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
					{
						"FederationID"
					})));
				}
			}
			if (!string.IsNullOrEmpty(entitiesDescriptor.Name))
			{
				xmlWriter.WriteAttributeString("Name", null, entitiesDescriptor.Name);
			}
			this.WriteCustomAttributes<EntitiesDescriptor>(xmlWriter, entitiesDescriptor);
			if (envelopedSignatureWriter != null)
			{
				envelopedSignatureWriter.WriteSignature();
			}
			foreach (EntityDescriptor entityDescriptor2 in entitiesDescriptor.ChildEntities)
			{
				this.WriteEntityDescriptor(xmlWriter, entityDescriptor2);
			}
			foreach (EntitiesDescriptor entitiesDescriptor2 in entitiesDescriptor.ChildEntityGroups)
			{
				this.WriteEntitiesDescriptor(xmlWriter, entitiesDescriptor2);
			}
			this.WriteCustomElements<EntitiesDescriptor>(xmlWriter, entitiesDescriptor);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		protected virtual void WriteEntityDescriptor(XmlWriter inputWriter, EntityDescriptor entityDescriptor)
		{
			if (inputWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputWriter");
			}
			if (entityDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entityDescriptor");
			}
			if (entityDescriptor.Contacts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entityDescriptor.Contacts");
			}
			if (entityDescriptor.RoleDescriptors == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("entityDescriptor.RoleDescriptors");
			}
			string text = "_" + Guid.NewGuid().ToString();
			XmlWriter xmlWriter = inputWriter;
			EnvelopedSignatureWriter envelopedSignatureWriter = null;
			if (entityDescriptor.SigningCredentials != null)
			{
				envelopedSignatureWriter = new EnvelopedSignatureWriter(inputWriter, entityDescriptor.SigningCredentials, text, this.SecurityTokenSerializer);
				xmlWriter = envelopedSignatureWriter;
			}
			xmlWriter.WriteStartElement("EntityDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			xmlWriter.WriteAttributeString("ID", null, text);
			if (entityDescriptor.EntityId == null || entityDescriptor.EntityId.Id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"entityID"
				})));
			}
			xmlWriter.WriteAttributeString("entityID", null, entityDescriptor.EntityId.Id);
			if (!string.IsNullOrEmpty(entityDescriptor.FederationId))
			{
				xmlWriter.WriteAttributeString("FederationID", "http://docs.oasis-open.org/wsfed/federation/200706", entityDescriptor.FederationId);
			}
			this.WriteCustomAttributes<EntityDescriptor>(xmlWriter, entityDescriptor);
			if (envelopedSignatureWriter != null)
			{
				envelopedSignatureWriter.WriteSignature();
			}
			if (entityDescriptor.RoleDescriptors.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"RoleDescriptor"
				})));
			}
			foreach (RoleDescriptor roleDescriptor in entityDescriptor.RoleDescriptors)
			{
				ServiceProviderSingleSignOnDescriptor serviceProviderSingleSignOnDescriptor = roleDescriptor as ServiceProviderSingleSignOnDescriptor;
				if (serviceProviderSingleSignOnDescriptor != null)
				{
					this.WriteServiceProviderSingleSignOnDescriptor(xmlWriter, serviceProviderSingleSignOnDescriptor);
				}
				IdentityProviderSingleSignOnDescriptor identityProviderSingleSignOnDescriptor = roleDescriptor as IdentityProviderSingleSignOnDescriptor;
				if (identityProviderSingleSignOnDescriptor != null)
				{
					this.WriteIdentityProviderSingleSignOnDescriptor(xmlWriter, identityProviderSingleSignOnDescriptor);
				}
				ApplicationServiceDescriptor applicationServiceDescriptor = roleDescriptor as ApplicationServiceDescriptor;
				if (applicationServiceDescriptor != null)
				{
					this.WriteApplicationServiceDescriptor(xmlWriter, applicationServiceDescriptor);
				}
				SecurityTokenServiceDescriptor securityTokenServiceDescriptor = roleDescriptor as SecurityTokenServiceDescriptor;
				if (securityTokenServiceDescriptor != null)
				{
					this.WriteSecurityTokenServiceDescriptor(xmlWriter, securityTokenServiceDescriptor);
				}
			}
			if (entityDescriptor.Organization != null)
			{
				this.WriteOrganization(xmlWriter, entityDescriptor.Organization);
			}
			foreach (ContactPerson contactPerson in entityDescriptor.Contacts)
			{
				this.WriteContactPerson(xmlWriter, contactPerson);
			}
			this.WriteCustomElements<EntityDescriptor>(xmlWriter, entityDescriptor);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001D72C File Offset: 0x0001B92C
		protected virtual void WriteIdentityProviderSingleSignOnDescriptor(XmlWriter writer, IdentityProviderSingleSignOnDescriptor identityProviderSingleSignOnDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (identityProviderSingleSignOnDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("idpssoDescriptor");
			}
			if (identityProviderSingleSignOnDescriptor.SupportedAttributes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("idpssoDescriptor.SupportedAttributes");
			}
			if (identityProviderSingleSignOnDescriptor.SingleSignOnServices == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("idpssoDescriptor.SingleSignOnServices");
			}
			writer.WriteStartElement("IDPSSODescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (identityProviderSingleSignOnDescriptor.WantAuthenticationRequestsSigned)
			{
				writer.WriteAttributeString("WantAuthnRequestsSigned", null, XmlConvert.ToString(identityProviderSingleSignOnDescriptor.WantAuthenticationRequestsSigned));
			}
			this.WriteSingleSignOnDescriptorAttributes(writer, identityProviderSingleSignOnDescriptor);
			this.WriteCustomAttributes<IdentityProviderSingleSignOnDescriptor>(writer, identityProviderSingleSignOnDescriptor);
			this.WriteSingleSignOnDescriptorElements(writer, identityProviderSingleSignOnDescriptor);
			if (identityProviderSingleSignOnDescriptor.SingleSignOnServices.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"SingleSignOnService"
				})));
			}
			foreach (ProtocolEndpoint protocolEndpoint in identityProviderSingleSignOnDescriptor.SingleSignOnServices)
			{
				if (protocolEndpoint.ResponseLocation != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3249", new object[]
					{
						"ResponseLocation"
					})));
				}
				XmlQualifiedName element = new XmlQualifiedName("SingleSignOnService", "urn:oasis:names:tc:SAML:2.0:metadata");
				this.WriteProtocolEndpoint(writer, protocolEndpoint, element);
			}
			foreach (Saml2Attribute data in identityProviderSingleSignOnDescriptor.SupportedAttributes)
			{
				this.WriteAttribute(writer, data);
			}
			this.WriteCustomElements<IdentityProviderSingleSignOnDescriptor>(writer, identityProviderSingleSignOnDescriptor);
			writer.WriteEndElement();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		protected virtual void WriteIndexedProtocolEndpoint(XmlWriter writer, IndexedProtocolEndpoint indexedEP, XmlQualifiedName element)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (indexedEP == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("indexedEP");
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			writer.WriteStartElement(element.Name, element.Namespace);
			if (indexedEP.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"Binding"
				})));
			}
			writer.WriteAttributeString("Binding", null, indexedEP.Binding.IsAbsoluteUri ? indexedEP.Binding.AbsoluteUri : indexedEP.Binding.ToString());
			if (indexedEP.Location == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"Location"
				})));
			}
			writer.WriteAttributeString("Location", null, indexedEP.Location.IsAbsoluteUri ? indexedEP.Location.AbsoluteUri : indexedEP.Location.ToString());
			if (indexedEP.Index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"index"
				})));
			}
			writer.WriteAttributeString("index", null, indexedEP.Index.ToString(CultureInfo.InvariantCulture));
			if (indexedEP.ResponseLocation != null)
			{
				writer.WriteAttributeString("ResponseLocation", null, indexedEP.ResponseLocation.IsAbsoluteUri ? indexedEP.ResponseLocation.AbsoluteUri : indexedEP.ResponseLocation.ToString());
			}
			if (indexedEP.IsDefault != null)
			{
				writer.WriteAttributeString("isDefault", null, XmlConvert.ToString(indexedEP.IsDefault.Value));
			}
			this.WriteCustomAttributes<IndexedProtocolEndpoint>(writer, indexedEP);
			this.WriteCustomElements<IndexedProtocolEndpoint>(writer, indexedEP);
			writer.WriteEndElement();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		protected virtual void WriteKeyDescriptor(XmlWriter writer, KeyDescriptor keyDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (keyDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyDescriptor");
			}
			writer.WriteStartElement("KeyDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (keyDescriptor.Use == KeyType.Encryption || keyDescriptor.Use == KeyType.Signing)
			{
				writer.WriteAttributeString("use", null, keyDescriptor.Use.ToString().ToLowerInvariant());
			}
			this.WriteCustomAttributes<KeyDescriptor>(writer, keyDescriptor);
			if (keyDescriptor.KeyInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"KeyInfo"
				})));
			}
			this.SecurityTokenSerializer.WriteKeyIdentifier(writer, keyDescriptor.KeyInfo);
			if (keyDescriptor.EncryptionMethods != null && keyDescriptor.EncryptionMethods.Count > 0)
			{
				foreach (EncryptionMethod encryptionMethod in keyDescriptor.EncryptionMethods)
				{
					if (encryptionMethod.Algorithm == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
						{
							"Algorithm"
						})));
					}
					if (!encryptionMethod.Algorithm.IsAbsoluteUri)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID0014", new object[]
						{
							"Algorithm"
						})));
					}
					writer.WriteStartElement("EncryptionMethod", "urn:oasis:names:tc:SAML:2.0:metadata");
					writer.WriteAttributeString("Algorithm", null, encryptionMethod.Algorithm.AbsoluteUri);
					writer.WriteEndElement();
				}
			}
			this.WriteCustomElements<KeyDescriptor>(writer, keyDescriptor);
			writer.WriteEndElement();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
		protected virtual void WriteLocalizedName(XmlWriter writer, LocalizedName name, XmlQualifiedName element)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			if (name.Name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name.Name");
			}
			writer.WriteStartElement(element.Name, element.Namespace);
			if (name.Language == null || string.IsNullOrEmpty(name.Name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"lang"
				})));
			}
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", name.Language.Name);
			this.WriteCustomAttributes<LocalizedName>(writer, name);
			writer.WriteString(name.Name);
			this.WriteCustomElements<LocalizedName>(writer, name);
			writer.WriteEndElement();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
		protected virtual void WriteLocalizedUri(XmlWriter writer, LocalizedUri uri, XmlQualifiedName element)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			writer.WriteStartElement(element.Name, element.Namespace);
			if (uri.Language == null || uri.Uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"lang"
				})));
			}
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", uri.Language.Name);
			this.WriteCustomAttributes<LocalizedUri>(writer, uri);
			writer.WriteString(uri.Uri.IsAbsoluteUri ? uri.Uri.AbsoluteUri : uri.Uri.ToString());
			this.WriteCustomElements<LocalizedUri>(writer, uri);
			writer.WriteEndElement();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		public void WriteMetadata(Stream stream, MetadataBase metadata)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (metadata == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("metadata");
			}
			using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, false))
			{
				this.WriteMetadata(xmlDictionaryWriter, metadata);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001DF04 File Offset: 0x0001C104
		public void WriteMetadata(XmlWriter writer, MetadataBase metadata)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (metadata == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("metadata");
			}
			this.WriteMetadataCore(writer, metadata);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001DF34 File Offset: 0x0001C134
		protected virtual void WriteMetadataCore(XmlWriter writer, MetadataBase metadataBase)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (metadataBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("metadataBase");
			}
			EntitiesDescriptor entitiesDescriptor = metadataBase as EntitiesDescriptor;
			if (entitiesDescriptor != null)
			{
				this.WriteEntitiesDescriptor(writer, entitiesDescriptor);
				return;
			}
			EntityDescriptor entityDescriptor = metadataBase as EntityDescriptor;
			if (entityDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"EntitiesDescriptor"
				})));
			}
			this.WriteEntityDescriptor(writer, entityDescriptor);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		protected virtual void WriteOrganization(XmlWriter writer, Organization organization)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (organization == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("organization");
			}
			if (organization.DisplayNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("organization.DisplayNames");
			}
			if (organization.Names == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("organization.Names");
			}
			if (organization.Urls == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("organization.Urls");
			}
			writer.WriteStartElement("Organization", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (organization.Names.Count < 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"OrganizationName"
				})));
			}
			foreach (LocalizedName name in organization.Names)
			{
				XmlQualifiedName element = new XmlQualifiedName("OrganizationName", "urn:oasis:names:tc:SAML:2.0:metadata");
				this.WriteLocalizedName(writer, name, element);
			}
			if (organization.DisplayNames.Count < 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"OrganizationDisplayName"
				})));
			}
			foreach (LocalizedName name2 in organization.DisplayNames)
			{
				XmlQualifiedName element2 = new XmlQualifiedName("OrganizationDisplayName", "urn:oasis:names:tc:SAML:2.0:metadata");
				this.WriteLocalizedName(writer, name2, element2);
			}
			if (organization.Urls.Count < 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"OrganizationURL"
				})));
			}
			foreach (LocalizedUri uri in organization.Urls)
			{
				XmlQualifiedName element3 = new XmlQualifiedName("OrganizationURL", "urn:oasis:names:tc:SAML:2.0:metadata");
				this.WriteLocalizedUri(writer, uri, element3);
			}
			this.WriteCustomAttributes<Organization>(writer, organization);
			this.WriteCustomElements<Organization>(writer, organization);
			writer.WriteEndElement();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		protected virtual void WriteRoleDescriptorAttributes(XmlWriter writer, RoleDescriptor roleDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			if (roleDescriptor.ProtocolsSupported == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.ProtocolsSupported");
			}
			DateTime validUntil = roleDescriptor.ValidUntil;
			if (roleDescriptor.ValidUntil != DateTime.MaxValue)
			{
				writer.WriteAttributeString("validUntil", null, roleDescriptor.ValidUntil.ToString("s", CultureInfo.InvariantCulture));
			}
			if (roleDescriptor.ErrorUrl != null)
			{
				writer.WriteAttributeString("errorURL", null, roleDescriptor.ErrorUrl.IsAbsoluteUri ? roleDescriptor.ErrorUrl.AbsoluteUri : roleDescriptor.ErrorUrl.ToString());
			}
			if (roleDescriptor.ProtocolsSupported.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"protocolSupportEnumeration"
				})));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Uri uri in roleDescriptor.ProtocolsSupported)
			{
				stringBuilder.AppendFormat("{0} ", uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.ToString());
			}
			string text = stringBuilder.ToString();
			writer.WriteAttributeString("protocolSupportEnumeration", null, text.Trim());
			this.WriteCustomAttributes<RoleDescriptor>(writer, roleDescriptor);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001E378 File Offset: 0x0001C578
		protected virtual void WriteRoleDescriptorElements(XmlWriter writer, RoleDescriptor roleDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (roleDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor");
			}
			if (roleDescriptor.Contacts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.Contacts");
			}
			if (roleDescriptor.Keys == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("roleDescriptor.Keys");
			}
			if (roleDescriptor.Organization != null)
			{
				this.WriteOrganization(writer, roleDescriptor.Organization);
			}
			foreach (KeyDescriptor keyDescriptor in roleDescriptor.Keys)
			{
				this.WriteKeyDescriptor(writer, keyDescriptor);
			}
			foreach (ContactPerson contactPerson in roleDescriptor.Contacts)
			{
				this.WriteContactPerson(writer, contactPerson);
			}
			this.WriteCustomElements<RoleDescriptor>(writer, roleDescriptor);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001E478 File Offset: 0x0001C678
		protected virtual void WriteSecurityTokenServiceDescriptor(XmlWriter writer, SecurityTokenServiceDescriptor securityTokenServiceDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (securityTokenServiceDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenServiceDescriptor");
			}
			if (securityTokenServiceDescriptor.SecurityTokenServiceEndpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenServiceDescriptor.Endpoints");
			}
			if (securityTokenServiceDescriptor.PassiveRequestorEndpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenServiceDescriptor.PassiveRequestorEndpoints");
			}
			writer.WriteStartElement("RoleDescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "fed:SecurityTokenServiceType");
			writer.WriteAttributeString("xmlns", "fed", null, "http://docs.oasis-open.org/wsfed/federation/200706");
			this.WriteWebServiceDescriptorAttributes(writer, securityTokenServiceDescriptor);
			this.WriteCustomAttributes<SecurityTokenServiceDescriptor>(writer, securityTokenServiceDescriptor);
			this.WriteWebServiceDescriptorElements(writer, securityTokenServiceDescriptor);
			if (securityTokenServiceDescriptor.SecurityTokenServiceEndpoints.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"SecurityTokenServiceEndpoint"
				})));
			}
			foreach (EndpointReference endpointReference in securityTokenServiceDescriptor.SecurityTokenServiceEndpoints)
			{
				writer.WriteStartElement("SecurityTokenServiceEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706");
				endpointReference.WriteTo(writer);
				writer.WriteEndElement();
			}
			foreach (EndpointReference endpointReference2 in securityTokenServiceDescriptor.PassiveRequestorEndpoints)
			{
				writer.WriteStartElement("PassiveRequestorEndpoint", "http://docs.oasis-open.org/wsfed/federation/200706");
				endpointReference2.WriteTo(writer);
				writer.WriteEndElement();
			}
			this.WriteCustomElements<SecurityTokenServiceDescriptor>(writer, securityTokenServiceDescriptor);
			writer.WriteEndElement();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001E620 File Offset: 0x0001C820
		protected virtual void WriteServiceProviderSingleSignOnDescriptor(XmlWriter writer, ServiceProviderSingleSignOnDescriptor serviceProviderSingleSignOnDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (serviceProviderSingleSignOnDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("spssoDescriptor");
			}
			if (serviceProviderSingleSignOnDescriptor.AssertionConsumerServices == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("spssoDescriptor.AssertionConsumerService");
			}
			writer.WriteStartElement("SPSSODescriptor", "urn:oasis:names:tc:SAML:2.0:metadata");
			if (serviceProviderSingleSignOnDescriptor.AuthenticationRequestsSigned)
			{
				writer.WriteAttributeString("AuthnRequestsSigned", null, XmlConvert.ToString(serviceProviderSingleSignOnDescriptor.AuthenticationRequestsSigned));
			}
			if (serviceProviderSingleSignOnDescriptor.WantAssertionsSigned)
			{
				writer.WriteAttributeString("WantAssertionsSigned", null, XmlConvert.ToString(serviceProviderSingleSignOnDescriptor.WantAssertionsSigned));
			}
			this.WriteSingleSignOnDescriptorAttributes(writer, serviceProviderSingleSignOnDescriptor);
			this.WriteCustomAttributes<ServiceProviderSingleSignOnDescriptor>(writer, serviceProviderSingleSignOnDescriptor);
			this.WriteSingleSignOnDescriptorElements(writer, serviceProviderSingleSignOnDescriptor);
			if (serviceProviderSingleSignOnDescriptor.AssertionConsumerServices.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
				{
					"AssertionConsumerService"
				})));
			}
			foreach (IndexedProtocolEndpoint indexedEP in serviceProviderSingleSignOnDescriptor.AssertionConsumerServices.Values)
			{
				XmlQualifiedName element = new XmlQualifiedName("AssertionConsumerService", "urn:oasis:names:tc:SAML:2.0:metadata");
				this.WriteIndexedProtocolEndpoint(writer, indexedEP, element);
			}
			this.WriteCustomElements<ServiceProviderSingleSignOnDescriptor>(writer, serviceProviderSingleSignOnDescriptor);
			writer.WriteEndElement();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001E76C File Offset: 0x0001C96C
		protected virtual void WriteSingleSignOnDescriptorAttributes(XmlWriter writer, SingleSignOnDescriptor singleSignOnDescriptor)
		{
			this.WriteRoleDescriptorAttributes(writer, singleSignOnDescriptor);
			this.WriteCustomAttributes<SingleSignOnDescriptor>(writer, singleSignOnDescriptor);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001E780 File Offset: 0x0001C980
		protected virtual void WriteSingleSignOnDescriptorElements(XmlWriter writer, SingleSignOnDescriptor singleSignOnDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (singleSignOnDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ssoDescriptor");
			}
			this.WriteRoleDescriptorElements(writer, singleSignOnDescriptor);
			if (singleSignOnDescriptor.ArtifactResolutionServices != null && singleSignOnDescriptor.ArtifactResolutionServices.Count > 0)
			{
				foreach (IndexedProtocolEndpoint indexedProtocolEndpoint in singleSignOnDescriptor.ArtifactResolutionServices.Values)
				{
					if (indexedProtocolEndpoint.ResponseLocation != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3249", new object[]
						{
							"ResponseLocation"
						})));
					}
					XmlQualifiedName element = new XmlQualifiedName("ArtifactResolutionService", "urn:oasis:names:tc:SAML:2.0:metadata");
					this.WriteIndexedProtocolEndpoint(writer, indexedProtocolEndpoint, element);
				}
			}
			if (singleSignOnDescriptor.SingleLogoutServices != null && singleSignOnDescriptor.SingleLogoutServices.Count > 0)
			{
				foreach (ProtocolEndpoint endpoint in singleSignOnDescriptor.SingleLogoutServices)
				{
					XmlQualifiedName element2 = new XmlQualifiedName("SingleLogoutService", "urn:oasis:names:tc:SAML:2.0:metadata");
					this.WriteProtocolEndpoint(writer, endpoint, element2);
				}
			}
			if (singleSignOnDescriptor.NameIdentifierFormats != null && singleSignOnDescriptor.NameIdentifierFormats.Count > 0)
			{
				foreach (Uri uri in singleSignOnDescriptor.NameIdentifierFormats)
				{
					if (!uri.IsAbsoluteUri)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID0014", new object[]
						{
							"NameIDFormat"
						})));
					}
					writer.WriteStartElement("NameIDFormat", "urn:oasis:names:tc:SAML:2.0:metadata");
					writer.WriteString(uri.AbsoluteUri);
					writer.WriteEndElement();
				}
			}
			this.WriteCustomElements<SingleSignOnDescriptor>(writer, singleSignOnDescriptor);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001E97C File Offset: 0x0001CB7C
		protected virtual void WriteWebServiceDescriptorAttributes(XmlWriter writer, WebServiceDescriptor wsDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (wsDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsDescriptor");
			}
			this.WriteRoleDescriptorAttributes(writer, wsDescriptor);
			if (!string.IsNullOrEmpty(wsDescriptor.ServiceDisplayName))
			{
				writer.WriteAttributeString("ServiceDisplayName", null, wsDescriptor.ServiceDisplayName);
			}
			if (!string.IsNullOrEmpty(wsDescriptor.ServiceDescription))
			{
				writer.WriteAttributeString("ServiceDescription", null, wsDescriptor.ServiceDescription);
			}
			this.WriteCustomAttributes<WebServiceDescriptor>(writer, wsDescriptor);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001EA00 File Offset: 0x0001CC00
		protected virtual void WriteWebServiceDescriptorElements(XmlWriter writer, WebServiceDescriptor wsDescriptor)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (wsDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsDescriptor");
			}
			if (wsDescriptor.TargetScopes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsDescriptor.TargetScopes");
			}
			if (wsDescriptor.ClaimTypesOffered == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsDescriptor.ClaimTypesOffered");
			}
			if (wsDescriptor.TokenTypesOffered == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsDescriptor.TokenTypesOffered");
			}
			this.WriteRoleDescriptorElements(writer, wsDescriptor);
			if (wsDescriptor.TokenTypesOffered.Count > 0)
			{
				writer.WriteStartElement("TokenTypesOffered", "http://docs.oasis-open.org/wsfed/federation/200706");
				foreach (Uri uri in wsDescriptor.TokenTypesOffered)
				{
					writer.WriteStartElement("TokenType", "http://docs.oasis-open.org/wsfed/federation/200706");
					if (!uri.IsAbsoluteUri)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataSerializationException(SR.GetString("ID3203", new object[]
						{
							"ClaimType"
						})));
					}
					writer.WriteAttributeString("Uri", uri.AbsoluteUri);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			if (wsDescriptor.ClaimTypesOffered.Count > 0)
			{
				writer.WriteStartElement("ClaimTypesOffered", "http://docs.oasis-open.org/wsfed/federation/200706");
				foreach (DisplayClaim claim in wsDescriptor.ClaimTypesOffered)
				{
					this.WriteDisplayClaim(writer, claim);
				}
				writer.WriteEndElement();
			}
			if (wsDescriptor.ClaimTypesRequested.Count > 0)
			{
				writer.WriteStartElement("ClaimTypesRequested", "http://docs.oasis-open.org/wsfed/federation/200706");
				foreach (DisplayClaim claim2 in wsDescriptor.ClaimTypesRequested)
				{
					this.WriteDisplayClaim(writer, claim2);
				}
				writer.WriteEndElement();
			}
			if (wsDescriptor.TargetScopes.Count > 0)
			{
				writer.WriteStartElement("TargetScopes", "http://docs.oasis-open.org/wsfed/federation/200706");
				foreach (EndpointReference endpointReference in wsDescriptor.TargetScopes)
				{
					endpointReference.WriteTo(writer);
				}
				writer.WriteEndElement();
			}
			this.WriteCustomElements<WebServiceDescriptor>(writer, wsDescriptor);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001EC74 File Offset: 0x0001CE74
		protected virtual Saml2Attribute ReadAttribute(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2Attribute result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AttributeType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("Name");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Name",
						"Attribute"
					}));
				}
				Saml2Attribute saml2Attribute = new Saml2Attribute(attribute);
				attribute = reader.GetAttribute("NameFormat");
				if (!string.IsNullOrEmpty(attribute))
				{
					if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
						{
							"Namespace",
							"Action"
						}));
					}
					saml2Attribute.NameFormat = new Uri(attribute);
				}
				saml2Attribute.FriendlyName = reader.GetAttribute("FriendlyName");
				reader.Read();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement("AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						bool isEmptyElement2 = reader.IsEmptyElement;
						bool flag = XmlUtil.IsNil(reader);
						XmlUtil.ValidateXsiType(reader, "string", "http://www.w3.org/2001/XMLSchema");
						if (flag)
						{
							reader.Read();
							if (!isEmptyElement2)
							{
								reader.ReadEndElement();
							}
							saml2Attribute.Values.Add(null);
						}
						else if (isEmptyElement2)
						{
							reader.Read();
							saml2Attribute.Values.Add("");
						}
						else
						{
							saml2Attribute.Values.Add(reader.ReadElementString());
						}
					}
					reader.ReadEndElement();
				}
				result = saml2Attribute;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = MetadataSerializer.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001EE4C File Offset: 0x0001D04C
		protected virtual void WriteAttribute(XmlWriter writer, Saml2Attribute data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("Name", data.Name);
			if (null != data.NameFormat)
			{
				writer.WriteAttributeString("NameFormat", data.NameFormat.AbsoluteUri);
			}
			if (data.FriendlyName != null)
			{
				writer.WriteAttributeString("FriendlyName", data.FriendlyName);
			}
			foreach (string text in data.Values)
			{
				writer.WriteStartElement("AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion");
				if (text == null)
				{
					writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", XmlConvert.ToString(true));
				}
				else if (text.Length > 0)
				{
					writer.WriteString(text);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001EF60 File Offset: 0x0001D160
		private static Exception TryWrapReadException(XmlReader reader, Exception inner)
		{
			if (inner is FormatException || inner is ArgumentException || inner is InvalidOperationException || inner is OverflowException)
			{
				return DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4125"), inner);
			}
			return null;
		}

		// Token: 0x04000A7E RID: 2686
		public const string LanguagePrefix = "xml";

		// Token: 0x04000A7F RID: 2687
		public const string LanguageLocalName = "lang";

		// Token: 0x04000A80 RID: 2688
		public const string LanguageAttribute = "xml:lang";

		// Token: 0x04000A81 RID: 2689
		public const string LanguageNamespaceUri = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000A82 RID: 2690
		private const int MaxEntitiesDescriptorDepth = 8;

		// Token: 0x04000A83 RID: 2691
		[ThreadStatic]
		private static int t_entitiesDescriptorDepth;

		// Token: 0x04000A84 RID: 2692
		private const string _uriReference = "_metadata";

		// Token: 0x04000A85 RID: 2693
		private List<string> _trustedIssuers = new List<string>();

		// Token: 0x04000A86 RID: 2694
		private SecurityTokenSerializer _tokenSerializer;
	}
}
