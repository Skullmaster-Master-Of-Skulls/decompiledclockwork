using System;
using System.Collections;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x02000428 RID: 1064
	public abstract class TspAlgorithms
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x000DC888 File Offset: 0x000DB888
		static TspAlgorithms()
		{
			string[] array = new string[]
			{
				TspAlgorithms.Gost3411,
				TspAlgorithms.MD5,
				TspAlgorithms.Sha1,
				TspAlgorithms.Sha224,
				TspAlgorithms.Sha256,
				TspAlgorithms.Sha384,
				TspAlgorithms.Sha512,
				TspAlgorithms.RipeMD128,
				TspAlgorithms.RipeMD160,
				TspAlgorithms.RipeMD256
			};
			TspAlgorithms.Allowed = new ArrayList();
			foreach (string value in array)
			{
				TspAlgorithms.Allowed.Add(value);
			}
		}

		// Token: 0x0400191A RID: 6426
		public static readonly string MD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x0400191B RID: 6427
		public static readonly string Sha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x0400191C RID: 6428
		public static readonly string Sha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x0400191D RID: 6429
		public static readonly string Sha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x0400191E RID: 6430
		public static readonly string Sha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x0400191F RID: 6431
		public static readonly string Sha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x04001920 RID: 6432
		public static readonly string RipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x04001921 RID: 6433
		public static readonly string RipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x04001922 RID: 6434
		public static readonly string RipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x04001923 RID: 6435
		public static readonly string Gost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x04001924 RID: 6436
		public static readonly ArrayList Allowed;
	}
}
