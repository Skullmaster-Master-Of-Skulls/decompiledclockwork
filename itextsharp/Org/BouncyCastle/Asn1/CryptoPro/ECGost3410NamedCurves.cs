using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000520 RID: 1312
	public sealed class ECGost3410NamedCurves
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x0010FB44 File Offset: 0x0010EB44
		private ECGost3410NamedCurves()
		{
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x0010FB4C File Offset: 0x0010EB4C
		static ECGost3410NamedCurves()
		{
			BigInteger q = new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639319");
			BigInteger n = new BigInteger("115792089237316195423570985008687907853073762908499243225378155805079068850323");
			FpCurve fpCurve = new FpCurve(q, new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639316"), new BigInteger("166"));
			ECDomainParameters value = new ECDomainParameters(fpCurve, fpCurve.CreatePoint(BigInteger.One, new BigInteger("64033881142927202683649881450433473985931760268884941288852745803908878638612"), false), n);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProA] = value;
			q = new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639319");
			n = new BigInteger("115792089237316195423570985008687907853073762908499243225378155805079068850323");
			fpCurve = new FpCurve(q, new BigInteger("115792089237316195423570985008687907853269984665640564039457584007913129639316"), new BigInteger("166"));
			value = new ECDomainParameters(fpCurve, fpCurve.CreatePoint(BigInteger.One, new BigInteger("64033881142927202683649881450433473985931760268884941288852745803908878638612"), false), n);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA] = value;
			q = new BigInteger("57896044618658097711785492504343953926634992332820282019728792003956564823193");
			n = new BigInteger("57896044618658097711785492504343953927102133160255826820068844496087732066703");
			fpCurve = new FpCurve(q, new BigInteger("57896044618658097711785492504343953926634992332820282019728792003956564823190"), new BigInteger("28091019353058090096996979000309560759124368558014865957655842872397301267595"));
			value = new ECDomainParameters(fpCurve, fpCurve.CreatePoint(BigInteger.One, new BigInteger("28792665814854611296992347458380284135028636778229113005756334730996303888124"), false), n);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProB] = value;
			q = new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502619");
			n = new BigInteger("70390085352083305199547718019018437840920882647164081035322601458352298396601");
			fpCurve = new FpCurve(q, new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502616"), new BigInteger("32858"));
			value = new ECDomainParameters(fpCurve, fpCurve.CreatePoint(BigInteger.Zero, new BigInteger("29818893917731240733471273240314769927240550812383695689146495261604565990247"), false), n);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB] = value;
			q = new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502619");
			n = new BigInteger("70390085352083305199547718019018437840920882647164081035322601458352298396601");
			fpCurve = new FpCurve(q, new BigInteger("70390085352083305199547718019018437841079516630045180471284346843705633502616"), new BigInteger("32858"));
			value = new ECDomainParameters(fpCurve, fpCurve.CreatePoint(BigInteger.Zero, new BigInteger("29818893917731240733471273240314769927240550812383695689146495261604565990247"), false), n);
			ECGost3410NamedCurves.parameters[CryptoProObjectIdentifiers.GostR3410x2001CryptoProC] = value;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-A"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProA;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-B"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProB;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-C"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProC;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-XchA"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA;
			ECGost3410NamedCurves.objIds["GostR3410-2001-CryptoPro-XchB"] = CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB;
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProA] = "GostR3410-2001-CryptoPro-A";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProB] = "GostR3410-2001-CryptoPro-B";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProC] = "GostR3410-2001-CryptoPro-C";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchA] = "GostR3410-2001-CryptoPro-XchA";
			ECGost3410NamedCurves.names[CryptoProObjectIdentifiers.GostR3410x2001CryptoProXchB] = "GostR3410-2001-CryptoPro-XchB";
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0010FE1A File Offset: 0x0010EE1A
		public static ECDomainParameters GetByOid(DerObjectIdentifier oid)
		{
			return (ECDomainParameters)ECGost3410NamedCurves.parameters[oid];
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x0010FE2C File Offset: 0x0010EE2C
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(ECGost3410NamedCurves.objIds.Keys);
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0010FE40 File Offset: 0x0010EE40
		public static ECDomainParameters GetByName(string name)
		{
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)ECGost3410NamedCurves.objIds[name];
			if (derObjectIdentifier != null)
			{
				return (ECDomainParameters)ECGost3410NamedCurves.parameters[derObjectIdentifier];
			}
			return null;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x0010FE73 File Offset: 0x0010EE73
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)ECGost3410NamedCurves.names[oid];
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0010FE85 File Offset: 0x0010EE85
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)ECGost3410NamedCurves.objIds[name];
		}

		// Token: 0x04001ECC RID: 7884
		internal static readonly Hashtable objIds = new Hashtable();

		// Token: 0x04001ECD RID: 7885
		internal static readonly Hashtable parameters = new Hashtable();

		// Token: 0x04001ECE RID: 7886
		internal static readonly Hashtable names = new Hashtable();
	}
}
