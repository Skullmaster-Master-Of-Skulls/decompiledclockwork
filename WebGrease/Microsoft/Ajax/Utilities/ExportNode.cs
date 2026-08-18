using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000016 RID: 22
	public class ExportNode : ImportExportStatement
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00004159 File Offset: 0x00002359
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00004161 File Offset: 0x00002361
		public bool IsDefault { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000416A File Offset: 0x0000236A
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00004172 File Offset: 0x00002372
		public Context DefaultContext { get; set; }

		// Token: 0x0600017D RID: 381 RVA: 0x0000417B File Offset: 0x0000237B
		public ExportNode(Context context) : base(context)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004184 File Offset: 0x00002384
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
