using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000099 RID: 153
	internal abstract class ColumnMap
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00035DB7 File Offset: 0x00033FB7
		internal ColumnMap(TypeUsage type, string name)
		{
			this.m_type = type;
			this.m_name = name;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00035DCD File Offset: 0x00033FCD
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00035DD5 File Offset: 0x00033FD5
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x00035DDD File Offset: 0x00033FDD
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00035DE6 File Offset: 0x00033FE6
		internal bool IsNamed
		{
			get
			{
				return this.m_name != null;
			}
		}

		// Token: 0x06000A03 RID: 2563
		[DebuggerNonUserCode]
		internal abstract void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg);

		// Token: 0x06000A04 RID: 2564
		[DebuggerNonUserCode]
		internal abstract TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg);

		// Token: 0x040008B0 RID: 2224
		private TypeUsage m_type;

		// Token: 0x040008B1 RID: 2225
		private string m_name;

		// Token: 0x040008B2 RID: 2226
		internal const string DefaultColumnName = "Value";
	}
}
