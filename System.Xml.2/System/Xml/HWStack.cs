using System;

namespace System.Xml
{
	// Token: 0x0200006E RID: 110
	internal class HWStack : ICloneable
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000EE9B File Offset: 0x0000D09B
		internal HWStack(int GrowthRate) : this(GrowthRate, int.MaxValue)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000EEA9 File Offset: 0x0000D0A9
		internal HWStack(int GrowthRate, int limit)
		{
			this.growthRate = GrowthRate;
			this.used = 0;
			this.stack = new object[GrowthRate];
			this.size = GrowthRate;
			this.limit = limit;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		internal object Push()
		{
			if (this.used == this.size)
			{
				if (this.limit <= this.used)
				{
					throw new XmlException("Xml_StackOverflow", string.Empty);
				}
				object[] destinationArray = new object[this.size + this.growthRate];
				if (this.used > 0)
				{
					Array.Copy(this.stack, 0, destinationArray, 0, this.used);
				}
				this.stack = destinationArray;
				this.size += this.growthRate;
			}
			object[] array = this.stack;
			int num = this.used;
			this.used = num + 1;
			return array[num];
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000EF78 File Offset: 0x0000D178
		internal object Pop()
		{
			if (0 < this.used)
			{
				this.used--;
				return this.stack[this.used];
			}
			return null;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000EFAD File Offset: 0x0000D1AD
		internal object Peek()
		{
			if (this.used <= 0)
			{
				return null;
			}
			return this.stack[this.used - 1];
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		internal void AddToTop(object o)
		{
			if (this.used > 0)
			{
				this.stack[this.used - 1] = o;
			}
		}

		// Token: 0x170000E2 RID: 226
		internal object this[int index]
		{
			get
			{
				if (index >= 0 && index < this.used)
				{
					return this.stack[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this.used)
				{
					this.stack[index] = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000F02C File Offset: 0x0000D22C
		internal int Length
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F034 File Offset: 0x0000D234
		private HWStack(object[] stack, int growthRate, int used, int size)
		{
			this.stack = stack;
			this.growthRate = growthRate;
			this.used = used;
			this.size = size;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F059 File Offset: 0x0000D259
		public object Clone()
		{
			return new HWStack((object[])this.stack.Clone(), this.growthRate, this.used, this.size);
		}

		// Token: 0x040001BE RID: 446
		private object[] stack;

		// Token: 0x040001BF RID: 447
		private int growthRate;

		// Token: 0x040001C0 RID: 448
		private int used;

		// Token: 0x040001C1 RID: 449
		private int size;

		// Token: 0x040001C2 RID: 450
		private int limit;
	}
}
