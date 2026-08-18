using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000059 RID: 89
	public struct AssemblyDefinitionHandle : IEquatable<AssemblyDefinitionHandle>
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000914E File Offset: 0x0000734E
		internal AssemblyDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009157 File Offset: 0x00007357
		internal static AssemblyDefinitionHandle FromRowId(int rowId)
		{
			return new AssemblyDefinitionHandle(rowId);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000915F File Offset: 0x0000735F
		public static implicit operator Handle(AssemblyDefinitionHandle handle)
		{
			return new Handle(32, handle._rowId);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000916E File Offset: 0x0000736E
		public static implicit operator EntityHandle(AssemblyDefinitionHandle handle)
		{
			return new EntityHandle((uint)(536870912L | (long)handle._rowId));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009184 File Offset: 0x00007384
		public static explicit operator AssemblyDefinitionHandle(Handle handle)
		{
			if (handle.VType != 32)
			{
				Throw.InvalidCast();
			}
			return new AssemblyDefinitionHandle(handle.RowId);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000091A2 File Offset: 0x000073A2
		public static explicit operator AssemblyDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 536870912U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000091C3 File Offset: 0x000073C3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000091CE File Offset: 0x000073CE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000091D6 File Offset: 0x000073D6
		public static bool operator ==(AssemblyDefinitionHandle left, AssemblyDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000091E6 File Offset: 0x000073E6
		public override bool Equals(object obj)
		{
			return obj is AssemblyDefinitionHandle && ((AssemblyDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000091D6 File Offset: 0x000073D6
		public bool Equals(AssemblyDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009208 File Offset: 0x00007408
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009223 File Offset: 0x00007423
		public static bool operator !=(AssemblyDefinitionHandle left, AssemblyDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002F8 RID: 760
		private const uint tokenType = 536870912U;

		// Token: 0x040002F9 RID: 761
		private const byte tokenTypeSmall = 32;

		// Token: 0x040002FA RID: 762
		private readonly int _rowId;
	}
}
