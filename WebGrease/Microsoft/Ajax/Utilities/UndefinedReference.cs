using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000A3 RID: 163
	public sealed class UndefinedReference
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000321BA File Offset: 0x000303BA
		public AstNode LookupNode
		{
			get
			{
				return this.m_lookup;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000321C2 File Offset: 0x000303C2
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000321CA File Offset: 0x000303CA
		public ReferenceType ReferenceType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000321D2 File Offset: 0x000303D2
		public int Column
		{
			get
			{
				if (this.m_context != null)
				{
					return this.m_context.StartColumn + 1;
				}
				return 0;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000321EB File Offset: 0x000303EB
		public int Line
		{
			get
			{
				if (this.m_context != null)
				{
					return this.m_context.StartLineNumber;
				}
				return 0;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00032202 File Offset: 0x00030402
		internal UndefinedReference(Lookup lookup, Context context)
		{
			this.m_lookup = lookup;
			this.m_name = lookup.Name;
			this.m_type = lookup.RefType;
			this.m_context = context;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00032230 File Offset: 0x00030430
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x040003EB RID: 1003
		private Context m_context;

		// Token: 0x040003EC RID: 1004
		private Lookup m_lookup;

		// Token: 0x040003ED RID: 1005
		private string m_name;

		// Token: 0x040003EE RID: 1006
		private ReferenceType m_type;
	}
}
