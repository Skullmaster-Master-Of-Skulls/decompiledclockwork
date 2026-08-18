using System;
using System.IdentityModel.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000BD RID: 189
	public class RsaEndpointIdentity : EndpointIdentity
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00012CD6 File Offset: 0x00010ED6
		public RsaEndpointIdentity(string publicKey)
		{
			if (publicKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("publicKey");
			}
			base.Initialize(Claim.CreateRsaClaim(RsaEndpointIdentity.ToRsa(publicKey)));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012D04 File Offset: 0x00010F04
		public RsaEndpointIdentity(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			RSA rsa = certificate.PublicKey.Key as RSA;
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PublicKeyNotRSA")));
			}
			base.Initialize(Claim.CreateRsaClaim(rsa));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012D64 File Offset: 0x00010F64
		public RsaEndpointIdentity(Claim identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			if (!identity.ClaimType.Equals(ClaimTypes.Rsa))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("UnrecognizedClaimTypeForIdentity", new object[]
				{
					identity.ClaimType,
					ClaimTypes.Rsa
				}));
			}
			base.Initialize(identity);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00012DD0 File Offset: 0x00010FD0
		internal RsaEndpointIdentity(XmlDictionaryReader reader)
		{
			reader.ReadStartElement(XD.XmlSignatureDictionary.RsaKeyValue, XD.XmlSignatureDictionary.Namespace);
			byte[] modulus = Convert.FromBase64String(reader.ReadElementString(XD.XmlSignatureDictionary.Modulus.Value, XD.XmlSignatureDictionary.Namespace.Value));
			byte[] exponent = Convert.FromBase64String(reader.ReadElementString(XD.XmlSignatureDictionary.Exponent.Value, XD.XmlSignatureDictionary.Namespace.Value));
			reader.ReadEndElement();
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(new RSAParameters
			{
				Exponent = exponent,
				Modulus = modulus
			});
			base.Initialize(Claim.CreateRsaClaim(rsacryptoServiceProvider));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00012E88 File Offset: 0x00011088
		internal override void WriteContentsTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace);
			writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.RsaKeyValue, XD.XmlSignatureDictionary.Namespace);
			RSA rsa = (RSA)base.IdentityClaim.Resource;
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.Modulus, XD.XmlSignatureDictionary.Namespace, Convert.ToBase64String(rsaparameters.Modulus));
			writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.Exponent, XD.XmlSignatureDictionary.Namespace, Convert.ToBase64String(rsaparameters.Exponent));
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012F88 File Offset: 0x00011188
		private static RSA ToRsa(string keyString)
		{
			if (keyString == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyString");
			}
			RSA rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(keyString);
			return rsa;
		}
	}
}
