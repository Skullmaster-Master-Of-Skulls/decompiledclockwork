using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000522 RID: 1314
	public class BerTaggedObject : DerTaggedObject
	{
		// Token: 0x06002CCD RID: 11469 RVA: 0x00110068 File Offset: 0x0010F068
		public BerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x00110072 File Offset: 0x0010F072
		public BerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0011007D File Offset: 0x0010F07D
		public BerTaggedObject(int tagNo) : base(false, tagNo, BerSequence.Empty)
		{
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0011008C File Offset: 0x0010F08C
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteTag(160, this.tagNo);
				derOut.WriteByte(128);
				if (!base.IsEmpty())
				{
					if (!this.explicitly)
					{
						IEnumerable enumerable;
						if (this.obj is Asn1OctetString)
						{
							if (this.obj is BerOctetString)
							{
								enumerable = (BerOctetString)this.obj;
							}
							else
							{
								Asn1OctetString asn1OctetString = (Asn1OctetString)this.obj;
								enumerable = new BerOctetString(asn1OctetString.GetOctets());
							}
						}
						else if (this.obj is Asn1Sequence)
						{
							enumerable = (Asn1Sequence)this.obj;
						}
						else
						{
							if (!(this.obj is Asn1Set))
							{
								throw Platform.CreateNotImplementedException(this.obj.GetType().Name);
							}
							enumerable = (Asn1Set)this.obj;
						}
						using (IEnumerator enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Asn1Encodable obj2 = (Asn1Encodable)obj;
								derOut.WriteObject(obj2);
							}
							goto IL_119;
						}
					}
					derOut.WriteObject(this.obj);
				}
				IL_119:
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}
	}
}
