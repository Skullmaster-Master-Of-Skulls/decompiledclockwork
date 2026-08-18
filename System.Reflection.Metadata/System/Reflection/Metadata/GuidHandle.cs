using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000075 RID: 117
	public struct GuidHandle : IEquatable<GuidHandle>
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0000AB6A File Offset: 0x00008D6A
		private GuidHandle(int index)
		{
			this._index = index;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000AB73 File Offset: 0x00008D73
		internal static GuidHandle FromIndex(int heapIndex)
		{
			return new GuidHandle(heapIndex);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000AB7B File Offset: 0x00008D7B
		public static implicit operator Handle(GuidHandle handle)
		{
			return new Handle(114, handle._index);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000AB8A File Offset: 0x00008D8A
		public static explicit operator GuidHandle(Handle handle)
		{
			if (handle.VType != 114)
			{
				Throw.InvalidCast();
			}
			return new GuidHandle(handle.Offset);
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public bool IsNil
		{
			get
			{
				return this._index == 0;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000ABBB File Offset: 0x00008DBB
		public override bool Equals(object obj)
		{
			return obj is GuidHandle && this.Equals((GuidHandle)obj);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000ABD3 File Offset: 0x00008DD3
		public bool Equals(GuidHandle other)
		{
			return this._index == other._index;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public override int GetHashCode()
		{
			return this._index;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000ABE3 File Offset: 0x00008DE3
		public static bool operator ==(GuidHandle left, GuidHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000ABED File Offset: 0x00008DED
		public static bool operator !=(GuidHandle left, GuidHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000345 RID: 837
		private readonly int _index;
	}
}
