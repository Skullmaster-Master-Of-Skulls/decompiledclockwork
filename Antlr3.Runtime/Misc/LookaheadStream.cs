using System;

namespace Antlr.Runtime.Misc
{
	// Token: 0x0200002A RID: 42
	public abstract class LookaheadStream<T> : FastQueue<T> where T : class
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000602D File Offset: 0x0000422D
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00006035 File Offset: 0x00004235
		public T EndOfFile
		{
			get
			{
				return this._eof;
			}
			protected set
			{
				this._eof = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000603E File Offset: 0x0000423E
		public T PreviousElement
		{
			get
			{
				return this._previousElement;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006046 File Offset: 0x00004246
		public virtual void Reset()
		{
			this.Clear();
			this._currentElementIndex = 0;
			this._p = 0;
			this._previousElement = default(T);
		}

		// Token: 0x060001EB RID: 491
		public abstract T NextElement();

		// Token: 0x060001EC RID: 492
		public abstract bool IsEndOfFile(T o);

		// Token: 0x060001ED RID: 493 RVA: 0x00006068 File Offset: 0x00004268
		public override T Dequeue()
		{
			T t = this[0];
			this._p++;
			if (this._p == this._data.Count && this._markDepth == 0)
			{
				this._previousElement = t;
				this.Clear();
			}
			return t;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000060B4 File Offset: 0x000042B4
		public virtual void Consume()
		{
			this.SyncAhead(1);
			this.Dequeue();
			this._currentElementIndex++;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000060D4 File Offset: 0x000042D4
		protected virtual void SyncAhead(int need)
		{
			int num = this._p + need - 1 - this._data.Count + 1;
			if (num > 0)
			{
				this.Fill(num);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006108 File Offset: 0x00004308
		public virtual void Fill(int n)
		{
			for (int i = 0; i < n; i++)
			{
				T t = this.NextElement();
				if (this.IsEndOfFile(t))
				{
					this._eof = t;
				}
				this._data.Add(t);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006144 File Offset: 0x00004344
		public override int Count
		{
			get
			{
				throw new NotSupportedException("streams are of unknown size");
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006150 File Offset: 0x00004350
		public virtual T LT(int k)
		{
			if (k == 0)
			{
				return default(T);
			}
			if (k < 0)
			{
				return this.LB(-k);
			}
			this.SyncAhead(k);
			if (this._p + k - 1 > this._data.Count)
			{
				return this._eof;
			}
			return this[k - 1];
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000061A5 File Offset: 0x000043A5
		public virtual int Index
		{
			get
			{
				return this._currentElementIndex;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000061AD File Offset: 0x000043AD
		public virtual int Mark()
		{
			this._markDepth++;
			this._lastMarker = this._p;
			return this._lastMarker;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000061CF File Offset: 0x000043CF
		public virtual void Release(int marker)
		{
			if (this._markDepth == 0)
			{
				throw new InvalidOperationException();
			}
			this._markDepth--;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000061F0 File Offset: 0x000043F0
		public virtual void Rewind(int marker)
		{
			this._markDepth--;
			int num = this._p - marker;
			this._currentElementIndex -= num;
			this._p = marker;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000622C File Offset: 0x0000442C
		public virtual void Rewind()
		{
			int num = this._p - this._lastMarker;
			this._currentElementIndex -= num;
			this._p = this._lastMarker;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006264 File Offset: 0x00004464
		public virtual void Seek(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = this._currentElementIndex - index;
			if (this._p - num < 0)
			{
				throw new NotSupportedException("can't seek before the beginning of this stream's buffer");
			}
			this._p -= num;
			this._currentElementIndex = index;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000062B4 File Offset: 0x000044B4
		protected virtual T LB(int k)
		{
			int num = this._p - k;
			if (num == -1)
			{
				return this._previousElement;
			}
			if (num >= 0)
			{
				return this._data[num];
			}
			if (num < -1)
			{
				throw new NotSupportedException("can't look more than one token before the beginning of this stream's buffer");
			}
			throw new NotSupportedException("can't look past the end of this stream's buffer using LB(int)");
		}

		// Token: 0x0400005A RID: 90
		private int _currentElementIndex;

		// Token: 0x0400005B RID: 91
		private T _previousElement;

		// Token: 0x0400005C RID: 92
		private T _eof = default(T);

		// Token: 0x0400005D RID: 93
		private int _lastMarker;

		// Token: 0x0400005E RID: 94
		private int _markDepth;
	}
}
