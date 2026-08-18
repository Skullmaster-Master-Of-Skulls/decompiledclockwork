using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000062 RID: 98
	internal class AnalyzeNodeVisitor : TreeVisitor
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0001AFD8 File Offset: 0x000191D8
		public AnalyzeNodeVisitor(JSParser parser)
		{
			this.m_parser = parser;
			this.m_scopeStack = new Stack<ActivationObject>();
			this.m_scopeStack.Push(parser.GlobalScope);
			this.m_stripDebug = (this.m_parser.Settings.StripDebugStatements && this.m_parser.Settings.IsModificationAllowed(TreeModifications.StripDebugStatements));
			this.m_lookForDebugNamespaces = (this.m_stripDebug && this.m_parser.DebugLookups.Count > 0);
			if (this.m_lookForDebugNamespaces)
			{
				this.m_possibleDebugMatches = new List<string[]>();
				this.m_debugNamespaceParts = new string[this.m_parser.DebugLookups.Count][];
				int num = 0;
				foreach (string text in this.m_parser.DebugLookups)
				{
					this.m_debugNamespaceParts[num++] = text.Split(new char[]
					{
						'.'
					});
				}
			}
			if (this.m_parser.Settings.LocalRenaming != LocalRenaming.KeepAll)
			{
				this.m_noRename = new HashSet<string>(this.m_parser.Settings.NoAutoRenameCollection);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B128 File Offset: 0x00019328
		public override void Visit(BinaryOperator node)
		{
			if (node != null)
			{
				if (node.Operand1 != null)
				{
					node.Operand1.Accept(this);
				}
				if (node.Operand2 != null)
				{
					node.Operand2.Accept(this);
				}
				if ((node.Operand1 == null || node.Operand1.IsDebugOnly) && (node.Operand2 == null || node.Operand2.IsDebugOnly))
				{
					node.IsDebugOnly = true;
					return;
				}
				if (node.Operand1 != null && node.Operand1.IsDebugOnly)
				{
					node.Operand1 = AnalyzeNodeVisitor.ClearDebugExpression(node.Operand1);
				}
				if (node.Operand2 != null && node.Operand2.IsDebugOnly)
				{
					node.Operand2 = AnalyzeNodeVisitor.ClearDebugExpression(node.Operand2);
				}
				if (node.OperatorToken == JSToken.Minus && this.m_parser.Settings.IsModificationAllowed(TreeModifications.SimplifyStringToNumericConversion))
				{
					Lookup lookup = node.Operand1 as Lookup;
					if (lookup != null)
					{
						ConstantWrapper constantWrapper = node.Operand2 as ConstantWrapper;
						if (constantWrapper != null && constantWrapper.IsIntegerLiteral && constantWrapper.ToNumber() == 0.0)
						{
							UnaryOperator newNode = new UnaryOperator(node.Context)
							{
								Operand = lookup,
								OperatorToken = JSToken.FirstBinaryOperator
							};
							node.Parent.ReplaceChild(node, newNode);
							return;
						}
					}
				}
				else if ((node.OperatorToken == JSToken.StrictEqual || node.OperatorToken == JSToken.StrictNotEqual) && this.m_parser.Settings.IsModificationAllowed(TreeModifications.ReduceStrictOperatorIfTypesAreSame))
				{
					PrimitiveType primitiveType = node.Operand1.FindPrimitiveType();
					if (primitiveType != PrimitiveType.Other)
					{
						PrimitiveType primitiveType2 = node.Operand2.FindPrimitiveType();
						if (primitiveType == primitiveType2)
						{
							node.OperatorToken = ((node.OperatorToken == JSToken.StrictEqual) ? JSToken.Equal : JSToken.NotEqual);
							return;
						}
						if (primitiveType2 != PrimitiveType.Other)
						{
							node.Context.HandleError(JSError.StrictComparisonIsAlwaysTrueOrFalse, false);
							node.Parent.ReplaceChild(node, new ConstantWrapper(node.OperatorToken == JSToken.StrictNotEqual, PrimitiveType.Boolean, node.Context));
							DetachReferences.Apply(node);
							return;
						}
					}
				}
				else if (node.IsAssign)
				{
					Lookup lookup2 = node.Operand1 as Lookup;
					if (lookup2 != null)
					{
						if (lookup2.VariableField != null && lookup2.VariableField.InitializationOnly)
						{
							lookup2.Context.HandleError(JSError.AssignmentToConstant, true);
							return;
						}
						if (this.m_scopeStack.Peek().UseStrict)
						{
							if (lookup2.VariableField == null || lookup2.VariableField.FieldType == FieldType.UndefinedGlobal)
							{
								node.Operand1.Context.HandleError(JSError.StrictModeUndefinedVariable, true);
								return;
							}
							if (lookup2.VariableField.FieldType == FieldType.Arguments || (lookup2.VariableField.FieldType == FieldType.Predefined && string.CompareOrdinal(lookup2.Name, "eval") == 0))
							{
								node.Operand1.Context.HandleError(JSError.StrictModeInvalidAssign, true);
								return;
							}
						}
					}
				}
				else if ((node.Parent is Block || (node.Parent is CommaOperator && node.Parent.Parent is Block)) && (node.OperatorToken == JSToken.LogicalOr || node.OperatorToken == JSToken.LogicalAnd))
				{
					LogicalNot logicalNot = new LogicalNot(node.Operand1, this.m_parser.Settings);
					if (logicalNot.Measure() < 0)
					{
						logicalNot.Apply();
						node.OperatorToken = ((node.OperatorToken == JSToken.LogicalAnd) ? JSToken.LogicalOr : JSToken.LogicalAnd);
					}
				}
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B48C File Offset: 0x0001968C
		public override void Visit(BindingIdentifier node)
		{
			if (node != null)
			{
				AnalyzeNodeVisitor.ValidateIdentifier(this.m_scopeStack.Peek().UseStrict, node.Name, node.Context, this.m_strictNameError);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B4B8 File Offset: 0x000196B8
		private void CombineExpressions(Block node)
		{
			for (int i = node.Count - 1; i > 0; i--)
			{
				if (i < node.Count)
				{
					if (node[i - 1].IsExpression)
					{
						this.CombineWithPreviousExpression(node, i);
					}
					else
					{
						Var var = node[i - 1] as Var;
						if (var != null)
						{
							AnalyzeNodeVisitor.CombineWithPreviousVar(node, i, var);
						}
					}
				}
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B518 File Offset: 0x00019718
		private void CombineWithPreviousExpression(Block node, int ndx)
		{
			if (node[ndx].IsExpression)
			{
				AnalyzeNodeVisitor.CombineTwoExpressions(node, ndx);
				return;
			}
			ReturnNode returnNode;
			if ((returnNode = (node[ndx] as ReturnNode)) != null)
			{
				AnalyzeNodeVisitor.CombineReturnWithExpression(node, ndx, returnNode);
				return;
			}
			ForNode forNode;
			if ((forNode = (node[ndx] as ForNode)) != null)
			{
				this.CombineForNodeWithExpression(node, ndx, forNode);
				return;
			}
			IfNode ifNode;
			if ((ifNode = (node[ndx] as IfNode)) != null)
			{
				ifNode.Condition = CommaOperator.CombineWithComma(node[ndx - 1].Context.FlattenToStart(), node[ndx - 1], ifNode.Condition);
				node.RemoveAt(ndx - 1);
				return;
			}
			WhileNode whileNode;
			if ((whileNode = (node[ndx] as WhileNode)) != null && this.m_parser.Settings.IsModificationAllowed(TreeModifications.ChangeWhileToFor))
			{
				AstNode astNode = node[ndx - 1];
				node[ndx] = new ForNode(astNode.Context.FlattenToStart())
				{
					Initializer = astNode,
					Condition = whileNode.Condition,
					Body = whileNode.Body
				};
				node.RemoveAt(ndx - 1);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B634 File Offset: 0x00019834
		private static void CombineTwoExpressions(Block node, int ndx)
		{
			BinaryOperator binaryOperator = node[ndx - 1] as BinaryOperator;
			BinaryOperator binaryOperator2 = node[ndx] as BinaryOperator;
			Lookup lookup;
			if (binaryOperator == null || binaryOperator2 == null || !binaryOperator.IsAssign || !binaryOperator2.IsAssign || binaryOperator2.OperatorToken == JSToken.Assign || (lookup = (binaryOperator2.Operand1 as Lookup)) == null || !binaryOperator.Operand1.IsEquivalentTo(binaryOperator2.Operand1))
			{
				AstNode value = CommaOperator.CombineWithComma(node[ndx - 1].Context.Clone().CombineWith(node[ndx].Context), node[ndx - 1], node[ndx]);
				node[ndx] = value;
				node[ndx - 1] = null;
				return;
			}
			if (binaryOperator.OperatorToken == JSToken.Assign)
			{
				BinaryOperator operand = new BinaryOperator(binaryOperator.Operand2.Context.Clone().CombineWith(binaryOperator2.Operand2.Context))
				{
					Operand1 = binaryOperator.Operand2,
					Operand2 = binaryOperator2.Operand2,
					OperatorToken = JSScanner.StripAssignment(binaryOperator2.OperatorToken),
					OperatorContext = binaryOperator2.OperatorContext
				};
				binaryOperator.Operand2 = operand;
				if (lookup.VariableField != null)
				{
					lookup.VariableField.References.Remove(lookup);
				}
				node[ndx] = null;
				return;
			}
			AstNode value2 = CommaOperator.CombineWithComma(binaryOperator.Context.Clone().CombineWith(binaryOperator2.Context), binaryOperator, binaryOperator2);
			node[ndx - 1] = value2;
			node[ndx] = null;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B7D0 File Offset: 0x000199D0
		private static void CombineReturnWithExpression(Block node, int ndx, ReturnNode returnNode)
		{
			if (returnNode.Operand != null && returnNode.Operand.IsExpression)
			{
				BinaryOperator binaryOperator = node[ndx - 1] as BinaryOperator;
				Lookup lookup;
				if (binaryOperator != null && binaryOperator.IsAssign && (lookup = (binaryOperator.Operand1 as Lookup)) != null)
				{
					if (!returnNode.Operand.IsEquivalentTo(lookup))
					{
						AstNode operand = CommaOperator.CombineWithComma(node[ndx - 1].Context.FlattenToStart(), node[ndx - 1], returnNode.Operand);
						returnNode.Operand = operand;
						node[ndx - 1] = null;
						return;
					}
					if (binaryOperator.OperatorToken == JSToken.Assign)
					{
						if (lookup.VariableField == null || lookup.VariableField.OuterField != null || lookup.VariableField.IsReferencedInnerScope)
						{
							DetachReferences.Apply(returnNode.Operand);
							returnNode.Operand = binaryOperator;
							node[ndx - 1] = null;
							return;
						}
						JSVariableField variableField = lookup.VariableField;
						DetachReferences.Apply(new AstNode[]
						{
							lookup,
							returnNode.Operand
						});
						returnNode.Operand = binaryOperator.Operand2;
						node[ndx - 1] = null;
						if (variableField.RefCount == 0)
						{
							INameDeclaration onlyDeclaration = variableField.OnlyDeclaration;
							if (onlyDeclaration != null && (onlyDeclaration.Initializer == null || onlyDeclaration.Initializer.IsConstant))
							{
								VariableDeclaration variableDeclaration = onlyDeclaration.Parent as VariableDeclaration;
								if (variableDeclaration != null)
								{
									Declaration declaration = variableDeclaration.Parent as Declaration;
									declaration.Remove(variableDeclaration);
									variableField.WasRemoved = true;
									if (declaration.Count == 0)
									{
										declaration.Parent.ReplaceChild(declaration, null);
										return;
									}
								}
							}
						}
					}
					else
					{
						if (lookup.VariableField != null)
						{
							DetachReferences.Apply(returnNode.Operand);
						}
						node.RemoveAt(ndx - 1);
						returnNode.Operand = binaryOperator;
						if (lookup.VariableField != null && lookup.VariableField.OuterField == null && !lookup.VariableField.IsReferencedInnerScope)
						{
							binaryOperator.OperatorToken = JSScanner.StripAssignment(binaryOperator.OperatorToken);
							return;
						}
					}
				}
				else
				{
					AstNode operand2 = CommaOperator.CombineWithComma(node[ndx - 1].Context.FlattenToStart(), node[ndx - 1], returnNode.Operand);
					returnNode.Operand = operand2;
					node[ndx - 1] = null;
				}
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001BA18 File Offset: 0x00019C18
		private void CombineForNodeWithExpression(Block node, int ndx, ForNode forNode)
		{
			if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.MoveInExpressionsIntoForStatement) || !node[ndx - 1].ContainsInOperator)
			{
				if (forNode.Initializer == null)
				{
					forNode.Initializer = node[ndx - 1];
					node[ndx - 1] = null;
					return;
				}
				if (forNode.Initializer.IsExpression)
				{
					AstNode initializer = CommaOperator.CombineWithComma(node[ndx - 1].Context.FlattenToStart(), node[ndx - 1], forNode.Initializer);
					forNode.Initializer = initializer;
					node[ndx - 1] = null;
				}
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001BAFC File Offset: 0x00019CFC
		private static void CombineWithPreviousVar(Block node, int ndx, Var previousVar)
		{
			if (previousVar.Count == 0)
			{
				return;
			}
			BinaryOperator binaryOperator = node[ndx] as BinaryOperator;
			VariableDeclaration variableDeclaration = previousVar[previousVar.Count - 1];
			Lookup lookup;
			BindingIdentifier bindingIdentifier;
			if (binaryOperator != null && binaryOperator.IsAssign && (lookup = (binaryOperator.Operand1 as Lookup)) != null && lookup.VariableField != null && !AnalyzeNodeVisitor.ContainsReference(binaryOperator.Operand2, lookup.VariableField) && (bindingIdentifier = (variableDeclaration.Binding as BindingIdentifier)) != null && bindingIdentifier.VariableField == lookup.VariableField)
			{
				if (variableDeclaration.Initializer != null)
				{
					if (binaryOperator.OperatorToken != JSToken.Assign)
					{
						lookup.VariableField.IfNotNull((JSVariableField v) => v.References.Remove(lookup));
						binaryOperator.OperatorToken = JSScanner.StripAssignment(binaryOperator.OperatorToken);
						binaryOperator.Operand1 = variableDeclaration.Initializer;
						binaryOperator.UpdateWith(binaryOperator.Operand1.Context);
						variableDeclaration.Initializer = binaryOperator;
						node[ndx] = null;
						return;
					}
					if (variableDeclaration.Initializer.IsConstant)
					{
						variableDeclaration.Initializer = binaryOperator.Operand2;
						lookup.VariableField.IfNotNull((JSVariableField v) => v.References.Remove(lookup));
						node[ndx] = null;
						return;
					}
				}
				else if (binaryOperator.OperatorToken == JSToken.Assign)
				{
					lookup.VariableField.IfNotNull((JSVariableField v) => v.References.Remove(lookup));
					variableDeclaration.Initializer = binaryOperator.Operand2;
					node[ndx] = null;
				}
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		private static bool ContainsReference(AstNode node, JSVariableField targetField)
		{
			Lookup lookup = node as Lookup;
			if (lookup == null)
			{
				foreach (AstNode node2 in node.Children)
				{
					if (AnalyzeNodeVisitor.ContainsReference(node2, targetField))
					{
						return true;
					}
				}
				return false;
			}
			if (lookup.VariableField != null)
			{
				return lookup.VariableField == targetField;
			}
			return string.CompareOrdinal(lookup.Name, targetField.Name) == 0;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001BD50 File Offset: 0x00019F50
		private static AstNode FindLastStatement(Block node)
		{
			int num = node.Count - 1;
			while (num >= 0 && (node[num] is FunctionObject || node[num] is ImportantComment))
			{
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			return node[num];
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		public override void Visit(Block node)
		{
			if (node != null)
			{
				ActivationObject activationObject = null;
				if (node.HasOwnScope)
				{
					activationObject = (node.EnclosingScope as BlockScope);
					FunctionObject functionObject = node.Parent as FunctionObject;
					if (functionObject != null)
					{
						activationObject = functionObject.EnclosingScope;
					}
				}
				if (activationObject != null)
				{
					foreach (INameDeclaration nameDeclaration in activationObject.LexicallyDeclaredNames)
					{
						INameDeclaration nameDeclaration2 = activationObject.VarDeclaredName(nameDeclaration.Name);
						if (nameDeclaration2 != null)
						{
							nameDeclaration2.Context.HandleError(JSError.DuplicateLexicalDeclaration, nameDeclaration is LexicalDeclaration);
							nameDeclaration.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
							nameDeclaration2.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
						}
					}
				}
				bool flag = node.Parent is FunctionObject;
				if (node.HasOwnScope)
				{
					this.m_scopeStack.Push(node.EnclosingScope);
				}
				JSError strictNameError = this.m_strictNameError;
				try
				{
					this.m_strictNameError = JSError.StrictModeVariableName;
					for (int i = node.Count - 1; i >= 0; i--)
					{
						node[i].Accept(this);
						if (this.m_stripDebug && node.Count > i && node[i].IsDebugOnly)
						{
							node.RemoveAt(i);
						}
					}
				}
				finally
				{
					this.m_strictNameError = strictNameError;
					if (node.HasOwnScope)
					{
						this.m_scopeStack.Pop();
					}
				}
				if (this.m_parser.Settings.RemoveUnneededCode)
				{
					for (int j = 0; j < node.Count; j++)
					{
						IfNode ifNode = node[j] as IfNode;
						if (ifNode != null && ifNode.TrueBlock != null && ifNode.TrueBlock.Count > 0 && ifNode.FalseBlock != null)
						{
							if (ifNode.TrueBlock[ifNode.TrueBlock.Count - 1] is ReturnNode)
							{
								node.InsertRange(j + 1, ifNode.FalseBlock.Children);
								ifNode.FalseBlock = null;
							}
						}
						else if (node[j] is ReturnNode || node[j] is Break || node[j] is ContinueNode || node[j] is ThrowNode)
						{
							for (int k = node.Count - 1; k > j; k--)
							{
								if (node[k].IsDeclaration)
								{
									Declaration declaration = node[k] as Declaration;
									if (declaration != null && declaration.StatementToken != JSToken.Const)
									{
										for (int l = 0; l < declaration.Count; l++)
										{
											if (declaration[l].Initializer != null)
											{
												DetachReferences.Apply(declaration[l].Initializer);
												declaration[l].Initializer = null;
											}
										}
									}
								}
								else
								{
									DetachReferences.Apply(node[k]);
									node.RemoveAt(k);
								}
							}
						}
					}
				}
				if (flag && node.Count > 0 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionReturnToCondition))
				{
					IfNode ifNode2 = AnalyzeNodeVisitor.FindLastStatement(node) as IfNode;
					ReturnNode returnNode;
					if (ifNode2 != null && ifNode2.FalseBlock == null && ifNode2.TrueBlock.Count == 1 && (returnNode = (ifNode2.TrueBlock[0] as ReturnNode)) != null)
					{
						if (returnNode.Operand == null)
						{
							if (ifNode2.Condition.IsConstant)
							{
								node.ReplaceChild(ifNode2, null);
							}
							else
							{
								node.ReplaceChild(ifNode2, ifNode2.Condition);
							}
						}
						else if (returnNode.Operand.IsExpression)
						{
							Conditional conditional = new Conditional(ifNode2.Condition.Context.FlattenToStart())
							{
								Condition = ifNode2.Condition,
								TrueExpression = returnNode.Operand,
								FalseExpression = AnalyzeNodeVisitor.CreateVoidNode(returnNode.Context.FlattenToStart())
							};
							node.ReplaceChild(ifNode2, new ReturnNode(ifNode2.Context)
							{
								Operand = conditional
							});
							this.Optimize(conditional);
						}
					}
				}
				if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.CombineAdjacentExpressionStatements))
				{
					this.CombineExpressions(node);
				}
				if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.MoveVarIntoFor))
				{
					for (int m = node.Count - 1; m > 0; m--)
					{
						Var var = node[m - 1] as Var;
						ForNode forNode;
						WhileNode whileNode;
						ForIn forIn;
						if (var != null && (forNode = (node[m] as ForNode)) != null)
						{
							if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.MoveInExpressionsIntoForStatement) || !var.ContainsInOperator)
							{
								if (forNode.Initializer != null)
								{
									Var var2 = forNode.Initializer as Var;
									if (var2 != null)
									{
										var2.InsertAt(0, var);
										node.RemoveAt(m - 1);
									}
									else
									{
										BinaryOperator binaryOperator = forNode.Initializer as BinaryOperator;
										if (binaryOperator != null && AnalyzeNodeVisitor.AreAssignmentsInVar(binaryOperator, var))
										{
											AnalyzeNodeVisitor.ConvertAssignmentsToVarDecls(binaryOperator, var, this.m_parser);
											forNode.Initializer = var;
											node.RemoveAt(m - 1);
										}
									}
								}
								else
								{
									node.RemoveAt(m - 1);
									forNode.Initializer = var;
								}
							}
						}
						else if (var != null && (whileNode = (node[m] as WhileNode)) != null && this.m_parser.Settings.IsModificationAllowed(TreeModifications.ChangeWhileToFor))
						{
							node[m] = new ForNode(whileNode.Context.FlattenToStart())
							{
								Initializer = var,
								Condition = whileNode.Condition,
								Body = whileNode.Body
							};
							node.RemoveAt(m - 1);
						}
						else if (var != null && (forIn = (node[m] as ForIn)) != null && !(forIn.Variable is Declaration))
						{
							VariableDeclaration variableDeclaration = var[var.Count - 1];
							if (variableDeclaration.IsEquivalentTo(forIn.Variable) && (variableDeclaration.Initializer == null || variableDeclaration.Initializer.IsConstant))
							{
								AstNode astNode = BindingTransform.ToBinding(forIn.Variable);
								if (astNode != null)
								{
									VariableDeclaration element = new VariableDeclaration(forIn.Variable.Context.Clone())
									{
										Binding = astNode
									};
									Var var3 = new Var(forIn.Variable.Context.Clone());
									var3.Append(element);
									forIn.Variable = var3;
									IList<BindingIdentifier> list = BindingsVisitor.Bindings(variableDeclaration.Binding);
									foreach (BindingIdentifier otherNode in BindingsVisitor.Bindings(astNode))
									{
										foreach (BindingIdentifier bindingIdentifier in list)
										{
											if (bindingIdentifier.IsEquivalentTo(otherNode))
											{
												ActivationObject.RemoveBinding(bindingIdentifier);
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				ReturnNode returnNode2;
				if ((returnNode2 = (AnalyzeNodeVisitor.FindLastStatement(node) as ReturnNode)) != null)
				{
					bool flag2 = false;
					int num = AnalyzeNodeVisitor.PreviousStatementIndex(node, returnNode2);
					Lookup lookup;
					if ((lookup = (returnNode2.Operand as Lookup)) != null && num >= 0)
					{
						Declaration declaration2 = node[num] as Declaration;
						if (declaration2 != null)
						{
							VariableDeclaration variableDeclaration2 = declaration2[declaration2.Count - 1];
							if (variableDeclaration2.Initializer != null && variableDeclaration2.IsEquivalentTo(lookup))
							{
								BindingIdentifier bindingIdentifier2 = variableDeclaration2.Binding as BindingIdentifier;
								if (bindingIdentifier2 != null)
								{
									if (bindingIdentifier2.VariableField.IfNotNull((JSVariableField v) => v.RefCount == 1))
									{
										bindingIdentifier2.VariableField.References.Remove(lookup);
										bindingIdentifier2.VariableField.Declarations.Remove(bindingIdentifier2);
										if (declaration2.Count == 1)
										{
											returnNode2.Operand = variableDeclaration2.Initializer;
											node.RemoveAt(num);
										}
										else
										{
											returnNode2.Operand = variableDeclaration2.Initializer;
											declaration2[declaration2.Count - 1] = null;
										}
									}
								}
							}
						}
					}
					IfNode ifNode3;
					Conditional conditional2;
					while (num >= 0 && returnNode2 != null && (ifNode3 = (node[num] as IfNode)) != null && ifNode3.TrueBlock != null && ifNode3.TrueBlock.Count == 1 && ifNode3.FalseBlock == null)
					{
						bool flag3 = false;
						ReturnNode returnNode3 = ifNode3.TrueBlock[0] as ReturnNode;
						if (returnNode3 != null)
						{
							if (returnNode2.Operand == null)
							{
								if (returnNode3.Operand == null)
								{
									if (!flag)
									{
										if (ifNode3.Condition.IsConstant)
										{
											node.RemoveAt(num);
											flag3 = true;
										}
										else
										{
											node[num] = ifNode3.Condition;
										}
									}
									else if (ifNode3.Condition.IsConstant)
									{
										node.ReplaceChild(returnNode2, null);
										node.RemoveAt(num);
										flag3 = true;
									}
									else if (node.ReplaceChild(returnNode2, ifNode3.Condition))
									{
										node.RemoveAt(num);
										flag3 = true;
									}
								}
								else
								{
									conditional2 = new Conditional(ifNode3.Condition.Context.FlattenToStart())
									{
										Condition = ifNode3.Condition,
										TrueExpression = returnNode3.Operand,
										FalseExpression = AnalyzeNodeVisitor.CreateVoidNode(returnNode3.Context.FlattenToStart())
									};
									if (node.ReplaceChild(returnNode2, new ReturnNode(returnNode3.Context.FlattenToStart())
									{
										Operand = conditional2
									}))
									{
										node.RemoveAt(num);
										this.Optimize(conditional2);
										flag3 = true;
									}
								}
							}
							else if (returnNode3.Operand == null)
							{
								conditional2 = new Conditional(ifNode3.Condition.Context.FlattenToStart())
								{
									Condition = ifNode3.Condition,
									TrueExpression = AnalyzeNodeVisitor.CreateVoidNode(returnNode2.Context.FlattenToStart()),
									FalseExpression = returnNode2.Operand
								};
								if (node.ReplaceChild(returnNode2, new ReturnNode(returnNode2.Context.FlattenToStart())
								{
									Operand = conditional2
								}))
								{
									node.RemoveAt(num);
									this.Optimize(conditional2);
									flag3 = true;
								}
							}
							else if (returnNode3.Operand.IsEquivalentTo(returnNode2.Operand))
							{
								if (ifNode3.Condition.IsConstant)
								{
									DetachReferences.Apply(returnNode3.Operand);
									node.RemoveAt(num);
									flag3 = true;
								}
								else
								{
									DetachReferences.Apply(returnNode3.Operand);
									returnNode2.Operand = CommaOperator.CombineWithComma(ifNode3.Condition.Context.FlattenToStart(), ifNode3.Condition, returnNode2.Operand);
									node.RemoveAt(num);
									flag3 = true;
								}
							}
							else
							{
								conditional2 = new Conditional(ifNode3.Condition.Context.FlattenToStart())
								{
									Condition = ifNode3.Condition,
									TrueExpression = returnNode3.Operand,
									FalseExpression = returnNode2.Operand
								};
								returnNode2.Operand = conditional2;
								node.RemoveAt(num);
								this.Optimize(conditional2);
								flag3 = true;
							}
						}
						if (!flag3)
						{
							break;
						}
						flag2 = true;
						returnNode2 = (node[num--] as ReturnNode);
					}
					if (flag2 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.CombineAdjacentExpressionStatements))
					{
						this.CombineExpressions(node);
					}
					if (returnNode2 != null && (conditional2 = (returnNode2.Operand as Conditional)) != null)
					{
						UnaryOperator unaryOperator = conditional2.FalseExpression as UnaryOperator;
						if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.Void && unaryOperator.Operand is ConstantWrapper)
						{
							unaryOperator = (conditional2.TrueExpression as UnaryOperator);
							if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.Void)
							{
								if (flag)
								{
									node.ReplaceChild(returnNode2, conditional2.Condition);
								}
								else
								{
									node.ReplaceChild(returnNode2, conditional2.Condition);
									node.Append(new ReturnNode(returnNode2.Context.Clone()));
								}
							}
							else if (flag)
							{
								IfNode newNode = new IfNode(returnNode2.Context)
								{
									Condition = conditional2.Condition,
									TrueBlock = AstNode.ForceToBlock(new ReturnNode(returnNode2.Context.Clone())
									{
										Operand = conditional2.TrueExpression
									})
								};
								node.ReplaceChild(returnNode2, newNode);
							}
						}
						else if (flag)
						{
							unaryOperator = (conditional2.TrueExpression as UnaryOperator);
							if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.Void && unaryOperator.Operand is ConstantWrapper)
							{
								LogicalNot logicalNot = new LogicalNot(conditional2.Condition, this.m_parser.Settings);
								logicalNot.Apply();
								IfNode newNode2 = new IfNode(returnNode2.Context)
								{
									Condition = conditional2.Condition,
									TrueBlock = AstNode.ForceToBlock(new ReturnNode(returnNode2.Context.Clone())
									{
										Operand = conditional2.FalseExpression
									})
								};
								node.ReplaceChild(returnNode2, newNode2);
							}
						}
					}
				}
				if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.CombineEquivalentIfReturns))
				{
					for (int n = node.Count - 1; n > 0; n--)
					{
						AstNode astNode2 = null;
						AstNode operand;
						if (AnalyzeNodeVisitor.IsIfReturnExpr(node[n], out operand, ref astNode2) != null)
						{
							AstNode astNode3 = astNode2;
							AstNode astNode4;
							IfNode ifNode4 = AnalyzeNodeVisitor.IsIfReturnExpr(node[n - 1], out astNode4, ref astNode3);
							if (ifNode4 != null)
							{
								ifNode4.Condition = new BinaryOperator(astNode4.Context.FlattenToStart())
								{
									Operand1 = astNode4,
									Operand2 = operand,
									OperatorToken = JSToken.LogicalOr,
									TerminatingContext = (ifNode4.TerminatingContext ?? node.TerminatingContext)
								};
								DetachReferences.Apply(astNode2);
								node.RemoveAt(n);
							}
						}
					}
				}
				if (flag && this.m_parser.Settings.IsModificationAllowed(TreeModifications.InvertIfReturn))
				{
					for (int num2 = node.Count - 1; num2 >= 0; num2--)
					{
						IfNode ifNode5 = node[num2] as IfNode;
						if (ifNode5 != null && ifNode5.FalseBlock == null && ifNode5.TrueBlock != null && ifNode5.TrueBlock.Count == 1)
						{
							ReturnNode returnNode4 = ifNode5.TrueBlock[0] as ReturnNode;
							if (returnNode4 != null && returnNode4.Operand == null)
							{
								LogicalNot.Apply(ifNode5.Condition, this.m_parser.Settings);
								ifNode5.TrueBlock.Clear();
								int num3 = num2 + 1;
								if (node.Count == num3 + 1)
								{
									IfNode ifNode6 = node[num3] as IfNode;
									if (ifNode6 != null && (ifNode6.FalseBlock == null || ifNode6.FalseBlock.Count == 0))
									{
										node.RemoveAt(num3);
										ifNode5.Condition = new BinaryOperator(ifNode5.Condition.Context.FlattenToStart())
										{
											Operand1 = ifNode5.Condition,
											Operand2 = ifNode6.Condition,
											OperatorToken = JSToken.LogicalAnd
										};
										ifNode5.TrueBlock = ifNode6.TrueBlock;
									}
									else if (node[num3].IsExpression && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionCallToConditionAndCall))
									{
										AstNode expression = node[num3];
										node.RemoveAt(num3);
										this.IfConditionExpressionToExpression(ifNode5, expression);
									}
								}
								while (node.Count > num3)
								{
									AstNode item = node[num3];
									node.RemoveAt(num3);
									ifNode5.TrueBlock.Append(item);
								}
							}
						}
					}
					return;
				}
				bool flag4 = node.Parent is ForNode || node.Parent is ForIn || node.Parent is WhileNode || node.Parent is DoWhile;
				if (flag4 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.InvertIfContinue))
				{
					for (int num4 = node.Count - 1; num4 >= 0; num4--)
					{
						IfNode ifNode7 = node[num4] as IfNode;
						if (ifNode7 != null && ifNode7.FalseBlock == null && ifNode7.TrueBlock != null && ifNode7.TrueBlock.Count == 1)
						{
							ContinueNode continueNode = ifNode7.TrueBlock[0] as ContinueNode;
							if (continueNode != null && (string.IsNullOrEmpty(continueNode.Label) || AnalyzeNodeVisitor.LabelMatchesParent(continueNode.Label, node.Parent)))
							{
								if (num4 < node.Count - 1)
								{
									LogicalNot.Apply(ifNode7.Condition, this.m_parser.Settings);
									ifNode7.TrueBlock.Clear();
									int num5 = num4 + 1;
									if (node.Count == num5 + 1)
									{
										IfNode ifNode8 = node[num5] as IfNode;
										if (ifNode8 != null && (ifNode8.FalseBlock == null || ifNode8.FalseBlock.Count == 0))
										{
											ifNode7.Condition = new BinaryOperator(ifNode7.Condition.Context.FlattenToStart())
											{
												Operand1 = ifNode7.Condition,
												Operand2 = ifNode8.Condition,
												OperatorToken = JSToken.LogicalAnd
											};
											ifNode7.TrueBlock = ifNode8.TrueBlock;
											node.RemoveAt(num5);
										}
										else if (node[num5].IsExpression && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionCallToConditionAndCall))
										{
											AstNode expression2 = node[num5];
											node.RemoveAt(num5);
											this.IfConditionExpressionToExpression(ifNode7, expression2);
										}
									}
									while (node.Count > num5)
									{
										AstNode item2 = node[num5];
										node.RemoveAt(num5);
										ifNode7.TrueBlock.Append(item2);
									}
								}
								else if (ifNode7.Condition.IsConstant)
								{
									node.RemoveAt(num4);
								}
								else
								{
									node[num4] = ifNode7.Condition;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		private static bool LabelMatchesParent(string label, AstNode parentNode)
		{
			bool result = false;
			LabeledStatement labeledStatement;
			while ((labeledStatement = (parentNode.Parent as LabeledStatement)) != null)
			{
				if (string.CompareOrdinal(label, labeledStatement.Label) == 0)
				{
					result = true;
					break;
				}
				parentNode = labeledStatement;
			}
			return result;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001D118 File Offset: 0x0001B318
		private static IfNode IsIfReturnExpr(AstNode node, out AstNode condition, ref AstNode matchExpression)
		{
			condition = null;
			IfNode ifNode = node as IfNode;
			if (ifNode != null && ifNode.FalseBlock == null && ifNode.TrueBlock != null && ifNode.TrueBlock.Count == 1)
			{
				ReturnNode returnNode = ifNode.TrueBlock[0] as ReturnNode;
				if (returnNode != null && (matchExpression == null || matchExpression.IsEquivalentTo(returnNode.Operand)))
				{
					matchExpression = returnNode.Operand;
					condition = ifNode.Condition;
				}
			}
			if (condition == null || matchExpression == null)
			{
				return null;
			}
			return ifNode;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001D194 File Offset: 0x0001B394
		private static int PreviousStatementIndex(Block node, AstNode child)
		{
			int num = node.IndexOf(child) - 1;
			while (num >= 0 && (node[num] is FunctionObject || node[num] is ImportantComment))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		public override void Visit(Break node)
		{
			if (node != null)
			{
				if (!node.Label.IsNullOrWhiteSpace() && node.LabelInfo == null && this.m_parser.Settings.RemoveUnneededCode && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryLabels))
				{
					node.Label = null;
				}
				if (!AnalyzeNodeVisitor.IsInsideLoop(node, true))
				{
					node.Context.HandleError(JSError.BadBreak, true);
				}
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001D254 File Offset: 0x0001B454
		public override void Visit(CallNode node)
		{
			if (node != null)
			{
				Member member = node.Function as Member;
				if (node.IsConstructor)
				{
					FunctionObject functionObject = node.Function as FunctionObject;
					if (functionObject == null)
					{
						if ((functionObject = (node.Function as GroupingOperator).IfNotNull((GroupingOperator g) => g.Operand as FunctionObject)) == null)
						{
							if (!this.m_parser.Settings.CollapseToLiteral)
							{
								goto IL_1FD;
							}
							Lookup lookup = node.Function as Lookup;
							if (lookup == null)
							{
								goto IL_1FD;
							}
							if (lookup.Name == "Object" && this.m_parser.Settings.IsModificationAllowed(TreeModifications.NewObjectToObjectLiteral))
							{
								if (node.Arguments == null || node.Arguments.Count == 0)
								{
									ObjectLiteral newNode = new ObjectLiteral(node.Context);
									if (node.Parent.ReplaceChild(node, newNode))
									{
										return;
									}
									goto IL_1FD;
								}
								else
								{
									if (node.Arguments.Count != 1)
									{
										goto IL_1FD;
									}
									ObjectLiteral objectLiteral = node.Arguments[0] as ObjectLiteral;
									if (objectLiteral != null)
									{
										node.Parent.ReplaceChild(node, objectLiteral);
										objectLiteral.Accept(this);
										return;
									}
									goto IL_1FD;
								}
							}
							else
							{
								if (!(lookup.Name == "Array") || !this.m_parser.Settings.IsModificationAllowed(TreeModifications.NewArrayToArrayLiteral))
								{
									goto IL_1FD;
								}
								ConstantWrapper constantWrapper = (node.Arguments != null && node.Arguments.Count == 1) ? (node.Arguments[0] as ConstantWrapper) : null;
								if (node.Arguments != null && node.Arguments.Count == 1 && (constantWrapper == null || constantWrapper.IsNumericLiteral))
								{
									goto IL_1FD;
								}
								ArrayLiteral arrayLiteral = new ArrayLiteral(node.Context)
								{
									Elements = node.Arguments
								};
								if (node.Parent.ReplaceChild(node, arrayLiteral))
								{
									arrayLiteral.Accept(this);
									return;
								}
								goto IL_1FD;
							}
						}
					}
					if (functionObject.FunctionType == FunctionType.ArrowFunction)
					{
						node.Function.Context.HandleError(JSError.ArrowCannotBeConstructor, true);
					}
				}
				IL_1FD:
				IList<ResourceStrings> resourceStrings = this.m_parser.Settings.ResourceStrings;
				if (node.InBrackets && resourceStrings.Count > 0)
				{
					if (this.m_matchVisitor == null)
					{
						this.m_matchVisitor = new MatchPropertiesVisitor();
					}
					for (int i = resourceStrings.Count - 1; i >= 0; i--)
					{
						ResourceStrings resourceStrings2 = resourceStrings[i];
						if (resourceStrings2 != null && this.m_matchVisitor.Match(node.Function, resourceStrings2.Name))
						{
							if (node.Arguments.Count == 1)
							{
								ConstantWrapper constantWrapper2 = node.Arguments[0] as ConstantWrapper;
								if (constantWrapper2 != null)
								{
									string name = constantWrapper2.Value.ToString();
									ConstantWrapper constantWrapper3 = new ConstantWrapper(resourceStrings2[name], PrimitiveType.String, node.Context);
									node.Parent.ReplaceChild(node, constantWrapper3);
									constantWrapper3.Accept(this);
									return;
								}
								node.Context.HandleError(JSError.ResourceReferenceMustBeConstant, true);
							}
							else
							{
								node.Context.HandleError(JSError.ResourceReferenceMustBeConstant, true);
							}
						}
					}
				}
				if (node.InBrackets && node.Arguments != null)
				{
					string singleConstantArgument = node.Arguments.SingleConstantArgument;
					if (singleConstantArgument != null)
					{
						string newName;
						if (this.m_parser.Settings.HasRenamePairs && this.m_parser.Settings.ManualRenamesProperties && this.m_parser.Settings.IsModificationAllowed((TreeModifications)((ulong)-2147483648)) && !string.IsNullOrEmpty(newName = this.m_parser.Settings.GetNewName(singleConstantArgument)))
						{
							if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.BracketMemberToDotMember) && JSScanner.IsSafeIdentifier(newName) && !JSScanner.IsKeyword(newName, (node.EnclosingScope ?? this.m_parser.GlobalScope).UseStrict))
							{
								Member member2 = new Member(node.Context)
								{
									Root = node.Function,
									Name = singleConstantArgument,
									NameContext = node.Arguments[0].Context
								};
								node.Parent.ReplaceChild(node, member2);
								member2.Accept(this);
								return;
							}
							node.Arguments[0] = new ConstantWrapper(newName, PrimitiveType.String, node.Arguments[0].Context);
						}
						else if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.BracketMemberToDotMember) && JSScanner.IsSafeIdentifier(singleConstantArgument) && !JSScanner.IsKeyword(singleConstantArgument, (node.EnclosingScope ?? this.m_parser.GlobalScope).UseStrict))
						{
							Member member3 = new Member(node.Context)
							{
								Root = node.Function,
								Name = singleConstantArgument,
								NameContext = node.Arguments[0].Context
							};
							node.Parent.ReplaceChild(node, member3);
							member3.Accept(this);
							return;
						}
					}
				}
				base.Visit(node);
				if (node.Function != null && node.Function.IsDebugOnly)
				{
					node.IsDebugOnly = true;
					if (node.IsConstructor)
					{
						node.Parent.ReplaceChild(node, new ObjectLiteral(node.Context)
						{
							IsDebugOnly = true
						});
						return;
					}
				}
				else
				{
					member = (node.Function as Member);
					Lookup lookup = node.Function as Lookup;
					bool flag = false;
					if (lookup != null && string.CompareOrdinal(lookup.Name, "eval") == 0 && lookup.VariableField.FieldType == FieldType.Predefined)
					{
						flag = true;
					}
					else if (member != null && string.CompareOrdinal(member.Name, "eval") == 0)
					{
						if (member.Root.IsWindowLookup)
						{
							flag = true;
						}
					}
					else
					{
						CallNode callNode = node.Function as CallNode;
						if (callNode != null && callNode.InBrackets && callNode.Function.IsWindowLookup && callNode.Arguments.IsSingleConstantArgument("eval"))
						{
							flag = true;
						}
					}
					if (flag && this.m_parser.Settings.EvalTreatment != EvalTreatment.Ignore)
					{
						this.m_scopeStack.Peek().IsKnownAtCompileTime = false;
					}
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001D87C File Offset: 0x0001BA7C
		public override void Visit(ClassNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.ClassType == ClassType.Expression && node.Binding != null)
				{
					BindingIdentifier bindingIdentifier = node.Binding as BindingIdentifier;
					if (bindingIdentifier != null && bindingIdentifier.VariableField != null && bindingIdentifier.VariableField.RefCount == 0 && this.m_parser.Settings.RemoveFunctionExpressionNames && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveFunctionExpressionNames))
					{
						node.Binding = null;
					}
				}
				if (node.Elements != null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					foreach (AstNode astNode in node.Elements)
					{
						FunctionObject functionObject = astNode as FunctionObject;
						string name;
						if (functionObject != null && functionObject.Binding != null && !(name = functionObject.Binding.Name).IsNullOrWhiteSpace())
						{
							Context context = functionObject.Binding.Context ?? functionObject.Context;
							if (!hashSet.Add(AnalyzeNodeVisitor.ClassElementKeyName(functionObject.FunctionType, name)))
							{
								context.HandleError(JSError.DuplicateClassElementName, true);
							}
							if (functionObject.FunctionType == FunctionType.Getter || functionObject.FunctionType == FunctionType.Setter)
							{
								if (hashSet.Contains(AnalyzeNodeVisitor.ClassElementKeyName(FunctionType.Method, name)))
								{
									context.HandleError(JSError.DuplicateClassElementName, true);
								}
							}
							else if (hashSet.Contains(AnalyzeNodeVisitor.ClassElementKeyName(FunctionType.Getter, name)) || hashSet.Contains(AnalyzeNodeVisitor.ClassElementKeyName(FunctionType.Setter, name)))
							{
								context.HandleError(JSError.DuplicateClassElementName, true);
							}
							if ((functionObject.FunctionType != FunctionType.Method || functionObject.IsGenerator) && string.CompareOrdinal(name, "constructor") == 0)
							{
								context.HandleError(JSError.SpecialConstructor, true);
							}
							else if (functionObject.IsStatic && string.CompareOrdinal(name, "prototype") == 0)
							{
								context.HandleError(JSError.StaticPrototype, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		private static string ClassElementKeyName(FunctionType funcType, string name)
		{
			switch (funcType)
			{
			case FunctionType.Getter:
				return "get_" + name;
			case FunctionType.Setter:
				return "set_" + name;
			default:
				return "method_" + name;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001DADC File Offset: 0x0001BCDC
		public override void Visit(ComprehensionNode node)
		{
			if (node != null)
			{
				node.BlockScope.IfNotNull(delegate(BlockScope s)
				{
					this.m_scopeStack.Push(s);
				});
				try
				{
					if (node.Clauses != null)
					{
						node.Clauses.Accept(this);
					}
					if (node.Expression != null)
					{
						node.Expression.Accept(this);
					}
				}
				finally
				{
					node.BlockScope.IfNotNull((BlockScope s) => this.m_scopeStack.Pop());
				}
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001DB64 File Offset: 0x0001BD64
		private void Optimize(Conditional node)
		{
			if (node.Condition == null || !node.Condition.IsDebugOnly)
			{
				UnaryOperator unaryOperator = node.Condition as UnaryOperator;
				if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.LogicalNot && !unaryOperator.OperatorInConditionalCompilationComment && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfNotTrueFalseToIfFalseTrue))
				{
					node.Condition = unaryOperator.Operand;
					node.SwapBranches();
				}
				BinaryOperator binaryOperator = node.TrueExpression as BinaryOperator;
				if (binaryOperator != null && binaryOperator.IsAssign)
				{
					BinaryOperator binaryOperator2 = node.FalseExpression as BinaryOperator;
					if (binaryOperator2 != null && binaryOperator2.OperatorToken == binaryOperator.OperatorToken && binaryOperator.Operand1.IsEquivalentTo(binaryOperator2.Operand1))
					{
						DetachReferences.Apply(binaryOperator2.Operand1);
						BinaryOperator newNode = new BinaryOperator(node.Context)
						{
							Operand1 = binaryOperator.Operand1,
							Operand2 = new Conditional(node.Context)
							{
								Condition = node.Condition,
								QuestionContext = node.QuestionContext,
								TrueExpression = binaryOperator.Operand2,
								ColonContext = node.ColonContext,
								FalseExpression = binaryOperator2.Operand2
							},
							OperatorContext = binaryOperator.OperatorContext,
							OperatorToken = binaryOperator.OperatorToken,
							TerminatingContext = node.TerminatingContext
						};
						node.Parent.ReplaceChild(node, newNode);
					}
				}
				return;
			}
			if (node.FalseExpression == null || node.FalseExpression.IsDebugOnly)
			{
				node.Parent.ReplaceChild(node, new ConstantWrapper(null, PrimitiveType.Null, node.Context)
				{
					IsDebugOnly = true
				});
				return;
			}
			node.Parent.ReplaceChild(node, node.FalseExpression);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001DD2D File Offset: 0x0001BF2D
		public override void Visit(Conditional node)
		{
			if (node != null)
			{
				base.Visit(node);
				this.Optimize(node);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public override void Visit(ConditionalCompilationOn node)
		{
			this.m_encounteredCCOn = true;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		private static bool StringSourceIsNotInlineSafe(string source)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(source))
			{
				result = (source.IndexOf("</", StringComparison.Ordinal) >= 0 || source.IndexOf("]]>", StringComparison.Ordinal) >= 0);
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		public override void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				if (node.PrimitiveType == PrimitiveType.String && this.m_parser.Settings.ErrorIfNotInlineSafe && node.Context != null && AnalyzeNodeVisitor.StringSourceIsNotInlineSafe(node.Context.Code))
				{
					node.Context.HandleError(JSError.StringNotInlineSafe, true);
				}
				AstNode astNode = null;
				for (AstNode parent = node.Parent; parent != null; parent = parent.Parent)
				{
					CallNode callNode = parent as CallNode;
					if (callNode != null && astNode == callNode.Arguments)
					{
						Lookup lookup = callNode.Function as Lookup;
						if (lookup != null && lookup.Name == "RegExp")
						{
							node.IsParameterToRegExp = true;
							return;
						}
					}
					astNode = parent;
				}
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001DE38 File Offset: 0x0001C038
		public override void Visit(ConstStatement node)
		{
			if (node != null)
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(node))
				{
					if (!hashSet.Add(bindingIdentifier.Name))
					{
						bindingIdentifier.Context.HandleError(JSError.DuplicateConstantDeclaration, true);
					}
				}
				base.Visit(node);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public override void Visit(ContinueNode node)
		{
			if (node != null)
			{
				if (!node.Label.IsNullOrWhiteSpace() && node.LabelInfo == null && this.m_parser.Settings.RemoveUnneededCode && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryLabels))
				{
					node.Label = null;
				}
				if (!AnalyzeNodeVisitor.IsInsideLoop(node, false))
				{
					node.Context.HandleError(JSError.BadContinue, true);
				}
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001DF24 File Offset: 0x0001C124
		public override void Visit(DebuggerNode node)
		{
			if (node != null)
			{
				node.IsDebugOnly = true;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001DF30 File Offset: 0x0001C130
		public override void Visit(DoWhile node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Body != null && node.Body.Count == 0)
				{
					node.Body = null;
				}
				if (node.Condition != null && node.Condition.IsDebugOnly)
				{
					if (node.Body == null)
					{
						node.Parent.ReplaceChild(node, null);
						return;
					}
					node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
				}
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		public override void Visit(ExportNode node)
		{
			if (node != null)
			{
				if (!node.IsDefault)
				{
					if (!node.ModuleName.IsNullOrWhiteSpace())
					{
						using (IEnumerator<BindingIdentifier> enumerator = BindingsVisitor.Bindings(node).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								BindingIdentifier bindingIdentifier = enumerator.Current;
								if (bindingIdentifier.VariableField != null)
								{
									bindingIdentifier.VariableField.CanCrunch = false;
								}
							}
							goto IL_D4;
						}
					}
					if (node.Count == 1 && (node[0] is Declaration || node[0] is FunctionObject || node[0] is ClassNode))
					{
						foreach (BindingIdentifier bindingIdentifier2 in BindingsVisitor.Bindings(node[0]))
						{
							if (bindingIdentifier2.VariableField != null)
							{
								bindingIdentifier2.VariableField.CanCrunch = false;
							}
						}
					}
				}
				IL_D4:
				base.Visit(node);
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		public override void Visit(ForNode node)
		{
			if (node != null)
			{
				if (node.BlockScope != null)
				{
					foreach (INameDeclaration nameDeclaration in node.BlockScope.LexicallyDeclaredNames)
					{
						if (node.Body != null && node.Body.HasOwnScope)
						{
							INameDeclaration nameDeclaration2 = node.Body.EnclosingScope.LexicallyDeclaredName(nameDeclaration.Name);
							if (nameDeclaration2 != null)
							{
								nameDeclaration2.Context.HandleError(JSError.DuplicateLexicalDeclaration, true);
								if (nameDeclaration2.VariableField != null)
								{
									nameDeclaration2.VariableField.OuterField = nameDeclaration.VariableField;
									if (nameDeclaration.VariableField != null && !nameDeclaration2.VariableField.CanCrunch)
									{
										nameDeclaration.VariableField.CanCrunch = false;
									}
								}
							}
						}
						INameDeclaration nameDeclaration3 = node.BlockScope.VarDeclaredName(nameDeclaration.Name);
						if (nameDeclaration3 != null)
						{
							nameDeclaration3.Context.HandleError(JSError.DuplicateLexicalDeclaration, nameDeclaration is LexicalDeclaration);
							nameDeclaration3.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
							nameDeclaration.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
						}
					}
				}
				base.Visit(node);
				if (node.Body != null && node.Body.Count == 0 && !node.Body.HasOwnScope)
				{
					node.Body = null;
				}
				if (node.Initializer != null && node.Initializer.IsDebugOnly)
				{
					node.Initializer = null;
				}
				if (node.Incrementer != null && node.Incrementer.IsDebugOnly)
				{
					node.Incrementer = null;
				}
				if (node.Condition != null && node.Condition.IsDebugOnly)
				{
					if (node.Initializer == null && node.Incrementer == null && node.Body == null)
					{
						node.IsDebugOnly = true;
						return;
					}
					node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001E334 File Offset: 0x0001C534
		public override void Visit(ForIn node)
		{
			if (node != null)
			{
				if (node.BlockScope != null)
				{
					foreach (INameDeclaration nameDeclaration in node.BlockScope.LexicallyDeclaredNames)
					{
						if (node.Body != null && node.Body.HasOwnScope)
						{
							INameDeclaration nameDeclaration2 = node.Body.EnclosingScope.LexicallyDeclaredName(nameDeclaration.Name);
							if (nameDeclaration2 != null)
							{
								nameDeclaration2.Context.HandleError(JSError.DuplicateLexicalDeclaration, true);
								if (nameDeclaration2.VariableField != null)
								{
									nameDeclaration2.VariableField.OuterField = nameDeclaration.VariableField;
									if (nameDeclaration.VariableField != null && !nameDeclaration2.VariableField.CanCrunch)
									{
										nameDeclaration.VariableField.CanCrunch = false;
									}
								}
							}
						}
						INameDeclaration nameDeclaration3 = node.BlockScope.VarDeclaredName(nameDeclaration.Name);
						if (nameDeclaration3 != null)
						{
							nameDeclaration3.Context.HandleError(JSError.DuplicateLexicalDeclaration, nameDeclaration is LexicalDeclaration);
							nameDeclaration3.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
							nameDeclaration.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
						}
					}
				}
				base.Visit(node);
				if (node.Body != null && node.Body.Count == 0 && !node.Body.HasOwnScope)
				{
					node.Body = null;
				}
				if (node.Collection != null && node.Collection.IsDebugOnly)
				{
					if (node.Body == null)
					{
						node.IsDebugOnly = true;
						return;
					}
					node.Collection = new ObjectLiteral(node.Collection.Context)
					{
						IsDebugOnly = true
					};
				}
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001E574 File Offset: 0x0001C774
		public override void Visit(FunctionObject node)
		{
			if (node != null)
			{
				if (node.Binding != null && !node.Binding.Name.IsNullOrWhiteSpace())
				{
					if (!node.IsExpression)
					{
						goto IL_8D;
					}
					if (!node.Binding.VariableField.IfNotNull((JSVariableField v) => v.RefCount == 0) || !this.m_parser.Settings.RemoveFunctionExpressionNames || !this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveFunctionExpressionNames))
					{
						goto IL_8D;
					}
				}
				node.NameGuess = AnalyzeNodeVisitor.GuessAtName(node);
				IL_8D:
				bool useStrict = this.m_scopeStack.Peek().UseStrict;
				if (useStrict && node.Binding != null && (string.CompareOrdinal(node.Binding.Name, "eval") == 0 || string.CompareOrdinal(node.Binding.Name, "arguments") == 0))
				{
					if (node.Binding.Context != null)
					{
						node.Binding.Context.HandleError(JSError.StrictModeFunctionName, true);
					}
					else if (node.Context != null)
					{
						node.Context.HandleError(JSError.StrictModeFunctionName, true);
					}
				}
				if (node.FunctionType == FunctionType.Setter && (node.ParameterDeclarations == null || node.ParameterDeclarations.Count != 1))
				{
					(node.ParameterDeclarations.IfNotNull((AstNodeList p) => p.Context) ?? node.Context).HandleError(JSError.SetterMustHaveOneParameter, true);
				}
				else if (node.ParameterDeclarations.IfNotNull((AstNodeList p) => p.Count > 1))
				{
					int lastParameterIndex = node.ParameterDeclarations.Count - 1;
					node.ParameterDeclarations.ForEach<ParameterDeclaration>(delegate(ParameterDeclaration paramDecl)
					{
						if (paramDecl.Position != lastParameterIndex && paramDecl.HasRest)
						{
							paramDecl.Context.HandleError(JSError.RestParameterNotLast, true);
						}
					});
				}
				if (node.ParameterDeclarations != null && node.ParameterDeclarations.Count > 0)
				{
					JSError strictNameError = this.m_strictNameError;
					this.m_strictNameError = JSError.StrictModeArgumentName;
					node.ParameterDeclarations.Accept(this);
					this.m_strictNameError = strictNameError;
					HashSet<string> hashSet = new HashSet<string>();
					foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(node.ParameterDeclarations))
					{
						if (!hashSet.Add(bindingIdentifier.Name))
						{
							if (useStrict)
							{
								bindingIdentifier.Context.HandleError(JSError.StrictModeDuplicateArgument, true);
							}
							else
							{
								bindingIdentifier.Context.HandleError(JSError.DuplicateName, false);
							}
						}
					}
				}
				if (node.Body != null)
				{
					this.m_scopeStack.Push(node.EnclosingScope);
					try
					{
						node.Body.Accept(this);
					}
					finally
					{
						this.m_scopeStack.Pop();
					}
				}
				if (node.ParameterDeclarations != null && node.ParameterDeclarations.Count > 0)
				{
					bool flag = this.m_parser.Settings.RemoveUnneededCode && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnusedParameters);
					bool flag2 = false;
					for (int i = node.ParameterDeclarations.Count - 1; i >= 0; i--)
					{
						ParameterDeclaration parameterDeclaration = node.ParameterDeclarations[i] as ParameterDeclaration;
						if (parameterDeclaration != null)
						{
							if (AnalyzeNodeVisitor.CheckParametersAreReferenced(parameterDeclaration.Binding, flag, flag2))
							{
								if (!flag2)
								{
									parameterDeclaration.Context.HandleError(JSError.ArgumentNotReferenced, false);
									if (flag)
									{
										node.ParameterDeclarations.RemoveAt(i);
									}
								}
							}
							else
							{
								flag2 = true;
							}
						}
					}
				}
				if (node.FunctionType == FunctionType.ArrowFunction)
				{
					ReturnNode returnNode;
					if (node.Body.IfNotNull((Block b) => b.Count == 1) && (returnNode = (node.Body[0] as ReturnNode)) != null)
					{
						node.Body.ReplaceChild(returnNode, returnNode.Operand);
						node.Body.IsConcise = true;
					}
				}
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001E988 File Offset: 0x0001CB88
		private static bool CheckParametersAreReferenced(AstNode binding, bool removeIfUnreferenced, bool foundLastReference)
		{
			bool flag = false;
			BindingIdentifier bindingIdentifier = binding as BindingIdentifier;
			if (bindingIdentifier != null)
			{
				flag = false;
				if (bindingIdentifier.VariableField != null)
				{
					flag = !bindingIdentifier.VariableField.IsReferenced;
					if (flag && removeIfUnreferenced && !foundLastReference)
					{
						bindingIdentifier.VariableField.Declarations.Remove(bindingIdentifier);
						bindingIdentifier.VariableField.WasRemoved = true;
					}
				}
			}
			else
			{
				flag = true;
				foreach (BindingIdentifier bindingIdentifier2 in BindingsVisitor.Bindings(binding))
				{
					if (bindingIdentifier2.VariableField.IfNotNull((JSVariableField v) => !v.IsReferenced))
					{
						bindingIdentifier2.Context.HandleError(JSError.ArgumentNotReferenced, false);
						if (removeIfUnreferenced)
						{
							ActivationObject.DeleteFromBindingPattern(bindingIdentifier2, false);
						}
					}
					else
					{
						flag = false;
					}
				}
				AnalyzeNodeVisitor.TrimTrailingElisionsFromArrayBindings(binding);
			}
			return flag;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		private static void TrimTrailingElisionsFromArrayBindings(AstNode binding)
		{
			ArrayLiteral arrayLiteral = binding as ArrayLiteral;
			if (arrayLiteral != null)
			{
				bool flag = true;
				for (int i = arrayLiteral.Elements.Count - 1; i >= 0; i--)
				{
					ConstantWrapper constantWrapper = arrayLiteral.Elements[i] as ConstantWrapper;
					if (constantWrapper != null)
					{
						if (flag)
						{
							arrayLiteral.Elements.RemoveAt(i);
						}
					}
					else
					{
						flag = false;
						AnalyzeNodeVisitor.TrimTrailingElisionsFromArrayBindings(arrayLiteral.Elements[i]);
					}
				}
				return;
			}
			ObjectLiteral objectLiteral;
			if ((objectLiteral = (binding as ObjectLiteral)) != null)
			{
				objectLiteral.Properties.ForEach<ObjectLiteralProperty>(delegate(ObjectLiteralProperty property)
				{
					AnalyzeNodeVisitor.TrimTrailingElisionsFromArrayBindings(property.Value);
				});
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public override void Visit(IfNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.TrueBlock != null && node.TrueBlock.Count == 0)
				{
					node.TrueBlock = null;
				}
				if (node.FalseBlock != null && node.FalseBlock.Count == 0)
				{
					node.FalseBlock = null;
				}
				if (node.Condition != null && node.Condition.IsDebugOnly)
				{
					node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
					node.TrueBlock = null;
				}
				if (node.TrueBlock != null && node.FalseBlock != null)
				{
					if (node.TrueBlock.IsExpression && node.FalseBlock.IsExpression && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfExpressionsToExpression))
					{
						LogicalNot logicalNot = new LogicalNot(node.Condition, this.m_parser.Settings);
						Conditional conditional;
						if (logicalNot.Measure() < 0)
						{
							logicalNot.Apply();
							conditional = new Conditional(node.Context)
							{
								Condition = node.Condition,
								TrueExpression = node.FalseBlock[0],
								FalseExpression = node.TrueBlock[0]
							};
						}
						else
						{
							conditional = new Conditional(node.Context)
							{
								Condition = node.Condition,
								TrueExpression = node.TrueBlock[0],
								FalseExpression = node.FalseBlock[0]
							};
						}
						node.Parent.ReplaceChild(node, conditional);
						this.Optimize(conditional);
					}
					else
					{
						LogicalNot logicalNot2 = new LogicalNot(node.Condition, this.m_parser.Settings);
						if (logicalNot2.Measure() < 0)
						{
							logicalNot2.Apply();
							node.SwapBranches();
						}
						if (node.TrueBlock.Count == 1 && node.FalseBlock.Count == 1)
						{
							ReturnNode returnNode = node.TrueBlock[0] as ReturnNode;
							if (returnNode != null && returnNode.Operand != null)
							{
								ReturnNode returnNode2 = node.FalseBlock[0] as ReturnNode;
								if (returnNode2 != null && returnNode2.Operand != null)
								{
									Conditional conditional2 = new Conditional(node.Condition.Context.FlattenToStart())
									{
										Condition = node.Condition,
										TrueExpression = returnNode.Operand,
										FalseExpression = returnNode2.Operand
									};
									ReturnNode newNode = new ReturnNode(node.Context)
									{
										Operand = conditional2
									};
									node.Parent.ReplaceChild(node, newNode);
									this.Optimize(conditional2);
								}
							}
						}
					}
				}
				else if (node.FalseBlock != null)
				{
					if (node.FalseBlock.IsExpression && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionCallToConditionAndCall))
					{
						JSToken operatorToken = JSToken.LogicalOr;
						LogicalNot logicalNot3 = new LogicalNot(node.Condition, this.m_parser.Settings);
						if (logicalNot3.Measure() < 0)
						{
							logicalNot3.Apply();
							operatorToken = JSToken.LogicalAnd;
						}
						BinaryOperator newNode2 = new BinaryOperator(node.Context)
						{
							Operand1 = node.Condition,
							Operand2 = node.FalseBlock[0],
							OperatorToken = operatorToken
						};
						node.Parent.ReplaceChild(node, newNode2);
					}
					else if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionFalseToIfNotConditionTrue))
					{
						LogicalNot logicalNot4 = new LogicalNot(node.Condition, this.m_parser.Settings);
						logicalNot4.Apply();
						node.SwapBranches();
					}
				}
				else if (node.TrueBlock != null)
				{
					if (node.TrueBlock.IsExpression && this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfConditionCallToConditionAndCall))
					{
						this.IfConditionExpressionToExpression(node, node.TrueBlock[0]);
					}
				}
				else if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.IfEmptyToExpression))
				{
					bool flag = node.Condition == null || node.Condition.IsConstant || node.Condition.IsDebugOnly;
					if (flag)
					{
						node.Parent.ReplaceChild(node, null);
					}
					else
					{
						node.Parent.ReplaceChild(node, node.Condition);
					}
				}
				if (node.FalseBlock == null && node.TrueBlock != null && node.TrueBlock.Count == 1 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.CombineNestedIfs))
				{
					IfNode ifNode = node.TrueBlock[0] as IfNode;
					if (ifNode != null && ifNode.FalseBlock == null)
					{
						node.Condition = new BinaryOperator(node.Condition.Context.FlattenToStart())
						{
							Operand1 = node.Condition,
							Operand2 = ifNode.Condition,
							OperatorToken = JSToken.LogicalAnd
						};
						node.TrueBlock = ifNode.TrueBlock;
					}
				}
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001F034 File Offset: 0x0001D234
		private void IfConditionExpressionToExpression(IfNode ifNode, AstNode expression)
		{
			JSToken operatorToken = JSToken.LogicalAnd;
			LogicalNot logicalNot = new LogicalNot(ifNode.Condition, this.m_parser.Settings);
			if (logicalNot.Measure() < 0)
			{
				logicalNot.Apply();
				operatorToken = JSToken.LogicalOr;
			}
			BinaryOperator newNode = new BinaryOperator(ifNode.Context)
			{
				Operand1 = ifNode.Condition,
				Operand2 = expression,
				OperatorToken = operatorToken
			};
			ifNode.Parent.ReplaceChild(ifNode, newNode);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		public override void Visit(ImportNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Count == 1 && !(node[0] is ImportExportSpecifier))
				{
					foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(node[0]))
					{
						if (bindingIdentifier.VariableField != null)
						{
							bindingIdentifier.VariableField.CanCrunch = false;
						}
					}
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001F128 File Offset: 0x0001D328
		public override void Visit(LabeledStatement node)
		{
			if (node != null)
			{
				if (node.Statement != null)
				{
					node.Statement.Accept(this);
				}
				if (node.LabelInfo != null && !node.LabelInfo.HasIssues)
				{
					if (node.LabelInfo.RefCount == 0)
					{
						node.LabelContext.HandleError(JSError.UnusedLabel, false);
					}
					if (node.LabelInfo.RefCount == 0 && this.m_parser.Settings.RemoveUnneededCode && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryLabels))
					{
						if (node.Statement == null)
						{
							node.Parent.ReplaceChild(node, null);
							return;
						}
						node.Parent.ReplaceChild(node, node.Statement);
						return;
					}
					else if (this.m_parser.Settings.LocalRenaming != LocalRenaming.KeepAll && this.m_parser.Settings.IsModificationAllowed(TreeModifications.LocalRenaming))
					{
						node.LabelInfo.MinLabel = CrunchEnumerator.CrunchedLabel(node.LabelInfo.NestLevel);
					}
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001F240 File Offset: 0x0001D440
		public override void Visit(Lookup node)
		{
			this.m_possibleDebugNamespace = false;
			if (node != null)
			{
				if (node.Parent is CallNode)
				{
					node.RefType = (((CallNode)node.Parent).IsConstructor ? ReferenceType.Constructor : ReferenceType.Function);
				}
				ActivationObject activationObject = this.m_scopeStack.Peek();
				if (JSScanner.IsKeyword(node.Name, activationObject.UseStrict))
				{
					if (node.VariableField.IfNotNull((JSVariableField v) => v.FieldType != FieldType.Super))
					{
						node.Context.HandleError(JSError.KeywordUsedAsIdentifier, true);
					}
				}
				bool flag = node.Parent is Member;
				if (node.VariableField != null && node.VariableField.FieldType == FieldType.Predefined)
				{
					if (string.CompareOrdinal(node.Name, "NaN") == 0)
					{
						node.Parent.ReplaceChild(node, new ConstantWrapper(double.NaN, PrimitiveType.Number, node.Context));
					}
					else if (string.CompareOrdinal(node.Name, "Infinity") == 0)
					{
						node.Parent.ReplaceChild(node, new ConstantWrapper(double.PositiveInfinity, PrimitiveType.Number, node.Context));
					}
					else if (this.m_lookForDebugNamespaces && flag && string.CompareOrdinal(node.Name, "window") == 0)
					{
						this.m_possibleDebugNamespace = true;
						this.m_possibleDebugNamespaceIndex = 0;
						this.m_possibleDebugMatches.Clear();
					}
				}
				if (this.m_lookForDebugNamespaces && !this.m_possibleDebugNamespace && this.InitialDebugNameSpaceMatches(node.Name, flag))
				{
					node.IsDebugOnly = true;
					node.Parent.ReplaceChild(node, new ObjectLiteral(node.Context)
					{
						IsDebugOnly = true
					});
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001F404 File Offset: 0x0001D604
		public override void Visit(Member node)
		{
			if (node != null)
			{
				IList<ResourceStrings> resourceStrings = this.m_parser.Settings.ResourceStrings;
				if (resourceStrings.Count > 0)
				{
					if (this.m_matchVisitor == null)
					{
						this.m_matchVisitor = new MatchPropertiesVisitor();
					}
					for (int i = resourceStrings.Count - 1; i >= 0; i--)
					{
						ResourceStrings resourceStrings2 = resourceStrings[i];
						if (this.m_matchVisitor.Match(node.Root, resourceStrings2.Name))
						{
							ConstantWrapper constantWrapper = new ConstantWrapper(resourceStrings2[node.Name] ?? string.Empty, PrimitiveType.String, node.Context);
							node.Parent.ReplaceChild(node, constantWrapper);
							constantWrapper.Accept(this);
							return;
						}
					}
				}
				if (this.m_parser.Settings.HasRenamePairs && this.m_parser.Settings.ManualRenamesProperties && this.m_parser.Settings.IsModificationAllowed((TreeModifications)((ulong)-2147483648)))
				{
					string newName = this.m_parser.Settings.GetNewName(node.Name);
					if (!string.IsNullOrEmpty(newName))
					{
						node.Name = newName;
					}
				}
				if (JSScanner.IsKeyword(node.Name, this.m_scopeStack.Peek().UseStrict))
				{
					node.NameContext.HandleError(JSError.KeywordUsedAsIdentifier, false);
				}
				if (node.Root != null)
				{
					node.Root.Accept(this);
				}
				if (this.m_stripDebug)
				{
					bool flag = node.Parent is Member;
					if (node.Root.IfNotNull((AstNode r) => r.IsDebugOnly))
					{
						node.IsDebugOnly = true;
						return;
					}
					if (this.m_possibleDebugNamespace)
					{
						if (this.m_possibleDebugMatches.Count == 0)
						{
							if (this.InitialDebugNameSpaceMatches(node.Name, flag))
							{
								node.IsDebugOnly = true;
							}
						}
						else
						{
							for (int j = this.m_possibleDebugMatches.Count - 1; j >= 0; j--)
							{
								string[] array = this.m_possibleDebugMatches[j];
								if (string.CompareOrdinal(node.Name, array[this.m_possibleDebugNamespaceIndex]) == 0)
								{
									if (array.Length == this.m_possibleDebugNamespaceIndex + 1)
									{
										node.IsDebugOnly = true;
										this.m_possibleDebugMatches.Clear();
										break;
									}
								}
								else
								{
									this.m_possibleDebugMatches.RemoveAt(j);
								}
							}
							if (this.m_possibleDebugMatches.Count > 0 && flag)
							{
								this.m_possibleDebugNamespaceIndex++;
							}
							else
							{
								this.m_possibleDebugMatches.Clear();
								this.m_possibleDebugNamespace = false;
							}
						}
						if (node.IsDebugOnly)
						{
							node.Parent.ReplaceChild(node, new ObjectLiteral(node.Context)
							{
								IsDebugOnly = true
							});
						}
					}
				}
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		private bool InitialDebugNameSpaceMatches(string name, bool parentIsMember)
		{
			foreach (string[] array in this.m_debugNamespaceParts)
			{
				if (string.CompareOrdinal(name, array[0]) == 0)
				{
					if (array.Length == 1)
					{
						this.m_possibleDebugMatches.Clear();
						this.m_possibleDebugNamespace = false;
						return true;
					}
					if (parentIsMember)
					{
						this.m_possibleDebugMatches.Add(array);
					}
				}
			}
			if (this.m_possibleDebugMatches.Count > 0)
			{
				this.m_possibleDebugNamespace = true;
				this.m_possibleDebugNamespaceIndex = 1;
			}
			return false;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001F904 File Offset: 0x0001DB04
		public override void Visit(ObjectLiteral node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (this.m_parser.Settings.LocalRenaming != LocalRenaming.KeepAll)
				{
					node.Properties.ForEach<ObjectLiteralProperty>(delegate(ObjectLiteralProperty property)
					{
						if (property.Name == null)
						{
							string text = property.Value.ToString();
							if (JSScanner.IsValidIdentifier(text) && !this.m_noRename.Contains(text) && AnalyzeNodeVisitor.FieldCanBeRenamed(property.Value))
							{
								property.Name = new ObjectLiteralField(text, PrimitiveType.String, property.Value.Context)
								{
									IsIdentifier = true
								};
							}
						}
					});
				}
				if (this.m_scopeStack.Peek().UseStrict)
				{
					Dictionary<string, string> nameMap = new Dictionary<string, string>();
					node.Properties.ForEach<ObjectLiteralProperty>(delegate(ObjectLiteralProperty property)
					{
						string propertyType = AnalyzeNodeVisitor.GetPropertyType(property.Value as FunctionObject);
						string key = (property.Name ?? property.Value) + propertyType;
						string a;
						if (propertyType == "data")
						{
							if (!nameMap.TryGetValue(key, out a) && !nameMap.TryGetValue((property.Name ?? property.Value) + "get", out a) && !nameMap.TryGetValue((property.Name ?? property.Value) + "set", out a))
							{
								nameMap.Add(key, propertyType);
								return;
							}
							(property.Name ?? property.Value).Context.HandleError(JSError.StrictModeDuplicateProperty, true);
							if (a != propertyType)
							{
								nameMap.Add(key, propertyType);
								return;
							}
						}
						else if (nameMap.TryGetValue(key, out a) || nameMap.TryGetValue((property.Name ?? property.Value) + "data", out a))
						{
							(property.Name ?? property.Value).Context.HandleError(JSError.StrictModeDuplicateProperty, true);
							if (a != propertyType)
							{
								nameMap.Add(key, propertyType);
								return;
							}
						}
						else
						{
							nameMap.Add(key, propertyType);
						}
					});
				}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001F9F4 File Offset: 0x0001DBF4
		private static bool FieldCanBeRenamed(AstNode node)
		{
			bool flag = false;
			if (node != null)
			{
				flag = (node as INameDeclaration).IfNotNull(delegate(INameDeclaration n)
				{
					if (!n.RenameNotAllowed)
					{
						return n.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch);
					}
					return false;
				});
				if (!flag)
				{
					flag = (node as INameReference).IfNotNull((INameReference n) => n.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch));
				}
			}
			return flag;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		public override void Visit(ObjectLiteralField node)
		{
			if (node != null && node.PrimitiveType == PrimitiveType.String && this.m_parser.Settings.HasRenamePairs && this.m_parser.Settings.ManualRenamesProperties && this.m_parser.Settings.IsModificationAllowed((TreeModifications)((ulong)-2147483648)))
			{
				string newName = this.m_parser.Settings.GetNewName(node.Value.ToString());
				if (!string.IsNullOrEmpty(newName))
				{
					node.Value = newName;
				}
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001FADC File Offset: 0x0001DCDC
		public override void Visit(ObjectLiteralProperty node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Value != null && node.Value.IsDebugOnly)
				{
					node.Value = new ConstantWrapper(null, PrimitiveType.Null, node.Value.Context);
				}
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001FB20 File Offset: 0x0001DD20
		private static string GetPropertyType(FunctionObject funcObj)
		{
			switch (funcObj.IfNotNull((FunctionObject f) => f.FunctionType))
			{
			case FunctionType.Getter:
				return "get";
			case FunctionType.Setter:
				return "set";
			case FunctionType.Method:
				return "method";
			}
			return "data";
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001FB84 File Offset: 0x0001DD84
		public override void Visit(RegExpLiteral node)
		{
			if (node != null)
			{
				try
				{
					if (new Regex(node.Pattern, RegexOptions.ECMAScript) == null)
					{
						node.Context.HandleError(JSError.RegExpSyntax, true);
					}
				}
				catch (ArgumentException)
				{
					node.Context.HandleError(JSError.RegExpSyntax, true);
				}
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		public override void Visit(ReturnNode node)
		{
			if (node != null)
			{
				ActivationObject activationObject = this.m_scopeStack.Peek();
				while (activationObject != null && !(activationObject is FunctionScope))
				{
					activationObject = activationObject.Parent;
				}
				if (activationObject == null)
				{
					node.Context.HandleError(JSError.BadReturn, false);
				}
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
					if (node.Operand == null || node.Operand.IsDebugOnly)
					{
						node.Operand = null;
						return;
					}
					Lookup lookup = node.Operand.LeftHandSide as Lookup;
					BinaryOperator binaryOperator;
					if (lookup != null && lookup.VariableField != null && lookup.VariableField.OuterField == null && (binaryOperator = (lookup.Parent as BinaryOperator)) != null && binaryOperator.IsAssign && !lookup.VariableField.IsReferencedInnerScope)
					{
						if (binaryOperator.OperatorToken != JSToken.Assign)
						{
							binaryOperator.OperatorToken = JSScanner.StripAssignment(binaryOperator.OperatorToken);
							return;
						}
						if (binaryOperator.Parent == node)
						{
							lookup.VariableField.References.Remove(lookup);
							node.Operand = binaryOperator.Operand2;
						}
					}
				}
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001FD1C File Offset: 0x0001DF1C
		public override void Visit(Switch node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Expression != null && node.Expression.IsDebugOnly)
				{
					node.Expression = new ConstantWrapper(null, PrimitiveType.Null, node.Expression.Context);
				}
				if (node.BlockScope != null)
				{
					foreach (INameDeclaration nameDeclaration in node.BlockScope.LexicallyDeclaredNames)
					{
						INameDeclaration nameDeclaration2 = node.BlockScope.VarDeclaredName(nameDeclaration.Name);
						if (nameDeclaration2 != null)
						{
							nameDeclaration2.Context.HandleError(JSError.DuplicateLexicalDeclaration, nameDeclaration is LexicalDeclaration);
							nameDeclaration2.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
							nameDeclaration.VariableField.IfNotNull((JSVariableField v) => v.CanCrunch = false);
						}
					}
				}
				if (this.m_parser.Settings.RemoveUnneededCode)
				{
					string b = string.Empty;
					LabeledStatement labeledStatement = node.Parent as LabeledStatement;
					if (labeledStatement != null)
					{
						b = labeledStatement.Label;
					}
					int num = -1;
					bool flag = false;
					for (int i = 0; i < node.Cases.Count; i++)
					{
						SwitchCase switchCase = node.Cases[i] as SwitchCase;
						if (switchCase != null)
						{
							if (switchCase.IsDefault)
							{
								num = i;
								flag = true;
							}
							if (flag && switchCase.Statements.Count > 0)
							{
								if (switchCase.Statements.Count != 1)
								{
									flag = false;
									break;
								}
								Break @break = switchCase.Statements[0] as Break;
								if (@break == null || (@break.Label != null && @break.Label != b))
								{
									flag = false;
									break;
								}
								break;
							}
						}
					}
					if (flag && num >= 0 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveEmptyDefaultCase))
					{
						node.Cases.RemoveAt(num);
						num = -1;
					}
					if (num == -1 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveEmptyCaseWhenNoDefault))
					{
						bool flag2 = true;
						Break break2 = null;
						for (int j = node.Cases.Count - 1; j >= 0; j--)
						{
							SwitchCase switchCase2 = node.Cases[j] as SwitchCase;
							if (switchCase2 != null)
							{
								if (switchCase2.Statements.Count == 0 && flag2)
								{
									DetachReferences.Apply(switchCase2.CaseValue);
									node.Cases.RemoveAt(j);
								}
								else
								{
									Break break3 = (switchCase2.Statements.Count == 1) ? (switchCase2.Statements[0] as Break) : null;
									if (break3 != null)
									{
										if (break3.Label == null || break3.Label == b)
										{
											break2 = break3;
											DetachReferences.Apply(switchCase2.CaseValue);
											node.Cases.RemoveAt(j);
											flag2 = true;
										}
										else
										{
											flag2 = false;
											break2 = null;
										}
									}
									else
									{
										if (flag2 && switchCase2.Statements.Count > 0 && break2 != null)
										{
											AstNode astNode = switchCase2.Statements[switchCase2.Statements.Count - 1];
											if (!(astNode is Break) && !(astNode is ContinueNode) && !(astNode is ReturnNode) && !(astNode is ThrowNode))
											{
												switchCase2.Statements.Append(break2);
											}
										}
										break2 = null;
										flag2 = false;
									}
								}
							}
						}
					}
					if (node.Cases.Count > 0 && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveBreakFromLastCaseBlock))
					{
						SwitchCase switchCase3 = node.Cases[node.Cases.Count - 1] as SwitchCase;
						if (switchCase3 != null)
						{
							Block statements = switchCase3.Statements;
							Break break4 = (statements.Count > 0) ? (statements[statements.Count - 1] as Break) : null;
							if (break4 != null && (break4.Label == null || break4.Label == b))
							{
								statements.RemoveLast();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00020158 File Offset: 0x0001E358
		public override void Visit(TryNode node)
		{
			if (node != null)
			{
				node.TryBlock.IfNotNull(delegate(Block b)
				{
					b.Accept(this);
				});
				if (node.TryBlock != null && node.TryBlock.Count == 0)
				{
					node.TryBlock = null;
				}
				this.DoCatchBlock(node);
				node.FinallyBlock.IfNotNull(delegate(Block b)
				{
					b.Accept(this);
				});
				if (node.FinallyBlock != null && node.FinallyBlock.Count == 0 && node.CatchBlock != null && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveEmptyFinally))
				{
					node.FinallyBlock = null;
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00020210 File Offset: 0x0001E410
		private void DoCatchBlock(TryNode node)
		{
			node.CatchBlock.IfNotNull(delegate(Block b)
			{
				b.Accept(this);
			});
			if (node.CatchParameter != null)
			{
				this.m_strictNameError = JSError.StrictModeCatchName;
				node.CatchParameter.Accept(this);
				this.m_strictNameError = JSError.StrictModeVariableName;
				IList<BindingIdentifier> list = BindingsVisitor.Bindings(node.CatchParameter);
				foreach (INameDeclaration nameDeclaration in node.CatchBlock.EnclosingScope.LexicallyDeclaredNames)
				{
					foreach (BindingIdentifier bindingIdentifier in list)
					{
						if (nameDeclaration != bindingIdentifier && string.CompareOrdinal(nameDeclaration.Name, bindingIdentifier.Name) == 0)
						{
							nameDeclaration.Context.HandleError(JSError.DuplicateLexicalDeclaration, nameDeclaration is LexicalDeclaration);
							if (nameDeclaration.VariableField != null)
							{
								nameDeclaration.VariableField.OuterField = bindingIdentifier.VariableField;
								if (bindingIdentifier.VariableField != null && !nameDeclaration.VariableField.CanCrunch)
								{
									bindingIdentifier.VariableField.CanCrunch = false;
								}
							}
						}
					}
				}
				foreach (INameDeclaration nameDeclaration2 in node.CatchBlock.EnclosingScope.VarDeclaredNames)
				{
					foreach (BindingIdentifier bindingIdentifier2 in list)
					{
						if (string.CompareOrdinal(nameDeclaration2.Name, bindingIdentifier2.Name) == 0)
						{
							nameDeclaration2.Context.HandleError(JSError.DuplicateLexicalDeclaration, false);
						}
					}
				}
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000203FC File Offset: 0x0001E5FC
		public override void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Operand != null && node.Operand.IsDebugOnly)
				{
					node.IsDebugOnly = true;
					switch (node.OperatorToken)
					{
					case JSToken.FirstOperator:
						node.Operand = new ConstantWrapper(true, PrimitiveType.Boolean, node.Context);
						return;
					case JSToken.Increment:
					case JSToken.Decrement:
						node.Parent.ReplaceChild(node, new ConstantWrapper(0, PrimitiveType.Number, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					case JSToken.Void:
						node.Operand = new ConstantWrapper(0, PrimitiveType.Number, node.Operand.Context);
						return;
					case JSToken.TypeOf:
						node.Parent.ReplaceChild(node, new ConstantWrapper("object", PrimitiveType.String, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					case JSToken.LogicalNot:
						node.Parent.ReplaceChild(node, new ConstantWrapper(true, PrimitiveType.Boolean, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					case JSToken.BitwiseNot:
						node.Parent.ReplaceChild(node, new ConstantWrapper(-1, PrimitiveType.Number, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					case JSToken.FirstBinaryOperator:
						node.Parent.ReplaceChild(node, new ConstantWrapper(0, PrimitiveType.Number, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					case JSToken.Minus:
						node.Parent.ReplaceChild(node, new ConstantWrapper(0, PrimitiveType.Number, node.Context)
						{
							IsDebugOnly = true
						});
						return;
					default:
						node.Operand = AnalyzeNodeVisitor.ClearDebugExpression(node.Operand);
						return;
					}
				}
				else if (node.OperatorToken == JSToken.FirstOperator)
				{
					if (this.m_scopeStack.Peek().UseStrict && node.Operand is Lookup)
					{
						node.Context.HandleError(JSError.StrictModeInvalidDelete, true);
						return;
					}
				}
				else if (node.OperatorToken == JSToken.Increment || node.OperatorToken == JSToken.Decrement)
				{
					Lookup lookup = node.Operand as Lookup;
					if (lookup != null)
					{
						if (lookup.VariableField != null && lookup.VariableField.InitializationOnly)
						{
							lookup.Context.HandleError(JSError.AssignmentToConstant, true);
						}
						if (this.m_scopeStack.Peek().UseStrict && (lookup.VariableField == null || lookup.VariableField.FieldType == FieldType.UndefinedGlobal || lookup.VariableField.FieldType == FieldType.Arguments || (lookup.VariableField.FieldType == FieldType.Predefined && string.CompareOrdinal(lookup.Name, "eval") == 0)))
						{
							node.Operand.Context.HandleError(JSError.StrictModeInvalidPreOrPost, true);
							return;
						}
					}
				}
				else if (node.OperatorToken == JSToken.TypeOf)
				{
					if (this.m_parser.Settings.RemoveUnneededCode && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveWindowDotFromTypeOf))
					{
						Member member = node.Operand as Member;
						if (member != null)
						{
							Lookup lookup2 = member.Root as Lookup;
							if (lookup2 != null && lookup2.VariableField != null && lookup2.VariableField.FieldType == FieldType.Predefined && lookup2.Name == "window")
							{
								string name = member.Name;
								ActivationObject enclosingScope = member.EnclosingScope;
								JSVariableField jsvariableField = enclosingScope.CanReference(name);
								if (jsvariableField == null || jsvariableField.FieldType == FieldType.Predefined || jsvariableField.FieldType == FieldType.Global || jsvariableField.FieldType == FieldType.UndefinedGlobal)
								{
									DetachReferences.Apply(lookup2);
									lookup2.Name = name;
									lookup2.VariableField = enclosingScope.FindReference(name);
									node.Operand = lookup2;
									lookup2.VariableField.AddReference(lookup2);
									return;
								}
							}
						}
					}
				}
				else
				{
					ConstantWrapper constantWrapper = node.Operand as ConstantWrapper;
					if (constantWrapper != null && constantWrapper.IsNumericLiteral)
					{
						double num = constantWrapper.ToNumber();
						if (node.OperatorToken == JSToken.Minus && this.m_parser.Settings.IsModificationAllowed(TreeModifications.ApplyUnaryMinusToNumericLiteral))
						{
							constantWrapper.Value = -num;
							if (node.Parent.ReplaceChild(node, constantWrapper))
							{
								constantWrapper.Context = node.Context.Clone();
								return;
							}
						}
						else if (node.OperatorToken == JSToken.FirstBinaryOperator && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnaryPlusOnNumericLiteral) && node.Parent.ReplaceChild(node, constantWrapper))
						{
							constantWrapper.Context = node.Context.Clone();
						}
					}
				}
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public override void Visit(Var node)
		{
			if (node != null)
			{
				if (this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveDuplicateVar))
				{
					int i = 0;
					while (i < node.Count)
					{
						BindingIdentifier bindingIdentifier = node[i].Binding as BindingIdentifier;
						if (bindingIdentifier != null)
						{
							string name = bindingIdentifier.Name;
							if (node[i].Initializer != null)
							{
								AnalyzeNodeVisitor.DeleteNoInits(node, ++i, name);
							}
							else if (AnalyzeNodeVisitor.VarDeclExists(node, i + 1, name))
							{
								bindingIdentifier.VariableField.Declarations.Remove(bindingIdentifier);
								node.RemoveAt(i);
							}
							else
							{
								i++;
							}
						}
						else
						{
							i++;
						}
					}
				}
				base.Visit(node);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0002095C File Offset: 0x0001EB5C
		public override void Visit(VariableDeclaration node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Initializer != null && node.Initializer.IsDebugOnly)
				{
					node.Initializer = AnalyzeNodeVisitor.ClearDebugExpression(node.Initializer);
				}
				if (node.Initializer == null && !(node.Binding is BindingIdentifier))
				{
					if (node.Parent.IfNotNull((AstNode p) => !(p.Parent is ForIn)))
					{
						node.Binding.Context.HandleError(JSError.BindingPatternRequiresInitializer, true);
					}
				}
				if (node.IsCCSpecialCase && this.m_parser.Settings.IsModificationAllowed(TreeModifications.RemoveUnnecessaryCCOnStatements))
				{
					node.UseCCOn = !this.m_encounteredCCOn;
					this.m_encounteredCCOn = true;
				}
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00020A28 File Offset: 0x0001EC28
		public override void Visit(WhileNode node)
		{
			if (node != null)
			{
				base.Visit(node);
				if (node.Body != null && node.Body.Count == 0)
				{
					node.Body = null;
				}
				if (node.Condition != null && node.Condition.IsDebugOnly)
				{
					if (node.Body == null)
					{
						node.IsDebugOnly = true;
						return;
					}
					node.Condition = new ConstantWrapper(0, PrimitiveType.Number, node.Condition.Context);
				}
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00020AA8 File Offset: 0x0001ECA8
		public override void Visit(WithNode node)
		{
			if (node != null)
			{
				if (this.m_scopeStack.Peek().UseStrict)
				{
					node.Context.HandleError(JSError.StrictModeNoWith, true);
				}
				else
				{
					node.Context.HandleError(JSError.WithNotRecommended, false);
				}
				ActivationObject activationObject = node.Body.IfNotNull((Block b) => b.EnclosingScope);
				base.Visit(node);
				if (node.Body != null && node.Body.Count == 0)
				{
					node.Body = null;
				}
				if (node.Body == null && activationObject != null)
				{
					activationObject.IsKnownAtCompileTime = true;
				}
				if (node.WithObject != null && node.WithObject.IsDebugOnly)
				{
					if (node.Body == null)
					{
						node.IsDebugOnly = true;
						return;
					}
					node.WithObject = new ObjectLiteral(node.WithObject.Context)
					{
						IsDebugOnly = true
					};
				}
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00020B94 File Offset: 0x0001ED94
		private static AstNode ClearDebugExpression(AstNode node)
		{
			if (node == null || node is ObjectLiteral || node is ConstantWrapper)
			{
				return node;
			}
			return new ObjectLiteral(node.Context)
			{
				IsDebugOnly = true
			};
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00020BCC File Offset: 0x0001EDCC
		private static string GuessAtName(AstNode node)
		{
			string result = string.Empty;
			AstNode parent = node.Parent;
			if (parent != null)
			{
				if (parent is AstNodeList)
				{
					parent = parent.Parent;
				}
				CallNode callNode = parent as CallNode;
				if (callNode != null && callNode.IsConstructor)
				{
					parent = parent.Parent;
				}
				result = parent.GetFunctionGuess(node);
			}
			return result;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00020C1C File Offset: 0x0001EE1C
		private static bool AreAssignmentsInVar(BinaryOperator binaryOp, Var varStatement)
		{
			bool result = false;
			if (binaryOp != null)
			{
				if (binaryOp.OperatorToken == JSToken.Assign)
				{
					Lookup lookup = binaryOp.Operand1 as Lookup;
					if (lookup != null)
					{
						result = varStatement.Contains(lookup.Name);
					}
				}
				else if (binaryOp.OperatorToken == JSToken.Comma)
				{
					result = (AnalyzeNodeVisitor.AreAssignmentsInVar(binaryOp.Operand1 as BinaryOperator, varStatement) && AnalyzeNodeVisitor.AreAssignmentsInVar(binaryOp.Operand2 as BinaryOperator, varStatement));
				}
			}
			return result;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00020C8C File Offset: 0x0001EE8C
		private static void ConvertAssignmentsToVarDecls(BinaryOperator binaryOp, Declaration declaration, JSParser parser)
		{
			if (binaryOp != null)
			{
				if (binaryOp.OperatorToken == JSToken.Assign)
				{
					Lookup lookup = binaryOp.Operand1 as Lookup;
					if (lookup != null)
					{
						BindingIdentifier bindingIdentifier = new BindingIdentifier(lookup.Context)
						{
							Name = lookup.Name,
							TerminatingContext = lookup.TerminatingContext,
							VariableField = lookup.VariableField
						};
						VariableDeclaration element = new VariableDeclaration(binaryOp.Context.Clone())
						{
							Binding = bindingIdentifier,
							AssignContext = binaryOp.OperatorContext,
							Initializer = binaryOp.Operand2
						};
						lookup.VariableField.Declarations.Add(bindingIdentifier);
						declaration.Append(element);
						return;
					}
				}
				else if (binaryOp.OperatorToken == JSToken.Comma)
				{
					AnalyzeNodeVisitor.ConvertAssignmentsToVarDecls(binaryOp.Operand1 as BinaryOperator, declaration, parser);
					AnalyzeNodeVisitor.ConvertAssignmentsToVarDecls(binaryOp.Operand2 as BinaryOperator, declaration, parser);
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00020D70 File Offset: 0x0001EF70
		private static bool VarDeclExists(Var node, int ndx, string name)
		{
			while (ndx < node.Count)
			{
				VariableDeclaration node2 = node[ndx];
				foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(node2))
				{
					if (string.CompareOrdinal(name, bindingIdentifier.Name) == 0)
					{
						return true;
					}
				}
				ndx++;
			}
			return false;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		private static void DeleteNoInits(Var node, int min, string name)
		{
			for (int i = node.Count - 1; i >= min; i--)
			{
				VariableDeclaration variableDeclaration = node[i];
				BindingIdentifier bindingIdentifier = variableDeclaration.Binding as BindingIdentifier;
				if (bindingIdentifier != null && string.CompareOrdinal(name, bindingIdentifier.Name) == 0 && variableDeclaration.Initializer == null)
				{
					node.RemoveAt(i);
					bindingIdentifier.VariableField.Declarations.Remove(bindingIdentifier);
				}
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00020E4C File Offset: 0x0001F04C
		private static UnaryOperator CreateVoidNode(Context context)
		{
			return new UnaryOperator(context.FlattenToStart())
			{
				Operand = new ConstantWrapper(0.0, PrimitiveType.Number, context),
				OperatorToken = JSToken.Void
			};
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00020E89 File Offset: 0x0001F089
		private static void ValidateIdentifier(bool isStrict, string identifier, Context context, JSError error)
		{
			if (JSScanner.IsKeyword(identifier, isStrict))
			{
				context.HandleError(JSError.KeywordUsedAsIdentifier, true);
				return;
			}
			if (isStrict && (string.CompareOrdinal(identifier, "eval") == 0 || string.CompareOrdinal(identifier, "arguments") == 0))
			{
				context.HandleError(error, true);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		private static bool IsInsideLoop(AstNode node, bool orSwitch)
		{
			bool result = false;
			while (node != null && !(node is FunctionObject))
			{
				if (node is WhileNode || node is DoWhile || node is ForIn || node is ForNode || (orSwitch && node is SwitchCase))
				{
					return true;
				}
				node = node.Parent;
			}
			return result;
		}

		// Token: 0x04000237 RID: 567
		private JSParser m_parser;

		// Token: 0x04000238 RID: 568
		private bool m_encounteredCCOn;

		// Token: 0x04000239 RID: 569
		private MatchPropertiesVisitor m_matchVisitor;

		// Token: 0x0400023A RID: 570
		private Stack<ActivationObject> m_scopeStack;

		// Token: 0x0400023B RID: 571
		private JSError m_strictNameError = JSError.StrictModeVariableName;

		// Token: 0x0400023C RID: 572
		private HashSet<string> m_noRename;

		// Token: 0x0400023D RID: 573
		private bool m_stripDebug;

		// Token: 0x0400023E RID: 574
		private bool m_lookForDebugNamespaces;

		// Token: 0x0400023F RID: 575
		private bool m_possibleDebugNamespace;

		// Token: 0x04000240 RID: 576
		private int m_possibleDebugNamespaceIndex;

		// Token: 0x04000241 RID: 577
		private List<string[]> m_possibleDebugMatches;

		// Token: 0x04000242 RID: 578
		private string[][] m_debugNamespaceParts;
	}
}
