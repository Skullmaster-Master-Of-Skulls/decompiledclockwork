using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000018 RID: 24
	public class ImportNode : ImportExportStatement
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000042A0 File Offset: 0x000024A0
		public ImportNode(Context context) : base(context)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000042A9 File Offset: 0x000024A9
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
