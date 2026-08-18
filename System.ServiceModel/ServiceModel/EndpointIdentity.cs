using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000B9 RID: 185
	[__DynamicallyInvokable]
	public abstract class EndpointIdentity
	{
		// Token: 0x06000318 RID: 792 RVA: 0x00011F33 File Offset: 0x00010133
		[__DynamicallyInvokable]
		protected EndpointIdentity()
		{
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00011F3B File Offset: 0x0001013B
		protected void Initialize(Claim identityClaim)
		{
			if (identityClaim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identityClaim");
			}
			this.Initialize(identityClaim, null);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011F58 File Offset: 0x00010158
		protected void Initialize(Claim identityClaim, IEqualityComparer<Claim> claimComparer)
		{
			if (identityClaim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identityClaim");
			}
			this.identityClaim = identityClaim;
			this.claimComparer = claimComparer;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00011F7B File Offset: 0x0001017B
		public Claim IdentityClaim
		{
			get
			{
				if (this.identityClaim == null)
				{
					this.EnsureIdentityClaim();
				}
				return this.identityClaim;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00011F94 File Offset: 0x00010194
		public static EndpointIdentity CreateIdentity(Claim identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			if (identity.ClaimType.Equals(ClaimTypes.Dns))
			{
				return new DnsEndpointIdentity(identity);
			}
			if (identity.ClaimType.Equals(ClaimTypes.Spn))
			{
				return new SpnEndpointIdentity(identity);
			}
			if (identity.ClaimType.Equals(ClaimTypes.Upn))
			{
				return new UpnEndpointIdentity(identity);
			}
			if (identity.ClaimType.Equals(ClaimTypes.Rsa))
			{
				return new RsaEndpointIdentity(identity);
			}
			return new GeneralEndpointIdentity(identity);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001201E File Offset: 0x0001021E
		public static EndpointIdentity CreateDnsIdentity(string dnsName)
		{
			return new DnsEndpointIdentity(dnsName);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012026 File Offset: 0x00010226
		public static EndpointIdentity CreateSpnIdentity(string spnName)
		{
			return new SpnEndpointIdentity(spnName);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001202E File Offset: 0x0001022E
		public static EndpointIdentity CreateUpnIdentity(string upnName)
		{
			return new UpnEndpointIdentity(upnName);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00012036 File Offset: 0x00010236
		public static EndpointIdentity CreateRsaIdentity(string publicKey)
		{
			return new RsaEndpointIdentity(publicKey);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001203E File Offset: 0x0001023E
		public static EndpointIdentity CreateRsaIdentity(X509Certificate2 certificate)
		{
			return new RsaEndpointIdentity(certificate);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00012046 File Offset: 0x00010246
		public static EndpointIdentity CreateX509CertificateIdentity(X509Certificate2 certificate)
		{
			return new X509CertificateEndpointIdentity(certificate);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001204E File Offset: 0x0001024E
		public static EndpointIdentity CreateX509CertificateIdentity(X509Certificate2 primaryCertificate, X509Certificate2Collection supportingCertificates)
		{
			return new X509CertificateEndpointIdentity(primaryCertificate, supportingCertificates);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00012058 File Offset: 0x00010258
		internal static EndpointIdentity CreateX509CertificateIdentity(X509Chain certificateChain)
		{
			if (certificateChain == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificateChain");
			}
			if (certificateChain.ChainElements.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("X509ChainIsEmpty"));
			}
			X509Certificate2 certificate = certificateChain.ChainElements[0].Certificate;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			for (int i = 1; i < certificateChain.ChainElements.Count; i++)
			{
				x509Certificate2Collection.Add(certificateChain.ChainElements[i].Certificate);
			}
			return new X509CertificateEndpointIdentity(certificate, x509Certificate2Collection);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000120E7 File Offset: 0x000102E7
		internal virtual void EnsureIdentityClaim()
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000120EC File Offset: 0x000102EC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			EndpointIdentity endpointIdentity = obj as EndpointIdentity;
			return endpointIdentity != null && this.Matches(endpointIdentity.IdentityClaim);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001211C File Offset: 0x0001031C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.GetClaimComparer().GetHashCode(this.IdentityClaim);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001212F File Offset: 0x0001032F
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string str = "identity(";
			Claim claim = this.IdentityClaim;
			return str + ((claim != null) ? claim.ToString() : null) + ")";
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00012152 File Offset: 0x00010352
		internal bool Matches(Claim claim)
		{
			return this.GetClaimComparer().Equals(this.IdentityClaim, claim);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00012166 File Offset: 0x00010366
		private IEqualityComparer<Claim> GetClaimComparer()
		{
			if (this.claimComparer == null)
			{
				this.claimComparer = Claim.DefaultComparer;
			}
			return this.claimComparer;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012184 File Offset: 0x00010384
		internal static EndpointIdentity ReadIdentity(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedEmptyElementExpectingClaim", new object[]
				{
					XD.AddressingDictionary.Identity.Value,
					XD.AddressingDictionary.IdentityExtensionNamespace.Value
				})));
			}
			reader.ReadStartElement(XD.AddressingDictionary.Identity, XD.AddressingDictionary.IdentityExtensionNamespace);
			EndpointIdentity result;
			if (reader.IsStartElement(XD.AddressingDictionary.Spn, XD.AddressingDictionary.IdentityExtensionNamespace))
			{
				result = new SpnEndpointIdentity(reader.ReadElementString());
			}
			else if (reader.IsStartElement(XD.AddressingDictionary.Upn, XD.AddressingDictionary.IdentityExtensionNamespace))
			{
				result = new UpnEndpointIdentity(reader.ReadElementString());
			}
			else if (reader.IsStartElement(XD.AddressingDictionary.Dns, XD.AddressingDictionary.IdentityExtensionNamespace))
			{
				result = new DnsEndpointIdentity(reader.ReadElementString());
			}
			else if (reader.IsStartElement(XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace))
			{
				reader.ReadStartElement();
				if (reader.IsStartElement(XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace))
				{
					result = new X509CertificateEndpointIdentity(reader);
				}
				else
				{
					if (!reader.IsStartElement(XD.XmlSignatureDictionary.RsaKeyValue, XD.XmlSignatureDictionary.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnrecognizedIdentityType", new object[]
						{
							reader.Name,
							reader.NamespaceURI
						})));
					}
					result = new RsaEndpointIdentity(reader);
				}
				reader.ReadEndElement();
			}
			else
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnrecognizedIdentityType", new object[]
					{
						reader.Name,
						reader.NamespaceURI
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidIdentityElement")));
			}
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001239F File Offset: 0x0001059F
		internal void WriteTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteStartElement(XD.AddressingDictionary.Identity, XD.AddressingDictionary.IdentityExtensionNamespace);
			this.WriteContentsTo(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000123DB File Offset: 0x000105DB
		internal virtual void WriteContentsTo(XmlDictionaryWriter writer)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnrecognizedIdentityPropertyType", new object[]
			{
				this.IdentityClaim.GetType().ToString()
			})));
		}

		// Token: 0x04000965 RID: 2405
		internal const StoreLocation defaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04000966 RID: 2406
		internal const StoreName defaultStoreName = StoreName.My;

		// Token: 0x04000967 RID: 2407
		internal const X509FindType defaultX509FindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04000968 RID: 2408
		private Claim identityClaim;

		// Token: 0x04000969 RID: 2409
		private IEqualityComparer<Claim> claimComparer;
	}
}
