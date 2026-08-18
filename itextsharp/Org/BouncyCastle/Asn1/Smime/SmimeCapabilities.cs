using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x02000574 RID: 1396
	public class SmimeCapabilities : Asn1Encodable
	{
		// Token: 0x06002FA4 RID: 12196 RVA: 0x00126DCC File Offset: 0x00125DCC
		public static SmimeCapabilities GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapabilities)
			{
				return (SmimeCapabilities)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapabilities((Asn1Sequence)obj);
			}
			if (obj is AttributeX509)
			{
				return new SmimeCapabilities((Asn1Sequence)((AttributeX509)obj).AttrValues[0]);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00126E42 File Offset: 0x00125E42
		public SmimeCapabilities(Asn1Sequence seq)
		{
			this.capabilities = seq;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00126E54 File Offset: 0x00125E54
		public ArrayList GetCapabilities(DerObjectIdentifier capability)
		{
			ArrayList arrayList = new ArrayList();
			if (capability == null)
			{
				using (IEnumerator enumerator = this.capabilities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SmimeCapability instance = SmimeCapability.GetInstance(obj);
						arrayList.Add(instance);
					}
					return arrayList;
				}
			}
			foreach (object obj2 in this.capabilities)
			{
				SmimeCapability instance2 = SmimeCapability.GetInstance(obj2);
				if (capability.Equals(instance2.CapabilityID))
				{
					arrayList.Add(instance2);
				}
			}
			return arrayList;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00126F24 File Offset: 0x00125F24
		public override Asn1Object ToAsn1Object()
		{
			return this.capabilities;
		}

		// Token: 0x040020BE RID: 8382
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x040020BF RID: 8383
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x040020C0 RID: 8384
		public static readonly DerObjectIdentifier SmimeCapabilitesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x040020C1 RID: 8385
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x040020C2 RID: 8386
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x040020C3 RID: 8387
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x040020C4 RID: 8388
		private Asn1Sequence capabilities;
	}
}
