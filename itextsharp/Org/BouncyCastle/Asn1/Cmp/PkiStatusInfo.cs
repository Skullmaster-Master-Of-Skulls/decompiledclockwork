using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000B1 RID: 177
	public class PkiStatusInfo : Asn1Encodable
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x0001CC70 File Offset: 0x0001BC70
		public static PkiStatusInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiStatusInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001CC80 File Offset: 0x0001BC80
		public static PkiStatusInfo GetInstance(object obj)
		{
			if (obj is PkiStatusInfo)
			{
				return (PkiStatusInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiStatusInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001CCD0 File Offset: 0x0001BCD0
		public PkiStatusInfo(Asn1Sequence seq)
		{
			this.status = DerInteger.GetInstance(seq[0]);
			this.statusString = null;
			this.failInfo = null;
			if (seq.Count > 2)
			{
				this.statusString = PkiFreeText.GetInstance(seq[1]);
				this.failInfo = DerBitString.GetInstance(seq[2]);
				return;
			}
			if (seq.Count > 1)
			{
				object obj = seq[1];
				if (obj is DerBitString)
				{
					this.failInfo = DerBitString.GetInstance(obj);
					return;
				}
				this.statusString = PkiFreeText.GetInstance(obj);
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001CD63 File Offset: 0x0001BD63
		public PkiStatusInfo(int status)
		{
			this.status = new DerInteger(status);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001CD77 File Offset: 0x0001BD77
		public PkiStatusInfo(int status, PkiFreeText statusString)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001CD92 File Offset: 0x0001BD92
		public PkiStatusInfo(int status, PkiFreeText statusString, PkiFailureInfo failInfo)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
			this.failInfo = failInfo;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001CDB4 File Offset: 0x0001BDB4
		public BigInteger Status
		{
			get
			{
				return this.status.Value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001CDC1 File Offset: 0x0001BDC1
		public PkiFreeText StatusString
		{
			get
			{
				return this.statusString;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001CDC9 File Offset: 0x0001BDC9
		public DerBitString FailInfo
		{
			get
			{
				return this.failInfo;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001CDD4 File Offset: 0x0001BDD4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			if (this.statusString != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.statusString
				});
			}
			if (this.failInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.failInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040002B6 RID: 694
		private DerInteger status;

		// Token: 0x040002B7 RID: 695
		private PkiFreeText statusString;

		// Token: 0x040002B8 RID: 696
		private DerBitString failInfo;
	}
}
