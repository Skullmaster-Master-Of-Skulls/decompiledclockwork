using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.Design;
using System.Diagnostics;
using System.Reflection;

namespace System.Data.Design
{
	// Token: 0x02000269 RID: 617
	internal sealed class TableAdapterManagerMethodGenerator
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x000820DC File Offset: 0x000802DC
		internal TableAdapterManagerMethodGenerator(TypedDataSourceCodeGenerator codeGenerator, DesignDataSource dataSource, CodeTypeDeclaration dataSourceType)
		{
			this.codeGenerator = codeGenerator;
			this.dataSource = dataSource;
			this.dataSourceType = dataSourceType;
			this.nameHandler = new TableAdapterManagerNameHandler(codeGenerator.CodeProvider);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0008210C File Offset: 0x0008030C
		internal void AddEverything(CodeTypeDeclaration dataComponentClass)
		{
			if (dataComponentClass == null)
			{
				throw new InternalException("dataComponent CodeTypeDeclaration should not be null.");
			}
			this.AddUpdateOrderMembers(dataComponentClass);
			this.AddAdapterMembers(dataComponentClass);
			this.AddVariableAndProperty(dataComponentClass, (MemberAttributes)24578, CodeGenHelper.GlobalType(typeof(bool)), "BackupDataSetBeforeUpdate", "_backupDataSetBeforeUpdate", false);
			this.AddConnectionMembers(dataComponentClass);
			this.AddTableAdapterCountMembers(dataComponentClass);
			this.AddUpdateAll(dataComponentClass);
			this.AddSortSelfRefRows(dataComponentClass);
			this.AddSelfRefComparer(dataComponentClass);
			this.AddMatchTableAdapterConnection(dataComponentClass);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00082188 File Offset: 0x00080388
		private void AddUpdateOrderMembers(CodeTypeDeclaration dataComponentClass)
		{
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class("UpdateOrderOption", false, TypeAttributes.NestedPublic);
			codeTypeDeclaration.IsEnum = true;
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Update Order Option", true));
			CodeMemberField value = CodeGenHelper.FieldDecl(CodeGenHelper.Type(typeof(int)), "InsertUpdateDelete", CodeGenHelper.Primitive(0));
			codeTypeDeclaration.Members.Add(value);
			CodeMemberField value2 = CodeGenHelper.FieldDecl(CodeGenHelper.Type(typeof(int)), "UpdateInsertDelete", CodeGenHelper.Primitive(1));
			codeTypeDeclaration.Members.Add(value2);
			dataComponentClass.Members.Add(codeTypeDeclaration);
			this.AddVariableAndProperty(dataComponentClass, (MemberAttributes)24578, CodeGenHelper.Type("UpdateOrderOption"), "UpdateOrder", "_updateOrder", false);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00082254 File Offset: 0x00080454
		private void AddAdapterMembers(CodeTypeDeclaration dataComponentClass)
		{
			foreach (object obj in this.dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				if (this.CanAddTableAdapter(designTable))
				{
					designTable.PropertyCache.TAMAdapterPropName = this.nameHandler.GetTableAdapterPropName(designTable.GeneratorDataComponentClassName);
					designTable.PropertyCache.TAMAdapterVarName = this.nameHandler.GetTableAdapterVarName(designTable.PropertyCache.TAMAdapterPropName);
					string tamadapterVarName = designTable.PropertyCache.TAMAdapterVarName;
					CodeMemberField value = CodeGenHelper.FieldDecl(CodeGenHelper.Type(designTable.GeneratorDataComponentClassName), tamadapterVarName);
					dataComponentClass.Members.Add(value);
					CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.Type(designTable.GeneratorDataComponentClassName), designTable.PropertyCache.TAMAdapterPropName, (MemberAttributes)24578);
					codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.EditorAttribute", CodeGenHelper.Str("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), CodeGenHelper.Str("System.Drawing.Design.UITypeEditor")));
					codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.ThisField(tamadapterVarName)));
					codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.ThisField(tamadapterVarName), CodeGenHelper.Argument("value")));
					dataComponentClass.Members.Add(codeMemberProperty);
				}
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000823CC File Offset: 0x000805CC
		private void AddConnectionMembers(CodeTypeDeclaration dataComponentClass)
		{
			string text = "_connection";
			CodeMemberField value = CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(IDbConnection)), text);
			dataComponentClass.Members.Add(value);
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(IDbConnection)), "Connection", (MemberAttributes)24578);
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.Browsable", CodeGenHelper.Primitive(false)));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(text)), CodeGenHelper.Return(CodeGenHelper.ThisField(text))));
			foreach (object obj in this.dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				if (this.CanAddTableAdapter(designTable))
				{
					string tamadapterVarName = designTable.PropertyCache.TAMAdapterVarName;
					codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(tamadapterVarName)), CodeGenHelper.IdIsNotNull(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName), "Connection"))), CodeGenHelper.Return(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName), "Connection"))));
				}
			}
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Primitive(null)));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.ThisField(text), CodeGenHelper.Argument("value")));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00082564 File Offset: 0x00080764
		private void AddTableAdapterCountMembers(CodeTypeDeclaration dataComponentClass)
		{
			string text = "count";
			CodeExpression codeExpression = CodeGenHelper.Variable(text);
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(int)), "TableAdapterInstanceCount", (MemberAttributes)24578);
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.Browsable", CodeGenHelper.Primitive(false)));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), text, CodeGenHelper.Primitive(0)));
			foreach (object obj in this.dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				if (this.CanAddTableAdapter(designTable))
				{
					string tamadapterVarName = designTable.PropertyCache.TAMAdapterVarName;
					codeMemberProperty.GetStatements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(tamadapterVarName)), CodeGenHelper.Assign(codeExpression, CodeGenHelper.BinOperator(codeExpression, CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1)))));
				}
			}
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(codeExpression));
			dataComponentClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000826A4 File Offset: 0x000808A4
		private void AddSortSelfRefRows(CodeTypeDeclaration dataComponentClass)
		{
			string text = "rows";
			string text2 = "relation";
			string text3 = "childFirst";
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "SortSelfReferenceRows", MemberAttributes.Family);
			codeMemberMethod.Parameters.AddRange(new CodeParameterDeclarationExpression[]
			{
				CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRow), 1), text),
				CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRelation)), text2),
				CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(bool)), text3)
			});
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(CodeGenHelper.GlobalTypeExpr("System.Array"), "Sort", new CodeTypeReference[]
			{
				CodeGenHelper.GlobalType(typeof(DataRow))
			});
			CodeMethodInvokeExpression expr = new CodeMethodInvokeExpression(method, new CodeExpression[]
			{
				CodeGenHelper.Argument(text),
				CodeGenHelper.New(CodeGenHelper.Type("SelfReferenceComparer"), new CodeExpression[]
				{
					CodeGenHelper.Argument(text2),
					CodeGenHelper.Argument(text3)
				})
			});
			codeMemberMethod.Statements.Add(CodeGenHelper.Stm(expr));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000827CC File Offset: 0x000809CC
		private void AddSelfRefComparer(CodeTypeDeclaration dataComponentClass)
		{
			string text = "_relation";
			string text2 = "_childFirst";
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class("SelfReferenceComparer", false, TypeAttributes.NestedPrivate);
			CodeTypeReference value = CodeGenHelper.GlobalGenericType("System.Collections.Generic.IComparer", typeof(DataRow));
			codeTypeDeclaration.BaseTypes.Add(CodeGenHelper.GlobalType(typeof(object)));
			codeTypeDeclaration.BaseTypes.Add(value);
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Used to sort self-referenced table's rows", true));
			dataComponentClass.Members.Add(codeTypeDeclaration);
			codeTypeDeclaration.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(DataRelation)), text));
			codeTypeDeclaration.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(int)), text2));
			CodeConstructor codeConstructor = CodeGenHelper.Constructor(MemberAttributes.Assembly);
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRelation)), "relation"));
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(bool)), "childFirst"));
			codeConstructor.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.ThisField(text), CodeGenHelper.Argument("relation")));
			codeConstructor.Statements.Add(CodeGenHelper.If(CodeGenHelper.Argument("childFirst"), CodeGenHelper.Assign(CodeGenHelper.ThisField(text2), CodeGenHelper.Primitive(-1)), CodeGenHelper.Assign(CodeGenHelper.ThisField(text2), CodeGenHelper.Primitive(1))));
			codeTypeDeclaration.Members.Add(codeConstructor);
			string text3 = "row";
			string text4 = "distance";
			string text5 = "root";
			string text6 = "parent";
			string name = "GetRoot";
			string text7 = "traversedRows";
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(DataRow)), name, MemberAttributes.Private);
			codeMemberMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text3));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(int)), text4);
			codeParameterDeclarationExpression.Direction = FieldDirection.Out;
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCallStm(CodeGenHelper.GlobalTypeExpr(typeof(Debug)), "Assert", CodeGenHelper.IdIsNotNull(CodeGenHelper.Argument(text3))));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text5, CodeGenHelper.Argument(text3)));
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Argument(text4), CodeGenHelper.Primitive(0)));
			codeMemberMethod.Statements.Add(new CodeSnippetStatement());
			CodeTypeReference codeTypeReference = new CodeTypeReference("System.Collections.Generic.IDictionary", new CodeTypeReference[]
			{
				CodeGenHelper.GlobalType(typeof(DataRow)),
				CodeGenHelper.GlobalType(typeof(DataRow))
			});
			codeTypeReference.Options = CodeTypeReferenceOptions.GlobalReference;
			CodeTypeReference codeTypeReference2 = new CodeTypeReference("System.Collections.Generic.Dictionary", new CodeTypeReference[]
			{
				CodeGenHelper.GlobalType(typeof(DataRow)),
				CodeGenHelper.GlobalType(typeof(DataRow))
			});
			codeTypeReference2.Options = CodeTypeReferenceOptions.GlobalReference;
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(codeTypeReference, text7, CodeGenHelper.New(codeTypeReference2, new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Indexer(CodeGenHelper.Variable(text7), CodeGenHelper.Argument(text3)), CodeGenHelper.Argument(text3)));
			codeMemberMethod.Statements.Add(new CodeSnippetStatement());
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text6, CodeGenHelper.MethodCall(CodeGenHelper.Argument(text3), "GetParentRow", new CodeExpression[]
			{
				CodeGenHelper.ThisField(text),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), "Default")
			})));
			CodeIterationStatement codeIterationStatement = new CodeIterationStatement();
			codeIterationStatement.TestExpression = CodeGenHelper.And(CodeGenHelper.IdIsNotNull(CodeGenHelper.Variable(text6)), CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text7), "ContainsKey", CodeGenHelper.Variable(text6)), CodeGenHelper.Primitive(false)));
			codeIterationStatement.InitStatement = new CodeSnippetStatement();
			codeIterationStatement.IncrementStatement = new CodeSnippetStatement();
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Argument(text4), CodeGenHelper.BinOperator(CodeGenHelper.Argument(text4), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1))));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Variable(text5), CodeGenHelper.Variable(text6)));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Indexer(CodeGenHelper.Variable(text7), CodeGenHelper.Variable(text6)), CodeGenHelper.Variable(text6)));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Variable(text6), CodeGenHelper.MethodCall(CodeGenHelper.Variable(text6), "GetParentRow", new CodeExpression[]
			{
				CodeGenHelper.ThisField(text),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), "Default")
			})));
			codeMemberMethod.Statements.Add(codeIterationStatement);
			codeMemberMethod.Statements.Add(new CodeSnippetStatement());
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement(CodeGenHelper.EQ(CodeGenHelper.Argument(text4), CodeGenHelper.Primitive(0)), new CodeStatement[0]);
			codeConditionStatement.TrueStatements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text7), "Clear"));
			codeConditionStatement.TrueStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Indexer(CodeGenHelper.Variable(text7), CodeGenHelper.Argument(text3)), CodeGenHelper.Argument(text3)));
			codeConditionStatement.TrueStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Variable(text6), CodeGenHelper.MethodCall(CodeGenHelper.Argument(text3), "GetParentRow", new CodeExpression[]
			{
				CodeGenHelper.ThisField(text),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), "Original")
			})));
			codeIterationStatement = new CodeIterationStatement();
			codeIterationStatement.TestExpression = CodeGenHelper.And(CodeGenHelper.IdIsNotNull(CodeGenHelper.Variable(text6)), CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text7), "ContainsKey", CodeGenHelper.Variable(text6)), CodeGenHelper.Primitive(false)));
			codeIterationStatement.InitStatement = new CodeSnippetStatement();
			codeIterationStatement.IncrementStatement = new CodeSnippetStatement();
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Argument(text4), CodeGenHelper.BinOperator(CodeGenHelper.Argument(text4), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1))));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Variable(text5), CodeGenHelper.Variable(text6)));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Indexer(CodeGenHelper.Variable(text7), CodeGenHelper.Variable(text6)), CodeGenHelper.Variable(text6)));
			codeIterationStatement.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Variable(text6), CodeGenHelper.MethodCall(CodeGenHelper.Variable(text6), "GetParentRow", new CodeExpression[]
			{
				CodeGenHelper.ThisField(text),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataRowVersion)), "Original")
			})));
			codeConditionStatement.TrueStatements.Add(codeIterationStatement);
			codeMemberMethod.Statements.Add(codeConditionStatement);
			codeMemberMethod.Statements.Add(new CodeSnippetStatement());
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(text5)));
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			string text8 = "row1";
			string text9 = "row2";
			string text10 = "root1";
			string text11 = "root2";
			string text12 = "distance1";
			string text13 = "distance2";
			CodeMemberMethod codeMemberMethod2 = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(int)), "Compare", (MemberAttributes)24578);
			codeMemberMethod2.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text8));
			codeMemberMethod2.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text9));
			codeMemberMethod2.ImplementationTypes.Add(value);
			codeTypeDeclaration.Members.Add(codeMemberMethod2);
			codeMemberMethod2.Statements.Add(CodeGenHelper.If(CodeGenHelper.ReferenceEquals(CodeGenHelper.Argument(text8), CodeGenHelper.Argument(text9)), CodeGenHelper.Return(CodeGenHelper.Primitive(0))));
			codeMemberMethod2.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNull(CodeGenHelper.Argument(text8)), CodeGenHelper.Return(CodeGenHelper.Primitive(-1))));
			codeMemberMethod2.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNull(CodeGenHelper.Argument(text9)), CodeGenHelper.Return(CodeGenHelper.Primitive(1))));
			codeMemberMethod2.Statements.Add(new CodeSnippetStatement());
			codeMemberMethod2.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), text12, CodeGenHelper.Primitive(0)));
			codeMemberMethod2.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text10, CodeGenHelper.MethodCall(CodeGenHelper.This(), "GetRoot", new CodeExpression[]
			{
				CodeGenHelper.Argument(text8),
				new CodeDirectionExpression(FieldDirection.Out, CodeGenHelper.Variable(text12))
			})));
			codeMemberMethod2.Statements.Add(new CodeSnippetStatement());
			codeMemberMethod2.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), text13, CodeGenHelper.Primitive(0)));
			codeMemberMethod2.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text11, CodeGenHelper.MethodCall(CodeGenHelper.This(), "GetRoot", new CodeExpression[]
			{
				CodeGenHelper.Argument(text9),
				new CodeDirectionExpression(FieldDirection.Out, CodeGenHelper.Variable(text13))
			})));
			codeMemberMethod2.Statements.Add(new CodeSnippetStatement());
			CodeBinaryOperatorExpression expr = CodeGenHelper.BinOperator(CodeGenHelper.ThisField(text2), CodeBinaryOperatorType.Multiply, CodeGenHelper.MethodCall(CodeGenHelper.Variable(text12), "CompareTo", CodeGenHelper.Variable(text13)));
			CodeStatement codeStatement = CodeGenHelper.MethodCallStm(CodeGenHelper.GlobalTypeExpr(typeof(Debug)), "Assert", CodeGenHelper.And(CodeGenHelper.IdIsNotNull(CodeGenHelper.Field(CodeGenHelper.Variable(text10), "Table")), CodeGenHelper.IdIsNotNull(CodeGenHelper.Field(CodeGenHelper.Variable(text11), "Table"))));
			CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement(CodeGenHelper.Less(CodeGenHelper.MethodCall(CodeGenHelper.Field(CodeGenHelper.Field(CodeGenHelper.Variable(text10), "Table"), "Rows"), "IndexOf", CodeGenHelper.Variable(text10)), CodeGenHelper.MethodCall(CodeGenHelper.Field(CodeGenHelper.Field(CodeGenHelper.Variable(text11), "Table"), "Rows"), "IndexOf", CodeGenHelper.Variable(text11))), new CodeStatement[]
			{
				CodeGenHelper.Return(CodeGenHelper.Primitive(-1))
			}, new CodeStatement[]
			{
				CodeGenHelper.Return(CodeGenHelper.Primitive(1))
			});
			codeMemberMethod2.Statements.Add(CodeGenHelper.If(CodeGenHelper.ReferenceEquals(CodeGenHelper.Variable(text10), CodeGenHelper.Variable(text11)), new CodeStatement[]
			{
				CodeGenHelper.Return(expr)
			}, new CodeStatement[]
			{
				codeStatement,
				codeConditionStatement2
			}));
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0008332C File Offset: 0x0008152C
		private void AddMatchTableAdapterConnection(CodeTypeDeclaration dataComponentClass)
		{
			string text = "inputConnection";
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(bool)), "MatchTableAdapterConnection", MemberAttributes.Family);
			CodeTypeReference type = CodeGenHelper.GlobalType(typeof(IDbConnection));
			CodeParameterDeclarationExpression value = CodeGenHelper.ParameterDecl(type, text);
			codeMemberMethod.Parameters.Add(value);
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField("_connection")), CodeGenHelper.Return(CodeGenHelper.Primitive(true))));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.Or(CodeGenHelper.IdIsNull(CodeGenHelper.ThisProperty("Connection")), CodeGenHelper.IdIsNull(CodeGenHelper.Argument(text))), CodeGenHelper.Return(CodeGenHelper.Primitive(true))));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(typeof(string)), "Equals", new CodeExpression[]
			{
				CodeGenHelper.Property(CodeGenHelper.ThisProperty("Connection"), "ConnectionString"),
				CodeGenHelper.Property(CodeGenHelper.Argument(text), "ConnectionString"),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(StringComparison)), "Ordinal")
			}), CodeGenHelper.Return(CodeGenHelper.Primitive(true))));
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Primitive(false)));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000834A4 File Offset: 0x000816A4
		private void AddUpdateAll(CodeTypeDeclaration dataComponentClass)
		{
			string text = "dataSet";
			string text2 = "backupDataSet";
			string deletedRowsStr = "deletedRows";
			string addedRowsStr = "addedRows";
			string updatedRowsStr = "updatedRows";
			string text3 = "result";
			string text4 = "workConnection";
			string text5 = "workTransaction";
			string text6 = "workConnOpened";
			string text7 = "allChangedRows";
			string text8 = "allAddedRows";
			string text9 = "adaptersWithAcceptChangesDuringUpdate";
			string text10 = "revertConnections";
			CodeExpression left = CodeGenHelper.Variable(text3);
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(int)), "UpdateAll", MemberAttributes.Public);
			string text11 = this.dataSourceType.Name;
			if (this.codeGenerator.DataSetNamespace != null)
			{
				text11 = CodeGenHelper.GetTypeName(this.codeGenerator.CodeProvider, this.codeGenerator.DataSetNamespace, text11);
			}
			CodeTypeReference type = CodeGenHelper.Type(text11);
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, text);
			codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
			codeMemberMethod.Comments.Add(CodeGenHelper.Comment("Update all changes to the dataset.", true));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNull(CodeGenHelper.Argument(text)), CodeGenHelper.Throw(CodeGenHelper.GlobalType(typeof(ArgumentNullException)), text)));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.Argument(text), "HasChanges"), CodeGenHelper.Primitive(false)), CodeGenHelper.Return(CodeGenHelper.Primitive(0))));
			foreach (object obj in this.dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				if (this.CanAddTableAdapter(designTable))
				{
					string tamadapterVarName = designTable.PropertyCache.TAMAdapterVarName;
					codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(tamadapterVarName)), CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.This(), "MatchTableAdapterConnection", CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName), "Connection")), CodeGenHelper.Primitive(false))), new CodeStatement[]
					{
						new CodeThrowExceptionStatement(CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(ArgumentException)), new CodeExpression[]
						{
							CodeGenHelper.Str(SR.GetString("CG_TableAdapterManagerNeedsSameConnString"))
						}))
					}));
				}
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(IDbConnection)), text4, CodeGenHelper.ThisProperty("Connection")));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNull(CodeGenHelper.Variable(text4)), CodeGenHelper.Throw(CodeGenHelper.GlobalType(typeof(ApplicationException)), SR.GetString("CG_TableAdapterManagerHasNoConnection"))));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(bool)), text6, CodeGenHelper.Primitive(false)));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.BitwiseAnd(CodeGenHelper.Property(CodeGenHelper.Variable(text4), "State"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Broken")), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Broken")), CodeGenHelper.MethodCallStm(CodeGenHelper.Variable(text4), "Close")));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.Variable(text4), "State"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(ConnectionState)), "Closed")), new CodeStatement[]
			{
				CodeGenHelper.MethodCallStm(CodeGenHelper.Variable(text4), "Open"),
				CodeGenHelper.Assign(CodeGenHelper.Variable(text6), CodeGenHelper.Primitive(true))
			}));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(IDbTransaction)), text5, CodeGenHelper.MethodCall(CodeGenHelper.Variable(text4), "BeginTransaction")));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.IdIsNull(CodeGenHelper.Variable(text5)), CodeGenHelper.Throw(CodeGenHelper.GlobalType(typeof(ApplicationException)), SR.GetString("CG_TableAdapterManagerNotSupportTransaction"))));
			CodeTypeReference type2 = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataRow));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(type2, text7, CodeGenHelper.New(type2, new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(type2, text8, CodeGenHelper.New(type2, new CodeExpression[0])));
			type2 = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataAdapter));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(type2, text9, CodeGenHelper.New(type2, new CodeExpression[0])));
			CodeTypeReference codeTypeReference = new CodeTypeReference("System.Collections.Generic.Dictionary", new CodeTypeReference[]
			{
				CodeGenHelper.GlobalType(typeof(object)),
				CodeGenHelper.GlobalType(typeof(IDbConnection))
			});
			codeTypeReference.Options = CodeTypeReferenceOptions.GlobalReference;
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(codeTypeReference, text10, CodeGenHelper.New(codeTypeReference, new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(typeof(int)), text3, CodeGenHelper.Primitive(0)));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataSet)), text2, CodeGenHelper.Primitive(null)));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.ThisProperty("BackupDataSetBeforeUpdate"), new CodeStatement[]
			{
				CodeGenHelper.Assign(CodeGenHelper.Variable(text2), CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(DataSet)), new CodeExpression[0])),
				CodeGenHelper.MethodCallStm(CodeGenHelper.Variable(text2), "Merge", CodeGenHelper.Argument(text))
			}));
			List<CodeStatement> list = new List<CodeStatement>();
			list.Add(new CodeCommentStatement("---- Prepare for update -----------\r\n"));
			foreach (object obj2 in this.dataSource.DesignTables)
			{
				DesignTable designTable2 = (DesignTable)obj2;
				if (this.CanAddTableAdapter(designTable2))
				{
					string tamadapterVarName2 = designTable2.PropertyCache.TAMAdapterVarName;
					CodeStatement codeStatement;
					if (designTable2.PropertyCache.TransactionType != null)
					{
						codeStatement = CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Transaction"), CodeGenHelper.Cast(CodeGenHelper.GlobalType(designTable2.PropertyCache.TransactionType), CodeGenHelper.Variable(text5)));
					}
					else
					{
						codeStatement = new CodeCommentStatement("Note: The TableAdapter does not have the Transaction property.");
					}
					CodeStatement codeStatement2;
					if (designTable2.PropertyCache.AdapterType != null && typeof(DataAdapter).IsAssignableFrom(designTable2.PropertyCache.AdapterType))
					{
						codeStatement2 = CodeGenHelper.If(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Adapter"), "AcceptChangesDuringUpdate"), new CodeStatement[]
						{
							CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Adapter"), "AcceptChangesDuringUpdate"), CodeGenHelper.Primitive(false)),
							CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text9), "Add", CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Adapter")))
						});
					}
					else
					{
						codeStatement2 = new CodeCommentStatement("Note: Adapter is not a DataAdapter, so AcceptChangesDuringUpdate cannot be set to false.");
					}
					list.Add(CodeGenHelper.If(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(tamadapterVarName2)), new CodeStatement[]
					{
						CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text10), "Add", new CodeExpression[]
						{
							CodeGenHelper.ThisField(tamadapterVarName2),
							CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Connection")
						})),
						CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName2), "Connection"), CodeGenHelper.Cast(CodeGenHelper.GlobalType(designTable2.PropertyCache.ConnectionType), CodeGenHelper.Variable(text4))),
						codeStatement,
						codeStatement2
					}));
				}
			}
			DataTable[] updateOrder = TableAdapterManagerHelper.GetUpdateOrder(this.dataSource.DataSet);
			this.AddUpdateUpdatedMethod(dataComponentClass, updateOrder, codeParameterDeclarationExpression, text, text3, updatedRowsStr, text7, text8);
			this.AddUpdateInsertedMethod(dataComponentClass, updateOrder, codeParameterDeclarationExpression, text, text3, addedRowsStr, text8);
			this.AddUpdateDeletedMethod(dataComponentClass, updateOrder, codeParameterDeclarationExpression, text, text3, deletedRowsStr, text7);
			this.AddRealUpdatedRowsMethod(dataComponentClass, updatedRowsStr, text8);
			list.Add(new CodeCommentStatement("\r\n---- Perform updates -----------\r\n"));
			CodeStatement codeStatement3 = CodeGenHelper.Assign(left, CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.Add, CodeGenHelper.MethodCall(CodeGenHelper.This(), "UpdateInsertedRows", new CodeExpression[]
			{
				CodeGenHelper.Argument(text),
				CodeGenHelper.Variable(text8)
			})));
			CodeStatement codeStatement4 = CodeGenHelper.Assign(left, CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.Add, CodeGenHelper.MethodCall(CodeGenHelper.This(), "UpdateUpdatedRows", new CodeExpression[]
			{
				CodeGenHelper.Argument(text),
				CodeGenHelper.Variable(text7),
				CodeGenHelper.Variable(text8)
			})));
			list.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.ThisProperty("UpdateOrder"), CodeGenHelper.Field(CodeGenHelper.TypeExpr(CodeGenHelper.Type("UpdateOrderOption")), "UpdateInsertDelete")), new CodeStatement[]
			{
				codeStatement4,
				codeStatement3
			}, new CodeStatement[]
			{
				codeStatement3,
				codeStatement4
			}));
			list.Add(CodeGenHelper.Assign(left, CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.Add, CodeGenHelper.MethodCall(CodeGenHelper.This(), "UpdateDeletedRows", new CodeExpression[]
			{
				CodeGenHelper.Argument(text),
				CodeGenHelper.Variable(text7)
			}))));
			list.Add(new CodeCommentStatement("\r\n---- Commit updates -----------\r\n"));
			list.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text5), "Commit")));
			list.Add(this.HandleForEachRowInList(text8, new string[]
			{
				"AcceptChanges"
			}));
			list.Add(this.HandleForEachRowInList(text7, new string[]
			{
				"AcceptChanges"
			}));
			CodeCatchClause codeCatchClause = new CodeCatchClause();
			codeCatchClause.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text5), "Rollback"));
			codeCatchClause.Statements.Add(new CodeCommentStatement("---- Restore the dataset -----------"));
			codeCatchClause.Statements.Add(CodeGenHelper.If(CodeGenHelper.ThisProperty("BackupDataSetBeforeUpdate"), new CodeStatement[]
			{
				CodeGenHelper.MethodCallStm(CodeGenHelper.GlobalTypeExpr(typeof(Debug)), "Assert", CodeGenHelper.IdIsNotNull(CodeGenHelper.Variable(text2))),
				CodeGenHelper.MethodCallStm(CodeGenHelper.Argument(text), "Clear"),
				CodeGenHelper.MethodCallStm(CodeGenHelper.Argument(text), "Merge", CodeGenHelper.Variable(text2))
			}, new CodeStatement[]
			{
				this.HandleForEachRowInList(text8, new string[]
				{
					"AcceptChanges",
					"SetAdded"
				})
			}));
			codeCatchClause.CatchExceptionType = CodeGenHelper.GlobalType(typeof(Exception));
			codeCatchClause.LocalName = "ex";
			codeCatchClause.Statements.Add(new CodeThrowExceptionStatement(CodeGenHelper.Variable("ex")));
			List<CodeStatement> list2 = new List<CodeStatement>();
			list2.Add(CodeGenHelper.If(CodeGenHelper.Variable(text6), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text4), "Close"))));
			foreach (object obj3 in this.dataSource.DesignTables)
			{
				DesignTable designTable3 = (DesignTable)obj3;
				if (this.CanAddTableAdapter(designTable3))
				{
					string tamadapterVarName3 = designTable3.PropertyCache.TAMAdapterVarName;
					CodeStatement codeStatement5;
					if (designTable3.PropertyCache.TransactionType != null)
					{
						codeStatement5 = CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName3), "Transaction"), CodeGenHelper.Primitive(null));
					}
					else
					{
						codeStatement5 = new CodeCommentStatement("Note: No Transaction property of the TableAdapter");
					}
					list2.Add(CodeGenHelper.If(CodeGenHelper.IdIsNotNull(CodeGenHelper.ThisField(tamadapterVarName3)), new CodeStatement[]
					{
						CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.ThisField(tamadapterVarName3), "Connection"), CodeGenHelper.Cast(CodeGenHelper.GlobalType(designTable3.PropertyCache.ConnectionType), CodeGenHelper.Indexer(CodeGenHelper.Variable(text10), CodeGenHelper.ThisField(tamadapterVarName3)))),
						codeStatement5
					}));
				}
			}
			list2.Add(this.RestoreAdaptersWithACDU(text9));
			codeMemberMethod.Statements.Add(CodeGenHelper.Try(list.ToArray(), new CodeCatchClause[]
			{
				codeCatchClause
			}, list2.ToArray()));
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(text3)));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000841B4 File Offset: 0x000823B4
		private void AddUpdateInsertedMethod(CodeTypeDeclaration dataComponentClass, DataTable[] orderedTables, CodeParameterDeclarationExpression dataSetPara, string dataSetStr, string resultStr, string addedRowsStr, string allAddedRowsStr)
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(int)), "UpdateInsertedRows", MemberAttributes.Private);
			CodeTypeReference type = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataRow));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, allAddedRowsStr);
			codeMemberMethod.Parameters.AddRange(new CodeParameterDeclarationExpression[]
			{
				dataSetPara,
				codeParameterDeclarationExpression
			});
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(typeof(int)), resultStr, CodeGenHelper.Primitive(0)));
			codeMemberMethod.Comments.Add(CodeGenHelper.Comment("Insert rows in top-down order.", true));
			for (int i = 0; i < orderedTables.Length; i++)
			{
				DesignTable table = this.dataSource.DesignTables[orderedTables[i]];
				if (this.CanAddTableAdapter(table))
				{
					codeMemberMethod.Statements.Add(this.AddUpdateAllTAUpdate(table, dataSetStr, resultStr, addedRowsStr, allAddedRowsStr, "Added", null));
				}
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(resultStr)));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000842D0 File Offset: 0x000824D0
		private void AddUpdateDeletedMethod(CodeTypeDeclaration dataComponentClass, DataTable[] orderedTables, CodeParameterDeclarationExpression dataSetPara, string dataSetStr, string resultStr, string deletedRowsStr, string allChangedRowsStr)
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(int)), "UpdateDeletedRows", MemberAttributes.Private);
			CodeTypeReference type = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataRow));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, allChangedRowsStr);
			codeMemberMethod.Parameters.AddRange(new CodeParameterDeclarationExpression[]
			{
				dataSetPara,
				codeParameterDeclarationExpression
			});
			codeMemberMethod.Comments.Add(CodeGenHelper.Comment("Delete rows in bottom-up order.", true));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(typeof(int)), resultStr, CodeGenHelper.Primitive(0)));
			for (int i = orderedTables.Length - 1; i >= 0; i--)
			{
				DesignTable table = this.dataSource.DesignTables[orderedTables[i]];
				if (this.CanAddTableAdapter(table))
				{
					codeMemberMethod.Statements.Add(this.AddUpdateAllTAUpdate(table, dataSetStr, resultStr, deletedRowsStr, allChangedRowsStr, "Deleted", null));
				}
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(resultStr)));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000843F0 File Offset: 0x000825F0
		private void AddUpdateUpdatedMethod(CodeTypeDeclaration dataComponentClass, DataTable[] orderedTables, CodeParameterDeclarationExpression dataSetPara, string dataSetStr, string resultStr, string updatedRowsStr, string allChangedRowsStr, string allAddedRowsStr)
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(int)), "UpdateUpdatedRows", MemberAttributes.Private);
			CodeTypeReference type = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataRow));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, allChangedRowsStr);
			CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = CodeGenHelper.ParameterDecl(type, allAddedRowsStr);
			codeMemberMethod.Parameters.AddRange(new CodeParameterDeclarationExpression[]
			{
				dataSetPara,
				codeParameterDeclarationExpression,
				codeParameterDeclarationExpression2
			});
			codeMemberMethod.Comments.Add(CodeGenHelper.Comment("Update rows in top-down order.", true));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(typeof(int)), resultStr, CodeGenHelper.Primitive(0)));
			for (int i = 0; i < orderedTables.Length; i++)
			{
				DesignTable table = this.dataSource.DesignTables[orderedTables[i]];
				if (this.CanAddTableAdapter(table))
				{
					codeMemberMethod.Statements.Add(this.AddUpdateAllTAUpdate(table, dataSetStr, resultStr, updatedRowsStr, allChangedRowsStr, "ModifiedCurrent", allAddedRowsStr));
				}
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable(resultStr)));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00084520 File Offset: 0x00082720
		private void AddRealUpdatedRowsMethod(CodeTypeDeclaration dataComponentClass, string updatedRowsStr, string allAddedRowsStr)
		{
			string text = "realUpdatedRows";
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(DataRow), 1), "GetRealUpdatedRows", MemberAttributes.Private);
			CodeTypeReference type = CodeGenHelper.GlobalGenericType("System.Collections.Generic.List", typeof(DataRow));
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = CodeGenHelper.ParameterDecl(type, allAddedRowsStr);
			CodeTypeReference type2 = CodeGenHelper.GlobalType(typeof(DataRow), 1);
			CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = CodeGenHelper.ParameterDecl(type2, updatedRowsStr);
			codeMemberMethod.Comments.Add(CodeGenHelper.Comment("Remove inserted rows that become updated rows after calling TableAdapter.Update(inserted rows) first", true));
			codeMemberMethod.Parameters.AddRange(new CodeParameterDeclarationExpression[]
			{
				codeParameterDeclarationExpression2,
				codeParameterDeclarationExpression
			});
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.Or(CodeGenHelper.IdIsNull(CodeGenHelper.Argument(updatedRowsStr)), CodeGenHelper.Less(CodeGenHelper.Property(CodeGenHelper.Argument(updatedRowsStr), "Length"), CodeGenHelper.Primitive(1))), CodeGenHelper.Return(CodeGenHelper.Variable(updatedRowsStr))));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.Or(CodeGenHelper.IdIsNull(CodeGenHelper.Argument(allAddedRowsStr)), CodeGenHelper.Less(CodeGenHelper.Property(CodeGenHelper.Argument(allAddedRowsStr), "Count"), CodeGenHelper.Primitive(1))), CodeGenHelper.Return(CodeGenHelper.Variable(updatedRowsStr))));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(type, text, CodeGenHelper.New(type, new CodeExpression[0])));
			string text2 = "row";
			CodeStatement[] forStms = new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), text2, CodeGenHelper.Indexer(CodeGenHelper.Variable(updatedRowsStr), CodeGenHelper.Variable("i"))),
				CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.Argument(allAddedRowsStr), "Contains", CodeGenHelper.Variable(text2)), CodeGenHelper.Primitive(false)), CodeGenHelper.MethodCallStm(CodeGenHelper.Variable(text), "Add", CodeGenHelper.Variable(text2)))
			};
			codeMemberMethod.Statements.Add(this.GetForLoopItoCount(CodeGenHelper.Property(CodeGenHelper.Argument(updatedRowsStr), "Length"), forStms));
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.Variable(text), "ToArray")));
			dataComponentClass.Members.Add(codeMemberMethod);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0008474C File Offset: 0x0008294C
		private CodeStatement AddUpdateAllTAUpdate(DesignTable table, string dataSetStr, string resultStr, string updateRowsStr, string allUpdateRowsStr, string rowState, string allAddedRowsStr)
		{
			string tamadapterVarName = table.PropertyCache.TAMAdapterVarName;
			CodeStatement[] array = new CodeStatement[]
			{
				CodeGenHelper.Assign(CodeGenHelper.Variable(resultStr), CodeGenHelper.BinOperator(CodeGenHelper.Variable(resultStr), CodeBinaryOperatorType.Add, CodeGenHelper.MethodCall(CodeGenHelper.ThisField(tamadapterVarName), "Update", CodeGenHelper.Variable(updateRowsStr)))),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(allUpdateRowsStr), "AddRange", CodeGenHelper.Variable(updateRowsStr)))
			};
			DataRelation[] selfRefRelations = TableAdapterManagerHelper.GetSelfRefRelations(table.DataTable);
			if (selfRefRelations != null && selfRefRelations.Length != 0)
			{
				bool flag = StringUtil.EqualValue("Deleted", rowState, true);
				List<CodeStatement> list = new List<CodeStatement>(array.Length + selfRefRelations.Length);
				for (int i = 0; i < selfRefRelations.Length; i++)
				{
					if (i > 0)
					{
						list.Add(new CodeCommentStatement("Note: More than one self-referenced relation found.  The generated code may not work correctly."));
					}
					list.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "SortSelfReferenceRows", new CodeExpression[]
					{
						CodeGenHelper.Variable(updateRowsStr),
						CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Argument(dataSetStr), "Relations"), CodeGenHelper.Str(selfRefRelations[i].RelationName)),
						CodeGenHelper.Primitive(flag)
					})));
				}
				list.AddRange(array);
				array = list.ToArray();
			}
			List<CodeStatement> list2 = new List<CodeStatement>(3);
			list2.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow), 1), updateRowsStr, CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Argument(dataSetStr), table.GeneratorTablePropName), "Select", new CodeExpression[]
			{
				CodeGenHelper.Primitive(null),
				CodeGenHelper.Primitive(null),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DataViewRowState)), rowState)
			})));
			if (StringUtil.NotEmptyAfterTrim(allAddedRowsStr))
			{
				list2.Add(CodeGenHelper.Assign(CodeGenHelper.Argument(updateRowsStr), CodeGenHelper.MethodCall(CodeGenHelper.This(), "GetRealUpdatedRows", new CodeExpression[]
				{
					CodeGenHelper.Argument(updateRowsStr),
					CodeGenHelper.Argument(allAddedRowsStr)
				})));
			}
			list2.Add(CodeGenHelper.If(CodeGenHelper.And(CodeGenHelper.IdNotEQ(CodeGenHelper.Variable(updateRowsStr), CodeGenHelper.Primitive(null)), CodeGenHelper.Less(CodeGenHelper.Primitive(0), CodeGenHelper.Property(CodeGenHelper.Variable(updateRowsStr), "Length"))), array));
			return CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.ThisField(tamadapterVarName), CodeGenHelper.Primitive(null)), list2.ToArray());
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000849A8 File Offset: 0x00082BA8
		private void AddVariableAndProperty(CodeTypeDeclaration codeType, MemberAttributes memberAttributes, CodeTypeReference propertyType, string propertyName, string variableName, bool getOnly)
		{
			codeType.Members.Add(CodeGenHelper.FieldDecl(propertyType, variableName));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(propertyType, propertyName, memberAttributes);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.ThisField(variableName)));
			if (!getOnly)
			{
				codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.ThisField(variableName), CodeGenHelper.Argument("value")));
			}
			codeType.Members.Add(codeMemberProperty);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00084A20 File Offset: 0x00082C20
		private bool CanAddTableAdapter(DesignTable table)
		{
			if (table != null && table.HasAnyUpdateCommand)
			{
				MemberAttributes memberAttributes = ((DesignConnection)table.Connection).Modifier & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.FamilyOrAssembly || memberAttributes == MemberAttributes.Assembly || memberAttributes == MemberAttributes.Public || memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00084A74 File Offset: 0x00082C74
		private CodeStatement RestoreAdaptersWithACDU(string listStr)
		{
			CodeStatement[] forStms = new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataAdapter)), "adapter", CodeGenHelper.Indexer(CodeGenHelper.Variable("adapters"), CodeGenHelper.Variable("i"))),
				CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("adapter"), "AcceptChangesDuringUpdate"), CodeGenHelper.Primitive(true))
			};
			return CodeGenHelper.If(CodeGenHelper.Less(CodeGenHelper.Primitive(0), CodeGenHelper.Property(CodeGenHelper.Variable(listStr), "Count")), new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataAdapter), 1), "adapters", this.NewArray(CodeGenHelper.GlobalType(typeof(DataAdapter), 1), CodeGenHelper.Property(CodeGenHelper.Variable(listStr), "Count"))),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(listStr), "CopyTo", CodeGenHelper.Variable("adapters"))),
				this.GetForLoopItoCount(CodeGenHelper.Property(CodeGenHelper.Variable("adapters"), "Length"), forStms)
			});
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00084B94 File Offset: 0x00082D94
		private CodeStatement HandleForEachRowInList(string listStr, string[] methods)
		{
			CodeStatement[] array = new CodeStatement[methods.Length + 1];
			array[0] = CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow)), "row", CodeGenHelper.Indexer(CodeGenHelper.Variable("rows"), CodeGenHelper.Variable("i")));
			for (int i = 0; i < methods.Length; i++)
			{
				array[i + 1] = CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("row"), methods[i]));
			}
			return CodeGenHelper.If(CodeGenHelper.Less(CodeGenHelper.Primitive(0), CodeGenHelper.Property(CodeGenHelper.Variable(listStr), "Count")), new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataRow), 1), "rows", this.NewArray(CodeGenHelper.GlobalType(typeof(DataRow), 1), CodeGenHelper.Property(CodeGenHelper.Variable(listStr), "Count"))),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable(listStr), "CopyTo", CodeGenHelper.Variable("rows"))),
				this.GetForLoopItoCount(CodeGenHelper.Property(CodeGenHelper.Variable("rows"), "Length"), array)
			});
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00084CBB File Offset: 0x00082EBB
		private CodeStatement GetForLoopItoCount(CodeExpression countExp, CodeStatement[] forStms)
		{
			return this.GetForLoopItoCount("i", countExp, forStms);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00084CCC File Offset: 0x00082ECC
		private CodeStatement GetForLoopItoCount(string iStr, CodeExpression countExp, CodeStatement[] forStms)
		{
			CodeStatement initStmt = CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(int)), iStr, CodeGenHelper.Primitive(0));
			CodeStatement incrementStmt = CodeGenHelper.Assign(CodeGenHelper.Variable(iStr), CodeGenHelper.BinOperator(CodeGenHelper.Variable(iStr), CodeBinaryOperatorType.Add, CodeGenHelper.Primitive(1)));
			CodeExpression testExpression = CodeGenHelper.Less(CodeGenHelper.Variable(iStr), countExp);
			return CodeGenHelper.ForLoop(initStmt, testExpression, incrementStmt, forStms);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00084D33 File Offset: 0x00082F33
		private CodeExpression NewArray(CodeTypeReference type, CodeExpression size)
		{
			return new CodeArrayCreateExpression(type, size);
		}

		// Token: 0x04000C0C RID: 3084
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000C0D RID: 3085
		private DesignDataSource dataSource;

		// Token: 0x04000C0E RID: 3086
		private CodeTypeDeclaration dataSourceType;

		// Token: 0x04000C0F RID: 3087
		private TableAdapterManagerNameHandler nameHandler;

		// Token: 0x04000C10 RID: 3088
		private const string adapterPropertyEditor = "Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor";
	}
}
