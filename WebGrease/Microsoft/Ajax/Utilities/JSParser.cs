using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000A2 RID: 162
	public class JSParser
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002BD2F File Offset: 0x00029F2F
		private Context CurrentPositionContext
		{
			get
			{
				return this.m_currentToken.FlattenToStart();
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002BD3C File Offset: 0x00029F3C
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x0002BD44 File Offset: 0x00029F44
		public ICollection<string> DebugLookups { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002BD4D File Offset: 0x00029F4D
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0002BD55 File Offset: 0x00029F55
		public ScriptVersion ParsedVersion { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002BD5E File Offset: 0x00029F5E
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0002BD79 File Offset: 0x00029F79
		public CodeSettings Settings
		{
			get
			{
				if (this.m_settings == null)
				{
					this.m_settings = new CodeSettings();
				}
				return this.m_settings;
			}
			set
			{
				this.m_settings = (value ?? new CodeSettings());
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002BD8B File Offset: 0x00029F8B
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0002BD93 File Offset: 0x00029F93
		public TextWriter EchoWriter { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002BD9C File Offset: 0x00029F9C
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		public GlobalScope GlobalScope
		{
			get
			{
				if (this.m_globalScope == null)
				{
					this.m_globalScope = new GlobalScope(this.m_settings);
				}
				return this.m_globalScope;
			}
			set
			{
				this.m_globalScope = value;
				if (this.m_globalScope != null)
				{
					foreach (ActivationObject activationObject in this.m_globalScope.ChildScopes)
					{
						activationObject.Existing = true;
					}
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002BE24 File Offset: 0x0002A024
		public IList<long> TimingPoints
		{
			get
			{
				return this.m_timingPoints;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A3A RID: 2618 RVA: 0x0002BE2C File Offset: 0x0002A02C
		// (remove) Token: 0x06000A3B RID: 2619 RVA: 0x0002BE64 File Offset: 0x0002A064
		public event EventHandler<ContextErrorEventArgs> CompilerError;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x0002BE9C File Offset: 0x0002A09C
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public event EventHandler<UndefinedReferenceEventArgs> UndefinedReference;

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002BF09 File Offset: 0x0002A109
		public JSParser()
		{
			this.m_importantComments = new List<Context>();
			this.m_labelInfo = new Dictionary<string, LabelInfo>();
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002BF27 File Offset: 0x0002A127
		[Obsolete("This Constructor will be removed in version 6. Please use the default constructor.", false)]
		public JSParser(string source) : this()
		{
			this.SetDocumentContext(new DocumentContext(source));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002BF3B File Offset: 0x0002A13B
		public Block Parse(DocumentContext sourceContext)
		{
			this.SetDocumentContext(sourceContext);
			if (this.m_settings == null)
			{
				this.m_settings = new CodeSettings();
			}
			this.m_importantComments.Clear();
			this.m_labelInfo.Clear();
			return this.InternalParse();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002BF73 File Offset: 0x0002A173
		public Block Parse(DocumentContext sourceContext, CodeSettings settings)
		{
			this.Settings = settings;
			return this.Parse(sourceContext);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002BF83 File Offset: 0x0002A183
		public Block Parse(string source)
		{
			return this.Parse(new DocumentContext(source));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002BF91 File Offset: 0x0002A191
		public Block Parse(string source, CodeSettings settings)
		{
			this.Settings = settings;
			return this.Parse(source);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002BFA1 File Offset: 0x0002A1A1
		[Obsolete("This method will be removed in version 6. Please use the default constructor and use a Parse override that is passed the source.", false)]
		public Block Parse(CodeSettings settings)
		{
			if (this.m_scanner == null)
			{
				throw new InvalidOperationException(JScript.NoSource);
			}
			settings = (this.m_settings = (settings ?? new CodeSettings()));
			return this.InternalParse();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002BFD0 File Offset: 0x0002A1D0
		private Block InternalParse()
		{
			this.DebugLookups = new HashSet<string>(this.m_settings.DebugLookupCollection);
			this.m_scanner.DebugLookupCollection = this.DebugLookups;
			this.m_scanner.AllowEmbeddedAspNetBlocks = this.m_settings.AllowEmbeddedAspNetBlocks;
			this.m_scanner.IgnoreConditionalCompilation = this.m_settings.IgnoreConditionalCompilation;
			this.m_scanner.UsePreprocessorDefines = !this.m_settings.IgnorePreprocessorDefines;
			if (this.m_scanner.UsePreprocessorDefines)
			{
				this.m_scanner.SetPreprocessorDefines(this.m_settings.PreprocessorValues);
			}
			this.m_scanner.StripDebugCommentBlocks = this.m_settings.StripDebugStatements;
			this.ParsedVersion = ScriptVersion.EcmaScript5;
			this.GlobalScope.UseStrict = this.m_settings.StrictMode;
			this.GlobalScope.SetAssumedGlobals(this.m_settings);
			this.m_newModule = true;
			long[] array = this.m_timingPoints = new long[9];
			int num = array.Length;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this.GetNextToken();
			Block block = null;
			Block block2 = null;
			switch (this.m_settings.SourceMode)
			{
			case JavaScriptSourceMode.Program:
				block2 = (block = this.ParseStatements(new Block(this.CurrentPositionContext)
				{
					EnclosingScope = this.GlobalScope
				}));
				goto IL_2E4;
			case JavaScriptSourceMode.Expression:
				block = (block2 = new Block(this.CurrentPositionContext)
				{
					EnclosingScope = this.GlobalScope
				});
				try
				{
					AstNode astNode = this.ParseExpression(false, JSToken.None);
					if (astNode != null)
					{
						block.Append(astNode);
						block.UpdateWith(astNode.Context);
					}
					goto IL_2E4;
				}
				catch (EndOfStreamException)
				{
					goto IL_2E4;
				}
				break;
			case JavaScriptSourceMode.EventHandler:
				break;
			case JavaScriptSourceMode.Module:
			{
				block = (block2 = new Block(this.CurrentPositionContext)
				{
					EnclosingScope = this.GlobalScope
				});
				ModuleDeclaration moduleDeclaration = new ModuleDeclaration(this.CurrentPositionContext)
				{
					IsImplicit = true,
					Body = new Block(this.CurrentPositionContext)
					{
						IsModule = true
					}
				};
				block.Append(moduleDeclaration);
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				this.ParseStatements(moduleDeclaration.Body);
				goto IL_2E4;
			}
			default:
				return null;
			}
			block = new Block(this.CurrentPositionContext)
			{
				EnclosingScope = this.GlobalScope
			};
			AstNodeList astNodeList = new AstNodeList(this.CurrentPositionContext);
			astNodeList.Append(new ParameterDeclaration(this.CurrentPositionContext)
			{
				Binding = new BindingIdentifier(this.CurrentPositionContext)
				{
					Name = "event",
					RenameNotAllowed = true
				}
			});
			FunctionObject functionObject = new FunctionObject(this.CurrentPositionContext)
			{
				FunctionType = FunctionType.Expression,
				ParameterDeclarations = astNodeList,
				Body = new Block(this.CurrentPositionContext)
			};
			block.Append(functionObject);
			this.ParseFunctionBody(functionObject.Body);
			block2 = functionObject.Body;
			IL_2E4:
			array[--num] = stopwatch.ElapsedTicks;
			ResolutionVisitor.Apply(block, this.GlobalScope, this);
			array[--num] = stopwatch.ElapsedTicks;
			if (block != null && this.Settings.MinifyCode && !this.Settings.PreprocessOnly)
			{
				ReorderScopeVisitor.Apply(block, this);
				array[--num] = stopwatch.ElapsedTicks;
				AnalyzeNodeVisitor visitor = new AnalyzeNodeVisitor(this);
				block.Accept(visitor);
				array[--num] = stopwatch.ElapsedTicks;
				this.GlobalScope.AnalyzeScope();
				array[--num] = stopwatch.ElapsedTicks;
				if (this.m_settings.LocalRenaming != LocalRenaming.KeepAll && this.m_settings.IsModificationAllowed(TreeModifications.LocalRenaming))
				{
					this.GlobalScope.AutoRenameFields();
				}
				array[--num] = stopwatch.ElapsedTicks;
				if (this.m_settings.EvalLiteralExpressions)
				{
					EvaluateLiteralVisitor visitor2 = new EvaluateLiteralVisitor(this);
					block.Accept(visitor2);
				}
				array[--num] = stopwatch.ElapsedTicks;
				FinalPassVisitor.Apply(block, this.m_settings);
				array[--num] = stopwatch.ElapsedTicks;
				this.GlobalScope.ValidateGeneratedNames();
				array[--num] = stopwatch.ElapsedTicks;
				stopwatch.Stop();
			}
			foreach (ActivationObject activationObject in this.GlobalScope.ChildScopes)
			{
				activationObject.Existing = true;
			}
			if (block2 != block)
			{
				block2.EnclosingScope = block2.Parent.EnclosingScope;
				block2.Parent = null;
			}
			return block2;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002C46C File Offset: 0x0002A66C
		internal void OnUndefinedReference(UndefinedReference ex)
		{
			if (this.UndefinedReference != null)
			{
				this.UndefinedReference(this, new UndefinedReferenceEventArgs(ex));
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002C488 File Offset: 0x0002A688
		internal void OnCompilerError(ContextError se)
		{
			if (this.CompilerError != null && !this.m_settings.IgnoreAllErrors && this.m_settings != null && !this.m_settings.IgnoreErrorCollection.Contains(se.ErrorCode))
			{
				this.CompilerError(this, new ContextErrorEventArgs
				{
					Error = se
				});
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		private Block ParseStatements(Block block)
		{
			Block block2 = block;
			try
			{
				bool flag = true;
				int endPosition = this.m_currentToken.EndPosition;
				while (this.m_currentToken.IsNot(JSToken.EndOfFile))
				{
					AstNode astNode = this.ParseStatement(true, false);
					if (flag)
					{
						ConstantWrapper constantWrapper = astNode as ConstantWrapper;
						if (constantWrapper != null && constantWrapper.PrimitiveType == PrimitiveType.String)
						{
							if (!(constantWrapper is DirectivePrologue))
							{
								astNode = new DirectivePrologue(constantWrapper.Value.ToString(), astNode.Context)
								{
									MayHaveIssues = constantWrapper.MayHaveIssues
								};
							}
						}
						else if (!this.m_newModule)
						{
							flag = false;
						}
					}
					else if (this.m_newModule)
					{
						flag = true;
					}
					if (astNode != null)
					{
						block.Append(astNode);
						if (astNode is ExportNode && !block.IsModule)
						{
							block.IsModule = true;
							if (block.Parent == null)
							{
								block2 = new Block(block.Context.Clone())
								{
									EnclosingScope = block.EnclosingScope
								};
								block.EnclosingScope = null;
								block2.Append(new ModuleDeclaration(new Context(this.m_currentToken.Document))
								{
									IsImplicit = true,
									Body = block
								});
							}
						}
						endPosition = this.m_currentToken.EndPosition;
					}
					else if (!this.m_scanner.IsEndOfFile && this.m_currentToken.StartLinePosition == endPosition)
					{
						this.m_currentToken.HandleError(JSError.ApplicationError, true);
						break;
					}
				}
				this.AppendImportantComments(block);
			}
			catch (EndOfStreamException)
			{
			}
			block.UpdateWith(this.CurrentPositionContext);
			return block2;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002C67C File Offset: 0x0002A87C
		private AstNode ParseStatement(bool fSourceElement, bool skipImportantComment = false)
		{
			AstNode result = null;
			if (skipImportantComment)
			{
				this.m_importantComments.Clear();
			}
			if (this.m_importantComments.Count > 0 && this.m_settings.PreserveImportantComments && this.m_settings.IsModificationAllowed(TreeModifications.PreserveImportantComments))
			{
				result = new ImportantComment(this.m_importantComments[0]);
				this.m_importantComments.RemoveAt(0);
			}
			else
			{
				JSToken token = this.m_currentToken.Token;
				switch (token)
				{
				case JSToken.EndOfFile:
					this.ReportError(JSError.ErrorEndOfFile, null, false);
					return null;
				case JSToken.Semicolon:
					result = new EmptyStatement(this.m_currentToken.Clone());
					this.GetNextToken();
					return result;
				case JSToken.RightCurly:
					this.ReportError(JSError.SyntaxError, null, false);
					this.GetNextToken();
					return result;
				case JSToken.LeftCurly:
					return this.ParseBlock();
				case JSToken.Debugger:
					return this.ParseDebuggerStatement();
				case JSToken.Var:
					break;
				case JSToken.If:
					return this.ParseIfStatement();
				case JSToken.For:
					return this.ParseForStatement();
				case JSToken.Do:
					return this.ParseDoStatement();
				case JSToken.While:
					return this.ParseWhileStatement();
				case JSToken.Continue:
					return this.ParseContinueStatement();
				case JSToken.Break:
					return this.ParseBreakStatement();
				case JSToken.Return:
					return this.ParseReturnStatement();
				case JSToken.With:
					return this.ParseWithStatement();
				case JSToken.Switch:
					return this.ParseSwitchStatement();
				case JSToken.Throw:
					return this.ParseThrowStatement();
				case JSToken.Try:
					return this.ParseTryStatement();
				case JSToken.Function:
				{
					FunctionObject functionObject = this.ParseFunction(FunctionType.Declaration, this.m_currentToken.Clone());
					functionObject.IsSourceElement = fSourceElement;
					return functionObject;
				}
				case JSToken.Else:
					this.ReportError(JSError.InvalidElse, null, false);
					this.GetNextToken();
					return result;
				case JSToken.ConditionalCommentStart:
					return this.ParseStatementLevelConditionalComment(fSourceElement);
				case JSToken.ConditionalCommentEnd:
				case JSToken.ConditionalCompilationVariable:
					goto IL_285;
				case JSToken.ConditionalCompilationOn:
				{
					ConditionalCompilationOn result2 = new ConditionalCompilationOn(this.m_currentToken.Clone());
					this.GetNextToken();
					return result2;
				}
				case JSToken.ConditionalCompilationSet:
					return this.ParseConditionalCompilationSet();
				case JSToken.ConditionalCompilationIf:
					return this.ParseConditionalCompilationIf(false);
				case JSToken.ConditionalCompilationElseIf:
					return this.ParseConditionalCompilationIf(true);
				case JSToken.ConditionalCompilationElse:
				{
					ConditionalCompilationElse result3 = new ConditionalCompilationElse(this.m_currentToken.Clone());
					this.GetNextToken();
					return result3;
				}
				case JSToken.ConditionalCompilationEnd:
				{
					ConditionalCompilationEnd result4 = new ConditionalCompilationEnd(this.m_currentToken.Clone());
					this.GetNextToken();
					return result4;
				}
				case JSToken.Identifier:
					if (this.m_currentToken.Is("module"))
					{
						goto IL_276;
					}
					goto IL_285;
				default:
					switch (token)
					{
					case JSToken.Class:
						return this.ParseClassNode(ClassType.Declaration);
					case JSToken.Const:
					case JSToken.Let:
						break;
					case JSToken.Export:
						return this.ParseExport();
					case JSToken.Import:
						return this.ParseImport();
					case JSToken.Module:
						goto IL_276;
					default:
						goto IL_285;
					}
					break;
				}
				return this.ParseVariableStatement();
				IL_276:
				if (this.PeekCanBeModule())
				{
					return this.ParseModule();
				}
				IL_285:
				result = this.ParseExpressionStatement(fSourceElement);
			}
			return result;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002C94C File Offset: 0x0002AB4C
		private AstNode ParseExpressionStatement(bool fSourceElement)
		{
			bool newModule = this.m_newModule;
			bool bCanAssign;
			AstNode astNode = this.ParseUnaryExpression(out bCanAssign, false);
			if (astNode != null)
			{
				Lookup lookup = astNode as Lookup;
				if (lookup != null && this.m_currentToken.Is(JSToken.Colon))
				{
					astNode = this.ParseLabeledStatement(lookup, fSourceElement);
				}
				else
				{
					astNode = this.ParseExpression(astNode, false, bCanAssign, JSToken.None);
					if (newModule && astNode.IsExpression)
					{
						ConstantWrapper constantWrapper = astNode as ConstantWrapper;
						if (constantWrapper != null && constantWrapper.PrimitiveType == PrimitiveType.String && !(astNode is DirectivePrologue))
						{
							astNode = new DirectivePrologue(constantWrapper.Value.ToString(), constantWrapper.Context)
							{
								MayHaveIssues = constantWrapper.MayHaveIssues
							};
						}
					}
					BinaryOperator binaryOperator = astNode as BinaryOperator;
					if (binaryOperator != null && (binaryOperator.OperatorToken == JSToken.Equal || binaryOperator.OperatorToken == JSToken.StrictEqual))
					{
						binaryOperator.OperatorContext.IfNotNull(delegate(Context c)
						{
							c.HandleError(JSError.SuspectEquality, false);
						});
					}
					lookup = (astNode as Lookup);
					if (lookup != null && lookup.Name.StartsWith("<%=", StringComparison.Ordinal) && lookup.Name.EndsWith("%>", StringComparison.Ordinal))
					{
						astNode = new AspNetBlockNode(astNode.Context)
						{
							AspNetBlockText = lookup.Name
						};
					}
					AspNetBlockNode aspNetBlockNode = astNode as AspNetBlockNode;
					if (aspNetBlockNode != null && this.m_currentToken.Is(JSToken.Semicolon))
					{
						aspNetBlockNode.IsTerminatedByExplicitSemicolon = true;
						astNode.IfNotNull((AstNode s) => s.TerminatingContext = this.m_currentToken.Clone());
						this.GetNextToken();
					}
					this.ExpectSemicolon(astNode);
				}
			}
			else
			{
				this.GetNextToken();
			}
			return astNode;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
		private LabeledStatement ParseLabeledStatement(Lookup lookup, bool fSourceElement)
		{
			string name = lookup.Name;
			Context colonContext = this.m_currentToken.Clone();
			bool flag = true;
			LabelInfo labelInfo;
			if (this.m_labelInfo.TryGetValue(name, out labelInfo))
			{
				labelInfo.HasIssues = true;
				flag = false;
				lookup.Context.HandleError(JSError.BadLabel, true);
			}
			else
			{
				labelInfo = new LabelInfo
				{
					NestLevel = this.m_labelInfo.Count,
					RefCount = 0
				};
				this.m_labelInfo.Add(name, labelInfo);
			}
			this.GetNextToken();
			LabeledStatement result;
			if (this.m_currentToken.IsNot(JSToken.EndOfFile))
			{
				result = new LabeledStatement(lookup.Context.Clone())
				{
					Label = name,
					LabelContext = lookup.Context,
					LabelInfo = labelInfo,
					ColonContext = colonContext,
					Statement = this.ParseStatement(fSourceElement, true)
				};
			}
			else
			{
				result = new LabeledStatement(lookup.Context.Clone())
				{
					Label = name,
					LabelContext = lookup.Context,
					LabelInfo = labelInfo,
					ColonContext = colonContext
				};
			}
			if (flag)
			{
				this.m_labelInfo.Remove(name);
			}
			return result;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002CC10 File Offset: 0x0002AE10
		private AstNode ParseStatementLevelConditionalComment(bool fSourceElement)
		{
			Context context = this.m_currentToken.Clone();
			ConditionalCompilationComment conditionalCompilationComment = new ConditionalCompilationComment(context);
			this.GetNextToken();
			while (this.m_currentToken.IsNot(JSToken.ConditionalCommentEnd) && this.m_currentToken.IsNot(JSToken.EndOfFile))
			{
				if (this.m_currentToken.Is(JSToken.ConditionalCommentStart))
				{
					this.GetNextToken();
				}
				else
				{
					conditionalCompilationComment.Append(this.ParseStatement(fSourceElement, false));
				}
			}
			this.GetNextToken();
			if (conditionalCompilationComment.Statements.Count <= 0)
			{
				return null;
			}
			return conditionalCompilationComment;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002CC94 File Offset: 0x0002AE94
		private ConditionalCompilationSet ParseConditionalCompilationSet()
		{
			Context context = this.m_currentToken.Clone();
			string variableName = null;
			AstNode astNode = null;
			this.GetNextToken();
			if (this.m_currentToken.Is(JSToken.ConditionalCompilationVariable))
			{
				context.UpdateWith(this.m_currentToken);
				variableName = this.m_currentToken.Code;
				this.GetNextToken();
				if (this.m_currentToken.Is(JSToken.Assign))
				{
					context.UpdateWith(this.m_currentToken);
					this.GetNextToken();
					astNode = this.ParseExpression(false, JSToken.None);
					if (astNode != null)
					{
						context.UpdateWith(astNode.Context);
					}
					else
					{
						this.m_currentToken.HandleError(JSError.ExpressionExpected, false);
					}
				}
				else
				{
					this.m_currentToken.HandleError(JSError.NoEqual, false);
				}
			}
			else
			{
				this.m_currentToken.HandleError(JSError.NoIdentifier, false);
			}
			return new ConditionalCompilationSet(context)
			{
				VariableName = variableName,
				Value = astNode
			};
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002CD74 File Offset: 0x0002AF74
		private ConditionalCompilationStatement ParseConditionalCompilationIf(bool isElseIf)
		{
			Context context = this.m_currentToken.Clone();
			AstNode astNode = null;
			this.GetNextToken();
			if (this.m_currentToken.Is(JSToken.LeftParenthesis))
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
				astNode = this.ParseExpression(false, JSToken.None);
				if (astNode != null)
				{
					context.UpdateWith(astNode.Context);
				}
				else
				{
					this.m_currentToken.HandleError(JSError.ExpressionExpected, false);
				}
				if (this.m_currentToken.Is(JSToken.RightParenthesis))
				{
					context.UpdateWith(this.m_currentToken);
					this.GetNextToken();
				}
				else
				{
					this.m_currentToken.HandleError(JSError.NoRightParenthesis, false);
				}
			}
			else
			{
				this.m_currentToken.HandleError(JSError.NoLeftParenthesis, false);
			}
			if (isElseIf)
			{
				return new ConditionalCompilationElseIf(context)
				{
					Condition = astNode
				};
			}
			return new ConditionalCompilationIf(context)
			{
				Condition = astNode
			};
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002CE50 File Offset: 0x0002B050
		private Block ParseBlock()
		{
			Block block = new Block(this.m_currentToken.Clone())
			{
				ForceBraces = true
			};
			block.BraceOnNewLine = this.m_foundEndOfLine;
			this.GetNextToken();
			while (this.m_currentToken.IsNot(JSToken.RightCurly) && this.m_currentToken.IsNot(JSToken.EndOfFile))
			{
				block.Append(this.ParseStatement(false, false));
			}
			this.AppendImportantComments(block);
			if (this.m_currentToken.IsNot(JSToken.RightCurly))
			{
				this.ReportError(JSError.NoRightCurly, null, false);
				if (this.m_currentToken.Is(JSToken.EndOfFile))
				{
					this.ReportError(JSError.ErrorEndOfFile, null, false);
				}
			}
			block.TerminatingContext = this.m_currentToken.Clone();
			block.Context.UpdateWith(this.m_currentToken);
			this.GetNextToken();
			return block;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002CF1C File Offset: 0x0002B11C
		private AstNode ParseDebuggerStatement()
		{
			DebuggerNode debuggerNode = new DebuggerNode(this.m_currentToken.Clone());
			this.GetNextToken();
			this.ExpectSemicolon(debuggerNode);
			return debuggerNode;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002CF48 File Offset: 0x0002B148
		private AstNode ParseVariableStatement()
		{
			Declaration declaration;
			if (this.m_currentToken.Is(JSToken.Var))
			{
				declaration = new Var(this.m_currentToken.Clone())
				{
					StatementToken = this.m_currentToken.Token,
					KeywordContext = this.m_currentToken.Clone()
				};
			}
			else
			{
				if (!this.m_currentToken.IsOne(new JSToken[]
				{
					JSToken.Const,
					JSToken.Let
				}))
				{
					return null;
				}
				if (this.m_currentToken.Is(JSToken.Const) && this.m_settings.ConstStatementsMozilla)
				{
					declaration = new ConstStatement(this.m_currentToken.Clone())
					{
						StatementToken = this.m_currentToken.Token,
						KeywordContext = this.m_currentToken.Clone()
					};
				}
				else
				{
					this.ParsedVersion = ScriptVersion.EcmaScript6;
					declaration = new LexicalDeclaration(this.m_currentToken.Clone())
					{
						StatementToken = this.m_currentToken.Token,
						KeywordContext = this.m_currentToken.Clone()
					};
				}
			}
			do
			{
				this.GetNextToken();
				VariableDeclaration variableDeclaration = this.ParseVarDecl(JSToken.None);
				if (variableDeclaration != null)
				{
					declaration.Append(variableDeclaration);
					declaration.Context.UpdateWith(variableDeclaration.Context);
				}
			}
			while (this.m_currentToken.Is(JSToken.Comma));
			this.ExpectSemicolon(declaration);
			return declaration;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002D09C File Offset: 0x0002B29C
		private VariableDeclaration ParseVarDecl(JSToken inToken)
		{
			Context context = this.m_currentToken.Clone();
			VariableDeclaration result = null;
			AstNode astNode = this.ParseBinding();
			if (astNode != null)
			{
				Context assignContext = null;
				AstNode astNode2 = null;
				bool flag = false;
				bool useCCOn = false;
				if (this.m_currentToken.Is(JSToken.ConditionalCommentStart))
				{
					flag = true;
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.ConditionalCompilationOn))
					{
						this.GetNextToken();
						if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
						{
							flag = false;
						}
						else
						{
							useCCOn = true;
						}
					}
				}
				if (this.m_currentToken.IsOne(new JSToken[]
				{
					JSToken.Assign,
					JSToken.Equal
				}))
				{
					assignContext = this.m_currentToken.Clone();
					if (this.m_currentToken.Is(JSToken.Equal))
					{
						this.ReportError(JSError.NoEqual, null, false);
					}
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
					{
						flag = false;
						this.m_currentToken.HandleError(JSError.ConditionalCompilationTooComplex, false);
						this.GetNextToken();
					}
					astNode2 = this.ParseExpression(true, inToken);
					if (astNode2 != null)
					{
						context.UpdateWith(astNode2.Context);
					}
				}
				else if (flag)
				{
					flag = false;
					this.m_currentToken.HandleError(JSError.ConditionalCompilationTooComplex, false);
					while (this.m_currentToken.IsNot(JSToken.EndOfFile) && this.m_currentToken.IsNot(JSToken.ConditionalCommentEnd))
					{
						this.GetNextToken();
					}
					this.GetNextToken();
				}
				if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
				{
					this.GetNextToken();
				}
				else if (flag)
				{
					flag = false;
					this.m_currentToken.HandleError(JSError.ConditionalCompilationTooComplex, false);
					astNode2 = null;
					while (this.m_currentToken.IsNot(JSToken.EndOfFile) && this.m_currentToken.IsNot(JSToken.ConditionalCommentEnd))
					{
						this.GetNextToken();
					}
					this.GetNextToken();
				}
				result = new VariableDeclaration(context)
				{
					Binding = astNode,
					AssignContext = assignContext,
					Initializer = astNode2,
					IsCCSpecialCase = flag,
					UseCCOn = useCCOn
				};
			}
			return result;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002D284 File Offset: 0x0002B484
		private AstNode ParseBinding()
		{
			AstNode result;
			if (this.m_currentToken.Is(JSToken.Identifier))
			{
				result = new BindingIdentifier(this.m_currentToken.Clone())
				{
					Name = this.m_scanner.Identifier
				};
				this.GetNextToken();
			}
			else if (this.m_currentToken.Is(JSToken.LeftBracket))
			{
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				result = this.ParseArrayLiteral(true);
			}
			else if (this.m_currentToken.Is(JSToken.LeftCurly))
			{
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				result = this.ParseObjectLiteral(true);
			}
			else
			{
				string text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token);
				if (text != null)
				{
					result = new BindingIdentifier(this.m_currentToken.Clone())
					{
						Name = text
					};
					this.GetNextToken();
				}
				else
				{
					if (!JSScanner.IsValidIdentifier(text = this.m_currentToken.Code))
					{
						this.ReportError(JSError.NoIdentifier, null, false);
						return null;
					}
					this.ReportError(JSError.NoIdentifier, null, false);
					result = new BindingIdentifier(this.m_currentToken.Clone())
					{
						Name = text
					};
					this.GetNextToken();
				}
			}
			return result;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002D3C0 File Offset: 0x0002B5C0
		private IfNode ParseIfStatement()
		{
			Context ifCtx = this.m_currentToken.Clone();
			AstNode astNode = null;
			Context elseContext = null;
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			AstNode astNode2 = this.ParseExpression(false, JSToken.None);
			if (this.m_currentToken.Is(JSToken.RightParenthesis))
			{
				ifCtx.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			else
			{
				astNode2.IfNotNull((AstNode c) => ifCtx.UpdateWith(c.Context));
				this.ReportError(JSError.NoRightParenthesis, null, false);
			}
			BinaryOperator binaryOperator = astNode2 as BinaryOperator;
			if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
			{
				astNode2.Context.HandleError(JSError.SuspectAssignment, false);
			}
			if (this.m_currentToken.Is(JSToken.Semicolon))
			{
				this.m_currentToken.HandleError(JSError.SuspectSemicolon, false);
			}
			else if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
			}
			AstNode astNode3 = this.ParseStatement(false, true);
			if (astNode3 != null)
			{
				ifCtx.UpdateWith(astNode3.Context);
			}
			if (this.m_currentToken.Is(JSToken.Else))
			{
				elseContext = this.m_currentToken.Clone();
				this.GetNextToken();
				if (this.m_currentToken.Is(JSToken.Semicolon))
				{
					this.m_currentToken.HandleError(JSError.SuspectSemicolon, false);
				}
				else if (this.m_currentToken.IsNot(JSToken.LeftCurly) && this.m_currentToken.IsNot(JSToken.If))
				{
					this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
				}
				astNode = this.ParseStatement(false, true);
				if (astNode != null)
				{
					ifCtx.UpdateWith(astNode.Context);
				}
			}
			return new IfNode(ifCtx)
			{
				Condition = astNode2,
				TrueBlock = AstNode.ForceToBlock(astNode3),
				ElseContext = elseContext,
				FalseBlock = AstNode.ForceToBlock(astNode)
			};
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002D5CC File Offset: 0x0002B7CC
		private AstNode ParseForStatement()
		{
			Context context = this.m_currentToken.Clone();
			this.GetNextToken();
			if (this.m_currentToken.Is(JSToken.LeftParenthesis))
			{
				this.GetNextToken();
			}
			else
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			AstNode astNode = null;
			AstNode astNode2 = null;
			AstNode incrementer = null;
			Context operatorContext = null;
			Context separator1Context = null;
			Context separator2Context = null;
			if (this.m_currentToken.IsOne(new JSToken[]
			{
				JSToken.Var,
				JSToken.Let,
				JSToken.Const
			}))
			{
				Declaration declaration;
				if (this.m_currentToken.Is(JSToken.Var))
				{
					declaration = new Var(this.m_currentToken.Clone())
					{
						StatementToken = this.m_currentToken.Token,
						KeywordContext = this.m_currentToken.Clone()
					};
				}
				else
				{
					this.ParsedVersion = ScriptVersion.EcmaScript6;
					declaration = new LexicalDeclaration(this.m_currentToken.Clone())
					{
						StatementToken = this.m_currentToken.Token,
						KeywordContext = this.m_currentToken.Clone()
					};
				}
				this.GetNextToken();
				declaration.Append(this.ParseVarDecl(JSToken.In));
				while (this.m_currentToken.Is(JSToken.Comma))
				{
					this.GetNextToken();
					declaration.Append(this.ParseVarDecl(JSToken.In));
				}
				astNode = declaration;
			}
			else if (this.m_currentToken.IsNot(JSToken.Semicolon))
			{
				astNode = this.ParseExpression(false, JSToken.In);
			}
			bool flag = this.m_currentToken.Is(JSToken.In) || this.m_currentToken.Is("of");
			if (flag)
			{
				if (this.m_currentToken.IsNot(JSToken.In))
				{
					this.ParsedVersion = ScriptVersion.EcmaScript6;
				}
				operatorContext = this.m_currentToken.Clone();
				this.GetNextToken();
				astNode2 = this.ParseExpression(false, JSToken.None);
			}
			else
			{
				if (this.m_currentToken.Is(JSToken.Semicolon))
				{
					separator1Context = this.m_currentToken.Clone();
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoSemicolon, null, false);
				}
				if (this.m_currentToken.IsNot(JSToken.Semicolon))
				{
					astNode2 = this.ParseExpression(false, JSToken.None);
				}
				if (this.m_currentToken.Is(JSToken.Semicolon))
				{
					separator2Context = this.m_currentToken.Clone();
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoSemicolon, null, false);
				}
				if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
				{
					incrementer = this.ParseExpression(false, JSToken.None);
				}
			}
			if (this.m_currentToken.Is(JSToken.RightParenthesis))
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			else
			{
				this.ReportError(JSError.NoRightParenthesis, null, false);
			}
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
			}
			AstNode node = this.ParseStatement(false, true);
			AstNode result;
			if (flag)
			{
				result = new ForIn(context)
				{
					Variable = astNode,
					OperatorContext = operatorContext,
					Collection = astNode2,
					Body = AstNode.ForceToBlock(node)
				};
			}
			else
			{
				BinaryOperator binaryOperator = astNode2 as BinaryOperator;
				if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
				{
					astNode2.Context.HandleError(JSError.SuspectAssignment, false);
				}
				result = new ForNode(context)
				{
					Initializer = astNode,
					Separator1Context = separator1Context,
					Condition = astNode2,
					Separator2Context = separator2Context,
					Incrementer = incrementer,
					Body = AstNode.ForceToBlock(node)
				};
			}
			return result;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002D920 File Offset: 0x0002BB20
		private DoWhile ParseDoStatement()
		{
			Context context = this.m_currentToken.Clone();
			Context context2 = null;
			Context terminatingContext = null;
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
			}
			AstNode node = this.ParseStatement(false, true);
			if (this.m_currentToken.IsNot(JSToken.While))
			{
				this.ReportError(JSError.NoWhile, null, false);
			}
			else
			{
				context2 = this.m_currentToken.Clone();
				context.UpdateWith(context2);
				this.GetNextToken();
			}
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			AstNode astNode = this.ParseExpression(false, JSToken.None);
			if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
			{
				this.ReportError(JSError.NoRightParenthesis, null, false);
				context.UpdateWith(astNode.Context);
			}
			else
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			if (this.m_currentToken.Is(JSToken.Semicolon))
			{
				terminatingContext = this.m_currentToken.Clone();
				this.GetNextToken();
			}
			BinaryOperator binaryOperator = astNode as BinaryOperator;
			if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
			{
				astNode.Context.HandleError(JSError.SuspectAssignment, false);
			}
			return new DoWhile(context)
			{
				Body = AstNode.ForceToBlock(node),
				WhileContext = context2,
				Condition = astNode,
				TerminatingContext = terminatingContext
			};
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002DA90 File Offset: 0x0002BC90
		private WhileNode ParseWhileStatement()
		{
			Context context = this.m_currentToken.Clone();
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			AstNode astNode = this.ParseExpression(false, JSToken.None);
			if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
			{
				this.ReportError(JSError.NoRightParenthesis, null, false);
				context.UpdateWith(astNode.Context);
			}
			else
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			BinaryOperator binaryOperator = astNode as BinaryOperator;
			if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
			{
				astNode.Context.HandleError(JSError.SuspectAssignment, false);
			}
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
			}
			AstNode node = this.ParseStatement(false, true);
			return new WhileNode(context)
			{
				Condition = astNode,
				Body = AstNode.ForceToBlock(node)
			};
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002DB8C File Offset: 0x0002BD8C
		private ContinueNode ParseContinueStatement()
		{
			ContinueNode continueNode = new ContinueNode(this.m_currentToken.Clone());
			this.GetNextToken();
			string text = null;
			if (!this.m_foundEndOfLine && (this.m_currentToken.Is(JSToken.Identifier) || (text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token)) != null))
			{
				continueNode.UpdateWith(this.m_currentToken);
				continueNode.LabelContext = this.m_currentToken.Clone();
				continueNode.Label = (text ?? this.m_scanner.Identifier);
				LabelInfo labelInfo;
				if (this.m_labelInfo.TryGetValue(continueNode.Label, out labelInfo))
				{
					labelInfo.RefCount++;
					continueNode.LabelInfo = labelInfo;
				}
				else
				{
					continueNode.LabelContext.HandleError(JSError.NoLabel, true);
				}
				this.GetNextToken();
			}
			this.ExpectSemicolon(continueNode);
			return continueNode;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002DC60 File Offset: 0x0002BE60
		private Break ParseBreakStatement()
		{
			Break @break = new Break(this.m_currentToken.Clone());
			this.GetNextToken();
			string text = null;
			if (!this.m_foundEndOfLine && (this.m_currentToken.Is(JSToken.Identifier) || (text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token)) != null))
			{
				@break.UpdateWith(this.m_currentToken);
				@break.LabelContext = this.m_currentToken.Clone();
				@break.Label = (text ?? this.m_scanner.Identifier);
				LabelInfo labelInfo;
				if (this.m_labelInfo.TryGetValue(@break.Label, out labelInfo))
				{
					labelInfo.RefCount++;
					@break.LabelInfo = labelInfo;
				}
				else
				{
					@break.LabelContext.HandleError(JSError.NoLabel, true);
				}
				this.GetNextToken();
			}
			this.ExpectSemicolon(@break);
			return @break;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002DD34 File Offset: 0x0002BF34
		private ReturnNode ParseReturnStatement()
		{
			ReturnNode returnNode = new ReturnNode(this.m_currentToken.Clone());
			this.GetNextToken();
			if (!this.m_foundEndOfLine)
			{
				if (this.m_currentToken.IsNot(JSToken.Semicolon) && this.m_currentToken.IsNot(JSToken.RightCurly))
				{
					returnNode.Operand = this.ParseExpression(false, JSToken.None);
					if (returnNode.Operand != null)
					{
						returnNode.UpdateWith(returnNode.Operand.Context);
					}
				}
				this.ExpectSemicolon(returnNode);
			}
			else
			{
				this.ReportError(JSError.SemicolonInsertion, returnNode.Context.FlattenToEnd(), false);
			}
			return returnNode;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002DDC4 File Offset: 0x0002BFC4
		private WithNode ParseWithStatement()
		{
			Context context = this.m_currentToken.Clone();
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			AstNode astNode = this.ParseExpression(false, JSToken.None);
			if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
			{
				context.UpdateWith(astNode.Context);
				this.ReportError(JSError.NoRightParenthesis, null, false);
			}
			else
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.StatementBlockExpected, this.CurrentPositionContext, false);
			}
			AstNode node = this.ParseStatement(false, true);
			return new WithNode(context)
			{
				WithObject = astNode,
				Body = AstNode.ForceToBlock(node)
			};
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002DE94 File Offset: 0x0002C094
		private AstNode ParseSwitchStatement()
		{
			Context context = this.m_currentToken.Clone();
			bool braceOnNewLine = false;
			Context braceContext = null;
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			AstNode expression = this.ParseExpression(false, JSToken.None);
			if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
			{
				this.ReportError(JSError.NoRightParenthesis, null, false);
			}
			else
			{
				this.GetNextToken();
			}
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.NoLeftCurly, null, false);
			}
			else
			{
				braceOnNewLine = this.m_foundEndOfLine;
				braceContext = this.m_currentToken.Clone();
				this.GetNextToken();
			}
			AstNodeList astNodeList = new AstNodeList(this.CurrentPositionContext);
			bool flag = false;
			while (this.m_currentToken.IsNot(JSToken.RightCurly))
			{
				AstNode caseValue = null;
				Context context2 = this.m_currentToken.Clone();
				Context colonContext = null;
				if (this.m_currentToken.Is(JSToken.Case))
				{
					this.GetNextToken();
					caseValue = this.ParseExpression(false, JSToken.None);
				}
				else if (this.m_currentToken.Is(JSToken.Default))
				{
					if (flag)
					{
						this.ReportError(JSError.DupDefault, null, false);
					}
					else
					{
						flag = true;
					}
					this.GetNextToken();
				}
				else
				{
					flag = true;
					this.ReportError(JSError.BadSwitch, null, false);
				}
				if (this.m_currentToken.IsNot(JSToken.Colon))
				{
					this.ReportError(JSError.NoColon, null, false);
				}
				else
				{
					colonContext = this.m_currentToken.Clone();
					this.GetNextToken();
				}
				Block block = new Block(this.m_currentToken.Clone());
				for (;;)
				{
					Context currentToken = this.m_currentToken;
					JSToken[] array = new JSToken[4];
					array[0] = JSToken.RightCurly;
					array[1] = JSToken.Case;
					array[2] = JSToken.Default;
					if (!currentToken.IsNotAny(array))
					{
						break;
					}
					block.Append(this.ParseStatement(false, false));
				}
				context2.UpdateWith(block.Context);
				SwitchCase node = new SwitchCase(context2)
				{
					CaseValue = caseValue,
					ColonContext = colonContext,
					Statements = block
				};
				astNodeList.Append(node);
			}
			context.UpdateWith(this.m_currentToken);
			this.GetNextToken();
			return new Switch(context)
			{
				Expression = expression,
				BraceContext = braceContext,
				Cases = astNodeList,
				BraceOnNewLine = braceOnNewLine
			};
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		private AstNode ParseThrowStatement()
		{
			ThrowNode throwNode = new ThrowNode(this.m_currentToken.Clone());
			this.GetNextToken();
			if (!this.m_foundEndOfLine)
			{
				if (this.m_currentToken.IsNot(JSToken.Semicolon))
				{
					throwNode.Operand = this.ParseExpression(false, JSToken.None);
					if (throwNode.Operand != null)
					{
						throwNode.UpdateWith(throwNode.Operand.Context);
					}
				}
				this.ExpectSemicolon(throwNode);
			}
			else
			{
				this.ReportError(JSError.SemicolonInsertion, throwNode.Context.FlattenToEnd(), false);
			}
			return throwNode;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002E15C File Offset: 0x0002C35C
		private AstNode ParseTryStatement()
		{
			Context context = this.m_currentToken.Clone();
			Context catchContext = null;
			ParameterDeclaration catchParameter = null;
			Block block = null;
			Context finallyContext = null;
			Block block2 = null;
			bool flag = false;
			this.GetNextToken();
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.NoLeftCurly, null, false);
			}
			Block tryBlock = this.ParseBlock();
			if (this.m_currentToken.Is(JSToken.Catch))
			{
				flag = true;
				catchContext = this.m_currentToken.Clone();
				this.GetNextToken();
				if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
				{
					this.ReportError(JSError.NoLeftParenthesis, null, false);
				}
				else
				{
					this.GetNextToken();
				}
				AstNode astNode = this.ParseBinding();
				if (astNode == null)
				{
					this.ReportError(JSError.NoBinding, null, false);
				}
				else
				{
					catchParameter = new ParameterDeclaration(astNode.Context.Clone())
					{
						Binding = astNode
					};
				}
				if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
				{
					this.ReportError(JSError.NoRightParenthesis, null, false);
				}
				else
				{
					this.GetNextToken();
				}
				if (this.m_currentToken.IsNot(JSToken.LeftCurly))
				{
					this.ReportError(JSError.NoLeftCurly, null, false);
				}
				block = this.ParseBlock();
				context.UpdateWith(block.Context);
			}
			if (this.m_currentToken.Is(JSToken.Finally))
			{
				flag = true;
				finallyContext = this.m_currentToken.Clone();
				this.GetNextToken();
				if (this.m_currentToken.IsNot(JSToken.LeftCurly))
				{
					this.ReportError(JSError.NoLeftCurly, null, false);
				}
				block2 = this.ParseBlock();
				context.UpdateWith(block2.Context);
			}
			if (!flag)
			{
				this.ReportError(JSError.NoCatch, null, false);
			}
			return new TryNode(context)
			{
				TryBlock = tryBlock,
				CatchContext = catchContext,
				CatchParameter = catchParameter,
				CatchBlock = block,
				FinallyContext = finallyContext,
				FinallyBlock = block2
			};
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002E330 File Offset: 0x0002C530
		private AstNode ParseModule()
		{
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			Context context = this.m_currentToken.Clone();
			this.GetNextToken();
			string moduleName = null;
			Context context2 = null;
			Block block = null;
			BindingIdentifier bindingIdentifier = null;
			Context context3 = null;
			if (this.m_currentToken.Is(JSToken.StringLiteral))
			{
				if (this.m_foundEndOfLine)
				{
					this.ReportError(JSError.NewLineNotAllowed, null, true);
				}
				moduleName = this.m_scanner.StringLiteralValue;
				context2 = this.m_currentToken.Clone();
				context.UpdateWith(context2);
				this.GetNextToken();
				if (this.m_currentToken.IsNot(JSToken.LeftCurly))
				{
					this.ReportError(JSError.NoLeftCurly, null, false);
				}
				else
				{
					block = this.ParseBlock();
					if (block != null)
					{
						context.UpdateWith(block.Context);
						block.IsModule = true;
					}
				}
			}
			else if (this.m_currentToken.Is(JSToken.Identifier) || JSKeyword.CanBeIdentifier(this.m_currentToken.Token) != null)
			{
				bindingIdentifier = (BindingIdentifier)this.ParseBinding();
				context.UpdateWith(bindingIdentifier.Context);
				if (this.m_currentToken.Is("from"))
				{
					context3 = this.m_currentToken.Clone();
					context.UpdateWith(context3);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoExpectedFrom, null, false);
				}
				if (this.m_currentToken.Is(JSToken.StringLiteral))
				{
					moduleName = this.m_scanner.StringLiteralValue;
					context2 = this.m_currentToken.Clone();
					context.UpdateWith(context2);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoStringLiteral, null, false);
				}
			}
			else
			{
				this.ReportError(JSError.NoIdentifier, null, false);
			}
			ModuleDeclaration moduleDeclaration = new ModuleDeclaration(context)
			{
				ModuleName = moduleName,
				ModuleContext = context2,
				Body = block,
				Binding = bindingIdentifier,
				FromContext = context3
			};
			if (bindingIdentifier != null)
			{
				this.ExpectSemicolon(moduleDeclaration);
			}
			return moduleDeclaration;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002E508 File Offset: 0x0002C708
		private AstNode ParseExport()
		{
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			ExportNode exportNode = new ExportNode(this.m_currentToken.Clone())
			{
				KeywordContext = this.m_currentToken.Clone()
			};
			this.GetNextToken();
			if (this.m_currentToken.IsOne(new JSToken[]
			{
				JSToken.Var,
				JSToken.Const,
				JSToken.Let,
				JSToken.Function,
				JSToken.Class
			}))
			{
				AstNode astNode = this.ParseStatement(true, true);
				if (astNode != null)
				{
					exportNode.Append(astNode);
				}
				else
				{
					this.ReportError(JSError.SyntaxError, null, false);
				}
			}
			else if (this.m_currentToken.Is(JSToken.Default))
			{
				exportNode.IsDefault = true;
				exportNode.DefaultContext = this.m_currentToken.Clone();
				exportNode.Context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
				AstNode astNode2 = this.ParseExpression(true, JSToken.None);
				if (astNode2 != null)
				{
					exportNode.Append(astNode2);
				}
				else
				{
					this.ReportError(JSError.ExpressionExpected, null, false);
				}
				this.ExpectSemicolon(exportNode);
			}
			else
			{
				if (this.m_currentToken.Is(JSToken.Identifier) || JSKeyword.CanBeIdentifier(this.m_currentToken.Token) != null)
				{
					Lookup node = new Lookup(this.m_currentToken.Clone())
					{
						Name = this.m_scanner.Identifier
					};
					exportNode.Append(node);
					this.GetNextToken();
				}
				else if (this.m_currentToken.Is(JSToken.Multiply))
				{
					exportNode.OpenContext = this.m_currentToken.Clone();
					exportNode.UpdateWith(exportNode.OpenContext);
					this.GetNextToken();
				}
				else if (this.m_currentToken.Is(JSToken.LeftCurly))
				{
					exportNode.OpenContext = this.m_currentToken.Clone();
					exportNode.UpdateWith(exportNode.OpenContext);
					do
					{
						this.GetNextToken();
						if (this.m_currentToken.IsNot(JSToken.RightCurly))
						{
							string text = null;
							if (this.m_currentToken.Is(JSToken.Identifier) || (text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token)) != null)
							{
								Context context = this.m_currentToken.Clone();
								Lookup localIdentifier = new Lookup(this.m_currentToken.Clone())
								{
									Name = (text ?? this.m_scanner.Identifier)
								};
								this.GetNextToken();
								Context context2 = null;
								Context context3 = null;
								string text2 = null;
								if (this.m_currentToken.Is("as"))
								{
									context2 = this.m_currentToken.Clone();
									context.UpdateWith(context2);
									this.GetNextToken();
									text2 = this.m_scanner.Identifier;
									if (text2 != null)
									{
										context3 = this.m_currentToken.Clone();
										context.UpdateWith(context3);
										this.GetNextToken();
									}
									else
									{
										this.ReportError(JSError.NoIdentifier, null, false);
									}
								}
								ImportExportSpecifier importExportSpecifier = new ImportExportSpecifier(context)
								{
									LocalIdentifier = localIdentifier,
									AsContext = context2,
									ExternalName = text2,
									NameContext = context3
								};
								exportNode.Append(importExportSpecifier);
								if (this.m_currentToken.Is(JSToken.Comma))
								{
									importExportSpecifier.TerminatingContext = this.m_currentToken.Clone();
								}
							}
							else
							{
								this.ReportError(JSError.NoIdentifier, null, false);
							}
						}
					}
					while (this.m_currentToken.Is(JSToken.Comma));
					if (this.m_currentToken.Is(JSToken.RightCurly))
					{
						exportNode.CloseContext = this.m_currentToken.Clone();
						exportNode.UpdateWith(exportNode.CloseContext);
						this.GetNextToken();
					}
					else
					{
						this.ReportError(JSError.NoRightCurly, null, false);
					}
				}
				else
				{
					this.ReportError(JSError.NoSpecifierSet, null, false);
				}
				if (this.m_currentToken.Is("from"))
				{
					exportNode.FromContext = this.m_currentToken.Clone();
					exportNode.UpdateWith(exportNode.FromContext);
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.StringLiteral))
					{
						exportNode.ModuleContext = this.m_currentToken.Clone();
						exportNode.UpdateWith(exportNode.ModuleContext);
						exportNode.ModuleName = this.m_scanner.StringLiteralValue;
						this.GetNextToken();
					}
					else
					{
						this.ReportError(JSError.NoStringLiteral, null, false);
					}
				}
				this.ExpectSemicolon(exportNode);
			}
			return exportNode;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002E928 File Offset: 0x0002CB28
		private AstNode ParseImport()
		{
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			ImportNode importNode = new ImportNode(this.m_currentToken.Clone())
			{
				KeywordContext = this.m_currentToken.Clone()
			};
			this.GetNextToken();
			if (this.m_currentToken.Is(JSToken.StringLiteral))
			{
				importNode.ModuleName = this.m_scanner.StringLiteralValue;
				importNode.ModuleContext = this.m_currentToken.Clone();
				this.GetNextToken();
			}
			else
			{
				if (this.m_currentToken.Is(JSToken.LeftCurly))
				{
					importNode.OpenContext = this.m_currentToken.Clone();
					importNode.UpdateWith(importNode.OpenContext);
					do
					{
						this.GetNextToken();
						if (this.m_currentToken.IsNot(JSToken.RightCurly))
						{
							string text = this.m_scanner.Identifier;
							if (text != null)
							{
								Context context = this.m_currentToken.Clone();
								Context context2 = context.Clone();
								this.GetNextToken();
								Context asContext = null;
								AstNode localIdentifier = null;
								if (this.m_currentToken.Is("as"))
								{
									asContext = this.m_currentToken.Clone();
									this.GetNextToken();
									if (this.m_currentToken.Is(JSToken.Identifier) || JSKeyword.CanBeIdentifier(this.m_currentToken.Token) != null)
									{
										localIdentifier = this.ParseBinding();
									}
									else
									{
										this.ReportError(JSError.NoIdentifier, null, false);
									}
								}
								else
								{
									localIdentifier = new BindingIdentifier(context)
									{
										Name = text
									};
									text = null;
									context = null;
								}
								ImportExportSpecifier node = new ImportExportSpecifier(context2)
								{
									ExternalName = text,
									NameContext = context,
									AsContext = asContext,
									LocalIdentifier = localIdentifier
								};
								importNode.Append(node);
								if (this.m_currentToken.Is(JSToken.Comma))
								{
									importNode.TerminatingContext = this.m_currentToken.Clone();
								}
							}
							else
							{
								this.ReportError(JSError.NoIdentifier, null, false);
							}
						}
					}
					while (this.m_currentToken.Is(JSToken.Comma));
					if (this.m_currentToken.Is(JSToken.RightCurly))
					{
						importNode.CloseContext = this.m_currentToken.Clone();
						importNode.UpdateWith(importNode.CloseContext);
						this.GetNextToken();
					}
					else
					{
						this.ReportError(JSError.NoRightCurly, null, false);
					}
				}
				else if (this.m_currentToken.Is(JSToken.Identifier) || JSKeyword.CanBeIdentifier(this.m_currentToken.Token) != null)
				{
					importNode.Append(this.ParseBinding());
				}
				if (this.m_currentToken.Is("from"))
				{
					importNode.FromContext = this.m_currentToken.Clone();
					importNode.UpdateWith(importNode.FromContext);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoExpectedFrom, null, false);
				}
				if (this.m_currentToken.Is(JSToken.StringLiteral))
				{
					importNode.ModuleName = this.m_scanner.StringLiteralValue;
					importNode.ModuleContext = this.m_currentToken.Clone();
					importNode.UpdateWith(importNode.ModuleContext);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoStringLiteral, null, false);
				}
			}
			this.ExpectSemicolon(importNode);
			return importNode;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002EC20 File Offset: 0x0002CE20
		private FunctionObject ParseFunction(FunctionType functionType, Context fncCtx)
		{
			BindingIdentifier bindingIdentifier = null;
			AstNodeList astNodeList = null;
			Block block = null;
			bool flag = functionType == FunctionType.Expression;
			if (functionType != FunctionType.Method)
			{
				this.GetNextToken();
			}
			bool flag2 = this.m_currentToken.Is(JSToken.Multiply);
			if (flag2)
			{
				this.GetNextToken();
				this.ParsedVersion = ScriptVersion.EcmaScript6;
			}
			if (this.m_currentToken.Is(JSToken.Identifier))
			{
				bindingIdentifier = new BindingIdentifier(this.m_currentToken.Clone())
				{
					Name = this.m_scanner.Identifier
				};
				this.GetNextToken();
			}
			else
			{
				string text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token);
				if (text != null)
				{
					bindingIdentifier = new BindingIdentifier(this.m_currentToken.Clone())
					{
						Name = text
					};
					this.GetNextToken();
				}
				else if (!flag)
				{
					this.ReportError(JSError.NoIdentifier, null, false);
					if (this.m_currentToken.IsNot(JSToken.LeftParenthesis) && this.m_currentToken.IsNot(JSToken.LeftCurly))
					{
						text = this.m_currentToken.Code;
						bindingIdentifier = new BindingIdentifier(this.CurrentPositionContext)
						{
							Name = text
						};
						this.GetNextToken();
					}
				}
			}
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				bool flag3 = false;
				while (this.m_currentToken.IsNot(JSToken.LeftParenthesis) && this.m_currentToken.IsNot(JSToken.LeftCurly) && this.m_currentToken.IsNot(JSToken.Semicolon) && this.m_currentToken.IsNot(JSToken.EndOfFile))
				{
					bindingIdentifier.Context.UpdateWith(this.m_currentToken);
					this.GetNextToken();
					flag3 = true;
				}
				if (flag3)
				{
					bindingIdentifier.Name = bindingIdentifier.Context.Code;
					bindingIdentifier.Context.HandleError(JSError.FunctionNameMustBeIdentifier, false);
				}
				else
				{
					this.ReportError(JSError.NoLeftParenthesis, null, false);
				}
			}
			astNodeList = this.ParseFormalParameters();
			fncCtx.UpdateWith(astNodeList.IfNotNull((AstNodeList p) => p.Context));
			if (this.m_currentToken.IsNot(JSToken.LeftCurly))
			{
				this.ReportError(JSError.NoLeftCurly, null, false);
			}
			try
			{
				block = new Block(this.m_currentToken.Clone());
				block.BraceOnNewLine = this.m_foundEndOfLine;
				this.GetNextToken();
				this.ParseFunctionBody(block);
				if (this.m_currentToken.Is(JSToken.RightCurly))
				{
					block.Context.UpdateWith(this.m_currentToken);
					this.GetNextToken();
				}
				else if (this.m_currentToken.Is(JSToken.EndOfFile))
				{
					fncCtx.HandleError(JSError.UnclosedFunction, true);
					this.ReportError(JSError.ErrorEndOfFile, null, false);
				}
				else
				{
					this.ReportError(JSError.NoRightCurly, null, false);
				}
				fncCtx.UpdateWith(block.Context);
			}
			catch (EndOfStreamException)
			{
				fncCtx.HandleError(JSError.UnclosedFunction, true);
			}
			return new FunctionObject(fncCtx)
			{
				FunctionType = functionType,
				Binding = bindingIdentifier,
				ParameterDeclarations = astNodeList,
				Body = block,
				IsGenerator = flag2
			};
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002EF10 File Offset: 0x0002D110
		private void ParseFunctionBody(Block body)
		{
			bool flag = true;
			while (this.m_currentToken.IsNot(JSToken.RightCurly) && this.m_currentToken.IsNot(JSToken.EndOfFile))
			{
				AstNode astNode = this.ParseStatement(true, false);
				if (flag)
				{
					ConstantWrapper constantWrapper = astNode as ConstantWrapper;
					if (constantWrapper != null && constantWrapper.PrimitiveType == PrimitiveType.String)
					{
						if (!(constantWrapper is DirectivePrologue))
						{
							astNode = new DirectivePrologue(constantWrapper.Value.ToString(), constantWrapper.Context)
							{
								MayHaveIssues = constantWrapper.MayHaveIssues
							};
						}
					}
					else if (!this.m_newModule)
					{
						flag = false;
					}
				}
				else if (this.m_newModule)
				{
					flag = true;
				}
				body.Append(astNode);
			}
			this.AppendImportantComments(body);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002F020 File Offset: 0x0002D220
		private AstNodeList ParseFormalParameters()
		{
			AstNodeList astNodeList = null;
			if (this.m_currentToken.Is(JSToken.LeftParenthesis))
			{
				astNodeList = new AstNodeList(this.m_currentToken.Clone());
				JSToken jstoken = JSToken.Comma;
				while (jstoken == JSToken.Comma)
				{
					ParameterDeclaration parameterDeclaration = null;
					this.GetNextToken();
					if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
					{
						Context context = null;
						if (this.m_currentToken.Is(JSToken.RestSpread))
						{
							this.ParsedVersion = ScriptVersion.EcmaScript6;
							context = this.m_currentToken.Clone();
							this.GetNextToken();
						}
						AstNode astNode = this.ParseBinding();
						if (astNode != null)
						{
							parameterDeclaration = new ParameterDeclaration(astNode.Context.Clone())
							{
								Binding = astNode,
								Position = astNodeList.Count,
								HasRest = (context != null),
								RestContext = context
							};
							astNodeList.Append(parameterDeclaration);
						}
						else
						{
							this.ReportError(JSError.NoBinding, null, false);
						}
						if (this.m_currentToken.Is(JSToken.Assign))
						{
							this.ParsedVersion = ScriptVersion.EcmaScript6;
							parameterDeclaration.IfNotNull((ParameterDeclaration p) => p.AssignContext = this.m_currentToken.Clone());
							this.GetNextToken();
							AstNode initializer = this.ParseExpression(true, JSToken.None);
							parameterDeclaration.IfNotNull((ParameterDeclaration p) => p.Initializer = initializer);
						}
					}
					jstoken = this.m_currentToken.Token;
					if (jstoken == JSToken.Comma)
					{
						parameterDeclaration.IfNotNull((ParameterDeclaration p) => p.TerminatingContext = this.m_currentToken.Clone());
					}
					else if (jstoken != JSToken.RightParenthesis)
					{
						this.ReportError(JSError.NoRightParenthesisOrComma, null, false);
					}
				}
				if (this.m_currentToken.Is(JSToken.RightParenthesis))
				{
					astNodeList.UpdateWith(this.m_currentToken);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoRightParenthesis, null, false);
				}
			}
			return astNodeList;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002F1EC File Offset: 0x0002D3EC
		private ClassNode ParseClassNode(ClassType classType)
		{
			Context context = this.m_currentToken.Clone();
			Context context2 = context.Clone();
			this.GetNextToken();
			AstNode astNode = null;
			if (this.m_currentToken.IsNot(JSToken.LeftCurly) && this.m_currentToken.IsNot(JSToken.Extends))
			{
				astNode = this.ParseBinding();
			}
			if (!(astNode is BindingIdentifier) && classType == ClassType.Declaration)
			{
				this.ReportError(JSError.NoIdentifier, astNode.IfNotNull((AstNode b) => b.Context), false);
			}
			Context context3 = null;
			AstNode astNode2 = null;
			Context context4 = null;
			Context context5 = null;
			if (this.m_currentToken.Is(JSToken.Extends))
			{
				context3 = this.m_currentToken.Clone();
				context2.UpdateWith(context3);
				this.GetNextToken();
				astNode2 = this.ParseExpression(true, JSToken.None);
				if (astNode2 != null)
				{
					context2.UpdateWith(astNode2.Context);
				}
				else
				{
					this.ReportError(JSError.ExpressionExpected, null, false);
				}
			}
			AstNodeList astNodeList = null;
			if (this.m_currentToken.Is(JSToken.LeftCurly))
			{
				context4 = this.m_currentToken.Clone();
				context2.UpdateWith(context4);
				this.GetNextToken();
				astNodeList = new AstNodeList(this.m_currentToken.FlattenToStart());
				while (this.m_currentToken.IsNot(JSToken.EndOfFile) && this.m_currentToken.IsNot(JSToken.RightCurly))
				{
					if (this.m_currentToken.Is(JSToken.Semicolon))
					{
						this.GetNextToken();
					}
					else
					{
						AstNode astNode3 = this.ParseClassElement();
						if (astNode3 != null)
						{
							astNodeList.Append(astNode3);
							context2.UpdateWith(astNode3.Context);
						}
						else
						{
							this.ReportError(JSError.ClassElementExpected, null, false);
						}
					}
				}
				if (this.m_currentToken.Is(JSToken.RightCurly))
				{
					context5 = this.m_currentToken.Clone();
					context2.UpdateWith(context5);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoRightCurly, null, false);
				}
			}
			else
			{
				this.ReportError(JSError.NoLeftCurly, null, false);
			}
			return new ClassNode(context2)
			{
				ClassType = classType,
				ClassContext = context,
				Binding = astNode,
				ExtendsContext = context3,
				Heritage = astNode2,
				OpenBrace = context4,
				Elements = astNodeList,
				CloseBrace = context5
			};
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002F420 File Offset: 0x0002D620
		private AstNode ParseClassElement()
		{
			Context context = this.m_currentToken.Is(JSToken.Static) ? this.m_currentToken.Clone() : null;
			if (context != null)
			{
				this.GetNextToken();
			}
			FunctionType functionType = this.m_currentToken.Is(JSToken.Get) ? FunctionType.Getter : (this.m_currentToken.Is(JSToken.Set) ? FunctionType.Setter : FunctionType.Method);
			FunctionObject functionObject = this.ParseFunction(functionType, this.m_currentToken.FlattenToStart());
			if (functionObject != null && context != null)
			{
				functionObject.IsStatic = true;
				functionObject.StaticContext = context;
			}
			return functionObject;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		private AstNode ParseExpression(bool single = false, JSToken inToken = JSToken.None)
		{
			bool bCanAssign;
			AstNode leftHandSide = this.ParseUnaryExpression(out bCanAssign, false);
			return this.ParseExpression(leftHandSide, single, bCanAssign, inToken);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		private AstNode ParseExpression(AstNode leftHandSide, bool single, bool bCanAssign, JSToken inToken)
		{
			Stack<Context> stack = new Stack<Context>();
			stack.Push(null);
			Stack<AstNode> stack2 = new Stack<AstNode>();
			stack2.Push(leftHandSide);
			while (JSScanner.IsProcessableOperator(this.m_currentToken.Token) && this.m_currentToken.IsNot(inToken) && (!single || this.m_currentToken.IsNot(JSToken.Comma)))
			{
				OperatorPrecedence operatorPrecedence = JSScanner.GetOperatorPrecedence(this.m_currentToken);
				bool flag = JSScanner.IsRightAssociativeOperator(this.m_currentToken.Token);
				OperatorPrecedence operatorPrecedence2 = JSScanner.GetOperatorPrecedence(stack.Peek());
				while (operatorPrecedence < operatorPrecedence2 || (operatorPrecedence == operatorPrecedence2 && !flag))
				{
					AstNode operand = stack2.Pop();
					AstNode operand2 = stack2.Pop();
					AstNode item = JSParser.CreateExpressionNode(stack.Pop(), operand2, operand);
					stack2.Push(item);
					operatorPrecedence2 = JSScanner.GetOperatorPrecedence(stack.Peek());
				}
				if (this.m_currentToken.Is(JSToken.ConditionalIf))
				{
					AstNode astNode = stack2.Pop();
					BinaryOperator binaryOperator = astNode as BinaryOperator;
					if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Assign)
					{
						astNode.Context.HandleError(JSError.SuspectAssignment, false);
					}
					Context questionContext = this.m_currentToken.Clone();
					this.GetNextToken();
					AstNode trueExpression = this.ParseExpression(true, JSToken.None);
					Context colonContext = null;
					if (this.m_currentToken.IsNot(JSToken.Colon))
					{
						this.ReportError(JSError.NoColon, null, false);
					}
					else
					{
						colonContext = this.m_currentToken.Clone();
					}
					this.GetNextToken();
					AstNode astNode2 = this.ParseExpression(true, inToken);
					AstNode item = new Conditional(astNode.Context.CombineWith(astNode2.Context))
					{
						Condition = astNode,
						QuestionContext = questionContext,
						TrueExpression = trueExpression,
						ColonContext = colonContext,
						FalseExpression = astNode2
					};
					stack2.Push(item);
				}
				else
				{
					if (JSScanner.IsAssignmentOperator(this.m_currentToken.Token))
					{
						if (!bCanAssign)
						{
							this.ReportError(JSError.IllegalAssignment, null, false);
						}
					}
					else
					{
						bCanAssign = this.m_currentToken.Is(JSToken.Comma);
					}
					stack.Push(this.m_currentToken.Clone());
					this.GetNextToken();
					if (bCanAssign)
					{
						stack2.Push(this.ParseUnaryExpression(out bCanAssign, false));
					}
					else
					{
						bool flag2;
						stack2.Push(this.ParseUnaryExpression(out flag2, false));
					}
				}
			}
			while (stack.Peek() != null)
			{
				AstNode operand3 = stack2.Pop();
				AstNode operand4 = stack2.Pop();
				AstNode item = JSParser.CreateExpressionNode(stack.Pop(), operand4, operand3);
				stack2.Push(item);
			}
			AstNode astNode3 = stack2.Pop();
			if (astNode3 != null && astNode3.Context.Token == JSToken.Yield && astNode3 is Lookup)
			{
				AstNode astNode4 = this.ParseExpression(true, JSToken.None);
				if (astNode4 != null)
				{
					astNode3 = new UnaryOperator(astNode3.Context.CombineWith(astNode4.Context))
					{
						OperatorToken = JSToken.Yield,
						OperatorContext = astNode3.Context,
						Operand = astNode4
					};
				}
			}
			return astNode3;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		private AstNode ParseUnaryExpression(out bool isLeftHandSideExpr, bool isMinus)
		{
			isLeftHandSideExpr = false;
			bool flag = false;
			JSToken token;
			JSToken jstoken;
			Context context;
			AstNode astNode;
			Context operatorContext;
			Context operatorContext2;
			for (;;)
			{
				token = this.m_currentToken.Token;
				jstoken = token;
				if (jstoken != JSToken.ConditionalCommentStart)
				{
					break;
				}
				context = this.m_currentToken.Clone();
				this.GetNextToken();
				if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
				{
					this.GetNextToken();
				}
				else if (this.m_currentToken.Is(JSToken.ConditionalCompilationOn))
				{
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.ConditionalCompilationVariable))
					{
						astNode = new ConstantWrapperPP(this.m_currentToken.Clone())
						{
							VarName = this.m_currentToken.Code,
							ForceComments = true
						};
						this.GetNextToken();
						if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
						{
							goto Block_6;
						}
						this.CCTooComplicated(null);
					}
					else if (this.m_currentToken.Is(JSToken.LogicalNot))
					{
						operatorContext = this.m_currentToken.Clone();
						this.GetNextToken();
						if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
						{
							goto Block_8;
						}
						this.CCTooComplicated(null);
					}
					else
					{
						this.CCTooComplicated(null);
					}
				}
				else if (this.m_currentToken.Is(JSToken.LogicalNot))
				{
					operatorContext2 = this.m_currentToken.Clone();
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
					{
						goto Block_10;
					}
					this.CCTooComplicated(null);
				}
				else if (this.m_currentToken.Is(JSToken.ConditionalCompilationVariable))
				{
					astNode = new ConstantWrapperPP(this.m_currentToken.Clone())
					{
						VarName = this.m_currentToken.Code,
						ForceComments = true
					};
					this.GetNextToken();
					if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
					{
						goto Block_12;
					}
					this.CCTooComplicated(null);
				}
				else
				{
					this.CCTooComplicated(null);
				}
			}
			switch (jstoken)
			{
			case JSToken.RestSpread:
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				break;
			case JSToken.FirstOperator:
			case JSToken.Increment:
			case JSToken.Decrement:
			case JSToken.Void:
			case JSToken.TypeOf:
			case JSToken.LogicalNot:
			case JSToken.BitwiseNot:
			case JSToken.FirstBinaryOperator:
			case JSToken.Minus:
				break;
			default:
				astNode = this.ParseLeftHandSideExpression(isMinus);
				return this.ParsePostfixExpression(astNode, out isLeftHandSideExpr);
			}
			context = this.m_currentToken.Clone();
			this.GetNextToken();
			AstNode astNode2 = this.ParseUnaryExpression(out flag, false);
			return new UnaryOperator(context.CombineWith(astNode2.Context))
			{
				Operand = astNode2,
				OperatorContext = context,
				OperatorToken = token
			};
			Block_6:
			this.GetNextToken();
			return astNode;
			Block_8:
			this.GetNextToken();
			astNode2 = this.ParseUnaryExpression(out flag, false);
			context.UpdateWith(astNode2.Context);
			UnaryOperator unaryOperator = new UnaryOperator(context)
			{
				Operand = astNode2,
				OperatorContext = operatorContext,
				OperatorToken = JSToken.LogicalNot
			};
			unaryOperator.OperatorInConditionalCompilationComment = true;
			unaryOperator.ConditionalCommentContainsOn = true;
			return unaryOperator;
			Block_10:
			this.GetNextToken();
			astNode2 = this.ParseUnaryExpression(out flag, false);
			context.UpdateWith(astNode2.Context);
			UnaryOperator unaryOperator2 = new UnaryOperator(context)
			{
				Operand = astNode2,
				OperatorContext = operatorContext2,
				OperatorToken = JSToken.LogicalNot
			};
			unaryOperator2.OperatorInConditionalCompilationComment = true;
			return unaryOperator2;
			Block_12:
			this.GetNextToken();
			return astNode;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002FADC File Offset: 0x0002DCDC
		private AstNode ParsePostfixExpression(AstNode ast, out bool isLeftHandSideExpr)
		{
			isLeftHandSideExpr = true;
			if (ast != null && !this.m_foundEndOfLine)
			{
				if (this.m_currentToken.Is(JSToken.Increment))
				{
					isLeftHandSideExpr = false;
					Context context = ast.Context.Clone();
					context.UpdateWith(this.m_currentToken);
					ast = new UnaryOperator(context)
					{
						Operand = ast,
						OperatorToken = this.m_currentToken.Token,
						OperatorContext = this.m_currentToken.Clone(),
						IsPostfix = true
					};
					this.GetNextToken();
				}
				else if (this.m_currentToken.Is(JSToken.Decrement))
				{
					isLeftHandSideExpr = false;
					Context context = ast.Context.Clone();
					context.UpdateWith(this.m_currentToken);
					ast = new UnaryOperator(context)
					{
						Operand = ast,
						OperatorToken = this.m_currentToken.Token,
						OperatorContext = this.m_currentToken.Clone(),
						IsPostfix = true
					};
					this.GetNextToken();
				}
			}
			return ast;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		private AstNode ParseLeftHandSideExpression(bool isMinus)
		{
			AstNode astNode = null;
			List<Context> list = null;
			JSToken token;
			JSToken jstoken;
			for (;;)
			{
				if (!this.m_currentToken.Is(JSToken.New))
				{
					token = this.m_currentToken.Token;
					jstoken = token;
					if (jstoken > JSToken.Modulo)
					{
						goto IL_CF;
					}
					if (jstoken != JSToken.LeftCurly)
					{
						switch (jstoken)
						{
						case JSToken.Function:
							goto IL_5B9;
						case JSToken.Else:
						case JSToken.ConditionalCommentEnd:
						case JSToken.ConditionalCompilationOn:
						case JSToken.ConditionalCompilationSet:
						case JSToken.ConditionalCompilationIf:
						case JSToken.ConditionalCompilationElseIf:
						case JSToken.ConditionalCompilationElse:
						case JSToken.ConditionalCompilationEnd:
							goto IL_659;
						case JSToken.ConditionalCommentStart:
							this.GetNextToken();
							if (this.m_currentToken.Is(JSToken.ConditionalCompilationVariable))
							{
								astNode = new ConstantWrapperPP(this.m_currentToken.Clone())
								{
									VarName = this.m_currentToken.Code,
									ForceComments = true
								};
								this.GetNextToken();
								if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
								{
									goto Block_13;
								}
								this.CCTooComplicated(null);
								continue;
							}
							else
							{
								if (this.m_currentToken.Is(JSToken.ConditionalCommentEnd))
								{
									this.GetNextToken();
									continue;
								}
								this.m_currentToken.HandleError(JSError.ConditionalCompilationTooComplex, false);
								while (this.m_currentToken.IsNot(JSToken.EndOfFile) && this.m_currentToken.IsNot(JSToken.ConditionalCommentEnd))
								{
									this.GetNextToken();
								}
								this.GetNextToken();
								continue;
							}
							break;
						case JSToken.ConditionalCompilationVariable:
							goto IL_397;
						case JSToken.Identifier:
							goto IL_103;
						case JSToken.Null:
							goto IL_379;
						case JSToken.True:
							goto IL_333;
						case JSToken.False:
							goto IL_356;
						case JSToken.This:
							goto IL_208;
						case JSToken.StringLiteral:
							goto IL_224;
						case JSToken.IntegerLiteral:
						case JSToken.NumericLiteral:
							goto IL_262;
						case JSToken.TemplateLiteral:
							goto IL_135;
						case JSToken.LeftParenthesis:
							goto IL_3F5;
						case JSToken.LeftBracket:
							goto IL_59F;
						}
						break;
					}
					goto IL_5AC;
				}
				else
				{
					if (list == null)
					{
						list = new List<Context>(4);
					}
					list.Add(this.m_currentToken.Clone());
					this.GetNextToken();
				}
			}
			switch (jstoken)
			{
			case JSToken.Divide:
				IL_3D1:
				astNode = this.ScanRegularExpression();
				if (astNode != null)
				{
					goto IL_6A2;
				}
				goto IL_659;
			case JSToken.Modulo:
				astNode = this.ScanReplacementToken();
				if (astNode != null)
				{
					goto IL_6A2;
				}
				goto IL_659;
			default:
				goto IL_659;
			}
			IL_CF:
			if (jstoken <= JSToken.Class)
			{
				if (jstoken == JSToken.DivideAssign)
				{
					goto IL_3D1;
				}
				if (jstoken != JSToken.Class)
				{
					goto IL_659;
				}
				astNode = this.ParseClassNode(ClassType.Expression);
				goto IL_6A2;
			}
			else if (jstoken != JSToken.Yield)
			{
				if (jstoken != JSToken.AspNetBlock)
				{
					goto IL_659;
				}
				astNode = new AspNetBlockNode(this.m_currentToken.Clone())
				{
					AspNetBlockText = this.m_currentToken.Code
				};
				this.GetNextToken();
				goto IL_6A2;
			}
			else
			{
				if (this.ParsedVersion == ScriptVersion.EcmaScript6 || this.m_settings.ScriptVersion == ScriptVersion.EcmaScript6)
				{
					astNode = this.ParseYieldExpression();
					goto IL_6A2;
				}
				astNode = new Lookup(this.m_currentToken.Clone())
				{
					Name = "yield"
				};
				this.GetNextToken();
				goto IL_6A2;
			}
			IL_103:
			astNode = new Lookup(this.m_currentToken.Clone())
			{
				Name = this.m_scanner.Identifier
			};
			this.GetNextToken();
			goto IL_6A2;
			IL_135:
			astNode = this.ParseTemplateLiteral();
			goto IL_6A2;
			Block_13:
			this.GetNextToken();
			goto IL_6A2;
			IL_208:
			astNode = new ThisLiteral(this.m_currentToken.Clone());
			this.GetNextToken();
			goto IL_6A2;
			IL_224:
			astNode = new ConstantWrapper(this.m_scanner.StringLiteralValue, PrimitiveType.String, this.m_currentToken.Clone())
			{
				MayHaveIssues = this.m_scanner.LiteralHasIssues
			};
			this.GetNextToken();
			goto IL_6A2;
			IL_262:
			Context context = this.m_currentToken.Clone();
			double num;
			if (this.ConvertNumericLiteralToDouble(this.m_currentToken.Code, token == JSToken.IntegerLiteral, out num))
			{
				bool literalHasIssues = this.m_scanner.LiteralHasIssues;
				if (num == 1.7976931348623157E+308)
				{
					this.ReportError(JSError.NumericMaximum, context, false);
				}
				else if (isMinus && -num == -1.7976931348623157E+308)
				{
					this.ReportError(JSError.NumericMinimum, context, false);
				}
				astNode = new ConstantWrapper(num, PrimitiveType.Number, context)
				{
					MayHaveIssues = literalHasIssues
				};
			}
			else
			{
				if (double.IsInfinity(num))
				{
					this.ReportError(JSError.NumericOverflow, context, false);
				}
				astNode = new ConstantWrapper(this.m_currentToken.Code, PrimitiveType.Other, context)
				{
					MayHaveIssues = true
				};
			}
			this.GetNextToken();
			goto IL_6A2;
			IL_333:
			astNode = new ConstantWrapper(true, PrimitiveType.Boolean, this.m_currentToken.Clone());
			this.GetNextToken();
			goto IL_6A2;
			IL_356:
			astNode = new ConstantWrapper(false, PrimitiveType.Boolean, this.m_currentToken.Clone());
			this.GetNextToken();
			goto IL_6A2;
			IL_379:
			astNode = new ConstantWrapper(null, PrimitiveType.Null, this.m_currentToken.Clone());
			this.GetNextToken();
			goto IL_6A2;
			IL_397:
			astNode = new ConstantWrapperPP(this.m_currentToken.Clone())
			{
				VarName = this.m_currentToken.Code,
				ForceComments = false
			};
			this.GetNextToken();
			goto IL_6A2;
			IL_3F5:
			Context context2 = this.m_currentToken.Clone();
			this.GetNextToken();
			if (this.m_currentToken.Is(JSToken.For))
			{
				astNode = this.ParseComprehension(false, context2, null);
				goto IL_6A2;
			}
			if (this.m_currentToken.Is(JSToken.RightParenthesis))
			{
				astNode = new GroupingOperator(context2);
				astNode.UpdateWith(this.m_currentToken);
				this.GetNextToken();
				goto IL_6A2;
			}
			if (this.m_currentToken.Is(JSToken.RestSpread))
			{
				Context context3 = this.m_currentToken.Clone();
				this.GetNextToken();
				astNode = this.ParseExpression(true, JSToken.None);
				if (astNode != null)
				{
					astNode = new UnaryOperator(context3.CombineWith(astNode.Context))
					{
						OperatorContext = context3,
						OperatorToken = JSToken.RestSpread,
						Operand = astNode
					};
				}
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					astNode = this.ParseExpression(astNode, false, true, JSToken.None);
				}
				if (this.m_currentToken.Is(JSToken.RightParenthesis))
				{
					astNode = new GroupingOperator(context2)
					{
						Operand = astNode
					};
					astNode.UpdateWith(this.m_currentToken);
					this.GetNextToken();
					goto IL_6A2;
				}
				this.ReportError(JSError.NoRightParenthesis, null, false);
				goto IL_6A2;
			}
			else
			{
				AstNode astNode2 = this.ParseExpression(false, JSToken.None);
				if (this.m_currentToken.Is(JSToken.For))
				{
					astNode = this.ParseComprehension(false, context2, astNode2);
					goto IL_6A2;
				}
				astNode = new GroupingOperator(context2)
				{
					Operand = astNode2
				};
				astNode.UpdateWith(astNode2.Context);
				if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
				{
					this.ReportError(JSError.NoRightParenthesis, null, false);
					goto IL_6A2;
				}
				astNode.UpdateWith(this.m_currentToken);
				this.GetNextToken();
				goto IL_6A2;
			}
			IL_59F:
			astNode = this.ParseArrayLiteral(false);
			goto IL_6A2;
			IL_5AC:
			astNode = this.ParseObjectLiteral(false);
			goto IL_6A2;
			IL_5B9:
			astNode = this.ParseFunction(FunctionType.Expression, this.m_currentToken.Clone());
			goto IL_6A2;
			IL_659:
			string text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token);
			if (text != null)
			{
				astNode = new Lookup(this.m_currentToken.Clone())
				{
					Name = text
				};
				this.GetNextToken();
			}
			else
			{
				this.ReportError(JSError.ExpressionExpected, null, false);
			}
			IL_6A2:
			if (this.m_currentToken.Is(JSToken.ArrowFunction))
			{
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				astNode = this.ParseArrowFunction(astNode);
			}
			return this.ParseMemberExpression(astNode, list);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000302B0 File Offset: 0x0002E4B0
		private RegExpLiteral ScanRegularExpression()
		{
			RegExpLiteral result = null;
			this.m_currentToken = this.m_scanner.UpdateToken(UpdateHint.RegularExpression);
			if (this.m_currentToken.Is(JSToken.RegularExpression))
			{
				Context context = this.m_currentToken.Clone();
				string text = this.m_currentToken.Code;
				text = text.Substring(1, text.Length - 2);
				this.GetNextToken();
				string patternSwitches = null;
				if (this.m_currentToken.Is(JSToken.Identifier))
				{
					context.UpdateWith(this.m_currentToken);
					patternSwitches = this.m_scanner.Identifier;
					this.GetNextToken();
				}
				result = new RegExpLiteral(this.m_currentToken.Clone())
				{
					Pattern = text,
					PatternSwitches = patternSwitches
				};
			}
			return result;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00030368 File Offset: 0x0002E568
		private ConstantWrapper ScanReplacementToken()
		{
			ConstantWrapper result = null;
			this.m_currentToken = this.m_scanner.UpdateToken(UpdateHint.ReplacementToken);
			if (this.m_currentToken.Is(JSToken.ReplacementToken))
			{
				result = new ConstantWrapper(this.m_currentToken.Code, PrimitiveType.Other, this.m_currentToken.Clone());
				this.GetNextToken();
			}
			return result;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000303BC File Offset: 0x0002E5BC
		private TemplateLiteral ParseTemplateLiteral()
		{
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			Context context = this.m_currentToken.Clone();
			Context context2 = this.m_currentToken.Clone();
			Lookup function = null;
			string text = this.m_scanner.StringLiteralValue;
			int num = text.IndexOf('`');
			if (num != 0)
			{
				string name = text.Substring(0, num);
				text = text.Substring(num);
				Context context3 = context2.SplitStart(num);
				function = new Lookup(context3)
				{
					Name = name
				};
			}
			bool flag = text[text.Length - 1] != '`';
			TemplateLiteral templateLiteral = new TemplateLiteral(context)
			{
				Function = function,
				Text = text,
				TextContext = context2,
				Expressions = (flag ? new AstNodeList(context.FlattenToEnd()) : null)
			};
			this.GetNextToken();
			if (flag)
			{
				do
				{
					flag = false;
					AstNode astNode = this.ParseExpression(false, JSToken.None);
					if (this.m_currentToken.Is(JSToken.RightCurly))
					{
						this.m_scanner.UpdateToken(UpdateHint.TemplateLiteral);
						if (this.m_currentToken.Is(JSToken.TemplateLiteral))
						{
							text = this.m_scanner.StringLiteralValue;
							TemplateLiteralExpression templateLiteralExpression = new TemplateLiteralExpression(astNode.Context.Clone())
							{
								Expression = astNode,
								Text = text
							};
							templateLiteral.UpdateWith(templateLiteralExpression.Context);
							templateLiteral.Expressions.Append(templateLiteralExpression);
							this.GetNextToken();
							flag = (text[text.Length - 1] != '`');
						}
					}
					else
					{
						this.ReportError(JSError.NoRightCurly, null, false);
					}
				}
				while (flag);
			}
			return templateLiteral;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00030558 File Offset: 0x0002E758
		private AstNode ParseYieldExpression()
		{
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			Context context = this.m_currentToken.Clone();
			Context operatorContext = context.Clone();
			this.GetNextToken();
			bool flag = this.m_currentToken.Is(JSToken.Multiply);
			if (flag)
			{
				this.GetNextToken();
			}
			AstNode astNode = this.ParseExpression(true, JSToken.None);
			if (astNode == null)
			{
				this.ReportError(JSError.ExpressionExpected, null, false);
			}
			else
			{
				context.UpdateWith(astNode.Context);
			}
			return new UnaryOperator(context)
			{
				OperatorContext = operatorContext,
				OperatorToken = JSToken.Yield,
				Operand = astNode,
				IsDelegator = flag
			};
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0003060C File Offset: 0x0002E80C
		private FunctionObject ParseArrowFunction(AstNode parameters)
		{
			Context context = this.m_currentToken.Clone();
			this.GetNextToken();
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			FunctionObject functionObject = new FunctionObject(parameters.Context.Clone())
			{
				ParameterDeclarations = BindingTransform.ToParameters(parameters),
				FunctionType = FunctionType.ArrowFunction
			};
			functionObject.UpdateWith(context);
			if (this.m_currentToken.Is(JSToken.LeftCurly))
			{
				functionObject.Body = this.ParseBlock();
			}
			else
			{
				functionObject.Body = AstNode.ForceToBlock(this.ParseExpression(true, JSToken.None));
				functionObject.Body.IsConcise = true;
			}
			functionObject.Body.IfNotNull(delegate(Block b)
			{
				functionObject.UpdateWith(b.Context);
			});
			return functionObject;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00030700 File Offset: 0x0002E900
		private AstNode ParseArrayLiteral(bool isBindingPattern)
		{
			Context context = this.m_currentToken.Clone();
			Context context2 = context.Clone();
			AstNodeList astNodeList = new AstNodeList(this.CurrentPositionContext);
			bool mayHaveIssues = false;
			Context commaContext = null;
			AstNode astNode;
			for (;;)
			{
				this.GetNextToken();
				astNode = null;
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					astNode = new ConstantWrapper(Missing.Value, PrimitiveType.Other, this.m_currentToken.FlattenToStart());
				}
				else if (this.m_currentToken.Is(JSToken.RightBracket))
				{
					if (astNodeList.Count == 0)
					{
						goto IL_213;
					}
					if (!isBindingPattern)
					{
						mayHaveIssues = true;
						astNode = new ConstantWrapper(Missing.Value, PrimitiveType.Other, this.m_currentToken.FlattenToStart());
						commaContext.HandleError(JSError.ArrayLiteralTrailingComma, false);
					}
				}
				else
				{
					if (this.m_currentToken.Is(JSToken.For))
					{
						break;
					}
					Context context3 = null;
					if (this.m_currentToken.Is(JSToken.RestSpread))
					{
						this.ParsedVersion = ScriptVersion.EcmaScript6;
						context3 = this.m_currentToken.Clone();
						this.GetNextToken();
					}
					if (isBindingPattern)
					{
						astNode = this.ParseBinding();
						if (this.m_currentToken.Is(JSToken.Assign))
						{
							Context context4 = this.m_currentToken.Clone();
							this.GetNextToken();
							astNode = new InitializerNode(context4.Clone())
							{
								Binding = astNode,
								AssignContext = context4,
								Initializer = this.ParseExpression(true, JSToken.None)
							};
						}
					}
					else
					{
						astNode = this.ParseExpression(true, JSToken.None);
					}
					if (context3 != null)
					{
						astNode = new UnaryOperator(context3.CombineWith(astNode.Context))
						{
							Operand = astNode,
							OperatorToken = JSToken.RestSpread,
							OperatorContext = context3
						};
					}
				}
				if (this.m_currentToken.Is(JSToken.For))
				{
					goto Block_10;
				}
				astNodeList.Append(astNode);
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					commaContext = this.m_currentToken.Clone();
					astNode.IfNotNull((AstNode e) => e.TerminatingContext = commaContext);
				}
				if (!this.m_currentToken.Is(JSToken.Comma))
				{
					goto IL_213;
				}
			}
			return this.ParseComprehension(true, context, null);
			Block_10:
			return this.ParseComprehension(true, context, astNode);
			IL_213:
			if (this.m_currentToken.Is(JSToken.RightBracket))
			{
				context2.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			else
			{
				this.m_currentToken.HandleError(JSError.NoRightBracketOrComma, true);
			}
			return new ArrayLiteral(context2)
			{
				Elements = astNodeList,
				MayHaveIssues = mayHaveIssues
			};
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000309B0 File Offset: 0x0002EBB0
		private ComprehensionNode ParseComprehension(bool isArray, Context openDelimiter, AstNode expression)
		{
			bool mozillaOrdering = expression != null;
			Context context = openDelimiter.Clone();
			Context context2 = null;
			expression.IfNotNull((AstNode e) => context.UpdateWith(e.Context));
			AstNodeList astNodeList = new AstNodeList(this.m_currentToken.Clone());
			do
			{
				if (this.m_currentToken.IsOne(new JSToken[]
				{
					JSToken.For,
					JSToken.If
				}))
				{
					ComprehensionClause comprehensionClause = this.ParseComprehensionClause();
					comprehensionClause.IfNotNull((ComprehensionClause c) => context.UpdateWith(c.Context));
					astNodeList.Append(comprehensionClause);
				}
				else
				{
					this.ReportError(JSError.NoForOrIf, null, false);
				}
			}
			while (this.m_currentToken.IsOne(new JSToken[]
			{
				JSToken.For,
				JSToken.If
			}));
			context.UpdateWith(astNodeList.Context);
			if (expression == null)
			{
				expression = this.ParseExpression(true, JSToken.None);
				expression.IfNotNull((AstNode e) => context.UpdateWith(e.Context));
			}
			if (this.m_currentToken.IsNot(isArray ? JSToken.RightBracket : JSToken.RightParenthesis))
			{
				this.ReportError(isArray ? JSError.NoRightBracket : JSError.NoRightParenthesis, null, false);
			}
			else
			{
				context2 = this.m_currentToken.Clone();
				context.UpdateWith(context2);
				this.GetNextToken();
			}
			this.ParsedVersion = ScriptVersion.EcmaScript6;
			return new ComprehensionNode(context)
			{
				OpenDelimiter = openDelimiter,
				Expression = expression,
				Clauses = astNodeList,
				CloseDelimiter = context2,
				ComprehensionType = (isArray ? ComprehensionType.Array : ComprehensionType.Generator),
				MozillaOrdering = mozillaOrdering
			};
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00030B9C File Offset: 0x0002ED9C
		private ComprehensionClause ParseComprehensionClause()
		{
			Context context = this.m_currentToken.Clone();
			Context clauseContext = context.Clone();
			this.GetNextToken();
			Context context2 = null;
			if (this.m_currentToken.IsNot(JSToken.LeftParenthesis))
			{
				this.ReportError(JSError.NoLeftParenthesis, context, false);
			}
			else
			{
				context2 = this.m_currentToken.Clone();
				clauseContext.UpdateWith(context2);
				this.GetNextToken();
			}
			AstNode astNode = null;
			Context context3 = null;
			bool isInOperation = false;
			AstNode astNode2;
			if (context.Is(JSToken.For))
			{
				astNode = this.ParseBinding();
				astNode.IfNotNull((AstNode b) => clauseContext.UpdateWith(b.Context));
				if (this.m_currentToken.Is(JSToken.In) || this.m_currentToken.Is("of"))
				{
					isInOperation = this.m_currentToken.Is(JSToken.In);
					context3 = this.m_currentToken.Clone();
					this.GetNextToken();
					clauseContext.UpdateWith(context3);
				}
				else
				{
					this.ReportError(JSError.NoForOrIf, null, false);
				}
				astNode2 = this.ParseExpression(true, JSToken.None);
				astNode2.IfNotNull((AstNode e) => clauseContext.UpdateWith(e.Context));
			}
			else
			{
				astNode2 = this.ParseExpression(true, JSToken.None);
				astNode2.IfNotNull((AstNode e) => clauseContext.UpdateWith(e.Context));
			}
			Context context4 = null;
			if (this.m_currentToken.IsNot(JSToken.RightParenthesis))
			{
				this.ReportError(JSError.NoRightParenthesis, null, false);
			}
			else
			{
				context4 = this.m_currentToken.Clone();
				clauseContext.UpdateWith(context4);
				this.GetNextToken();
			}
			if (context.Is(JSToken.For))
			{
				return new ComprehensionForClause(clauseContext)
				{
					OperatorContext = context,
					OpenContext = context2,
					Binding = astNode,
					IsInOperation = isInOperation,
					OfContext = context3,
					Expression = astNode2,
					CloseContext = context4
				};
			}
			return new ComprehensionIfClause(clauseContext)
			{
				OperatorContext = context,
				OpenContext = context2,
				Condition = astNode2,
				CloseContext = context4
			};
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		private ObjectLiteral ParseObjectLiteral(bool isBindingPattern)
		{
			Context context = this.m_currentToken.Clone();
			AstNodeList astNodeList = new AstNodeList(this.CurrentPositionContext);
			do
			{
				this.GetNextToken();
				if (this.m_currentToken.IsNot(JSToken.RightCurly))
				{
					ObjectLiteralProperty node = this.ParseObjectLiteralProperty(isBindingPattern);
					astNodeList.Append(node);
				}
			}
			while (this.m_currentToken.Is(JSToken.Comma));
			if (this.m_currentToken.Is(JSToken.RightCurly))
			{
				context.UpdateWith(this.m_currentToken);
				this.GetNextToken();
			}
			else
			{
				this.ReportError(JSError.NoRightCurly, null, false);
			}
			return new ObjectLiteral(context)
			{
				Properties = astNodeList
			};
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		private ObjectLiteralProperty ParseObjectLiteralProperty(bool isBindingPattern)
		{
			ObjectLiteralProperty objectLiteralProperty = null;
			ObjectLiteralField objectLiteralField = null;
			AstNode astNode = null;
			JSToken jstoken = this.PeekToken();
			Context propertyContext = this.m_currentToken.Clone();
			if (jstoken == JSToken.Colon)
			{
				objectLiteralField = this.ParseObjectLiteralFieldName();
				if (this.m_currentToken.Is(JSToken.Colon))
				{
					objectLiteralField.IfNotNull((ObjectLiteralField f) => f.ColonContext = this.m_currentToken.Clone());
					this.GetNextToken();
					astNode = this.ParseObjectPropertyValue(isBindingPattern);
					if (isBindingPattern && this.m_currentToken.Is(JSToken.Assign))
					{
						Context context = this.m_currentToken.Clone();
						this.GetNextToken();
						astNode = new InitializerNode(context.Clone())
						{
							Binding = astNode,
							AssignContext = context,
							Initializer = this.ParseExpression(true, JSToken.None)
						};
					}
				}
			}
			else if (jstoken == JSToken.Comma || jstoken == JSToken.RightCurly || jstoken == JSToken.Assign)
			{
				this.ParsedVersion = ScriptVersion.EcmaScript6;
				astNode = this.ParseObjectPropertyValue(isBindingPattern);
				if (isBindingPattern && this.m_currentToken.Is(JSToken.Assign))
				{
					Context context2 = this.m_currentToken.Clone();
					this.GetNextToken();
					astNode = new InitializerNode(context2.Clone())
					{
						Binding = astNode,
						AssignContext = context2,
						Initializer = this.ParseExpression(true, JSToken.None)
					};
				}
			}
			else if (this.m_currentToken.IsOne(new JSToken[]
			{
				JSToken.Get,
				JSToken.Set
			}))
			{
				bool flag = this.m_currentToken.Is(JSToken.Get);
				Context context3 = this.m_currentToken.Clone();
				FunctionObject functionObject = this.ParseFunction(flag ? FunctionType.Getter : FunctionType.Setter, context3);
				if (functionObject != null)
				{
					objectLiteralField = new GetterSetter(functionObject.Binding.Name, flag, functionObject.Binding.Context.Clone());
					astNode = functionObject;
					if (isBindingPattern)
					{
						context3.HandleError(JSError.MethodsNotAllowedInBindings, true);
					}
				}
				else
				{
					this.ReportError(JSError.FunctionExpressionExpected, null, false);
				}
			}
			else if (this.m_currentToken.Is(JSToken.Multiply) || jstoken == JSToken.LeftParenthesis)
			{
				astNode = this.ParseFunction(FunctionType.Method, this.m_currentToken.Clone());
				if (astNode != null)
				{
					this.ParsedVersion = ScriptVersion.EcmaScript6;
				}
			}
			if (objectLiteralField != null || astNode != null)
			{
				objectLiteralField.IfNotNull((ObjectLiteralField f) => propertyContext.UpdateWith(f.Context));
				astNode.IfNotNull((AstNode v) => propertyContext.UpdateWith(v.Context));
				objectLiteralProperty = new ObjectLiteralProperty(propertyContext)
				{
					Name = objectLiteralField,
					Value = astNode
				};
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					objectLiteralProperty.IfNotNull((ObjectLiteralProperty p) => p.TerminatingContext = this.m_currentToken.Clone());
				}
			}
			return objectLiteralProperty;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000311A0 File Offset: 0x0002F3A0
		private ObjectLiteralField ParseObjectLiteralFieldName()
		{
			JSToken token = this.m_currentToken.Token;
			ObjectLiteralField result;
			if (token != JSToken.Identifier)
			{
				switch (token)
				{
				case JSToken.StringLiteral:
					result = new ObjectLiteralField(this.m_scanner.StringLiteralValue, PrimitiveType.String, this.m_currentToken.Clone())
					{
						MayHaveIssues = this.m_scanner.LiteralHasIssues
					};
					goto IL_18A;
				case JSToken.IntegerLiteral:
				case JSToken.NumericLiteral:
				{
					double num;
					if (this.ConvertNumericLiteralToDouble(this.m_currentToken.Code, this.m_currentToken.Is(JSToken.IntegerLiteral), out num))
					{
						result = new ObjectLiteralField(num, PrimitiveType.Number, this.m_currentToken.Clone());
						goto IL_18A;
					}
					if (double.IsInfinity(num))
					{
						this.ReportError(JSError.NumericOverflow, null, false);
					}
					result = new ObjectLiteralField(this.m_currentToken.Code, PrimitiveType.Other, this.m_currentToken.Clone());
					goto IL_18A;
				}
				default:
					switch (token)
					{
					case JSToken.Get:
					case JSToken.Set:
						break;
					default:
					{
						string identifier = this.m_scanner.Identifier;
						if (JSScanner.IsValidIdentifier(identifier))
						{
							if (JSKeyword.CanBeIdentifier(this.m_currentToken.Token) == null)
							{
								this.ReportError(JSError.ObjectLiteralKeyword, null, false);
							}
							result = new ObjectLiteralField(identifier, PrimitiveType.String, this.m_currentToken.Clone());
							goto IL_18A;
						}
						this.ReportError(JSError.NoMemberIdentifier, null, false);
						result = new ObjectLiteralField(this.m_currentToken.Code, PrimitiveType.String, this.m_currentToken.Clone());
						goto IL_18A;
					}
					}
					break;
				}
			}
			result = new ObjectLiteralField(this.m_scanner.Identifier, PrimitiveType.String, this.m_currentToken.Clone())
			{
				IsIdentifier = true
			};
			IL_18A:
			this.GetNextToken();
			return result;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0003133E File Offset: 0x0002F53E
		private AstNode ParseObjectPropertyValue(bool isBindingPattern)
		{
			if (isBindingPattern)
			{
				return this.ParseBinding();
			}
			return this.ParseExpression(true, JSToken.None);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00031370 File Offset: 0x0002F570
		private AstNode ParseMemberExpression(AstNode expression, List<Context> newContexts)
		{
			for (;;)
			{
				switch (this.m_currentToken.Token)
				{
				case JSToken.LeftParenthesis:
				{
					AstNodeList astNodeList = this.ParseExpressionList(JSToken.RightParenthesis);
					expression = new CallNode(expression.Context.CombineWith(astNodeList.Context))
					{
						Function = expression,
						Arguments = astNodeList,
						InBrackets = false
					};
					if (newContexts != null && newContexts.Count > 0)
					{
						newContexts[newContexts.Count - 1].UpdateWith(expression.Context);
						if (!(expression is CallNode))
						{
							expression = new CallNode(newContexts[newContexts.Count - 1])
							{
								Function = expression,
								Arguments = new AstNodeList(this.CurrentPositionContext)
							};
						}
						else
						{
							expression.Context = newContexts[newContexts.Count - 1];
						}
						((CallNode)expression).IsConstructor = true;
						newContexts.RemoveAt(newContexts.Count - 1);
					}
					this.GetNextToken();
					continue;
				}
				case JSToken.LeftBracket:
				{
					this.GetNextToken();
					AstNodeList astNodeList = new AstNodeList(this.CurrentPositionContext);
					AstNode astNode = this.ParseExpression(false, JSToken.None);
					if (astNode != null)
					{
						astNodeList.Append(astNode);
					}
					expression = new CallNode(expression.Context.CombineWith(this.m_currentToken.Clone()))
					{
						Function = expression,
						Arguments = astNodeList,
						InBrackets = true
					};
					this.GetNextToken();
					continue;
				}
				case JSToken.AccessField:
				{
					ConstantWrapper constantWrapper = null;
					Context nameContext = this.m_currentToken.Clone();
					this.GetNextToken();
					string text;
					if (this.m_currentToken.IsNot(JSToken.Identifier))
					{
						text = JSKeyword.CanBeIdentifier(this.m_currentToken.Token);
						if (text != null)
						{
							constantWrapper = new ConstantWrapper(text, PrimitiveType.String, this.m_currentToken.Clone());
						}
						else if (JSScanner.IsValidIdentifier(this.m_currentToken.Code))
						{
							this.ReportError(JSError.KeywordUsedAsIdentifier, null, false);
							text = this.m_currentToken.Code;
							constantWrapper = new ConstantWrapper(text, PrimitiveType.String, this.m_currentToken.Clone());
						}
						else
						{
							this.ReportError(JSError.NoIdentifier, null, false);
						}
					}
					else
					{
						text = this.m_scanner.Identifier;
						constantWrapper = new ConstantWrapper(text, PrimitiveType.String, this.m_currentToken.Clone());
					}
					if (constantWrapper != null)
					{
						nameContext.UpdateWith(constantWrapper.Context);
					}
					this.GetNextToken();
					expression = new Member(expression.IfNotNull((AstNode e) => e.Context.CombineWith(nameContext), nameContext.Clone()))
					{
						Root = expression,
						Name = text,
						NameContext = nameContext
					};
					continue;
				}
				}
				break;
			}
			if (newContexts != null)
			{
				while (newContexts.Count > 0)
				{
					newContexts[newContexts.Count - 1].UpdateWith(expression.Context);
					expression = new CallNode(newContexts[newContexts.Count - 1])
					{
						Function = expression,
						Arguments = new AstNodeList(this.CurrentPositionContext)
					};
					((CallNode)expression).IsConstructor = true;
					newContexts.RemoveAt(newContexts.Count - 1);
				}
			}
			return expression;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000316C4 File Offset: 0x0002F8C4
		private AstNodeList ParseExpressionList(JSToken terminator)
		{
			AstNodeList astNodeList = new AstNodeList(this.m_currentToken.Clone());
			do
			{
				this.GetNextToken();
				AstNode astNode = null;
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					astNode = new ConstantWrapper(Missing.Value, PrimitiveType.Other, this.m_currentToken.FlattenToStart());
					astNodeList.Append(astNode);
					astNodeList.UpdateWith(this.m_currentToken);
				}
				else if (this.m_currentToken.IsNot(terminator))
				{
					Context context = null;
					if (this.m_currentToken.Is(JSToken.RestSpread))
					{
						this.ParsedVersion = ScriptVersion.EcmaScript6;
						context = this.m_currentToken.Clone();
						this.GetNextToken();
					}
					astNode = this.ParseExpression(true, JSToken.None);
					if (context != null)
					{
						astNode = new UnaryOperator(context.CombineWith(astNode.Context))
						{
							Operand = astNode,
							OperatorToken = JSToken.RestSpread,
							OperatorContext = context
						};
					}
					astNodeList.Append(astNode);
				}
				if (this.m_currentToken.Is(JSToken.Comma))
				{
					astNode.IfNotNull((AstNode i) => i.TerminatingContext = this.m_currentToken.Clone());
				}
			}
			while (this.m_currentToken.Is(JSToken.Comma));
			if (this.m_currentToken.Is(terminator))
			{
				astNodeList.Context.UpdateWith(this.m_currentToken);
			}
			else if (terminator == JSToken.RightParenthesis)
			{
				if (this.m_currentToken.Is(JSToken.Semicolon) && this.PeekToken() == JSToken.RightParenthesis)
				{
					this.ReportError(JSError.UnexpectedSemicolon, null, false);
					this.GetNextToken();
				}
				else
				{
					this.ReportError(JSError.NoRightParenthesis, null, false);
				}
			}
			else
			{
				this.ReportError(JSError.NoRightBracket, null, false);
			}
			return astNodeList;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0003189C File Offset: 0x0002FA9C
		private void SetDocumentContext(DocumentContext documentContext)
		{
			documentContext.Parser = this;
			this.m_scanner = new JSScanner(documentContext);
			this.m_currentToken = this.m_scanner.CurrentToken;
			this.m_scanner.GlobalDefine += delegate(object sender, GlobalDefineEventArgs ea)
			{
				GlobalScope globalScope = this.GlobalScope;
				if (globalScope[ea.Name] == null)
				{
					JSVariableField variableField = globalScope.CreateField(ea.Name, null, FieldAttributes.SpecialName);
					globalScope.AddField(variableField);
				}
			};
			this.m_scanner.NewModule += delegate(object sender, NewModuleEventArgs ea)
			{
				this.m_newModule = true;
				this.m_foundEndOfLine = true;
			};
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0003190C File Offset: 0x0002FB0C
		private static AstNode CreateExpressionNode(Context operatorContext, AstNode operand1, AstNode operand2)
		{
			Context context = (operand1.IfNotNull((AstNode operand) => operand.Context) ?? operatorContext).CombineWith(operand2.IfNotNull((AstNode operand) => operand.Context));
			switch (operatorContext.Token)
			{
			case JSToken.FirstBinaryOperator:
			case JSToken.Minus:
			case JSToken.Multiply:
			case JSToken.Divide:
			case JSToken.Modulo:
			case JSToken.BitwiseAnd:
			case JSToken.BitwiseOr:
			case JSToken.BitwiseXor:
			case JSToken.LeftShift:
			case JSToken.RightShift:
			case JSToken.UnsignedRightShift:
			case JSToken.Equal:
			case JSToken.NotEqual:
			case JSToken.StrictEqual:
			case JSToken.StrictNotEqual:
			case JSToken.LessThan:
			case JSToken.LessThanEqual:
			case JSToken.GreaterThan:
			case JSToken.GreaterThanEqual:
			case JSToken.LogicalAnd:
			case JSToken.LogicalOr:
			case JSToken.InstanceOf:
			case JSToken.In:
			case JSToken.Assign:
			case JSToken.PlusAssign:
			case JSToken.MinusAssign:
			case JSToken.MultiplyAssign:
			case JSToken.DivideAssign:
			case JSToken.ModuloAssign:
			case JSToken.BitwiseAndAssign:
			case JSToken.BitwiseOrAssign:
			case JSToken.BitwiseXorAssign:
			case JSToken.LeftShiftAssign:
			case JSToken.RightShiftAssign:
			case JSToken.UnsignedRightShiftAssign:
				return new BinaryOperator(context)
				{
					Operand1 = operand1,
					Operand2 = operand2,
					OperatorContext = operatorContext,
					OperatorToken = operatorContext.Token
				};
			case JSToken.Comma:
				return CommaOperator.CombineWithComma(context, operand1, operand2);
			default:
				return null;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00031A40 File Offset: 0x0002FC40
		private bool ConvertNumericLiteralToDouble(string str, bool isInteger, out double doubleValue)
		{
			bool result;
			try
			{
				if (isInteger)
				{
					if (str[0] == '0' && str.Length > 1)
					{
						if (str[1] == 'x' || str[1] == 'X')
						{
							if (str.Length == 2)
							{
								doubleValue = 0.0;
								return false;
							}
							doubleValue = (double)Convert.ToInt64(str, 16);
							goto IL_136;
						}
						else if (str[1] == 'o' || str[1] == 'O')
						{
							if (str.Length == 2)
							{
								doubleValue = 0.0;
								return false;
							}
							doubleValue = (double)Convert.ToInt64(str.Substring(2), 8);
							goto IL_136;
						}
						else if (str[1] == 'b' || str[1] == 'B')
						{
							if (str.Length == 2)
							{
								doubleValue = 0.0;
								return false;
							}
							doubleValue = (double)Convert.ToInt64(str.Substring(2), 2);
							goto IL_136;
						}
						else
						{
							try
							{
								doubleValue = (double)Convert.ToInt64(str, 8);
								double num = (double)Convert.ToInt64(str, 10);
								if (num != doubleValue)
								{
									this.ReportError(JSError.OctalLiteralsDeprecated, null, false);
									return false;
								}
								goto IL_136;
							}
							catch (FormatException)
							{
								doubleValue = Convert.ToDouble(str, CultureInfo.InvariantCulture);
								goto IL_136;
							}
						}
					}
					doubleValue = Convert.ToDouble(str, CultureInfo.InvariantCulture);
					IL_136:
					if (doubleValue < -9007199254740992.0 || 9007199254740992.0 < doubleValue)
					{
						return false;
					}
				}
				else
				{
					doubleValue = Convert.ToDouble(str, CultureInfo.InvariantCulture);
				}
				result = true;
			}
			catch (OverflowException)
			{
				doubleValue = ((str[0] == '-') ? double.NegativeInfinity : double.PositiveInfinity);
				result = false;
			}
			catch (FormatException)
			{
				doubleValue = double.NaN;
				result = false;
			}
			return result;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00031C38 File Offset: 0x0002FE38
		private void AppendImportantComments(Block block)
		{
			if (block != null && this.m_importantComments.Count > 0 && this.m_settings.PreserveImportantComments && this.m_settings.IsModificationAllowed(TreeModifications.PreserveImportantComments))
			{
				foreach (Context context in this.m_importantComments)
				{
					block.Append(new ImportantComment(context));
				}
				this.m_importantComments.Clear();
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		private void GetNextToken()
		{
			this.m_currentToken = this.ScanNextToken();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00031D30 File Offset: 0x0002FF30
		private Context ScanNextToken()
		{
			this.EchoWriter.IfNotNull(delegate(TextWriter w)
			{
				if (this.m_currentToken.IsNot(JSToken.None))
				{
					w.Write(this.m_currentToken.Code);
				}
			});
			this.m_newModule = false;
			this.m_foundEndOfLine = false;
			this.m_importantComments.Clear();
			Context nextToken = this.m_scanner.ScanNextToken();
			while (nextToken.IsOne(new JSToken[]
			{
				JSToken.WhiteSpace,
				JSToken.EndOfLine,
				JSToken.SingleLineComment,
				JSToken.MultipleLineComment,
				JSToken.PreprocessorDirective,
				JSToken.Error
			}))
			{
				if (nextToken.Is(JSToken.EndOfLine))
				{
					this.m_foundEndOfLine = true;
				}
				else if (nextToken.IsOne(new JSToken[]
				{
					JSToken.MultipleLineComment,
					JSToken.SingleLineComment
				}) && nextToken.HasCode && ((nextToken.Code.Length > 2 && nextToken.Code[2] == '!') || nextToken.Code.IndexOf("@preserve", StringComparison.OrdinalIgnoreCase) >= 0 || nextToken.Code.IndexOf("@license", StringComparison.OrdinalIgnoreCase) >= 0))
				{
					this.m_importantComments.Add(nextToken.Clone());
				}
				this.EchoWriter.IfNotNull(delegate(TextWriter w)
				{
					if (!this.Settings.PreprocessOnly || nextToken.Token != JSToken.PreprocessorDirective)
					{
						w.Write(nextToken.Code);
					}
				});
				nextToken = this.m_scanner.ScanNextToken();
			}
			if (nextToken.Is(JSToken.EndOfFile))
			{
				this.m_foundEndOfLine = true;
			}
			return nextToken;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00031EC8 File Offset: 0x000300C8
		private JSToken PeekToken()
		{
			JSScanner jsscanner = this.m_scanner.Clone();
			jsscanner.SuppressErrors = true;
			Context context = jsscanner.ScanNextToken();
			while (context.IsOne(new JSToken[]
			{
				JSToken.WhiteSpace,
				JSToken.EndOfLine,
				JSToken.Error,
				JSToken.SingleLineComment,
				JSToken.MultipleLineComment,
				JSToken.PreprocessorDirective,
				JSToken.ConditionalCommentEnd,
				JSToken.ConditionalCommentStart,
				JSToken.ConditionalCompilationElse,
				JSToken.ConditionalCompilationElseIf,
				JSToken.ConditionalCompilationEnd,
				JSToken.ConditionalCompilationIf,
				JSToken.ConditionalCompilationOn,
				JSToken.ConditionalCompilationSet,
				JSToken.ConditionalCompilationVariable,
				JSToken.ConditionalIf
			}))
			{
				context = jsscanner.ScanNextToken();
			}
			return context.Token;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00031F68 File Offset: 0x00030168
		private bool PeekCanBeModule()
		{
			if (this.ParsedVersion == ScriptVersion.EcmaScript6 || this.m_settings.ScriptVersion == ScriptVersion.EcmaScript6)
			{
				return true;
			}
			JSScanner jsscanner = this.m_scanner.Clone();
			jsscanner.SuppressErrors = true;
			Context context = jsscanner.ScanNextToken();
			bool flag = false;
			while (context.IsOne(new JSToken[]
			{
				JSToken.WhiteSpace,
				JSToken.EndOfLine,
				JSToken.Error,
				JSToken.SingleLineComment,
				JSToken.MultipleLineComment,
				JSToken.PreprocessorDirective,
				JSToken.ConditionalCommentEnd,
				JSToken.ConditionalCommentStart,
				JSToken.ConditionalCompilationElse,
				JSToken.ConditionalCompilationElseIf,
				JSToken.ConditionalCompilationEnd,
				JSToken.ConditionalCompilationIf,
				JSToken.ConditionalCompilationOn,
				JSToken.ConditionalCompilationSet,
				JSToken.ConditionalCompilationVariable,
				JSToken.ConditionalIf
			}))
			{
				if (context.Is(JSToken.EndOfLine))
				{
					flag = true;
				}
				context = jsscanner.ScanNextToken();
			}
			return (context.Is(JSToken.StringLiteral) && !flag) || context.Is(JSToken.Identifier) || JSKeyword.CanBeIdentifier(context.Token) != null;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00032064 File Offset: 0x00030264
		private void ExpectSemicolon(AstNode node)
		{
			if (this.m_currentToken.Is(JSToken.Semicolon))
			{
				node.TerminatingContext = this.m_currentToken.Clone();
				this.GetNextToken();
				return;
			}
			if (!this.m_foundEndOfLine)
			{
				Context currentToken = this.m_currentToken;
				JSToken[] array = new JSToken[2];
				array[0] = JSToken.RightCurly;
				if (!currentToken.IsOne(array))
				{
					this.ReportError(JSError.NoSemicolon, node.Context.IfNotNull((Context c) => c.FlattenToEnd()), false);
					return;
				}
			}
			if (this.m_currentToken.IsNot(JSToken.RightCurly) && this.m_currentToken.IsNot(JSToken.EndOfFile))
			{
				this.ReportError(JSError.SemicolonInsertion, node.Context.IfNotNull((Context c) => c.FlattenToEnd()), false);
				return;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0003213D File Offset: 0x0003033D
		private void ReportError(JSError errorId, Context context = null, bool forceToError = false)
		{
			context = (context ?? this.m_currentToken.Clone());
			if (context.Token == JSToken.EndOfFile)
			{
				context.HandleError(errorId, true);
				return;
			}
			context.HandleError(errorId, forceToError);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0003216C File Offset: 0x0003036C
		private void CCTooComplicated(Context context)
		{
			(context ?? this.m_currentToken).HandleError(JSError.ConditionalCompilationTooComplex, false);
			while (this.m_currentToken.IsNot(JSToken.EndOfFile) && this.m_currentToken.IsNot(JSToken.ConditionalCommentEnd))
			{
				this.GetNextToken();
			}
			this.GetNextToken();
		}

		// Token: 0x040003D6 RID: 982
		private GlobalScope m_globalScope;

		// Token: 0x040003D7 RID: 983
		private JSScanner m_scanner;

		// Token: 0x040003D8 RID: 984
		private Context m_currentToken;

		// Token: 0x040003D9 RID: 985
		private bool m_newModule;

		// Token: 0x040003DA RID: 986
		private CodeSettings m_settings;

		// Token: 0x040003DB RID: 987
		private bool m_foundEndOfLine;

		// Token: 0x040003DC RID: 988
		private IList<Context> m_importantComments;

		// Token: 0x040003DD RID: 989
		private Dictionary<string, LabelInfo> m_labelInfo;

		// Token: 0x040003DE RID: 990
		private long[] m_timingPoints;
	}
}
