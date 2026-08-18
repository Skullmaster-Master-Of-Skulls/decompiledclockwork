using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000503 RID: 1283
	internal class SubExprOpcode : Opcode
	{
		// Token: 0x06003094 RID: 12436 RVA: 0x000BA40A File Offset: 0x000B860A
		internal SubExprOpcode(SubExpr expr) : base(OpcodeID.SubExpr)
		{
			this.expr = expr;
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000BA41A File Offset: 0x000B861A
		internal SubExpr Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000BA424 File Offset: 0x000B8624
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				SubExprOpcode subExprOpcode = op as SubExprOpcode;
				if (subExprOpcode != null)
				{
					return this.expr == subExprOpcode.expr;
				}
			}
			return false;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000BA454 File Offset: 0x000B8654
		internal override Opcode Eval(ProcessingContext context)
		{
			if (!context.LoadVariable(this.expr.Variable))
			{
				context.PushSequenceFrame();
				NodeSequence nodeSequence = context.CreateSequence();
				nodeSequence.Add(context.Processor.ContextNode);
				context.PushSequence(nodeSequence);
				int counterMarker = context.Processor.CounterMarker;
				try
				{
					this.expr.Eval(context);
				}
				catch (XPathNavigatorException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(this));
				}
				catch (NavigatorInvalidBodyAccessException ex2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(this));
				}
				context.Processor.CounterMarker = counterMarker;
				context.PopSequenceFrame();
				context.PopSequenceFrame();
				context.LoadVariable(this.expr.Variable);
			}
			return this.next;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000BA528 File Offset: 0x000B8728
		internal override Opcode EvalSpecial(ProcessingContext context)
		{
			try
			{
				this.expr.EvalSpecial(context);
			}
			catch (XPathNavigatorException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Process(this));
			}
			catch (NavigatorInvalidBodyAccessException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2.Process(this));
			}
			return this.next;
		}

		// Token: 0x0400260B RID: 9739
		protected SubExpr expr;
	}
}
