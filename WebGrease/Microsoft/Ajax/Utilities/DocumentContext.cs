using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200008A RID: 138
	public class DocumentContext
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00025688 File Offset: 0x00023888
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00025690 File Offset: 0x00023890
		public string Source { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00025699 File Offset: 0x00023899
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x000256A1 File Offset: 0x000238A1
		public string FileContext { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000256AA File Offset: 0x000238AA
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x000256B2 File Offset: 0x000238B2
		public bool IsGenerated { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000256BB File Offset: 0x000238BB
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x000256C3 File Offset: 0x000238C3
		public JSParser Parser { get; set; }

		// Token: 0x06000857 RID: 2135 RVA: 0x000256CC File Offset: 0x000238CC
		public DocumentContext(string source)
		{
			this.Source = source;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000256DC File Offset: 0x000238DC
		public DocumentContext Clone()
		{
			return new DocumentContext(this.Source)
			{
				IsGenerated = this.IsGenerated,
				FileContext = this.FileContext,
				Parser = this.Parser,
				m_reportedVariables = this.m_reportedVariables
			};
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00025726 File Offset: 0x00023926
		internal void HandleError(ContextError error)
		{
			if (this.Parser != null)
			{
				this.Parser.OnCompilerError(error);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002573C File Offset: 0x0002393C
		internal void ReportUndefined(UndefinedReference referernce)
		{
			if (this.Parser != null)
			{
				this.Parser.OnUndefinedReference(referernce);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00025752 File Offset: 0x00023952
		internal bool HasAlreadySeenErrorFor(string varName)
		{
			if (this.m_reportedVariables == null)
			{
				this.m_reportedVariables = new Dictionary<string, string>();
			}
			else if (this.m_reportedVariables.ContainsKey(varName))
			{
				return true;
			}
			this.m_reportedVariables.Add(varName, varName);
			return false;
		}

		// Token: 0x04000314 RID: 788
		private Dictionary<string, string> m_reportedVariables;
	}
}
