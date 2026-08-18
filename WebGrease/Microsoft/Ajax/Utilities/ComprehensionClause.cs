using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200000E RID: 14
	public abstract class ComprehensionClause : AstNode
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000396E File Offset: 0x00001B6E
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003976 File Offset: 0x00001B76
		public Context OperatorContext { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000397F File Offset: 0x00001B7F
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003987 File Offset: 0x00001B87
		public Context OpenContext { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003990 File Offset: 0x00001B90
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003998 File Offset: 0x00001B98
		public Context CloseContext { get; set; }

		// Token: 0x06000125 RID: 293 RVA: 0x000039A1 File Offset: 0x00001BA1
		protected ComprehensionClause(Context context) : base(context)
		{
		}
	}
}
