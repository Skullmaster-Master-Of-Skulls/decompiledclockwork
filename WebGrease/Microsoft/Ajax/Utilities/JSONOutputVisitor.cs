using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000A0 RID: 160
	public class JSONOutputVisitor : IVisitor
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002B222 File Offset: 0x00029422
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002B22A File Offset: 0x0002942A
		public bool IsValid { get; private set; }

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002B233 File Offset: 0x00029433
		private JSONOutputVisitor(TextWriter writer, CodeSettings settings)
		{
			this.m_writer = writer;
			this.m_settings = settings;
			this.IsValid = true;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002B250 File Offset: 0x00029450
		public static bool Apply(TextWriter writer, AstNode node, CodeSettings settings)
		{
			if (node != null)
			{
				JSONOutputVisitor jsonoutputVisitor = new JSONOutputVisitor(writer, settings);
				node.Accept(jsonoutputVisitor);
				return jsonoutputVisitor.IsValid;
			}
			return false;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002B278 File Offset: 0x00029478
		public void Visit(ArrayLiteral node)
		{
			if (node != null)
			{
				bool flag = false;
				if (this.m_settings.OutputMode == OutputMode.MultipleLines && (node.Elements.Count > 5 || JSONOutputVisitor.NotJustPrimitives(node.Elements)))
				{
					flag = true;
				}
				this.m_writer.Write('[');
				if (node.Elements != null)
				{
					if (flag)
					{
						this.m_settings.Indent();
						try
						{
							bool flag2 = true;
							foreach (AstNode astNode in node.Elements)
							{
								if (flag2)
								{
									flag2 = false;
								}
								else
								{
									this.m_writer.Write(',');
								}
								this.NewLine();
								astNode.Accept(this);
							}
						}
						finally
						{
							this.m_settings.Unindent();
						}
						this.NewLine();
					}
					else
					{
						node.Elements.Accept(this);
					}
				}
				this.m_writer.Write(']');
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002B374 File Offset: 0x00029574
		public void Visit(AstNodeList node)
		{
			if (node != null)
			{
				for (int i = 0; i < node.Count; i++)
				{
					if (i > 0)
					{
						this.m_writer.Write(',');
						if (this.m_settings.OutputMode == OutputMode.MultipleLines)
						{
							this.m_writer.Write(' ');
						}
					}
					if (node[i] != null)
					{
						node[i].Accept(this);
					}
				}
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002B3D7 File Offset: 0x000295D7
		public void Visit(Block node)
		{
			if (node != null && node.Count > 0)
			{
				node[0].Accept(this);
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002B3F4 File Offset: 0x000295F4
		public void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				switch (node.PrimitiveType)
				{
				case PrimitiveType.Null:
					this.m_writer.Write("null");
					return;
				case PrimitiveType.Boolean:
					this.m_writer.Write(((bool)node.Value) ? "true" : "false");
					return;
				case PrimitiveType.Number:
					this.OutputNumber((double)node.Value, node.Context);
					return;
				case PrimitiveType.String:
				case PrimitiveType.Other:
					this.OutputString(node.Value.ToString());
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002B488 File Offset: 0x00029688
		public void Visit(CustomNode node)
		{
			if (node != null)
			{
				this.OutputString(node.ToCode());
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002B499 File Offset: 0x00029699
		public void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				if (node.OperatorToken == JSToken.Minus)
				{
					this.m_writer.Write('-');
					if (node.Operand != null)
					{
						node.Operand.Accept(this);
						return;
					}
				}
				else
				{
					this.IsValid = false;
				}
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002B4D4 File Offset: 0x000296D4
		public void Visit(ObjectLiteral node)
		{
			if (node != null)
			{
				this.m_writer.Write('{');
				if (node.Properties != null)
				{
					bool flag = false;
					if (this.m_settings.OutputMode == OutputMode.MultipleLines && (node.Properties.Count > 5 || JSONOutputVisitor.NotJustPrimitives(node.Properties)))
					{
						flag = true;
					}
					if (flag)
					{
						this.m_settings.Indent();
						try
						{
							bool flag2 = true;
							foreach (AstNode astNode in node.Properties)
							{
								if (flag2)
								{
									flag2 = false;
								}
								else
								{
									this.m_writer.Write(',');
								}
								this.NewLine();
								astNode.Accept(this);
							}
						}
						finally
						{
							this.m_settings.Unindent();
						}
						this.NewLine();
					}
					else
					{
						node.Properties.Accept(this);
					}
				}
				this.m_writer.Write('}');
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002B5D4 File Offset: 0x000297D4
		public void Visit(ObjectLiteralField node)
		{
			if (node != null)
			{
				if (node.PrimitiveType == PrimitiveType.String)
				{
					this.OutputString(node.Value.ToString());
					return;
				}
				this.m_writer.Write('"');
				this.Visit(node);
				this.m_writer.Write('"');
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002B620 File Offset: 0x00029820
		public void Visit(ObjectLiteralProperty node)
		{
			if (node != null)
			{
				if (node.Name != null)
				{
					node.Name.Accept(this);
				}
				this.m_writer.Write(':');
				if (node.Value != null)
				{
					node.Value.Accept(this);
				}
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002B65A File Offset: 0x0002985A
		public void Visit(AspNetBlockNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002B663 File Offset: 0x00029863
		public void Visit(BinaryOperator node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002B66C File Offset: 0x0002986C
		public void Visit(BindingIdentifier node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002B675 File Offset: 0x00029875
		public void Visit(Break node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002B67E File Offset: 0x0002987E
		public void Visit(ClassNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002B687 File Offset: 0x00029887
		public void Visit(ComprehensionNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002B690 File Offset: 0x00029890
		public void Visit(ComprehensionForClause node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002B699 File Offset: 0x00029899
		public void Visit(ComprehensionIfClause node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002B6A2 File Offset: 0x000298A2
		public void Visit(CallNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002B6AB File Offset: 0x000298AB
		public void Visit(ConditionalCompilationComment node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002B6B4 File Offset: 0x000298B4
		public void Visit(ConditionalCompilationElse node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002B6BD File Offset: 0x000298BD
		public void Visit(ConditionalCompilationElseIf node)
		{
			this.IsValid = false;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002B6C6 File Offset: 0x000298C6
		public void Visit(ConditionalCompilationEnd node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002B6CF File Offset: 0x000298CF
		public void Visit(ConditionalCompilationIf node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002B6D8 File Offset: 0x000298D8
		public void Visit(ConditionalCompilationOn node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002B6E1 File Offset: 0x000298E1
		public void Visit(ConditionalCompilationSet node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002B6EA File Offset: 0x000298EA
		public void Visit(Conditional node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002B6F3 File Offset: 0x000298F3
		public void Visit(ConstantWrapperPP node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002B6FC File Offset: 0x000298FC
		public void Visit(ConstStatement node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002B705 File Offset: 0x00029905
		public void Visit(ContinueNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002B70E File Offset: 0x0002990E
		public void Visit(DebuggerNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002B717 File Offset: 0x00029917
		public void Visit(DirectivePrologue node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002B720 File Offset: 0x00029920
		public void Visit(DoWhile node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002B729 File Offset: 0x00029929
		public void Visit(EmptyStatement node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002B732 File Offset: 0x00029932
		public void Visit(ExportNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002B73B File Offset: 0x0002993B
		public void Visit(ForIn node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002B744 File Offset: 0x00029944
		public void Visit(ForNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002B74D File Offset: 0x0002994D
		public void Visit(FunctionObject node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002B756 File Offset: 0x00029956
		public void Visit(GetterSetter node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002B75F File Offset: 0x0002995F
		public void Visit(GroupingOperator node)
		{
			this.IsValid = false;
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002B77F File Offset: 0x0002997F
		public void Visit(IfNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002B788 File Offset: 0x00029988
		public void Visit(ImportantComment node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002B791 File Offset: 0x00029991
		public void Visit(ImportExportSpecifier node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002B79A File Offset: 0x0002999A
		public void Visit(ImportNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002B7A3 File Offset: 0x000299A3
		public void Visit(InitializerNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002B7AC File Offset: 0x000299AC
		public void Visit(LabeledStatement node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002B7B5 File Offset: 0x000299B5
		public void Visit(LexicalDeclaration node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002B7BE File Offset: 0x000299BE
		public void Visit(Lookup node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002B7C7 File Offset: 0x000299C7
		public void Visit(Member node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002B7D0 File Offset: 0x000299D0
		public void Visit(ModuleDeclaration node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002B7D9 File Offset: 0x000299D9
		public void Visit(ParameterDeclaration node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002B7E2 File Offset: 0x000299E2
		public void Visit(RegExpLiteral node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002B7EB File Offset: 0x000299EB
		public void Visit(ReturnNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002B7F4 File Offset: 0x000299F4
		public void Visit(Switch node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002B7FD File Offset: 0x000299FD
		public void Visit(SwitchCase node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002B806 File Offset: 0x00029A06
		public void Visit(TemplateLiteral node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002B80F File Offset: 0x00029A0F
		public void Visit(TemplateLiteralExpression node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002B818 File Offset: 0x00029A18
		public void Visit(ThisLiteral node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002B821 File Offset: 0x00029A21
		public void Visit(ThrowNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002B82A File Offset: 0x00029A2A
		public void Visit(TryNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002B833 File Offset: 0x00029A33
		public void Visit(Var node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002B83C File Offset: 0x00029A3C
		public void Visit(VariableDeclaration node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002B845 File Offset: 0x00029A45
		public void Visit(WhileNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002B84E File Offset: 0x00029A4E
		public void Visit(WithNode node)
		{
			this.IsValid = false;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002B858 File Offset: 0x00029A58
		private void OutputString(string text)
		{
			this.m_writer.Write('"');
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				char c2 = c;
				switch (c2)
				{
				case '\b':
					this.m_writer.Write("\\b");
					break;
				case '\t':
					this.m_writer.Write("\\t");
					break;
				case '\n':
					this.m_writer.Write("\\n");
					break;
				case '\v':
					goto IL_B2;
				case '\f':
					this.m_writer.Write("\\f");
					break;
				case '\r':
					this.m_writer.Write("\\r");
					break;
				default:
					if (c2 != '"')
					{
						goto IL_B2;
					}
					this.m_writer.Write("\\\"");
					break;
				}
				IL_DB:
				i++;
				continue;
				IL_B2:
				if (c < ' ')
				{
					this.m_writer.Write("\\u{0:x4}", (int)c);
					goto IL_DB;
				}
				this.m_writer.Write(c);
				goto IL_DB;
			}
			this.m_writer.Write('"');
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002B960 File Offset: 0x00029B60
		public void OutputNumber(double numericValue, Context originalContext)
		{
			if (double.IsNaN(numericValue) || double.IsInfinity(numericValue))
			{
				if (originalContext != null && !string.IsNullOrEmpty(originalContext.Code) && !originalContext.Document.IsGenerated)
				{
					this.m_writer.Write(originalContext.Code);
					return;
				}
				string text = double.IsNaN(numericValue) ? "NaN" : "Infinity";
				this.m_writer.Write(double.IsNegativeInfinity(numericValue) ? "-Infinity" : text);
				return;
			}
			else
			{
				if (numericValue == 0.0)
				{
					this.m_writer.Write((1.0 / numericValue < 0.0) ? "-0" : "0");
					return;
				}
				this.m_writer.Write(JSONOutputVisitor.GetSmallestRep(numericValue.ToString("R", CultureInfo.InvariantCulture)));
				return;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002BA38 File Offset: 0x00029C38
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
									length.ToString(CultureInfo.InvariantCulture)
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
				else if (int.TryParse(match.Result("${eng}") + match.Result("${pow}"), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					number = string.Concat(new object[]
					{
						match.Result("${neg}"),
						match.Result("${mag}"),
						text,
						'e',
						(num - text.Length).ToString(CultureInfo.InvariantCulture)
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

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		private static bool NotJustPrimitives(AstNodeList nodeList)
		{
			foreach (AstNode astNode in nodeList)
			{
				if (!(astNode is ConstantWrapper) && !(astNode is UnaryOperator))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002BD0C File Offset: 0x00029F0C
		private void NewLine()
		{
			this.m_writer.WriteLine();
			this.m_writer.Write(this.m_settings.TabSpaces);
		}

		// Token: 0x040003CF RID: 975
		private TextWriter m_writer;

		// Token: 0x040003D0 RID: 976
		private CodeSettings m_settings;
	}
}
