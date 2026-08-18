using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200011B RID: 283
	public class RsaDigestSigner : ISigner
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x00037A20 File Offset: 0x00036A20
		static RsaDigestSigner()
		{
			RsaDigestSigner.oidMap["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			RsaDigestSigner.oidMap["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			RsaDigestSigner.oidMap["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			RsaDigestSigner.oidMap["SHA-1"] = X509ObjectIdentifiers.IdSha1;
			RsaDigestSigner.oidMap["SHA-224"] = NistObjectIdentifiers.IdSha224;
			RsaDigestSigner.oidMap["SHA-256"] = NistObjectIdentifiers.IdSha256;
			RsaDigestSigner.oidMap["SHA-384"] = NistObjectIdentifiers.IdSha384;
			RsaDigestSigner.oidMap["SHA-512"] = NistObjectIdentifiers.IdSha512;
			RsaDigestSigner.oidMap["MD2"] = PkcsObjectIdentifiers.MD2;
			RsaDigestSigner.oidMap["MD4"] = PkcsObjectIdentifiers.MD4;
			RsaDigestSigner.oidMap["MD5"] = PkcsObjectIdentifiers.MD5;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00037B14 File Offset: 0x00036B14
		public RsaDigestSigner(IDigest digest)
		{
			this.digest = digest;
			string algorithmName = digest.AlgorithmName;
			if (algorithmName.Equals("NULL"))
			{
				this.algId = null;
				return;
			}
			this.algId = new AlgorithmIdentifier((DerObjectIdentifier)RsaDigestSigner.oidMap[digest.AlgorithmName], DerNull.Instance);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00037B7F File Offset: 0x00036B7F
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withRSA";
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00037B98 File Offset: 0x00036B98
		public void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.rsaEngine.Init(forSigning, parameters);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00037C0D File Offset: 0x00036C0D
		public void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00037C1B File Offset: 0x00036C1B
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00037C2C File Offset: 0x00036C2C
		public byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2 = this.DerEncode(array);
			return this.rsaEngine.ProcessBlock(array2, 0, array2.Length);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00037C84 File Offset: 0x00036C84
		public bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2;
			byte[] array3;
			try
			{
				array2 = this.rsaEngine.ProcessBlock(signature, 0, signature.Length);
				array3 = this.DerEncode(array);
			}
			catch (Exception)
			{
				return false;
			}
			if (array2.Length == array3.Length)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != array3[i])
					{
						return false;
					}
				}
			}
			else
			{
				if (array2.Length != array3.Length - 2)
				{
					return false;
				}
				int num = array2.Length - array.Length - 2;
				int num2 = array3.Length - array.Length - 2;
				byte[] array4 = array3;
				int num3 = 1;
				array4[num3] -= 2;
				byte[] array5 = array3;
				int num4 = 3;
				array5[num4] -= 2;
				for (int j = 0; j < array.Length; j++)
				{
					if (array2[num + j] != array3[num2 + j])
					{
						return false;
					}
				}
				for (int k = 0; k < num; k++)
				{
					if (array2[k] != array3[k])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00037DB4 File Offset: 0x00036DB4
		public void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00037DC4 File Offset: 0x00036DC4
		private byte[] DerEncode(byte[] hash)
		{
			if (this.algId == null)
			{
				return hash;
			}
			DigestInfo digestInfo = new DigestInfo(this.algId, hash);
			return digestInfo.GetDerEncoded();
		}

		// Token: 0x04000873 RID: 2163
		private readonly IAsymmetricBlockCipher rsaEngine = new Pkcs1Encoding(new RsaBlindedEngine());

		// Token: 0x04000874 RID: 2164
		private readonly AlgorithmIdentifier algId;

		// Token: 0x04000875 RID: 2165
		private readonly IDigest digest;

		// Token: 0x04000876 RID: 2166
		private bool forSigning;

		// Token: 0x04000877 RID: 2167
		private static readonly Hashtable oidMap = new Hashtable();
	}
}
