using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x020001A3 RID: 419
	public abstract class X9ObjectIdentifiers
	{
		// Token: 0x04000B9F RID: 2975
		internal const string AnsiX962 = "1.2.840.10045";

		// Token: 0x04000BA0 RID: 2976
		internal const string IdFieldType = "1.2.840.10045.1";

		// Token: 0x04000BA1 RID: 2977
		public const string IdECSigType = "1.2.840.10045.4";

		// Token: 0x04000BA2 RID: 2978
		public const string IdPublicKeyType = "1.2.840.10045.2";

		// Token: 0x04000BA3 RID: 2979
		public static readonly DerObjectIdentifier PrimeField = new DerObjectIdentifier("1.2.840.10045.1.1");

		// Token: 0x04000BA4 RID: 2980
		public static readonly DerObjectIdentifier CharacteristicTwoField = new DerObjectIdentifier("1.2.840.10045.1.2");

		// Token: 0x04000BA5 RID: 2981
		public static readonly DerObjectIdentifier GNBasis = new DerObjectIdentifier("1.2.840.10045.1.2.3.1");

		// Token: 0x04000BA6 RID: 2982
		public static readonly DerObjectIdentifier TPBasis = new DerObjectIdentifier("1.2.840.10045.1.2.3.2");

		// Token: 0x04000BA7 RID: 2983
		public static readonly DerObjectIdentifier PPBasis = new DerObjectIdentifier("1.2.840.10045.1.2.3.3");

		// Token: 0x04000BA8 RID: 2984
		public static readonly DerObjectIdentifier ECDsaWithSha1 = new DerObjectIdentifier("1.2.840.10045.4.1");

		// Token: 0x04000BA9 RID: 2985
		public static readonly DerObjectIdentifier IdECPublicKey = new DerObjectIdentifier("1.2.840.10045.2.1");

		// Token: 0x04000BAA RID: 2986
		public static readonly DerObjectIdentifier ECDsaWithSha2 = new DerObjectIdentifier("1.2.840.10045.4.3");

		// Token: 0x04000BAB RID: 2987
		public static readonly DerObjectIdentifier ECDsaWithSha224 = new DerObjectIdentifier(X9ObjectIdentifiers.ECDsaWithSha2 + ".1");

		// Token: 0x04000BAC RID: 2988
		public static readonly DerObjectIdentifier ECDsaWithSha256 = new DerObjectIdentifier(X9ObjectIdentifiers.ECDsaWithSha2 + ".2");

		// Token: 0x04000BAD RID: 2989
		public static readonly DerObjectIdentifier ECDsaWithSha384 = new DerObjectIdentifier(X9ObjectIdentifiers.ECDsaWithSha2 + ".3");

		// Token: 0x04000BAE RID: 2990
		public static readonly DerObjectIdentifier ECDsaWithSha512 = new DerObjectIdentifier(X9ObjectIdentifiers.ECDsaWithSha2 + ".4");

		// Token: 0x04000BAF RID: 2991
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.2.840.10045.3");

		// Token: 0x04000BB0 RID: 2992
		public static readonly DerObjectIdentifier CTwoCurve = new DerObjectIdentifier(X9ObjectIdentifiers.EllipticCurve + ".0");

		// Token: 0x04000BB1 RID: 2993
		public static readonly DerObjectIdentifier C2Pnb163v1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".1");

		// Token: 0x04000BB2 RID: 2994
		public static readonly DerObjectIdentifier C2Pnb163v2 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".2");

		// Token: 0x04000BB3 RID: 2995
		public static readonly DerObjectIdentifier C2Pnb163v3 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".3");

		// Token: 0x04000BB4 RID: 2996
		public static readonly DerObjectIdentifier C2Pnb176w1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".4");

		// Token: 0x04000BB5 RID: 2997
		public static readonly DerObjectIdentifier C2Tnb191v1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".5");

		// Token: 0x04000BB6 RID: 2998
		public static readonly DerObjectIdentifier C2Tnb191v2 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".6");

		// Token: 0x04000BB7 RID: 2999
		public static readonly DerObjectIdentifier C2Tnb191v3 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".7");

		// Token: 0x04000BB8 RID: 3000
		public static readonly DerObjectIdentifier C2Onb191v4 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".8");

		// Token: 0x04000BB9 RID: 3001
		public static readonly DerObjectIdentifier C2Onb191v5 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".9");

		// Token: 0x04000BBA RID: 3002
		public static readonly DerObjectIdentifier C2Pnb208w1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".10");

		// Token: 0x04000BBB RID: 3003
		public static readonly DerObjectIdentifier C2Tnb239v1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".11");

		// Token: 0x04000BBC RID: 3004
		public static readonly DerObjectIdentifier C2Tnb239v2 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".12");

		// Token: 0x04000BBD RID: 3005
		public static readonly DerObjectIdentifier C2Tnb239v3 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".13");

		// Token: 0x04000BBE RID: 3006
		public static readonly DerObjectIdentifier C2Onb239v4 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".14");

		// Token: 0x04000BBF RID: 3007
		public static readonly DerObjectIdentifier C2Onb239v5 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".15");

		// Token: 0x04000BC0 RID: 3008
		public static readonly DerObjectIdentifier C2Pnb272w1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".16");

		// Token: 0x04000BC1 RID: 3009
		public static readonly DerObjectIdentifier C2Pnb304w1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".17");

		// Token: 0x04000BC2 RID: 3010
		public static readonly DerObjectIdentifier C2Tnb359v1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".18");

		// Token: 0x04000BC3 RID: 3011
		public static readonly DerObjectIdentifier C2Pnb368w1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".19");

		// Token: 0x04000BC4 RID: 3012
		public static readonly DerObjectIdentifier C2Tnb431r1 = new DerObjectIdentifier(X9ObjectIdentifiers.CTwoCurve + ".20");

		// Token: 0x04000BC5 RID: 3013
		public static readonly DerObjectIdentifier PrimeCurve = new DerObjectIdentifier(X9ObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x04000BC6 RID: 3014
		public static readonly DerObjectIdentifier Prime192v1 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".1");

		// Token: 0x04000BC7 RID: 3015
		public static readonly DerObjectIdentifier Prime192v2 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".2");

		// Token: 0x04000BC8 RID: 3016
		public static readonly DerObjectIdentifier Prime192v3 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".3");

		// Token: 0x04000BC9 RID: 3017
		public static readonly DerObjectIdentifier Prime239v1 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".4");

		// Token: 0x04000BCA RID: 3018
		public static readonly DerObjectIdentifier Prime239v2 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".5");

		// Token: 0x04000BCB RID: 3019
		public static readonly DerObjectIdentifier Prime239v3 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".6");

		// Token: 0x04000BCC RID: 3020
		public static readonly DerObjectIdentifier Prime256v1 = new DerObjectIdentifier(X9ObjectIdentifiers.PrimeCurve + ".7");

		// Token: 0x04000BCD RID: 3021
		public static readonly DerObjectIdentifier DHPublicNumber = new DerObjectIdentifier("1.2.840.10046.2.1");

		// Token: 0x04000BCE RID: 3022
		public static readonly DerObjectIdentifier IdDsa = new DerObjectIdentifier("1.2.840.10040.4.1");

		// Token: 0x04000BCF RID: 3023
		public static readonly DerObjectIdentifier IdDsaWithSha1 = new DerObjectIdentifier("1.2.840.10040.4.3");

		// Token: 0x04000BD0 RID: 3024
		public static readonly DerObjectIdentifier X9x63Scheme = new DerObjectIdentifier("1.3.133.16.840.63.0");

		// Token: 0x04000BD1 RID: 3025
		public static readonly DerObjectIdentifier DHSinglePassStdDHSha1KdfScheme = new DerObjectIdentifier(X9ObjectIdentifiers.X9x63Scheme + ".2");

		// Token: 0x04000BD2 RID: 3026
		public static readonly DerObjectIdentifier DHSinglePassCofactorDHSha1KdfScheme = new DerObjectIdentifier(X9ObjectIdentifiers.X9x63Scheme + ".3");

		// Token: 0x04000BD3 RID: 3027
		public static readonly DerObjectIdentifier MqvSinglePassSha1KdfScheme = new DerObjectIdentifier(X9ObjectIdentifiers.X9x63Scheme + ".16");

		// Token: 0x04000BD4 RID: 3028
		public static readonly DerObjectIdentifier X9x42Schemes = new DerObjectIdentifier("1.2.840.10046.3");

		// Token: 0x04000BD5 RID: 3029
		public static readonly DerObjectIdentifier DHStatic = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".1");

		// Token: 0x04000BD6 RID: 3030
		public static readonly DerObjectIdentifier DHEphem = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".2");

		// Token: 0x04000BD7 RID: 3031
		public static readonly DerObjectIdentifier DHOneFlow = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".3");

		// Token: 0x04000BD8 RID: 3032
		public static readonly DerObjectIdentifier DHHybrid1 = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".4");

		// Token: 0x04000BD9 RID: 3033
		public static readonly DerObjectIdentifier DHHybrid2 = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".5");

		// Token: 0x04000BDA RID: 3034
		public static readonly DerObjectIdentifier DHHybridOneFlow = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".6");

		// Token: 0x04000BDB RID: 3035
		public static readonly DerObjectIdentifier Mqv2 = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".7");

		// Token: 0x04000BDC RID: 3036
		public static readonly DerObjectIdentifier Mqv1 = new DerObjectIdentifier(X9ObjectIdentifiers.X9x42Schemes + ".8");
	}
}
