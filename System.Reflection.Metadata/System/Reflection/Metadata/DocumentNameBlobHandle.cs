using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008C RID: 140
	public struct DocumentNameBlobHandle : IEquatable<DocumentNameBlobHandle>
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0000EE31 File Offset: 0x0000D031
		private DocumentNameBlobHandle(int heapOffset)
		{
			this._heapOffset = heapOffset;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000EE3A File Offset: 0x0000D03A
		internal static DocumentNameBlobHandle FromOffset(int heapOffset)
		{
			return new DocumentNameBlobHandle(heapOffset);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000EE42 File Offset: 0x0000D042
		public static implicit operator BlobHandle(DocumentNameBlobHandle handle)
		{
			return BlobHandle.FromOffset(handle._heapOffset);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000EE4F File Offset: 0x0000D04F
		public static explicit operator DocumentNameBlobHandle(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				Throw.InvalidCast();
			}
			return DocumentNameBlobHandle.FromOffset(handle.GetHeapOffset());
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000EE6B File Offset: 0x0000D06B
		public bool IsNil
		{
			get
			{
				return this._heapOffset == 0;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000EE76 File Offset: 0x0000D076
		public override bool Equals(object obj)
		{
			return obj is DocumentNameBlobHandle && this.Equals((DocumentNameBlobHandle)obj);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public bool Equals(DocumentNameBlobHandle other)
		{
			return this._heapOffset == other._heapOffset;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000EE9E File Offset: 0x0000D09E
		public override int GetHashCode()
		{
			return this._heapOffset;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000EEA6 File Offset: 0x0000D0A6
		public static bool operator ==(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public static bool operator !=(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040003D1 RID: 977
		private readonly int _heapOffset;
	}
}
