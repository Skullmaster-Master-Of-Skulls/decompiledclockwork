using System;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.ImageAssemblyAnalysis.PropertyModel
{
	// Token: 0x02000198 RID: 408
	internal sealed class BackgroundRepeat
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x0007A436 File Offset: 0x00078636
		internal BackgroundRepeat()
		{
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0007A440 File Offset: 0x00078640
		internal BackgroundRepeat(DeclarationNode declarationNode)
		{
			if (declarationNode == null)
			{
				throw new ArgumentNullException("declarationNode");
			}
			ExprNode exprNode = declarationNode.ExprNode;
			this.ParseTerm(exprNode.TermNode);
			exprNode.TermsWithOperators.ForEach(new Action<TermWithOperatorNode>(this.ParseTermWithOperator));
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0007A48B File Offset: 0x0007868B
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0007A493 File Offset: 0x00078693
		internal Repeat? RepeatValue { get; private set; }

		// Token: 0x06001505 RID: 5381 RVA: 0x0007A49C File Offset: 0x0007869C
		internal bool VerifyBackgroundNoRepeat()
		{
			return !(this.RepeatValue != Repeat.NoRepeat);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0007A4CC File Offset: 0x000786CC
		internal void ParseTerm(TermNode termNode)
		{
			if (string.IsNullOrWhiteSpace(termNode.StringBasedValue))
			{
				return;
			}
			string stringBasedValue;
			if ((stringBasedValue = termNode.StringBasedValue) != null)
			{
				if (stringBasedValue == "repeat")
				{
					this.RepeatValue = new Repeat?(Repeat.Repeat);
					return;
				}
				if (stringBasedValue == "no-repeat")
				{
					this.RepeatValue = new Repeat?(Repeat.NoRepeat);
					return;
				}
				if (stringBasedValue == "repeat-x")
				{
					this.RepeatValue = new Repeat?(Repeat.RepeatX);
					return;
				}
				if (!(stringBasedValue == "repeat-y"))
				{
					return;
				}
				this.RepeatValue = new Repeat?(Repeat.RepeatY);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0007A559 File Offset: 0x00078759
		internal void ParseTermWithOperator(TermWithOperatorNode termWithOperatorNode)
		{
			this.ParseTerm(termWithOperatorNode.TermNode);
		}
	}
}
