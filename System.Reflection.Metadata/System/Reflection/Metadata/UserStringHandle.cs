using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000071 RID: 113
	public struct UserStringHandle : IEquatable<UserStringHandle>
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000A736 File Offset: 0x00008936
		private UserStringHandle(int offset)
		{
			this._offset = offset;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000A73F File Offset: 0x0000893F
		internal static UserStringHandle FromOffset(int heapOffset)
		{
			return new UserStringHandle(heapOffset);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000A747 File Offset: 0x00008947
		public static implicit operator Handle(UserStringHandle handle)
		{
			return new Handle(112, handle._offset);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000A756 File Offset: 0x00008956
		public static explicit operator UserStringHandle(Handle handle)
		{
			if (handle.VType != 112)
			{
				Throw.InvalidCast();
			}
			return new UserStringHandle(handle.Offset);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000A774 File Offset: 0x00008974
		public bool IsNil
		{
			get
			{
				return this._offset == 0;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000A77F File Offset: 0x0000897F
		internal int GetHeapOffset()
		{
			return this._offset;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000A787 File Offset: 0x00008987
		public static bool operator ==(UserStringHandle left, UserStringHandle right)
		{
			return left._offset == right._offset;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000A797 File Offset: 0x00008997
		public override bool Equals(object obj)
		{
			return obj is UserStringHandle && ((UserStringHandle)obj)._offset == this._offset;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000A787 File Offset: 0x00008987
		public bool Equals(UserStringHandle other)
		{
			return this._offset == other._offset;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000A7B8 File Offset: 0x000089B8
		public override int GetHashCode()
		{
			return this._offset.GetHashCode();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000A7D3 File Offset: 0x000089D3
		public static bool operator !=(UserStringHandle left, UserStringHandle right)
		{
			return left._offset != right._offset;
		}

		// Token: 0x04000340 RID: 832
		private readonly int _offset;
	}
}
