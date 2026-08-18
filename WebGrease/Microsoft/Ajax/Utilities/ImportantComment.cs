using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200009A RID: 154
	public class ImportantComment : AstNode
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002A370 File Offset: 0x00028570
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002A378 File Offset: 0x00028578
		public string Comment { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002A381 File Offset: 0x00028581
		public override bool IsDeclaration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002A384 File Offset: 0x00028584
		public ImportantComment(Context context) : base(context)
		{
			this.Comment = base.Context.Code;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002A39E File Offset: 0x0002859E
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
