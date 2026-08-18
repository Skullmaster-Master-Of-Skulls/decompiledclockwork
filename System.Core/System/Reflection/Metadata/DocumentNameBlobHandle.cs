using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005F RID: 95
	internal struct DocumentNameBlobHandle : IEquatable<DocumentNameBlobHandle>
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00007249 File Offset: 0x00005449
		private DocumentNameBlobHandle(int heapOffset)
		{
			this._heapOffset = heapOffset;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007252 File Offset: 0x00005452
		internal static DocumentNameBlobHandle FromOffset(int heapOffset)
		{
			return new DocumentNameBlobHandle(heapOffset);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000725A File Offset: 0x0000545A
		public static implicit operator BlobHandle(DocumentNameBlobHandle handle)
		{
			return BlobHandle.FromOffset(handle._heapOffset);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007267 File Offset: 0x00005467
		public static explicit operator DocumentNameBlobHandle(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				Throw.InvalidCast();
			}
			return DocumentNameBlobHandle.FromOffset(handle.GetHeapOffset());
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007283 File Offset: 0x00005483
		public bool IsNil
		{
			get
			{
				return this._heapOffset == 0;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000728E File Offset: 0x0000548E
		public override bool Equals(object obj)
		{
			return obj is DocumentNameBlobHandle && this.Equals((DocumentNameBlobHandle)obj);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000072A6 File Offset: 0x000054A6
		public bool Equals(DocumentNameBlobHandle other)
		{
			return this._heapOffset == other._heapOffset;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000072B6 File Offset: 0x000054B6
		public override int GetHashCode()
		{
			return this._heapOffset;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000072BE File Offset: 0x000054BE
		public static bool operator ==(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000072C8 File Offset: 0x000054C8
		public static bool operator !=(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400034B RID: 843
		private readonly int _heapOffset;
	}
}
