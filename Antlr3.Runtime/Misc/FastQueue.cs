using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr.Runtime.Misc
{
	// Token: 0x02000025 RID: 37
	public class FastQueue<T>
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005DE1 File Offset: 0x00003FE1
		public virtual int Count
		{
			get
			{
				return this._data.Count - this._p;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005DF5 File Offset: 0x00003FF5
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00005DFD File Offset: 0x00003FFD
		public virtual int Range { get; protected set; }

		// Token: 0x1700006B RID: 107
		public virtual T this[int i]
		{
			get
			{
				int num = this._p + i;
				if (num >= this._data.Count)
				{
					throw new ArgumentException(string.Format("queue index {0} > last index {1}", num, this._data.Count - 1));
				}
				if (num < 0)
				{
					throw new ArgumentException(string.Format("queue index {0} < 0", num));
				}
				if (num > this.Range)
				{
					this.Range = num;
				}
				return this._data[num];
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005E8C File Offset: 0x0000408C
		public virtual T Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException();
			}
			T result = this[0];
			this._p++;
			if (this._p == this._data.Count)
			{
				this.Clear();
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005ED7 File Offset: 0x000040D7
		public virtual void Enqueue(T o)
		{
			this._data.Add(o);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005EE5 File Offset: 0x000040E5
		public virtual T Peek()
		{
			return this[0];
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005EEE File Offset: 0x000040EE
		public virtual void Clear()
		{
			this._p = 0;
			this._data.Clear();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005F04 File Offset: 0x00004104
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(this[i]);
				if (i + 1 < count)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000057 RID: 87
		internal List<T> _data = new List<T>();

		// Token: 0x04000058 RID: 88
		internal int _p;
	}
}
