using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008EA RID: 2282
	internal struct ReadOnlySpan<T>
	{
		// Token: 0x060052DB RID: 21211 RVA: 0x0012AC43 File Offset: 0x00129C43
		public ReadOnlySpan(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0012AC4C File Offset: 0x00129C4C
		public ReadOnlySpan(T[] array, int offset, int count)
		{
			this = new ReadOnlySpan<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0012AC7C File Offset: 0x00129C7C
		public ReadOnlySpan(T[] array)
		{
			this = new ReadOnlySpan<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x17000E42 RID: 3650
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._Segment.Array[index + this._Segment.Offset];
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060052DF RID: 21215 RVA: 0x0012ACDF File Offset: 0x00129CDF
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060052E0 RID: 21216 RVA: 0x0012ACEF File Offset: 0x00129CEF
		public bool IsNull
		{
			get
			{
				return this._Segment.Array == null;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x0012ACFF File Offset: 0x00129CFF
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0012AD0C File Offset: 0x00129D0C
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0012AD42 File Offset: 0x00129D42
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0012AD84 File Offset: 0x00129D84
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0012ADD4 File Offset: 0x00129DD4
		public void CopyTo(Span<T> destination)
		{
			if (destination.Length < this.Length)
			{
				throw new InvalidOperationException("Destination too short");
			}
			if (!this.IsEmpty)
			{
				ArraySegment<T> arraySegment = destination.DangerousGetArraySegment();
				Array.Copy(this._Segment.Array, this._Segment.Offset, arraySegment.Array, arraySegment.Offset, this._Segment.Count);
			}
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0012AE40 File Offset: 0x00129E40
		public bool Overlaps(ReadOnlySpan<T> destination)
		{
			int num;
			return this.Overlaps(destination, out num);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0012AE58 File Offset: 0x00129E58
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			elementOffset = 0;
			if (this.IsEmpty || destination.IsEmpty)
			{
				return false;
			}
			if (this._Segment.Array != destination._Segment.Array)
			{
				return false;
			}
			elementOffset = destination._Segment.Offset - this._Segment.Offset;
			if (elementOffset >= this._Segment.Count || elementOffset <= -destination._Segment.Count)
			{
				elementOffset = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0012AED6 File Offset: 0x00129ED6
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0012AEDE File Offset: 0x00129EDE
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x04002AB3 RID: 10931
		public static readonly Span<T> Empty = default(Span<T>);

		// Token: 0x04002AB4 RID: 10932
		private ArraySegment<T> _Segment;
	}
}
