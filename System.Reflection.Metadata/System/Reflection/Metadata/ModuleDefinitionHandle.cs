using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000058 RID: 88
	public struct ModuleDefinitionHandle : IEquatable<ModuleDefinitionHandle>
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00009075 File Offset: 0x00007275
		internal ModuleDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000907E File Offset: 0x0000727E
		internal static ModuleDefinitionHandle FromRowId(int rowId)
		{
			return new ModuleDefinitionHandle(rowId);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009086 File Offset: 0x00007286
		public static implicit operator Handle(ModuleDefinitionHandle handle)
		{
			return new Handle(0, handle._rowId);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009094 File Offset: 0x00007294
		public static implicit operator EntityHandle(ModuleDefinitionHandle handle)
		{
			return new EntityHandle((uint)(0L | (long)handle._rowId));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000090A6 File Offset: 0x000072A6
		public static explicit operator ModuleDefinitionHandle(Handle handle)
		{
			if (handle.VType != 0)
			{
				Throw.InvalidCast();
			}
			return new ModuleDefinitionHandle(handle.RowId);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000090C2 File Offset: 0x000072C2
		public static explicit operator ModuleDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 0U)
			{
				Throw.InvalidCast();
			}
			return new ModuleDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000090DE File Offset: 0x000072DE
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000090E9 File Offset: 0x000072E9
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000090F1 File Offset: 0x000072F1
		public static bool operator ==(ModuleDefinitionHandle left, ModuleDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00009101 File Offset: 0x00007301
		public override bool Equals(object obj)
		{
			return obj is ModuleDefinitionHandle && ((ModuleDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000090F1 File Offset: 0x000072F1
		public bool Equals(ModuleDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00009120 File Offset: 0x00007320
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000913B File Offset: 0x0000733B
		public static bool operator !=(ModuleDefinitionHandle left, ModuleDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002F5 RID: 757
		private const uint tokenType = 0U;

		// Token: 0x040002F6 RID: 758
		private const byte tokenTypeSmall = 0;

		// Token: 0x040002F7 RID: 759
		private readonly int _rowId;
	}
}
