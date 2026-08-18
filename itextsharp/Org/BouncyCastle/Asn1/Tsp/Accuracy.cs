using System;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x02000363 RID: 867
	public class Accuracy : Asn1Encodable
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x000BA9CC File Offset: 0x000B99CC
		public Accuracy(DerInteger seconds, DerInteger millis, DerInteger micros)
		{
			if (millis != null && (millis.Value.IntValue < 1 || millis.Value.IntValue > 999))
			{
				throw new ArgumentException("Invalid millis field : not in (1..999)");
			}
			if (micros != null && (micros.Value.IntValue < 1 || micros.Value.IntValue > 999))
			{
				throw new ArgumentException("Invalid micros field : not in (1..999)");
			}
			this.seconds = seconds;
			this.millis = millis;
			this.micros = micros;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000BAA50 File Offset: 0x000B9A50
		private Accuracy(Asn1Sequence seq)
		{
			for (int i = 0; i < seq.Count; i++)
			{
				if (seq[i] is DerInteger)
				{
					this.seconds = (DerInteger)seq[i];
				}
				else if (seq[i] is DerTaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)seq[i];
					switch (derTaggedObject.TagNo)
					{
					case 0:
						this.millis = DerInteger.GetInstance(derTaggedObject, false);
						if (this.millis.Value.IntValue < 1 || this.millis.Value.IntValue > 999)
						{
							throw new ArgumentException("Invalid millis field : not in (1..999).");
						}
						break;
					case 1:
						this.micros = DerInteger.GetInstance(derTaggedObject, false);
						if (this.micros.Value.IntValue < 1 || this.micros.Value.IntValue > 999)
						{
							throw new ArgumentException("Invalid micros field : not in (1..999).");
						}
						break;
					default:
						throw new ArgumentException("Invalig tag number");
					}
				}
			}
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000BAB68 File Offset: 0x000B9B68
		public static Accuracy GetInstance(object o)
		{
			if (o == null || o is Accuracy)
			{
				return (Accuracy)o;
			}
			if (o is Asn1Sequence)
			{
				return new Accuracy((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'Accuracy' factory: " + o.GetType().FullName);
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000BABB5 File Offset: 0x000B9BB5
		public DerInteger Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000BABBD File Offset: 0x000B9BBD
		public DerInteger Millis
		{
			get
			{
				return this.millis;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000BABC5 File Offset: 0x000B9BC5
		public DerInteger Micros
		{
			get
			{
				return this.micros;
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000BABD0 File Offset: 0x000B9BD0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.seconds != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.seconds
				});
			}
			if (this.millis != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.millis)
				});
			}
			if (this.micros != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.micros)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400156F RID: 5487
		protected const int MinMillis = 1;

		// Token: 0x04001570 RID: 5488
		protected const int MaxMillis = 999;

		// Token: 0x04001571 RID: 5489
		protected const int MinMicros = 1;

		// Token: 0x04001572 RID: 5490
		protected const int MaxMicros = 999;

		// Token: 0x04001573 RID: 5491
		private readonly DerInteger seconds;

		// Token: 0x04001574 RID: 5492
		private readonly DerInteger millis;

		// Token: 0x04001575 RID: 5493
		private readonly DerInteger micros;
	}
}
