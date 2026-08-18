using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008EC RID: 2284
	internal struct ReadOnlyMemory<T>
	{
		// Token: 0x060052FE RID: 21246 RVA: 0x0012B212 File Offset: 0x0012A212
		public ReadOnlyMemory(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0012B21C File Offset: 0x0012A21C
		public ReadOnlyMemory(T[] array, int offset, int count)
		{
			this = new ReadOnlyMemory<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0012B24C File Offset: 0x0012A24C
		public ReadOnlyMemory(T[] array)
		{
			this = new ReadOnlyMemory<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x0012B274 File Offset: 0x0012A274
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x0012B294 File Offset: 0x0012A294
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x0012B2AF File Offset: 0x0012A2AF
		public ReadOnlySpan<T> Span
		{
			get
			{
				return new ReadOnlySpan<T>(this._Segment);
			}
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0012B2BC File Offset: 0x0012A2BC
		public ReadOnlyMemory<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0012B308 File Offset: 0x0012A308
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0012B35C File Offset: 0x0012A35C
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

		// Token: 0x06005307 RID: 21255 RVA: 0x0012B3D0 File Offset: 0x0012A3D0
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0012B3D8 File Offset: 0x0012A3D8
		public static implicit operator ArraySegment<T>(ReadOnlyMemory<T> memory)
		{
			return memory._Segment;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0012B3E1 File Offset: 0x0012A3E1
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment);
		}

		// Token: 0x04002AB7 RID: 10935
		private readonly ArraySegment<T> _Segment;
	}
}
