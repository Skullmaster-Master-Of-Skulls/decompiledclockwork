using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000229 RID: 553
	internal sealed class SpanDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x0600141C RID: 5148 RVA: 0x0004405C File Offset: 0x0004225C
		internal SpanDebugInfoExpression(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn) : base(document)
		{
			this._startLine = startLine;
			this._startColumn = startColumn;
			this._endLine = endLine;
			this._endColumn = endColumn;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00044083 File Offset: 0x00042283
		public override int StartLine
		{
			get
			{
				return this._startLine;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0004408B File Offset: 0x0004228B
		public override int StartColumn
		{
			get
			{
				return this._startColumn;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00044093 File Offset: 0x00042293
		public override int EndLine
		{
			get
			{
				return this._endLine;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0004409B File Offset: 0x0004229B
		public override int EndColumn
		{
			get
			{
				return this._endColumn;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x000440A3 File Offset: 0x000422A3
		public override bool IsClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000440A6 File Offset: 0x000422A6
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x04000982 RID: 2434
		private readonly int _startLine;

		// Token: 0x04000983 RID: 2435
		private readonly int _startColumn;

		// Token: 0x04000984 RID: 2436
		private readonly int _endLine;

		// Token: 0x04000985 RID: 2437
		private readonly int _endColumn;
	}
}
