using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x02000364 RID: 868
	public sealed class SecNamedCurves
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x000BAC5A File Offset: 0x000B9C5A
		private SecNamedCurves()
		{
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000BAC62 File Offset: 0x000B9C62
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.Decode(hex));
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000BAC70 File Offset: 0x000B9C70
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			SecNamedCurves.objIds.Add(name, oid);
			SecNamedCurves.names.Add(oid, name);
			SecNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000BAC98 File Offset: 0x000B9C98
		static SecNamedCurves()
		{
			SecNamedCurves.DefineCurve("secp112r1", SecObjectIdentifiers.SecP112r1, SecNamedCurves.Secp112r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp112r2", SecObjectIdentifiers.SecP112r2, SecNamedCurves.Secp112r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp128r1", SecObjectIdentifiers.SecP128r1, SecNamedCurves.Secp128r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp128r2", SecObjectIdentifiers.SecP128r2, SecNamedCurves.Secp128r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp160k1", SecObjectIdentifiers.SecP160k1, SecNamedCurves.Secp160k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp160r1", SecObjectIdentifiers.SecP160r1, SecNamedCurves.Secp160r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp160r2", SecObjectIdentifiers.SecP160r2, SecNamedCurves.Secp160r2Holder.Instance);
			SecNamedCurves.DefineCurve("secp192k1", SecObjectIdentifiers.SecP192k1, SecNamedCurves.Secp192k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp192r1", SecObjectIdentifiers.SecP192r1, SecNamedCurves.Secp192r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp224k1", SecObjectIdentifiers.SecP224k1, SecNamedCurves.Secp224k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp224r1", SecObjectIdentifiers.SecP224r1, SecNamedCurves.Secp224r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp256k1", SecObjectIdentifiers.SecP256k1, SecNamedCurves.Secp256k1Holder.Instance);
			SecNamedCurves.DefineCurve("secp256r1", SecObjectIdentifiers.SecP256r1, SecNamedCurves.Secp256r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp384r1", SecObjectIdentifiers.SecP384r1, SecNamedCurves.Secp384r1Holder.Instance);
			SecNamedCurves.DefineCurve("secp521r1", SecObjectIdentifiers.SecP521r1, SecNamedCurves.Secp521r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect113r1", SecObjectIdentifiers.SecT113r1, SecNamedCurves.Sect113r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect113r2", SecObjectIdentifiers.SecT113r2, SecNamedCurves.Sect113r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect131r1", SecObjectIdentifiers.SecT131r1, SecNamedCurves.Sect131r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect131r2", SecObjectIdentifiers.SecT131r2, SecNamedCurves.Sect131r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect163k1", SecObjectIdentifiers.SecT163k1, SecNamedCurves.Sect163k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect163r1", SecObjectIdentifiers.SecT163r1, SecNamedCurves.Sect163r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect163r2", SecObjectIdentifiers.SecT163r2, SecNamedCurves.Sect163r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect193r1", SecObjectIdentifiers.SecT193r1, SecNamedCurves.Sect193r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect193r2", SecObjectIdentifiers.SecT193r2, SecNamedCurves.Sect193r2Holder.Instance);
			SecNamedCurves.DefineCurve("sect233k1", SecObjectIdentifiers.SecT233k1, SecNamedCurves.Sect233k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect233r1", SecObjectIdentifiers.SecT233r1, SecNamedCurves.Sect233r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect239k1", SecObjectIdentifiers.SecT239k1, SecNamedCurves.Sect239k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect283k1", SecObjectIdentifiers.SecT283k1, SecNamedCurves.Sect283k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect283r1", SecObjectIdentifiers.SecT283r1, SecNamedCurves.Sect283r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect409k1", SecObjectIdentifiers.SecT409k1, SecNamedCurves.Sect409k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect409r1", SecObjectIdentifiers.SecT409r1, SecNamedCurves.Sect409r1Holder.Instance);
			SecNamedCurves.DefineCurve("sect571k1", SecObjectIdentifiers.SecT571k1, SecNamedCurves.Sect571k1Holder.Instance);
			SecNamedCurves.DefineCurve("sect571r1", SecObjectIdentifiers.SecT571r1, SecNamedCurves.Sect571r1Holder.Instance);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000BAF58 File Offset: 0x000B9F58
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)SecNamedCurves.objIds[name.ToLower(CultureInfo.InvariantCulture)];
			if (derObjectIdentifier != null)
			{
				return SecNamedCurves.GetByOid(derObjectIdentifier);
			}
			return null;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000BAF8C File Offset: 0x000B9F8C
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)SecNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000BAFB5 File Offset: 0x000B9FB5
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)SecNamedCurves.objIds[name.ToLower(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000BAFD1 File Offset: 0x000B9FD1
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)SecNamedCurves.names[oid];
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000BAFE3 File Offset: 0x000B9FE3
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(SecNamedCurves.objIds.Keys);
			}
		}

		// Token: 0x04001576 RID: 5494
		private static readonly Hashtable objIds = new Hashtable();

		// Token: 0x04001577 RID: 5495
		private static readonly Hashtable curves = new Hashtable();

		// Token: 0x04001578 RID: 5496
		private static readonly Hashtable names = new Hashtable();

		// Token: 0x02000365 RID: 869
		internal class Secp112r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F18 RID: 7960 RVA: 0x000BAFF4 File Offset: 0x000B9FF4
			private Secp112r1Holder()
			{
			}

			// Token: 0x06001F19 RID: 7961 RVA: 0x000BAFFC File Offset: 0x000B9FFC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD208B");
				BigInteger a = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD2088");
				BigInteger b = SecNamedCurves.FromHex("659EF8BA043916EEDE8911702B22");
				byte[] seed = Hex.Decode("00F50B028E4D696E676875615175290472783FB1");
				BigInteger n = SecNamedCurves.FromHex("DB7C2ABF62E35E7628DFAC6561C5");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0409487239995A5EE76B55F9C2F098A89CE5AF8724C0A23E0E0FF77500"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001579 RID: 5497
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp112r1Holder();
		}

		// Token: 0x02000366 RID: 870
		internal class Secp112r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F1B RID: 7963 RVA: 0x000BB081 File Offset: 0x000BA081
			private Secp112r2Holder()
			{
			}

			// Token: 0x06001F1C RID: 7964 RVA: 0x000BB08C File Offset: 0x000BA08C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("DB7C2ABF62E35E668076BEAD208B");
				BigInteger a = SecNamedCurves.FromHex("6127C24C05F38A0AAAF65C0EF02C");
				BigInteger b = SecNamedCurves.FromHex("51DEF1815DB5ED74FCC34C85D709");
				byte[] seed = Hex.Decode("002757A1114D696E6768756151755316C05E0BD4");
				BigInteger n = SecNamedCurves.FromHex("36DF0AAFD8B8D7597CA10520D04B");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("044BA30AB5E892B4E1649DD0928643ADCD46F5882E3747DEF36E956E97"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157A RID: 5498
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp112r2Holder();
		}

		// Token: 0x02000367 RID: 871
		internal class Secp128r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F1E RID: 7966 RVA: 0x000BB111 File Offset: 0x000BA111
			private Secp128r1Holder()
			{
			}

			// Token: 0x06001F1F RID: 7967 RVA: 0x000BB11C File Offset: 0x000BA11C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("E87579C11079F43DD824993C2CEE5ED3");
				byte[] seed = Hex.Decode("000E0D4D696E6768756151750CC03A4473D03679");
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFE0000000075A30D1B9038A115");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04161FF7528B899B2D0C28607CA52C5B86CF5AC8395BAFEB13C02DA292DDED7A83"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157B RID: 5499
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp128r1Holder();
		}

		// Token: 0x02000368 RID: 872
		internal class Secp128r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F21 RID: 7969 RVA: 0x000BB1A1 File Offset: 0x000BA1A1
			private Secp128r2Holder()
			{
			}

			// Token: 0x06001F22 RID: 7970 RVA: 0x000BB1AC File Offset: 0x000BA1AC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("D6031998D1B3BBFEBF59CC9BBFF9AEE1");
				BigInteger b = SecNamedCurves.FromHex("5EEEFCA380D02919DC2C6558BB6D8A5D");
				byte[] seed = Hex.Decode("004D696E67687561517512D8F03431FCE63B88F4");
				BigInteger n = SecNamedCurves.FromHex("3FFFFFFF7FFFFFFFBE0024720613B5A3");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("047B6AA5D85E572983E6FB32A7CDEBC14027B6916A894D3AEE7106FE805FC34B44"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157C RID: 5500
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp128r2Holder();
		}

		// Token: 0x02000369 RID: 873
		internal class Secp160k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F24 RID: 7972 RVA: 0x000BB231 File Offset: 0x000BA231
			private Secp160k1Holder()
			{
			}

			// Token: 0x06001F25 RID: 7973 RVA: 0x000BB23C File Offset: 0x000BA23C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(7L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("0100000000000000000001B8FA16DFAB9ACA16B6B3");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, zero, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("043B4C382CE37AA192A4019E763036F4F5DD4D7EBB938CF935318FDCED6BC28286531733C3F03C4FEE"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157D RID: 5501
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160k1Holder();
		}

		// Token: 0x0200036A RID: 874
		internal class Secp160r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F27 RID: 7975 RVA: 0x000BB2B0 File Offset: 0x000BA2B0
			private Secp160r1Holder()
			{
			}

			// Token: 0x06001F28 RID: 7976 RVA: 0x000BB2B8 File Offset: 0x000BA2B8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("1C97BEFC54BD7A8B65ACF89F81D4D4ADC565FA45");
				byte[] seed = Hex.Decode("1053CDE42C14D696E67687561517533BF3F83345");
				BigInteger n = SecNamedCurves.FromHex("0100000000000000000001F4C8F927AED3CA752257");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("044A96B5688EF573284664698968C38BB913CBFC8223A628553168947D59DCC912042351377AC5FB32"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157E RID: 5502
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160r1Holder();
		}

		// Token: 0x0200036B RID: 875
		internal class Secp160r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F2A RID: 7978 RVA: 0x000BB33D File Offset: 0x000BA33D
			private Secp160r2Holder()
			{
			}

			// Token: 0x06001F2B RID: 7979 RVA: 0x000BB348 File Offset: 0x000BA348
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC70");
				BigInteger b = SecNamedCurves.FromHex("B4E134D3FB59EB8BAB57274904664D5AF50388BA");
				byte[] seed = Hex.Decode("B99B99B099B323E02709A4D696E6768756151751");
				BigInteger n = SecNamedCurves.FromHex("0100000000000000000000351EE786A818F3A1A16B");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0452DCB034293A117E1F4FF11B30F7199D3144CE6DFEAFFEF2E331F296E071FA0DF9982CFEA7D43F2E"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400157F RID: 5503
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp160r2Holder();
		}

		// Token: 0x0200036C RID: 876
		internal class Secp192k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F2D RID: 7981 RVA: 0x000BB3CD File Offset: 0x000BA3CD
			private Secp192k1Holder()
			{
			}

			// Token: 0x06001F2E RID: 7982 RVA: 0x000BB3D8 File Offset: 0x000BA3D8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFEE37");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(3L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFE26F2FC170F69466A74DEFD8D");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, zero, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04DB4FF10EC057E9AE26B07D0280B7F4341DA5D1B1EAE06C7D9B2F2F6D9C5628A7844163D015BE86344082AA88D95E2F9D"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001580 RID: 5504
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp192k1Holder();
		}

		// Token: 0x0200036D RID: 877
		internal class Secp192r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F30 RID: 7984 RVA: 0x000BB44C File Offset: 0x000BA44C
			private Secp192r1Holder()
			{
			}

			// Token: 0x06001F31 RID: 7985 RVA: 0x000BB454 File Offset: 0x000BA454
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1");
				byte[] seed = Hex.Decode("3045AE6FC8422F64ED579528D38120EAE12196D5");
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04188DA80EB03090F67CBF20EB43A18800F4FF0AFD82FF101207192B95FFC8DA78631011ED6B24CDD573F977A11E794811"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001581 RID: 5505
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp192r1Holder();
		}

		// Token: 0x0200036E RID: 878
		internal class Secp224k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F33 RID: 7987 RVA: 0x000BB4D9 File Offset: 0x000BA4D9
			private Secp224k1Holder()
			{
			}

			// Token: 0x06001F34 RID: 7988 RVA: 0x000BB4E4 File Offset: 0x000BA4E4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFE56D");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(5L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("010000000000000000000000000001DCE8D2EC6184CAF0A971769FB1F7");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, zero, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04A1455B334DF099DF30FC28A169A467E9E47075A90F7E650EB6B7A45C7E089FED7FBA344282CAFBD6F7E319F7C0B0BD59E2CA4BDB556D61A5"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001582 RID: 5506
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp224k1Holder();
		}

		// Token: 0x0200036F RID: 879
		internal class Secp224r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F36 RID: 7990 RVA: 0x000BB558 File Offset: 0x000BA558
			private Secp224r1Holder()
			{
			}

			// Token: 0x06001F37 RID: 7991 RVA: 0x000BB560 File Offset: 0x000BA560
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE");
				BigInteger b = SecNamedCurves.FromHex("B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4");
				byte[] seed = Hex.Decode("BD71344799D5C7FCDC45B59FA3B9AB8F6A948BC5");
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04B70E0CBD6BB4BF7F321390B94A03C1D356C21122343280D6115C1D21BD376388B5F723FB4C22DFE6CD4375A05A07476444D5819985007E34"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001583 RID: 5507
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp224r1Holder();
		}

		// Token: 0x02000370 RID: 880
		internal class Secp256k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F39 RID: 7993 RVA: 0x000BB5E5 File Offset: 0x000BA5E5
			private Secp256k1Holder()
			{
			}

			// Token: 0x06001F3A RID: 7994 RVA: 0x000BB5F0 File Offset: 0x000BA5F0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F");
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(7L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, zero, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001584 RID: 5508
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp256k1Holder();
		}

		// Token: 0x02000371 RID: 881
		internal class Secp256r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F3C RID: 7996 RVA: 0x000BB664 File Offset: 0x000BA664
			private Secp256r1Holder()
			{
			}

			// Token: 0x06001F3D RID: 7997 RVA: 0x000BB66C File Offset: 0x000BA66C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B");
				byte[] seed = Hex.Decode("C49D360886E704936A6678E1139D26B7819F7E90");
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("046B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C2964FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001585 RID: 5509
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp256r1Holder();
		}

		// Token: 0x02000372 RID: 882
		internal class Secp384r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F3F RID: 7999 RVA: 0x000BB6F1 File Offset: 0x000BA6F1
			private Secp384r1Holder()
			{
			}

			// Token: 0x06001F40 RID: 8000 RVA: 0x000BB6FC File Offset: 0x000BA6FC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF");
				byte[] seed = Hex.Decode("A335926AA319A27A1D00896A6773A4827ACDAC73");
				BigInteger n = SecNamedCurves.FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04AA87CA22BE8B05378EB1C71EF320AD746E1D3B628BA79B9859F741E082542A385502F25DBF55296C3A545E3872760AB73617DE4A96262C6F5D9E98BF9292DC29F8F41DBD289A147CE9DA3113B5F0B8C00A60B1CE1D7E819D7A431D7C90EA0E5F"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001586 RID: 5510
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp384r1Holder();
		}

		// Token: 0x02000373 RID: 883
		internal class Secp521r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F42 RID: 8002 RVA: 0x000BB781 File Offset: 0x000BA781
			private Secp521r1Holder()
			{
			}

			// Token: 0x06001F43 RID: 8003 RVA: 0x000BB78C File Offset: 0x000BA78C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
				BigInteger a = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC");
				BigInteger b = SecNamedCurves.FromHex("0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00");
				byte[] seed = Hex.Decode("D09E8800291CB85396CC6717393284AAA0DA64BA");
				BigInteger n = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409");
				BigInteger h = BigInteger.ValueOf(1L);
				ECCurve eccurve = new FpCurve(q, a, b);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0400C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66011839296A789A3BC0045C8A5FB42C7D1BD998F54449579B446817AFBD17273E662C97EE72995EF42640C550B9013FAD0761353C7086A272C24088BE94769FD16650"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001587 RID: 5511
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Secp521r1Holder();
		}

		// Token: 0x02000374 RID: 884
		internal class Sect113r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F45 RID: 8005 RVA: 0x000BB811 File Offset: 0x000BA811
			private Sect113r1Holder()
			{
			}

			// Token: 0x06001F46 RID: 8006 RVA: 0x000BB81C File Offset: 0x000BA81C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("003088250CA6E7C7FE649CE85820F7");
				BigInteger b = SecNamedCurves.FromHex("00E8BEE4D3E2260744188BE0E9C723");
				byte[] seed = Hex.Decode("10E723AB14D696E6768756151756FEBF8FCB49A9");
				BigInteger n = SecNamedCurves.FromHex("0100000000000000D9CCEC8A39E56F");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(113, 9, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04009D73616F35F4AB1407D73562C10F00A52830277958EE84D1315ED31886"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001588 RID: 5512
			private const int m = 113;

			// Token: 0x04001589 RID: 5513
			private const int k = 9;

			// Token: 0x0400158A RID: 5514
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect113r1Holder();
		}

		// Token: 0x02000375 RID: 885
		internal class Sect113r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F48 RID: 8008 RVA: 0x000BB89A File Offset: 0x000BA89A
			private Sect113r2Holder()
			{
			}

			// Token: 0x06001F49 RID: 8009 RVA: 0x000BB8A4 File Offset: 0x000BA8A4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("00689918DBEC7E5A0DD6DFC0AA55C7");
				BigInteger b = SecNamedCurves.FromHex("0095E9A9EC9B297BD4BF36E059184F");
				byte[] seed = Hex.Decode("10C0FB15760860DEF1EEF4D696E676875615175D");
				BigInteger n = SecNamedCurves.FromHex("010000000000000108789B2496AF93");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(113, 9, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0401A57A6A7B26CA5EF52FCDB816479700B3ADC94ED1FE674C06E695BABA1D"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400158B RID: 5515
			private const int m = 113;

			// Token: 0x0400158C RID: 5516
			private const int k = 9;

			// Token: 0x0400158D RID: 5517
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect113r2Holder();
		}

		// Token: 0x02000376 RID: 886
		internal class Sect131r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F4B RID: 8011 RVA: 0x000BB922 File Offset: 0x000BA922
			private Sect131r1Holder()
			{
			}

			// Token: 0x06001F4C RID: 8012 RVA: 0x000BB92C File Offset: 0x000BA92C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("07A11B09A76B562144418FF3FF8C2570B8");
				BigInteger b = SecNamedCurves.FromHex("0217C05610884B63B9C6C7291678F9D341");
				byte[] seed = Hex.Decode("4D696E676875615175985BD3ADBADA21B43A97E2");
				BigInteger n = SecNamedCurves.FromHex("0400000000000000023123953A9464B54D");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(131, 2, 3, 8, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040081BAF91FDF9833C40F9C181343638399078C6E7EA38C001F73C8134B1B4EF9E150"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400158E RID: 5518
			private const int m = 131;

			// Token: 0x0400158F RID: 5519
			private const int k1 = 2;

			// Token: 0x04001590 RID: 5520
			private const int k2 = 3;

			// Token: 0x04001591 RID: 5521
			private const int k3 = 8;

			// Token: 0x04001592 RID: 5522
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect131r1Holder();
		}

		// Token: 0x02000377 RID: 887
		internal class Sect131r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F4E RID: 8014 RVA: 0x000BB9AE File Offset: 0x000BA9AE
			private Sect131r2Holder()
			{
			}

			// Token: 0x06001F4F RID: 8015 RVA: 0x000BB9B8 File Offset: 0x000BA9B8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("03E5A88919D7CAFCBF415F07C2176573B2");
				BigInteger b = SecNamedCurves.FromHex("04B8266A46C55657AC734CE38F018F2192");
				byte[] seed = Hex.Decode("985BD3ADBAD4D696E676875615175A21B43A97E3");
				BigInteger n = SecNamedCurves.FromHex("0400000000000000016954A233049BA98F");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(131, 2, 3, 8, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040356DCD8F2F95031AD652D23951BB366A80648F06D867940A5366D9E265DE9EB240F"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001593 RID: 5523
			private const int m = 131;

			// Token: 0x04001594 RID: 5524
			private const int k1 = 2;

			// Token: 0x04001595 RID: 5525
			private const int k2 = 3;

			// Token: 0x04001596 RID: 5526
			private const int k3 = 8;

			// Token: 0x04001597 RID: 5527
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect131r2Holder();
		}

		// Token: 0x02000378 RID: 888
		internal class Sect163k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F51 RID: 8017 RVA: 0x000BBA3A File Offset: 0x000BAA3A
			private Sect163k1Holder()
			{
			}

			// Token: 0x06001F52 RID: 8018 RVA: 0x000BBA44 File Offset: 0x000BAA44
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("04000000000000000000020108A2E0CC0D99F8A5EF");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(163, 3, 6, 7, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0402FE13C0537BBC11ACAA07D793DE4E6D5E5C94EEE80289070FB05D38FF58321F2E800536D538CCDAA3D9"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x04001598 RID: 5528
			private const int m = 163;

			// Token: 0x04001599 RID: 5529
			private const int k1 = 3;

			// Token: 0x0400159A RID: 5530
			private const int k2 = 6;

			// Token: 0x0400159B RID: 5531
			private const int k3 = 7;

			// Token: 0x0400159C RID: 5532
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163k1Holder();
		}

		// Token: 0x02000379 RID: 889
		internal class Sect163r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F54 RID: 8020 RVA: 0x000BBAB7 File Offset: 0x000BAAB7
			private Sect163r1Holder()
			{
			}

			// Token: 0x06001F55 RID: 8021 RVA: 0x000BBAC0 File Offset: 0x000BAAC0
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("07B6882CAAEFA84F9554FF8428BD88E246D2782AE2");
				BigInteger b = SecNamedCurves.FromHex("0713612DCDDCB40AAB946BDA29CA91F73AF958AFD9");
				byte[] seed = Hex.Decode("24B7B137C8A14D696E6768756151756FD0DA2E5C");
				BigInteger n = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFF48AAB689C29CA710279B");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(163, 3, 6, 7, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040369979697AB43897789566789567F787A7876A65400435EDB42EFAFB2989D51FEFCE3C80988F41FF883"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x0400159D RID: 5533
			private const int m = 163;

			// Token: 0x0400159E RID: 5534
			private const int k1 = 3;

			// Token: 0x0400159F RID: 5535
			private const int k2 = 6;

			// Token: 0x040015A0 RID: 5536
			private const int k3 = 7;

			// Token: 0x040015A1 RID: 5537
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163r1Holder();
		}

		// Token: 0x0200037A RID: 890
		internal class Sect163r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F57 RID: 8023 RVA: 0x000BBB42 File Offset: 0x000BAB42
			private Sect163r2Holder()
			{
			}

			// Token: 0x06001F58 RID: 8024 RVA: 0x000BBB4C File Offset: 0x000BAB4C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = SecNamedCurves.FromHex("020A601907B8C953CA1481EB10512F78744A3205FD");
				byte[] seed = Hex.Decode("85E25BFE5C86226CDB12016F7553F9D0E693A268");
				BigInteger n = SecNamedCurves.FromHex("040000000000000000000292FE77E70C12A4234C33");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(163, 3, 6, 7, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0403F0EBA16286A2D57EA0991168D4994637E8343E3600D51FBC6C71A0094FA2CDD545B11C5C0C797324F1"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015A2 RID: 5538
			private const int m = 163;

			// Token: 0x040015A3 RID: 5539
			private const int k1 = 3;

			// Token: 0x040015A4 RID: 5540
			private const int k2 = 6;

			// Token: 0x040015A5 RID: 5541
			private const int k3 = 7;

			// Token: 0x040015A6 RID: 5542
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect163r2Holder();
		}

		// Token: 0x0200037B RID: 891
		internal class Sect193r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F5A RID: 8026 RVA: 0x000BBBCB File Offset: 0x000BABCB
			private Sect193r1Holder()
			{
			}

			// Token: 0x06001F5B RID: 8027 RVA: 0x000BBBD4 File Offset: 0x000BABD4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("0017858FEB7A98975169E171F77B4087DE098AC8A911DF7B01");
				BigInteger b = SecNamedCurves.FromHex("00FDFB49BFE6C3A89FACADAA7A1E5BBC7CC1C2E5D831478814");
				byte[] seed = Hex.Decode("103FAEC74D696E676875615175777FC5B191EF30");
				BigInteger n = SecNamedCurves.FromHex("01000000000000000000000000C7F34A778F443ACC920EBA49");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(193, 15, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0401F481BC5F0FF84A74AD6CDF6FDEF4BF6179625372D8C0C5E10025E399F2903712CCF3EA9E3A1AD17FB0B3201B6AF7CE1B05"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015A7 RID: 5543
			private const int m = 193;

			// Token: 0x040015A8 RID: 5544
			private const int k = 15;

			// Token: 0x040015A9 RID: 5545
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect193r1Holder();
		}

		// Token: 0x0200037C RID: 892
		internal class Sect193r2Holder : X9ECParametersHolder
		{
			// Token: 0x06001F5D RID: 8029 RVA: 0x000BBC55 File Offset: 0x000BAC55
			private Sect193r2Holder()
			{
			}

			// Token: 0x06001F5E RID: 8030 RVA: 0x000BBC60 File Offset: 0x000BAC60
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = SecNamedCurves.FromHex("0163F35A5137C2CE3EA6ED8667190B0BC43ECD69977702709B");
				BigInteger b = SecNamedCurves.FromHex("00C9BB9E8927D4D64C377E2AB2856A5B16E3EFB7F61D4316AE");
				byte[] seed = Hex.Decode("10B7B4D696E676875615175137C8A16FD0DA2211");
				BigInteger n = SecNamedCurves.FromHex("010000000000000000000000015AAB561B005413CCD4EE99D5");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(193, 15, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0400D9B67D192E0367C803F39E1A7E82CA14A651350AAE617E8F01CE94335607C304AC29E7DEFBD9CA01F596F927224CDECF6C"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015AA RID: 5546
			private const int m = 193;

			// Token: 0x040015AB RID: 5547
			private const int k = 15;

			// Token: 0x040015AC RID: 5548
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect193r2Holder();
		}

		// Token: 0x0200037D RID: 893
		internal class Sect233k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F60 RID: 8032 RVA: 0x000BBCE1 File Offset: 0x000BACE1
			private Sect233k1Holder()
			{
			}

			// Token: 0x06001F61 RID: 8033 RVA: 0x000BBCEC File Offset: 0x000BACEC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("8000000000000000000000000000069D5BB915BCD46EFB1AD5F173ABDF");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new F2mCurve(233, 74, zero, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04017232BA853A7E731AF129F22FF4149563A419C26BF50A4C9D6EEFAD612601DB537DECE819B7F70F555A67C427A8CD9BF18AEB9B56E0C11056FAE6A3"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015AD RID: 5549
			private const int m = 233;

			// Token: 0x040015AE RID: 5550
			private const int k = 74;

			// Token: 0x040015AF RID: 5551
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect233k1Holder();
		}

		// Token: 0x0200037E RID: 894
		internal class Sect233r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F63 RID: 8035 RVA: 0x000BBD5C File Offset: 0x000BAD5C
			private Sect233r1Holder()
			{
			}

			// Token: 0x06001F64 RID: 8036 RVA: 0x000BBD64 File Offset: 0x000BAD64
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = SecNamedCurves.FromHex("0066647EDE6C332C7F8C0923BB58213B333B20E9CE4281FE115F7D8F90AD");
				byte[] seed = Hex.Decode("74D59FF07F6B413D0EA14B344B20A2DB049B50C3");
				BigInteger n = SecNamedCurves.FromHex("01000000000000000000000000000013E974E72F8A6922031D2603CFE0D7");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(233, 74, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0400FAC9DFCBAC8313BB2139F1BB755FEF65BC391F8B36F8F8EB7371FD558B01006A08A41903350678E58528BEBF8A0BEFF867A7CA36716F7E01F81052"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015B0 RID: 5552
			private const int m = 233;

			// Token: 0x040015B1 RID: 5553
			private const int k = 74;

			// Token: 0x040015B2 RID: 5554
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect233r1Holder();
		}

		// Token: 0x0200037F RID: 895
		internal class Sect239k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F66 RID: 8038 RVA: 0x000BBDE2 File Offset: 0x000BADE2
			private Sect239k1Holder()
			{
			}

			// Token: 0x06001F67 RID: 8039 RVA: 0x000BBDEC File Offset: 0x000BADEC
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("2000000000000000000000000000005A79FEC67CB6E91F1C1DA800E478A5");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new F2mCurve(239, 158, zero, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0429A0B6A887A983E9730988A68727A8B2D126C44CC2CC7B2A6555193035DC76310804F12E549BDB011C103089E73510ACB275FC312A5DC6B76553F0CA"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015B3 RID: 5555
			private const int m = 239;

			// Token: 0x040015B4 RID: 5556
			private const int k = 158;

			// Token: 0x040015B5 RID: 5557
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect239k1Holder();
		}

		// Token: 0x02000380 RID: 896
		internal class Sect283k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F69 RID: 8041 RVA: 0x000BBE5F File Offset: 0x000BAE5F
			private Sect283k1Holder()
			{
			}

			// Token: 0x06001F6A RID: 8042 RVA: 0x000BBE68 File Offset: 0x000BAE68
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE9AE2ED07577265DFF7F94451E061E163C61");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new F2mCurve(283, 5, 7, 12, zero, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040503213F78CA44883F1A3B8162F188E553CD265F23C1567A16876913B0C2AC245849283601CCDA380F1C9E318D90F95D07E5426FE87E45C0E8184698E45962364E34116177DD2259"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015B6 RID: 5558
			private const int m = 283;

			// Token: 0x040015B7 RID: 5559
			private const int k1 = 5;

			// Token: 0x040015B8 RID: 5560
			private const int k2 = 7;

			// Token: 0x040015B9 RID: 5561
			private const int k3 = 12;

			// Token: 0x040015BA RID: 5562
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect283k1Holder();
		}

		// Token: 0x02000381 RID: 897
		internal class Sect283r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F6C RID: 8044 RVA: 0x000BBEDA File Offset: 0x000BAEDA
			private Sect283r1Holder()
			{
			}

			// Token: 0x06001F6D RID: 8045 RVA: 0x000BBEE4 File Offset: 0x000BAEE4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = SecNamedCurves.FromHex("027B680AC8B8596DA5A4AF8A19A0303FCA97FD7645309FA2A581485AF6263E313B79A2F5");
				byte[] seed = Hex.Decode("77E2B07370EB0F832A6DD5B62DFC88CD06BB84BE");
				BigInteger n = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEF90399660FC938A90165B042A7CEFADB307");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(283, 5, 7, 12, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("0405F939258DB7DD90E1934F8C70B0DFEC2EED25B8557EAC9C80E2E198F8CDBECD86B1205303676854FE24141CB98FE6D4B20D02B4516FF702350EDDB0826779C813F0DF45BE8112F4"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015BB RID: 5563
			private const int m = 283;

			// Token: 0x040015BC RID: 5564
			private const int k1 = 5;

			// Token: 0x040015BD RID: 5565
			private const int k2 = 7;

			// Token: 0x040015BE RID: 5566
			private const int k3 = 12;

			// Token: 0x040015BF RID: 5567
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect283r1Holder();
		}

		// Token: 0x02000382 RID: 898
		internal class Sect409k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F6F RID: 8047 RVA: 0x000BBF64 File Offset: 0x000BAF64
			private Sect409k1Holder()
			{
			}

			// Token: 0x06001F70 RID: 8048 RVA: 0x000BBF6C File Offset: 0x000BAF6C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE5F83B2D4EA20400EC4557D5ED3E3E7CA5B4B5C83B8E01E5FCF");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new F2mCurve(409, 87, zero, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040060F05F658F49C1AD3AB1890F7184210EFD0987E307C84C27ACCFB8F9F67CC2C460189EB5AAAA62EE222EB1B35540CFE902374601E369050B7C4E42ACBA1DACBF04299C3460782F918EA427E6325165E9EA10E3DA5F6C42E9C55215AA9CA27A5863EC48D8E0286B"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015C0 RID: 5568
			private const int m = 409;

			// Token: 0x040015C1 RID: 5569
			private const int k = 87;

			// Token: 0x040015C2 RID: 5570
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect409k1Holder();
		}

		// Token: 0x02000383 RID: 899
		internal class Sect409r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F72 RID: 8050 RVA: 0x000BBFDC File Offset: 0x000BAFDC
			private Sect409r1Holder()
			{
			}

			// Token: 0x06001F73 RID: 8051 RVA: 0x000BBFE4 File Offset: 0x000BAFE4
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = SecNamedCurves.FromHex("0021A5C2C8EE9FEB5C4B9A753B7B476B7FD6422EF1F3DD674761FA99D6AC27C8A9A197B272822F6CD57A55AA4F50AE317B13545F");
				byte[] seed = Hex.Decode("4099B5A457F9D69F79213D094C4BCD4D4262210B");
				BigInteger n = SecNamedCurves.FromHex("010000000000000000000000000000000000000000000000000001E2AAD6A612F33307BE5FA47C3C9E052F838164CD37D9A21173");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(409, 87, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04015D4860D088DDB3496B0C6064756260441CDE4AF1771D4DB01FFE5B34E59703DC255A868A1180515603AEAB60794E54BB7996A70061B1CFAB6BE5F32BBFA78324ED106A7636B9C5A7BD198D0158AA4F5488D08F38514F1FDF4B4F40D2181B3681C364BA0273C706"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015C3 RID: 5571
			private const int m = 409;

			// Token: 0x040015C4 RID: 5572
			private const int k = 87;

			// Token: 0x040015C5 RID: 5573
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect409r1Holder();
		}

		// Token: 0x02000384 RID: 900
		internal class Sect571k1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F75 RID: 8053 RVA: 0x000BC062 File Offset: 0x000BB062
			private Sect571k1Holder()
			{
			}

			// Token: 0x06001F76 RID: 8054 RVA: 0x000BC06C File Offset: 0x000BB06C
			protected override X9ECParameters CreateParameters()
			{
				BigInteger zero = BigInteger.Zero;
				BigInteger b = BigInteger.ValueOf(1L);
				byte[] seed = null;
				BigInteger n = SecNamedCurves.FromHex("020000000000000000000000000000000000000000000000000000000000000000000000131850E1F19A63E4B391A8DB917F4138B630D84BE5D639381E91DEB45CFE778F637C1001");
				BigInteger h = BigInteger.ValueOf(4L);
				ECCurve eccurve = new F2mCurve(571, 2, 5, 10, zero, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("04026EB7A859923FBC82189631F8103FE4AC9CA2970012D5D46024804801841CA44370958493B205E647DA304DB4CEB08CBBD1BA39494776FB988B47174DCA88C7E2945283A01C89720349DC807F4FBF374F4AEADE3BCA95314DD58CEC9F307A54FFC61EFC006D8A2C9D4979C0AC44AEA74FBEBBB9F772AEDCB620B01A7BA7AF1B320430C8591984F601CD4C143EF1C7A3"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015C6 RID: 5574
			private const int m = 571;

			// Token: 0x040015C7 RID: 5575
			private const int k1 = 2;

			// Token: 0x040015C8 RID: 5576
			private const int k2 = 5;

			// Token: 0x040015C9 RID: 5577
			private const int k3 = 10;

			// Token: 0x040015CA RID: 5578
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect571k1Holder();
		}

		// Token: 0x02000385 RID: 901
		internal class Sect571r1Holder : X9ECParametersHolder
		{
			// Token: 0x06001F78 RID: 8056 RVA: 0x000BC0DE File Offset: 0x000BB0DE
			private Sect571r1Holder()
			{
			}

			// Token: 0x06001F79 RID: 8057 RVA: 0x000BC0E8 File Offset: 0x000BB0E8
			protected override X9ECParameters CreateParameters()
			{
				BigInteger a = BigInteger.ValueOf(1L);
				BigInteger b = SecNamedCurves.FromHex("02F40E7E2221F295DE297117B7F3D62F5C6A97FFCB8CEFF1CD6BA8CE4A9A18AD84FFABBD8EFA59332BE7AD6756A66E294AFD185A78FF12AA520E4DE739BACA0C7FFEFF7F2955727A");
				byte[] seed = Hex.Decode("2AA058F73A0E33AB486B0F610410C53A7F132310");
				BigInteger n = SecNamedCurves.FromHex("03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE661CE18FF55987308059B186823851EC7DD9CA1161DE93D5174D66E8382E9BB2FE84E47");
				BigInteger h = BigInteger.ValueOf(2L);
				ECCurve eccurve = new F2mCurve(571, 2, 5, 10, a, b, n, h);
				ECPoint g = eccurve.DecodePoint(Hex.Decode("040303001D34B856296C16C0D40D3CD7750A93D1D2955FA80AA5F40FC8DB7B2ABDBDE53950F4C0D293CDD711A35B67FB1499AE60038614F1394ABFA3B4C850D927E1E7769C8EEC2D19037BF27342DA639B6DCCFFFEB73D69D78C6C27A6009CBBCA1980F8533921E8A684423E43BAB08A576291AF8F461BB2A8B3531D2F0485C19B16E2F1516E23DD3C1A4827AF1B8AC15B"));
				return new X9ECParameters(eccurve, g, n, h, seed);
			}

			// Token: 0x040015CB RID: 5579
			private const int m = 571;

			// Token: 0x040015CC RID: 5580
			private const int k1 = 2;

			// Token: 0x040015CD RID: 5581
			private const int k2 = 5;

			// Token: 0x040015CE RID: 5582
			private const int k3 = 10;

			// Token: 0x040015CF RID: 5583
			internal static readonly X9ECParametersHolder Instance = new SecNamedCurves.Sect571r1Holder();
		}
	}
}
