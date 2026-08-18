using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x020007FE RID: 2046
	internal abstract class BaseTemplateCodeDomTreeGenerator : BaseCodeDomTreeGenerator
	{
		// Token: 0x060061A4 RID: 24996 RVA: 0x00152602 File Offset: 0x00150802
		internal BaseTemplateCodeDomTreeGenerator(TemplateParser parser) : base(parser)
		{
			this._parser = parser;
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x00152612 File Offset: 0x00150812
		private TemplateParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x0015261C File Offset: 0x0015081C
		private CodeStatement GetOutputWriteStatement(CodeExpression expr, bool encode)
		{
			if (encode)
			{
				expr = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(HttpUtility)), "HtmlEncode"), new CodeExpression[]
				{
					expr
				});
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			CodeExpressionStatement result = new CodeExpressionStatement(codeMethodInvokeExpression);
			codeMethodInvokeExpression.Method.TargetObject = new CodeArgumentReferenceExpression("__w");
			codeMethodInvokeExpression.Method.MethodName = "Write";
			codeMethodInvokeExpression.Parameters.Add(expr);
			return result;
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x00152698 File Offset: 0x00150898
		private void AddOutputWriteStatement(CodeStatementCollection methodStatements, CodeExpression expr, CodeLinePragma linePragma)
		{
			CodeStatement outputWriteStatement = this.GetOutputWriteStatement(expr, false);
			if (linePragma != null)
			{
				outputWriteStatement.LinePragma = linePragma;
			}
			methodStatements.Add(outputWriteStatement);
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x001526C0 File Offset: 0x001508C0
		private void AddOutputWriteStringStatement(CodeStatementCollection methodStatements, string s)
		{
			if (!this.UseResourceLiteralString(s))
			{
				this.AddOutputWriteStatement(methodStatements, new CodePrimitiveExpression(s), null);
				return;
			}
			int num;
			int num2;
			bool flag;
			this._stringResourceBuilder.AddString(s, out num, out num2, out flag);
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			CodeExpressionStatement value = new CodeExpressionStatement(codeMethodInvokeExpression);
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "WriteUTF8ResourceString";
			codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("__w"));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(num));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(num2));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(flag));
			methodStatements.Add(value);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x0015278C File Offset: 0x0015098C
		private static void BuildAddParsedSubObjectStatement(CodeStatementCollection statements, CodeExpression ctrlToAdd, CodeLinePragma linePragma, CodeExpression ctrlRefExpr, ref bool gotParserVariable)
		{
			if (!gotParserVariable)
			{
				statements.Add(new CodeVariableDeclarationStatement
				{
					Name = "__parser",
					Type = new CodeTypeReference(typeof(IParserAccessor)),
					InitExpression = new CodeCastExpression(typeof(IParserAccessor), ctrlRefExpr)
				});
				gotParserVariable = true;
			}
			statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("__parser"), "AddParsedSubObject", new CodeExpression[0])
			{
				Parameters = 
				{
					ctrlToAdd
				}
			})
			{
				LinePragma = linePragma
			});
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00152824 File Offset: 0x00150A24
		internal virtual CodeExpression BuildPagePropertyReferenceExpression()
		{
			return new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.pagePropertyName);
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x00152838 File Offset: 0x00150A38
		protected CodeMemberMethod BuildBuildMethod(ControlBuilder builder, bool fTemplate, bool fInTemplate, bool topLevelControlInTemplate, PropertyEntry pse, bool fControlSkin)
		{
			ServiceContainer serviceContainer = new ServiceContainer();
			serviceContainer.AddService(typeof(IFilterResolutionService), HttpCapabilitiesBase.EmptyHttpCapabilitiesBase);
			try
			{
				builder.SetServiceProvider(serviceContainer);
				builder.EnsureEntriesSorted();
			}
			finally
			{
				builder.SetServiceProvider(null);
			}
			string methodNameForBuilder = this.GetMethodNameForBuilder(BaseTemplateCodeDomTreeGenerator.buildMethodPrefix, builder);
			Type ctrlTypeForBuilder = this.GetCtrlTypeForBuilder(builder, fTemplate);
			bool flag = false;
			bool fControlFieldDeclared = false;
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
			codeMemberMethod.Name = methodNameForBuilder;
			codeMemberMethod.Attributes = (MemberAttributes)20482;
			this._sourceDataClass.Members.Add(codeMemberMethod);
			ComplexPropertyEntry complexPropertyEntry = pse as ComplexPropertyEntry;
			if (fTemplate || (complexPropertyEntry != null && complexPropertyEntry.ReadOnly))
			{
				if (builder is RootBuilder)
				{
					codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(this._sourceDataClass.Name, "__ctrl"));
				}
				else
				{
					codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(ctrlTypeForBuilder, "__ctrl"));
				}
			}
			else
			{
				if (typeof(Control).IsAssignableFrom(builder.ControlType))
				{
					flag = true;
				}
				if (builder.ControlType != null)
				{
					if (fControlSkin)
					{
						if (flag)
						{
							codeMemberMethod.ReturnType = new CodeTypeReference(typeof(Control));
						}
					}
					else
					{
						PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)TypeDescriptor.GetAttributes(builder.ControlType)[typeof(PartialCachingAttribute)];
						if (partialCachingAttribute != null)
						{
							codeMemberMethod.ReturnType = new CodeTypeReference(typeof(Control));
						}
						else
						{
							codeMemberMethod.ReturnType = CodeDomUtility.BuildGlobalCodeTypeReference(builder.ControlType);
						}
					}
				}
				fControlFieldDeclared = true;
			}
			if (fControlSkin)
			{
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Control).FullName, "ctrl"));
			}
			this.BuildBuildMethodInternal(builder, builder.ControlType, fInTemplate, topLevelControlInTemplate, pse, codeMemberMethod.Statements, flag, fControlFieldDeclared, null, fControlSkin);
			return codeMemberMethod;
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x00152A20 File Offset: 0x00150C20
		private void BuildBuildMethodInternal(ControlBuilder builder, Type ctrlType, bool fInTemplate, bool topLevelControlInTemplate, PropertyEntry pse, CodeStatementCollection statements, bool fStandardControl, bool fControlFieldDeclared, string deviceFilter, bool fControlSkin)
		{
			CodeLinePragma linePragma = base.CreateCodeLinePragma(builder);
			CodeExpression codeExpression;
			if (fControlSkin)
			{
				CodeCastExpression initExpression = new CodeCastExpression(builder.ControlType.FullName, new CodeArgumentReferenceExpression("ctrl"));
				statements.Add(new CodeVariableDeclarationStatement(builder.ControlType.FullName, "__ctrl", initExpression));
				codeExpression = new CodeVariableReferenceExpression("__ctrl");
			}
			else if (!fControlFieldDeclared)
			{
				codeExpression = new CodeArgumentReferenceExpression("__ctrl");
			}
			else
			{
				CodeTypeReference codeTypeReference = CodeDomUtility.BuildGlobalCodeTypeReference(ctrlType);
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(codeTypeReference, new CodeExpression[0]);
				ConstructorNeedsTagAttribute constructorNeedsTagAttribute = (ConstructorNeedsTagAttribute)TypeDescriptor.GetAttributes(ctrlType)[typeof(ConstructorNeedsTagAttribute)];
				if (constructorNeedsTagAttribute != null && constructorNeedsTagAttribute.NeedsTag)
				{
					codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(builder.TagName));
				}
				DataBoundLiteralControlBuilder dataBoundLiteralControlBuilder = builder as DataBoundLiteralControlBuilder;
				if (dataBoundLiteralControlBuilder != null)
				{
					codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(dataBoundLiteralControlBuilder.GetStaticLiteralsCount()));
					codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(dataBoundLiteralControlBuilder.GetDataBoundLiteralCount()));
				}
				statements.Add(new CodeVariableDeclarationStatement(codeTypeReference, "__ctrl"));
				codeExpression = new CodeVariableReferenceExpression("__ctrl");
				statements.Add(new CodeAssignStatement(codeExpression, codeObjectCreateExpression)
				{
					LinePragma = linePragma
				});
				if (!builder.IsGeneratedID)
				{
					CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), builder.ID);
					CodeAssignStatement value = new CodeAssignStatement(left, codeExpression);
					statements.Add(value);
				}
				if (topLevelControlInTemplate && !typeof(TemplateControl).IsAssignableFrom(ctrlType))
				{
					statements.Add(this.BuildTemplatePropertyStatement(codeExpression));
				}
				if (fStandardControl)
				{
					if (builder.SkinID != null)
					{
						statements.Add(new CodeAssignStatement
						{
							Left = new CodePropertyReferenceExpression(codeExpression, "SkinID"),
							Right = new CodePrimitiveExpression(builder.SkinID)
						});
					}
					if (ThemeableAttribute.IsTypeThemeable(ctrlType))
					{
						statements.Add(new CodeMethodInvokeExpression(codeExpression, BaseTemplateCodeDomTreeGenerator.applyStyleSheetMethodName, new CodeExpression[0])
						{
							Parameters = 
							{
								this.BuildPagePropertyReferenceExpression()
							}
						});
					}
				}
			}
			if (builder.TemplatePropertyEntries.Count > 0)
			{
				CodeStatementCollection codeStatementCollection = statements;
				PropertyEntry propertyEntry = null;
				foreach (object obj in builder.TemplatePropertyEntries)
				{
					TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj;
					CodeStatementCollection codeStatementCollection2 = codeStatementCollection;
					this.HandleDeviceFilterConditional(ref propertyEntry, templatePropertyEntry, statements, ref codeStatementCollection2, out codeStatementCollection);
					string id = templatePropertyEntry.Builder.ID;
					CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression();
					codeDelegateCreateExpression.DelegateType = new CodeTypeReference(typeof(BuildTemplateMethod));
					codeDelegateCreateExpression.TargetObject = new CodeThisReferenceExpression();
					codeDelegateCreateExpression.MethodName = BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + id;
					CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
					if (templatePropertyEntry.PropertyInfo != null)
					{
						codeAssignStatement.Left = new CodePropertyReferenceExpression(codeExpression, templatePropertyEntry.Name);
					}
					else
					{
						codeAssignStatement.Left = new CodeFieldReferenceExpression(codeExpression, templatePropertyEntry.Name);
					}
					CodeObjectCreateExpression codeObjectCreateExpression;
					if (templatePropertyEntry.BindableTemplate)
					{
						CodeExpression codeExpression2;
						if (templatePropertyEntry.Builder.HasTwoWayBoundProperties)
						{
							codeExpression2 = new CodeDelegateCreateExpression();
							((CodeDelegateCreateExpression)codeExpression2).DelegateType = new CodeTypeReference(typeof(ExtractTemplateValuesMethod));
							((CodeDelegateCreateExpression)codeExpression2).TargetObject = new CodeThisReferenceExpression();
							((CodeDelegateCreateExpression)codeExpression2).MethodName = BaseTemplateCodeDomTreeGenerator.extractTemplateValuesMethodPrefix + id;
						}
						else
						{
							codeExpression2 = new CodePrimitiveExpression(null);
						}
						codeObjectCreateExpression = new CodeObjectCreateExpression(typeof(CompiledBindableTemplateBuilder), new CodeExpression[0]);
						codeObjectCreateExpression.Parameters.Add(codeDelegateCreateExpression);
						codeObjectCreateExpression.Parameters.Add(codeExpression2);
					}
					else
					{
						codeObjectCreateExpression = new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder), new CodeExpression[0]);
						codeObjectCreateExpression.Parameters.Add(codeDelegateCreateExpression);
					}
					codeAssignStatement.Right = codeObjectCreateExpression;
					codeAssignStatement.LinePragma = base.CreateCodeLinePragma(templatePropertyEntry.Builder);
					codeStatementCollection2.Add(codeAssignStatement);
				}
			}
			if (typeof(UserControl).IsAssignableFrom(ctrlType) && fControlFieldDeclared && !fControlSkin)
			{
				statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(codeExpression, "InitializeAsUserControl", new CodeExpression[0])
				{
					Parameters = 
					{
						new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.pagePropertyName)
					}
				})
				{
					LinePragma = linePragma
				});
			}
			if (builder.SimplePropertyEntries.Count > 0)
			{
				CodeStatementCollection codeStatementCollection3 = statements;
				PropertyEntry propertyEntry2 = null;
				foreach (object obj2 in builder.SimplePropertyEntries)
				{
					SimplePropertyEntry simplePropertyEntry = (SimplePropertyEntry)obj2;
					CodeStatementCollection codeStatementCollection4 = codeStatementCollection3;
					this.HandleDeviceFilterConditional(ref propertyEntry2, simplePropertyEntry, statements, ref codeStatementCollection4, out codeStatementCollection3);
					CodeStatement codeStatement = simplePropertyEntry.GetCodeStatement(this, codeExpression);
					codeStatement.LinePragma = linePragma;
					codeStatementCollection4.Add(codeStatement);
				}
			}
			if (typeof(Page).IsAssignableFrom(ctrlType) && !fControlSkin)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InitializeCulture", new CodeExpression[0]);
				statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression)
				{
					LinePragma = linePragma
				});
			}
			CodeStatementCollection codeStatementCollection5 = statements;
			if (builder is ContentPlaceHolderBuilder)
			{
				string name = ((ContentPlaceHolderBuilder)builder).Name;
				string text = MasterPageControlBuilder.AutoTemplatePrefix + name;
				string fieldName = "__" + text;
				Type type = builder.BindingContainerType;
				if (!typeof(INamingContainer).IsAssignableFrom(type))
				{
					if (typeof(INamingContainer).IsAssignableFrom(this.Parser.BaseType))
					{
						type = this.Parser.BaseType;
					}
					else
					{
						type = typeof(Control);
					}
				}
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
				CodeAttributeDeclaration value2 = new CodeAttributeDeclaration("TemplateContainer", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodeTypeOfExpression(type))
				});
				codeAttributeDeclarationCollection.Add(value2);
				if (!fInTemplate)
				{
					CodeAttributeDeclaration value3 = new CodeAttributeDeclaration("TemplateInstanceAttribute", new CodeAttributeArgument[]
					{
						new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(TemplateInstance)), "Single"))
					});
					codeAttributeDeclarationCollection.Add(value3);
				}
				base.BuildFieldAndAccessorProperty(text, fieldName, typeof(ITemplate), false, codeAttributeDeclarationCollection);
				CodeExpression codeExpression3 = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
				if (builder is ContentPlaceHolderBuilder)
				{
					CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "ContentTemplates");
					CodeAssignStatement codeAssignStatement2 = new CodeAssignStatement();
					codeAssignStatement2.Left = codeExpression3;
					codeAssignStatement2.Right = new CodeCastExpression(typeof(ITemplate), new CodeIndexerExpression(codePropertyReferenceExpression, new CodeExpression[]
					{
						new CodePrimitiveExpression(name)
					}));
					CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
					CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(codePropertyReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
					CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(codePropertyReferenceExpression, "Remove", new CodeExpression[0]);
					codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(name));
					codeConditionStatement.Condition = condition;
					codeConditionStatement.TrueStatements.Add(codeAssignStatement2);
					statements.Add(codeConditionStatement);
				}
				CodeMethodInvokeExpression codeMethodInvokeExpression3;
				if (MultiTargetingUtil.IsTargetFramework40OrAbove)
				{
					codeMethodInvokeExpression3 = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InstantiateInContentPlaceHolder", new CodeExpression[0]);
					codeMethodInvokeExpression3.Parameters.Add(codeExpression);
					codeMethodInvokeExpression3.Parameters.Add(codeExpression3);
				}
				else
				{
					codeMethodInvokeExpression3 = new CodeMethodInvokeExpression(codeExpression3, "InstantiateIn", new CodeExpression[0]);
					codeMethodInvokeExpression3.Parameters.Add(codeExpression);
				}
				CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
				codeConditionStatement2.Condition = new CodeBinaryOperatorExpression(codeExpression3, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				codeConditionStatement2.TrueStatements.Add(new CodeExpressionStatement(codeMethodInvokeExpression3));
				codeStatementCollection5 = codeConditionStatement2.FalseStatements;
				statements.Add(codeConditionStatement2);
			}
			if (builder is FileLevelPageControlBuilder)
			{
				ICollection contentBuilderEntries = ((FileLevelPageControlBuilder)builder).ContentBuilderEntries;
				if (contentBuilderEntries != null)
				{
					CodeStatementCollection codeStatementCollection6 = statements;
					PropertyEntry propertyEntry3 = null;
					foreach (object obj3 in contentBuilderEntries)
					{
						TemplatePropertyEntry templatePropertyEntry2 = (TemplatePropertyEntry)obj3;
						ContentBuilderInternal contentBuilderInternal = (ContentBuilderInternal)templatePropertyEntry2.Builder;
						CodeStatementCollection codeStatementCollection7 = codeStatementCollection6;
						this.HandleDeviceFilterConditional(ref propertyEntry3, templatePropertyEntry2, statements, ref codeStatementCollection7, out codeStatementCollection6);
						string id2 = contentBuilderInternal.ID;
						string contentPlaceHolder = contentBuilderInternal.ContentPlaceHolder;
						CodeDelegateCreateExpression codeDelegateCreateExpression2 = new CodeDelegateCreateExpression();
						codeDelegateCreateExpression2.DelegateType = new CodeTypeReference(typeof(BuildTemplateMethod));
						codeDelegateCreateExpression2.TargetObject = new CodeThisReferenceExpression();
						codeDelegateCreateExpression2.MethodName = BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + id2;
						CodeObjectCreateExpression codeObjectCreateExpression2 = new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder), new CodeExpression[0]);
						codeObjectCreateExpression2.Parameters.Add(codeDelegateCreateExpression2);
						CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "AddContentTemplate", new CodeExpression[0])
						{
							Parameters = 
							{
								new CodePrimitiveExpression(contentPlaceHolder),
								codeObjectCreateExpression2
							}
						});
						codeExpressionStatement.LinePragma = base.CreateCodeLinePragma(contentBuilderInternal);
						codeStatementCollection7.Add(codeExpressionStatement);
					}
				}
			}
			if (builder is DataBoundLiteralControlBuilder)
			{
				int num = -1;
				using (IEnumerator enumerator4 = builder.SubBuilders.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						object obj4 = enumerator4.Current;
						num++;
						if (obj4 != null && num % 2 != 1)
						{
							string value4 = (string)obj4;
							statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(codeExpression, "SetStaticString", new CodeExpression[0])
							{
								Parameters = 
								{
									new CodePrimitiveExpression(num / 2),
									new CodePrimitiveExpression(value4)
								}
							}));
						}
					}
					goto IL_E00;
				}
			}
			if (builder.SubBuilders != null)
			{
				bool flag = false;
				int num2 = 1;
				foreach (object obj5 in builder.SubBuilders)
				{
					if (obj5 is ControlBuilder && !(obj5 is CodeBlockBuilder) && !(obj5 is CodeStatementBuilder) && !(obj5 is ContentBuilderInternal))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj5;
						if (fControlSkin)
						{
							throw new HttpParseException(SR.GetString("ControlSkin_cannot_contain_controls"), null, builder.VirtualPath, null, builder.Line);
						}
						PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)TypeDescriptor.GetAttributes(controlBuilder.ControlType)[typeof(PartialCachingAttribute)];
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + controlBuilder.ID, new CodeExpression[0]);
						CodeExpressionStatement codeExpressionStatement2 = new CodeExpressionStatement(codeMethodInvokeExpression);
						if (partialCachingAttribute == null)
						{
							string text2 = "__ctrl" + num2++.ToString(CultureInfo.InvariantCulture);
							CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression(text2);
							CodeTypeReference type2 = CodeDomUtility.BuildGlobalCodeTypeReference(controlBuilder.ControlType);
							codeStatementCollection5.Add(new CodeVariableDeclarationStatement(type2, text2));
							codeStatementCollection5.Add(new CodeAssignStatement(codeVariableReferenceExpression, codeMethodInvokeExpression)
							{
								LinePragma = linePragma
							});
							BaseTemplateCodeDomTreeGenerator.BuildAddParsedSubObjectStatement(codeStatementCollection5, codeVariableReferenceExpression, linePragma, codeExpression, ref flag);
						}
						else
						{
							string text3 = null;
							bool isTargetFramework40OrAbove = MultiTargetingUtil.IsTargetFramework40OrAbove;
							if (isTargetFramework40OrAbove)
							{
								text3 = partialCachingAttribute.ProviderName;
								if (text3 == "AspNetInternalProvider")
								{
									text3 = null;
								}
							}
							CodeMethodInvokeExpression codeMethodInvokeExpression4 = new CodeMethodInvokeExpression();
							codeMethodInvokeExpression4.Method.TargetObject = new CodeTypeReferenceExpression(typeof(StaticPartialCachingControl));
							codeMethodInvokeExpression4.Method.MethodName = "BuildCachedControl";
							codeMethodInvokeExpression4.Parameters.Add(codeExpression);
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(controlBuilder.ID));
							if (partialCachingAttribute.Shared)
							{
								codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(controlBuilder.ControlType.GetHashCode().ToString(CultureInfo.InvariantCulture)));
							}
							else
							{
								codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(Guid.NewGuid().ToString()));
							}
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.Duration));
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByParams));
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByControls));
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByCustom));
							codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.SqlDependency));
							CodeDelegateCreateExpression codeDelegateCreateExpression3 = new CodeDelegateCreateExpression();
							codeDelegateCreateExpression3.DelegateType = new CodeTypeReference(typeof(BuildMethod));
							codeDelegateCreateExpression3.TargetObject = new CodeThisReferenceExpression();
							codeDelegateCreateExpression3.MethodName = BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + controlBuilder.ID;
							codeMethodInvokeExpression4.Parameters.Add(codeDelegateCreateExpression3);
							if (isTargetFramework40OrAbove)
							{
								codeMethodInvokeExpression4.Parameters.Add(new CodePrimitiveExpression(text3));
							}
							codeStatementCollection5.Add(new CodeExpressionStatement(codeMethodInvokeExpression4));
						}
					}
					else if (obj5 is string && !builder.HasAspCode && (!fControlSkin || !builder.AllowWhitespaceLiterals()))
					{
						string text4 = (string)obj5;
						CodeExpression ctrlToAdd;
						if (!this.UseResourceLiteralString(text4))
						{
							ctrlToAdd = new CodeObjectCreateExpression(typeof(LiteralControl), new CodeExpression[0])
							{
								Parameters = 
								{
									new CodePrimitiveExpression(text4)
								}
							};
						}
						else
						{
							int num3;
							int num4;
							bool flag2;
							this._stringResourceBuilder.AddString(text4, out num3, out num4, out flag2);
							ctrlToAdd = new CodeMethodInvokeExpression
							{
								Method = 
								{
									TargetObject = new CodeThisReferenceExpression(),
									MethodName = "CreateResourceBasedLiteralControl"
								},
								Parameters = 
								{
									new CodePrimitiveExpression(num3),
									new CodePrimitiveExpression(num4),
									new CodePrimitiveExpression(flag2)
								}
							};
						}
						BaseTemplateCodeDomTreeGenerator.BuildAddParsedSubObjectStatement(codeStatementCollection5, ctrlToAdd, linePragma, codeExpression, ref flag);
					}
				}
			}
			IL_E00:
			if (builder.ComplexPropertyEntries.Count > 0)
			{
				CodeStatementCollection codeStatementCollection8 = statements;
				PropertyEntry propertyEntry4 = null;
				int num5 = 1;
				foreach (object obj6 in builder.ComplexPropertyEntries)
				{
					ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj6;
					CodeStatementCollection codeStatementCollection9 = codeStatementCollection8;
					this.HandleDeviceFilterConditional(ref propertyEntry4, complexPropertyEntry, statements, ref codeStatementCollection9, out codeStatementCollection8);
					if (complexPropertyEntry.Builder is StringPropertyBuilder)
					{
						CodeExpression left2 = new CodePropertyReferenceExpression(codeExpression, complexPropertyEntry.Name);
						CodeExpression right = this.BuildStringPropertyExpression(complexPropertyEntry);
						CodeAssignStatement codeAssignStatement3 = new CodeAssignStatement(left2, right);
						codeAssignStatement3.LinePragma = linePragma;
						codeStatementCollection9.Add(codeAssignStatement3);
					}
					else if (complexPropertyEntry.ReadOnly)
					{
						if (fControlSkin && complexPropertyEntry.Builder != null && complexPropertyEntry.Builder is CollectionBuilder && complexPropertyEntry.Builder.ComplexPropertyEntries.Count > 0)
						{
							BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
							if (complexPropertyEntry.Type.GetMethod("Clear", bindingAttr) != null)
							{
								CodeMethodReferenceExpression codeMethodReferenceExpression = new CodeMethodReferenceExpression();
								codeMethodReferenceExpression.MethodName = "Clear";
								codeMethodReferenceExpression.TargetObject = new CodePropertyReferenceExpression(codeExpression, complexPropertyEntry.Name);
								CodeMethodInvokeExpression codeMethodInvokeExpression5 = new CodeMethodInvokeExpression();
								codeMethodInvokeExpression5.Method = codeMethodReferenceExpression;
								codeStatementCollection9.Add(codeMethodInvokeExpression5);
							}
						}
						CodeExpressionStatement codeExpressionStatement2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + complexPropertyEntry.Builder.ID, new CodeExpression[0])
						{
							Parameters = 
							{
								new CodePropertyReferenceExpression(codeExpression, complexPropertyEntry.Name)
							}
						});
						codeExpressionStatement2.LinePragma = linePragma;
						codeStatementCollection9.Add(codeExpressionStatement2);
					}
					else
					{
						string text5 = "__ctrl" + num5++.ToString(CultureInfo.InvariantCulture);
						CodeTypeReference type3 = CodeDomUtility.BuildGlobalCodeTypeReference(complexPropertyEntry.Builder.ControlType);
						codeStatementCollection9.Add(new CodeVariableDeclarationStatement(type3, text5));
						CodeVariableReferenceExpression codeVariableReferenceExpression2 = new CodeVariableReferenceExpression(text5);
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.buildMethodPrefix + complexPropertyEntry.Builder.ID, new CodeExpression[0]);
						CodeExpressionStatement codeExpressionStatement2 = new CodeExpressionStatement(codeMethodInvokeExpression);
						CodeAssignStatement codeAssignStatement4 = new CodeAssignStatement(codeVariableReferenceExpression2, codeMethodInvokeExpression);
						codeAssignStatement4.LinePragma = linePragma;
						codeStatementCollection9.Add(codeAssignStatement4);
						if (complexPropertyEntry.IsCollectionItem)
						{
							codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeExpression, "Add", new CodeExpression[0]);
							codeExpressionStatement2 = new CodeExpressionStatement(codeMethodInvokeExpression);
							codeExpressionStatement2.LinePragma = linePragma;
							codeStatementCollection9.Add(codeExpressionStatement2);
							codeMethodInvokeExpression.Parameters.Add(codeVariableReferenceExpression2);
						}
						else
						{
							CodeAssignStatement codeAssignStatement5 = new CodeAssignStatement();
							codeAssignStatement5.Left = new CodePropertyReferenceExpression(codeExpression, complexPropertyEntry.Name);
							codeAssignStatement5.Right = codeVariableReferenceExpression2;
							codeAssignStatement5.LinePragma = linePragma;
							codeStatementCollection9.Add(codeAssignStatement5);
						}
					}
				}
			}
			if (builder.BoundPropertyEntries.Count > 0)
			{
				bool flag3 = builder is BindableTemplateBuilder;
				bool flag4 = false;
				CodeStatementCollection codeStatementCollection10 = statements;
				PropertyEntry propertyEntry5 = null;
				bool flag5 = false;
				foreach (object obj7 in builder.BoundPropertyEntries)
				{
					BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj7;
					if (!boundPropertyEntry.TwoWayBound || (!flag3 && !boundPropertyEntry.ReadOnlyProperty))
					{
						if (boundPropertyEntry.IsDataBindingEntry)
						{
							flag4 = true;
						}
						else
						{
							CodeStatementCollection statements2 = codeStatementCollection10;
							this.HandleDeviceFilterConditional(ref propertyEntry5, boundPropertyEntry, statements, ref statements2, out codeStatementCollection10);
							ExpressionBuilder expressionBuilder = boundPropertyEntry.ExpressionBuilder;
							expressionBuilder.BuildExpression(boundPropertyEntry, builder, codeExpression, statements, statements2, null, ref flag5);
						}
					}
				}
				if (flag4)
				{
					EventInfo @event = DataBindingExpressionBuilder.Event;
					CodeDelegateCreateExpression codeDelegateCreateExpression4 = new CodeDelegateCreateExpression();
					CodeAttachEventStatement codeAttachEventStatement = new CodeAttachEventStatement(codeExpression, @event.Name, codeDelegateCreateExpression4);
					codeAttachEventStatement.LinePragma = linePragma;
					codeDelegateCreateExpression4.DelegateType = new CodeTypeReference(typeof(EventHandler));
					codeDelegateCreateExpression4.TargetObject = new CodeThisReferenceExpression();
					codeDelegateCreateExpression4.MethodName = this.GetExpressionBuilderMethodName(@event.Name, builder);
					statements.Add(codeAttachEventStatement);
				}
			}
			if (builder is DataBoundLiteralControlBuilder)
			{
				CodeDelegateCreateExpression codeDelegateCreateExpression5 = new CodeDelegateCreateExpression();
				CodeAttachEventStatement codeAttachEventStatement2 = new CodeAttachEventStatement(codeExpression, "DataBinding", codeDelegateCreateExpression5);
				codeAttachEventStatement2.LinePragma = linePragma;
				codeDelegateCreateExpression5.DelegateType = new CodeTypeReference(typeof(EventHandler));
				codeDelegateCreateExpression5.TargetObject = new CodeThisReferenceExpression();
				codeDelegateCreateExpression5.MethodName = this.BindingMethodName(builder);
				statements.Add(codeAttachEventStatement2);
			}
			if (builder.HasAspCode && !fControlSkin)
			{
				CodeDelegateCreateExpression codeDelegateCreateExpression6 = new CodeDelegateCreateExpression();
				codeDelegateCreateExpression6.DelegateType = new CodeTypeReference(typeof(RenderMethod));
				codeDelegateCreateExpression6.TargetObject = new CodeThisReferenceExpression();
				codeDelegateCreateExpression6.MethodName = "__Render" + builder.ID;
				CodeExpressionStatement codeExpressionStatement2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(codeExpression, "SetRenderMethodDelegate", new CodeExpression[0])
				{
					Parameters = 
					{
						codeDelegateCreateExpression6
					}
				});
				if (builder is ContentPlaceHolderBuilder)
				{
					string name2 = ((ContentPlaceHolderBuilder)builder).Name;
					string text = MasterPageControlBuilder.AutoTemplatePrefix + name2;
					string fieldName2 = "__" + text;
					CodeExpression left3 = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName2);
					statements.Add(new CodeConditionStatement
					{
						Condition = new CodeBinaryOperatorExpression(left3, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)),
						TrueStatements = 
						{
							codeExpressionStatement2
						}
					});
				}
				else
				{
					statements.Add(codeExpressionStatement2);
				}
			}
			if (builder.EventEntries.Count > 0)
			{
				foreach (object obj8 in builder.EventEntries)
				{
					EventEntry eventEntry = (EventEntry)obj8;
					CodeDelegateCreateExpression codeDelegateCreateExpression7 = new CodeDelegateCreateExpression();
					codeDelegateCreateExpression7.DelegateType = new CodeTypeReference(eventEntry.HandlerType);
					codeDelegateCreateExpression7.TargetObject = new CodeThisReferenceExpression();
					codeDelegateCreateExpression7.MethodName = eventEntry.HandlerMethodName;
					if (this.Parser.HasCodeBehind)
					{
						statements.Add(new CodeRemoveEventStatement(codeExpression, eventEntry.Name, codeDelegateCreateExpression7)
						{
							LinePragma = linePragma
						});
					}
					statements.Add(new CodeAttachEventStatement(codeExpression, eventEntry.Name, codeDelegateCreateExpression7)
					{
						LinePragma = linePragma
					});
				}
			}
			if (fControlFieldDeclared)
			{
				statements.Add(new CodeMethodReturnStatement(codeExpression));
			}
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x00153F38 File Offset: 0x00152138
		protected void BuildExtractMethod(ControlBuilder builder)
		{
			BindableTemplateBuilder bindableTemplateBuilder = builder as BindableTemplateBuilder;
			if (bindableTemplateBuilder != null && bindableTemplateBuilder.HasTwoWayBoundProperties)
			{
				string name = this.ExtractMethodName(builder);
				CodeLinePragma linePragma = base.CreateCodeLinePragma(builder);
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				base.AddDebuggerNonUserCodeAttribute(codeMemberMethod);
				codeMemberMethod.Name = name;
				codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
				codeMemberMethod.Attributes |= MemberAttributes.Public;
				codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IOrderedDictionary));
				this._sourceDataClass.Members.Add(codeMemberMethod);
				CodeStatementCollection statements = codeMemberMethod.Statements;
				CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Control), "__container"));
				CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement(typeof(OrderedDictionary), "__table");
				statements.Add(value);
				CodeObjectCreateExpression right = new CodeObjectCreateExpression(typeof(OrderedDictionary), new CodeExpression[0]);
				codeStatementCollection.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("__table"), right)
				{
					LinePragma = linePragma
				});
				this.BuildExtractStatementsRecursive(bindableTemplateBuilder.SubBuilders, codeStatementCollection, statements, linePragma, "__table", "__container");
				CodeMethodReturnStatement value2 = new CodeMethodReturnStatement(new CodeVariableReferenceExpression("__table"));
				codeStatementCollection.Add(value2);
				codeMemberMethod.Statements.AddRange(codeStatementCollection);
			}
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x0015409C File Offset: 0x0015229C
		private void BuildExtractStatementsRecursive(ArrayList subBuilders, CodeStatementCollection statements, CodeStatementCollection topLevelStatements, CodeLinePragma linePragma, string tableVarName, string containerVarName)
		{
			foreach (object obj in subBuilders)
			{
				ControlBuilder controlBuilder = obj as ControlBuilder;
				if (controlBuilder != null)
				{
					CodeStatementCollection codeStatementCollection = null;
					CodeStatementCollection codeStatementCollection2 = statements;
					PropertyEntry propertyEntry = null;
					string strA = null;
					foreach (object obj2 in controlBuilder.BoundPropertyEntries)
					{
						BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj2;
						if (boundPropertyEntry.TwoWayBound)
						{
							bool flag;
							if (string.Compare(strA, boundPropertyEntry.ControlID, StringComparison.Ordinal) != 0)
							{
								propertyEntry = null;
								flag = true;
							}
							else
							{
								flag = false;
							}
							strA = boundPropertyEntry.ControlID;
							codeStatementCollection = codeStatementCollection2;
							this.HandleDeviceFilterConditional(ref propertyEntry, boundPropertyEntry, statements, ref codeStatementCollection, out codeStatementCollection2);
							if (flag)
							{
								CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement(boundPropertyEntry.ControlType, boundPropertyEntry.ControlID);
								topLevelStatements.Add(value);
								CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(containerVarName), "FindControl", new CodeExpression[0]);
								string controlID = boundPropertyEntry.ControlID;
								codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(controlID));
								CodeCastExpression right = new CodeCastExpression(boundPropertyEntry.ControlType, codeMethodInvokeExpression);
								topLevelStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(boundPropertyEntry.ControlID), right)
								{
									LinePragma = linePragma
								});
							}
							CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
							codeConditionStatement.Condition = new CodeBinaryOperatorExpression
							{
								Operator = CodeBinaryOperatorType.IdentityInequality,
								Left = new CodeVariableReferenceExpression(boundPropertyEntry.ControlID),
								Right = new CodePrimitiveExpression(null)
							};
							string fieldName = boundPropertyEntry.FieldName;
							CodeIndexerExpression left = new CodeIndexerExpression(new CodeVariableReferenceExpression(tableVarName), new CodeExpression[]
							{
								new CodePrimitiveExpression(fieldName)
							});
							CodeExpression codeExpression = CodeDomUtility.BuildPropertyReferenceExpression(new CodeVariableReferenceExpression(boundPropertyEntry.ControlID), boundPropertyEntry.Name);
							if (this._usingVJSCompiler)
							{
								codeExpression = CodeDomUtility.BuildJSharpCastExpression(boundPropertyEntry.Type, codeExpression);
							}
							CodeAssignStatement value2 = new CodeAssignStatement(left, codeExpression);
							codeConditionStatement.TrueStatements.Add(value2);
							codeConditionStatement.LinePragma = linePragma;
							codeStatementCollection.Add(codeConditionStatement);
						}
					}
					if (controlBuilder.SubBuilders.Count > 0)
					{
						this.BuildExtractStatementsRecursive(controlBuilder.SubBuilders, statements, topLevelStatements, linePragma, tableVarName, containerVarName);
					}
					ArrayList arrayList = new ArrayList();
					this.AddEntryBuildersToList(controlBuilder.ComplexPropertyEntries, arrayList);
					this.AddEntryBuildersToList(controlBuilder.TemplatePropertyEntries, arrayList);
					if (arrayList.Count > 0)
					{
						this.BuildExtractStatementsRecursive(arrayList, statements, topLevelStatements, linePragma, tableVarName, containerVarName);
					}
				}
			}
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x00154368 File Offset: 0x00152568
		private void AddEntryBuildersToList(ICollection entries, ArrayList list)
		{
			if (entries == null || list == null)
			{
				return;
			}
			foreach (object obj in entries)
			{
				BuilderPropertyEntry builderPropertyEntry = (BuilderPropertyEntry)obj;
				if (builderPropertyEntry.Builder != null)
				{
					TemplatePropertyEntry templatePropertyEntry = builderPropertyEntry as TemplatePropertyEntry;
					if (templatePropertyEntry == null || !templatePropertyEntry.IsMultiple)
					{
						list.Add(builderPropertyEntry.Builder);
					}
				}
			}
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x001543E4 File Offset: 0x001525E4
		private void BuildFieldDeclaration(ControlBuilder builder)
		{
			if (builder is ContentBuilderInternal)
			{
				return;
			}
			bool flag = false;
			if (this.Parser.BaseType != null)
			{
				Type type = Util.GetNonPrivateFieldType(this.Parser.BaseType, builder.ID);
				if (type == null)
				{
					type = Util.GetNonPrivatePropertyType(this.Parser.BaseType, builder.ID);
				}
				if (type != null)
				{
					if (type.IsAssignableFrom(builder.ControlType))
					{
						return;
					}
					if (typeof(Control).IsAssignableFrom(type))
					{
						throw new HttpParseException(SR.GetString("Base_class_field_with_type_different_from_type_of_control", new object[]
						{
							builder.ID,
							type.FullName,
							builder.ControlType.FullName
						}), null, builder.VirtualPath, null, builder.Line);
					}
					flag = true;
				}
			}
			CodeMemberField codeMemberField = new CodeMemberField(CodeDomUtility.BuildGlobalCodeTypeReference(builder.DeclareType), builder.ID);
			codeMemberField.Attributes &= (MemberAttributes)(-61441);
			if (flag)
			{
				codeMemberField.Attributes |= MemberAttributes.New;
			}
			codeMemberField.LinePragma = base.CreateCodeLinePragma(builder);
			codeMemberField.Attributes |= MemberAttributes.Family;
			if (typeof(Control).IsAssignableFrom(builder.DeclareType))
			{
				codeMemberField.UserData["WithEvents"] = true;
			}
			this._intermediateClass.Members.Add(codeMemberField);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x00154554 File Offset: 0x00152754
		private string GetExpressionBuilderMethodName(string eventName, ControlBuilder builder)
		{
			return "__" + eventName + builder.ID;
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x00154567 File Offset: 0x00152767
		private string BindingMethodName(ControlBuilder builder)
		{
			return "__DataBind" + builder.ID;
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x0015457C File Offset: 0x0015277C
		protected CodeMemberMethod BuildPropertyBindingMethod(ControlBuilder builder, bool fControlSkin)
		{
			bool tempObjectVariableDeclared = false;
			if (builder is DataBoundLiteralControlBuilder)
			{
				string name = this.BindingMethodName(builder);
				CodeLinePragma linePragma = base.CreateCodeLinePragma(builder);
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = name;
				codeMemberMethod.Attributes &= (MemberAttributes)(-61441);
				codeMemberMethod.Attributes |= MemberAttributes.Public;
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "sender"));
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(EventArgs), "e"));
				CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
				CodeStatementCollection codeStatementCollection2 = new CodeStatementCollection();
				CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(builder.ControlType, "target");
				Type bindingContainerType = builder.BindingContainerType;
				CodeVariableDeclarationStatement codeVariableDeclarationStatement2 = new CodeVariableDeclarationStatement(bindingContainerType, "Container");
				codeStatementCollection.Add(codeVariableDeclarationStatement2);
				codeStatementCollection.Add(codeVariableDeclarationStatement);
				codeStatementCollection2.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name), new CodeCastExpression(builder.ControlType, new CodeArgumentReferenceExpression("sender")))
				{
					LinePragma = linePragma
				});
				codeStatementCollection2.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(codeVariableDeclarationStatement2.Name), new CodeCastExpression(bindingContainerType, new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("target"), "BindingContainer")))
				{
					LinePragma = linePragma
				});
				DataBindingExpressionBuilder.GenerateItemTypeExpressions(builder, codeStatementCollection, codeStatementCollection2, linePragma, "Item");
				if (this._designerMode)
				{
					DataBindingExpressionBuilder.GenerateItemTypeExpressions(builder, codeStatementCollection, codeStatementCollection2, linePragma, "BindItem");
				}
				int num = -1;
				foreach (object obj in builder.SubBuilders)
				{
					num++;
					if (obj != null && num % 2 != 0)
					{
						CodeBlockBuilder codeBlockBuilder = (CodeBlockBuilder)obj;
						if (this._designerMode)
						{
							tempObjectVariableDeclared = this.GenerateSimpleAssignmentAtDesignTime(tempObjectVariableDeclared, codeStatementCollection, codeStatementCollection2, codeBlockBuilder.Content, base.CreateCodeLinePragma(codeBlockBuilder));
						}
						else
						{
							CodeExpression codeExpression = new CodeSnippetExpression(codeBlockBuilder.Content.Trim());
							if (codeBlockBuilder.IsEncoded)
							{
								codeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(HttpUtility)), "HtmlEncode"), new CodeExpression[]
								{
									codeExpression
								});
							}
							else
							{
								codeExpression = CodeDomUtility.GenerateConvertToString(codeExpression);
							}
							codeStatementCollection2.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("target"), "SetDataBoundString", new CodeExpression[0])
							{
								Parameters = 
								{
									new CodePrimitiveExpression(num / 2),
									codeExpression
								}
							})
							{
								LinePragma = base.CreateCodeLinePragma(codeBlockBuilder)
							});
						}
					}
				}
				foreach (object obj2 in codeStatementCollection)
				{
					CodeStatement value = (CodeStatement)obj2;
					codeMemberMethod.Statements.Add(value);
				}
				foreach (object obj3 in codeStatementCollection2)
				{
					CodeStatement value2 = (CodeStatement)obj3;
					codeMemberMethod.Statements.Add(value2);
				}
				this._sourceDataClass.Members.Add(codeMemberMethod);
				return codeMemberMethod;
			}
			EventInfo @event = DataBindingExpressionBuilder.Event;
			CodeLinePragma linePragma2 = base.CreateCodeLinePragma(builder);
			CodeMemberMethod codeMemberMethod2 = null;
			CodeStatementCollection codeStatementCollection3 = null;
			CodeStatementCollection codeStatementCollection4 = null;
			CodeStatementCollection codeStatementCollection5 = null;
			PropertyEntry propertyEntry = null;
			bool flag = builder is BindableTemplateBuilder;
			bool flag2 = true;
			bool flag3 = false;
			foreach (object obj4 in builder.BoundPropertyEntries)
			{
				BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj4;
				if ((!boundPropertyEntry.TwoWayBound || (!flag && !boundPropertyEntry.ReadOnlyProperty)) && boundPropertyEntry.IsDataBindingEntry)
				{
					if (flag2)
					{
						flag2 = false;
						codeMemberMethod2 = new CodeMemberMethod();
						codeStatementCollection3 = new CodeStatementCollection();
						codeStatementCollection4 = new CodeStatementCollection();
						string expressionBuilderMethodName = this.GetExpressionBuilderMethodName(@event.Name, builder);
						codeMemberMethod2.Name = expressionBuilderMethodName;
						codeMemberMethod2.Attributes &= (MemberAttributes)(-61441);
						codeMemberMethod2.Attributes |= MemberAttributes.Public;
						if (this._designerMode)
						{
							base.ApplyEditorBrowsableCustomAttribute(codeMemberMethod2);
						}
						Type eventHandlerType = @event.EventHandlerType;
						MethodInfo method = eventHandlerType.GetMethod("Invoke");
						ParameterInfo[] parameters = method.GetParameters();
						foreach (ParameterInfo parameterInfo in parameters)
						{
							codeMemberMethod2.Parameters.Add(new CodeParameterDeclarationExpression(parameterInfo.ParameterType, parameterInfo.Name));
						}
						codeStatementCollection5 = codeStatementCollection4;
						DataBindingExpressionBuilder.BuildExpressionSetup(builder, codeStatementCollection3, codeStatementCollection4, linePragma2, boundPropertyEntry.TwoWayBound, this._designerMode);
						this._sourceDataClass.Members.Add(codeMemberMethod2);
					}
					CodeStatementCollection statements = codeStatementCollection5;
					this.HandleDeviceFilterConditional(ref propertyEntry, boundPropertyEntry, codeStatementCollection4, ref statements, out codeStatementCollection5);
					if (this._designerMode)
					{
						int generatedColumn = "__o".Length + BaseCodeDomTreeGenerator.GetGeneratedColumnOffset(this._codeDomProvider);
						CodeLinePragma linePragma3 = base.CreateCodeLinePragma(builder.PageVirtualPath, boundPropertyEntry.Line, boundPropertyEntry.Column, generatedColumn, boundPropertyEntry.Expression.Length);
						tempObjectVariableDeclared = this.GenerateSimpleAssignmentAtDesignTime(tempObjectVariableDeclared, codeStatementCollection3, codeStatementCollection4, boundPropertyEntry.Expression, linePragma3);
					}
					else if (boundPropertyEntry.TwoWayBound)
					{
						DataBindingExpressionBuilder.BuildEvalExpression(boundPropertyEntry.FieldName, boundPropertyEntry.FormatString, boundPropertyEntry.Name, boundPropertyEntry.Type, builder, codeStatementCollection3, statements, linePragma2, boundPropertyEntry.IsEncoded, ref flag3);
					}
					else
					{
						DataBindingExpressionBuilder.BuildExpressionStatic(boundPropertyEntry, builder, null, codeStatementCollection3, statements, linePragma2, boundPropertyEntry.IsEncoded, ref flag3);
					}
				}
			}
			if (codeStatementCollection3 != null)
			{
				foreach (object obj5 in codeStatementCollection3)
				{
					CodeStatement value3 = (CodeStatement)obj5;
					codeMemberMethod2.Statements.Add(value3);
				}
			}
			if (codeStatementCollection4 != null)
			{
				foreach (object obj6 in codeStatementCollection4)
				{
					CodeStatement value4 = (CodeStatement)obj6;
					codeMemberMethod2.Statements.Add(value4);
				}
			}
			return codeMemberMethod2;
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x00154C78 File Offset: 0x00152E78
		internal void BuildRenderMethod(ControlBuilder builder, bool fTemplate)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Attributes = (MemberAttributes)20482;
			codeMemberMethod.Name = "__Render" + builder.ID;
			if (this._designerMode)
			{
				base.ApplyEditorBrowsableCustomAttribute(codeMemberMethod);
			}
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(HtmlTextWriter), "__w"));
			codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Control), "parameterContainer"));
			this._sourceDataClass.Members.Add(codeMemberMethod);
			bool tempObjectVariableDeclared = false;
			if (builder.SubBuilders != null)
			{
				IEnumerator enumerator = builder.SubBuilders.GetEnumerator();
				int num = 0;
				int num2 = 0;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					CodeLinePragma linePragma = null;
					if (obj is ControlBuilder)
					{
						linePragma = base.CreateCodeLinePragma((ControlBuilder)obj);
					}
					if (obj is string)
					{
						if (!this._designerMode)
						{
							this.AddOutputWriteStringStatement(codeMemberMethod.Statements, (string)obj);
						}
					}
					else if (obj is CodeBlockBuilder)
					{
						CodeBlockBuilder codeBlockBuilder = (CodeBlockBuilder)obj;
						if (codeBlockBuilder.BlockType == CodeBlockType.Expression || codeBlockBuilder.BlockType == CodeBlockType.EncodedExpression)
						{
							string content = codeBlockBuilder.Content;
							if (this._designerMode)
							{
								tempObjectVariableDeclared = this.GenerateSimpleAssignmentAtDesignTime(tempObjectVariableDeclared, codeMemberMethod.Statements, codeMemberMethod.Statements, content, linePragma);
							}
							else
							{
								CodeStatement outputWriteStatement = this.GetOutputWriteStatement(new CodeSnippetExpression(content), codeBlockBuilder.BlockType == CodeBlockType.EncodedExpression);
								TextWriter textWriter = new StringWriter(CultureInfo.InvariantCulture);
								this._codeDomProvider.GenerateCodeFromStatement(outputWriteStatement, textWriter, null);
								string text = textWriter.ToString();
								text = text.PadLeft(codeBlockBuilder.Column + content.Length + 3);
								CodeSnippetStatement codeSnippetStatement = new CodeSnippetStatement(text);
								codeSnippetStatement.LinePragma = linePragma;
								codeMemberMethod.Statements.Add(codeSnippetStatement);
							}
						}
						else
						{
							string text2 = codeBlockBuilder.Content;
							text2 = text2.PadLeft(text2.Length + codeBlockBuilder.Column - 1);
							CodeSnippetStatement codeSnippetStatement2 = new CodeSnippetStatement(text2);
							codeSnippetStatement2.LinePragma = linePragma;
							codeMemberMethod.Statements.Add(codeSnippetStatement2);
						}
					}
					else if (obj is CodeStatementBuilder)
					{
						if (!this._designerMode)
						{
							CodeStatementBuilder codeStatementBuilder = (CodeStatementBuilder)obj;
							CodeStatement value = codeStatementBuilder.BuildStatement(new CodeArgumentReferenceExpression("__w"));
							codeMemberMethod.Statements.Add(value);
						}
					}
					else if (obj is ControlBuilder && !this._designerMode)
					{
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
						CodeExpressionStatement value2 = new CodeExpressionStatement(codeMethodInvokeExpression);
						codeMethodInvokeExpression.Method.TargetObject = new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("parameterContainer"), "Controls"), new CodeExpression[]
						{
							new CodePrimitiveExpression(num++)
						});
						codeMethodInvokeExpression.Method.MethodName = "RenderControl";
						codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("__w"));
						codeMemberMethod.Statements.Add(value2);
					}
					num2++;
				}
			}
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x00154F8C File Offset: 0x0015318C
		private bool GenerateSimpleAssignmentAtDesignTime(bool tempObjectVariableDeclared, CodeStatementCollection topMethodStatements, CodeStatementCollection otherMethodStatements, string content, CodeLinePragma linePragma)
		{
			if (!tempObjectVariableDeclared)
			{
				tempObjectVariableDeclared = true;
				topMethodStatements.Add(new CodeVariableDeclarationStatement(typeof(object), "__o"));
			}
			otherMethodStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("__o"), new CodeSnippetExpression(content))
			{
				LinePragma = linePragma
			});
			return tempObjectVariableDeclared;
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x00154FE4 File Offset: 0x001531E4
		protected virtual void BuildSourceDataTreeFromBuilder(ControlBuilder builder, bool fInTemplate, bool topLevelControlInTemplate, PropertyEntry pse)
		{
			if (builder is CodeBlockBuilder || builder is CodeStatementBuilder)
			{
				return;
			}
			bool flag = builder is TemplateBuilder;
			if (builder.ID == null || fInTemplate)
			{
				this._controlCount++;
				builder.ID = "__control" + this._controlCount.ToString(NumberFormatInfo.InvariantInfo);
				builder.IsGeneratedID = true;
			}
			if (builder.SubBuilders != null)
			{
				foreach (object obj in builder.SubBuilders)
				{
					if (obj is ControlBuilder)
					{
						bool topLevelControlInTemplate2 = flag && typeof(Control).IsAssignableFrom(((ControlBuilder)obj).ControlType) && !(builder is RootBuilder);
						this.BuildSourceDataTreeFromBuilder((ControlBuilder)obj, fInTemplate, topLevelControlInTemplate2, null);
					}
				}
			}
			foreach (object obj2 in builder.TemplatePropertyEntries)
			{
				TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj2;
				bool fInTemplate2 = true;
				if (templatePropertyEntry.PropertyInfo != null)
				{
					fInTemplate2 = templatePropertyEntry.IsMultiple;
				}
				this.BuildSourceDataTreeFromBuilder(templatePropertyEntry.Builder, fInTemplate2, false, templatePropertyEntry);
			}
			foreach (object obj3 in builder.ComplexPropertyEntries)
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj3;
				if (!(complexPropertyEntry.Builder is StringPropertyBuilder))
				{
					this.BuildSourceDataTreeFromBuilder(complexPropertyEntry.Builder, fInTemplate, false, complexPropertyEntry);
				}
			}
			if (!builder.IsGeneratedID)
			{
				this.BuildFieldDeclaration(builder);
			}
			CodeMemberMethod buildMethod = null;
			CodeMemberMethod dataBindingMethod = null;
			if (this._sourceDataClass != null)
			{
				if (!this._designerMode)
				{
					buildMethod = this.BuildBuildMethod(builder, flag, fInTemplate, topLevelControlInTemplate, pse, false);
				}
				if (builder.HasAspCode)
				{
					this.BuildRenderMethod(builder, flag);
				}
				this.BuildExtractMethod(builder);
				dataBindingMethod = this.BuildPropertyBindingMethod(builder, false);
			}
			builder.ProcessGeneratedCode(this._codeCompileUnit, this._intermediateClass, this._sourceDataClass, buildMethod, dataBindingMethod);
			if (this.Parser.ControlBuilderInterceptor != null)
			{
				this.Parser.ControlBuilderInterceptor.OnProcessGeneratedCode(builder, this._codeCompileUnit, this._intermediateClass, this._sourceDataClass, buildMethod, dataBindingMethod, builder.AdditionalState);
			}
			this.Parser.ParseRecorders.ProcessGeneratedCode(builder, this._codeCompileUnit, this._intermediateClass, this._sourceDataClass, buildMethod, dataBindingMethod);
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0015528C File Offset: 0x0015348C
		internal virtual CodeExpression BuildStringPropertyExpression(PropertyEntry pse)
		{
			string value = string.Empty;
			if (pse is SimplePropertyEntry)
			{
				value = (string)((SimplePropertyEntry)pse).Value;
			}
			else
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)pse;
				value = (string)((StringPropertyBuilder)complexPropertyEntry.Builder).BuildObject();
			}
			return CodeDomUtility.GenerateExpressionForValue(pse.PropertyInfo, value, typeof(string));
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001552F0 File Offset: 0x001534F0
		protected virtual CodeAssignStatement BuildTemplatePropertyStatement(CodeExpression ctrlRefExpr)
		{
			return new CodeAssignStatement
			{
				Left = new CodePropertyReferenceExpression(ctrlRefExpr, "TemplateControl"),
				Right = new CodeThisReferenceExpression()
			};
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x00155320 File Offset: 0x00153520
		private string ExtractMethodName(ControlBuilder builder)
		{
			return BaseTemplateCodeDomTreeGenerator.extractTemplateValuesMethodPrefix + builder.ID;
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x00155332 File Offset: 0x00153532
		private Type GetCtrlTypeForBuilder(ControlBuilder builder, bool fTemplate)
		{
			if (builder is RootBuilder && builder.ControlType != null)
			{
				return builder.ControlType;
			}
			if (fTemplate)
			{
				return typeof(Control);
			}
			return builder.ControlType;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x00155365 File Offset: 0x00153565
		protected string GetMethodNameForBuilder(string prefix, ControlBuilder builder)
		{
			if (builder is RootBuilder)
			{
				return prefix + "Tree";
			}
			return prefix + builder.ID;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x00155388 File Offset: 0x00153588
		private void HandleDeviceFilterConditional(ref PropertyEntry previous, PropertyEntry current, CodeStatementCollection topStmts, ref CodeStatementCollection currentStmts, out CodeStatementCollection nextStmts)
		{
			bool flag = previous != null && StringUtil.EqualsIgnoreCase(previous.Name, current.Name);
			if (current.Filter.Length != 0)
			{
				if (!flag)
				{
					currentStmts = topStmts;
					previous = null;
				}
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "TestDeviceFilter", new CodeExpression[0])
				{
					Parameters = 
					{
						new CodePrimitiveExpression(current.Filter)
					}
				};
				currentStmts.Add(codeConditionStatement);
				currentStmts = codeConditionStatement.TrueStatements;
				nextStmts = codeConditionStatement.FalseStatements;
				previous = current;
				return;
			}
			if (!flag)
			{
				currentStmts = topStmts;
			}
			nextStmts = topStmts;
			previous = null;
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0015542E File Offset: 0x0015362E
		protected virtual bool UseResourceLiteralString(string s)
		{
			return PageParser.EnableLongStringsAsResources && s.Length >= 256 && this._codeDomProvider.Supports(GeneratorSupport.Win32Resources);
		}

		// Token: 0x040032C1 RID: 12993
		protected static readonly string buildMethodPrefix = "__BuildControl";

		// Token: 0x040032C2 RID: 12994
		protected static readonly string extractTemplateValuesMethodPrefix = "__ExtractValues";

		// Token: 0x040032C3 RID: 12995
		protected static readonly string templateSourceDirectoryName = "AppRelativeTemplateSourceDirectory";

		// Token: 0x040032C4 RID: 12996
		protected static readonly string applyStyleSheetMethodName = "ApplyStyleSheetSkin";

		// Token: 0x040032C5 RID: 12997
		protected static readonly string pagePropertyName = "Page";

		// Token: 0x040032C6 RID: 12998
		internal const string skinIDPropertyName = "SkinID";

		// Token: 0x040032C7 RID: 12999
		private const string _localVariableRef = "__ctrl";

		// Token: 0x040032C8 RID: 13000
		private TemplateParser _parser;

		// Token: 0x040032C9 RID: 13001
		private int _controlCount;

		// Token: 0x040032CA RID: 13002
		private const int minLongLiteralStringLength = 256;

		// Token: 0x040032CB RID: 13003
		private const string renderMethodParameterName = "__w";

		// Token: 0x040032CC RID: 13004
		internal const string tempObjectVariable = "__o";
	}
}
