using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020004F8 RID: 1272
	internal class X509Utilities
	{
		// Token: 0x06002B87 RID: 11143 RVA: 0x00107BC0 File Offset: 0x00106BC0
		static X509Utilities()
		{
			X509Utilities.algorithms.Add("MD2WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			X509Utilities.algorithms.Add("MD2WITHRSA", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			X509Utilities.algorithms.Add("MD5WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			X509Utilities.algorithms.Add("MD5WITHRSA", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSA", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA224WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA224WITHRSA", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA256WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA256WITHRSA", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA384WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA384WITHRSA", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA512WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA512WITHRSA", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			X509Utilities.algorithms.Add("SHA1WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA224WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA256WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA384WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("SHA512WITHRSAANDMGF1", PkcsObjectIdentifiers.IdRsassaPss);
			X509Utilities.algorithms.Add("RIPEMD160WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			X509Utilities.algorithms.Add("RIPEMD160WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			X509Utilities.algorithms.Add("RIPEMD128WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			X509Utilities.algorithms.Add("RIPEMD128WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			X509Utilities.algorithms.Add("RIPEMD256WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			X509Utilities.algorithms.Add("RIPEMD256WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			X509Utilities.algorithms.Add("SHA1WITHDSA", X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.algorithms.Add("DSAWITHSHA1", X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.algorithms.Add("SHA224WITHDSA", NistObjectIdentifiers.DsaWithSha224);
			X509Utilities.algorithms.Add("SHA256WITHDSA", NistObjectIdentifiers.DsaWithSha256);
			X509Utilities.algorithms.Add("SHA1WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.algorithms.Add("ECDSAWITHSHA1", X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.algorithms.Add("SHA224WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha224);
			X509Utilities.algorithms.Add("SHA256WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha256);
			X509Utilities.algorithms.Add("SHA384WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha384);
			X509Utilities.algorithms.Add("SHA512WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha512);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410-94", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.algorithms.Add("GOST3411WITHECGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.algorithms.Add("GOST3411WITHECGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.algorithms.Add("GOST3411WITHGOST3410-2001", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			X509Utilities.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			X509Utilities.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
			X509Utilities.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			X509Utilities.noParams.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x2001);
			AlgorithmIdentifier hashAlgId = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			X509Utilities.exParams.Add("SHA1WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId, 20));
			AlgorithmIdentifier hashAlgId2 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha224, DerNull.Instance);
			X509Utilities.exParams.Add("SHA224WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId2, 28));
			AlgorithmIdentifier hashAlgId3 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256, DerNull.Instance);
			X509Utilities.exParams.Add("SHA256WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId3, 32));
			AlgorithmIdentifier hashAlgId4 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha384, DerNull.Instance);
			X509Utilities.exParams.Add("SHA384WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId4, 48));
			AlgorithmIdentifier hashAlgId5 = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha512, DerNull.Instance);
			X509Utilities.exParams.Add("SHA512WITHRSAANDMGF1", X509Utilities.CreatePssParams(hashAlgId5, 64));
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00108066 File Offset: 0x00107066
		private static RsassaPssParameters CreatePssParams(AlgorithmIdentifier hashAlgId, int saltSize)
		{
			return new RsassaPssParameters(hashAlgId, new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, hashAlgId), new DerInteger(saltSize), new DerInteger(1));
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00108085 File Offset: 0x00107085
		internal static DerObjectIdentifier GetAlgorithmOid(string algorithmName)
		{
			algorithmName = algorithmName.ToUpper(CultureInfo.InvariantCulture);
			if (X509Utilities.algorithms.ContainsKey(algorithmName))
			{
				return (DerObjectIdentifier)X509Utilities.algorithms[algorithmName];
			}
			return new DerObjectIdentifier(algorithmName);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x001080B8 File Offset: 0x001070B8
		internal static AlgorithmIdentifier GetSigAlgID(DerObjectIdentifier sigOid, string algorithmName)
		{
			if (X509Utilities.noParams.Contains(sigOid))
			{
				return new AlgorithmIdentifier(sigOid);
			}
			algorithmName = algorithmName.ToUpper(CultureInfo.InvariantCulture);
			if (X509Utilities.exParams.ContainsKey(algorithmName))
			{
				return new AlgorithmIdentifier(sigOid, (Asn1Encodable)X509Utilities.exParams[algorithmName]);
			}
			return new AlgorithmIdentifier(sigOid, DerNull.Instance);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00108115 File Offset: 0x00107115
		internal static IEnumerable GetAlgNames()
		{
			return new EnumerableProxy(X509Utilities.algorithms.Keys);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00108128 File Offset: 0x00107128
		internal static byte[] GetSignatureForObject(DerObjectIdentifier sigOid, string sigName, AsymmetricKeyParameter privateKey, SecureRandom random, Asn1Encodable ae)
		{
			if (sigOid == null)
			{
				throw new ArgumentNullException("sigOid");
			}
			ISigner signer = SignerUtilities.GetSigner(sigName);
			if (random != null)
			{
				signer.Init(true, new ParametersWithRandom(privateKey, random));
			}
			else
			{
				signer.Init(true, privateKey);
			}
			byte[] derEncoded = ae.GetDerEncoded();
			signer.BlockUpdate(derEncoded, 0, derEncoded.Length);
			return signer.GenerateSignature();
		}

		// Token: 0x04001E2B RID: 7723
		private static readonly Hashtable algorithms = new Hashtable();

		// Token: 0x04001E2C RID: 7724
		private static readonly Hashtable exParams = new Hashtable();

		// Token: 0x04001E2D RID: 7725
		private static readonly ISet noParams = new HashSet();
	}
}
