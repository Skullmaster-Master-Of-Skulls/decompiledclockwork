using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008EB RID: 2283
	internal struct Span<T>
	{
		// Token: 0x060052EB RID: 21227 RVA: 0x0012AEF3 File Offset: 0x00129EF3
		public Span(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0012AEFC File Offset: 0x00129EFC
		public Span(T[] array, int offset, int count)
		{
			this = new Span<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0012AF2C File Offset: 0x00129F2C
		public Span(T[] array)
		{
			this = new Span<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x17000E46 RID: 3654
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
			set
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._Segment.Array[index + this._Segment.Offset] = value;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060052F0 RID: 21232 RVA: 0x0012AFCC File Offset: 0x00129FCC
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x0012AFDC File Offset: 0x00129FDC
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x0012AFE9 File Offset: 0x00129FE9
		public Span<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0012B01F File Offset: 0x0012A01F
		public Span<T> Slice(int start, int length)
		{
			if (start < 0 || length > this._Segment.Count - start)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x0012B05C File Offset: 0x0012A05C
		public void Fill(T value)
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = value;
			}
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0012B0A8 File Offset: 0x0012A0A8
		public void Clear()
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = default(T);
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0012B0FC File Offset: 0x0012A0FC
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0012B14C File Offset: 0x0012A14C
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

		// Token: 0x060052F8 RID: 21240 RVA: 0x0012B1B8 File Offset: 0x0012A1B8
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			return this.Overlaps(destination, out elementOffset);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0012B1DA File Offset: 0x0012A1DA
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0012B1E2 File Offset: 0x0012A1E2
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0012B1EA File Offset: 0x0012A1EA
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._Segment);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0012B1F8 File Offset: 0x0012A1F8
		public T[] DangerousGetArrayForPinning()
		{
			return this._Segment.Array;
		}

		// Token: 0x04002AB5 RID: 10933
		public static readonly Span<T> Empty = default(Span<T>);

		// Token: 0x04002AB6 RID: 10934
		private ArraySegment<T> _Segment;
	}
}
