using System;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200062B RID: 1579
	public class DataGroupHash : Asn1Encodable
	{
		// Token: 0x06003585 RID: 13701 RVA: 0x0014BC80 File Offset: 0x0014AC80
		public static DataGroupHash GetInstance(object obj)
		{
			if (obj == null || obj is DataGroupHash)
			{
				return (DataGroupHash)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DataGroupHash(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x0014BCD0 File Offset: 0x0014ACD0
		private DataGroupHash(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.dataGroupNumber = DerInteger.GetInstance(seq[0]);
			this.dataGroupHashValue = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0014BD20 File Offset: 0x0014AD20
		public DataGroupHash(int dataGroupNumber, Asn1OctetString dataGroupHashValue)
		{
			this.dataGroupNumber = new DerInteger(dataGroupNumber);
			this.dataGroupHashValue = dataGroupHashValue;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x0014BD3B File Offset: 0x0014AD3B
		public int DataGroupNumber
		{
			get
			{
				return this.dataGroupNumber.Value.IntValue;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x0014BD4D File Offset: 0x0014AD4D
		public Asn1OctetString DataGroupHashValue
		{
			get
			{
				return this.dataGroupHashValue;
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0014BD58 File Offset: 0x0014AD58
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.dataGroupNumber,
				this.dataGroupHashValue
			});
		}

		// Token: 0x040023D0 RID: 9168
		private readonly DerInteger dataGroupNumber;

		// Token: 0x040023D1 RID: 9169
		private readonly Asn1OctetString dataGroupHashValue;
	}
}
