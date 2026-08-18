using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C0 RID: 192
	public class ResolutionVisitor : IVisitor
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003E53C File Offset: 0x0003C73C
		private ActivationObject CurrentLexicalScope
		{
			get
			{
				return this.m_lexicalStack.Peek();
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0003E549 File Offset: 0x0003C749
		private ActivationObject CurrentVariableScope
		{
			get
			{
				return this.m_variableStack.Peek();
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003E558 File Offset: 0x0003C758
		private long NextOrderIndex
		{
			get
			{
				if (!this.m_isUnreachable)
				{
					return this.m_orderIndex += 1L;
				}
				return 0L;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003E584 File Offset: 0x0003C784
		private ResolutionVisitor(ActivationObject rootScope, JSParser parser)
		{
			this.m_lexicalStack = new Stack<ActivationObject>();
			this.m_lexicalStack.Push(rootScope);
			this.m_variableStack = new Stack<ActivationObject>();
			this.m_variableStack.Push(rootScope);
			this.m_settings = parser.Settings;
			this.m_scriptVersion = parser.ParsedVersion;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003E5E0 File Offset: 0x0003C7E0
		public static void Apply(Block block, ActivationObject scope, JSParser parser)
		{
			if (block != null && scope != null && parser != null)
			{
				ResolutionVisitor visitor = new ResolutionVisitor(scope, parser);
				block.Accept(visitor);
				ResolutionVisitor.CreateFields(scope);
				ResolutionVisitor.ResolveLookups(scope, parser.Settings);
				ResolutionVisitor.AddGhostedFields(scope);
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003E620 File Offset: 0x0003C820
		private static void CollapseBlockScope(ActivationObject blockScope)
		{
			blockScope.ScopeLookups.CopyItemsTo(blockScope.Parent.ScopeLookups);
			blockScope.VarDeclaredNames.CopyItemsTo(blockScope.Parent.VarDeclaredNames);
			blockScope.ChildScopes.CopyItemsTo(blockScope.Parent.ChildScopes);
			blockScope.GhostedCatchParameters.CopyItemsTo(blockScope.Parent.GhostedCatchParameters);
			blockScope.GhostedFunctions.CopyItemsTo(blockScope.Parent.GhostedFunctions);
			blockScope.Parent.ChildScopes.Remove(blockScope);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
		private static void CreateFields(ActivationObject scope)
		{
			scope.DeclareScope();
			foreach (ActivationObject scope2 in scope.ChildScopes)
			{
				ResolutionVisitor.CreateFields(scope2);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003E704 File Offset: 0x0003C904
		private static void ResolveLookups(ActivationObject scope, CodeSettings settings)
		{
			foreach (Lookup lookup in scope.ScopeLookups)
			{
				ResolutionVisitor.ResolveLookup(scope, lookup, settings);
			}
			foreach (ActivationObject scope2 in scope.ChildScopes)
			{
				ResolutionVisitor.ResolveLookups(scope2, settings);
			}
			foreach (JSVariableField jsvariableField in scope.NameTable.Values)
			{
				if (jsvariableField.RefCount == 0)
				{
					jsvariableField.HasNoReferences = true;
				}
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003E7E4 File Offset: 0x0003C9E4
		private static void MakeExpectedGlobal(JSVariableField varField)
		{
			do
			{
				varField.FieldType = FieldType.Global;
				varField = varField.OuterField;
			}
			while (varField != null);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003E7F8 File Offset: 0x0003C9F8
		private static void ResolveLookup(ActivationObject scope, Lookup lookup, CodeSettings settings)
		{
			lookup.VariableField = scope.FindReference(lookup.Name);
			if (lookup.VariableField.FieldType == FieldType.UndefinedGlobal)
			{
				ResolutionVisitor.ResolveUndefinedGlobal(lookup);
			}
			else if (lookup.VariableField.FieldType == FieldType.Predefined)
			{
				ResolutionVisitor.ResolvePredefinedGlobal(lookup, scope, settings);
			}
			lookup.VariableField.AddReference(lookup);
			lookup.VariableField.IsPlaceholder = false;
			if (lookup.Parent is ImportExportSpecifier || lookup.Parent is ExportNode)
			{
				lookup.VariableField.IsExported = true;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0003E884 File Offset: 0x0003CA84
		private static void ResolvePredefinedGlobal(Lookup lookup, ActivationObject scope, CodeSettings settings)
		{
			if (lookup.Name.Length == 6 && string.CompareOrdinal(lookup.Name, "window") == 0)
			{
				Member member = lookup.Parent as Member;
				if (member != null)
				{
					scope.AddGlobal(member.Name);
					return;
				}
				CallNode callNode = lookup.Parent as CallNode;
				if (callNode != null && callNode.InBrackets && callNode.Arguments.Count == 1 && callNode.Arguments[0] is ConstantWrapper && callNode.Arguments[0].FindPrimitiveType() == PrimitiveType.String)
				{
					string name = callNode.Arguments[0].ToString();
					if (JSScanner.IsValidIdentifier(name))
					{
						scope.AddGlobal(name);
						return;
					}
				}
			}
			else if (settings.EvalTreatment != EvalTreatment.Ignore && lookup.Name.Length == 4 && string.CompareOrdinal(lookup.Name, "eval") == 0)
			{
				CallNode callNode2 = lookup.Parent as CallNode;
				if (callNode2 != null && callNode2.Function == lookup)
				{
					scope.IsKnownAtCompileTime = false;
				}
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0003E994 File Offset: 0x0003CB94
		private static void ResolveUndefinedGlobal(Lookup lookup)
		{
			if (!lookup.IsGenerated)
			{
				UnaryOperator unaryOperator = lookup.Parent as UnaryOperator;
				if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.TypeOf)
				{
					ResolutionVisitor.MakeExpectedGlobal(lookup.VariableField);
					return;
				}
				if (lookup.Parent is TemplateLiteral)
				{
					string[] array = new string[]
					{
						"safehtml"
					};
					bool flag = false;
					foreach (string text in array)
					{
						if (lookup.Name.Length == text.Length && string.CompareOrdinal(lookup.Name, text) == 0)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						lookup.Context.ReportUndefined(lookup);
						lookup.Context.HandleError(JSError.UndeclaredFunction, false);
						return;
					}
				}
				else
				{
					lookup.Context.ReportUndefined(lookup);
					CallNode callNode = lookup.Parent as CallNode;
					bool flag2 = callNode != null && callNode.Function == lookup;
					lookup.Context.HandleError(flag2 ? JSError.UndeclaredFunction : JSError.UndeclaredVariable, false);
				}
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
		private static void AddGhostedFields(ActivationObject scope)
		{
			foreach (BindingIdentifier catchBinding in scope.GhostedCatchParameters)
			{
				ResolutionVisitor.ResolveGhostedCatchParameter(scope, catchBinding);
			}
			foreach (FunctionObject funcObject in scope.GhostedFunctions)
			{
				ResolutionVisitor.ResolveGhostedFunctions(scope, funcObject);
			}
			foreach (ActivationObject scope2 in scope.ChildScopes)
			{
				ResolutionVisitor.AddGhostedFields(scope2);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0003EB70 File Offset: 0x0003CD70
		private static void ResolveGhostedCatchParameter(ActivationObject scope, BindingIdentifier catchBinding)
		{
			if (catchBinding != null)
			{
				JSVariableField jsvariableField = scope[catchBinding.Name];
				if (jsvariableField == null)
				{
					jsvariableField = new JSVariableField(FieldType.GhostCatch, catchBinding.Name, FieldAttributes.PrivateScope, null)
					{
						OriginalContext = catchBinding.Context
					};
					scope.AddField(jsvariableField);
				}
				else if (jsvariableField.FieldType != FieldType.GhostCatch)
				{
					jsvariableField.IsAmbiguous = true;
					if (jsvariableField.OuterField != null)
					{
						catchBinding.Context.HandleError(JSError.AmbiguousCatchVar, false);
					}
				}
				catchBinding.VariableField.OuterField = jsvariableField;
				jsvariableField.GhostedField = catchBinding.VariableField;
				if (catchBinding.VariableField.RefCount > 0)
				{
					jsvariableField.AddReferences(catchBinding.VariableField.References);
				}
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003EC1C File Offset: 0x0003CE1C
		private static void ResolveGhostedFunctions(ActivationObject scope, FunctionObject funcObject)
		{
			BindingIdentifier binding = funcObject.Binding;
			JSVariableField variableField = binding.VariableField;
			JSVariableField jsvariableField = scope[binding.Name];
			if (jsvariableField == null)
			{
				jsvariableField = new JSVariableField(FieldType.GhostFunction, binding.Name, FieldAttributes.PrivateScope, funcObject)
				{
					OriginalContext = variableField.OriginalContext,
					CanCrunch = (binding.VariableField != null && binding.VariableField.CanCrunch)
				};
				scope.AddField(jsvariableField);
			}
			else if (jsvariableField.FieldType == FieldType.GhostFunction)
			{
				jsvariableField.IsAmbiguous = true;
			}
			else
			{
				jsvariableField.IsFunction = true;
				if (jsvariableField.OuterField != null)
				{
					jsvariableField.IsAmbiguous = true;
					binding.Context.HandleError(JSError.AmbiguousNamedFunctionExpression, false);
				}
				else if (jsvariableField.IsReferenced)
				{
					VariableDeclaration varDecl = funcObject.Parent as VariableDeclaration;
					if (ResolutionVisitor.IsBindingIdentifierWithName(varDecl, binding.Name))
					{
						BinaryOperator binaryOperator = funcObject.Parent as BinaryOperator;
						Lookup lookup;
						if (binaryOperator == null || binaryOperator.OperatorToken != JSToken.Assign || binaryOperator.Operand2 != funcObject || (lookup = (binaryOperator.Operand1 as Lookup)) == null || string.CompareOrdinal(lookup.Name, binding.Name) != 0)
						{
							jsvariableField.IsAmbiguous = true;
						}
					}
				}
			}
			variableField.OuterField = jsvariableField;
			jsvariableField.GhostedField = variableField;
			if (variableField.RefCount > 0)
			{
				jsvariableField.AddReferences(variableField.References);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003ED64 File Offset: 0x0003CF64
		private static bool IsBindingIdentifierWithName(VariableDeclaration varDecl, string name)
		{
			BindingIdentifier bindingIdentifier = (varDecl == null) ? null : (varDecl.Binding as BindingIdentifier);
			return bindingIdentifier != null && bindingIdentifier.Name.Length == name.Length && string.CompareOrdinal(bindingIdentifier.Name, name) == 0;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003EDAC File Offset: 0x0003CFAC
		private static void AddDeclaredNames(AstNode node, ICollection<INameDeclaration> collection)
		{
			INameDeclaration nameDeclaration = node as INameDeclaration;
			if (nameDeclaration != null)
			{
				collection.Add(nameDeclaration);
				return;
			}
			foreach (BindingIdentifier item in BindingsVisitor.Bindings(node))
			{
				collection.Add(item);
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003EE0C File Offset: 0x0003D00C
		private static ModuleScope GetModuleScope(AstNode node)
		{
			ActivationObject enclosingScope = node.EnclosingScope;
			ModuleScope moduleScope = enclosingScope as ModuleScope;
			if (moduleScope == null && enclosingScope != null)
			{
				node.Context.HandleError(JSError.ExportNotAtModuleLevel, false);
				ActivationObject parent = enclosingScope.Parent;
				while (parent != null && (moduleScope = (parent as ModuleScope)) == null)
				{
					parent = parent.Parent;
				}
			}
			return moduleScope;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003EE5C File Offset: 0x0003D05C
		public void Visit(ArrayLiteral node)
		{
			if (node != null)
			{
				if (node.Elements != null)
				{
					node.Elements.Accept(this);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003EE81 File Offset: 0x0003D081
		public void Visit(AspNetBlockNode node)
		{
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003EE84 File Offset: 0x0003D084
		public void Visit(AstNodeList node)
		{
			if (node != null)
			{
				for (int i = 0; i < node.Count; i++)
				{
					AstNode astNode = node[i];
					if (astNode != null)
					{
						astNode.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003EEB7 File Offset: 0x0003D0B7
		public void Visit(BinaryOperator node)
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
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
		public void Visit(BindingIdentifier node)
		{
			if (node != null)
			{
				ResolutionVisitor.AddDeclaredNames(node, this.CurrentLexicalScope.LexicallyDeclaredNames);
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003EF08 File Offset: 0x0003D108
		public void Visit(Block node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (!node.HasOwnScope && node.Parent != null && !(node.Parent is SwitchCase) && !(node.Parent is FunctionObject) && !(node.Parent is ModuleDeclaration) && !(node.Parent is ClassNode) && !(node.Parent is ConditionalCompilationComment))
				{
					node.EnclosingScope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Lexical)
					{
						Owner = node,
						IsInWithScope = (this.m_withDepth > 0)
					};
				}
				ActivationObject activationObject = node.HasOwnScope ? node.EnclosingScope : null;
				if (activationObject != null)
				{
					this.m_lexicalStack.Push(activationObject);
				}
				try
				{
					for (int i = 0; i < node.Count; i++)
					{
						AstNode astNode = node[i];
						if (astNode != null)
						{
							astNode.Accept(this);
						}
					}
				}
				finally
				{
					this.m_isUnreachable = false;
					if (activationObject != null)
					{
						this.m_lexicalStack.Pop();
					}
				}
				if (activationObject != null && node.EnclosingScope.ScopeType == ScopeType.Lexical && node.EnclosingScope.LexicallyDeclaredNames.Count == 0)
				{
					ResolutionVisitor.CollapseBlockScope(activationObject);
					node.EnclosingScope = null;
				}
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003F048 File Offset: 0x0003D248
		public void Visit(Break node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				this.m_isUnreachable = true;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003F060 File Offset: 0x0003D260
		public void Visit(CallNode node)
		{
			if (node != null)
			{
				if (node.Function != null)
				{
					node.Function.Accept(this);
				}
				if (node.Arguments != null)
				{
					node.Arguments.Accept(this);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003F09C File Offset: 0x0003D29C
		public void Visit(ClassNode node)
		{
			if (node != null)
			{
				if (node.Heritage != null)
				{
					node.Heritage.Accept(this);
				}
				BindingIdentifier bindingIdentifier = node.Binding as BindingIdentifier;
				node.Scope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Class)
				{
					Owner = node,
					IsInWithScope = (this.m_withDepth > 0),
					UseStrict = true,
					ScopeName = ((bindingIdentifier == null) ? null : bindingIdentifier.Name.IfNullOrWhiteSpace(null))
				};
				if (node.Binding != null)
				{
					if (node.ClassType == ClassType.Declaration)
					{
						ResolutionVisitor.AddDeclaredNames(node.Binding, this.CurrentLexicalScope.LexicallyDeclaredNames);
					}
					else
					{
						ResolutionVisitor.AddDeclaredNames(node.Binding, node.Scope.LexicallyDeclaredNames);
					}
				}
				if (node.Elements != null)
				{
					this.m_lexicalStack.Push(node.Scope);
					try
					{
						node.Elements.Accept(this);
					}
					finally
					{
						this.m_lexicalStack.Pop();
					}
				}
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		public void Visit(ComprehensionNode node)
		{
			if (node != null)
			{
				node.BlockScope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Lexical)
				{
					Owner = node,
					IsInWithScope = (this.m_withDepth > 0)
				};
				this.m_lexicalStack.Push(node.BlockScope);
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
					this.m_lexicalStack.Pop();
				}
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003F23C File Offset: 0x0003D43C
		public void Visit(ComprehensionForClause node)
		{
			if (node != null)
			{
				ResolutionVisitor.AddDeclaredNames(node.Binding, this.CurrentLexicalScope.LexicallyDeclaredNames);
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003F26B File Offset: 0x0003D46B
		public void Visit(ComprehensionIfClause node)
		{
			if (node != null && node.Condition != null)
			{
				node.Condition.Accept(this);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003F284 File Offset: 0x0003D484
		public void Visit(ConditionalCompilationComment node)
		{
			if (node != null && node.Statements != null)
			{
				node.Statements.Accept(this);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003F29D File Offset: 0x0003D49D
		public void Visit(ConditionalCompilationElse node)
		{
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003F29F File Offset: 0x0003D49F
		public void Visit(ConditionalCompilationElseIf node)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003F2A1 File Offset: 0x0003D4A1
		public void Visit(ConditionalCompilationEnd node)
		{
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003F2A3 File Offset: 0x0003D4A3
		public void Visit(ConditionalCompilationIf node)
		{
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003F2A5 File Offset: 0x0003D4A5
		public void Visit(ConditionalCompilationOn node)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003F2A7 File Offset: 0x0003D4A7
		public void Visit(ConditionalCompilationSet node)
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003F2AC File Offset: 0x0003D4AC
		public void Visit(Conditional node)
		{
			if (node != null)
			{
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				long orderIndex = this.m_orderIndex;
				if (node.TrueExpression != null)
				{
					node.TrueExpression.Accept(this);
				}
				long orderIndex2 = this.m_orderIndex;
				this.m_orderIndex = orderIndex;
				if (node.FalseExpression != null)
				{
					node.FalseExpression.Accept(this);
				}
				this.m_orderIndex = Math.Max(orderIndex2, this.m_orderIndex);
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003F32B File Offset: 0x0003D52B
		public void Visit(ConstantWrapper node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003F33C File Offset: 0x0003D53C
		public void Visit(ConstantWrapperPP node)
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003F340 File Offset: 0x0003D540
		public void Visit(ConstStatement node)
		{
			if (node != null)
			{
				node.Index = -1L;
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						variableDeclaration.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003F37B File Offset: 0x0003D57B
		public void Visit(ContinueNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				this.m_isUnreachable = true;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003F393 File Offset: 0x0003D593
		public void Visit(CustomNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003F3A4 File Offset: 0x0003D5A4
		public void Visit(DebuggerNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003F3B5 File Offset: 0x0003D5B5
		public void Visit(DirectivePrologue node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.UseStrict)
				{
					this.CurrentVariableScope.UseStrict = true;
				}
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003F3DA File Offset: 0x0003D5DA
		public void Visit(DoWhile node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003F413 File Offset: 0x0003D613
		public void Visit(EmptyStatement node)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003F418 File Offset: 0x0003D618
		public void Visit(ExportNode node)
		{
			if (node != null)
			{
				ModuleScope moduleScope = ResolutionVisitor.GetModuleScope(node);
				if (node.IsDefault)
				{
					if (moduleScope != null)
					{
						if (moduleScope.HasDefaultExport)
						{
							(node.DefaultContext ?? node.Context).HandleError(JSError.MultipleDefaultExports, true);
						}
						else
						{
							moduleScope.HasDefaultExport = true;
						}
					}
					using (IEnumerator<AstNode> enumerator = node.Children.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AstNode astNode = enumerator.Current;
							astNode.Accept(this);
						}
						return;
					}
				}
				if (!node.ModuleName.IsNullOrWhiteSpace())
				{
					if (node.Count == 0 && moduleScope != null)
					{
						moduleScope.IsNotComplete = true;
						return;
					}
				}
				else
				{
					foreach (AstNode astNode2 in node.Children)
					{
						astNode2.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003F50C File Offset: 0x0003D70C
		public void Visit(ForIn node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Collection != null)
				{
					node.Collection.Accept(this);
				}
				if (node.Variable != null)
				{
					LexicalDeclaration lexicalDeclaration = node.Variable as LexicalDeclaration;
					if (lexicalDeclaration != null)
					{
						node.BlockScope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Lexical)
						{
							Owner = node,
							IsInWithScope = (this.m_withDepth > 0)
						};
						this.m_lexicalStack.Push(node.BlockScope);
					}
				}
				try
				{
					if (node.Variable != null)
					{
						node.Variable.Accept(this);
					}
					if (node.Body != null)
					{
						node.Body.Accept(this);
					}
				}
				finally
				{
					if (node.BlockScope != null)
					{
						this.m_lexicalStack.Pop();
					}
				}
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		public void Visit(ForNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Initializer != null)
				{
					LexicalDeclaration lexicalDeclaration = node.Initializer as LexicalDeclaration;
					if (lexicalDeclaration != null)
					{
						node.BlockScope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Lexical)
						{
							Owner = node,
							IsInWithScope = (this.m_withDepth > 0)
						};
						this.m_lexicalStack.Push(node.BlockScope);
					}
				}
				try
				{
					if (node.Initializer != null)
					{
						node.Initializer.Accept(this);
					}
					if (node.Condition != null)
					{
						node.Condition.Accept(this);
					}
					if (node.Body != null)
					{
						node.Body.Accept(this);
					}
					if (node.Incrementer != null)
					{
						node.Incrementer.Accept(this);
					}
				}
				finally
				{
					if (node.BlockScope != null)
					{
						this.m_lexicalStack.Pop();
					}
				}
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		public void Visit(FunctionObject node)
		{
			if (node != null)
			{
				node.Index = -1L;
				ActivationObject parent = this.CurrentLexicalScope;
				if (node.FunctionType == FunctionType.Expression && node.Binding != null && !node.Binding.Name.IsNullOrWhiteSpace())
				{
					parent = new FunctionScope(parent, true, this.m_settings, node)
					{
						IsInWithScope = (this.m_withDepth > 0),
						ScopeName = node.Binding.Name
					};
					this.CurrentVariableScope.GhostedFunctions.Add(node);
				}
				bool hasSuperBinding = node.Parent != null && node.Parent.Parent is ClassNode;
				node.EnclosingScope = new FunctionScope(parent, node.FunctionType != FunctionType.Declaration, this.m_settings, node)
				{
					IsInWithScope = (this.m_withDepth > 0),
					HasSuperBinding = hasSuperBinding,
					ScopeName = ((node.Binding == null) ? null : node.Binding.Name.IfNullOrWhiteSpace(null))
				};
				this.m_lexicalStack.Push(node.EnclosingScope);
				this.m_variableStack.Push(node.EnclosingScope);
				long orderIndex = this.m_orderIndex;
				try
				{
					if (node.Body != null)
					{
						this.m_orderIndex = 0L;
						node.Body.Accept(this);
					}
					if (node.ParameterDeclarations != null)
					{
						node.ParameterDeclarations.Accept(this);
					}
				}
				finally
				{
					this.m_lexicalStack.Pop();
					this.m_variableStack.Pop();
					this.m_orderIndex = orderIndex;
				}
				if (node.FunctionType == FunctionType.Declaration && node.Binding != null && !node.Binding.Name.IsNullOrWhiteSpace())
				{
					ActivationObject currentLexicalScope = this.CurrentLexicalScope;
					currentLexicalScope.LexicallyDeclaredNames.Add(node.Binding);
					if (currentLexicalScope != this.CurrentVariableScope && this.m_scriptVersion != ScriptVersion.EcmaScript6 && this.m_settings.ScriptVersion != ScriptVersion.EcmaScript6)
					{
						node.Context.HandleError(JSError.MisplacedFunctionDeclaration, false);
						this.CurrentVariableScope.GhostedFunctions.Add(node);
					}
				}
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003F8E8 File Offset: 0x0003DAE8
		public void Visit(GetterSetter node)
		{
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003F8EA File Offset: 0x0003DAEA
		public void Visit(GroupingOperator node)
		{
			if (node != null)
			{
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003F910 File Offset: 0x0003DB10
		public void Visit(IfNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				long orderIndex = this.m_orderIndex;
				if (node.TrueBlock != null)
				{
					node.TrueBlock.Accept(this);
				}
				long orderIndex2 = this.m_orderIndex;
				this.m_orderIndex = orderIndex;
				if (node.FalseBlock != null)
				{
					node.FalseBlock.Accept(this);
				}
				this.m_orderIndex = Math.Max(orderIndex2, this.m_orderIndex);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003F98F File Offset: 0x0003DB8F
		public void Visit(ImportantComment node)
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003F991 File Offset: 0x0003DB91
		public void Visit(ImportExportSpecifier node)
		{
			if (node != null && node.LocalIdentifier != null)
			{
				node.LocalIdentifier.Accept(this);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003F9AC File Offset: 0x0003DBAC
		public void Visit(ImportNode node)
		{
			if (node != null)
			{
				if (node.ModuleName.IsNullOrWhiteSpace())
				{
					node.Context.HandleError(JSError.ImportNoModuleName, true);
				}
				foreach (AstNode astNode in node.Children)
				{
					astNode.Accept(this);
				}
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		public void Visit(InitializerNode node)
		{
			if (node != null)
			{
				if (node.Binding != null)
				{
					node.Binding.Accept(this);
				}
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
				}
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003FA49 File Offset: 0x0003DC49
		public void Visit(LabeledStatement node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Statement != null)
				{
					node.Statement.Accept(this);
				}
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003FA70 File Offset: 0x0003DC70
		public void Visit(LexicalDeclaration node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						variableDeclaration.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003FAAF File Offset: 0x0003DCAF
		public void Visit(Lookup node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				this.CurrentLexicalScope.ScopeLookups.Add(node);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003FAD1 File Offset: 0x0003DCD1
		public void Visit(Member node)
		{
			if (node != null)
			{
				if (node.Root != null)
				{
					node.Root.Accept(this);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003FAF8 File Offset: 0x0003DCF8
		public void Visit(ModuleDeclaration node)
		{
			if (node != null)
			{
				if (node.Binding == null)
				{
					if (node.Body == null)
					{
						return;
					}
					ModuleScope enclosingScope = new ModuleScope(node, this.CurrentLexicalScope, this.m_settings)
					{
						IsInWithScope = (this.m_withDepth > 0),
						ScopeName = node.ModuleName.IfNullOrWhiteSpace(null)
					};
					node.EnclosingScope = enclosingScope;
					this.m_variableStack.Push(node.EnclosingScope);
					this.m_lexicalStack.Push(node.EnclosingScope);
					try
					{
						node.Body.Accept(this);
						return;
					}
					finally
					{
						this.m_variableStack.Pop();
						this.m_lexicalStack.Pop();
					}
				}
				node.Binding.Accept(this);
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003FBC4 File Offset: 0x0003DDC4
		public void Visit(ObjectLiteral node)
		{
			if (node != null)
			{
				node.Properties.Accept(this);
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003FBE1 File Offset: 0x0003DDE1
		public void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003FBE4 File Offset: 0x0003DDE4
		public void Visit(ObjectLiteralProperty node)
		{
			if (node != null)
			{
				if (node.Value != null)
				{
					node.Value.Accept(this);
				}
				if (node.Name == null && !(node.Value is Lookup))
				{
					node.Context.HandleError(JSError.ImplicitPropertyNameMustBeIdentifier, true);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003FC3C File Offset: 0x0003DE3C
		public void Visit(ParameterDeclaration node)
		{
			if (node != null && node.Initializer != null)
			{
				node.Initializer.Accept(this);
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003FC55 File Offset: 0x0003DE55
		public void Visit(RegExpLiteral node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003FC66 File Offset: 0x0003DE66
		public void Visit(ReturnNode node)
		{
			if (node != null)
			{
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
				node.Index = this.NextOrderIndex;
				this.m_isUnreachable = true;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003FC94 File Offset: 0x0003DE94
		public void Visit(Switch node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Expression != null)
				{
					node.Expression.Accept(this);
				}
				node.BlockScope = new BlockScope(this.CurrentLexicalScope, this.m_settings, ScopeType.Block)
				{
					Owner = node,
					IsInWithScope = (this.m_withDepth > 0)
				};
				this.m_lexicalStack.Push(node.BlockScope);
				try
				{
					if (node.Cases != null)
					{
						node.Cases.Accept(this);
					}
				}
				finally
				{
					this.m_lexicalStack.Pop();
				}
				if (node.BlockScope.LexicallyDeclaredNames.Count == 0)
				{
					ResolutionVisitor.CollapseBlockScope(node.BlockScope);
					node.BlockScope = null;
				}
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003FD60 File Offset: 0x0003DF60
		public void Visit(SwitchCase node)
		{
			if (node != null)
			{
				if (node.CaseValue != null)
				{
					node.CaseValue.Accept(this);
				}
				if (node.Statements != null)
				{
					node.Statements.Accept(this);
				}
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003FD8D File Offset: 0x0003DF8D
		public void Visit(TemplateLiteral node)
		{
			if (node != null)
			{
				if (node.Function != null)
				{
					node.Function.Accept(this);
				}
				if (node.Expressions != null)
				{
					node.Expressions.Accept(this);
				}
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003FDBA File Offset: 0x0003DFBA
		public void Visit(TemplateLiteralExpression node)
		{
			if (node != null && node.Expression != null)
			{
				node.Expression.Accept(this);
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003FDD3 File Offset: 0x0003DFD3
		public void Visit(ThisLiteral node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003FDE4 File Offset: 0x0003DFE4
		public void Visit(ThrowNode node)
		{
			if (node != null)
			{
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
				node.Index = this.NextOrderIndex;
				this.m_isUnreachable = true;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003FE10 File Offset: 0x0003E010
		public void Visit(TryNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.TryBlock != null)
				{
					node.TryBlock.Accept(this);
				}
				if (node.CatchParameter != null)
				{
					BindingIdentifier bindingIdentifier = node.CatchParameter.Binding as BindingIdentifier;
					if (bindingIdentifier != null)
					{
						this.CurrentVariableScope.GhostedCatchParameters.Add(bindingIdentifier);
					}
				}
				if (node.CatchBlock != null)
				{
					node.CatchBlock.EnclosingScope = new CatchScope(this.CurrentLexicalScope, this.m_settings)
					{
						Owner = node.CatchBlock,
						CatchParameter = node.CatchParameter,
						IsInWithScope = (this.m_withDepth > 0)
					};
					ResolutionVisitor.AddDeclaredNames(node.CatchParameter, node.CatchBlock.EnclosingScope.LexicallyDeclaredNames);
					node.CatchBlock.Accept(this);
				}
				if (node.FinallyBlock != null)
				{
					node.FinallyBlock.Accept(this);
				}
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		public void Visit(UnaryOperator node)
		{
			if (node != null)
			{
				if (node.Operand != null)
				{
					node.Operand.Accept(this);
				}
				node.Index = this.NextOrderIndex;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003FF20 File Offset: 0x0003E120
		public void Visit(Var node)
		{
			if (node != null)
			{
				node.Index = -1L;
				for (int i = 0; i < node.Count; i++)
				{
					VariableDeclaration variableDeclaration = node[i];
					if (variableDeclaration != null)
					{
						variableDeclaration.Accept(this);
					}
				}
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003FF5C File Offset: 0x0003E15C
		public void Visit(VariableDeclaration node)
		{
			if (node != null)
			{
				if (node.Parent is LexicalDeclaration)
				{
					ResolutionVisitor.AddDeclaredNames(node.Binding, this.CurrentLexicalScope.LexicallyDeclaredNames);
				}
				else
				{
					ResolutionVisitor.AddDeclaredNames(node.Binding, this.CurrentLexicalScope.VarDeclaredNames);
					ResolutionVisitor.AddDeclaredNames(node.Binding, this.CurrentVariableScope.VarDeclaredNames);
				}
				if (node.Initializer != null)
				{
					node.Initializer.Accept(this);
					node.Index = this.NextOrderIndex;
					return;
				}
				node.Index = -1L;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003FFE6 File Offset: 0x0003E1E6
		public void Visit(WhileNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.Condition != null)
				{
					node.Condition.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.Accept(this);
				}
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00040020 File Offset: 0x0003E220
		public void Visit(WithNode node)
		{
			if (node != null)
			{
				node.Index = this.NextOrderIndex;
				if (node.WithObject != null)
				{
					node.WithObject.Accept(this);
				}
				if (node.Body != null)
				{
					node.Body.EnclosingScope = new WithScope(this.CurrentLexicalScope, this.m_settings)
					{
						Owner = node
					};
					try
					{
						this.m_withDepth++;
						node.Body.Accept(this);
					}
					finally
					{
						this.m_withDepth--;
					}
				}
			}
		}

		// Token: 0x04000523 RID: 1315
		private long m_orderIndex;

		// Token: 0x04000524 RID: 1316
		private bool m_isUnreachable;

		// Token: 0x04000525 RID: 1317
		private int m_withDepth;

		// Token: 0x04000526 RID: 1318
		private Stack<ActivationObject> m_lexicalStack;

		// Token: 0x04000527 RID: 1319
		private Stack<ActivationObject> m_variableStack;

		// Token: 0x04000528 RID: 1320
		private CodeSettings m_settings;

		// Token: 0x04000529 RID: 1321
		private ScriptVersion m_scriptVersion;
	}
}
