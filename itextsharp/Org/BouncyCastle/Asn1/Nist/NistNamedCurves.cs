using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x020004D3 RID: 1235
	public sealed class NistNamedCurves
	{
		// Token: 0x06002A1E RID: 10782 RVA: 0x001001F6 File Offset: 0x000FF1F6
		private NistNamedCurves()
		{
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x001001FE File Offset: 0x000FF1FE
		private static void DefineCurve(string name, DerObjectIdentifier oid)
		{
			NistNamedCurves.objIds.Add(name, oid);
			NistNamedCurves.names.Add(oid, name);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00100218 File Offset: 0x000FF218
		static NistNamedCurves()
		{
			NistNamedCurves.DefineCurve("B-571", SecObjectIdentifiers.SecT571r1);
			NistNamedCurves.DefineCurve("B-409", SecObjectIdentifiers.SecT409r1);
			NistNamedCurves.DefineCurve("B-283", SecObjectIdentifiers.SecT283r1);
			NistNamedCurves.DefineCurve("B-233", SecObjectIdentifiers.SecT233r1);
			NistNamedCurves.DefineCurve("B-163", SecObjectIdentifiers.SecT163r2);
			NistNamedCurves.DefineCurve("P-521", SecObjectIdentifiers.SecP521r1);
			NistNamedCurves.DefineCurve("P-384", SecObjectIdentifiers.SecP384r1);
			NistNamedCurves.DefineCurve("P-256", SecObjectIdentifiers.SecP256r1);
			NistNamedCurves.DefineCurve("P-224", SecObjectIdentifiers.SecP224r1);
			NistNamedCurves.DefineCurve("P-192", SecObjectIdentifiers.SecP192r1);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x001002D0 File Offset: 0x000FF2D0
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)NistNamedCurves.objIds[name.ToUpper(CultureInfo.InvariantCulture)];
			if (derObjectIdentifier != null)
			{
				return NistNamedCurves.GetByOid(derObjectIdentifier);
			}
			return null;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00100303 File Offset: 0x000FF303
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			return SecNamedCurves.GetByOid(oid);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0010030B File Offset: 0x000FF30B
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)NistNamedCurves.objIds[name.ToUpper(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00100327 File Offset: 0x000FF327
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)NistNamedCurves.names[oid];
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x00100339 File Offset: 0x000FF339
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(NistNamedCurves.objIds.Keys);
			}
		}

		// Token: 0x04001D51 RID: 7505
		private static readonly Hashtable objIds = new Hashtable();

		// Token: 0x04001D52 RID: 7506
		private static readonly Hashtable names = new Hashtable();
	}
}
