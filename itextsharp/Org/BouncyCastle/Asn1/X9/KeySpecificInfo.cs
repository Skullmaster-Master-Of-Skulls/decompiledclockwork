using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000622 RID: 1570
	public class KeySpecificInfo : Asn1Encodable
	{
		// Token: 0x0600355E RID: 13662 RVA: 0x0014B3BC File Offset: 0x0014A3BC
		public KeySpecificInfo(DerObjectIdentifier algorithm, Asn1OctetString counter)
		{
			this.algorithm = algorithm;
			this.counter = counter;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x0014B3D4 File Offset: 0x0014A3D4
		public KeySpecificInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.algorithm = (DerObjectIdentifier)enumerator.Current;
			enumerator.MoveNext();
			this.counter = (Asn1OctetString)enumerator.Current;
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x0014B41E File Offset: 0x0014A41E
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x0014B426 File Offset: 0x0014A426
		public Asn1OctetString Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x0014B430 File Offset: 0x0014A430
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algorithm,
				this.counter
			});
		}

		// Token: 0x040023A9 RID: 9129
		private DerObjectIdentifier algorithm;

		// Token: 0x040023AA RID: 9130
		private Asn1OctetString counter;
	}
}
