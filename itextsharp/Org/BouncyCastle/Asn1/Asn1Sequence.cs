using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000A4 RID: 164
	public abstract class Asn1Sequence : Asn1Object, IEnumerable
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0001BA52 File Offset: 0x0001AA52
		public static Asn1Sequence GetInstance(object obj)
		{
			if (obj == null || obj is Asn1Sequence)
			{
				return (Asn1Sequence)obj;
			}
			throw new ArgumentException("Unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001BA88 File Offset: 0x0001AA88
		public static Asn1Sequence GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly)
			{
				if (!obj.IsExplicit())
				{
					throw new ArgumentException("object implicit - explicit expected.");
				}
				return (Asn1Sequence)@object;
			}
			else if (obj.IsExplicit())
			{
				if (obj is BerTaggedObject)
				{
					return new BerSequence(@object);
				}
				return new DerSequence(@object);
			}
			else
			{
				if (@object is Asn1Sequence)
				{
					return (Asn1Sequence)@object;
				}
				throw new ArgumentException("Unknown object in GetInstance: " + obj.GetType().FullName, "obj");
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001BB05 File Offset: 0x0001AB05
		protected internal Asn1Sequence(int capacity)
		{
			this.seq = new ArrayList(capacity);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001BB19 File Offset: 0x0001AB19
		public virtual IEnumerator GetEnumerator()
		{
			return this.seq.GetEnumerator();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001BB26 File Offset: 0x0001AB26
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001BB2E File Offset: 0x0001AB2E
		public virtual Asn1SequenceParser Parser
		{
			get
			{
				return new Asn1Sequence.Asn1SequenceParserImpl(this);
			}
		}

		// Token: 0x170000EC RID: 236
		public virtual Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this.seq[index];
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001BB49 File Offset: 0x0001AB49
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable GetObjectAt(int index)
		{
			return this[index];
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001BB52 File Offset: 0x0001AB52
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001BB5A File Offset: 0x0001AB5A
		public virtual int Count
		{
			get
			{
				return this.seq.Count;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001BB68 File Offset: 0x0001AB68
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

		// Token: 0x06000539 RID: 1337 RVA: 0x0001BBCC File Offset: 0x0001ABCC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1Sequence asn1Sequence = asn1Object as Asn1Sequence;
			if (asn1Sequence == null)
			{
				return false;
			}
			if (this.Count != asn1Sequence.Count)
			{
				return false;
			}
			IEnumerator enumerator = this.GetEnumerator();
			IEnumerator enumerator2 = asn1Sequence.GetEnumerator();
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

		// Token: 0x0600053A RID: 1338 RVA: 0x0001BC37 File Offset: 0x0001AC37
		protected internal void AddObject(Asn1Encodable obj)
		{
			this.seq.Add(obj);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001BC46 File Offset: 0x0001AC46
		public override string ToString()
		{
			return CollectionUtilities.ToString(this.seq);
		}

		// Token: 0x0400029A RID: 666
		private readonly ArrayList seq;

		// Token: 0x020000A6 RID: 166
		private class Asn1SequenceParserImpl : Asn1SequenceParser, IAsn1Convertible
		{
			// Token: 0x0600053D RID: 1341 RVA: 0x0001BC53 File Offset: 0x0001AC53
			public Asn1SequenceParserImpl(Asn1Sequence outer)
			{
				this.outer = outer;
				this.max = outer.Count;
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x0001BC70 File Offset: 0x0001AC70
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

			// Token: 0x0600053F RID: 1343 RVA: 0x0001BCD3 File Offset: 0x0001ACD3
			public Asn1Object ToAsn1Object()
			{
				return this.outer;
			}

			// Token: 0x0400029B RID: 667
			private readonly Asn1Sequence outer;

			// Token: 0x0400029C RID: 668
			private readonly int max;

			// Token: 0x0400029D RID: 669
			private int index;
		}
	}
}
