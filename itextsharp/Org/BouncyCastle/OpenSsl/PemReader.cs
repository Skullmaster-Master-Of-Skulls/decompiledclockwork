using System;
using System.Collections;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x020001E2 RID: 482
	public class PemReader
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0006C917 File Offset: 0x0006B917
		public TextReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0006C91F File Offset: 0x0006B91F
		public PemReader(TextReader reader) : this(reader, null)
		{
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0006C929 File Offset: 0x0006B929
		public PemReader(TextReader reader, IPasswordFinder pFinder)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
			this.pFinder = pFinder;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0006C950 File Offset: 0x0006B950
		public object ReadObject()
		{
			string text;
			while ((text = this.reader.ReadLine()) != null)
			{
				int num = text.IndexOf("-----BEGIN ");
				if (num != -1)
				{
					num += "-----BEGIN ".Length;
					int num2 = text.IndexOf('-', num);
					if (num2 == -1)
					{
						num2 = text.Length;
					}
					string text2 = text.Substring(num, num2 - num).Trim();
					string endMarker = "-----END " + text2;
					if (text2.EndsWith(" PRIVATE KEY"))
					{
						string type = text2.Substring(0, text2.Length - " PRIVATE KEY".Length);
						return this.ReadKeyPair(type, endMarker);
					}
					string key;
					switch (key = text2)
					{
					case "PUBLIC KEY":
						return this.ReadPublicKey(endMarker);
					case "RSA PUBLIC KEY":
						return this.ReadRsaPublicKey(endMarker);
					case "CERTIFICATE REQUEST":
					case "NEW CERTIFICATE REQUEST":
						return this.ReadCertificateRequest(endMarker);
					case "CERTIFICATE":
					case "X509 CERTIFICATE":
						return this.ReadCertificate(endMarker);
					case "PKCS7":
						return this.ReadPkcs7(endMarker);
					case "X509 CRL":
						return this.ReadCrl(endMarker);
					case "ATTRIBUTE CERTIFICATE":
						return this.ReadAttributeCertificate(endMarker);
					}
					this.ReadBytes(endMarker);
				}
			}
			return null;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0006CB0C File Offset: 0x0006BB0C
		private byte[] ReadBytes(string endMarker)
		{
			return this.ReadBytesAndFields(endMarker, null);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0006CB18 File Offset: 0x0006BB18
		private byte[] ReadBytesAndFields(string endMarker, IDictionary fields)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			while ((text = this.reader.ReadLine()) != null && text.IndexOf(endMarker) == -1)
			{
				int num = text.IndexOf(':');
				if (num == -1)
				{
					stringBuilder.Append(text.Trim());
				}
				else if (fields != null)
				{
					string text2 = text.Substring(0, num).Trim();
					if (text2.StartsWith("X-"))
					{
						text2 = text2.Substring(2);
					}
					string value = text.Substring(num + 1).Trim();
					fields[text2] = value;
				}
			}
			if (text == null)
			{
				throw new IOException(endMarker + " not found");
			}
			if (stringBuilder.Length % 4 != 0)
			{
				throw new IOException("base64 data appears to be truncated");
			}
			return Base64.Decode(stringBuilder.ToString());
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0006CBD8 File Offset: 0x0006BBD8
		private AsymmetricKeyParameter ReadRsaPublicKey(string endMarker)
		{
			RsaPublicKeyStructure instance = RsaPublicKeyStructure.GetInstance(Asn1Object.FromByteArray(this.ReadBytes(endMarker)));
			return new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0006CC09 File Offset: 0x0006BC09
		private AsymmetricKeyParameter ReadPublicKey(string endMarker)
		{
			return PublicKeyFactory.CreateKey(this.ReadBytes(endMarker));
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0006CC18 File Offset: 0x0006BC18
		private X509Certificate ReadCertificate(string endMarker)
		{
			byte[] input = this.ReadBytes(endMarker);
			X509Certificate result;
			try
			{
				result = new X509CertificateParser().ReadCertificate(input);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0006CC64 File Offset: 0x0006BC64
		private X509Crl ReadCrl(string endMarker)
		{
			byte[] input = this.ReadBytes(endMarker);
			X509Crl result;
			try
			{
				result = new X509CrlParser().ReadCrl(input);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0006CCB0 File Offset: 0x0006BCB0
		private Pkcs10CertificationRequest ReadCertificateRequest(string endMarker)
		{
			byte[] encoded = this.ReadBytes(endMarker);
			Pkcs10CertificationRequest result;
			try
			{
				result = new Pkcs10CertificationRequest(encoded);
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing cert: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006CCF8 File Offset: 0x0006BCF8
		private IX509AttributeCertificate ReadAttributeCertificate(string endMarker)
		{
			byte[] encoded = this.ReadBytes(endMarker);
			return new X509V2AttributeCertificate(encoded);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0006CD14 File Offset: 0x0006BD14
		private Org.BouncyCastle.Asn1.Cms.ContentInfo ReadPkcs7(string endMarker)
		{
			byte[] data = this.ReadBytes(endMarker);
			Org.BouncyCastle.Asn1.Cms.ContentInfo instance;
			try
			{
				instance = Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(Asn1Object.FromByteArray(data));
			}
			catch (Exception ex)
			{
				throw new PemException("problem parsing PKCS7 object: " + ex.ToString());
			}
			return instance;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0006CD60 File Offset: 0x0006BD60
		private AsymmetricCipherKeyPair ReadKeyPair(string type, string endMarker)
		{
			IDictionary dictionary = new Hashtable();
			byte[] array = this.ReadBytesAndFields(endMarker, dictionary);
			string a = (string)dictionary["Proc-Type"];
			if (a == "4,ENCRYPTED")
			{
				if (this.pFinder == null)
				{
					throw new PasswordException("No password finder specified, but a password is required");
				}
				char[] password = this.pFinder.GetPassword();
				if (password == null)
				{
					throw new PasswordException("Password is null, but a password is required");
				}
				string text = (string)dictionary["DEK-Info"];
				string[] array2 = text.Split(new char[]
				{
					','
				});
				string dekAlgName = array2[0].Trim();
				byte[] iv = Hex.Decode(array2[1].Trim());
				array = PemUtilities.Crypt(false, array, password, dekAlgName, iv);
			}
			try
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(array);
				if (type != null)
				{
					AsymmetricKeyParameter asymmetricKeyParameter;
					AsymmetricKeyParameter publicParameter;
					if (!(type == "RSA"))
					{
						if (!(type == "DSA"))
						{
							if (!(type == "EC"))
							{
								goto IL_254;
							}
							ECPrivateKeyStructure ecprivateKeyStructure = new ECPrivateKeyStructure(asn1Sequence);
							AlgorithmIdentifier algID = new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, ecprivateKeyStructure.GetParameters());
							PrivateKeyInfo keyInfo = new PrivateKeyInfo(algID, ecprivateKeyStructure.ToAsn1Object());
							asymmetricKeyParameter = PrivateKeyFactory.CreateKey(keyInfo);
							DerBitString publicKey = ecprivateKeyStructure.GetPublicKey();
							if (publicKey != null)
							{
								SubjectPublicKeyInfo keyInfo2 = new SubjectPublicKeyInfo(algID, publicKey.GetBytes());
								publicParameter = PublicKeyFactory.CreateKey(keyInfo2);
							}
							else
							{
								publicParameter = ECKeyPairGenerator.GetCorrespondingPublicKey((ECPrivateKeyParameters)asymmetricKeyParameter);
							}
						}
						else
						{
							DerInteger derInteger = (DerInteger)asn1Sequence[1];
							DerInteger derInteger2 = (DerInteger)asn1Sequence[2];
							DerInteger derInteger3 = (DerInteger)asn1Sequence[3];
							DerInteger derInteger4 = (DerInteger)asn1Sequence[4];
							DerInteger derInteger5 = (DerInteger)asn1Sequence[5];
							DsaParameters parameters = new DsaParameters(derInteger.Value, derInteger2.Value, derInteger3.Value);
							asymmetricKeyParameter = new DsaPrivateKeyParameters(derInteger5.Value, parameters);
							publicParameter = new DsaPublicKeyParameters(derInteger4.Value, parameters);
						}
					}
					else
					{
						RsaPrivateKeyStructure rsaPrivateKeyStructure = new RsaPrivateKeyStructure(asn1Sequence);
						publicParameter = new RsaKeyParameters(false, rsaPrivateKeyStructure.Modulus, rsaPrivateKeyStructure.PublicExponent);
						asymmetricKeyParameter = new RsaPrivateCrtKeyParameters(rsaPrivateKeyStructure.Modulus, rsaPrivateKeyStructure.PublicExponent, rsaPrivateKeyStructure.PrivateExponent, rsaPrivateKeyStructure.Prime1, rsaPrivateKeyStructure.Prime2, rsaPrivateKeyStructure.Exponent1, rsaPrivateKeyStructure.Exponent2, rsaPrivateKeyStructure.Coefficient);
					}
					return new AsymmetricCipherKeyPair(publicParameter, asymmetricKeyParameter);
				}
				IL_254:
				throw new ArgumentException("Unknown key type: " + type, "type");
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new PemException("problem creating " + type + " private key: " + ex2.ToString());
			}
			AsymmetricCipherKeyPair result;
			return result;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006D040 File Offset: 0x0006C040
		private X9ECParameters ReadECParameters(string endMarker)
		{
			byte[] data = this.ReadBytes(endMarker);
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)Asn1Object.FromByteArray(data);
			return PemReader.GetCurveParameters(derObjectIdentifier.Id);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006D06C File Offset: 0x0006C06C
		private static X9ECParameters GetCurveParameters(string name)
		{
			X9ECParameters byName = X962NamedCurves.GetByName(name);
			if (byName == null)
			{
				byName = SecNamedCurves.GetByName(name);
				if (byName == null)
				{
					byName = NistNamedCurves.GetByName(name);
					if (byName == null)
					{
						byName = TeleTrusTNamedCurves.GetByName(name);
						if (byName == null)
						{
							throw new Exception("unknown curve name: " + name);
						}
					}
				}
			}
			return byName;
		}

		// Token: 0x04000D52 RID: 3410
		private const string BeginString = "-----BEGIN ";

		// Token: 0x04000D53 RID: 3411
		private readonly TextReader reader;

		// Token: 0x04000D54 RID: 3412
		private readonly IPasswordFinder pFinder;
	}
}
