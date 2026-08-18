using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000061 RID: 97
	internal struct MethodDebugInformationHandle : IEquatable<MethodDebugInformationHandle>
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00007366 File Offset: 0x00005566
		private MethodDebugInformationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000736F File Offset: 0x0000556F
		internal static MethodDebugInformationHandle FromRowId(int rowId)
		{
			return new MethodDebugInformationHandle(rowId);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007377 File Offset: 0x00005577
		public static implicit operator Handle(MethodDebugInformationHandle handle)
		{
			return new Handle(49, handle._rowId);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007386 File Offset: 0x00005586
		public static explicit operator MethodDebugInformationHandle(Handle handle)
		{
			if (handle.VType != 49)
			{
				Throw.InvalidCast();
			}
			return new MethodDebugInformationHandle(handle.RowId);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000073A4 File Offset: 0x000055A4
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000073AF File Offset: 0x000055AF
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000073B7 File Offset: 0x000055B7
		public static bool operator ==(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000073C7 File Offset: 0x000055C7
		public override bool Equals(object obj)
		{
			return obj is MethodDebugInformationHandle && ((MethodDebugInformationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000073E6 File Offset: 0x000055E6
		public bool Equals(MethodDebugInformationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000073F8 File Offset: 0x000055F8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007413 File Offset: 0x00005613
		public static bool operator !=(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400034D RID: 845
		private const uint tokenType = 822083584U;

		// Token: 0x0400034E RID: 846
		private const byte tokenTypeSmall = 49;

		// Token: 0x0400034F RID: 847
		private readonly int _rowId;
	}
}
