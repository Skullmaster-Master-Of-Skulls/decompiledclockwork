using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B1 RID: 177
	public class LexicalDeclaration : Declaration
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00036D8D File Offset: 0x00034F8D
		public LexicalDeclaration(Context context) : base(context)
		{
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00036D96 File Offset: 0x00034F96
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
