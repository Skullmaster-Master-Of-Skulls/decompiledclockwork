using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000041 RID: 65
	public abstract class Asn1Set : Asn1Object, IEnumerable
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000097AD File Offset: 0x000087AD
		public static Asn1Set GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Set)
			{
				return (Asn1Set)obj;
			}
			throw new ArgumentException("Unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000097E0 File Offset: 0x000087E0
		public static Asn1Set GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Set)@object;
			}
			else
			{
				if (obj.IsExplicit())
				{
					return new DerSet(@object);
				}
				if (@object is Asn1Set)
				{
					return (Asn1Set)@object;
				}
				if (@object is Asn1Sequence)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
					Asn1Sequence asn1Sequence = (Asn1Sequence)@object;
					foreach (object obj2 in asn1Sequence)
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1Encodable
						});
					}
					return new DerSet(asn1EncodableVector, false);
				}
				throw new ArgumentException("Unknown object in GetInstance: " + obj.GetType().FullName, "obj");
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000098D0 File Offset: 0x000088D0
		protected internal Asn1Set(int capacity)
		{
			this._set = new ArrayList(capacity);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000098E4 File Offset: 0x000088E4
		public virtual IEnumerator GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000098F1 File Offset: 0x000088F1
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700003B RID: 59
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this._set[index];
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000990C File Offset: 0x0000890C
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetObjectAt(int index)
		{
			return this[index];
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009915 File Offset: 0x00008915
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000991D File Offset: 0x0000891D
		public virtual int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000992A File Offset: 0x0000892A
		public Asn1SetParser Parser
		{
			get
			{
				return new Asn1Set.Asn1SetParserImpl(this);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009934 File Offset: 0x00008934
		protected override int Asn1GetHashCode()
		{
			int num = this.Count;
			foreach (object obj in this)
			{
				num *= 17;
				if (obj != null)
				{
					num ^= obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009998 File Offset: 0x00008998
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Set asn1Set = asn1Object as Asn1Set;
			if (asn1Set == null)
			{
				return false;
			}
			if (this.Count != asn1Set.Count)
			{
				return false;
			}
			IEnumerator enumerator = this.GetEnumerator();
			IEnumerator enumerator2 = asn1Set.GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				Asn1Object asn1Object2 = ((Asn1Encodable)enumerator.Current).ToAsn1Object();
				if (!asn1Object2.Equals(enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009A04 File Offset: 0x00008A04
		private bool LessThanOrEqual(byte[] a, byte[] b)
		{
			int num = Math.Min(a.Length, b.Length);
			for (int num2 = 0; num2 != num; num2++)
			{
				if (a[num2] != b[num2])
				{
					return a[num2] < b[num2];
				}
			}
			return num == a.Length;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009A40 File Offset: 0x00008A40
		protected internal void Sort()
		{
			if (this._set.Count > 1)
			{
				bool flag = true;
				int num = this._set.Count - 1;
				while (flag)
				{
					int num2 = 0;
					int num3 = 0;
					byte[] a = ((Asn1Encodable)this._set[0]).GetEncoded();
					flag = false;
					while (num2 != num)
					{
						byte[] encoded = ((Asn1Encodable)this._set[num2 + 1]).GetEncoded();
						if (this.LessThanOrEqual(a, encoded))
						{
							a = encoded;
						}
						else
						{
							object value = this._set[num2];
							this._set[num2] = this._set[num2 + 1];
							this._set[num2 + 1] = value;
							flag = true;
							num3 = num2;
						}
						num2++;
					}
					num = num3;
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009B0B File Offset: 0x00008B0B
		protected internal void AddObject(Asn1Encodable obj)
		{
			this._set.Add(obj);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009B1A File Offset: 0x00008B1A
		public override string ToString()
		{
			return CollectionUtilities.ToString(this._set);
		}

		// Token: 0x040000CC RID: 204
		private readonly ArrayList _set;

		// Token: 0x02000043 RID: 67
		private class Asn1SetParserImpl : Asn1SetParser, IAsn1Convertible
		{
			// Token: 0x060001BA RID: 442 RVA: 0x00009B27 File Offset: 0x00008B27
			public Asn1SetParserImpl(Asn1Set outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00009B44 File Offset: 0x00008B44
			public IAsn1Convertible ReadObject()
			{
				if (this.index == this.max)
				{
					return null;
				}
				Asn1Encodable asn1Encodable = this.outer[this.index++];
				if (asn1Encodable is Asn1Sequence)
				{
					return ((Asn1Sequence)asn1Encodable).Parser;
				}
				if (asn1Encodable is Asn1Set)
				{
					return ((Asn1Set)asn1Encodable).Parser;
				}
				return asn1Encodable;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00009BA7 File Offset: 0x00008BA7
			public virtual Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x040000CD RID: 205
			private readonly Asn1Set outer;

			// Token: 0x040000CE RID: 206
			private readonly int max;

			// Token: 0x040000CF RID: 207
			private int index;
		}
	}
}
