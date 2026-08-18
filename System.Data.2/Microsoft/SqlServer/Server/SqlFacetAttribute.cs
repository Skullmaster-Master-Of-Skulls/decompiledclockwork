using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000057 RID: 87
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public class SqlFacetAttribute : Attribute
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0004399C File Offset: 0x00042D9C
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x000439B0 File Offset: 0x00042DB0
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000439C4 File Offset: 0x00042DC4
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000439D8 File Offset: 0x00042DD8
		public int MaxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000439EC File Offset: 0x00042DEC
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00043A00 File Offset: 0x00042E00
		public int Precision
		{
			get
			{
				return this.m_Precision;
			}
			set
			{
				this.m_Precision = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00043A14 File Offset: 0x00042E14
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00043A28 File Offset: 0x00042E28
		public int Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00043A3C File Offset: 0x00042E3C
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00043A50 File Offset: 0x00042E50
		public bool IsNullable
		{
			get
			{
				return this.m_IsNullable;
			}
			set
			{
				this.m_IsNullable = value;
			}
		}

		// Token: 0x040001A3 RID: 419
		private bool m_IsFixedLength;

		// Token: 0x040001A4 RID: 420
		private int m_MaxSize;

		// Token: 0x040001A5 RID: 421
		private int m_Scale;

		// Token: 0x040001A6 RID: 422
		private int m_Precision;

		// Token: 0x040001A7 RID: 423
		private bool m_IsNullable;
	}
}
