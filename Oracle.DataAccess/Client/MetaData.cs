using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000DD RID: 221
	[SuppressUnmanagedCodeSecurity]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class MetaData
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x000503BB File Offset: 0x0004F3BB
		public unsafe MetaData(OpoMetValCtx* pOpoMetValCtx, bool addRowid)
		{
			this.m_addParam = true;
			if (!addRowid)
			{
				this.m_pOpoMetValCtx = pOpoMetValCtx;
				return;
			}
			this.m_pOpoMetValCtxWRowid = pOpoMetValCtx;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000503DC File Offset: 0x0004F3DC
		public MetaData()
		{
			this.m_addParam = true;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000503EC File Offset: 0x0004F3EC
		protected override void Finalize()
		{
			try
			{
				if (this.m_pOpoMetValCtx != null)
				{
					try
					{
						OpsMet.FreeValCtx(this.m_pOpoMetValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_pOpoMetValCtx = null;
				}
				if (this.m_pOpoMetValCtxWRowid != null)
				{
					try
					{
						OpsMet.FreeValCtx(this.m_pOpoMetValCtxWRowid);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pOpoMetValCtxWRowid = null;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040006DE RID: 1758
		internal unsafe OpoMetValCtx* m_pOpoMetValCtx;

		// Token: 0x040006DF RID: 1759
		internal unsafe OpoMetValCtx* m_pOpoMetValCtxWRowid;

		// Token: 0x040006E0 RID: 1760
		internal ColMetaRef[] m_colMetaRef;

		// Token: 0x040006E1 RID: 1761
		internal ColMetaRef[] m_colMetaRefWRowid;

		// Token: 0x040006E2 RID: 1762
		internal bool m_parsed;

		// Token: 0x040006E3 RID: 1763
		internal bool m_addParam;

		// Token: 0x040006E4 RID: 1764
		internal long m_rowSize;

		// Token: 0x040006E5 RID: 1765
		internal uint[] m_colOffset;

		// Token: 0x040006E6 RID: 1766
		internal uint[] m_colIndOffset;

		// Token: 0x040006E7 RID: 1767
		internal uint[] m_colLenOffset;

		// Token: 0x040006E8 RID: 1768
		internal OraType[] m_oraType;

		// Token: 0x040006E9 RID: 1769
		internal OracleDbType[] m_oracleDbType;

		// Token: 0x040006EA RID: 1770
		internal DotNetNumericAccessor[] m_dotNetNumericAccessor;

		// Token: 0x040006EB RID: 1771
		internal static Pooler m_connDataPooler = new Pooler(10, 50);
	}
}
