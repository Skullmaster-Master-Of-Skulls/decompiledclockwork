using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000070 RID: 112
	public struct AssemblyFileHandle : IEquatable<AssemblyFileHandle>
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000A64E File Offset: 0x0000884E
		private AssemblyFileHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000A657 File Offset: 0x00008857
		internal static AssemblyFileHandle FromRowId(int rowId)
		{
			return new AssemblyFileHandle(rowId);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000A65F File Offset: 0x0000885F
		public static implicit operator Handle(AssemblyFileHandle handle)
		{
			return new Handle(38, handle._rowId);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000A66E File Offset: 0x0000886E
		public static implicit operator EntityHandle(AssemblyFileHandle handle)
		{
			return new EntityHandle((uint)(637534208L | (long)handle._rowId));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000A684 File Offset: 0x00008884
		public static explicit operator AssemblyFileHandle(Handle handle)
		{
			if (handle.VType != 38)
			{
				Throw.InvalidCast();
			}
			return new AssemblyFileHandle(handle.RowId);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000A6A2 File Offset: 0x000088A2
		public static explicit operator AssemblyFileHandle(EntityHandle handle)
		{
			if (handle.VType != 637534208U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyFileHandle(handle.RowId);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000A6C3 File Offset: 0x000088C3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000A6CE File Offset: 0x000088CE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000A6D6 File Offset: 0x000088D6
		public static bool operator ==(AssemblyFileHandle left, AssemblyFileHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000A6E6 File Offset: 0x000088E6
		public override bool Equals(object obj)
		{
			return obj is AssemblyFileHandle && ((AssemblyFileHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000A6D6 File Offset: 0x000088D6
		public bool Equals(AssemblyFileHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000A708 File Offset: 0x00008908
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000A723 File Offset: 0x00008923
		public static bool operator !=(AssemblyFileHandle left, AssemblyFileHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400033D RID: 829
		private const uint tokenType = 637534208U;

		// Token: 0x0400033E RID: 830
		private const byte tokenTypeSmall = 38;

		// Token: 0x0400033F RID: 831
		private readonly int _rowId;
	}
}
