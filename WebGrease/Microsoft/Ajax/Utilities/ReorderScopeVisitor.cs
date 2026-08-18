using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BF RID: 191
	internal class ReorderScopeVisitor : TreeVisitor
	{
		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003D618 File Offset: 0x0003B818
		private ReorderScopeVisitor(JSParser parser)
		{
			CodeSettings settings = parser.Settings;
			this.m_moveVarStatements = (settings.ReorderScopeDeclarations && settings.IsModificationAllowed(TreeModifications.CombineVarStatementsToTopOfScope));
			this.m_moveFunctionDecls = (settings.ReorderScopeDeclarations && settings.IsModificationAllowed(TreeModifications.MoveFunctionToTopOfScope));
			this.m_combineAdjacentVars = settings.IsModificationAllowed(TreeModifications.CombineVarStatements);
			this.m_globalScope = parser.GlobalScope;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003D690 File Offset: 0x0003B890
		public static void Apply(Block block, JSParser parser)
		{
			if (parser == null)
			{
				throw new ArgumentNullException("parser");
			}
			if (block != null)
			{
				ReorderScopeVisitor reorderScopeVisitor = new ReorderScopeVisitor(parser);
				block.Accept(reorderScopeVisitor);
				int num = 0;
				if (reorderScopeVisitor.m_moduleDirectives == null)
				{
					goto IL_69;
				}
				using (List<DirectivePrologue>.Enumerator enumerator = reorderScopeVisitor.m_moduleDirectives.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DirectivePrologue directivePrologue = enumerator.Current;
						num = ReorderScopeVisitor.RelocateDirectivePrologue(block, num, directivePrologue);
					}
					goto IL_69;
				}
				IL_65:
				num++;
				IL_69:
				if (num < block.Count && (block[num] is DirectivePrologue || block[num] is ImportantComment))
				{
					goto IL_65;
				}
				if (reorderScopeVisitor.m_functionDeclarations != null)
				{
					foreach (FunctionObject funcDecl in reorderScopeVisitor.m_functionDeclarations)
					{
						num = ReorderScopeVisitor.RelocateFunction(block, num, funcDecl);
					}
				}
				if (reorderScopeVisitor.m_varStatements != null && reorderScopeVisitor.m_varStatements.Count > 1)
				{
					foreach (Var varStatement in reorderScopeVisitor.m_varStatements)
					{
						num = ReorderScopeVisitor.RelocateVar(block, num, varStatement);
					}
				}
				if (reorderScopeVisitor.m_functionDeclarations != null)
				{
					foreach (FunctionObject functionObject in reorderScopeVisitor.m_functionDeclarations)
					{
						ReorderScopeVisitor.Apply(functionObject.Body, parser);
					}
				}
				if (reorderScopeVisitor.m_functionExpressions != null)
				{
					foreach (FunctionObject functionObject2 in reorderScopeVisitor.m_functionExpressions)
					{
						ReorderScopeVisitor.Apply(functionObject2.Body, parser);
					}
				}
				if (reorderScopeVisitor.m_moduleDeclarations != null)
				{
					foreach (ModuleDeclaration moduleDeclaration in reorderScopeVisitor.m_moduleDeclarations)
					{
						ReorderScopeVisitor.Apply(moduleDeclaration.Body, parser);
					}
				}
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003D8DC File Offset: 0x0003BADC
		private static int RelocateDirectivePrologue(Block block, int insertAt, DirectivePrologue directivePrologue)
		{
			while (insertAt < block.Count && block[insertAt] is ImportantComment)
			{
				insertAt++;
			}
			if (block[insertAt] != directivePrologue)
			{
				directivePrologue.Parent.ReplaceChild(directivePrologue, null);
				block.Insert(insertAt, directivePrologue);
			}
			return ++insertAt;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003D930 File Offset: 0x0003BB30
		private static int RelocateFunction(Block block, int insertAt, AstNode funcDecl)
		{
			if (funcDecl.Parent is ExportNode)
			{
				funcDecl = funcDecl.Parent;
			}
			if (block[insertAt] != funcDecl)
			{
				if (funcDecl.Parent == block)
				{
					funcDecl.Parent.ReplaceChild(funcDecl, null);
					block.Insert(insertAt++, funcDecl);
				}
			}
			else
			{
				insertAt++;
			}
			return insertAt;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003D988 File Offset: 0x0003BB88
		private static int RelocateVar(Block block, int insertAt, Var varStatement)
		{
			ForIn forIn = varStatement.Parent as ForIn;
			if (forIn != null)
			{
				insertAt = ReorderScopeVisitor.RelocateForInVar(block, insertAt, varStatement, forIn);
			}
			else if (block[insertAt] != varStatement)
			{
				Var var = block[insertAt] as Var;
				ForNode forNode;
				if (var != null && block[insertAt + 1] == varStatement)
				{
					var.Append(varStatement);
					block.RemoveAt(insertAt + 1);
				}
				else if (var != null && (forNode = (varStatement.Parent as ForNode)) != null && forNode.Initializer == varStatement && forNode == block[insertAt + 1])
				{
					varStatement.InsertAt(0, var);
					block.RemoveAt(insertAt);
				}
				else
				{
					int num = 0;
					for (int i = 0; i < varStatement.Count; i++)
					{
						if (varStatement[i].Initializer != null)
						{
							num++;
						}
					}
					if (num <= 2)
					{
						List<AstNode> list = new List<AstNode>();
						for (int j = 0; j < varStatement.Count; j++)
						{
							VariableDeclaration variableDeclaration = varStatement[j];
							if (variableDeclaration.Initializer != null)
							{
								AstNode initializer = variableDeclaration.Initializer;
								variableDeclaration.Initializer = null;
								AstNode astNode = BindingTransform.FromBinding(variableDeclaration.Binding);
								if (variableDeclaration.IsCCSpecialCase)
								{
									list.Add(new VariableDeclaration(variableDeclaration.Context)
									{
										Binding = astNode,
										AssignContext = variableDeclaration.AssignContext,
										Initializer = initializer,
										IsCCSpecialCase = true,
										UseCCOn = variableDeclaration.UseCCOn,
										TerminatingContext = variableDeclaration.TerminatingContext
									});
								}
								else
								{
									list.Add(new BinaryOperator(variableDeclaration.Context)
									{
										Operand1 = astNode,
										Operand2 = initializer,
										OperatorToken = JSToken.Assign,
										OperatorContext = variableDeclaration.AssignContext
									});
								}
							}
							if (!(variableDeclaration.Binding is BindingIdentifier))
							{
								bool flag = true;
								foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(variableDeclaration.Binding))
								{
									if (flag)
									{
										varStatement[j] = new VariableDeclaration(bindingIdentifier.Context)
										{
											Binding = new BindingIdentifier(bindingIdentifier.Context)
											{
												Name = bindingIdentifier.Name,
												VariableField = bindingIdentifier.VariableField
											}
										};
										flag = false;
									}
									else
									{
										varStatement.InsertAt(++j, new VariableDeclaration(bindingIdentifier.Context)
										{
											Binding = new BindingIdentifier(bindingIdentifier.Context)
											{
												Name = bindingIdentifier.Name,
												VariableField = bindingIdentifier.VariableField
											}
										});
									}
								}
							}
						}
						if (list.Count > 0)
						{
							AstNode astNode2 = list[0];
							for (int k = 1; k < list.Count; k++)
							{
								astNode2 = CommaOperator.CombineWithComma(astNode2.Context.FlattenToStart(), astNode2, list[k]);
							}
							varStatement.Parent.ReplaceChild(varStatement, astNode2);
						}
						else
						{
							varStatement.Parent.ReplaceChild(varStatement, null);
						}
						if (var != null)
						{
							var.Append(varStatement);
						}
						else
						{
							block.Insert(insertAt, varStatement);
						}
					}
				}
			}
			return insertAt;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003DCDC File Offset: 0x0003BEDC
		private static int RelocateForInVar(Block block, int insertAt, Var varStatement, ForIn forIn)
		{
			VariableDeclaration variableDeclaration;
			if (varStatement.Count == 1 && (variableDeclaration = varStatement[0]).Initializer == null)
			{
				IList<BindingIdentifier> list = BindingsVisitor.Bindings(variableDeclaration.Binding);
				forIn.Variable = BindingTransform.FromBinding(variableDeclaration.Binding);
				if (!(variableDeclaration.Binding is BindingIdentifier))
				{
					bool flag = true;
					foreach (BindingIdentifier bindingIdentifier in list)
					{
						if (flag)
						{
							varStatement[0] = new VariableDeclaration(bindingIdentifier.Context)
							{
								Binding = new BindingIdentifier(bindingIdentifier.Context)
								{
									Name = bindingIdentifier.Name,
									VariableField = bindingIdentifier.VariableField
								}
							};
							flag = false;
						}
						else
						{
							varStatement.Append(new VariableDeclaration(bindingIdentifier.Context)
							{
								Binding = new BindingIdentifier(bindingIdentifier.Context)
								{
									Name = bindingIdentifier.Name,
									VariableField = bindingIdentifier.VariableField
								}
							});
						}
					}
				}
				Var var = block[insertAt] as Var;
				if (var != null)
				{
					var.Append(varStatement);
				}
				else
				{
					block.Insert(insertAt, varStatement);
				}
			}
			return insertAt;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003DE30 File Offset: 0x0003C030
		private static void UnnestBlocks(Block node)
		{
			for (int i = node.Count - 1; i >= 0; i--)
			{
				Block block = node[i] as Block;
				if (block != null)
				{
					ReorderScopeVisitor.UnnestBlocks(block);
					if (!block.HasOwnScope)
					{
						node.RemoveAt(i);
						node.InsertRange(i, block.Children);
					}
				}
				else if (node[i] is EmptyStatement)
				{
					node.RemoveAt(i);
				}
				else if (i > 0)
				{
					ConditionalCompilationComment conditionalCompilationComment = node[i - 1] as ConditionalCompilationComment;
					if (conditionalCompilationComment != null)
					{
						ConditionalCompilationComment conditionalCompilationComment2 = node[i] as ConditionalCompilationComment;
						if (conditionalCompilationComment2 != null)
						{
							conditionalCompilationComment.Statements.Append(conditionalCompilationComment2.Statements);
							node.RemoveAt(i);
						}
					}
				}
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0003DEE0 File Offset: 0x0003C0E0
		public override void Visit(Block node)
		{
			if (node != null)
			{
				ReorderScopeVisitor.UnnestBlocks(node);
				node.ForceBraces = (node.Parent is TryNode);
				if (this.m_combineAdjacentVars)
				{
					for (int i = node.Count - 1; i > 0; i--)
					{
						Declaration declaration = node[i - 1] as Declaration;
						if (declaration != null)
						{
							if (declaration.StatementToken == ReorderScopeVisitor.DeclarationType(node[i]))
							{
								declaration.Append(node[i]);
								node.RemoveAt(i);
							}
						}
						else
						{
							ExportNode exportNode = node[i - 1] as ExportNode;
							if (exportNode != null && exportNode.Count == 1 && exportNode.ModuleName.IsNullOrWhiteSpace())
							{
								JSToken jstoken = ReorderScopeVisitor.DeclarationType(exportNode[0]);
								if (jstoken != JSToken.None)
								{
									ExportNode exportNode2 = node[i] as ExportNode;
									if (exportNode2 != null && exportNode2.Count == 1 && exportNode2.ModuleName.IsNullOrWhiteSpace() && jstoken == ReorderScopeVisitor.DeclarationType(exportNode2[0]))
									{
										((Declaration)exportNode[0]).Append(exportNode2[0]);
										node.RemoveAt(i);
									}
								}
							}
						}
					}
				}
				if (node.IsModule)
				{
					int num = node.Count - 1;
					while (num >= 0 && node[num] is ImportantComment)
					{
						num--;
					}
					if (num > 0)
					{
						ExportNode exportNode3 = ReorderScopeVisitor.IfTargetExport(node[num]);
						int num2 = node.Count - ((exportNode3 == null) ? 1 : 2);
						List<ExportNode> list = new List<ExportNode>();
						for (int j = num2; j >= 0; j--)
						{
							ExportNode exportNode4 = node[j] as ExportNode;
							if (exportNode4 != null && exportNode4.ModuleName.IsNullOrWhiteSpace())
							{
								if (ReorderScopeVisitor.IfTargetExport(exportNode4) != null)
								{
									if (exportNode4 != exportNode3)
									{
										if (exportNode3 != null)
										{
											exportNode3.Insert(0, exportNode4);
											node.RemoveAt(j);
										}
										else
										{
											node.RemoveAt(j);
											node.Append(exportNode4);
											exportNode3 = exportNode4;
										}
									}
								}
								else if (exportNode4.Count == 1)
								{
									list.Add(exportNode4);
								}
							}
						}
						int count = list.Count;
					}
				}
				base.Visit(node);
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003E100 File Offset: 0x0003C300
		private static JSToken DeclarationType(AstNode node)
		{
			Declaration declaration = node as Declaration;
			if (declaration != null)
			{
				return declaration.StatementToken;
			}
			return JSToken.None;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0003E120 File Offset: 0x0003C320
		private static ExportNode IfTargetExport(AstNode node)
		{
			ExportNode exportNode = node as ExportNode;
			if (exportNode == null || !exportNode.ModuleName.IsNullOrWhiteSpace() || exportNode.Count <= 0 || !(exportNode[0] is ImportExportSpecifier))
			{
				return null;
			}
			return exportNode;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003E15E File Offset: 0x0003C35E
		public override void Visit(ConditionalCompilationComment node)
		{
			if (node != null && node.Statements != null && node.Statements.Count > 0)
			{
				this.m_conditionalCommentLevel++;
				base.Visit(node);
				this.m_conditionalCommentLevel--;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003E19C File Offset: 0x0003C39C
		public override void Visit(ConditionalCompilationIf node)
		{
			if (node != null)
			{
				this.m_conditionalCommentLevel++;
				base.Visit(node);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0003E1B6 File Offset: 0x0003C3B6
		public override void Visit(ConditionalCompilationEnd node)
		{
			if (node != null)
			{
				this.m_conditionalCommentLevel--;
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0003E1C9 File Offset: 0x0003C3C9
		public override void Visit(ConstantWrapper node)
		{
			if (node != null && node.Parent is Block && this.IsMinificationHint(node))
			{
				node.Parent.ReplaceChild(node, null);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003E1F2 File Offset: 0x0003C3F2
		public override void Visit(DirectivePrologue node)
		{
			if (node != null)
			{
				if (this.IsMinificationHint(node))
				{
					node.Parent.ReplaceChild(node, null);
					return;
				}
				if (this.m_moduleDirectives == null)
				{
					this.m_moduleDirectives = new List<DirectivePrologue>();
				}
				this.m_moduleDirectives.Add(node);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0003E230 File Offset: 0x0003C430
		public override void Visit(FunctionObject node)
		{
			if (node != null)
			{
				if (this.m_moveVarStatements || this.m_moveFunctionDecls)
				{
					if (node.FunctionType == FunctionType.Declaration && this.m_conditionalCommentLevel == 0)
					{
						if (this.m_functionDeclarations == null)
						{
							this.m_functionDeclarations = new List<FunctionObject>();
						}
						this.m_functionDeclarations.Add(node);
						return;
					}
					if (this.m_functionExpressions == null)
					{
						this.m_functionExpressions = new List<FunctionObject>();
					}
					this.m_functionExpressions.Add(node);
					return;
				}
				else
				{
					base.Visit(node);
				}
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003E2A7 File Offset: 0x0003C4A7
		public override void Visit(Var node)
		{
			if (node != null)
			{
				if (this.m_moveVarStatements && this.m_conditionalCommentLevel == 0)
				{
					if (this.m_varStatements == null)
					{
						this.m_varStatements = new List<Var>();
					}
					this.m_varStatements.Add(node);
				}
				base.Visit(node);
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		public override void Visit(GroupingOperator node)
		{
			if (node != null)
			{
				if (node.Parent != null)
				{
					bool flag = false;
					if (node.Operand == null)
					{
						flag = true;
					}
					else if (node.Parent is Block)
					{
						if (!(node.Operand is FunctionObject) && !(node.Operand is ObjectLiteral))
						{
							flag = true;
						}
					}
					else if (node.Parent is AstNodeList)
					{
						BinaryOperator binaryOperator = node.Operand as BinaryOperator;
						if (binaryOperator == null || binaryOperator.OperatorToken != JSToken.Comma)
						{
							flag = true;
						}
					}
					else if (node.Parent.IsExpression)
					{
						OperatorPrecedence operatorPrecedence = node.Parent.Precedence;
						Conditional conditional = node.Parent as Conditional;
						if (conditional != null)
						{
							operatorPrecedence = ((conditional.Condition == node) ? OperatorPrecedence.LogicalOr : OperatorPrecedence.Assignment);
						}
						if (operatorPrecedence <= node.Operand.Precedence)
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						node.Parent.ReplaceChild(node, node.Operand);
					}
				}
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003E3DF File Offset: 0x0003C5DF
		public override void Visit(ModuleDeclaration node)
		{
			if (node != null && node.Body != null)
			{
				if (this.m_moduleDeclarations == null)
				{
					this.m_moduleDeclarations = new List<ModuleDeclaration>();
				}
				this.m_moduleDeclarations.Add(node);
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003E40C File Offset: 0x0003C60C
		private bool IsMinificationHint(ConstantWrapper node)
		{
			bool result = false;
			if (node.PrimitiveType == PrimitiveType.String)
			{
				string[] array = node.ToString().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					int num = text.IndexOf(':');
					if (num >= 0 && string.Compare(text.Substring(num + 1).Trim(), "nomunge", StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = true;
						string text2 = text.Substring(0, num).Trim();
						if (string.IsNullOrEmpty(text2) || string.CompareOrdinal(text2, "*") == 0)
						{
							text2 = null;
						}
						ActivationObject activationObject = node.EnclosingScope ?? this.m_globalScope;
						foreach (JSVariableField jsvariableField in activationObject.NameTable.Values)
						{
							if (jsvariableField.OuterField == null && (text2 == null || string.CompareOrdinal(text2, jsvariableField.Name) == 0))
							{
								jsvariableField.CanCrunch = false;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000519 RID: 1305
		private List<FunctionObject> m_functionDeclarations;

		// Token: 0x0400051A RID: 1306
		private List<FunctionObject> m_functionExpressions;

		// Token: 0x0400051B RID: 1307
		private List<ModuleDeclaration> m_moduleDeclarations;

		// Token: 0x0400051C RID: 1308
		private List<DirectivePrologue> m_moduleDirectives;

		// Token: 0x0400051D RID: 1309
		private List<Var> m_varStatements;

		// Token: 0x0400051E RID: 1310
		private bool m_moveVarStatements;

		// Token: 0x0400051F RID: 1311
		private bool m_moveFunctionDecls;

		// Token: 0x04000520 RID: 1312
		private bool m_combineAdjacentVars;

		// Token: 0x04000521 RID: 1313
		private int m_conditionalCommentLevel;

		// Token: 0x04000522 RID: 1314
		private GlobalScope m_globalScope;
	}
}
