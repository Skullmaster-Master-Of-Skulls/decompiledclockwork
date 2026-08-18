using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002C5 RID: 709
	public class Asn1EncodableVector : IEnumerable
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x0009C5A0 File Offset: 0x0009B5A0
		public static Asn1EncodableVector FromEnumerable(IEnumerable e)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in e)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0009C610 File Offset: 0x0009B610
		public Asn1EncodableVector(params Asn1Encodable[] v)
		{
			this.Add(v);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0009C62C File Offset: 0x0009B62C
		public void Add(params Asn1Encodable[] objs)
		{
			foreach (Asn1Encodable value in objs)
			{
				this.v.Add(value);
			}
		}

		// Token: 0x170004CA RID: 1226
		public Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this.v[index];
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0009C66D File Offset: 0x0009B66D
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable Get(int index)
		{
			return this[index];
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0009C676 File Offset: 0x0009B676
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.v.Count;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0009C683 File Offset: 0x0009B683
		public int Count
		{
			get
			{
				return this.v.Count;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0009C690 File Offset: 0x0009B690
		public IEnumerator GetEnumerator()
		{
			return this.v.GetEnumerator();
		}

		// Token: 0x040011BE RID: 4542
		private ArrayList v = new ArrayList();
	}
}
