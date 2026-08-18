using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001C RID: 28
	public abstract class ActivationObject
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004C95 File Offset: 0x00002E95
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00004C9D File Offset: 0x00002E9D
		internal bool Existing { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004CA6 File Offset: 0x00002EA6
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00004CAE File Offset: 0x00002EAE
		public AstNode Owner { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004CB7 File Offset: 0x00002EB7
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00004CBF File Offset: 0x00002EBF
		public bool HasSuperBinding { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004CC8 File Offset: 0x00002EC8
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public bool UseStrict
		{
			get
			{
				return this.m_useStrict;
			}
			set
			{
				if (value)
				{
					this.m_useStrict = value;
					foreach (ActivationObject activationObject in this.ChildScopes)
					{
						activationObject.UseStrict = value;
					}
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004D28 File Offset: 0x00002F28
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00004D48 File Offset: 0x00002F48
		public bool IsKnownAtCompileTime
		{
			get
			{
				return this.m_isKnownAtCompileTime;
			}
			set
			{
				this.m_isKnownAtCompileTime = value;
				if (!value && this.Settings.EvalTreatment == EvalTreatment.MakeAllSafe)
				{
					FunctionObject functionObject = this.Owner as FunctionObject;
					if (functionObject == null)
					{
						this.Parent.IfNotNull((ActivationObject p) => p.IsKnownAtCompileTime = false);
						return;
					}
					if (functionObject.IsReferenced)
					{
						this.Parent.IsKnownAtCompileTime = false;
					}
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004DBA File Offset: 0x00002FBA
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public ActivationObject Parent { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00004DCB File Offset: 0x00002FCB
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00004DD3 File Offset: 0x00002FD3
		public bool IsInWithScope { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00004DDC File Offset: 0x00002FDC
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public IDictionary<string, JSVariableField> NameTable { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00004DED File Offset: 0x00002FED
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00004DF5 File Offset: 0x00002FF5
		public IList<ActivationObject> ChildScopes { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00004DFE File Offset: 0x00002FFE
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00004E06 File Offset: 0x00003006
		public ICollection<Lookup> ScopeLookups { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004E0F File Offset: 0x0000300F
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00004E17 File Offset: 0x00003017
		public ICollection<INameDeclaration> VarDeclaredNames { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004E20 File Offset: 0x00003020
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00004E28 File Offset: 0x00003028
		public ICollection<INameDeclaration> LexicallyDeclaredNames { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00004E31 File Offset: 0x00003031
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00004E39 File Offset: 0x00003039
		public ICollection<BindingIdentifier> GhostedCatchParameters { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00004E42 File Offset: 0x00003042
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00004E4A File Offset: 0x0000304A
		public ICollection<FunctionObject> GhostedFunctions { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00004E53 File Offset: 0x00003053
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00004E5B File Offset: 0x0000305B
		public string ScopeName { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00004E64 File Offset: 0x00003064
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00004E6C File Offset: 0x0000306C
		public ScopeType ScopeType { get; protected set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004E75 File Offset: 0x00003075
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00004E7D File Offset: 0x0000307D
		private protected CodeSettings Settings { protected get; private set; }

		// Token: 0x060001F0 RID: 496 RVA: 0x00004E88 File Offset: 0x00003088
		protected ActivationObject(ActivationObject parent, CodeSettings codeSettings)
		{
			this.m_isKnownAtCompileTime = true;
			this.m_useStrict = false;
			this.Settings = codeSettings;
			this.Parent = parent;
			this.NameTable = new Dictionary<string, JSVariableField>();
			this.ChildScopes = new List<ActivationObject>();
			if (parent != null)
			{
				parent.ChildScopes.Add(this);
				this.UseStrict = parent.UseStrict;
			}
			this.ScopeLookups = new HashSet<Lookup>();
			this.VarDeclaredNames = new HashSet<INameDeclaration>();
			this.LexicallyDeclaredNames = new HashSet<INameDeclaration>();
			this.GhostedCatchParameters = new HashSet<BindingIdentifier>();
			this.GhostedFunctions = new HashSet<FunctionObject>();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00004F20 File Offset: 0x00003120
		public static bool DeleteFromBindingPattern(AstNode binding, bool normalizePattern)
		{
			bool flag = false;
			if (binding != null)
			{
				AstNodeList astNodeList = binding.Parent as AstNodeList;
				ObjectLiteralProperty objectLiteralProperty;
				VariableDeclaration variableDeclaration;
				if (astNodeList != null && astNodeList.Parent is ArrayLiteral)
				{
					flag = astNodeList.ReplaceChild(binding, new ConstantWrapper(Missing.Value, PrimitiveType.Other, binding.Context.Clone()));
				}
				else if ((objectLiteralProperty = (binding.Parent as ObjectLiteralProperty)) != null)
				{
					astNodeList = (objectLiteralProperty.Parent as AstNodeList);
					flag = objectLiteralProperty.Parent.ReplaceChild(objectLiteralProperty, null);
				}
				else if ((variableDeclaration = (binding.Parent as VariableDeclaration)) != null)
				{
					Declaration declaration = variableDeclaration.Parent as Declaration;
					if (declaration != null)
					{
						ForIn forIn = declaration.Parent as ForIn;
						if ((forIn == null || forIn.Variable != declaration) && (variableDeclaration.Initializer == null || variableDeclaration.Initializer.IsConstant))
						{
							flag = variableDeclaration.Parent.ReplaceChild(variableDeclaration, null);
							if (declaration.Count == 0)
							{
								declaration.Parent.ReplaceChild(declaration, null);
							}
						}
					}
				}
				if (flag)
				{
					BindingIdentifier bindingIdentifier = binding as BindingIdentifier;
					if (bindingIdentifier != null)
					{
						bindingIdentifier.VariableField.Declarations.Remove(bindingIdentifier);
						if (!bindingIdentifier.VariableField.IsReferenced && bindingIdentifier.VariableField.Declarations.Count == 0)
						{
							bindingIdentifier.VariableField.WasRemoved = true;
						}
					}
					if (normalizePattern && astNodeList != null)
					{
						if (astNodeList.Parent is ArrayLiteral)
						{
							for (int i = astNodeList.Count - 1; i >= 0; i--)
							{
								ConstantWrapper constantWrapper = astNodeList[i] as ConstantWrapper;
								if (constantWrapper == null || constantWrapper.Value != Missing.Value)
								{
									break;
								}
								astNodeList.RemoveAt(i);
							}
						}
						if (astNodeList.Count == 0)
						{
							ActivationObject.DeleteFromBindingPattern(astNodeList.Parent, normalizePattern);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000050F4 File Offset: 0x000032F4
		public static void RemoveBinding(AstNode binding)
		{
			using (IEnumerator<BindingIdentifier> enumerator = BindingsVisitor.Bindings(binding).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BindingIdentifier boundName = enumerator.Current;
					boundName.VariableField.IfNotNull((JSVariableField v) => v.Declarations.Remove(boundName));
				}
			}
			ActivationObject.DeleteFromBindingPattern(binding, true);
		}

		// Token: 0x060001F3 RID: 499
		public abstract void DeclareScope();

		// Token: 0x060001F4 RID: 500 RVA: 0x00005174 File Offset: 0x00003374
		protected void DefineLexicalDeclarations()
		{
			foreach (INameDeclaration nameDeclaration in this.LexicallyDeclaredNames)
			{
				AstNode astNode = nameDeclaration.Parent as FunctionObject;
				if (astNode == null)
				{
					astNode = (nameDeclaration.Parent as ClassNode);
				}
				this.DefineField(nameDeclaration, astNode);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000051E0 File Offset: 0x000033E0
		protected void DefineVarDeclarations()
		{
			foreach (INameDeclaration nameDecl in this.VarDeclaredNames)
			{
				this.DefineField(nameDecl, null);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005240 File Offset: 0x00003440
		private void DefineField(INameDeclaration nameDecl, AstNode fieldValue)
		{
			JSVariableField jsvariableField = this[nameDecl.Name];
			if (nameDecl.IsParameter)
			{
				if (jsvariableField == null)
				{
					jsvariableField = new JSVariableField(FieldType.CatchError, nameDecl.Name, FieldAttributes.PrivateScope, null)
					{
						OriginalContext = nameDecl.Context,
						IsDeclared = true
					};
					this.AddField(jsvariableField);
				}
				else
				{
					jsvariableField.OriginalContext.HandleError(JSError.DuplicateCatch, true);
				}
			}
			else
			{
				if (jsvariableField == null)
				{
					jsvariableField = this.CreateField(nameDecl.Name, null, FieldAttributes.PrivateScope);
					jsvariableField.OriginalContext = nameDecl.Context;
					jsvariableField.IsDeclared = true;
					jsvariableField.IsFunction = (nameDecl is FunctionObject);
					jsvariableField.FieldValue = fieldValue;
					AstNode astNode = nameDecl.Parent.IfNotNull((AstNode p) => p.Parent);
					LexicalDeclaration lexicalDeclaration;
					jsvariableField.InitializationOnly = (astNode is ConstStatement || ((lexicalDeclaration = (astNode as LexicalDeclaration)) != null && lexicalDeclaration.StatementToken == JSToken.Const));
					this.AddField(jsvariableField);
				}
				else
				{
					if (nameDecl.Parent.IfNotNull((AstNode p) => p.Parent) is LexicalDeclaration)
					{
						nameDecl.Context.HandleError(JSError.DuplicateLexicalDeclaration, true);
					}
					if (nameDecl.Initializer != null)
					{
						INameReference nameReference = nameDecl as INameReference;
						if (nameReference != null)
						{
							jsvariableField.AddReference(nameReference);
						}
					}
					if (fieldValue != null)
					{
						jsvariableField.FieldValue = fieldValue;
					}
				}
				AstNode astNode2 = (AstNode)nameDecl;
				while ((astNode2 = astNode2.Parent) != null && !(astNode2 is Block))
				{
					if (astNode2 is ExportNode)
					{
						jsvariableField.IsExported = true;
						break;
					}
					if (astNode2 is ImportNode)
					{
						jsvariableField.InitializationOnly = true;
						break;
					}
				}
			}
			nameDecl.VariableField = jsvariableField;
			jsvariableField.Declarations.Add(nameDecl);
			if (this.IsInWithScope || nameDecl.RenameNotAllowed)
			{
				jsvariableField.CanCrunch = false;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00005418 File Offset: 0x00003618
		internal virtual void AnalyzeScope()
		{
			this.AnalyzeNonGlobalScope();
			this.ManualRenameFields();
			foreach (ActivationObject activationObject in this.ChildScopes)
			{
				activationObject.AnalyzeScope();
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00005470 File Offset: 0x00003670
		private void AnalyzeNonGlobalScope()
		{
			foreach (JSVariableField jsvariableField in this.NameTable.Values)
			{
				if (jsvariableField.OuterField == null)
				{
					if (!jsvariableField.IsReferenced && !jsvariableField.IsGenerated && jsvariableField.FieldType != FieldType.CatchError && jsvariableField.FieldType != FieldType.GhostCatch && !jsvariableField.IsExported && jsvariableField.OriginalContext != null)
					{
						this.UnreferencedVariableField(jsvariableField);
					}
					else if (jsvariableField.FieldType == FieldType.Local && jsvariableField.RefCount == 1 && this.IsKnownAtCompileTime && this.Settings.RemoveUnneededCode && this.Settings.IsModificationAllowed(TreeModifications.RemoveUnusedVariables))
					{
						ActivationObject.SingleReferenceVariableField(jsvariableField);
					}
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005548 File Offset: 0x00003748
		private void UnreferencedVariableField(JSVariableField variableField)
		{
			FunctionObject functionObject = variableField.FieldValue as FunctionObject;
			if (functionObject != null)
			{
				this.UnreferencedFunction(variableField, functionObject);
				return;
			}
			if (variableField.FieldType != FieldType.Argument && !variableField.WasRemoved)
			{
				this.UnreferencedVariable(variableField);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000559C File Offset: 0x0000379C
		private void UnreferencedFunction(JSVariableField variableField, FunctionObject functionObject)
		{
			if (functionObject.Binding != null && variableField.FieldType != FieldType.GhostFunction)
			{
				if (JSScanner.IsValidIdentifier(functionObject.Binding.Name))
				{
					Context context = functionObject.Binding.Context ?? variableField.OriginalContext;
					context.HandleError(JSError.FunctionNotReferenced, false);
					if (this.IsKnownAtCompileTime && this.Settings.MinifyCode && this.Settings.RemoveUnneededCode && !(this is BlockScope))
					{
						functionObject.Parent.IfNotNull((AstNode p) => p.ReplaceChild(functionObject, null));
						return;
					}
				}
				else
				{
					variableField.CanCrunch = false;
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005674 File Offset: 0x00003874
		private void UnreferencedVariable(JSVariableField variableField)
		{
			bool flag = true;
			if (variableField.Declarations.Count == 1 && this.IsKnownAtCompileTime)
			{
				INameDeclaration onlyDeclaration = variableField.OnlyDeclaration;
				VariableDeclaration variableDeclaration = onlyDeclaration.IfNotNull((INameDeclaration decl) => decl.Parent as VariableDeclaration);
				BindingIdentifier binding;
				if (variableDeclaration != null)
				{
					Declaration declaration = variableDeclaration.Parent as Declaration;
					if (declaration != null && (variableDeclaration.Initializer == null || variableDeclaration.Initializer.IsConstant))
					{
						ForIn forIn = declaration.Parent as ForIn;
						if (forIn != null && declaration == forIn.Variable)
						{
							flag = false;
						}
						else if (this.Settings.RemoveUnneededCode && this.Settings.IsModificationAllowed(TreeModifications.RemoveUnusedVariables))
						{
							variableField.Declarations.Remove(onlyDeclaration);
							if (variableField.GhostedField == null)
							{
								variableField.WasRemoved = true;
							}
							declaration.Remove(variableDeclaration);
							if (declaration.Count == 0)
							{
								declaration.Parent.ReplaceChild(declaration, null);
							}
						}
					}
					else if (variableDeclaration.Parent is ForIn)
					{
						flag = false;
					}
				}
				else if ((binding = (onlyDeclaration as BindingIdentifier)) != null)
				{
					ActivationObject.DeleteFromBindingPattern(binding, true);
				}
			}
			if (flag && variableField.HasNoReferences)
			{
				variableField.OriginalContext.HandleError(JSError.VariableDefinedNotReferenced, false);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005814 File Offset: 0x00003A14
		private static void SingleReferenceVariableField(JSVariableField variableField)
		{
			if (variableField.Declarations.Count == 1)
			{
				INameDeclaration onlyDeclaration = variableField.OnlyDeclaration;
				VariableDeclaration varDecl = onlyDeclaration.IfNotNull((INameDeclaration d) => d.Parent as VariableDeclaration);
				if (varDecl != null && varDecl.Initializer != null && varDecl.Initializer.IsConstant)
				{
					INameReference onlyReference = variableField.OnlyReference;
					if (onlyReference != null && !onlyReference.IsAssignment && onlyReference.VariableField != null && onlyReference.VariableField.OuterField == null && onlyReference.VariableField.CanCrunch && !onlyReference.VariableField.IsExported && varDecl.Index < onlyReference.Index && !ActivationObject.IsIterativeReference(varDecl.Initializer, onlyReference))
					{
						Declaration declaration = varDecl.Parent as Declaration;
						if (declaration != null)
						{
							variableField.References.Remove(onlyReference);
							AstNode refNode = onlyReference as AstNode;
							refNode.Parent.IfNotNull((AstNode p) => p.ReplaceChild(refNode, varDecl.Initializer));
							variableField.Declarations.Remove(onlyDeclaration);
							variableField.WasRemoved = true;
							declaration.Remove(varDecl);
							if (declaration.Count == 0)
							{
								declaration.Parent.IfNotNull((AstNode p) => p.ReplaceChild(declaration, null));
							}
						}
					}
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000059FC File Offset: 0x00003BFC
		private static bool IsIterativeReference(AstNode initializer, INameReference reference)
		{
			RegExpLiteral regExpLiteral = initializer as RegExpLiteral;
			if (initializer is ArrayLiteral || initializer is ObjectLiteral || (regExpLiteral != null && regExpLiteral.PatternSwitches != null && regExpLiteral.PatternSwitches.IndexOf("g", StringComparison.OrdinalIgnoreCase) >= 0))
			{
				Block parentBlock = ActivationObject.GetParentBlock(initializer);
				AstNode astNode = reference as AstNode;
				AstNode parent = astNode.Parent;
				while (parent != null && parent != parentBlock && !(parent is FunctionObject))
				{
					if (parent is WhileNode || parent is DoWhile)
					{
						return true;
					}
					ForNode forNode = parent as ForNode;
					if (forNode != null && astNode != forNode.Initializer)
					{
						return true;
					}
					ForIn forIn = parent as ForIn;
					if (forIn != null && astNode == forIn.Body)
					{
						return true;
					}
					astNode = parent;
					parent = parent.Parent;
				}
			}
			return false;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005AB8 File Offset: 0x00003CB8
		private static Block GetParentBlock(AstNode node)
		{
			while (node != null)
			{
				Block block = node as Block;
				if (block != null)
				{
					return block;
				}
				node = node.Parent;
			}
			return null;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005AE0 File Offset: 0x00003CE0
		protected void ManualRenameFields()
		{
			if (this.Settings.IsModificationAllowed(TreeModifications.LocalRenaming))
			{
				if (this.Settings.HasRenamePairs)
				{
					foreach (JSVariableField jsvariableField in this.NameTable.Values)
					{
						if (jsvariableField.OuterField == null && jsvariableField.FieldType != FieldType.Arguments && jsvariableField.FieldType != FieldType.Predefined)
						{
							string newName = this.Settings.GetNewName(jsvariableField.Name);
							if (!string.IsNullOrEmpty(newName))
							{
								jsvariableField.CanCrunch = true;
								jsvariableField.CrunchedName = newName;
								jsvariableField.CanCrunch = false;
							}
						}
					}
				}
				if (this.Settings.LocalRenaming != LocalRenaming.KeepAll)
				{
					foreach (string key in this.Settings.NoAutoRenameCollection)
					{
						JSVariableField jsvariableField2;
						if (this.NameTable.TryGetValue(key, out jsvariableField2) && jsvariableField2.OuterField == null && jsvariableField2.CanCrunch)
						{
							jsvariableField2.CanCrunch = false;
						}
					}
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005C10 File Offset: 0x00003E10
		internal void ValidateGeneratedNames()
		{
			foreach (JSVariableField jsvariableField in this.NameTable.Values)
			{
				if (jsvariableField.IsGenerated && jsvariableField.CrunchedName == null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					this.GenerateAvoidList(hashSet, jsvariableField.Name);
					CrunchEnumerator crunchEnumerator = new CrunchEnumerator(hashSet);
					jsvariableField.CrunchedName = crunchEnumerator.NextName();
				}
			}
			foreach (ActivationObject activationObject in this.ChildScopes)
			{
				if (!activationObject.Existing)
				{
					activationObject.ValidateGeneratedNames();
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005CE0 File Offset: 0x00003EE0
		private bool GenerateAvoidList(HashSet<string> table, string name)
		{
			bool flag = false;
			foreach (ActivationObject activationObject in this.ChildScopes)
			{
				if (activationObject.GenerateAvoidList(table, name))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				flag = this.NameTable.ContainsKey(name);
			}
			if (flag)
			{
				foreach (JSVariableField jsvariableField in this.NameTable.Values)
				{
					table.Add(jsvariableField.ToString());
				}
			}
			return flag;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005D94 File Offset: 0x00003F94
		internal virtual void AutoRenameFields()
		{
			if (this.m_isKnownAtCompileTime)
			{
				IEnumerable<JSVariableField> uncrunchedLocals = this.GetUncrunchedLocals();
				if (uncrunchedLocals != null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					foreach (JSVariableField jsvariableField in this.NameTable.Values)
					{
						if (!jsvariableField.CanCrunch || jsvariableField.CrunchedName != null || (jsvariableField.OuterField != null && !jsvariableField.IsGenerated && jsvariableField.OwningScope != null && !jsvariableField.OwningScope.IsKnownAtCompileTime))
						{
							hashSet.Add(jsvariableField.ToString());
						}
					}
					CrunchEnumerator crunchEnumerator = new CrunchEnumerator(hashSet);
					foreach (JSVariableField jsvariableField2 in uncrunchedLocals)
					{
						if (jsvariableField2.CanCrunch && (jsvariableField2.RefCount > 0 || jsvariableField2.IsDeclared || jsvariableField2.IsPlaceholder || !this.Settings.RemoveFunctionExpressionNames || !this.Settings.IsModificationAllowed(TreeModifications.RemoveFunctionExpressionNames)))
						{
							jsvariableField2.CrunchedName = crunchEnumerator.NextName();
						}
					}
				}
			}
			foreach (ActivationObject activationObject in this.ChildScopes)
			{
				activationObject.AutoRenameFields();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005F18 File Offset: 0x00004118
		internal IEnumerable<JSVariableField> GetUncrunchedLocals()
		{
			List<JSVariableField> list = new List<JSVariableField>(this.NameTable.Count);
			foreach (JSVariableField jsvariableField in this.NameTable.Values)
			{
				if (jsvariableField != null && jsvariableField.OuterField == null && jsvariableField.CrunchedName == null && jsvariableField.CanCrunch && !jsvariableField.WasRemoved && (this.Settings.LocalRenaming == LocalRenaming.CrunchAll || !jsvariableField.Name.StartsWith("L_", StringComparison.Ordinal)) && (!this.Settings.PreserveFunctionNames || !jsvariableField.IsFunction))
				{
					list.Add(jsvariableField);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			list.Sort(ReferenceComparer.Instance);
			return list;
		}

		// Token: 0x17000082 RID: 130
		public virtual JSVariableField this[string name]
		{
			get
			{
				JSVariableField result;
				if (!this.NameTable.TryGetValue(name, out result))
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000600C File Offset: 0x0000420C
		public JSVariableField CanReference(string name)
		{
			JSVariableField jsvariableField = this[name];
			if (jsvariableField == null)
			{
				ActivationObject parent = this.Parent;
				while (parent != null && jsvariableField == null)
				{
					jsvariableField = parent[name];
					parent = parent.Parent;
				}
			}
			return jsvariableField;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006044 File Offset: 0x00004244
		public JSVariableField FindReference(string name)
		{
			JSVariableField jsvariableField = this[name];
			if (jsvariableField == null && name != null)
			{
				if (string.CompareOrdinal(name, "super") == 0 && this.HasSuperBinding)
				{
					jsvariableField = new JSVariableField(FieldType.Super, name, FieldAttributes.PrivateScope, null);
					this.NameTable.Add(name, jsvariableField);
				}
				else if (this.Parent != null)
				{
					jsvariableField = this.CreateInnerField(this.Parent.FindReference(name));
					jsvariableField.IsPlaceholder = true;
				}
				else
				{
					jsvariableField = this.AddField(new JSVariableField(FieldType.UndefinedGlobal, name, FieldAttributes.PrivateScope, null));
				}
			}
			return jsvariableField;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000060C4 File Offset: 0x000042C4
		public virtual JSVariableField DeclareField(string name, object value, FieldAttributes attributes)
		{
			JSVariableField jsvariableField;
			if (!this.NameTable.TryGetValue(name, out jsvariableField))
			{
				jsvariableField = this.CreateField(name, value, attributes);
				this.AddField(jsvariableField);
			}
			return jsvariableField;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006102 File Offset: 0x00004302
		public virtual JSVariableField CreateField(JSVariableField outerField)
		{
			return outerField.IfNotNull((JSVariableField o) => new JSVariableField(o.FieldType, o));
		}

		// Token: 0x06000209 RID: 521
		public abstract JSVariableField CreateField(string name, object value, FieldAttributes attributes);

		// Token: 0x0600020A RID: 522 RVA: 0x00006128 File Offset: 0x00004328
		public virtual JSVariableField CreateInnerField(JSVariableField outerField)
		{
			JSVariableField jsvariableField = null;
			if (outerField != null)
			{
				jsvariableField = this.CreateField(outerField);
				this.AddField(jsvariableField);
			}
			return jsvariableField;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000614B File Offset: 0x0000434B
		internal JSVariableField AddField(JSVariableField variableField)
		{
			this.NameTable[variableField.Name] = variableField;
			variableField.OwningScope = ((variableField.OuterField == null) ? this : variableField.OuterField.OwningScope);
			return variableField;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000617C File Offset: 0x0000437C
		public INameDeclaration VarDeclaredName(string name)
		{
			foreach (INameDeclaration nameDeclaration in this.VarDeclaredNames)
			{
				if (string.CompareOrdinal(nameDeclaration.Name, name) == 0)
				{
					return nameDeclaration;
				}
			}
			return null;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000061D8 File Offset: 0x000043D8
		public INameDeclaration LexicallyDeclaredName(string name)
		{
			foreach (INameDeclaration nameDeclaration in this.LexicallyDeclaredNames)
			{
				if (string.CompareOrdinal(nameDeclaration.Name, name) == 0)
				{
					return nameDeclaration;
				}
			}
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006234 File Offset: 0x00004434
		public void AddGlobal(string name)
		{
			ActivationObject activationObject = this;
			while (activationObject.Parent != null)
			{
				activationObject = activationObject.Parent;
			}
			if (activationObject[name] == null)
			{
				activationObject.AddField(activationObject.CreateField(name, null, FieldAttributes.PrivateScope));
			}
		}

		// Token: 0x04000057 RID: 87
		private bool m_useStrict;

		// Token: 0x04000058 RID: 88
		private bool m_isKnownAtCompileTime;
	}
}
