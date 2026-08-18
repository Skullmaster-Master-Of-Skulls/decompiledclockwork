using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005D RID: 93
	[CLSCompliant(true)]
	public abstract class Asn1Structured : Asn1Object
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00011130 File Offset: 0x00010130
		protected internal Asn1Structured(Asn1Identifier id) : this(id, 10)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011148 File Offset: 0x00010148
		protected internal Asn1Structured(Asn1Identifier id, int size)
		{
			this.contentIndex = 0;
			base..ctor(id);
			this.content = new Asn1Object[size];
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011174 File Offset: 0x00010174
		protected internal Asn1Structured(Asn1Identifier id, Asn1Object[] newContent, int size)
		{
			this.contentIndex = 0;
			base..ctor(id);
			this.content = newContent;
			this.contentIndex = size;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000111A0 File Offset: 0x000101A0
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000111B8 File Offset: 0x000101B8
		[CLSCompliant(false)]
		protected internal void decodeStructured(Asn1Decoder dec, Stream in_Renamed, int len)
		{
			int[] array = new int[1];
			while (len > 0)
			{
				this.add(dec.decode(in_Renamed, array));
				len -= array[0];
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000111EC File Offset: 0x000101EC
		public Asn1Object[] toArray()
		{
			Asn1Object[] array = new Asn1Object[this.contentIndex];
			Array.Copy(this.content, 0, array, 0, this.contentIndex);
			return array;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011220 File Offset: 0x00010220
		public void add(Asn1Object value_Renamed)
		{
			if (this.contentIndex == this.content.Length)
			{
				int num = this.contentIndex + this.contentIndex;
				Asn1Object[] destinationArray = new Asn1Object[num];
				Array.Copy(this.content, 0, destinationArray, 0, this.contentIndex);
				this.content = destinationArray;
			}
			this.content[this.contentIndex++] = value_Renamed;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011288 File Offset: 0x00010288
		public void set_Renamed(int index, Asn1Object value_Renamed)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Asn1Structured: get: index ",
					index,
					", size ",
					this.contentIndex
				}));
			}
			this.content[index] = value_Renamed;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000112E8 File Offset: 0x000102E8
		public Asn1Object get_Renamed(int index)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Asn1Structured: set: index ",
					index,
					", size ",
					this.contentIndex
				}));
			}
			return this.content[index];
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011348 File Offset: 0x00010348
		public int size()
		{
			return this.contentIndex;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011360 File Offset: 0x00010360
		[CLSCompliant(false)]
		public virtual string toString(string type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(type);
			for (int i = 0; i < this.contentIndex; i++)
			{
				stringBuilder.Append(this.content[i]);
				if (i != this.contentIndex - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(" }");
			return base.ToString() + stringBuilder.ToString();
		}

		// Token: 0x04000198 RID: 408
		private Asn1Object[] content;

		// Token: 0x04000199 RID: 409
		private int contentIndex;
	}
}
