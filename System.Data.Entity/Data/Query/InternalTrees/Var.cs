using System;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200010D RID: 269
	internal abstract class Var
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x0003D0D8 File Offset: 0x0003B2D8
		internal Var(int id, VarType varType, TypeUsage type)
		{
			this.m_id = id;
			this.m_varType = varType;
			this.m_type = type;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0003D0F5 File Offset: 0x0003B2F5
		internal int Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0003D0FD File Offset: 0x0003B2FD
		internal VarType VarType
		{
			get
			{
				return this.m_varType;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0003D105 File Offset: 0x0003B305
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003D10D File Offset: 0x0003B30D
		internal virtual bool TryGetName(out string name)
		{
			name = null;
			return false;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003D113 File Offset: 0x0003B313
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.Id
			});
		}

		// Token: 0x040009D2 RID: 2514
		private int m_id;

		// Token: 0x040009D3 RID: 2515
		private VarType m_varType;

		// Token: 0x040009D4 RID: 2516
		private TypeUsage m_type;
	}
}
