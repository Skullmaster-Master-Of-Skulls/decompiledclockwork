using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001614 RID: 5652
	internal class KerningPairs
	{
		// Token: 0x0600DC24 RID: 56356 RVA: 0x00301F6B File Offset: 0x0030016B
		public KerningPairs() : this(100)
		{
		}

		// Token: 0x0600DC25 RID: 56357 RVA: 0x00301F75 File Offset: 0x00300175
		public KerningPairs(int numPairs)
		{
			this.pairs = new Hashtable(100);
		}

		// Token: 0x0600DC26 RID: 56358 RVA: 0x00301F8A File Offset: 0x0030018A
		public bool HasKerning(int left, int right)
		{
			return this.pairs.Contains(this.GetIndex(left, right));
		}

		// Token: 0x1700435E RID: 17246
		public int this[int left, int right]
		{
			get
			{
				int index = this.GetIndex(left, right);
				if (!this.pairs.Contains(index))
				{
					return 0;
				}
				return (int)this.pairs[index];
			}
		}

		// Token: 0x1700435F RID: 17247
		// (get) Token: 0x0600DC28 RID: 56360 RVA: 0x00301FE5 File Offset: 0x003001E5
		public int Length
		{
			get
			{
				return this.pairs.Count;
			}
		}

		// Token: 0x0600DC29 RID: 56361 RVA: 0x00301FF4 File Offset: 0x003001F4
		internal void Add(int left, int right, int value)
		{
			if (value != 0)
			{
				int index = this.GetIndex(left, right);
				if (!this.pairs.Contains(index))
				{
					this.pairs[index] = value;
				}
			}
		}

		// Token: 0x0600DC2A RID: 56362 RVA: 0x00302037 File Offset: 0x00300237
		private int GetIndex(int left, int right)
		{
			return (left << 16) + right;
		}

		// Token: 0x04003D88 RID: 15752
		private IDictionary pairs;
	}
}
