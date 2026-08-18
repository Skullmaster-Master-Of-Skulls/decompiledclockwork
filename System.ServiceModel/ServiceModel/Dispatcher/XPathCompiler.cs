using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200050E RID: 1294
	internal class XPathCompiler
	{
		// Token: 0x06003156 RID: 12630 RVA: 0x000BE016 File Offset: 0x000BC216
		internal XPathCompiler(QueryCompilerFlags flags)
		{
			this.flags = flags;
			this.pushInitialContext = false;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000BE02C File Offset: 0x000BC22C
		private void SetPushInitialContext(bool pushInitial)
		{
			if (pushInitial)
			{
				this.pushInitialContext = pushInitial;
			}
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000BE038 File Offset: 0x000BC238
		internal virtual OpcodeBlock Compile(XPathExpr expr)
		{
			this.nestingLevel = 1;
			this.pushInitialContext = false;
			XPathCompiler.XPathExprCompiler xpathExprCompiler = new XPathCompiler.XPathExprCompiler(this);
			OpcodeBlock opcodeBlock = xpathExprCompiler.Compile(expr);
			if (this.pushInitialContext)
			{
				OpcodeBlock result = default(OpcodeBlock);
				result.Append(new PushContextNodeOpcode());
				result.Append(opcodeBlock);
				result.Append(new PopContextNodes());
				return result;
			}
			return opcodeBlock;
		}

		// Token: 0x04002646 RID: 9798
		private QueryCompilerFlags flags;

		// Token: 0x04002647 RID: 9799
		private int nestingLevel;

		// Token: 0x04002648 RID: 9800
		private bool pushInitialContext;

		// Token: 0x02000C4F RID: 3151
		internal struct XPathExprCompiler
		{
			// Token: 0x0600777D RID: 30589 RVA: 0x001BE102 File Offset: 0x001BC302
			internal XPathExprCompiler(XPathCompiler compiler)
			{
				this.compiler = compiler;
				this.codeBlock = default(OpcodeBlock);
			}

			// Token: 0x0600777E RID: 30590 RVA: 0x001BE117 File Offset: 0x001BC317
			private XPathExprCompiler(XPathCompiler.XPathExprCompiler xpathCompiler)
			{
				this.compiler = xpathCompiler.compiler;
				this.codeBlock = default(OpcodeBlock);
			}

			// Token: 0x0600777F RID: 30591 RVA: 0x001BE131 File Offset: 0x001BC331
			internal OpcodeBlock Compile(XPathExpr expr)
			{
				this.codeBlock = default(OpcodeBlock);
				this.CompileExpression(expr);
				return this.codeBlock;
			}

			// Token: 0x06007780 RID: 30592 RVA: 0x001BE14C File Offset: 0x001BC34C
			private OpcodeBlock CompileBlock(XPathExpr expr)
			{
				XPathCompiler.XPathExprCompiler xpathExprCompiler = new XPathCompiler.XPathExprCompiler(this);
				return xpathExprCompiler.Compile(expr);
			}

			// Token: 0x06007781 RID: 30593 RVA: 0x001BE170 File Offset: 0x001BC370
			private void CompileBoolean(XPathExpr expr, bool testValue)
			{
				if (this.compiler.nestingLevel == 1)
				{
					this.CompileBasicBoolean(expr, testValue);
					return;
				}
				OpcodeBlock block = default(OpcodeBlock);
				Opcode opcode = new BlockEndOpcode();
				block.Append(new PushBooleanOpcode(testValue));
				XPathExprList subExpr = expr.SubExpr;
				XPathExpr xpathExpr = subExpr[0];
				block.Append(this.CompileBlock(xpathExpr));
				if (xpathExpr.ReturnType != ValueDataType.Boolean)
				{
					block.Append(new TypecastOpcode(ValueDataType.Boolean));
				}
				block.Append(new ApplyBooleanOpcode(opcode, testValue));
				for (int i = 1; i < subExpr.Count; i++)
				{
					xpathExpr = subExpr[i];
					block.Append(new StartBooleanOpcode(testValue));
					block.Append(this.CompileBlock(xpathExpr));
					if (xpathExpr.ReturnType != ValueDataType.Boolean)
					{
						block.Append(new TypecastOpcode(ValueDataType.Boolean));
					}
					block.Append(new EndBooleanOpcode(opcode, testValue));
				}
				block.Append(opcode);
				this.codeBlock.Append(block);
			}

			// Token: 0x06007782 RID: 30594 RVA: 0x001BE264 File Offset: 0x001BC464
			private void CompileBasicBoolean(XPathExpr expr, bool testValue)
			{
				OpcodeBlock block = default(OpcodeBlock);
				Opcode opcode = new BlockEndOpcode();
				XPathExprList subExpr = expr.SubExpr;
				for (int i = 0; i < subExpr.Count; i++)
				{
					XPathExpr xpathExpr = subExpr[i];
					block.Append(this.CompileBlock(xpathExpr));
					if (xpathExpr.ReturnType != ValueDataType.Boolean)
					{
						block.Append(new TypecastOpcode(ValueDataType.Boolean));
					}
					if (i < subExpr.Count - 1)
					{
						block.Append(new JumpIfOpcode(opcode, testValue));
					}
				}
				block.Append(opcode);
				this.codeBlock.Append(block);
			}

			// Token: 0x06007783 RID: 30595 RVA: 0x001BE2F4 File Offset: 0x001BC4F4
			private void CompileExpression(XPathExpr expr)
			{
				switch (expr.Type)
				{
				case XPathExprType.Or:
					this.CompileBoolean(expr, false);
					break;
				case XPathExprType.And:
					this.CompileBoolean(expr, true);
					break;
				case XPathExprType.Relational:
					this.CompileRelational((XPathRelationExpr)expr);
					break;
				case XPathExprType.Union:
				{
					XPathConjunctExpr xpathConjunctExpr = (XPathConjunctExpr)expr;
					this.CompileExpression(xpathConjunctExpr.Left);
					this.CompileExpression(xpathConjunctExpr.Right);
					this.codeBlock.Append(new UnionOpcode());
					break;
				}
				case XPathExprType.LocationPath:
					if (expr.SubExprCount > 0)
					{
						this.CompileLocationPath(expr);
						this.codeBlock.Append(new PopSequenceToValueStackOpcode());
					}
					break;
				case XPathExprType.RelativePath:
					this.CompileRelativePath(expr, true);
					break;
				default:
					this.ThrowError(QueryCompileError.UnsupportedExpression);
					break;
				case XPathExprType.XsltVariable:
					this.CompileXsltVariable((XPathXsltVariableExpr)expr);
					break;
				case XPathExprType.String:
					this.codeBlock.Append(new PushStringOpcode(((XPathStringExpr)expr).String));
					break;
				case XPathExprType.Number:
				{
					XPathNumberExpr xpathNumberExpr = (XPathNumberExpr)expr;
					double num = xpathNumberExpr.Number;
					if (xpathNumberExpr.Negate)
					{
						xpathNumberExpr.Negate = false;
						num = -num;
					}
					this.codeBlock.Append(new PushNumberOpcode(num));
					break;
				}
				case XPathExprType.Function:
					this.CompileFunction((XPathFunctionExpr)expr);
					break;
				case XPathExprType.XsltFunction:
					this.CompileXsltFunction((XPathXsltFunctionExpr)expr);
					break;
				case XPathExprType.Math:
					this.CompileMath((XPathMathExpr)expr);
					break;
				case XPathExprType.Filter:
					this.CompileFilter(expr);
					if (expr.ReturnType == ValueDataType.Sequence)
					{
						this.codeBlock.Append(new PopSequenceToValueStackOpcode());
					}
					break;
				case XPathExprType.Path:
					this.CompilePath(expr);
					if (expr.SubExprCount == 0 && expr.ReturnType == ValueDataType.Sequence)
					{
						this.codeBlock.Append(new PopSequenceToValueStackOpcode());
					}
					break;
				}
				this.NegateIfRequired(expr);
			}

			// Token: 0x06007784 RID: 30596 RVA: 0x001BE4D0 File Offset: 0x001BC6D0
			private void CompileFilter(XPathExpr expr)
			{
				XPathExprList subExpr = expr.SubExpr;
				XPathExpr xpathExpr = subExpr[0];
				if (subExpr.Count > 1 && ValueDataType.Sequence != xpathExpr.ReturnType)
				{
					this.ThrowError(QueryCompileError.InvalidExpression);
				}
				this.CompileExpression(xpathExpr);
				if (xpathExpr.ReturnType == ValueDataType.Sequence)
				{
					if (!this.IsSpecialInternalFunction(xpathExpr) && expr.SubExprCount > 1)
					{
						this.codeBlock.Append(new MergeOpcode());
						this.codeBlock.Append(new PopSequenceToSequenceStackOpcode());
					}
					else if (this.IsSpecialInternalFunction(xpathExpr) && expr.SubExprCount > 1)
					{
						this.codeBlock.DetachLast();
					}
					this.compiler.nestingLevel++;
					if (this.compiler.nestingLevel > 3)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.PredicateNestingTooDeep));
					}
					for (int i = 1; i < expr.SubExprCount; i++)
					{
						this.CompilePredicate(subExpr[i]);
					}
					this.compiler.nestingLevel--;
				}
			}

			// Token: 0x06007785 RID: 30597 RVA: 0x001BE5D0 File Offset: 0x001BC7D0
			private bool IsSpecialInternalFunction(XPathExpr expr)
			{
				if (expr.Type != XPathExprType.XsltFunction)
				{
					return false;
				}
				XPathMessageFunction xpathMessageFunction = ((XPathXsltFunctionExpr)expr).Function as XPathMessageFunction;
				return xpathMessageFunction != null && xpathMessageFunction.ReturnType == XPathResultType.NodeSet && xpathMessageFunction.Maxargs == 0;
			}

			// Token: 0x06007786 RID: 30598 RVA: 0x001BE614 File Offset: 0x001BC814
			private void CompileFunction(XPathFunctionExpr expr)
			{
				if (this.CompileFunctionSpecial(expr))
				{
					return;
				}
				QueryFunction function = expr.Function;
				if (expr.SubExprCount > 0)
				{
					XPathExprList subExpr = expr.SubExpr;
					for (int i = subExpr.Count - 1; i >= 0; i--)
					{
						this.CompileFunctionParam(function, expr.SubExpr, i);
					}
				}
				this.codeBlock.Append(new FunctionCallOpcode(function));
				if (1 == this.compiler.nestingLevel && function.TestFlag(QueryFunctionFlag.UsesContextNode))
				{
					this.compiler.SetPushInitialContext(true);
				}
			}

			// Token: 0x06007787 RID: 30599 RVA: 0x001BE698 File Offset: 0x001BC898
			private void CompileFunctionParam(QueryFunction function, XPathExprList paramList, int index)
			{
				XPathExpr xpathExpr = paramList[index];
				this.CompileExpression(xpathExpr);
				if (function.ParamTypes[index] != ValueDataType.None && xpathExpr.ReturnType != function.ParamTypes[index])
				{
					if (function.ParamTypes[index] == ValueDataType.Sequence)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidTypeConversion));
					}
					this.CompileTypecast(function.ParamTypes[index]);
				}
			}

			// Token: 0x06007788 RID: 30600 RVA: 0x001BE6FC File Offset: 0x001BC8FC
			private bool CompileFunctionSpecial(XPathFunctionExpr expr)
			{
				XPathFunction xpathFunction = expr.Function as XPathFunction;
				if (xpathFunction != null && XPathFunctionID.StartsWith == xpathFunction.ID && XPathExprType.String == expr.SubExpr[1].Type)
				{
					this.CompileFunctionParam(xpathFunction, expr.SubExpr, 0);
					this.codeBlock.Append(new StringPrefixOpcode(((XPathStringExpr)expr.SubExpr[1]).String));
					return true;
				}
				return false;
			}

			// Token: 0x06007789 RID: 30601 RVA: 0x001BE770 File Offset: 0x001BC970
			private void CompileLiteralRelation(XPathRelationExpr expr)
			{
				XPathLiteralExpr xpathLiteralExpr = (XPathLiteralExpr)expr.Left;
				XPathLiteralExpr xpathLiteralExpr2 = (XPathLiteralExpr)expr.Right;
				bool literal = QueryValueModel.CompileTimeCompare(xpathLiteralExpr.Literal, xpathLiteralExpr2.Literal, expr.Op);
				this.codeBlock.Append(new PushBooleanOpcode(literal));
			}

			// Token: 0x0600778A RID: 30602 RVA: 0x001BE7C0 File Offset: 0x001BC9C0
			private void CompileLiteralOrdinal(XPathExpr expr)
			{
				int num = 0;
				try
				{
					XPathNumberExpr xpathNumberExpr = (XPathNumberExpr)expr;
					num = Convert.ToInt32(xpathNumberExpr.Number);
					if (xpathNumberExpr.Negate)
					{
						num = -num;
						xpathNumberExpr.Negate = false;
					}
					if (num < 1)
					{
						this.ThrowError(QueryCompileError.InvalidOrdinal);
					}
				}
				catch (OverflowException)
				{
					this.ThrowError(QueryCompileError.InvalidOrdinal);
				}
				if ((this.compiler.flags & QueryCompilerFlags.InverseQuery) != QueryCompilerFlags.None)
				{
					this.codeBlock.Append(new PushContextPositionOpcode());
					this.codeBlock.Append(new NumberEqualsOpcode((double)num));
					return;
				}
				this.codeBlock.Append(new LiteralOrdinalOpcode(num));
			}

			// Token: 0x0600778B RID: 30603 RVA: 0x001BE860 File Offset: 0x001BCA60
			private void CompileLocationPath(XPathExpr expr)
			{
				XPathStepExpr xpathStepExpr = (XPathStepExpr)expr.SubExpr[0];
				this.CompileSteps(expr.SubExpr);
				if (1 == this.compiler.nestingLevel)
				{
					this.compiler.SetPushInitialContext(xpathStepExpr.SelectDesc.Type != QueryNodeType.Root);
				}
			}

			// Token: 0x0600778C RID: 30604 RVA: 0x001BE8B8 File Offset: 0x001BCAB8
			private void CompileMath(XPathMathExpr mathExpr)
			{
				if (XPathExprType.Number == mathExpr.Right.Type && XPathExprType.Number == mathExpr.Left.Type)
				{
					double num = ((XPathNumberExpr)mathExpr.Left).Number;
					if (((XPathNumberExpr)mathExpr.Left).Negate)
					{
						((XPathNumberExpr)mathExpr.Left).Negate = false;
						num = -num;
					}
					double num2 = ((XPathNumberExpr)mathExpr.Right).Number;
					if (((XPathNumberExpr)mathExpr.Right).Negate)
					{
						((XPathNumberExpr)mathExpr.Right).Negate = false;
						num2 = -num2;
					}
					switch (mathExpr.Op)
					{
					case MathOperator.Plus:
						num += num2;
						break;
					case MathOperator.Minus:
						num -= num2;
						break;
					case MathOperator.Div:
						num /= num2;
						break;
					case MathOperator.Multiply:
						num *= num2;
						break;
					case MathOperator.Mod:
						num %= num2;
						break;
					}
					this.codeBlock.Append(new PushNumberOpcode(num));
					return;
				}
				this.CompileExpression(mathExpr.Right);
				if (ValueDataType.Double != mathExpr.Right.ReturnType)
				{
					this.CompileTypecast(ValueDataType.Double);
				}
				this.CompileExpression(mathExpr.Left);
				if (ValueDataType.Double != mathExpr.Left.ReturnType)
				{
					this.CompileTypecast(ValueDataType.Double);
				}
				this.codeBlock.Append(this.CreateMathOpcode(mathExpr.Op));
			}

			// Token: 0x0600778D RID: 30605 RVA: 0x001BEA04 File Offset: 0x001BCC04
			private void CompileNumberLiteralEquality(XPathRelationExpr expr)
			{
				bool flag = XPathExprType.Number == expr.Left.Type;
				bool flag2 = XPathExprType.Number == expr.Right.Type;
				this.CompileExpression(flag ? expr.Right : expr.Left);
				XPathNumberExpr xpathNumberExpr = flag ? ((XPathNumberExpr)expr.Left) : ((XPathNumberExpr)expr.Right);
				double num = xpathNumberExpr.Number;
				if (xpathNumberExpr.Negate)
				{
					xpathNumberExpr.Negate = false;
					num = -num;
				}
				this.codeBlock.Append(new NumberEqualsOpcode(num));
			}

			// Token: 0x0600778E RID: 30606 RVA: 0x001BEA90 File Offset: 0x001BCC90
			private void CompileNumberRelation(XPathRelationExpr expr)
			{
				if (expr.Op == RelationOperator.Eq)
				{
					this.CompileNumberLiteralEquality(expr);
					return;
				}
				bool flag = XPathExprType.Number == expr.Left.Type;
				bool flag2 = XPathExprType.Number == expr.Right.Type;
				this.CompileExpression(flag ? expr.Right : expr.Left);
				XPathNumberExpr xpathNumberExpr = flag ? ((XPathNumberExpr)expr.Left) : ((XPathNumberExpr)expr.Right);
				double num = xpathNumberExpr.Number;
				if (xpathNumberExpr.Negate)
				{
					xpathNumberExpr.Negate = false;
					num = -num;
				}
				if (flag)
				{
					switch (expr.Op)
					{
					case RelationOperator.Gt:
						expr.Op = RelationOperator.Lt;
						break;
					case RelationOperator.Ge:
						expr.Op = RelationOperator.Le;
						break;
					case RelationOperator.Lt:
						expr.Op = RelationOperator.Gt;
						break;
					case RelationOperator.Le:
						expr.Op = RelationOperator.Ge;
						break;
					}
				}
				if ((this.compiler.flags & QueryCompilerFlags.InverseQuery) != QueryCompilerFlags.None)
				{
					this.codeBlock.Append(new NumberIntervalOpcode(num, expr.Op));
					return;
				}
				this.codeBlock.Append(new NumberRelationOpcode(num, expr.Op));
			}

			// Token: 0x0600778F RID: 30607 RVA: 0x001BEBA0 File Offset: 0x001BCDA0
			private void CompilePath(XPathExpr expr)
			{
				if (expr.Type == XPathExprType.Filter)
				{
					this.CompileFilter(expr.SubExpr[0]);
				}
				else
				{
					this.CompileExpression(expr.SubExpr[0]);
					if (expr.SubExpr[0].ReturnType == ValueDataType.Sequence)
					{
						if (this.IsSpecialInternalFunction(expr.SubExpr[0]))
						{
							this.codeBlock.DetachLast();
						}
						else
						{
							this.codeBlock.Append(new MergeOpcode());
							this.codeBlock.Append(new PopSequenceToSequenceStackOpcode());
						}
					}
				}
				if (expr.SubExprCount == 2)
				{
					this.CompileRelativePath(expr.SubExpr[1], false);
					return;
				}
				if (expr.SubExprCount == 3)
				{
					XPathExpr xpathExpr = expr.SubExpr[1];
					XPathStepExpr xpathStepExpr = (XPathStepExpr)xpathExpr;
					if (!xpathStepExpr.SelectDesc.Axis.IsSupported())
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.UnsupportedAxis));
					}
					this.codeBlock.Append(new SelectOpcode(xpathStepExpr.SelectDesc));
					if (xpathStepExpr.SubExprCount > 0)
					{
						this.compiler.nestingLevel++;
						if (this.compiler.nestingLevel > 3)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.PredicateNestingTooDeep));
						}
						this.CompilePredicates(xpathStepExpr.SubExpr);
						this.compiler.nestingLevel--;
					}
					this.CompileRelativePath(expr.SubExpr[2], false);
				}
			}

			// Token: 0x06007790 RID: 30608 RVA: 0x001BED1C File Offset: 0x001BCF1C
			private void CompilePredicate(XPathExpr expr)
			{
				if (expr.IsLiteral && XPathExprType.Number == expr.Type)
				{
					this.CompileLiteralOrdinal(expr);
				}
				else
				{
					this.CompileExpression(expr);
					if (expr.ReturnType == ValueDataType.Double)
					{
						this.codeBlock.Append(new OrdinalOpcode());
					}
					else if (expr.ReturnType != ValueDataType.Boolean)
					{
						this.CompileTypecast(ValueDataType.Boolean);
					}
				}
				this.codeBlock.Append(new ApplyFilterOpcode());
			}

			// Token: 0x06007791 RID: 30609 RVA: 0x001BED88 File Offset: 0x001BCF88
			private void CompilePredicates(XPathExprList exprList)
			{
				for (int i = 0; i < exprList.Count; i++)
				{
					this.CompilePredicate(exprList[i]);
				}
			}

			// Token: 0x06007792 RID: 30610 RVA: 0x001BEDB4 File Offset: 0x001BCFB4
			private void CompileRelational(XPathRelationExpr expr)
			{
				if (expr.Left.IsLiteral && expr.Right.IsLiteral)
				{
					this.CompileLiteralRelation(expr);
					return;
				}
				if (expr.Op != RelationOperator.Ne)
				{
					if (XPathExprType.Number == expr.Left.Type || XPathExprType.Number == expr.Right.Type)
					{
						this.CompileNumberRelation(expr);
						return;
					}
					if (expr.Op == RelationOperator.Eq && (XPathExprType.String == expr.Left.Type || XPathExprType.String == expr.Right.Type))
					{
						this.CompileStringLiteralEquality(expr);
						return;
					}
				}
				this.CompileExpression(expr.Left);
				this.CompileExpression(expr.Right);
				this.codeBlock.Append(new RelationOpcode(expr.Op));
			}

			// Token: 0x06007793 RID: 30611 RVA: 0x001BEE6F File Offset: 0x001BD06F
			private void CompileRelativePath(XPathExpr expr, bool start)
			{
				this.CompileSteps(expr.SubExpr, start);
				this.codeBlock.Append(new PopSequenceToValueStackOpcode());
			}

			// Token: 0x06007794 RID: 30612 RVA: 0x001BEE90 File Offset: 0x001BD090
			private void CompileStringLiteralEquality(XPathRelationExpr expr)
			{
				bool flag = XPathExprType.String == expr.Left.Type;
				bool flag2 = XPathExprType.String == expr.Right.Type;
				this.CompileExpression(flag ? expr.Right : expr.Left);
				string literal = flag ? ((XPathStringExpr)expr.Left).String : ((XPathStringExpr)expr.Right).String;
				this.codeBlock.Append(new StringEqualsOpcode(literal));
			}

			// Token: 0x06007795 RID: 30613 RVA: 0x001BEF0B File Offset: 0x001BD10B
			private void CompileSteps(XPathExprList steps)
			{
				this.CompileSteps(steps, true);
			}

			// Token: 0x06007796 RID: 30614 RVA: 0x001BEF18 File Offset: 0x001BD118
			private void CompileSteps(XPathExprList steps, bool start)
			{
				for (int i = 0; i < steps.Count; i++)
				{
					XPathStepExpr xpathStepExpr = (XPathStepExpr)steps[i];
					if (!xpathStepExpr.SelectDesc.Axis.IsSupported())
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.UnsupportedAxis));
					}
					Opcode opcode;
					if (start && i == 0)
					{
						if (QueryNodeType.Root == xpathStepExpr.SelectDesc.Type)
						{
							opcode = new SelectRootOpcode();
						}
						else
						{
							opcode = new InitialSelectOpcode(xpathStepExpr.SelectDesc);
						}
					}
					else
					{
						opcode = new SelectOpcode(xpathStepExpr.SelectDesc);
					}
					this.codeBlock.Append(opcode);
					if (xpathStepExpr.SubExprCount > 0)
					{
						this.compiler.nestingLevel++;
						if (this.compiler.nestingLevel > 3)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.PredicateNestingTooDeep));
						}
						this.CompilePredicates(xpathStepExpr.SubExpr);
						this.compiler.nestingLevel--;
					}
				}
			}

			// Token: 0x06007797 RID: 30615 RVA: 0x001BF00E File Offset: 0x001BD20E
			private void CompileTypecast(ValueDataType destType)
			{
				this.codeBlock.Append(new TypecastOpcode(destType));
			}

			// Token: 0x06007798 RID: 30616 RVA: 0x001BF024 File Offset: 0x001BD224
			private void CompileXsltFunction(XPathXsltFunctionExpr expr)
			{
				if (expr.SubExprCount > 0)
				{
					XPathExprList subExpr = expr.SubExpr;
					for (int i = subExpr.Count - 1; i >= 0; i--)
					{
						XPathExpr xpathExpr = subExpr[i];
						this.CompileExpression(xpathExpr);
						ValueDataType valueDataType = XPathXsltFunctionExpr.ConvertTypeFromXslt(expr.Function.ArgTypes[i]);
						if (valueDataType != ValueDataType.None && xpathExpr.ReturnType != valueDataType)
						{
							this.CompileTypecast(valueDataType);
						}
					}
				}
				if (expr.Function is XPathMessageFunction)
				{
					this.codeBlock.Append(new XPathMessageFunctionCallOpcode((XPathMessageFunction)expr.Function, expr.SubExprCount));
					if (this.IsSpecialInternalFunction(expr))
					{
						this.codeBlock.Append(new PopSequenceToValueStackOpcode());
						return;
					}
				}
				else
				{
					this.codeBlock.Append(new XsltFunctionCallOpcode(expr.Context, expr.Function, expr.SubExprCount));
				}
			}

			// Token: 0x06007799 RID: 30617 RVA: 0x001BF0F3 File Offset: 0x001BD2F3
			private void CompileXsltVariable(XPathXsltVariableExpr expr)
			{
				this.codeBlock.Append(new PushXsltVariableOpcode(expr.Context, expr.Variable));
			}

			// Token: 0x0600779A RID: 30618 RVA: 0x001BF114 File Offset: 0x001BD314
			private MathOpcode CreateMathOpcode(MathOperator op)
			{
				MathOpcode result = null;
				switch (op)
				{
				case MathOperator.Plus:
					result = new PlusOpcode();
					break;
				case MathOperator.Minus:
					result = new MinusOpcode();
					break;
				case MathOperator.Div:
					result = new DivideOpcode();
					break;
				case MathOperator.Multiply:
					result = new MultiplyOpcode();
					break;
				case MathOperator.Mod:
					result = new ModulusOpcode();
					break;
				case MathOperator.Negate:
					result = new NegateOpcode();
					break;
				}
				return result;
			}

			// Token: 0x0600779B RID: 30619 RVA: 0x001BF176 File Offset: 0x001BD376
			private void NegateIfRequired(XPathExpr expr)
			{
				this.TypecastIfRequired(expr);
				if (expr.Negate)
				{
					expr.Negate = false;
					this.codeBlock.Append(new NegateOpcode());
				}
			}

			// Token: 0x0600779C RID: 30620 RVA: 0x001BF19E File Offset: 0x001BD39E
			private void TypecastIfRequired(XPathExpr expr)
			{
				if (expr.TypecastRequired)
				{
					expr.TypecastRequired = false;
					this.CompileTypecast(expr.ReturnType);
				}
			}

			// Token: 0x0600779D RID: 30621 RVA: 0x001BF1BB File Offset: 0x001BD3BB
			private void ThrowError(QueryCompileError error)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(error));
			}

			// Token: 0x04004469 RID: 17513
			private OpcodeBlock codeBlock;

			// Token: 0x0400446A RID: 17514
			private XPathCompiler compiler;
		}
	}
}
