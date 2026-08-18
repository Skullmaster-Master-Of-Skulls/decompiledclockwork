using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data.Design
{
	// Token: 0x02000220 RID: 544
	internal sealed class DatasetMethodGenerator
	{
		// Token: 0x0600142F RID: 5167 RVA: 0x000727F1 File Offset: 0x000709F1
		internal DatasetMethodGenerator(TypedDataSourceCodeGenerator codeGenerator, DesignDataSource dataSource)
		{
			this.codeGenerator = codeGenerator;
			this.dataSource = dataSource;
			this.dataSet = dataSource.DataSet;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00072814 File Offset: 0x00070A14
		internal void AddMethods(CodeTypeDeclaration dataSourceClass)
		{
			this.AddSchemaSerializationModeMembers(dataSourceClass);
			this.initExpressionsMethod = this.InitExpressionsMethod();
			dataSourceClass.Members.Add(this.PublicConstructor());
			dataSourceClass.Members.Add(this.DeserializingConstructor());
			dataSourceClass.Members.Add(this.InitializeDerivedDataSet());
			dataSourceClass.Members.Add(this.CloneMethod(this.initExpressionsMethod));
			dataSourceClass.Members.Add(this.ShouldSerializeTablesMethod());
			dataSourceClass.Members.Add(this.ShouldSerializeRelationsMethod());
			dataSourceClass.Members.Add(this.ReadXmlSerializableMethod());
			dataSourceClass.Members.Add(this.GetSchemaSerializableMethod());
			dataSourceClass.Members.Add(this.InitVarsParamLess());
			CodeMemberMethod value = null;
			CodeMemberMethod value2 = null;
			this.InitClassAndInitVarsMethods(out value, out value2);
			dataSourceClass.Members.Add(value2);
			dataSourceClass.Members.Add(value);
			this.AddShouldSerializeSingleTableMethods(dataSourceClass);
			dataSourceClass.Members.Add(this.SchemaChangedMethod());
			dataSourceClass.Members.Add(this.GetTypedDataSetSchema());
			dataSourceClass.Members.Add(this.TablesProperty());
			dataSourceClass.Members.Add(this.RelationsProperty());
			if (this.initExpressionsMethod != null)
			{
				dataSourceClass.Members.Add(this.initExpressionsMethod);
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00072970 File Offset: 0x00070B70
		private void AddSchemaSerializationModeMembers(CodeTypeDeclaration dataSourceClass)
		{
			CodeMemberField value = CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(SchemaSerializationMode)), "_schemaSerializationMode", CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(SchemaSerializationMode)), this.dataSource.SchemaSerializationMode.ToString()));
			dataSourceClass.Members.Add(value);
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(SchemaSerializationMode)), "SchemaSerializationMode", (MemberAttributes)24580);
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(BrowsableAttribute).FullName, CodeGenHelper.Primitive(true)));
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(DesignerSerializationVisibilityAttribute).FullName, CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DesignerSerializationVisibility)), "Visible")));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), "_schemaSerializationMode")));
			codeMemberProperty.SetStatements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), "_schemaSerializationMode"), CodeGenHelper.Argument("value")));
			dataSourceClass.Members.Add(codeMemberProperty);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00072AAC File Offset: 0x00070CAC
		private CodeConstructor PublicConstructor()
		{
			CodeConstructor codeConstructor = CodeGenHelper.Constructor(MemberAttributes.Public);
			codeConstructor.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "BeginInit"));
			codeConstructor.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitClass"));
			codeConstructor.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), "schemaChangedHandler", new CodeDelegateCreateExpression(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), CodeGenHelper.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.Base(), "Relations"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "EndInit"));
			if (this.initExpressionsMethod != null)
			{
				codeConstructor.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitExpressions"));
			}
			return codeConstructor;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00072BE4 File Offset: 0x00070DE4
		private CodeConstructor DeserializingConstructor()
		{
			CodeConstructor codeConstructor = CodeGenHelper.Constructor(MemberAttributes.Family);
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(SerializationInfo)), "info"));
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(StreamingContext)), "context"));
			codeConstructor.BaseConstructorArgs.AddRange(new CodeExpression[]
			{
				CodeGenHelper.Argument("info"),
				CodeGenHelper.Argument("context"),
				CodeGenHelper.Primitive(false)
			});
			List<CodeStatement> list = new List<CodeStatement>();
			list.AddRange(new CodeStatement[]
			{
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars", CodeGenHelper.Primitive(false))),
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), "schemaChangedHandler1", new CodeDelegateCreateExpression(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), CodeGenHelper.This(), "SchemaChanged")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.This(), "Tables"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler1")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.This(), "Relations"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler1"))
			});
			if (this.initExpressionsMethod != null)
			{
				list.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.This(), "DetermineSchemaSerializationMode", new CodeExpression[]
				{
					CodeGenHelper.Argument("info"),
					CodeGenHelper.Argument("context")
				}), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(SchemaSerializationMode)), "ExcludeSchema")), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitExpressions"))));
			}
			list.Add(CodeGenHelper.Return());
			codeConstructor.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.This(), "IsBinarySerialized", new CodeExpression[]
			{
				CodeGenHelper.Argument("info"),
				CodeGenHelper.Argument("context")
			}), CodeGenHelper.Primitive(true)), list.ToArray()));
			codeConstructor.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(string)), "strSchema", CodeGenHelper.Cast(CodeGenHelper.GlobalType(typeof(string)), CodeGenHelper.MethodCall(CodeGenHelper.Argument("info"), "GetValue", new CodeExpression[]
			{
				CodeGenHelper.Str("XmlSchema"),
				CodeGenHelper.TypeOf(CodeGenHelper.GlobalType(typeof(string)))
			}))));
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			arrayList.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataSet)), "ds", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(DataSet)), new CodeExpression[0])));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("ds"), "ReadXmlSchema", new CodeExpression[]
			{
				CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlTextReader)), new CodeExpression[]
				{
					CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(StringReader)), new CodeExpression[]
					{
						CodeGenHelper.Variable("strSchema")
					})
				})
			})));
			foreach (object obj in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable designTable = (DesignTable)obj;
				arrayList.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Tables"), CodeGenHelper.Str(designTable.Name)), CodeGenHelper.Primitive(null)), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), "Add", CodeGenHelper.New(CodeGenHelper.Type(designTable.GeneratorTableClassName), new CodeExpression[]
				{
					CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Tables"), CodeGenHelper.Str(designTable.Name))
				})))));
			}
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "DataSetName"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "DataSetName")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Prefix"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Prefix")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Namespace"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Namespace")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Locale"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Locale")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "CaseSensitive"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "CaseSensitive")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "EnforceConstraints"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "EnforceConstraints")));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "Merge", new CodeExpression[]
			{
				CodeGenHelper.Variable("ds"),
				CodeGenHelper.Primitive(false),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(MissingSchemaAction)), "Add")
			})));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars")));
			arrayList2.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "ReadXmlSchema", new CodeExpression[]
			{
				CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlTextReader)), new CodeExpression[]
				{
					CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(StringReader)), new CodeExpression[]
					{
						CodeGenHelper.Variable("strSchema")
					})
				})
			})));
			if (this.initExpressionsMethod != null)
			{
				arrayList2.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitExpressions")));
			}
			codeConstructor.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.This(), "DetermineSchemaSerializationMode", new CodeExpression[]
			{
				CodeGenHelper.Argument("info"),
				CodeGenHelper.Argument("context")
			}), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(SchemaSerializationMode)), "IncludeSchema")), (CodeStatement[])arrayList.ToArray(typeof(CodeStatement)), (CodeStatement[])arrayList2.ToArray(typeof(CodeStatement))));
			codeConstructor.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "GetSerializationData", new CodeExpression[]
			{
				CodeGenHelper.Argument("info"),
				CodeGenHelper.Argument("context")
			}));
			codeConstructor.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), "schemaChangedHandler", new CodeDelegateCreateExpression(CodeGenHelper.GlobalType(typeof(CollectionChangeEventHandler)), CodeGenHelper.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(CodeGenHelper.Property(CodeGenHelper.This(), "Relations"), "CollectionChanged"), CodeGenHelper.Variable("schemaChangedHandler")));
			return codeConstructor;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000733C0 File Offset: 0x000715C0
		private CodeMemberMethod InitializeDerivedDataSet()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "InitializeDerivedDataSet", (MemberAttributes)12292);
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "BeginInit"));
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitClass"));
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "EndInit"));
			return codeMemberMethod;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00073440 File Offset: 0x00071640
		private CodeMemberMethod CloneMethod(CodeMemberMethod initExpressionsMethod)
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(DataSet)), "Clone", (MemberAttributes)24580);
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(this.codeGenerator.DataSourceName), "cln", CodeGenHelper.Cast(CodeGenHelper.Type(this.codeGenerator.DataSourceName), CodeGenHelper.MethodCall(CodeGenHelper.Base(), "Clone", new CodeExpression[0]))));
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Variable("cln"), "InitVars", new CodeExpression[0]));
			if (initExpressionsMethod != null)
			{
				codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Variable("cln"), "InitExpressions", new CodeExpression[0]));
			}
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("cln"), "SchemaSerializationMode"), CodeGenHelper.Property(CodeGenHelper.This(), "SchemaSerializationMode")));
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable("cln")));
			return codeMemberMethod;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0007355C File Offset: 0x0007175C
		private CodeMemberMethod ShouldSerializeTablesMethod()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(bool)), "ShouldSerializeTables", (MemberAttributes)12292);
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Primitive(false)));
			return codeMemberMethod;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000735A8 File Offset: 0x000717A8
		private CodeMemberMethod ShouldSerializeRelationsMethod()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(bool)), "ShouldSerializeRelations", (MemberAttributes)12292);
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Primitive(false)));
			return codeMemberMethod;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000735F4 File Offset: 0x000717F4
		private CodeMemberMethod ReadXmlSerializableMethod()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "ReadXmlSerializable", (MemberAttributes)12292);
			codeMemberMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(XmlReader)), "reader"));
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "Reset", new CodeExpression[0])));
			arrayList.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(DataSet)), "ds", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(DataSet)), new CodeExpression[0])));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("ds"), "ReadXml", new CodeExpression[]
			{
				CodeGenHelper.Argument("reader")
			})));
			foreach (object obj in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable designTable = (DesignTable)obj;
				arrayList.Add(CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Tables"), CodeGenHelper.Str(designTable.Name)), CodeGenHelper.Primitive(null)), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), "Add", CodeGenHelper.New(CodeGenHelper.Type(designTable.GeneratorTableClassName), new CodeExpression[]
				{
					CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Tables"), CodeGenHelper.Str(designTable.Name))
				})))));
			}
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "DataSetName"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "DataSetName")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Prefix"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Prefix")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Namespace"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Namespace")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Locale"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Locale")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "CaseSensitive"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "CaseSensitive")));
			arrayList.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "EnforceConstraints"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "EnforceConstraints")));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "Merge", new CodeExpression[]
			{
				CodeGenHelper.Variable("ds"),
				CodeGenHelper.Primitive(false),
				CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(MissingSchemaAction)), "Add")
			})));
			arrayList.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars")));
			arrayList2.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "ReadXml", new CodeExpression[]
			{
				CodeGenHelper.Argument("reader")
			})));
			arrayList2.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars")));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.This(), "DetermineSchemaSerializationMode", new CodeExpression[]
			{
				CodeGenHelper.Argument("reader")
			}), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(SchemaSerializationMode)), "IncludeSchema")), (CodeStatement[])arrayList.ToArray(typeof(CodeStatement)), (CodeStatement[])arrayList2.ToArray(typeof(CodeStatement))));
			return codeMemberMethod;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00073A24 File Offset: 0x00071C24
		private CodeMemberMethod GetSchemaSerializableMethod()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(XmlSchema)), "GetSchemaSerializable", (MemberAttributes)12292);
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(MemoryStream)), "stream", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(MemoryStream)), new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "WriteXmlSchema", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlTextWriter)), new CodeExpression[]
			{
				CodeGenHelper.Argument("stream"),
				CodeGenHelper.Primitive(null)
			})));
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Argument("stream"), "Position"), CodeGenHelper.Primitive(0)));
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(typeof(XmlSchema)), "Read", new CodeExpression[]
			{
				CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlTextReader)), new CodeExpression[]
				{
					CodeGenHelper.Argument("stream")
				}),
				CodeGenHelper.Primitive(null)
			})));
			return codeMemberMethod;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00073B70 File Offset: 0x00071D70
		private CodeMemberMethod InitVarsParamLess()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "InitVars", (MemberAttributes)4098);
			codeMemberMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars", new CodeExpression[]
			{
				CodeGenHelper.Primitive(true)
			}));
			return codeMemberMethod;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00073BCC File Offset: 0x00071DCC
		private void InitClassAndInitVarsMethods(out CodeMemberMethod initClassMethod, out CodeMemberMethod initVarsMethod)
		{
			initClassMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "InitClass", MemberAttributes.Private);
			initVarsMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "InitVars", (MemberAttributes)4098);
			initVarsMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(bool)), "initTable"));
			initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "DataSetName"), CodeGenHelper.Str(this.dataSet.DataSetName)));
			initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Prefix"), CodeGenHelper.Str(this.dataSet.Prefix)));
			if (DatasetMethodGenerator.namespaceProperty.ShouldSerializeValue(this.dataSet))
			{
				initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Namespace"), CodeGenHelper.Str(this.dataSet.Namespace)));
			}
			if (DatasetMethodGenerator.localeProperty.ShouldSerializeValue(this.dataSet))
			{
				initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "Locale"), CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(CultureInfo)), new CodeExpression[]
				{
					CodeGenHelper.Str(this.dataSet.Locale.ToString())
				})));
			}
			if (DatasetMethodGenerator.caseSensitiveProperty.ShouldSerializeValue(this.dataSet))
			{
				initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "CaseSensitive"), CodeGenHelper.Primitive(this.dataSet.CaseSensitive)));
			}
			initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "EnforceConstraints"), CodeGenHelper.Primitive(this.dataSet.EnforceConstraints)));
			initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.This(), "SchemaSerializationMode"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(SchemaSerializationMode)), this.dataSource.SchemaSerializationMode.ToString())));
			foreach (object obj in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable designTable = (DesignTable)obj;
				CodeExpression codeExpression = CodeGenHelper.Field(CodeGenHelper.This(), designTable.GeneratorTableVarName);
				if (this.TableContainsExpressions(designTable))
				{
					initClassMethod.Statements.Add(CodeGenHelper.Assign(codeExpression, CodeGenHelper.New(CodeGenHelper.Type(designTable.GeneratorTableClassName), new CodeExpression[]
					{
						CodeGenHelper.Primitive(false)
					})));
				}
				else
				{
					initClassMethod.Statements.Add(CodeGenHelper.Assign(codeExpression, CodeGenHelper.New(CodeGenHelper.Type(designTable.GeneratorTableClassName), new CodeExpression[0])));
				}
				initClassMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), "Add", codeExpression));
				initVarsMethod.Statements.Add(CodeGenHelper.Assign(codeExpression, CodeGenHelper.Cast(CodeGenHelper.Type(designTable.GeneratorTableClassName), CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables"), CodeGenHelper.Str(designTable.Name)))));
				initVarsMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Variable("initTable"), CodeGenHelper.Primitive(true)), new CodeStatement[]
				{
					CodeGenHelper.If(CodeGenHelper.IdNotEQ(codeExpression, CodeGenHelper.Primitive(null)), CodeGenHelper.Stm(CodeGenHelper.MethodCall(codeExpression, "InitVars")))
				}));
			}
			CodeExpression codeExpression2 = null;
			foreach (object obj2 in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable designTable2 = (DesignTable)obj2;
				DataTable dataTable = designTable2.DataTable;
				foreach (object obj3 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj3;
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
						CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression(CodeGenHelper.GlobalType(typeof(DataColumn)), 0);
						foreach (DataColumn dataColumn in foreignKeyConstraint.Columns)
						{
							codeArrayCreateExpression.Initializers.Add(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), this.codeGenerator.TableHandler.Tables[dataColumn.Table.TableName].GeneratorTableVarName), this.codeGenerator.TableHandler.Tables[dataColumn.Table.TableName].DesignColumns[dataColumn.ColumnName].GeneratorColumnPropNameInTable));
						}
						CodeArrayCreateExpression codeArrayCreateExpression2 = new CodeArrayCreateExpression(CodeGenHelper.GlobalType(typeof(DataColumn)), 0);
						foreach (DataColumn dataColumn2 in foreignKeyConstraint.RelatedColumns)
						{
							codeArrayCreateExpression2.Initializers.Add(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), this.codeGenerator.TableHandler.Tables[dataColumn2.Table.TableName].GeneratorTableVarName), this.codeGenerator.TableHandler.Tables[dataColumn2.Table.TableName].DesignColumns[dataColumn2.ColumnName].GeneratorColumnPropNameInTable));
						}
						if (codeExpression2 == null)
						{
							initClassMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(ForeignKeyConstraint)), "fkc"));
							codeExpression2 = CodeGenHelper.Variable("fkc");
						}
						initClassMethod.Statements.Add(CodeGenHelper.Assign(codeExpression2, CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(ForeignKeyConstraint)), new CodeExpression[]
						{
							CodeGenHelper.Str(foreignKeyConstraint.ConstraintName),
							codeArrayCreateExpression2,
							codeArrayCreateExpression
						})));
						initClassMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), this.codeGenerator.TableHandler.Tables[dataTable.TableName].GeneratorTableVarName), "Constraints"), "Add", codeExpression2));
						string field = foreignKeyConstraint.AcceptRejectRule.ToString();
						string field2 = foreignKeyConstraint.DeleteRule.ToString();
						string field3 = foreignKeyConstraint.UpdateRule.ToString();
						initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(codeExpression2, "AcceptRejectRule"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(foreignKeyConstraint.AcceptRejectRule.GetType()), field)));
						initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(codeExpression2, "DeleteRule"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(foreignKeyConstraint.DeleteRule.GetType()), field2)));
						initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(codeExpression2, "UpdateRule"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(foreignKeyConstraint.UpdateRule.GetType()), field3)));
					}
				}
			}
			foreach (object obj4 in this.codeGenerator.RelationHandler.Relations)
			{
				DesignRelation designRelation = (DesignRelation)obj4;
				DataRelation dataRelation = designRelation.DataRelation;
				if (dataRelation != null)
				{
					CodeArrayCreateExpression codeArrayCreateExpression3 = new CodeArrayCreateExpression(CodeGenHelper.GlobalType(typeof(DataColumn)), 0);
					string generatorTableVarName = designRelation.ParentDesignTable.GeneratorTableVarName;
					foreach (DataColumn dataColumn3 in dataRelation.ParentColumns)
					{
						codeArrayCreateExpression3.Initializers.Add(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), generatorTableVarName), this.codeGenerator.TableHandler.Tables[dataColumn3.Table.TableName].DesignColumns[dataColumn3.ColumnName].GeneratorColumnPropNameInTable));
					}
					CodeArrayCreateExpression codeArrayCreateExpression4 = new CodeArrayCreateExpression(CodeGenHelper.GlobalType(typeof(DataColumn)), 0);
					string generatorTableVarName2 = designRelation.ChildDesignTable.GeneratorTableVarName;
					foreach (DataColumn dataColumn4 in dataRelation.ChildColumns)
					{
						codeArrayCreateExpression4.Initializers.Add(CodeGenHelper.Property(CodeGenHelper.Field(CodeGenHelper.This(), generatorTableVarName2), this.codeGenerator.TableHandler.Tables[dataColumn4.Table.TableName].DesignColumns[dataColumn4.ColumnName].GeneratorColumnPropNameInTable));
					}
					CodeExpression codeExpression3 = CodeGenHelper.Field(CodeGenHelper.This(), this.codeGenerator.RelationHandler.Relations[dataRelation.RelationName].GeneratorRelationVarName);
					initClassMethod.Statements.Add(CodeGenHelper.Assign(codeExpression3, CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(DataRelation)), new CodeExpression[]
					{
						CodeGenHelper.Str(dataRelation.RelationName),
						codeArrayCreateExpression3,
						codeArrayCreateExpression4,
						CodeGenHelper.Primitive(false)
					})));
					if (dataRelation.Nested)
					{
						initClassMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(codeExpression3, "Nested"), CodeGenHelper.Primitive(true)));
					}
					ExtendedPropertiesHandler.CodeGenerator = this.codeGenerator;
					ExtendedPropertiesHandler.AddExtendedProperties(designRelation, codeExpression3, initClassMethod.Statements, dataRelation.ExtendedProperties);
					initClassMethod.Statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.This(), "Relations"), "Add", codeExpression3));
					initVarsMethod.Statements.Add(CodeGenHelper.Assign(codeExpression3, CodeGenHelper.Indexer(CodeGenHelper.Property(CodeGenHelper.This(), "Relations"), CodeGenHelper.Str(dataRelation.RelationName))));
				}
			}
			ExtendedPropertiesHandler.CodeGenerator = this.codeGenerator;
			ExtendedPropertiesHandler.AddExtendedProperties(this.dataSource, CodeGenHelper.This(), initClassMethod.Statements, this.dataSet.ExtendedProperties);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000746DC File Offset: 0x000728DC
		private void AddShouldSerializeSingleTableMethods(CodeTypeDeclaration dataSourceClass)
		{
			foreach (object obj in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable designTable = (DesignTable)obj;
				string text = designTable.GeneratorTablePropName;
				if (this.codeGenerator.CodeProvider.IsValidIdentifier(text) && text.StartsWith("[", StringComparison.Ordinal) && text.EndsWith("]", StringComparison.Ordinal))
				{
					text = text.Substring(1, text.Length - 2);
				}
				string name = MemberNameValidator.GenerateIdName("ShouldSerialize" + text, this.codeGenerator.CodeProvider, false);
				CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(bool)), name, MemberAttributes.Private);
				codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Primitive(false)));
				dataSourceClass.Members.Add(codeMemberMethod);
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000747EC File Offset: 0x000729EC
		private CodeMemberMethod SchemaChangedMethod()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "SchemaChanged", MemberAttributes.Private);
			codeMemberMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(object)), "sender"));
			codeMemberMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(CollectionChangeEventArgs)), "e"));
			codeMemberMethod.Statements.Add(CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.Argument("e"), "Action"), CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(CollectionChangeAction)), "Remove")), CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.This(), "InitVars"))));
			return codeMemberMethod;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x000748BC File Offset: 0x00072ABC
		internal static void GetSchemaIsInCollection(CodeStatementCollection statements, string dsName, string collectionName)
		{
			CodeStatement[] trueStms = new CodeStatement[]
			{
				CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Position"), CodeGenHelper.Primitive(0)),
				CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("s2"), "Position"), CodeGenHelper.Primitive(0)),
				CodeGenHelper.ForLoop(CodeGenHelper.Stm(new CodeSnippetExpression("")), CodeGenHelper.And(CodeGenHelper.IdNotEQ(CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Position"), CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Length")), CodeGenHelper.EQ(CodeGenHelper.MethodCall(CodeGenHelper.Variable("s1"), "ReadByte", new CodeExpression[0]), CodeGenHelper.MethodCall(CodeGenHelper.Variable("s2"), "ReadByte", new CodeExpression[0]))), CodeGenHelper.Stm(new CodeSnippetExpression("")), new CodeStatement[]
				{
					CodeGenHelper.Stm(new CodeSnippetExpression(""))
				}),
				CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Position"), CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Length")), new CodeStatement[]
				{
					CodeGenHelper.Return(CodeGenHelper.Variable("type"))
				})
			};
			CodeStatement[] statements2 = new CodeStatement[]
			{
				CodeGenHelper.Assign(CodeGenHelper.Variable("schema"), CodeGenHelper.Cast(CodeGenHelper.GlobalType(typeof(XmlSchema)), CodeGenHelper.Property(CodeGenHelper.Variable("schemas"), "Current"))),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("s2"), "SetLength", new CodeExpression[]
				{
					CodeGenHelper.Primitive(0)
				})),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("schema"), "Write", new CodeExpression[]
				{
					CodeGenHelper.Variable("s2")
				})),
				CodeGenHelper.If(CodeGenHelper.EQ(CodeGenHelper.Property(CodeGenHelper.Variable("s1"), "Length"), CodeGenHelper.Property(CodeGenHelper.Variable("s2"), "Length")), trueStms)
			};
			CodeStatement[] tryStmnts = new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(XmlSchema)), "schema", CodeGenHelper.Primitive(null)),
				CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("dsSchema"), "Write", new CodeExpression[]
				{
					CodeGenHelper.Variable("s1")
				})),
				CodeGenHelper.ForLoop(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(IEnumerator)), "schemas", CodeGenHelper.MethodCall(CodeGenHelper.MethodCall(CodeGenHelper.Variable(collectionName), "Schemas", new CodeExpression[]
				{
					CodeGenHelper.Property(CodeGenHelper.Variable("dsSchema"), "TargetNamespace")
				}), "GetEnumerator", new CodeExpression[0])), CodeGenHelper.MethodCall(CodeGenHelper.Variable("schemas"), "MoveNext", new CodeExpression[0]), CodeGenHelper.Stm(new CodeSnippetExpression("")), statements2)
			};
			CodeStatement[] finallyStmnts = new CodeStatement[]
			{
				CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Variable("s1"), CodeGenHelper.Primitive(null)), new CodeStatement[]
				{
					CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("s1"), "Close", new CodeExpression[0]))
				}),
				CodeGenHelper.If(CodeGenHelper.IdNotEQ(CodeGenHelper.Variable("s2"), CodeGenHelper.Primitive(null)), new CodeStatement[]
				{
					CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Variable("s2"), "Close", new CodeExpression[0]))
				})
			};
			CodeStatement[] trueStms2 = new CodeStatement[]
			{
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(MemoryStream)), "s1", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(MemoryStream)), new CodeExpression[0])),
				CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(MemoryStream)), "s2", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(MemoryStream)), new CodeExpression[0])),
				CodeGenHelper.Try(tryStmnts, new CodeCatchClause[0], finallyStmnts)
			};
			statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(XmlSchema)), "dsSchema", CodeGenHelper.MethodCall(CodeGenHelper.Variable(dsName), "GetSchemaSerializable", new CodeExpression[0])));
			statements.Add(CodeGenHelper.If(CodeGenHelper.MethodCall(CodeGenHelper.Variable(collectionName), "Contains", new CodeExpression[]
			{
				CodeGenHelper.Property(CodeGenHelper.Variable("dsSchema"), "TargetNamespace")
			}), trueStms2));
			statements.Add(CodeGenHelper.MethodCall(CodeGenHelper.Argument("xs"), "Add", new CodeExpression[]
			{
				CodeGenHelper.Variable("dsSchema")
			}));
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00074D80 File Offset: 0x00072F80
		private CodeMemberMethod GetTypedDataSetSchema()
		{
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(XmlSchemaComplexType)), "GetTypedDataSetSchema", (MemberAttributes)24579);
			codeMemberMethod.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(XmlSchemaSet)), "xs"));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.Type(this.dataSource.GeneratorDataSetName), "ds", CodeGenHelper.New(CodeGenHelper.Type(this.dataSource.GeneratorDataSetName), new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(XmlSchemaComplexType)), "type", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlSchemaComplexType)), new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(XmlSchemaSequence)), "sequence", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlSchemaSequence)), new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.VariableDecl(CodeGenHelper.GlobalType(typeof(XmlSchemaAny)), "any", CodeGenHelper.New(CodeGenHelper.GlobalType(typeof(XmlSchemaAny)), new CodeExpression[0])));
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("any"), "Namespace"), CodeGenHelper.Property(CodeGenHelper.Variable("ds"), "Namespace")));
			codeMemberMethod.Statements.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(CodeGenHelper.Variable("sequence"), "Items"), "Add", new CodeExpression[]
			{
				CodeGenHelper.Variable("any")
			})));
			codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(CodeGenHelper.Variable("type"), "Particle"), CodeGenHelper.Variable("sequence")));
			DatasetMethodGenerator.GetSchemaIsInCollection(codeMemberMethod.Statements, "ds", "xs");
			codeMemberMethod.Statements.Add(CodeGenHelper.Return(CodeGenHelper.Variable("type")));
			return codeMemberMethod;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00074FAC File Offset: 0x000731AC
		private CodeMemberProperty TablesProperty()
		{
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(DataTableCollection)), DataSourceNameHandler.TablesPropertyName, (MemberAttributes)24594);
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerSerializationVisibilityAttribute", CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DesignerSerializationVisibility)), "Hidden")));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Property(CodeGenHelper.Base(), "Tables")));
			return codeMemberProperty;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00075028 File Offset: 0x00073228
		private CodeMemberProperty RelationsProperty()
		{
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(DataRelationCollection)), DataSourceNameHandler.RelationsPropertyName, (MemberAttributes)24594);
			codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerSerializationVisibilityAttribute", CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DesignerSerializationVisibility)), "Hidden")));
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Property(CodeGenHelper.Base(), "Relations")));
			return codeMemberProperty;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000750A4 File Offset: 0x000732A4
		private CodeMemberMethod InitExpressionsMethod()
		{
			bool flag = false;
			CodeMemberMethod codeMemberMethod = CodeGenHelper.MethodDecl(CodeGenHelper.GlobalType(typeof(void)), "InitExpressions", MemberAttributes.Private);
			foreach (object obj in this.dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				DataTable dataTable = designTable.DataTable;
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (dataColumn.Expression.Length > 0)
					{
						CodeExpression exp = CodeGenHelper.Property(CodeGenHelper.Property(CodeGenHelper.This(), designTable.GeneratorTablePropName), this.codeGenerator.TableHandler.Tables[dataColumn.Table.TableName].DesignColumns[dataColumn.ColumnName].GeneratorColumnPropNameInTable);
						flag = true;
						codeMemberMethod.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Property(exp, "Expression"), CodeGenHelper.Str(dataColumn.Expression)));
					}
				}
			}
			if (flag)
			{
				return codeMemberMethod;
			}
			return null;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0007520C File Offset: 0x0007340C
		private bool TableContainsExpressions(DesignTable designTable)
		{
			DataTable dataTable = designTable.DataTable;
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.Expression.Length > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000ACE RID: 2766
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000ACF RID: 2767
		private DesignDataSource dataSource;

		// Token: 0x04000AD0 RID: 2768
		private DataSet dataSet;

		// Token: 0x04000AD1 RID: 2769
		private CodeMemberMethod initExpressionsMethod;

		// Token: 0x04000AD2 RID: 2770
		private static PropertyDescriptor namespaceProperty = TypeDescriptor.GetProperties(typeof(DataSet))["Namespace"];

		// Token: 0x04000AD3 RID: 2771
		private static PropertyDescriptor localeProperty = TypeDescriptor.GetProperties(typeof(DataSet))["Locale"];

		// Token: 0x04000AD4 RID: 2772
		private static PropertyDescriptor caseSensitiveProperty = TypeDescriptor.GetProperties(typeof(DataSet))["CaseSensitive"];
	}
}
