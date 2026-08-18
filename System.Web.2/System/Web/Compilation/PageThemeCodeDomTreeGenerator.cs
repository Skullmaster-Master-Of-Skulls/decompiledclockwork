using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000854 RID: 2132
	internal class PageThemeCodeDomTreeGenerator : BaseTemplateCodeDomTreeGenerator
	{
		// Token: 0x0600650C RID: 25868 RVA: 0x00162FD0 File Offset: 0x001611D0
		internal PageThemeCodeDomTreeGenerator(PageThemeParser parser) : base(parser)
		{
			this._themeParser = parser;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x0016302C File Offset: 0x0016122C
		private void AddMemberOverride(string name, Type type, CodeExpression expr)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = (MemberAttributes)12292;
			codeMemberProperty.Type = new CodeTypeReference(type.FullName);
			CodeMethodReturnStatement value = new CodeMethodReturnStatement(expr);
			codeMemberProperty.GetStatements.Add(value);
			this._sourceDataClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x00163088 File Offset: 0x00161288
		private void BuildControlSkins(CodeStatementCollection statements)
		{
			foreach (object obj in this._controlSkinBuilderEntryList)
			{
				PageThemeCodeDomTreeGenerator.ControlSkinBuilderEntry controlSkinBuilderEntry = (PageThemeCodeDomTreeGenerator.ControlSkinBuilderEntry)obj;
				string skinID = controlSkinBuilderEntry.SkinID;
				ControlBuilder builder = controlSkinBuilderEntry.Builder;
				statements.Add(this.BuildControlSkinAssignmentStatement(builder, skinID));
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x001630FC File Offset: 0x001612FC
		private CodeStatement BuildControlSkinAssignmentStatement(ControlBuilder builder, string skinID)
		{
			Type controlType = builder.ControlType;
			string text = base.GetMethodNameForBuilder(BaseTemplateCodeDomTreeGenerator.buildMethodPrefix, builder) + "_skinKey";
			CodeMemberField codeMemberField = new CodeMemberField(typeof(object), text);
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.InitExpression = new CodeMethodInvokeExpression
			{
				Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(PageTheme)), "CreateSkinKey"),
				Parameters = 
				{
					new CodeTypeOfExpression(controlType),
					new CodePrimitiveExpression(skinID)
				}
			};
			this._sourceDataClass.Members.Add(codeMemberField);
			CodeFieldReferenceExpression targetObject = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "__controlSkins");
			CodeIndexerExpression left = new CodeIndexerExpression(targetObject, new CodeExpression[]
			{
				new CodeVariableReferenceExpression(text)
			});
			CodeDelegateCreateExpression value = new CodeDelegateCreateExpression(this._controlSkinDelegateType, new CodeThisReferenceExpression(), base.GetMethodNameForBuilder(BaseTemplateCodeDomTreeGenerator.buildMethodPrefix, builder));
			return new CodeAssignStatement(left, new CodeObjectCreateExpression(this._controlSkinType, new CodeExpression[0])
			{
				Parameters = 
				{
					new CodeTypeOfExpression(controlType),
					value
				}
			});
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0016322C File Offset: 0x0016142C
		private void BuildControlSkinMember()
		{
			int count = this._controlSkinBuilderEntryList.Count;
			CodeMemberField codeMemberField = new CodeMemberField(typeof(HybridDictionary).FullName, "__controlSkins");
			codeMemberField.InitExpression = new CodeObjectCreateExpression(typeof(HybridDictionary), new CodeExpression[0])
			{
				Parameters = 
				{
					new CodePrimitiveExpression(count)
				}
			};
			this._sourceDataClass.Members.Add(codeMemberField);
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x001632A8 File Offset: 0x001614A8
		private void BuildControlSkinProperty()
		{
			CodeFieldReferenceExpression expr = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "__controlSkins");
			this.AddMemberOverride("ControlSkins", typeof(IDictionary), expr);
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x001632DC File Offset: 0x001614DC
		private void BuildLinkedStyleSheetMember()
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(string[]), "__linkedStyleSheets");
			if (this._themeParser.CssFileList != null && this._themeParser.CssFileList.Count > 0)
			{
				CodeExpression[] array = new CodeExpression[this._themeParser.CssFileList.Count];
				int num = 0;
				foreach (object obj in this._themeParser.CssFileList)
				{
					string value = (string)obj;
					array[num++] = new CodePrimitiveExpression(value);
				}
				CodeArrayCreateExpression initExpression = new CodeArrayCreateExpression(typeof(string), array);
				codeMemberField.InitExpression = initExpression;
			}
			else
			{
				codeMemberField.InitExpression = new CodePrimitiveExpression(null);
			}
			this._sourceDataClass.Members.Add(codeMemberField);
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x001633D4 File Offset: 0x001615D4
		private void BuildLinkedStyleSheetProperty()
		{
			CodeFieldReferenceExpression expr = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "__linkedStyleSheets");
			this.AddMemberOverride("LinkedStyleSheets", typeof(string[]), expr);
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x00163407 File Offset: 0x00161607
		protected override void BuildInitStatements(CodeStatementCollection trueStatements, CodeStatementCollection topLevelStatements)
		{
			base.BuildInitStatements(trueStatements, topLevelStatements);
			this.BuildControlSkins(topLevelStatements);
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x00163418 File Offset: 0x00161618
		protected override void BuildMiscClassMembers()
		{
			base.BuildMiscClassMembers();
			this.AddMemberOverride(BaseTemplateCodeDomTreeGenerator.templateSourceDirectoryName, typeof(string), new CodePrimitiveExpression(this._themeParser.VirtualDirPath.VirtualPathString));
			this.BuildSourceDataTreeFromBuilder(this._themeParser.RootBuilder, false, false, null);
			this.BuildControlSkinMember();
			this.BuildControlSkinProperty();
			this.BuildLinkedStyleSheetMember();
			this.BuildLinkedStyleSheetProperty();
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x00163484 File Offset: 0x00161684
		protected override void BuildSourceDataTreeFromBuilder(ControlBuilder builder, bool fInTemplate, bool topLevelControlInTemplate, PropertyEntry pse)
		{
			if (builder is CodeBlockBuilder)
			{
				return;
			}
			bool flag = builder is TemplateBuilder;
			bool flag2 = builder == this._themeParser.RootBuilder;
			bool flag3 = !fInTemplate && !flag && topLevelControlInTemplate;
			this._controlCount++;
			builder.ID = "__control" + this._controlCount.ToString(NumberFormatInfo.InvariantInfo);
			builder.IsGeneratedID = true;
			if (flag3 && !(builder is DataBoundLiteralControlBuilder))
			{
				Type controlType = builder.ControlType;
				string skinID = builder.SkinID;
				object key = PageTheme.CreateSkinKey(builder.ControlType, skinID);
				if (this._controlSkinTypeNameCollection.Contains(key))
				{
					if (string.IsNullOrEmpty(skinID))
					{
						throw new HttpParseException(SR.GetString("Page_theme_default_theme_already_defined", new object[]
						{
							builder.ControlType.FullName
						}), null, builder.VirtualPath, null, builder.Line);
					}
					throw new HttpParseException(SR.GetString("Page_theme_skinID_already_defined", new object[]
					{
						skinID
					}), null, builder.VirtualPath, null, builder.Line);
				}
				else
				{
					this._controlSkinTypeNameCollection.Add(key, true);
					this._controlSkinBuilderEntryList.Add(new PageThemeCodeDomTreeGenerator.ControlSkinBuilderEntry(builder, skinID));
				}
			}
			if (builder.SubBuilders != null)
			{
				foreach (object obj in builder.SubBuilders)
				{
					if (obj is ControlBuilder)
					{
						bool topLevelControlInTemplate2 = flag && typeof(Control).IsAssignableFrom(((ControlBuilder)obj).ControlType);
						this.BuildSourceDataTreeFromBuilder((ControlBuilder)obj, fInTemplate, topLevelControlInTemplate2, null);
					}
				}
			}
			foreach (object obj2 in builder.TemplatePropertyEntries)
			{
				TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj2;
				this.BuildSourceDataTreeFromBuilder(templatePropertyEntry.Builder, true, false, templatePropertyEntry);
			}
			foreach (object obj3 in builder.ComplexPropertyEntries)
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj3;
				if (!(complexPropertyEntry.Builder is StringPropertyBuilder))
				{
					this.BuildSourceDataTreeFromBuilder(complexPropertyEntry.Builder, fInTemplate, false, complexPropertyEntry);
				}
			}
			if (!flag2)
			{
				base.BuildBuildMethod(builder, flag, fInTemplate, topLevelControlInTemplate, pse, flag3);
			}
			if (!flag3 && builder.HasAspCode)
			{
				base.BuildRenderMethod(builder, flag);
			}
			base.BuildExtractMethod(builder);
			base.BuildPropertyBindingMethod(builder, flag3);
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x00163744 File Offset: 0x00161944
		internal override CodeExpression BuildStringPropertyExpression(PropertyEntry pse)
		{
			if (pse.PropertyInfo != null)
			{
				UrlPropertyAttribute urlPropertyAttribute = Attribute.GetCustomAttribute(pse.PropertyInfo, typeof(UrlPropertyAttribute)) as UrlPropertyAttribute;
				if (urlPropertyAttribute != null)
				{
					if (pse is SimplePropertyEntry)
					{
						SimplePropertyEntry simplePropertyEntry = (SimplePropertyEntry)pse;
						string text = (string)simplePropertyEntry.Value;
						if (UrlPath.IsRelativeUrl(text) && !UrlPath.IsAppRelativePath(text))
						{
							simplePropertyEntry.Value = UrlPath.MakeVirtualPathAppRelative(UrlPath.Combine(this._themeParser.VirtualDirPath.VirtualPathString, text));
						}
					}
					else
					{
						ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)pse;
						StringPropertyBuilder stringPropertyBuilder = (StringPropertyBuilder)complexPropertyEntry.Builder;
						string text2 = (string)stringPropertyBuilder.BuildObject();
						if (UrlPath.IsRelativeUrl(text2) && !UrlPath.IsAppRelativePath(text2))
						{
							complexPropertyEntry.Builder = new StringPropertyBuilder(UrlPath.MakeVirtualPathAppRelative(UrlPath.Combine(this._themeParser.VirtualDirPath.VirtualPathString, text2)));
						}
					}
				}
			}
			return base.BuildStringPropertyExpression(pse);
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x00163838 File Offset: 0x00161A38
		protected override CodeAssignStatement BuildTemplatePropertyStatement(CodeExpression ctrlRefExpr)
		{
			return new CodeAssignStatement
			{
				Left = new CodePropertyReferenceExpression(ctrlRefExpr, BaseTemplateCodeDomTreeGenerator.templateSourceDirectoryName),
				Right = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), BaseTemplateCodeDomTreeGenerator.templateSourceDirectoryName)
			};
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00163874 File Offset: 0x00161A74
		protected override string GetGeneratedClassName()
		{
			string fileName = this._themeParser.VirtualDirPath.FileName;
			return Util.MakeValidTypeNameFromString(fileName);
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool UseResourceLiteralString(string s)
		{
			return false;
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool NeedProfileProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400341D RID: 13341
		private Hashtable _controlSkinTypeNameCollection = new Hashtable();

		// Token: 0x0400341E RID: 13342
		private ArrayList _controlSkinBuilderEntryList = new ArrayList();

		// Token: 0x0400341F RID: 13343
		private int _controlCount;

		// Token: 0x04003420 RID: 13344
		private CodeTypeReference _controlSkinDelegateType = new CodeTypeReference(typeof(ControlSkinDelegate));

		// Token: 0x04003421 RID: 13345
		private CodeTypeReference _controlSkinType = new CodeTypeReference(typeof(ControlSkin));

		// Token: 0x04003422 RID: 13346
		private PageThemeParser _themeParser;

		// Token: 0x04003423 RID: 13347
		private const string _controlSkinsVarName = "__controlSkins";

		// Token: 0x04003424 RID: 13348
		private const string _controlSkinsPropertyName = "ControlSkins";

		// Token: 0x04003425 RID: 13349
		private const string _linkedStyleSheetsVarName = "__linkedStyleSheets";

		// Token: 0x04003426 RID: 13350
		private const string _linkedStyleSheetsPropertyName = "LinkedStyleSheets";

		// Token: 0x02000A72 RID: 2674
		private class ControlSkinBuilderEntry
		{
			// Token: 0x06006F31 RID: 28465 RVA: 0x0018B998 File Offset: 0x00189B98
			public ControlSkinBuilderEntry(ControlBuilder builder, string skinID)
			{
				this._builder = builder;
				this._id = skinID;
			}

			// Token: 0x17001E49 RID: 7753
			// (get) Token: 0x06006F32 RID: 28466 RVA: 0x0018B9AE File Offset: 0x00189BAE
			public ControlBuilder Builder
			{
				get
				{
					return this._builder;
				}
			}

			// Token: 0x17001E4A RID: 7754
			// (get) Token: 0x06006F33 RID: 28467 RVA: 0x0018B9B6 File Offset: 0x00189BB6
			public string SkinID
			{
				get
				{
					if (this._id != null)
					{
						return this._id;
					}
					return string.Empty;
				}
			}

			// Token: 0x04003BAC RID: 15276
			private ControlBuilder _builder;

			// Token: 0x04003BAD RID: 15277
			private string _id;
		}
	}
}
