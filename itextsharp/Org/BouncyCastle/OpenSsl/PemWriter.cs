using System;
using System.Globalization;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x020001E1 RID: 481
	public class PemWriter
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0006C3E2 File Offset: 0x0006B3E2
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0006C3EA File Offset: 0x0006B3EA
		public PemWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0006C408 File Offset: 0x0006B408
		public void WriteObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string type;
			byte[] data;
			if (obj is X509Certificate)
			{
				type = "CERTIFICATE";
				try
				{
					data = ((X509Certificate)obj).GetEncoded();
					goto IL_153;
				}
				catch (CertificateEncodingException ex)
				{
					throw new IOException("Cannot Encode object: " + ex.ToString());
				}
			}
			if (obj is X509Crl)
			{
				type = "X509 CRL";
				try
				{
					data = ((X509Crl)obj).GetEncoded();
					goto IL_153;
				}
				catch (CrlException ex2)
				{
					throw new IOException("Cannot Encode object: " + ex2.ToString());
				}
			}
			if (obj is AsymmetricCipherKeyPair)
			{
				this.WriteObject(((AsymmetricCipherKeyPair)obj).Private);
				return;
			}
			if (obj is AsymmetricKeyParameter)
			{
				AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)obj;
				if (asymmetricKeyParameter.IsPrivate)
				{
					string str;
					data = this.EncodePrivateKey(asymmetricKeyParameter, out str);
					type = str + " PRIVATE KEY";
				}
				else
				{
					type = "PUBLIC KEY";
					data = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(asymmetricKeyParameter).GetDerEncoded();
				}
			}
			else if (obj is IX509AttributeCertificate)
			{
				type = "ATTRIBUTE CERTIFICATE";
				data = ((X509V2AttributeCertificate)obj).GetEncoded();
			}
			else if (obj is Pkcs10CertificationRequest)
			{
				type = "CERTIFICATE REQUEST";
				data = ((Pkcs10CertificationRequest)obj).GetEncoded();
			}
			else
			{
				if (!(obj is Org.BouncyCastle.Asn1.Cms.ContentInfo))
				{
					throw new ArgumentException("Object type not supported: " + obj.GetType().FullName, "obj");
				}
				type = "PKCS7";
				data = ((Org.BouncyCastle.Asn1.Cms.ContentInfo)obj).GetEncoded();
			}
			IL_153:
			this.WritePemBlock(type, data, new string[0]);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0006C594 File Offset: 0x0006B594
		public void WriteObject(object obj, string algorithm, char[] password, SecureRandom random)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			if (obj is AsymmetricCipherKeyPair)
			{
				this.WriteObject(((AsymmetricCipherKeyPair)obj).Private, algorithm, password, random);
				return;
			}
			string text = null;
			byte[] array = null;
			if (obj is AsymmetricKeyParameter)
			{
				AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)obj;
				if (asymmetricKeyParameter.IsPrivate)
				{
					string str;
					array = this.EncodePrivateKey(asymmetricKeyParameter, out str);
					text = str + " PRIVATE KEY";
				}
			}
			if (text == null || array == null)
			{
				throw new ArgumentException("Object type not supported: " + obj.GetType().FullName, "obj");
			}
			string text2 = algorithm.ToUpper(CultureInfo.InvariantCulture);
			if (text2 == "DESEDE")
			{
				text2 = "DES-EDE3-CBC";
			}
			int num = text2.StartsWith("AES-") ? 16 : 8;
			byte[] array2 = new byte[num];
			random.NextBytes(array2);
			byte[] data = PemUtilities.Crypt(true, array, password, text2, array2);
			this.WritePemBlock(text, data, new string[]
			{
				"Proc-Type: 4,ENCRYPTED",
				"DEK-Info: " + text2 + "," + Hex.ToHexString(array2)
			});
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0006C6E0 File Offset: 0x0006B6E0
		private byte[] EncodePrivateKey(AsymmetricKeyParameter akp, out string keyType)
		{
			PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(akp);
			DerObjectIdentifier objectID = privateKeyInfo.AlgorithmID.ObjectID;
			if (objectID.Equals(X9ObjectIdentifiers.IdDsa))
			{
				keyType = "DSA";
				DsaParameter instance = DsaParameter.GetInstance(privateKeyInfo.AlgorithmID.Parameters);
				BigInteger x = ((DsaPrivateKeyParameters)akp).X;
				BigInteger value = instance.G.ModPow(x, instance.P);
				return new DerSequence(new Asn1Encodable[]
				{
					new DerInteger(0),
					new DerInteger(instance.P),
					new DerInteger(instance.Q),
					new DerInteger(instance.G),
					new DerInteger(value),
					new DerInteger(x)
				}).GetEncoded();
			}
			if (objectID.Equals(PkcsObjectIdentifiers.RsaEncryption))
			{
				keyType = "RSA";
			}
			else
			{
				if (!objectID.Equals(CryptoProObjectIdentifiers.GostR3410x2001) && !objectID.Equals(X9ObjectIdentifiers.IdECPublicKey))
				{
					throw new ArgumentException("Cannot handle private key of type: " + akp.GetType().FullName, "akp");
				}
				keyType = "EC";
			}
			return privateKeyInfo.PrivateKey.GetEncoded();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0006C810 File Offset: 0x0006B810
		private void WritePemBlock(string type, byte[] data, params string[] fields)
		{
			this.WriteHeader(type);
			if (fields.Length > 0)
			{
				foreach (string value in fields)
				{
					this.writer.WriteLine(value);
				}
				this.writer.WriteLine();
			}
			this.WriteBytes(Base64.Encode(data));
			this.WriteFooter(type);
			this.writer.Flush();
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0006C873 File Offset: 0x0006B873
		private void WriteHeader(string type)
		{
			this.writer.WriteLine("-----BEGIN " + type + "-----");
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0006C890 File Offset: 0x0006B890
		private void WriteFooter(string type)
		{
			this.writer.WriteLine("-----END " + type + "-----");
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0006C8B0 File Offset: 0x0006B8B0
		private void WriteBytes(byte[] bytes)
		{
			int num = 0;
			int i = bytes.Length;
			char[] array = new char[64];
			while (i > 64)
			{
				Encoding.ASCII.GetChars(bytes, num, 64, array, 0);
				this.writer.WriteLine(array);
				num += 64;
				i -= 64;
			}
			Encoding.ASCII.GetChars(bytes, num, i, array, 0);
			this.writer.WriteLine(array, 0, i);
		}

		// Token: 0x04000D50 RID: 3408
		private const int LineLength = 64;

		// Token: 0x04000D51 RID: 3409
		private readonly TextWriter writer;
	}
}
