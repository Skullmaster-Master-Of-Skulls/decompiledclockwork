using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000181 RID: 385
	internal class OcspUtilities
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00056D80 File Offset: 0x00055D80
		static OcspUtilities()
		{
			OcspUtilities.algorithms.Add("MD2WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			OcspUtilities.algorithms.Add("MD2WITHRSA", PkcsObjectIdentifiers.MD2WithRsaEncryption);
			OcspUtilities.algorithms.Add("MD5WITHRSAENCRYPTION", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			OcspUtilities.algorithms.Add("MD5WITHRSA", PkcsObjectIdentifiers.MD5WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA1WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA1WITHRSA", PkcsObjectIdentifiers.Sha1WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA224WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA224WITHRSA", PkcsObjectIdentifiers.Sha224WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA256WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA256WITHRSA", PkcsObjectIdentifiers.Sha256WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA384WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA384WITHRSA", PkcsObjectIdentifiers.Sha384WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA512WITHRSAENCRYPTION", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			OcspUtilities.algorithms.Add("SHA512WITHRSA", PkcsObjectIdentifiers.Sha512WithRsaEncryption);
			OcspUtilities.algorithms.Add("RIPEMD160WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			OcspUtilities.algorithms.Add("RIPEMD160WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160);
			OcspUtilities.algorithms.Add("RIPEMD128WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			OcspUtilities.algorithms.Add("RIPEMD128WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128);
			OcspUtilities.algorithms.Add("RIPEMD256WITHRSAENCRYPTION", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			OcspUtilities.algorithms.Add("RIPEMD256WITHRSA", TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256);
			OcspUtilities.algorithms.Add("SHA1WITHDSA", X9ObjectIdentifiers.IdDsaWithSha1);
			OcspUtilities.algorithms.Add("DSAWITHSHA1", X9ObjectIdentifiers.IdDsaWithSha1);
			OcspUtilities.algorithms.Add("SHA224WITHDSA", NistObjectIdentifiers.DsaWithSha224);
			OcspUtilities.algorithms.Add("SHA256WITHDSA", NistObjectIdentifiers.DsaWithSha256);
			OcspUtilities.algorithms.Add("SHA1WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha1);
			OcspUtilities.algorithms.Add("ECDSAWITHSHA1", X9ObjectIdentifiers.ECDsaWithSha1);
			OcspUtilities.algorithms.Add("SHA224WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha224);
			OcspUtilities.algorithms.Add("SHA256WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha256);
			OcspUtilities.algorithms.Add("SHA384WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha384);
			OcspUtilities.algorithms.Add("SHA512WITHECDSA", X9ObjectIdentifiers.ECDsaWithSha512);
			OcspUtilities.algorithms.Add("GOST3411WITHGOST3410", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			OcspUtilities.algorithms.Add("GOST3411WITHGOST3410-94", CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94);
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.MD2WithRsaEncryption, "MD2WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.MD5WithRsaEncryption, "MD5WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption, "SHA1WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption, "SHA224WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption, "SHA256WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption, "SHA384WITHRSA");
			OcspUtilities.oids.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption, "SHA512WITHRSA");
			OcspUtilities.oids.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD160, "RIPEMD160WITHRSA");
			OcspUtilities.oids.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD128, "RIPEMD128WITHRSA");
			OcspUtilities.oids.Add(TeleTrusTObjectIdentifiers.RsaSignatureWithRipeMD256, "RIPEMD256WITHRSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.IdDsaWithSha1, "SHA1WITHDSA");
			OcspUtilities.oids.Add(NistObjectIdentifiers.DsaWithSha224, "SHA224WITHDSA");
			OcspUtilities.oids.Add(NistObjectIdentifiers.DsaWithSha256, "SHA256WITHDSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.ECDsaWithSha1, "SHA1WITHECDSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.ECDsaWithSha224, "SHA224WITHECDSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.ECDsaWithSha256, "SHA256WITHECDSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.ECDsaWithSha384, "SHA384WITHECDSA");
			OcspUtilities.oids.Add(X9ObjectIdentifiers.ECDsaWithSha512, "SHA512WITHECDSA");
			OcspUtilities.oids.Add(CryptoProObjectIdentifiers.GostR3411x94WithGostR3410x94, "GOST3411WITHGOST3410");
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha1);
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha224);
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha256);
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha384);
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.ECDsaWithSha512);
			OcspUtilities.noParams.Add(X9ObjectIdentifiers.IdDsaWithSha1);
			OcspUtilities.noParams.Add(NistObjectIdentifiers.DsaWithSha224);
			OcspUtilities.noParams.Add(NistObjectIdentifiers.DsaWithSha256);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0005721F File Offset: 0x0005621F
		internal static DerObjectIdentifier GetAlgorithmOid(string algorithmName)
		{
			algorithmName = algorithmName.ToUpper(CultureInfo.InvariantCulture);
			if (OcspUtilities.algorithms.ContainsKey(algorithmName))
			{
				return (DerObjectIdentifier)OcspUtilities.algorithms[algorithmName];
			}
			return new DerObjectIdentifier(algorithmName);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00057252 File Offset: 0x00056252
		internal static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			if (OcspUtilities.oids.ContainsKey(oid))
			{
				return (string)OcspUtilities.oids[oid];
			}
			return oid.Id;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00057278 File Offset: 0x00056278
		internal static AlgorithmIdentifier GetSigAlgID(DerObjectIdentifier sigOid)
		{
			if (OcspUtilities.noParams.Contains(sigOid))
			{
				return new AlgorithmIdentifier(sigOid);
			}
			return new AlgorithmIdentifier(sigOid, DerNull.Instance);
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00057299 File Offset: 0x00056299
		internal static IEnumerable AlgNames
		{
			get
			{
				return new EnumerableProxy(OcspUtilities.algorithms.Keys);
			}
		}

		// Token: 0x04000B08 RID: 2824
		private static readonly Hashtable algorithms = new Hashtable();

		// Token: 0x04000B09 RID: 2825
		private static readonly Hashtable oids = new Hashtable();

		// Token: 0x04000B0A RID: 2826
		private static readonly ISet noParams = new HashSet();
	}
}
