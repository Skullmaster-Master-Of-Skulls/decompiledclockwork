using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005B RID: 91
	public struct MethodDefinitionHandle : IEquatable<MethodDefinitionHandle>
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000931E File Offset: 0x0000751E
		private MethodDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009327 File Offset: 0x00007527
		internal static MethodDefinitionHandle FromRowId(int rowId)
		{
			return new MethodDefinitionHandle(rowId);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000932F File Offset: 0x0000752F
		public static implicit operator Handle(MethodDefinitionHandle handle)
		{
			return new Handle(6, handle._rowId);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000933D File Offset: 0x0000753D
		public static implicit operator EntityHandle(MethodDefinitionHandle handle)
		{
			return new EntityHandle((uint)(100663296L | (long)handle._rowId));
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00009353 File Offset: 0x00007553
		public static explicit operator MethodDefinitionHandle(Handle handle)
		{
			if (handle.VType != 6)
			{
				Throw.InvalidCast();
			}
			return new MethodDefinitionHandle(handle.RowId);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00009370 File Offset: 0x00007570
		public static explicit operator MethodDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 100663296U)
			{
				Throw.InvalidCast();
			}
			return new MethodDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00009391 File Offset: 0x00007591
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000939C File Offset: 0x0000759C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000093A4 File Offset: 0x000075A4
		public static bool operator ==(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000093B4 File Offset: 0x000075B4
		public override bool Equals(object obj)
		{
			return obj is MethodDefinitionHandle && ((MethodDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000093A4 File Offset: 0x000075A4
		public bool Equals(MethodDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000093D4 File Offset: 0x000075D4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000093EF File Offset: 0x000075EF
		public static bool operator !=(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00009402 File Offset: 0x00007602
		public MethodDebugInformationHandle ToDebugInformationHandle()
		{
			return MethodDebugInformationHandle.FromRowId(this._rowId);
		}

		// Token: 0x040002FE RID: 766
		private const uint tokenType = 100663296U;

		// Token: 0x040002FF RID: 767
		private const byte tokenTypeSmall = 6;

		// Token: 0x04000300 RID: 768
		private readonly int _rowId;
	}
}
