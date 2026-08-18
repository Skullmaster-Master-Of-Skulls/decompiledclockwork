using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000039 RID: 57
	public class CrlDistPoint : Asn1Encodable
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000914E File Offset: 0x0000814E
		public static CrlDistPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CrlDistPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000915C File Offset: 0x0000815C
		public static CrlDistPoint GetInstance(object obj)
		{
			if (obj is CrlDistPoint || obj == null)
			{
				return (CrlDistPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlDistPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000091AE File Offset: 0x000081AE
		private CrlDistPoint(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000091BD File Offset: 0x000081BD
		public CrlDistPoint(DistributionPoint[] points)
		{
			this.seq = new DerSequence(points);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000091D4 File Offset: 0x000081D4
		public DistributionPoint[] GetDistributionPoints()
		{
			DistributionPoint[] array = new DistributionPoint[this.seq.Count];
			for (int num = 0; num != this.seq.Count; num++)
			{
				array[num] = DistributionPoint.GetInstance(this.seq[num]);
			}
			return array;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000921D File Offset: 0x0000821D
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009228 File Offset: 0x00008228
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("CRLDistPoint:");
			stringBuilder.Append(newLine);
			DistributionPoint[] distributionPoints = this.GetDistributionPoints();
			for (int num = 0; num != distributionPoints.Length; num++)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(distributionPoints[num]);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000B3 RID: 179
		internal readonly Asn1Sequence seq;
	}
}
