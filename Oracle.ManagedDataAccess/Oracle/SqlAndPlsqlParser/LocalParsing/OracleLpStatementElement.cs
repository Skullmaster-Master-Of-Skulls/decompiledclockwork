using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001C9 RID: 457
	public abstract class OracleLpStatementElement
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x000C3B90 File Offset: 0x000C1D90
		internal int Depth
		{
			get
			{
				if (this.m_vParent != null)
				{
					return this.m_vParent.Depth + 1;
				}
				return 0;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x000C3BAC File Offset: 0x000C1DAC
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x000C3BB4 File Offset: 0x000C1DB4
		internal OracleLpStatementElement Parent
		{
			get
			{
				return this.m_vParent;
			}
			set
			{
				this.m_vParent = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x000C3BC0 File Offset: 0x000C1DC0
		internal string DepthIndent
		{
			get
			{
				if (this.m_vDepthIndent == null)
				{
					this.m_vDepthIndent = new string(' ', 4 * this.Depth);
				}
				return this.m_vDepthIndent;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000C3BE8 File Offset: 0x000C1DE8
		internal OracleLpStatementElement TopElement
		{
			get
			{
				if (this.m_vTopElement == null)
				{
					OracleLpStatementElement oracleLpStatementElement = this;
					while (oracleLpStatementElement.m_vParent != null)
					{
						oracleLpStatementElement = oracleLpStatementElement.m_vParent;
					}
					this.m_vTopElement = oracleLpStatementElement;
				}
				return this.m_vTopElement;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001188 RID: 4488
		internal abstract OracleLpStatementElementType ElementType { get; }

		// Token: 0x06001189 RID: 4489 RVA: 0x000C3C20 File Offset: 0x000C1E20
		internal OracleLpStatementElement(OracleLpStatementElement parent)
		{
			this.m_vParent = parent;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000C3C30 File Offset: 0x000C1E30
		internal OracleLpQueryBlock GetParentQueryBlock()
		{
			for (OracleLpStatementElement oracleLpStatementElement = this.m_vParent; oracleLpStatementElement != null; oracleLpStatementElement = oracleLpStatementElement.Parent)
			{
				if (oracleLpStatementElement.ElementType == OracleLpStatementElementType.QueryBlock)
				{
					return oracleLpStatementElement as OracleLpQueryBlock;
				}
			}
			return null;
		}

		// Token: 0x0600118B RID: 4491
		internal abstract void ToString(StringBuilder sb);

		// Token: 0x040013EE RID: 5102
		protected OracleLpStatementElement m_vParent;

		// Token: 0x040013EF RID: 5103
		private string m_vDepthIndent;

		// Token: 0x040013F0 RID: 5104
		private OracleLpStatementElement m_vTopElement;
	}
}
