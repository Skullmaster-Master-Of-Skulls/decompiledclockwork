using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x020000AA RID: 170
	public class LdsSecurityObject : Asn1Encodable
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001C16C File Offset: 0x0001B16C
		public static LdsSecurityObject GetInstance(object obj)
		{
			if (obj == null || obj is LdsSecurityObject)
			{
				return (LdsSecurityObject)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new LdsSecurityObject(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001C1BC File Offset: 0x0001B1BC
		public LdsSecurityObject(Asn1Sequence seq)
		{
			if (seq == null || seq.Count == 0)
			{
				throw new ArgumentException("null or empty sequence passed.");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digestAlgorithmIdentifier = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			Asn1Sequence instance = Asn1Sequence.GetInstance(enumerator.Current);
			this.CheckDatagroupHashSeqSize(instance.Count);
			this.datagroupHash = new DataGroupHash[instance.Count];
			for (int i = 0; i < instance.Count; i++)
			{
				this.datagroupHash[i] = DataGroupHash.GetInstance(instance[i]);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001C27D File Offset: 0x0001B27D
		public LdsSecurityObject(AlgorithmIdentifier digestAlgorithmIdentifier, DataGroupHash[] datagroupHash)
		{
			this.digestAlgorithmIdentifier = digestAlgorithmIdentifier;
			this.datagroupHash = datagroupHash;
			this.CheckDatagroupHashSeqSize(datagroupHash.Length);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001C2A8 File Offset: 0x0001B2A8
		private void CheckDatagroupHashSeqSize(int size)
		{
			if (size < 2 || size > 16)
			{
				throw new ArgumentException("wrong size in DataGroupHashValues : not in (2.." + 16 + ")");
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001C2CF File Offset: 0x0001B2CF
		public AlgorithmIdentifier DigestAlgorithmIdentifier
		{
			get
			{
				return this.digestAlgorithmIdentifier;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001C2D7 File Offset: 0x0001B2D7
		public DataGroupHash[] GetDatagroupHash()
		{
			return this.datagroupHash;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001C2E0 File Offset: 0x0001B2E0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithmIdentifier,
				new DerSequence(this.datagroupHash)
			});
		}

		// Token: 0x040002A5 RID: 677
		public const int UBDataGroups = 16;

		// Token: 0x040002A6 RID: 678
		internal DerInteger version = new DerInteger(0);

		// Token: 0x040002A7 RID: 679
		internal AlgorithmIdentifier digestAlgorithmIdentifier;

		// Token: 0x040002A8 RID: 680
		internal DataGroupHash[] datagroupHash;
	}
}
