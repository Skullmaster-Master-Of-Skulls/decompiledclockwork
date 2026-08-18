using System;
using System.IdentityModel.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000BE RID: 190
	public class X509CertificateEndpointIdentity : EndpointIdentity
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00012FB8 File Offset: 0x000111B8
		public X509CertificateEndpointIdentity(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			base.Initialize(new Claim(ClaimTypes.Thumbprint, certificate.GetCertHash(), Rights.PossessProperty));
			this.certificateCollection.Add(certificate);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00013014 File Offset: 0x00011214
		public X509CertificateEndpointIdentity(X509Certificate2 primaryCertificate, X509Certificate2Collection supportingCertificates)
		{
			if (primaryCertificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("primaryCertificate");
			}
			if (supportingCertificates == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("supportingCertificates");
			}
			base.Initialize(new Claim(ClaimTypes.Thumbprint, primaryCertificate.GetCertHash(), Rights.PossessProperty));
			this.certificateCollection.Add(primaryCertificate);
			for (int i = 0; i < supportingCertificates.Count; i++)
			{
				this.certificateCollection.Add(supportingCertificates[i]);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000130A4 File Offset: 0x000112A4
		internal X509CertificateEndpointIdentity(XmlDictionaryReader reader)
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
					XD.AddressingDictionary.X509v3Certificate.Value,
					XD.AddressingDictionary.IdentityExtensionNamespace.Value
				})));
			}
			reader.ReadStartElement(XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
			while (reader.IsStartElement(XD.XmlSignatureDictionary.X509Certificate, XD.XmlSignatureDictionary.Namespace))
			{
				byte[] rawData = Convert.FromBase64String(reader.ReadElementString());
				X509Helper.VerifyNotPfx(rawData);
				X509Certificate2 x509Certificate = new X509Certificate2(rawData);
				if (this.certificateCollection.Count == 0)
				{
					base.Initialize(new Claim(ClaimTypes.Thumbprint, x509Certificate.GetCertHash(), Rights.PossessProperty));
				}
				this.certificateCollection.Add(x509Certificate);
			}
			reader.ReadEndElement();
			if (this.certificateCollection.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedEmptyElementExpectingClaim", new object[]
				{
					XD.AddressingDictionary.X509v3Certificate.Value,
					XD.AddressingDictionary.IdentityExtensionNamespace.Value
				})));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00013206 File Offset: 0x00011406
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this.certificateCollection;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00013210 File Offset: 0x00011410
		internal override void WriteContentsTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace);
			writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
			for (int i = 0; i < this.certificateCollection.Count; i++)
			{
				writer.WriteElementString(XD.XmlSignatureDictionary.X509Certificate, XD.XmlSignatureDictionary.Namespace, Convert.ToBase64String(this.certificateCollection[i].RawData));
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x04000974 RID: 2420
		private X509Certificate2Collection certificateCollection = new X509Certificate2Collection();
	}
}
