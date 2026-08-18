using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BC RID: 188
	public class OutputVisitor : IVisitor
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00037E30 File Offset: 0x00036030
		private OutputVisitor(TextWriter writer, CodeSettings settings)
		{
			this.m_outputStream = writer;
			this.m_settings = (settings ?? new CodeSettings());
			this.m_onNewLine = true;
			this.m_requiresSeparator = new RequiresSeparatorVisitor(this.m_settings);
			this.m_hasReplacementTokens = (settings.ReplacementTokens.Count > 0);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00037ED0 File Offset: 0x000360D0
		public static void Apply(TextWriter writer, AstNode node, CodeSettings settings)
		{
			if (node != null)
			{
				OutputVisitor outputVisitor = new OutputVisitor(writer, settings);
				node.Accept(outputVisitor);
				settings.IfNotNull(delegate(CodeSettings s)
				{
					s.SymbolsMap.IfNotNull(delegate(ISourceMap m)
					{
						m.EndOutputRun(outputVisitor.m_lineCount, outputVisitor.m_lineLength);
					});
				});
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00037F14 File Offset: 0x00036114
		public static string Apply(AstNode node, CodeSettings settings)
		{
			if (node != null)
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					OutputVisitor.Apply(stringWriter, node, settings);
					return stringWriter.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00037F6C File Offset: 0x0003616C
		public void Visit(ArrayLiteral node)
		{
			bool noIn = this.m_noIn;
			this.m_noIn = false;
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.OutputPossibleLineBreak('[');
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Elements.Count > 0)
				{
					this.Indent();
					AstNode astNode = null;
					for (int i = 0; i < node.Elements.Count; i++)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							this.MarkSegment(node, null, astNode.IfNotNull((AstNode e) => e.TerminatingContext));
							if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
						astNode = node.Elements[i];
						if (astNode != null)
						{
							this.AcceptNodeWithParens(astNode, astNode.Precedence == OperatorPrecedence.Comma);
						}
					}
					this.Unindent();
				}
				this.Output(']');
				this.MarkSegment(node, null, node.Context);
				this.EndSymbol(symbol);
			}
			this.m_noIn = noIn;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00038084 File Offset: 0x00036284
		public void Visit(AspNetBlockNode node)
		{
			if (node != null)
			{
				this.Output(node.AspNetBlockText);
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000380C0 File Offset: 0x000362C0
		public void Visit(AstNodeList node)
		{
			if (node != null && node.Count > 0)
			{
				object symbol = this.StartSymbol(node);
				bool flag = node.Parent is CommaOperator && node.Parent.Parent is Block && this.m_settings.OutputMode == OutputMode.MultipleLines;
				node[0].Accept(this);
				OutputVisitor.SetContextOutputPosition(node.Context, node[0].Context);
				this.m_startOfStatement = false;
				if (!flag)
				{
					this.Indent();
				}
				for (int i = 1; i < node.Count; i++)
				{
					this.OutputPossibleLineBreak(',');
					this.MarkSegment(node, null, node[i - 1].IfNotNull((AstNode n) => n.TerminatingContext));
					if (flag)
					{
						this.NewLine();
					}
					else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.OutputPossibleLineBreak(' ');
					}
					node[i].Accept(this);
				}
				if (!flag)
				{
					this.Unindent();
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000381E0 File Offset: 0x000363E0
		public void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (node.OperatorToken == JSToken.Comma)
				{
					if (node.Operand1 != null)
					{
						node.Operand1.Accept(this);
						OutputVisitor.SetContextOutputPosition(node.Context, node.Operand1.Context);
						if (node.Operand2 != null)
						{
							this.OutputPossibleLineBreak(',');
							this.MarkSegment(node, null, node.Operand1.TerminatingContext);
							this.m_startOfStatement = false;
							if (node.Parent is Block)
							{
								this.NewLine();
							}
							else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
					}
					if (node.Operand2 != null)
					{
						node.Operand2.Accept(this);
						this.m_startOfStatement = false;
						return;
					}
				}
				else
				{
					OperatorPrecedence precedence = node.Precedence;
					bool noIn = this.m_noIn;
					if (noIn)
					{
						if (node.OperatorToken == JSToken.In)
						{
							this.OutputPossibleLineBreak('(');
							this.m_noIn = false;
						}
						else
						{
							this.m_noIn = (precedence <= OperatorPrecedence.Relational);
						}
					}
					if (node.Operand1 != null)
					{
						this.AcceptNodeWithParens(node.Operand1, node.Operand1.Precedence < precedence);
						OutputVisitor.SetContextOutputPosition(node.Context, node.Operand1.Context);
					}
					this.m_startOfStatement = false;
					if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						if (node.OperatorToken != JSToken.Comma)
						{
							this.OutputPossibleLineBreak(' ');
						}
						this.Output(OutputVisitor.OperatorString(node.OperatorToken));
						this.MarkSegment(node, null, node.OperatorContext);
						this.BreakLine(false);
						if (!this.m_onNewLine)
						{
							this.OutputPossibleLineBreak(' ');
						}
					}
					else
					{
						this.Output(OutputVisitor.OperatorString(node.OperatorToken));
						this.MarkSegment(node, null, node.OperatorContext);
						this.BreakLine(false);
					}
					if (node.OperatorToken == JSToken.Divide)
					{
						this.m_addSpaceIfTrue = ((char c) => c == '/');
					}
					if (node.Operand2 != null)
					{
						OperatorPrecedence precedence2 = node.Operand2.Precedence;
						bool needsParens = precedence2 < precedence;
						BinaryOperator binaryOperator = node.Operand2 as BinaryOperator;
						if (binaryOperator != null)
						{
							if (precedence == precedence2 && precedence != OperatorPrecedence.Assignment && precedence != OperatorPrecedence.Comma)
							{
								if (node.OperatorToken == binaryOperator.OperatorToken)
								{
									JSToken operatorToken = node.OperatorToken;
									switch (operatorToken)
									{
									case JSToken.Multiply:
									case JSToken.BitwiseAnd:
									case JSToken.BitwiseOr:
									case JSToken.BitwiseXor:
										break;
									case JSToken.Divide:
									case JSToken.Modulo:
										goto IL_269;
									default:
										switch (operatorToken)
										{
										case JSToken.LogicalAnd:
										case JSToken.LogicalOr:
											break;
										default:
											goto IL_269;
										}
										break;
									}
									needsParens = false;
									goto IL_279;
									IL_269:
									needsParens = true;
								}
								else
								{
									needsParens = true;
								}
							}
							else
							{
								needsParens = (precedence2 < precedence);
							}
						}
						IL_279:
						this.m_noIn = (noIn && precedence <= OperatorPrecedence.Relational);
						this.AcceptNodeWithParens(node.Operand2, needsParens);
					}
					if (noIn && node.OperatorToken == JSToken.In)
					{
						this.OutputPossibleLineBreak(')');
					}
					this.m_noIn = noIn;
					this.EndSymbol(symbol);
				}
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000384BC File Offset: 0x000366BC
		public void Visit(BindingIdentifier node)
		{
			if (node != null)
			{
				this.Output((node.VariableField != null) ? node.VariableField.ToString() : node.Name);
				this.MarkSegment(node, node.Name, node.Context);
				this.SetContextOutputPosition(node.Context);
				node.VariableField.IfNotNull(delegate(JSVariableField f)
				{
					this.SetContextOutputPosition(f.OriginalContext);
				});
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00038534 File Offset: 0x00036734
		public void Visit(Block node)
		{
			if (node != null)
			{
				object obj = (node.Parent != null) ? this.StartSymbol(node) : null;
				bool flag = true;
				if (node.Parent != null)
				{
					AstNode parent = node.Parent;
					ModuleDeclaration moduleDeclaration;
					if (parent is FunctionObject && parent.EnclosingScope.UseStrict && !parent.EnclosingScope.Parent.UseStrict)
					{
						this.m_needsStrictDirective = true;
					}
					else if ((moduleDeclaration = (node.Parent as ModuleDeclaration)) != null && moduleDeclaration.IsImplicit)
					{
						this.m_needsStrictDirective = false;
						flag = false;
					}
					if (flag)
					{
						this.OutputPossibleLineBreak('{');
						this.SetContextOutputPosition(node.Context);
						this.MarkSegment(node, null, node.Context);
						this.Indent();
					}
				}
				else
				{
					this.m_needsStrictDirective = (node.EnclosingScope.IfNotNull((ActivationObject s) => s.UseStrict) && !this.m_doneWithGlobalDirectives);
					flag = false;
				}
				AstNode astNode = null;
				for (int i = 0; i < node.Count; i++)
				{
					AstNode astNode2 = node[i];
					if (astNode2 != null)
					{
						if (astNode != null && this.m_requiresSeparator.Query(astNode))
						{
							this.OutputPossibleLineBreak(';');
							this.MarkSegment(astNode, null, astNode.TerminatingContext);
						}
						if (!(astNode2 is DirectivePrologue))
						{
							if (this.m_needsStrictDirective)
							{
								this.Output("\"use strict\";");
								this.m_needsStrictDirective = false;
							}
							this.m_doneWithGlobalDirectives = true;
						}
						this.NewLine();
						this.m_startOfStatement = true;
						astNode2.Accept(this);
						astNode = astNode2;
					}
				}
				if (flag)
				{
					this.Unindent();
					if (node.Count > 0)
					{
						this.NewLine();
					}
					this.OutputPossibleLineBreak('}');
					this.MarkSegment(node, null, node.Context);
				}
				else if (astNode != null && this.m_requiresSeparator.Query(astNode) && this.m_settings.TermSemicolons)
				{
					this.OutputPossibleLineBreak(';');
					this.MarkSegment(astNode, null, astNode.TerminatingContext);
				}
				if (obj != null)
				{
					this.EndSymbol(obj);
				}
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00038748 File Offset: 0x00036948
		public void Visit(Break node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("break");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (!node.Label.IsNullOrWhiteSpace())
				{
					this.m_noLineBreaks = true;
					if (node.LabelInfo.IfNotNull((LabelInfo li) => !li.MinLabel.IsNullOrWhiteSpace()))
					{
						this.Output(node.LabelInfo.MinLabel);
					}
					else
					{
						this.Output(node.Label);
					}
					this.MarkSegment(node, null, node.LabelContext);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0003880C File Offset: 0x00036A0C
		public void Visit(CallNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				if (node.IsConstructor)
				{
					this.Output("new");
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
					this.m_startOfStatement = false;
				}
				if (node.Function != null)
				{
					bool flag = node.Function.Precedence < node.Precedence;
					if (!flag)
					{
						if (node.IsConstructor)
						{
							flag = NewParensVisitor.NeedsParens(node.Function, node.Arguments == null || node.Arguments.Count == 0);
						}
						else
						{
							CallNode callNode = node.Function as CallNode;
							if (callNode != null && callNode.IsConstructor && (callNode.Arguments == null || callNode.Arguments.Count == 0))
							{
								flag = true;
							}
						}
					}
					this.AcceptNodeWithParens(node.Function, flag);
					if (!node.IsConstructor)
					{
						this.SetContextOutputPosition(node.Context);
					}
				}
				if (!node.IsConstructor || node.Arguments.Count > 0)
				{
					this.OutputPossibleLineBreak(node.InBrackets ? '[' : '(');
					this.MarkSegment(node, null, node.Arguments.Context);
					AstNode astNode = null;
					for (int i = 0; i < node.Arguments.Count; i++)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							this.MarkSegment(node.Arguments, null, astNode.IfNotNull((AstNode a) => a.TerminatingContext) ?? node.Arguments.Context);
							if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
						astNode = node.Arguments[i];
						if (astNode != null)
						{
							this.AcceptNodeWithParens(astNode, astNode.Precedence <= OperatorPrecedence.Comma);
						}
					}
					this.Output(node.InBrackets ? ']' : ')');
					this.MarkSegment(node, null, node.Arguments.Context);
				}
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00038A5C File Offset: 0x00036C5C
		public void Visit(ClassNode node)
		{
			if (node != null)
			{
				bool flag = this.m_startOfStatement && node.ClassType != ClassType.Declaration;
				if (flag)
				{
					this.OutputPossibleLineBreak('(');
					this.m_startOfStatement = false;
				}
				this.Output("class");
				this.MarkSegment(node, null, node.ClassContext);
				this.SetContextOutputPosition(node.ClassContext);
				this.m_startOfStatement = false;
				if (node.Binding != null)
				{
					BindingIdentifier bindingIdentifier = node.Binding as BindingIdentifier;
					if (bindingIdentifier != null)
					{
						if (node.ClassType != ClassType.Expression || bindingIdentifier.VariableField.IsReferenced || !this.m_settings.RemoveFunctionExpressionNames)
						{
							node.Binding.Accept(this);
						}
					}
					else
					{
						node.Binding.Accept(this);
					}
				}
				if (node.Heritage != null)
				{
					this.Output("extends");
					this.MarkSegment(node, null, node.ExtendsContext);
					this.SetContextOutputPosition(node.ExtendsContext);
					node.Heritage.Accept(this);
				}
				node.Elements.IfNotNull(delegate(AstNodeList e)
				{
					if (e.Count > 0)
					{
						this.NewLine();
						this.Indent();
					}
				});
				this.OutputPossibleLineBreak('{');
				this.MarkSegment(node, null, node.OpenBrace);
				this.SetContextOutputPosition(node.OpenBrace);
				if (node.Elements != null && node.Elements.Count > 0)
				{
					foreach (AstNode astNode in node.Elements)
					{
						this.NewLine();
						astNode.Accept(this);
					}
				}
				node.Elements.IfNotNull(delegate(AstNodeList e)
				{
					if (e.Count > 0)
					{
						this.Unindent();
						this.NewLine();
					}
				});
				this.OutputPossibleLineBreak('}');
				this.MarkSegment(node, null, node.CloseBrace);
				this.SetContextOutputPosition(node.CloseBrace);
				if (flag)
				{
					this.OutputPossibleLineBreak(')');
				}
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00038C44 File Offset: 0x00036E44
		public void Visit(ComprehensionNode node)
		{
			if (node != null)
			{
				this.OutputPossibleLineBreak((node.ComprehensionType == ComprehensionType.Generator) ? '(' : '[');
				this.MarkSegment(node, null, node.OpenDelimiter);
				if (node.MozillaOrdering)
				{
					if (node.Expression != null)
					{
						node.Expression.Accept(this);
					}
					using (IEnumerator<AstNode> enumerator = node.Clauses.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AstNode astNode = enumerator.Current;
							astNode.Accept(this);
						}
						goto IL_B9;
					}
				}
				foreach (AstNode astNode2 in node.Clauses)
				{
					astNode2.Accept(this);
				}
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				IL_B9:
				this.OutputPossibleLineBreak((node.ComprehensionType == ComprehensionType.Generator) ? ')' : ']');
				this.MarkSegment(node, null, node.CloseDelimiter);
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00038D4C File Offset: 0x00036F4C
		public void Visit(ComprehensionForClause node)
		{
			if (node != null)
			{
				this.Output("for");
				this.MarkSegment(node, null, node.OperatorContext);
				this.OutputPossibleLineBreak('(');
				this.MarkSegment(node, null, node.OpenContext);
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				this.Output(node.IsInOperation ? "in" : "of");
				this.MarkSegment(node, null, node.OfContext);
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.MarkSegment(node, null, node.CloseContext);
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00038DF4 File Offset: 0x00036FF4
		public void Visit(ComprehensionIfClause node)
		{
			if (node != null)
			{
				this.Output("if");
				this.MarkSegment(node, null, node.OperatorContext);
				this.OutputPossibleLineBreak('(');
				this.MarkSegment(node, null, node.OpenContext);
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.MarkSegment(node, null, node.CloseContext);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00038E60 File Offset: 0x00037060
		public void Visit(ConditionalCompilationComment node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				int num = 0;
				if (this.m_outputCCOn && this.m_settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryCCOnStatements))
				{
					while (num < node.Statements.Count && node.Statements[num] is ConditionalCompilationOn)
					{
						num++;
					}
				}
				if (num < node.Statements.Count)
				{
					this.Output("/*");
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
					AstNode astNode = node.Statements[num];
					if (astNode is ConditionalCompilationStatement || astNode is ConstantWrapperPP)
					{
						astNode.Accept(this);
					}
					else
					{
						this.OutputPossibleLineBreak('@');
						astNode.Accept(this);
					}
					AstNode astNode2 = astNode;
					while (++num < node.Statements.Count)
					{
						astNode = node.Statements[num];
						if (astNode != null)
						{
							if (astNode2 != null && this.m_requiresSeparator.Query(astNode2))
							{
								this.OutputPossibleLineBreak(';');
								this.MarkSegment(astNode2, null, astNode2.TerminatingContext);
							}
							this.NewLine();
							this.m_startOfStatement = true;
							astNode.Accept(this);
							astNode2 = astNode;
						}
					}
					this.Output("@*/");
					this.MarkSegment(node, null, node.Context);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00038FB0 File Offset: 0x000371B0
		public void Visit(ConditionalCompilationElse node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("@else");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00038FF4 File Offset: 0x000371F4
		public void Visit(ConditionalCompilationElseIf node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("@elif(");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0003905C File Offset: 0x0003725C
		public void Visit(ConditionalCompilationEnd node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("@end");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000390A0 File Offset: 0x000372A0
		public void Visit(ConditionalCompilationIf node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("@if(");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00039108 File Offset: 0x00037308
		public void Visit(ConditionalCompilationOn node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (!this.m_outputCCOn || !this.m_settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryCCOnStatements))
				{
					this.m_outputCCOn = true;
					this.Output("@cc_on");
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00039170 File Offset: 0x00037370
		public void Visit(ConditionalCompilationSet node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("@set");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Output(node.VariableName);
				this.Output('=');
				if (node.Value is BinaryOperator || node.Value is UnaryOperator)
				{
					this.Output('(');
					node.Value.Accept(this);
					this.OutputPossibleLineBreak(')');
				}
				else if (node.Value != null)
				{
					node.Value.Accept(this);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00039220 File Offset: 0x00037420
		public void Visit(Conditional node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				if (node.Condition != null)
				{
					this.AcceptNodeWithParens(node.Condition, node.Condition.Precedence < OperatorPrecedence.LogicalOr);
					this.SetContextOutputPosition(node.Context);
				}
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
					this.OutputPossibleLineBreak('?');
					this.MarkSegment(node, null, node.QuestionContext ?? node.Context);
					this.BreakLine(false);
					if (!this.m_onNewLine)
					{
						this.OutputPossibleLineBreak(' ');
					}
				}
				else
				{
					this.OutputPossibleLineBreak('?');
					this.MarkSegment(node, null, node.QuestionContext ?? node.Context);
				}
				this.m_startOfStatement = false;
				if (node.TrueExpression != null)
				{
					this.m_noIn = noIn;
					this.AcceptNodeWithParens(node.TrueExpression, node.TrueExpression.Precedence < OperatorPrecedence.Assignment);
				}
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
					this.OutputPossibleLineBreak(':');
					this.MarkSegment(node, null, node.ColonContext ?? node.Context);
					this.BreakLine(false);
					if (!this.m_onNewLine)
					{
						this.OutputPossibleLineBreak(' ');
					}
				}
				else
				{
					this.OutputPossibleLineBreak(':');
					this.MarkSegment(node, null, node.ColonContext ?? node.Context);
				}
				if (node.FalseExpression != null)
				{
					this.m_noIn = noIn;
					this.AcceptNodeWithParens(node.FalseExpression, node.FalseExpression.Precedence < OperatorPrecedence.Assignment);
				}
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000393B8 File Offset: 0x000375B8
		public void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				switch (node.PrimitiveType)
				{
				case PrimitiveType.Null:
					this.Output("null");
					break;
				case PrimitiveType.Boolean:
					this.Output(node.ToBoolean() ? "true" : "false");
					break;
				case PrimitiveType.Number:
					if (node.Context == null || !node.Context.HasCode || (!node.MayHaveIssues && this.m_settings.IsModificationAllowed(TreeModifications.MinifyNumericLiterals)))
					{
						this.Output(OutputVisitor.NormalizeNumber(node.ToNumber(), node.Context));
					}
					else
					{
						this.Output(node.Context.Code);
					}
					break;
				case PrimitiveType.String:
					if (node.Context == null || !node.Context.HasCode)
					{
						this.Output(this.InlineSafeString(OutputVisitor.EscapeString(this.ReplaceTokens(node.Value.ToString()))));
					}
					else if (!this.m_settings.IsModificationAllowed(TreeModifications.MinifyStringLiterals))
					{
						this.Output(this.ReplaceTokens(node.Context.Code));
					}
					else if (node.MayHaveIssues || (this.m_settings.AllowEmbeddedAspNetBlocks && node.StringContainsAspNetReplacement))
					{
						this.Output(this.InlineSafeString(this.ReplaceTokens(node.Context.Code)));
					}
					else
					{
						this.Output(this.InlineSafeString(OutputVisitor.EscapeString(this.ReplaceTokens(node.Value.ToString()))));
					}
					break;
				case PrimitiveType.Other:
				{
					Match match;
					if (this.m_hasReplacementTokens && (match = CommonData.ReplacementToken.Match(node.Value.ToString())).Success && match.Value.Equals(node.Value))
					{
						this.Output(this.GetSyntacticReplacementToken(match));
					}
					else
					{
						this.Output(node.Value.ToString());
					}
					break;
				}
				}
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000395EE File Offset: 0x000377EE
		private string ReplaceTokens(string text)
		{
			if (this.m_hasReplacementTokens)
			{
				text = CommonData.ReplacementToken.Replace(text, new MatchEvaluator(this.GetReplacementToken));
			}
			return text;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00039614 File Offset: 0x00037814
		private string GetReplacementToken(Match match)
		{
			string text;
			if (!this.m_settings.ReplacementTokens.TryGetValue(match.Result("${token}"), out text))
			{
				string text2 = match.Result("${fallback}");
				if (!text2.IsNullOrWhiteSpace())
				{
					this.m_settings.ReplacementFallbacks.TryGetValue(text2, out text);
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00039674 File Offset: 0x00037874
		private string GetSyntacticReplacementToken(Match match)
		{
			string text;
			if (this.m_settings.ReplacementTokens.TryGetValue(match.Result("${token}"), out text))
			{
				string text2 = JSON.Validate(text);
				if (!text2.IsNullOrWhiteSpace())
				{
					text = text2;
				}
				else
				{
					text = this.InlineSafeString(OutputVisitor.EscapeString(text));
				}
			}
			else
			{
				string text3 = match.Result("${fallback}");
				if (!text3.IsNullOrWhiteSpace())
				{
					this.m_settings.ReplacementFallbacks.TryGetValue(text3, out text);
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000396F4 File Offset: 0x000378F4
		public void Visit(ConstantWrapperPP node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (node.ForceComments)
				{
					this.Output("/*");
				}
				this.Output(node.VarName);
				this.m_startOfStatement = false;
				this.SetContextOutputPosition(node.Context);
				if (node.ForceComments)
				{
					this.Output("@*/");
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00039758 File Offset: 0x00037958
		public void Visit(ConstStatement node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("const");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Indent();
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							this.NewLine();
						}
						variableDeclaration.Accept(this);
					}
				}
				this.Unindent();
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000397F4 File Offset: 0x000379F4
		public void Visit(ContinueNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("continue");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (!node.Label.IsNullOrWhiteSpace())
				{
					this.m_noLineBreaks = true;
					if (node.LabelInfo.IfNotNull((LabelInfo li) => !li.MinLabel.IsNullOrWhiteSpace()))
					{
						this.Output(node.LabelInfo.MinLabel);
					}
					else
					{
						this.Output(node.Label);
					}
					this.MarkSegment(node, null, node.LabelContext);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000398B0 File Offset: 0x00037AB0
		public void Visit(CustomNode node)
		{
			if (node != null)
			{
				string text = node.ToCode();
				if (!text.IsNullOrWhiteSpace())
				{
					object symbol = this.StartSymbol(node);
					this.Output(text);
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
					this.EndSymbol(symbol);
				}
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00039900 File Offset: 0x00037B00
		public void Visit(DebuggerNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("debugger");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0003994B File Offset: 0x00037B4B
		public void Visit(DirectivePrologue node)
		{
			if (node != null)
			{
				node.IsRedundant = (node.UseStrict && !this.m_needsStrictDirective);
				if (!node.IsRedundant)
				{
					this.Visit(node);
					if (node.UseStrict)
					{
						this.m_needsStrictDirective = false;
					}
				}
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00039988 File Offset: 0x00037B88
		public void Visit(DoWhile node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("do");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (node.Body == null || node.Body.Count == 0)
				{
					this.OutputPossibleLineBreak(';');
				}
				else if (node.Body.Count == 1 && !node.Body.EncloseBlock(EncloseBlockType.SingleDoWhile))
				{
					this.Indent();
					this.NewLine();
					this.m_startOfStatement = true;
					node.Body[0].Accept(this);
					if (this.m_requiresSeparator.Query(node.Body[0]) && this.ReplaceableSemicolon())
					{
						this.MarkSegment(node.Body[0], null, node.Body[0].TerminatingContext);
					}
					this.Unindent();
					this.NewLine();
				}
				else
				{
					if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.Body.BraceOnNewLine))
					{
						this.NewLine();
					}
					else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.OutputPossibleLineBreak(' ');
					}
					node.Body.Accept(this);
					if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.OutputPossibleLineBreak(' ');
					}
				}
				this.Output("while");
				this.MarkSegment(node, null, node.WhileContext);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.Output(')');
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00039B4A File Offset: 0x00037D4A
		public void Visit(EmptyStatement node)
		{
			if (node != null)
			{
				this.OutputPossibleLineBreak(';');
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00039B74 File Offset: 0x00037D74
		public void Visit(ExportNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("export");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.SetContextOutputPosition(node.KeywordContext);
				this.m_startOfStatement = false;
				if (node.IsDefault)
				{
					this.Output("default");
					if (node.Count > 0)
					{
						node[0].Accept(this);
					}
				}
				else if (node.Count == 1 && (node[0] is Declaration || node[0] is FunctionObject || node[0] is ClassNode))
				{
					node[0].Accept(this);
				}
				else
				{
					if (node.Count == 0)
					{
						this.OutputPossibleLineBreak('*');
						this.SetContextOutputPosition(node.OpenContext);
					}
					else
					{
						this.OutputPossibleLineBreak('{');
						this.SetContextOutputPosition(node.OpenContext);
						bool flag = true;
						foreach (AstNode astNode in node.Children)
						{
							if (flag)
							{
								flag = false;
							}
							else
							{
								this.OutputPossibleLineBreak(',');
							}
							astNode.Accept(this);
						}
						this.OutputPossibleLineBreak('}');
						this.SetContextOutputPosition(node.CloseContext);
					}
					if (node.ModuleName != null)
					{
						this.Output("from");
						this.SetContextOutputPosition(node.FromContext);
						this.Output(OutputVisitor.EscapeString(node.ModuleName));
						this.SetContextOutputPosition(node.ModuleContext);
					}
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00039D18 File Offset: 0x00037F18
		public void Visit(ForIn node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("for");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Variable != null)
				{
					this.m_noIn = true;
					node.Variable.Accept(this);
					this.m_noIn = false;
				}
				if (node.OperatorContext != null && !node.OperatorContext.Code.IsNullOrWhiteSpace())
				{
					this.Output(node.OperatorContext.Code);
				}
				else
				{
					this.Output("in");
				}
				this.MarkSegment(node, null, node.OperatorContext);
				if (node.Collection != null)
				{
					node.Collection.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.MarkSegment(node, null, node.Context);
				this.OutputBlock(node.Body);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00039E24 File Offset: 0x00038024
		public void Visit(ForNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("for");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Initializer != null)
				{
					this.m_noIn = true;
					node.Initializer.Accept(this);
					this.m_noIn = false;
				}
				this.OutputPossibleLineBreak(';');
				this.MarkSegment(node, null, node.Separator1Context ?? node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(';');
				this.MarkSegment(node, null, node.Separator2Context ?? node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				if (node.Incrementer != null)
				{
					node.Incrementer.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.MarkSegment(node, null, node.Context);
				this.OutputBlock(node.Body);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00039F68 File Offset: 0x00038168
		private void OutputFunctionPrefix(FunctionObject node, string functionName)
		{
			if (node.FunctionType == FunctionType.Method)
			{
				if (node.IsGenerator)
				{
					this.Output('*');
					this.MarkSegment(node, functionName, node.Context);
					this.SetContextOutputPosition(node.Context);
					return;
				}
			}
			else
			{
				if (node.FunctionType == FunctionType.Getter)
				{
					this.Output("get");
					this.MarkSegment(node, functionName, node.Context);
					this.SetContextOutputPosition(node.Context);
					return;
				}
				if (node.FunctionType == FunctionType.Setter)
				{
					this.Output("set");
					this.MarkSegment(node, functionName, node.Context);
					this.SetContextOutputPosition(node.Context);
					return;
				}
				this.Output("function");
				this.MarkSegment(node, functionName, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (node.IsGenerator)
				{
					this.Output('*');
				}
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003A040 File Offset: 0x00038240
		public void Visit(FunctionObject node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					this.OutputFunctionArgsAndBody(node);
				}
				else
				{
					bool flag = node.IsExpression && this.m_startOfStatement;
					if (flag)
					{
						this.OutputPossibleLineBreak('(');
					}
					bool flag2 = node.Binding != null && !node.Binding.Name.IsNullOrWhiteSpace() && (node.FunctionType != FunctionType.Expression || node.Binding.VariableField.RefCount > 0 || !this.m_settings.RemoveFunctionExpressionNames || !this.m_settings.IsModificationAllowed(TreeModifications.RemoveFunctionExpressionNames));
					string functionName = flag2 ? node.Binding.Name : node.NameGuess;
					if (node.IsStatic)
					{
						this.Output("static");
					}
					this.OutputFunctionPrefix(node, functionName);
					this.m_startOfStatement = false;
					bool flag3 = true;
					if (node.Binding != null && !node.Binding.Name.IsNullOrWhiteSpace())
					{
						flag3 = false;
						string text = (node.Binding.VariableField != null) ? node.Binding.VariableField.ToString() : node.Binding.Name;
						if (this.m_settings.SymbolsMap != null)
						{
							this.m_functionStack.Push(text);
						}
						if (flag2)
						{
							if (JSScanner.IsValidIdentifierPart(this.m_lastCharacter))
							{
								this.Output(' ');
							}
							this.Output(text);
							this.MarkSegment(node, node.Binding.Name, node.Binding.Context);
							this.SetContextOutputPosition(node.Context);
						}
					}
					if (this.m_settings.SymbolsMap != null && flag3)
					{
						BinaryOperator binaryOperator = node.Parent as BinaryOperator;
						if (binaryOperator != null && binaryOperator.Operand1 is Lookup)
						{
							this.m_functionStack.Push("(anonymous) [{0}]".FormatInvariant(new object[]
							{
								binaryOperator.Operand1
							}));
						}
						else
						{
							this.m_functionStack.Push("(anonymous)");
						}
					}
					this.OutputFunctionArgsAndBody(node);
					if (flag)
					{
						this.OutputPossibleLineBreak(')');
					}
				}
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
				if (this.m_settings.SymbolsMap != null)
				{
					this.m_functionStack.Pop();
				}
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003A290 File Offset: 0x00038490
		public void Visit(GetterSetter node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output(node.IsGetter ? "get" : "set");
				this.MarkSegment(node, node.Value.ToString(), node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Output(node.Value.ToString());
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003A308 File Offset: 0x00038508
		public virtual void Visit(GroupingOperator node)
		{
			if (node != null)
			{
				this.Output('(');
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.MarkSegment(node, null, node.Context);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003A36C File Offset: 0x0003856C
		public void Visit(IfNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("if");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				if (node.TrueBlock != null && node.TrueBlock.ForceBraces)
				{
					this.OutputBlockWithBraces(node.TrueBlock);
				}
				else if (node.TrueBlock == null || node.TrueBlock.Count == 0)
				{
					this.OutputPossibleLineBreak(';');
				}
				else if (node.TrueBlock.Count == 1 && (node.FalseBlock == null || (!node.TrueBlock.EncloseBlock(EncloseBlockType.IfWithoutElse) && !node.TrueBlock.EncloseBlock(EncloseBlockType.SingleDoWhile))) && (!this.m_settings.MacSafariQuirks || !(node.TrueBlock[0] is FunctionObject)))
				{
					this.Indent();
					this.NewLine();
					this.m_startOfStatement = true;
					node.TrueBlock[0].Accept(this);
					if (node.TrueBlock[0] is ImportantComment)
					{
						this.OutputPossibleLineBreak(';');
					}
					if (node.FalseBlock != null && node.FalseBlock.Count > 0 && this.m_requiresSeparator.Query(node.TrueBlock[0]) && this.ReplaceableSemicolon())
					{
						this.MarkSegment(node.TrueBlock[0], null, node.TrueBlock[0].TerminatingContext);
					}
					this.Unindent();
				}
				else
				{
					this.OutputBlockWithBraces(node.TrueBlock);
				}
				if (node.FalseBlock != null && (node.FalseBlock.Count > 0 || node.FalseBlock.ForceBraces))
				{
					this.NewLine();
					this.Output("else");
					this.MarkSegment(node, null, node.ElseContext);
					if (node.FalseBlock.Count == 1 && !node.FalseBlock.ForceBraces)
					{
						AstNode astNode = node.FalseBlock[0];
						if (astNode is IfNode)
						{
							astNode.Accept(this);
						}
						else
						{
							this.Indent();
							this.NewLine();
							this.m_startOfStatement = true;
							astNode.Accept(this);
							this.Unindent();
						}
					}
					else
					{
						this.OutputBlockWithBraces(node.FalseBlock);
					}
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003A600 File Offset: 0x00038800
		public void Visit(ImportantComment node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.BreakLine(true);
				node.Context.OutputLine = this.m_lineCount;
				char[] anyOf = new char[]
				{
					'\n',
					'\r',
					'\u2028',
					'\u2029'
				};
				int num = 0;
				int num2 = node.Comment.IndexOfAny(anyOf, num);
				if (num2 < 0)
				{
					this.Output(node.Comment);
				}
				else
				{
					this.Output(node.Comment.Substring(0, num2));
					for (;;)
					{
						if (node.Comment[num2] == '\r' && num2 < node.Comment.Length - 1 && node.Comment[num2 + 1] == '\n')
						{
							num = num2 + 2;
						}
						else
						{
							num = num2 + 1;
						}
						this.BreakLine(true);
						num2 = node.Comment.IndexOfAny(anyOf, num);
						if (num2 > num)
						{
							this.Output(node.Comment.Substring(num, num2 - num));
						}
						else if (num2 < 0)
						{
							break;
						}
					}
					this.Output(node.Comment.Substring(num));
				}
				this.BreakLine(true);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003A710 File Offset: 0x00038910
		public void Visit(ImportExportSpecifier node)
		{
			if (node != null)
			{
				if (node.Parent is ImportNode)
				{
					if (!node.ExternalName.IsNullOrWhiteSpace())
					{
						this.Output(node.ExternalName);
						this.SetContextOutputPosition(node.Context);
						this.SetContextOutputPosition(node.NameContext);
						this.Output("as");
						this.SetContextOutputPosition(node.AsContext);
					}
					if (node.LocalIdentifier != null)
					{
						node.LocalIdentifier.Accept(this);
						if (node.ExternalName.IsNullOrWhiteSpace())
						{
							this.SetContextOutputPosition(node.Context);
							return;
						}
					}
				}
				else
				{
					if (node.LocalIdentifier != null)
					{
						node.LocalIdentifier.Accept(this);
						this.SetContextOutputPosition(node.Context);
					}
					if (!node.ExternalName.IsNullOrWhiteSpace())
					{
						this.Output("as");
						this.SetContextOutputPosition(node.AsContext);
						this.Output(node.ExternalName);
						this.SetContextOutputPosition(node.NameContext);
					}
				}
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003A808 File Offset: 0x00038A08
		public void Visit(ImportNode node)
		{
			if (node != null)
			{
				this.Output("import");
				this.SetContextOutputPosition(node.Context);
				this.SetContextOutputPosition(node.KeywordContext);
				this.m_startOfStatement = false;
				if (node.Count > 0)
				{
					if (node.Count == 1 && node[0] is BindingIdentifier)
					{
						node[0].Accept(this);
					}
					else
					{
						this.OutputPossibleLineBreak('{');
						this.SetContextOutputPosition(node.OpenContext);
						bool flag = true;
						foreach (AstNode astNode in node.Children)
						{
							if (flag)
							{
								flag = false;
							}
							else
							{
								this.OutputPossibleLineBreak(',');
							}
							astNode.Accept(this);
						}
						this.OutputPossibleLineBreak('}');
						this.SetContextOutputPosition(node.CloseContext);
					}
					this.Output("from");
					this.SetContextOutputPosition(node.FromContext);
				}
				if (node.ModuleName != null)
				{
					this.Output(OutputVisitor.EscapeString(node.ModuleName));
					this.SetContextOutputPosition(node.ModuleContext);
				}
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003A92C File Offset: 0x00038B2C
		public void Visit(InitializerNode node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				this.OutputPossibleLineBreak('=');
				this.MarkSegment(node, null, node.AssignContext);
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003A98C File Offset: 0x00038B8C
		public void Visit(LabeledStatement node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (!node.Label.IsNullOrWhiteSpace())
				{
					if (node.LabelInfo.IfNotNull((LabelInfo li) => !li.MinLabel.IsNullOrWhiteSpace()))
					{
						this.Output(node.LabelInfo.MinLabel);
					}
					else
					{
						this.Output(node.Label);
					}
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
					this.OutputPossibleLineBreak(':');
					this.MarkSegment(node, null, node.ColonContext);
				}
				if (node.Statement != null)
				{
					this.m_startOfStatement = true;
					node.Statement.Accept(this);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003AA50 File Offset: 0x00038C50
		public void Visit(LexicalDeclaration node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.Output(OutputVisitor.OperatorString(node.StatementToken));
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Indent();
				bool flag = !(node.Parent is ForNode);
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							if (flag)
							{
								this.NewLine();
							}
							else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
						this.m_noIn = noIn;
						variableDeclaration.Accept(this);
					}
				}
				this.Unindent();
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0003AB24 File Offset: 0x00038D24
		public void Visit(Lookup node)
		{
			if (node != null)
			{
				if (JSScanner.IsValidIdentifierPart(this.m_lastCharacter))
				{
					this.OutputSpaceOrLineBreak();
				}
				object symbol = this.StartSymbol(node);
				this.Output((node.VariableField != null) ? node.VariableField.ToString() : node.Name);
				this.MarkSegment(node, node.Name, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		public void Visit(Member node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				if (node.Root != null)
				{
					ConstantWrapper constantWrapper = node.Root as ConstantWrapper;
					if (constantWrapper != null && (constantWrapper.IsFiniteNumericLiteral || constantWrapper.IsOtherDecimal))
					{
						string text;
						if (constantWrapper.Context == null || !constantWrapper.Context.HasCode || (this.m_settings.IsModificationAllowed(TreeModifications.MinifyNumericLiterals) && !constantWrapper.MayHaveIssues))
						{
							text = OutputVisitor.NormalizeNumber(constantWrapper.ToNumber(), constantWrapper.Context);
						}
						else
						{
							text = constantWrapper.Context.Code;
						}
						if (text.StartsWith("-", StringComparison.Ordinal))
						{
							this.Output('(');
							this.Output(text);
							this.Output(')');
						}
						else
						{
							this.Output(text);
							if (text.IndexOf('.') < 0 && text.IndexOf("e", StringComparison.OrdinalIgnoreCase) < 0)
							{
								bool flag = !text.StartsWith("0", StringComparison.Ordinal) || text.Length == 1;
								if (!flag && JSScanner.IsDigit(text[1]))
								{
									for (int i = 1; i < text.Length; i++)
									{
										if ('7' < text[i])
										{
											flag = true;
											break;
										}
									}
								}
								if (flag)
								{
									this.Output('.');
								}
							}
						}
					}
					else
					{
						bool flag2 = node.Root.Precedence < node.Precedence;
						if (!flag2)
						{
							CallNode callNode = node.Root as CallNode;
							if (callNode != null && callNode.IsConstructor && (callNode.Arguments == null || callNode.Arguments.Count == 0))
							{
								flag2 = true;
							}
						}
						this.AcceptNodeWithParens(node.Root, flag2);
					}
					this.SetContextOutputPosition(node.Context);
				}
				this.OutputPossibleLineBreak('.');
				this.MarkSegment(node, node.Name, node.NameContext);
				this.Output(node.Name);
				this.m_startOfStatement = false;
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		public void Visit(ModuleDeclaration node)
		{
			if (node != null)
			{
				if (node.IsImplicit)
				{
					if (node.Body != null)
					{
						node.Body.Accept(this);
						return;
					}
				}
				else
				{
					this.Output("module");
					this.SetContextOutputPosition(node.Context);
					this.SetContextOutputPosition(node.ModuleContext);
					if (node.Binding != null)
					{
						node.Binding.Accept(this);
						this.Output("from");
						this.SetContextOutputPosition(node.FromContext);
						if (node.ModuleName != null)
						{
							this.Output(OutputVisitor.EscapeString(node.ModuleName));
							this.SetContextOutputPosition(node.ModuleContext);
							return;
						}
					}
					else
					{
						this.m_noLineBreaks = true;
						if (node.ModuleName != null)
						{
							this.Output(OutputVisitor.EscapeString(node.ModuleName));
							this.SetContextOutputPosition(node.ModuleContext);
						}
						if (node.Body != null)
						{
							node.Body.Accept(this);
							return;
						}
						this.Output("{}");
					}
				}
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0003AE9C File Offset: 0x0003909C
		public void Visit(ObjectLiteral node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				bool startOfStatement = this.m_startOfStatement;
				if (startOfStatement)
				{
					this.OutputPossibleLineBreak('(');
				}
				this.OutputPossibleLineBreak('{');
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Indent();
				int num = node.Properties.IfNotNull((AstNodeList p) => p.Count);
				if (num > 1)
				{
					this.NewLine();
				}
				if (node.Properties != null)
				{
					node.Properties.Accept(this);
				}
				this.Unindent();
				if (num > 1)
				{
					this.NewLine();
				}
				this.Output('}');
				this.MarkSegment(node, null, node.Context);
				if (startOfStatement)
				{
					this.Output(')');
				}
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0003AF94 File Offset: 0x00039194
		public void Visit(ObjectLiteralField node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (this.m_settings.QuoteObjectLiteralProperties)
				{
					if (node.PrimitiveType == PrimitiveType.String)
					{
						this.Visit(node);
					}
					else
					{
						this.Output('"');
						this.Visit(node);
						this.Output('"');
					}
				}
				else if (node.PrimitiveType == PrimitiveType.String)
				{
					string text = node.ToString();
					if (!string.IsNullOrEmpty(text) && JSScanner.IsSafeIdentifier(text))
					{
						if (!JSScanner.IsKeyword(text, node.EnclosingScope.IfNotNull((ActivationObject s) => s.UseStrict)))
						{
							this.Output(text);
							this.MarkSegment(node, null, node.Context);
							goto IL_C0;
						}
					}
					this.Visit(node);
				}
				else
				{
					this.Visit(node);
				}
				IL_C0:
				this.OutputPossibleLineBreak(':');
				this.MarkSegment(node, null, node.ColonContext);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003B094 File Offset: 0x00039294
		public void Visit(ObjectLiteralProperty node)
		{
			if (node != null)
			{
				if (node.Name != null && !(node.Name is GetterSetter))
				{
					node.Name.Accept(this);
					this.SetContextOutputPosition(node.Context);
				}
				if (node.Value != null)
				{
					this.AcceptNodeWithParens(node.Value, node.Value.Precedence == OperatorPrecedence.Comma);
				}
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003B0FC File Offset: 0x000392FC
		public void Visit(ParameterDeclaration node)
		{
			if (node != null)
			{
				if (node.HasRest)
				{
					this.Output(OutputVisitor.OperatorString(JSToken.RestSpread));
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
				}
				node.Binding.IfNotNull(delegate(AstNode b)
				{
					b.Accept(this);
				});
				if (node.Initializer != null)
				{
					if (this.m_settings.OutputMode == OutputMode.MultipleLines && this.m_settings.IndentSize > 0)
					{
						this.OutputPossibleLineBreak(' ');
						this.OutputPossibleLineBreak('=');
						this.BreakLine(false);
						if (!this.m_onNewLine)
						{
							this.OutputPossibleLineBreak(' ');
						}
					}
					else
					{
						this.OutputPossibleLineBreak('=');
					}
					this.AcceptNodeWithParens(node.Initializer, node.Initializer.Precedence == OperatorPrecedence.Comma);
				}
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003B1CC File Offset: 0x000393CC
		public void Visit(RegExpLiteral node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.m_startOfStatement = false;
				this.Output('/');
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.Output(node.Pattern);
				this.Output('/');
				if (!string.IsNullOrEmpty(node.PatternSwitches))
				{
					this.Output(node.PatternSwitches);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003B244 File Offset: 0x00039444
		public void Visit(ReturnNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("return");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Operand != null)
				{
					if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.Output(' ');
					}
					this.m_noLineBreaks = true;
					this.Indent();
					node.Operand.Accept(this);
					this.Unindent();
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0003B2CC File Offset: 0x000394CC
		public void Visit(Switch node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("switch");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.BraceOnNewLine))
				{
					this.NewLine();
				}
				else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('{');
				this.MarkSegment(node, null, node.BraceContext);
				this.Indent();
				AstNode astNode = null;
				for (int i = 0; i < node.Cases.Count; i++)
				{
					AstNode astNode2 = node.Cases[i];
					if (astNode2 != null)
					{
						if (astNode != null && this.m_requiresSeparator.Query(astNode) && this.ReplaceableSemicolon())
						{
							this.MarkSegment(astNode, null, astNode.TerminatingContext);
						}
						this.NewLine();
						astNode2.Accept(this);
						astNode = astNode2;
					}
				}
				this.Unindent();
				this.NewLine();
				this.OutputPossibleLineBreak('}');
				this.MarkSegment(node, null, node.BraceContext);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0003B434 File Offset: 0x00039634
		public void Visit(SwitchCase node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				if (node.CaseValue != null)
				{
					this.Output("case");
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
					this.m_startOfStatement = false;
					node.CaseValue.Accept(this);
				}
				else
				{
					this.Output("default");
					this.MarkSegment(node, null, node.Context);
					this.SetContextOutputPosition(node.Context);
				}
				this.OutputPossibleLineBreak(':');
				this.MarkSegment(node, null, node.ColonContext);
				if (node.Statements != null && node.Statements.Count > 0)
				{
					this.Indent();
					AstNode astNode = null;
					for (int i = 0; i < node.Statements.Count; i++)
					{
						AstNode astNode2 = node.Statements[i];
						if (astNode2 != null)
						{
							if (astNode != null && this.m_requiresSeparator.Query(astNode))
							{
								this.OutputPossibleLineBreak(';');
								this.MarkSegment(astNode, null, astNode.TerminatingContext);
							}
							this.NewLine();
							this.m_startOfStatement = true;
							astNode2.Accept(this);
							astNode = astNode2;
						}
					}
					this.Unindent();
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003B564 File Offset: 0x00039764
		public virtual void Visit(TemplateLiteral node)
		{
			if (node != null)
			{
				if (node.Function != null)
				{
					node.Function.Accept(this);
					this.m_startOfStatement = false;
				}
				string text = node.Text;
				if (node.TextContext != null && !this.m_settings.IsModificationAllowed(TreeModifications.MinifyStringLiterals))
				{
					text = node.TextContext.Code;
				}
				if (!text.IsNullOrWhiteSpace())
				{
					this.Output(text);
					this.MarkSegment(node, null, node.TextContext ?? node.Context);
					this.SetContextOutputPosition(node.TextContext);
					this.m_startOfStatement = false;
				}
				if (node.Expressions != null && node.Expressions.Count > 0)
				{
					node.Expressions.ForEach<TemplateLiteralExpression>(delegate(TemplateLiteralExpression expr)
					{
						expr.Accept(this);
					});
				}
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003B630 File Offset: 0x00039830
		public virtual void Visit(TemplateLiteralExpression node)
		{
			if (node != null)
			{
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				if (!node.Text.IsNullOrWhiteSpace())
				{
					this.Output(node.Text);
					this.MarkSegment(node, null, node.TextContext);
					this.SetContextOutputPosition(node.TextContext);
					this.m_startOfStatement = false;
				}
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003B690 File Offset: 0x00039890
		public void Visit(ThisLiteral node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("this");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003B6DC File Offset: 0x000398DC
		public void Visit(ThrowNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("throw");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				if (node.Operand != null)
				{
					this.m_noLineBreaks = true;
					node.Operand.Accept(this);
				}
				if (this.m_settings.MacSafariQuirks)
				{
					this.OutputPossibleLineBreak(';');
					this.MarkSegment(node, null, node.TerminatingContext);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003B768 File Offset: 0x00039968
		public void Visit(TryNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.OutputTryBranch(node);
				bool flag = false;
				if (node.CatchParameter != null)
				{
					flag = true;
					this.OutputCatchBranch(node);
				}
				if (!flag || (node.FinallyBlock != null && node.FinallyBlock.Count > 0))
				{
					this.OutputFinallyBranch(node);
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0003B7C4 File Offset: 0x000399C4
		private void OutputTryBranch(TryNode node)
		{
			this.Output("try");
			this.MarkSegment(node, null, node.Context);
			this.SetContextOutputPosition(node.Context);
			if (node.TryBlock == null || node.TryBlock.Count == 0)
			{
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.Output("{}");
				this.BreakLine(false);
				return;
			}
			if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.TryBlock.BraceOnNewLine))
			{
				this.NewLine();
			}
			else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
			{
				this.OutputPossibleLineBreak(' ');
			}
			node.TryBlock.Accept(this);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003B890 File Offset: 0x00039A90
		private void OutputCatchBranch(TryNode node)
		{
			this.NewLine();
			this.Output("catch(");
			node.CatchParameter.IfNotNull(delegate(ParameterDeclaration p)
			{
				p.Accept(this);
			});
			this.OutputPossibleLineBreak(')');
			if (node.CatchBlock == null || node.CatchBlock.Count == 0)
			{
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.Output("{}");
				this.BreakLine(false);
				return;
			}
			if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.CatchBlock.BraceOnNewLine))
			{
				this.NewLine();
			}
			else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
			{
				this.OutputPossibleLineBreak(' ');
			}
			node.CatchBlock.Accept(this);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003B960 File Offset: 0x00039B60
		private void OutputFinallyBranch(TryNode node)
		{
			this.NewLine();
			this.Output("finally");
			this.MarkSegment(node, null, node.FinallyContext);
			if (node.FinallyBlock == null || node.FinallyBlock.Count == 0)
			{
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.Output("{}");
				this.BreakLine(false);
				return;
			}
			if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.FinallyBlock.BraceOnNewLine))
			{
				this.NewLine();
			}
			else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
			{
				this.OutputPossibleLineBreak(' ');
			}
			node.FinallyBlock.Accept(this);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003BA1C File Offset: 0x00039C1C
		public void Visit(Var node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.Output("var");
				this.MarkSegment(node, null, node.Context);
				this.SetContextOutputPosition(node.Context);
				this.m_startOfStatement = false;
				this.Indent();
				bool flag = !(node.Parent is ForNode);
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							if (flag)
							{
								this.NewLine();
							}
							else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
						this.m_noIn = noIn;
						variableDeclaration.Accept(this);
					}
				}
				this.Unindent();
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		public void Visit(VariableDeclaration node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				node.Binding.IfNotNull(delegate(AstNode b)
				{
					b.Accept(this);
				});
				this.m_startOfStatement = false;
				if (node.Initializer != null)
				{
					if (node.IsCCSpecialCase)
					{
						if (!this.m_outputCCOn || (node.UseCCOn && !this.m_settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryCCOnStatements)))
						{
							this.Output("/*@cc_on=");
							this.m_outputCCOn = true;
						}
						else
						{
							this.Output("/*@=");
						}
					}
					else if (this.m_settings.OutputMode == OutputMode.MultipleLines && this.m_settings.IndentSize > 0)
					{
						this.OutputPossibleLineBreak(' ');
						this.OutputPossibleLineBreak('=');
						this.BreakLine(false);
						if (!this.m_onNewLine)
						{
							this.OutputPossibleLineBreak(' ');
						}
					}
					else
					{
						this.OutputPossibleLineBreak('=');
					}
					this.AcceptNodeWithParens(node.Initializer, node.Initializer.Precedence == OperatorPrecedence.Comma);
					if (node.IsCCSpecialCase)
					{
						this.Output("@*/");
					}
				}
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0003BC18 File Offset: 0x00039E18
		public void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				bool noIn = this.m_noIn;
				this.m_noIn = false;
				if (node.IsPostfix)
				{
					if (node.Operand != null)
					{
						this.AcceptNodeWithParens(node.Operand, node.Operand.Precedence < node.Precedence);
						OutputVisitor.SetContextOutputPosition(node.Context, node.Operand.Context);
					}
					this.m_noLineBreaks = true;
					this.Output(OutputVisitor.OperatorString(node.OperatorToken));
					this.MarkSegment(node, null, node.OperatorContext);
					this.m_startOfStatement = false;
				}
				else if (node.OperatorToken == JSToken.RestSpread)
				{
					this.Output(OutputVisitor.OperatorString(JSToken.RestSpread));
					this.MarkSegment(node, null, node.OperatorContext ?? node.Context);
					node.Operand.IfNotNull(delegate(AstNode o)
					{
						o.Accept(this);
					});
				}
				else
				{
					if (node.OperatorInConditionalCompilationComment)
					{
						if (!this.m_outputCCOn || (node.ConditionalCommentContainsOn && !this.m_settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryCCOnStatements)))
						{
							this.Output("/*@cc_on");
							this.m_outputCCOn = true;
						}
						else
						{
							this.Output("/*@");
						}
						this.Output(OutputVisitor.OperatorString(node.OperatorToken));
						this.MarkSegment(node, null, node.OperatorContext);
						this.SetContextOutputPosition(node.Context);
						this.Output("@*/");
					}
					else
					{
						this.Output(OutputVisitor.OperatorString(node.OperatorToken));
						this.MarkSegment(node, null, node.OperatorContext ?? node.Context);
						this.SetContextOutputPosition(node.Context);
						if (node.OperatorToken == JSToken.Yield && node.IsDelegator)
						{
							this.Output('*');
						}
					}
					this.m_startOfStatement = false;
					if (node.Operand != null)
					{
						this.AcceptNodeWithParens(node.Operand, node.Operand.Precedence < node.Precedence);
					}
				}
				this.m_noIn = noIn;
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003BE18 File Offset: 0x0003A018
		public void Visit(WhileNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("while");
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.OutputBlock(node.Body);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003BE9C File Offset: 0x0003A09C
		public void Visit(WithNode node)
		{
			if (node != null)
			{
				object symbol = this.StartSymbol(node);
				this.Output("with");
				this.SetContextOutputPosition(node.Context);
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				if (node.WithObject != null)
				{
					node.WithObject.Accept(this);
				}
				this.OutputPossibleLineBreak(')');
				this.OutputBlock(node.Body);
				this.EndSymbol(symbol);
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003BF20 File Offset: 0x0003A120
		private void Output([Localizable(false)] string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this.InsertSpaceIfNeeded(text);
				this.m_segmentStartLine = this.m_lineCount;
				this.m_segmentStartColumn = this.m_lineLength;
				this.m_lineLength += this.WriteToStream(text);
				this.m_noLineBreaks = false;
				this.m_onNewLine = (text[text.Length - 1] == '\n' || text[text.Length - 1] == '\r');
				this.SetLastCharState(text);
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
		private void Output(char ch)
		{
			this.InsertSpaceIfNeeded(ch);
			this.m_segmentStartLine = this.m_lineCount;
			this.m_segmentStartColumn = this.m_lineLength;
			this.m_lineLength += this.WriteToStream(ch);
			this.m_noLineBreaks = false;
			this.m_onNewLine = (ch == '\n' || ch == '\r');
			this.SetLastCharState(ch);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003C005 File Offset: 0x0003A205
		private void OutputSpaceOrLineBreak()
		{
			if (this.m_noLineBreaks)
			{
				this.m_outputStream.Write(' ');
				this.m_lineLength++;
				this.m_lastCharacter = ' ';
				return;
			}
			this.OutputPossibleLineBreak(' ');
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003C03C File Offset: 0x0003A23C
		private void InsertSpaceIfNeeded(char ch)
		{
			if (ch != ' ')
			{
				if (this.m_addSpaceIfTrue != null)
				{
					if (this.m_addSpaceIfTrue(ch))
					{
						this.OutputSpaceOrLineBreak();
					}
					this.m_addSpaceIfTrue = null;
					return;
				}
				if ((ch == '+' || ch == '-') && this.m_lastCharacter == ch)
				{
					if (this.m_lastCountOdd)
					{
						this.OutputSpaceOrLineBreak();
						return;
					}
				}
				else if ((this.m_lastCharacter == '@' || JSScanner.IsValidIdentifierPart(this.m_lastCharacter)) && JSScanner.IsValidIdentifierPart(ch))
				{
					this.OutputSpaceOrLineBreak();
				}
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003C0BC File Offset: 0x0003A2BC
		private void InsertSpaceIfNeeded(string text)
		{
			char c = text[0];
			if (this.m_addSpaceIfTrue != null)
			{
				if (this.m_addSpaceIfTrue(c))
				{
					this.OutputSpaceOrLineBreak();
				}
				this.m_addSpaceIfTrue = null;
				return;
			}
			if ((c == '+' || c == '-') && this.m_lastCharacter == c)
			{
				if (this.m_lastCountOdd)
				{
					this.OutputSpaceOrLineBreak();
					return;
				}
			}
			else if ((this.m_lastCharacter == '@' || JSScanner.IsValidIdentifierPart(this.m_lastCharacter)) && (text[0] == '\\' || JSScanner.StartsWithValidIdentifierPart(text)))
			{
				this.OutputSpaceOrLineBreak();
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003C147 File Offset: 0x0003A347
		private void SetLastCharState(char ch)
		{
			if (ch == '+' || ch == '-')
			{
				if (ch == this.m_lastCharacter)
				{
					this.m_lastCountOdd = !this.m_lastCountOdd;
				}
				else
				{
					this.m_lastCountOdd = true;
				}
			}
			else
			{
				this.m_lastCountOdd = false;
			}
			this.m_lastCharacter = ch;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003C184 File Offset: 0x0003A384
		private void SetLastCharState(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				char c = text[text.Length - 1];
				if (c == '+' || c == '-')
				{
					int num = text.Length - 1;
					while (--num >= 0 && text[num] == c)
					{
					}
					if (num < 0 && this.m_lastCharacter == c)
					{
						this.m_lastCountOdd = (text.Length % 2 == 1 ^ this.m_lastCountOdd);
					}
					else
					{
						this.m_lastCountOdd = ((text.Length - 1 - num) % 2 == 1);
					}
				}
				else
				{
					this.m_lastCountOdd = false;
				}
				this.m_lastCharacter = c;
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003C21E File Offset: 0x0003A41E
		private void Indent()
		{
			this.m_indentLevel++;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003C22E File Offset: 0x0003A42E
		private void Unindent()
		{
			this.m_indentLevel--;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003C240 File Offset: 0x0003A440
		private void OutputPossibleLineBreak(char ch)
		{
			if (ch == ' ')
			{
				this.BreakLine(false);
				if (!this.m_onNewLine)
				{
					this.m_lineLength += this.WriteToStream(ch);
					this.m_lastCharacter = ch;
					return;
				}
			}
			else
			{
				this.InsertSpaceIfNeeded(ch);
				this.m_segmentStartLine = this.m_lineCount;
				this.m_segmentStartColumn = this.m_lineLength;
				this.m_lineLength += this.WriteToStream(ch);
				this.m_onNewLine = false;
				this.m_lastCharacter = ch;
				this.BreakLine(false);
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		private bool ReplaceableSemicolon()
		{
			bool result = false;
			if (this.m_lineLength < this.m_settings.LineBreakThreshold)
			{
				this.m_segmentStartLine = this.m_lineCount;
				this.m_segmentStartColumn = this.m_lineLength;
				this.m_outputStream.Write(';');
				this.m_lineLength++;
				this.m_onNewLine = false;
				this.m_lastCharacter = ';';
				result = true;
			}
			this.BreakLine(false);
			return result;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003C338 File Offset: 0x0003A538
		private void BreakLine(bool forceBreak)
		{
			if (!this.m_onNewLine && (forceBreak || this.m_lineLength >= this.m_settings.LineBreakThreshold))
			{
				if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.NewLine();
					return;
				}
				this.m_outputStream.Write('\n');
				this.m_lineCount++;
				this.m_lineLength = 0;
				this.m_onNewLine = true;
				this.m_lastCharacter = ' ';
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		private void NewLine()
		{
			if (this.m_settings.OutputMode == OutputMode.MultipleLines && !this.m_onNewLine)
			{
				this.m_outputStream.WriteLine();
				this.m_lineCount++;
				if (this.m_indentLevel > 0)
				{
					int lineLength = this.m_indentLevel * this.m_settings.IndentSize;
					this.m_lineLength = lineLength;
					while (lineLength-- > 0)
					{
						this.m_outputStream.Write(' ');
					}
				}
				else
				{
					this.m_lineLength = 0;
				}
				this.m_lastCharacter = ' ';
				this.m_onNewLine = true;
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003C43C File Offset: 0x0003A63C
		private int WriteToStream(string text)
		{
			if (this.m_settings.AlwaysEscapeNonAscii)
			{
				StringBuilder stringBuilder = null;
				int num = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] > '\u007f')
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						if (i > num)
						{
							stringBuilder.Append(text, num, i - num);
						}
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\u{0:x4}".FormatInvariant(new object[]
						{
							(int)text[i]
						}), new object[0]);
						num = i + 1;
					}
				}
				if (stringBuilder != null)
				{
					if (num < text.Length)
					{
						stringBuilder.Append(text, num, text.Length - num);
					}
					text = stringBuilder.ToString();
				}
			}
			this.m_outputStream.Write(text);
			return text.Length;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003C500 File Offset: 0x0003A700
		private int WriteToStream(char ch)
		{
			if (this.m_settings.AlwaysEscapeNonAscii && ch > '\u007f')
			{
				this.m_outputStream.Write("\\u{0:x4}", (int)ch);
				return 6;
			}
			this.m_outputStream.Write(ch);
			return 1;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0003C53C File Offset: 0x0003A73C
		public static string OperatorString(JSToken token)
		{
			switch (token)
			{
			case JSToken.ArrowFunction:
				return "=>";
			case JSToken.RestSpread:
				return "...";
			case JSToken.FirstOperator:
				return "delete";
			case JSToken.Increment:
				return "++";
			case JSToken.Decrement:
				return "--";
			case JSToken.Void:
				return "void";
			case JSToken.TypeOf:
				return "typeof";
			case JSToken.LogicalNot:
				return "!";
			case JSToken.BitwiseNot:
				return "~";
			case JSToken.FirstBinaryOperator:
				return "+";
			case JSToken.Minus:
				return "-";
			case JSToken.Multiply:
				return "*";
			case JSToken.Divide:
				return "/";
			case JSToken.Modulo:
				return "%";
			case JSToken.BitwiseAnd:
				return "&";
			case JSToken.BitwiseOr:
				return "|";
			case JSToken.BitwiseXor:
				return "^";
			case JSToken.LeftShift:
				return "<<";
			case JSToken.RightShift:
				return ">>";
			case JSToken.UnsignedRightShift:
				return ">>>";
			case JSToken.Equal:
				return "==";
			case JSToken.NotEqual:
				return "!=";
			case JSToken.StrictEqual:
				return "===";
			case JSToken.StrictNotEqual:
				return "!==";
			case JSToken.LessThan:
				return "<";
			case JSToken.LessThanEqual:
				return "<=";
			case JSToken.GreaterThan:
				return ">";
			case JSToken.GreaterThanEqual:
				return ">=";
			case JSToken.LogicalAnd:
				return "&&";
			case JSToken.LogicalOr:
				return "||";
			case JSToken.InstanceOf:
				return "instanceof";
			case JSToken.In:
				return "in";
			case JSToken.Comma:
				return ",";
			case JSToken.Assign:
				return "=";
			case JSToken.PlusAssign:
				return "+=";
			case JSToken.MinusAssign:
				return "-=";
			case JSToken.MultiplyAssign:
				return "*=";
			case JSToken.DivideAssign:
				return "/=";
			case JSToken.ModuloAssign:
				return "%=";
			case JSToken.BitwiseAndAssign:
				return "&=";
			case JSToken.BitwiseOrAssign:
				return "|=";
			case JSToken.BitwiseXorAssign:
				return "^=";
			case JSToken.LeftShiftAssign:
				return "<<=";
			case JSToken.RightShiftAssign:
				return ">>=";
			case JSToken.UnsignedRightShiftAssign:
				return ">>>=";
			case JSToken.Const:
				return "const";
			case JSToken.Let:
				return "let";
			case JSToken.Yield:
				return "yield";
			case JSToken.Get:
				return "get";
			case JSToken.Set:
				return "set";
			}
			return string.Empty;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003C7C2 File Offset: 0x0003A9C2
		private void AcceptNodeWithParens(AstNode node, bool needsParens)
		{
			if (needsParens)
			{
				this.OutputPossibleLineBreak('(');
				this.m_startOfStatement = false;
				this.m_noIn = false;
			}
			node.Accept(this);
			if (needsParens)
			{
				this.Output(')');
			}
			this.m_startOfStatement = false;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003C810 File Offset: 0x0003AA10
		private void OutputFunctionArgsAndBody(FunctionObject node)
		{
			if (node != null)
			{
				if (node.ParameterDeclarations != null)
				{
					this.Indent();
					bool flag;
					if (node.FunctionType == FunctionType.ArrowFunction && node.ParameterDeclarations.Count == 1)
					{
						flag = (node.ParameterDeclarations[0] as ParameterDeclaration).IfNotNull((ParameterDeclaration d) => d.HasRest, true);
					}
					else
					{
						flag = true;
					}
					bool flag2 = flag;
					if (flag2)
					{
						this.m_startOfStatement = false;
						this.OutputPossibleLineBreak('(');
						this.MarkSegment(node, null, node.ParameterDeclarations.Context);
					}
					AstNode astNode = null;
					for (int i = 0; i < node.ParameterDeclarations.Count; i++)
					{
						if (i > 0)
						{
							this.OutputPossibleLineBreak(',');
							this.MarkSegment(node, null, astNode.IfNotNull((AstNode p) => p.TerminatingContext) ?? node.ParameterDeclarations.Context);
							if (this.m_settings.OutputMode == OutputMode.MultipleLines)
							{
								this.OutputPossibleLineBreak(' ');
							}
						}
						astNode = node.ParameterDeclarations[i];
						if (astNode != null)
						{
							astNode.Accept(this);
						}
					}
					this.Unindent();
					if (flag2)
					{
						this.OutputPossibleLineBreak(')');
						this.MarkSegment(node, null, node.ParameterDeclarations.Context);
					}
				}
				else if (node.FunctionType == FunctionType.ArrowFunction)
				{
					this.OutputPossibleLineBreak('(');
					this.OutputPossibleLineBreak(')');
					this.m_startOfStatement = false;
				}
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.OutputPossibleLineBreak(' ');
					}
					this.Output(OutputVisitor.OperatorString(JSToken.ArrowFunction));
					if (this.m_settings.OutputMode == OutputMode.MultipleLines)
					{
						this.OutputPossibleLineBreak(' ');
					}
				}
				if (node.Body == null || node.Body.Count == 0)
				{
					this.Output("{}");
					this.MarkSegment(node, null, node.Body.IfNotNull((Block b) => b.Context));
					this.BreakLine(false);
					return;
				}
				if (node.FunctionType == FunctionType.ArrowFunction && node.Body.Count == 1 && node.Body.IsConcise)
				{
					node.Body[0].Accept(this);
					return;
				}
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					this.Indent();
				}
				if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && node.Body.BraceOnNewLine))
				{
					this.NewLine();
				}
				else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
				{
					this.OutputPossibleLineBreak(' ');
				}
				node.Body.Accept(this);
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					this.Unindent();
				}
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
		private void OutputBlock(Block block)
		{
			if (block != null && block.ForceBraces)
			{
				this.OutputBlockWithBraces(block);
				return;
			}
			if (block == null || block.Count == 0)
			{
				this.OutputPossibleLineBreak(';');
				this.MarkSegment(block, null, block.IfNotNull((Block b) => b.Context));
				return;
			}
			if (block.Count == 1)
			{
				this.Indent();
				this.NewLine();
				if (block[0] is ImportantComment)
				{
					block[0].Accept(this);
					this.OutputPossibleLineBreak(';');
					this.MarkSegment(block, null, block.Context);
				}
				else
				{
					this.m_startOfStatement = true;
					block[0].Accept(this);
				}
				this.Unindent();
				return;
			}
			this.OutputBlockWithBraces(block);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003CB94 File Offset: 0x0003AD94
		private void OutputBlockWithBraces(Block block)
		{
			if (this.m_settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.m_settings.BlocksStartOnSameLine == BlockStart.UseSource && block.BraceOnNewLine))
			{
				this.NewLine();
			}
			else if (this.m_settings.OutputMode == OutputMode.MultipleLines)
			{
				this.OutputPossibleLineBreak(' ');
			}
			block.Accept(this);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003CBEC File Offset: 0x0003ADEC
		private string InlineSafeString(string text)
		{
			if (this.m_settings.InlineSafeStrings)
			{
				if (text.IndexOf("</", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					text = text.Replace("</", "<\\/");
				}
				if (text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				{
					text = text.Replace("]]>", "]\\]>");
				}
			}
			return text;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003CC4C File Offset: 0x0003AE4C
		public static string NormalizeNumber(double numericValue, Context originalContext)
		{
			if (double.IsNaN(numericValue) || double.IsInfinity(numericValue))
			{
				if (originalContext != null && !string.IsNullOrEmpty(originalContext.Code) && !originalContext.Document.IsGenerated)
				{
					return originalContext.Code;
				}
				string result = double.IsNaN(numericValue) ? "NaN" : "Infinity";
				if (!double.IsNegativeInfinity(numericValue))
				{
					return result;
				}
				return "-Infinity";
			}
			else
			{
				if (numericValue != 0.0)
				{
					string text = OutputVisitor.GetSmallestRep(numericValue.ToStringInvariant("R"));
					if (Math.Floor(numericValue) == numericValue)
					{
						string text2 = OutputVisitor.NormalOrHexIfSmaller(numericValue, text);
						if (text2.Length < text.Length)
						{
							text = text2;
						}
					}
					return text;
				}
				if (1.0 / numericValue >= 0.0)
				{
					return "0";
				}
				return "-0";
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003CD14 File Offset: 0x0003AF14
		private static string GetSmallestRep(string number)
		{
			Match match = CommonData.DecimalFormat.Match(number);
			if (match.Success)
			{
				string text = match.Result("${man}");
				int num;
				if (string.IsNullOrEmpty(match.Result("${exp}")))
				{
					if (string.IsNullOrEmpty(text))
					{
						if (string.IsNullOrEmpty(match.Result("${sig}")))
						{
							number = match.Result("${neg}") + "0";
						}
						else
						{
							int length = match.Result("${zer}").Length;
							if (length > 2)
							{
								number = string.Concat(new object[]
								{
									match.Result("${neg}"),
									match.Result("${sig}"),
									'e',
									length.ToStringInvariant()
								});
							}
						}
					}
					else
					{
						number = string.Concat(new object[]
						{
							match.Result("${neg}"),
							match.Result("${mag}"),
							'.',
							text
						});
					}
				}
				else if (string.IsNullOrEmpty(text))
				{
					number = string.Concat(new string[]
					{
						match.Result("${neg}"),
						match.Result("${mag}"),
						"e",
						match.Result("${eng}"),
						match.Result("${pow}")
					});
				}
				else if ((match.Result("${eng}") + match.Result("${pow}")).TryParseIntInvariant(NumberStyles.Integer, out num))
				{
					number = string.Concat(new object[]
					{
						match.Result("${neg}"),
						match.Result("${mag}"),
						text,
						'e',
						(num - text.Length).ToStringInvariant()
					});
				}
				else
				{
					number = string.Concat(new object[]
					{
						match.Result("${neg}"),
						match.Result("${mag}"),
						'.',
						text,
						'e',
						match.Result("${eng}"),
						match.Result("${pow}")
					});
				}
			}
			return number;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003CF7C File Offset: 0x0003B17C
		private static string NormalOrHexIfSmaller(double doubleValue, string normal)
		{
			int num = normal.Length - 2;
			int num2 = Math.Sign(doubleValue);
			if (num2 < 0)
			{
				doubleValue = -doubleValue;
				num--;
			}
			char[] array = new char[normal.Length - 1];
			int num3 = array.Length;
			while (num > 0 && doubleValue > 0.0)
			{
				int num4 = (int)(doubleValue % 16.0);
				array[--num3] = (char)(((num4 < 10) ? 48 : 87) + num4);
				doubleValue = Math.Floor(doubleValue / 16.0);
				num--;
			}
			if (num > 0)
			{
				array[--num3] = 'x';
				array[--num3] = '0';
				if (num2 < 0)
				{
					array[--num3] = '-';
				}
				normal = new string(array, num3, array.Length - num3);
			}
			return normal;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003D038 File Offset: 0x0003B238
		public static string EscapeString(string text)
		{
			char c = (OutputVisitor.QuoteFactor(text) < 0) ? '\'' : '"';
			int num = 0;
			StringBuilder stringBuilder = null;
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				int i = 0;
				while (i < text.Length)
				{
					char c2 = text[i];
					char c3 = c2;
					if (c3 <= '"')
					{
						switch (c3)
						{
						case '\b':
							c2 = 'b';
							goto IL_B9;
						case '\t':
							c2 = 't';
							goto IL_B9;
						case '\n':
							c2 = 'n';
							goto IL_B9;
						case '\v':
							goto IL_13B;
						case '\f':
							c2 = 'f';
							goto IL_B9;
						case '\r':
							c2 = 'r';
							goto IL_B9;
						default:
							if (c3 != '"')
							{
								goto IL_13B;
							}
							goto IL_93;
						}
					}
					else
					{
						if (c3 == '\'')
						{
							goto IL_93;
						}
						if (c3 == '\\')
						{
							goto IL_B9;
						}
						switch (c3)
						{
						case '\u2028':
						case '\u2029':
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							if (num < i)
							{
								stringBuilder.Append(text.Substring(num, i - num));
							}
							num = i + 1;
							stringBuilder.Append("\\u");
							stringBuilder.Append(((int)c2).ToStringInvariant("x4"));
							break;
						default:
							goto IL_13B;
						}
					}
					IL_189:
					i++;
					continue;
					IL_93:
					if (c2 != c)
					{
						goto IL_189;
					}
					IL_B9:
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					if (num < i)
					{
						stringBuilder.Append(text.Substring(num, i - num));
					}
					num = i + 1;
					stringBuilder.Append('\\');
					stringBuilder.Append(c2);
					goto IL_189;
					IL_13B:
					if (c2 < ' ')
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						if (num < i)
						{
							stringBuilder.Append(text.Substring(num, i - num));
						}
						num = i + 1;
						int number = (int)c2;
						stringBuilder.Append("\\x");
						stringBuilder.Append(number.ToStringInvariant("x2"));
						goto IL_189;
					}
					goto IL_189;
				}
				if (stringBuilder != null)
				{
					if (num < text.Length)
					{
						stringBuilder.Append(text.Substring(num));
					}
					arg = stringBuilder.ToString();
				}
				else
				{
					arg = text;
				}
			}
			return c + arg + c;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003D218 File Offset: 0x0003B418
		private static int QuoteFactor(string text)
		{
			int num = 0;
			if (!text.IsNullOrWhiteSpace())
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '\'')
					{
						num++;
					}
					else if (text[i] == '"')
					{
						num--;
					}
				}
			}
			return num;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003D261 File Offset: 0x0003B461
		private object StartSymbol(AstNode node)
		{
			if (this.m_settings.SymbolsMap != null)
			{
				return this.m_settings.SymbolsMap.StartSymbol(node, this.m_lineCount, this.m_lineLength);
			}
			return null;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003D28F File Offset: 0x0003B48F
		private void MarkSegment(AstNode node, string name, Context context)
		{
			if (this.m_settings.SymbolsMap != null && node != null)
			{
				this.m_settings.SymbolsMap.MarkSegment(node, this.m_segmentStartLine, this.m_segmentStartColumn, name, context);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
		private void EndSymbol(object symbol)
		{
			if (this.m_settings.SymbolsMap != null && symbol != null)
			{
				string parentContext = null;
				if (this.m_functionStack.Count > 0)
				{
					parentContext = this.m_functionStack.Peek();
				}
				this.m_settings.SymbolsMap.EndSymbol(symbol, this.m_lineCount, this.m_lineLength, parentContext);
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0003D317 File Offset: 0x0003B517
		private void SetContextOutputPosition(Context context)
		{
			if (context != null)
			{
				context.OutputLine = this.m_segmentStartLine + 1;
				context.OutputColumn = this.m_segmentStartColumn;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003D336 File Offset: 0x0003B536
		private static void SetContextOutputPosition(Context context, Context fromContext)
		{
			if (context != null && fromContext != null)
			{
				context.OutputLine = fromContext.OutputLine;
				context.OutputColumn = fromContext.OutputColumn;
			}
		}

		// Token: 0x040004EF RID: 1263
		private TextWriter m_outputStream;

		// Token: 0x040004F0 RID: 1264
		private char m_lastCharacter;

		// Token: 0x040004F1 RID: 1265
		private bool m_lastCountOdd;

		// Token: 0x040004F2 RID: 1266
		private bool m_onNewLine;

		// Token: 0x040004F3 RID: 1267
		private bool m_startOfStatement;

		// Token: 0x040004F4 RID: 1268
		private bool m_outputCCOn;

		// Token: 0x040004F5 RID: 1269
		private bool m_doneWithGlobalDirectives;

		// Token: 0x040004F6 RID: 1270
		private bool m_needsStrictDirective;

		// Token: 0x040004F7 RID: 1271
		private bool m_noLineBreaks;

		// Token: 0x040004F8 RID: 1272
		private int m_indentLevel;

		// Token: 0x040004F9 RID: 1273
		private int m_lineLength;

		// Token: 0x040004FA RID: 1274
		private int m_lineCount;

		// Token: 0x040004FB RID: 1275
		private Stack<string> m_functionStack = new Stack<string>();

		// Token: 0x040004FC RID: 1276
		private int m_segmentStartLine;

		// Token: 0x040004FD RID: 1277
		private int m_segmentStartColumn;

		// Token: 0x040004FE RID: 1278
		private Func<char, bool> m_addSpaceIfTrue;

		// Token: 0x040004FF RID: 1279
		private bool m_noIn;

		// Token: 0x04000500 RID: 1280
		private bool m_hasReplacementTokens;

		// Token: 0x04000501 RID: 1281
		private CodeSettings m_settings;

		// Token: 0x04000502 RID: 1282
		private RequiresSeparatorVisitor m_requiresSeparator;
	}
}
