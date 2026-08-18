using System;
using System.Threading;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000035 RID: 53
	public abstract class ParserVisitor
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000073AD File Offset: 0x000055AD
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000073B5 File Offset: 0x000055B5
		public CancellationToken? CancelToken { get; set; }

		// Token: 0x060001F9 RID: 505 RVA: 0x000073C0 File Offset: 0x000055C0
		public virtual void VisitBlock(Block block)
		{
			this.VisitStartBlock(block);
			foreach (SyntaxTreeNode syntaxTreeNode in block.Children)
			{
				syntaxTreeNode.Accept(this);
			}
			this.VisitEndBlock(block);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000741C File Offset: 0x0000561C
		public virtual void VisitStartBlock(Block block)
		{
			this.ThrowIfCanceled();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007424 File Offset: 0x00005624
		public virtual void VisitSpan(Span span)
		{
			this.ThrowIfCanceled();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000742C File Offset: 0x0000562C
		public virtual void VisitEndBlock(Block block)
		{
			this.ThrowIfCanceled();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007434 File Offset: 0x00005634
		public virtual void VisitError(RazorError err)
		{
			this.ThrowIfCanceled();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000743C File Offset: 0x0000563C
		public virtual void OnComplete()
		{
			this.ThrowIfCanceled();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007444 File Offset: 0x00005644
		public virtual void ThrowIfCanceled()
		{
			if (this.CancelToken != null && this.CancelToken.Value.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
		}
	}
}
