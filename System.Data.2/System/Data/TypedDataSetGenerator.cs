using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x02000091 RID: 145
	[Obsolete("TypedDataSetGenerator class will be removed in a future release. Please use System.Data.Design.TypedDataSetGenerator in System.Design.dll.")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true)]
	public class TypedDataSetGenerator
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x000514BC File Offset: 0x000508BC
		public static void Generate(DataSet dataSet, CodeNamespace codeNamespace, ICodeGenerator codeGen)
		{
			new TypedDataSetGenerator().GenerateCode(dataSet, codeNamespace, codeGen);
			CodeGenerator.ValidateIdentifiers(codeNamespace);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000514E0 File Offset: 0x000508E0
		public static string GenerateIdName(string name, ICodeGenerator codeGen)
		{
			if (codeGen.IsValidIdentifier(name))
			{
				return name;
			}
			string text = name.Replace(' ', '_');
			if (!codeGen.IsValidIdentifier(text))
			{
				text = "_" + text;
				for (int i = 1; i < text.Length; i++)
				{
					UnicodeCategory unicodeCategory = char.GetUnicodeCategory(text[i]);
					if (unicodeCategory != UnicodeCategory.UppercaseLetter && UnicodeCategory.LowercaseLetter != unicodeCategory && UnicodeCategory.TitlecaseLetter != unicodeCategory && UnicodeCategory.ModifierLetter != unicodeCategory && UnicodeCategory.OtherLetter != unicodeCategory && UnicodeCategory.LetterNumber != unicodeCategory && UnicodeCategory.NonSpacingMark != unicodeCategory && UnicodeCategory.SpacingCombiningMark != unicodeCategory && UnicodeCategory.DecimalDigitNumber != unicodeCategory && UnicodeCategory.ConnectorPunctuation != unicodeCategory)
					{
						text = text.Replace(text[i], '_');
					}
				}
			}
			return text;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00051570 File Offset: 0x00050970
		internal CodeTypeDeclaration GenerateCode(DataSet dataSet, CodeNamespace codeNamespace, ICodeGenerator codeGen)
		{
			this.useExtendedNaming = false;
			this.errorList = new ArrayList();
			this.conflictingTables = new ArrayList();
			this.codeGen = codeGen;
			CodeTypeDeclaration codeTypeDeclaration = this.CreateTypedDataSet(dataSet);
			foreach (object obj in dataSet.Tables)
			{
				DataTable table = (DataTable)obj;
				codeTypeDeclaration.Members.Add(this.CreateTypedRowEventHandler(table));
			}
			foreach (object obj2 in dataSet.Tables)
			{
				DataTable table2 = (DataTable)obj2;
				codeTypeDeclaration.Members.Add(this.CreateTypedTable(table2));
				codeTypeDeclaration.Members.Add(this.CreateTypedRow(table2));
				codeTypeDeclaration.Members.Add(this.CreateTypedRowEvent(table2));
			}
			if (this.errorList.Count > 0)
			{
				throw new TypedDataSetGeneratorException(this.errorList);
			}
			codeNamespace.Types.Add(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000516C4 File Offset: 0x00050AC4
		private void InitLookupIdentifiers()
		{
			this.lookupIdentifiers = new Hashtable();
			PropertyInfo[] properties = typeof(DataRow).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				this.lookupIdentifiers[propertyInfo.Name] = "_" + propertyInfo.Name;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00051724 File Offset: 0x00050B24
		private string FixIdName(string inVarName)
		{
			if (this.lookupIdentifiers == null)
			{
				this.InitLookupIdentifiers();
			}
			string text = (string)this.lookupIdentifiers[inVarName];
			if (text == null)
			{
				text = TypedDataSetGenerator.GenerateIdName(inVarName, this.codeGen);
				while (this.lookupIdentifiers.ContainsValue(text))
				{
					text = "_" + text;
				}
				this.lookupIdentifiers[inVarName] = text;
				if (!this.codeGen.IsValidIdentifier(text))
				{
					this.errorList.Add(Res.GetString("CodeGen_InvalidIdentifier", new object[]
					{
						text
					}));
				}
			}
			return text;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000517BC File Offset: 0x00050BBC
		private static bool isEmpty(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000517D8 File Offset: 0x00050BD8
		private string RowClassName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedName"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.FixIdName(table.TableName) + "Row";
			}
			return text;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0005181C File Offset: 0x00050C1C
		private string RowBaseClassName(DataTable table)
		{
			if (this.useExtendedNaming)
			{
				string text = (string)table.ExtendedProperties["typedBaseClass"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = (string)table.DataSet.ExtendedProperties["typedBaseClass"];
					if (TypedDataSetGenerator.isEmpty(text))
					{
						text = "DataRow";
					}
				}
				return text;
			}
			return "DataRow";
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00051880 File Offset: 0x00050C80
		private string RowConcreteClassName(DataTable table)
		{
			if (this.useExtendedNaming)
			{
				string text = (string)table.ExtendedProperties["typedConcreteClass"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = this.RowClassName(table);
				}
				return text;
			}
			return this.RowClassName(table);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000518C4 File Offset: 0x00050CC4
		private string TableClassName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					if (table.DataSet.Tables.InternalIndexOf(table.TableName) == -3 && !this.conflictingTables.Contains(table.TableName))
					{
						this.conflictingTables.Add(table.TableName);
						this.errorList.Add(Res.GetString("CodeGen_DuplicateTableName", new object[]
						{
							table.TableName
						}));
					}
					text = this.FixIdName(table.TableName);
				}
			}
			return text + "DataTable";
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0005198C File Offset: 0x00050D8C
		private string TablePropertyName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = this.FixIdName(table.TableName);
				}
				else
				{
					text += "Table";
				}
			}
			return text;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000519F4 File Offset: 0x00050DF4
		private string TableFieldName(DataTable table)
		{
			return "table" + this.TablePropertyName(table);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00051A14 File Offset: 0x00050E14
		private string RowColumnPropertyName(DataColumn column)
		{
			string text = (string)column.ExtendedProperties["typedName"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.FixIdName(column.ColumnName);
			}
			return text;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00051A50 File Offset: 0x00050E50
		private string TableColumnFieldName(DataColumn column)
		{
			string text = this.RowColumnPropertyName(column);
			if (string.Compare("column", text, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return "column" + text;
			}
			return "columnField" + text;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00051A8C File Offset: 0x00050E8C
		private string TableColumnPropertyName(DataColumn column)
		{
			return this.RowColumnPropertyName(column) + "Column";
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00051AAC File Offset: 0x00050EAC
		private static int TablesConnectedness(DataTable parentTable, DataTable childTable)
		{
			int num = 0;
			DataRelationCollection parentRelations = childTable.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ParentTable == parentTable)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00051AE8 File Offset: 0x00050EE8
		private string ChildPropertyName(DataRelation relation)
		{
			string text = (string)relation.ExtendedProperties["typedChildren"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				string text2 = (string)relation.ChildTable.ExtendedProperties["typedPlural"];
				if (TypedDataSetGenerator.isEmpty(text2))
				{
					text2 = (string)relation.ChildTable.ExtendedProperties["typedName"];
					if (TypedDataSetGenerator.isEmpty(text2))
					{
						text = "Get" + relation.ChildTable.TableName + "Rows";
						if (1 < TypedDataSetGenerator.TablesConnectedness(relation.ParentTable, relation.ChildTable))
						{
							text = text + "By" + relation.RelationName;
						}
						return this.FixIdName(text);
					}
					text2 += "Rows";
				}
				text = "Get" + text2;
			}
			return text;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00051BC4 File Offset: 0x00050FC4
		private string ParentPropertyName(DataRelation relation)
		{
			string text = (string)relation.ExtendedProperties["typedParent"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.RowClassName(relation.ParentTable);
				if (relation.ChildTable == relation.ParentTable || relation.ChildColumnsReference.Length != 1)
				{
					text += "Parent";
				}
				if (1 < TypedDataSetGenerator.TablesConnectedness(relation.ParentTable, relation.ChildTable))
				{
					text = text + "By" + this.FixIdName(relation.RelationName);
				}
			}
			return text;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00051C50 File Offset: 0x00051050
		private string RelationFieldName(DataRelation relation)
		{
			return this.FixIdName("relation" + relation.RelationName);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00051C74 File Offset: 0x00051074
		private string GetTypeName(Type t)
		{
			return t.FullName;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00051C88 File Offset: 0x00051088
		private bool ChildRelationFollowable(DataRelation relation)
		{
			return relation != null && (relation.ChildTable != relation.ParentTable || relation.ChildTable.Columns.Count != 1);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00051CC0 File Offset: 0x000510C0
		private static CodeMemberMethod CreateOnRowEventMethod(string eventName, string rowClassName)
		{
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(void), "OnRow" + eventName, (MemberAttributes)12292);
			codeMemberMethod.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowChangeEventArgs), "e"));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "OnRow" + eventName, TypedDataSetGenerator.Argument("e")));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Event(rowClassName + eventName), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.DelegateCall(TypedDataSetGenerator.Event(rowClassName + eventName), TypedDataSetGenerator.New(rowClassName + "ChangeEvent", new CodeExpression[]
			{
				TypedDataSetGenerator.Cast(rowClassName, TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Row")),
				TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Action")
			})))));
			return codeMemberMethod;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00051DC4 File Offset: 0x000511C4
		private CodeTypeDeclaration CreateTypedTable(DataTable table)
		{
			string text = this.RowClassName(table);
			string text2 = this.TableClassName(table);
			string type = this.RowConcreteClassName(table);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text2);
			codeTypeDeclaration.BaseTypes.Add(typeof(DataTable));
			codeTypeDeclaration.BaseTypes.Add(typeof(IEnumerable));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Serializable"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			for (int i = 0; i < table.Columns.Count; i++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataColumn), this.TableColumnFieldName(table.Columns[i])));
			}
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Changed"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Changing"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Deleted"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Deleting"));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Str(table.TableName));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Family;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(SerializationInfo), "info"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(StreamingContext), "context"));
			codeConstructor.BaseConstructorArgs.AddRange(new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			});
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataTable), "table"));
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "TableName"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "CaseSensitive")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "CaseSensitive"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Locale"), "ToString"), TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "Locale"), "ToString")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Locale"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "Namespace")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Namespace"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Prefix")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "MinimumCapacity"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "MinimumCapacity")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DisplayExpression"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DisplayExpression")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(typeof(int), "Count", (MemberAttributes)24578);
			codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.Browsable", TypedDataSetGenerator.Primitive(false)));
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Count")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			for (int j = 0; j < table.Columns.Count; j++)
			{
				DataColumn column = table.Columns[j];
				CodeMemberProperty codeMemberProperty2 = TypedDataSetGenerator.PropertyDecl(typeof(DataColumn), this.TableColumnPropertyName(column), (MemberAttributes)4098);
				codeMemberProperty2.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableColumnFieldName(column))));
				codeTypeDeclaration.Members.Add(codeMemberProperty2);
			}
			CodeMemberProperty codeMemberProperty3 = TypedDataSetGenerator.PropertyDecl(type, "Item", (MemberAttributes)24578);
			codeMemberProperty3.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(int), "index"));
			codeMemberProperty3.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(type, TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), TypedDataSetGenerator.Argument("index")))));
			codeTypeDeclaration.Members.Add(codeMemberProperty3);
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(void), "Add" + text, (MemberAttributes)24578);
			codeMemberMethod.Parameters.Add(TypedDataSetGenerator.ParameterDecl(type, "row"));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Add", TypedDataSetGenerator.Argument("row")));
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			ArrayList arrayList = new ArrayList();
			for (int k = 0; k < table.Columns.Count; k++)
			{
				if (!table.Columns[k].AutoIncrement)
				{
					arrayList.Add(table.Columns[k]);
				}
			}
			CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(type, "Add" + text, (MemberAttributes)24578);
			DataColumn[] array = new DataColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			for (int l = 0; l < array.Length; l++)
			{
				Type dataType = array[l].DataType;
				DataRelation dataRelation = array[l].FindParentRelation();
				if (this.ChildRelationFollowable(dataRelation))
				{
					string text3 = this.RowClassName(dataRelation.ParentTable);
					string name = this.FixIdName("parent" + text3 + "By" + dataRelation.RelationName);
					codeMemberMethod2.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text3, name));
				}
				else
				{
					codeMemberMethod2.Parameters.Add(TypedDataSetGenerator.ParameterDecl(this.GetTypeName(dataType), this.RowColumnPropertyName(array[l])));
				}
			}
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.VariableDecl(type, "row" + text, TypedDataSetGenerator.Cast(type, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "NewRow"))));
			CodeExpression codeExpression = TypedDataSetGenerator.Variable("row" + text);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = TypedDataSetGenerator.Property(codeExpression, "ItemArray");
			CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
			codeArrayCreateExpression.CreateType = TypedDataSetGenerator.Type(typeof(object));
			array = new DataColumn[table.Columns.Count];
			table.Columns.CopyTo(array, 0);
			for (int m = 0; m < array.Length; m++)
			{
				if (array[m].AutoIncrement)
				{
					codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Primitive(null));
				}
				else
				{
					DataRelation dataRelation2 = array[m].FindParentRelation();
					if (this.ChildRelationFollowable(dataRelation2))
					{
						string str = this.RowClassName(dataRelation2.ParentTable);
						string argument = this.FixIdName("parent" + str + "By" + dataRelation2.RelationName);
						codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Argument(argument), TypedDataSetGenerator.Primitive(dataRelation2.ParentColumnsReference[0].Ordinal)));
					}
					else
					{
						codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Argument(this.RowColumnPropertyName(array[m])));
					}
				}
			}
			codeAssignStatement.Right = codeArrayCreateExpression;
			codeMemberMethod2.Statements.Add(codeAssignStatement);
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Add", codeExpression));
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.Return(codeExpression));
			codeTypeDeclaration.Members.Add(codeMemberMethod2);
			for (int n = 0; n < table.Constraints.Count; n++)
			{
				if (table.Constraints[n] is UniqueConstraint && ((UniqueConstraint)table.Constraints[n]).IsPrimaryKey)
				{
					DataColumn[] columnsReference = ((UniqueConstraint)table.Constraints[n]).ColumnsReference;
					string text4 = "FindBy";
					bool flag = true;
					for (int num = 0; num < columnsReference.Length; num++)
					{
						text4 += this.RowColumnPropertyName(columnsReference[num]);
						if (columnsReference[num].ColumnMapping != MappingType.Hidden)
						{
							flag = false;
						}
					}
					if (!flag)
					{
						CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.MethodDecl(text, this.FixIdName(text4), (MemberAttributes)24578);
						for (int num2 = 0; num2 < columnsReference.Length; num2++)
						{
							codeMemberMethod3.Parameters.Add(TypedDataSetGenerator.ParameterDecl(this.GetTypeName(columnsReference[num2].DataType), this.RowColumnPropertyName(columnsReference[num2])));
						}
						CodeArrayCreateExpression codeArrayCreateExpression2 = new CodeArrayCreateExpression(typeof(object), columnsReference.Length);
						for (int num3 = 0; num3 < columnsReference.Length; num3++)
						{
							codeArrayCreateExpression2.Initializers.Add(TypedDataSetGenerator.Argument(this.RowColumnPropertyName(columnsReference[num3])));
						}
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(text, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Find", codeArrayCreateExpression2))));
						codeTypeDeclaration.Members.Add(codeMemberMethod3);
					}
				}
			}
			CodeMemberMethod codeMemberMethod4 = TypedDataSetGenerator.MethodDecl(typeof(IEnumerator), "GetEnumerator", (MemberAttributes)24578);
			codeMemberMethod4.ImplementationTypes.Add(TypedDataSetGenerator.Type("System.Collections.IEnumerable"));
			codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "GetEnumerator")));
			codeTypeDeclaration.Members.Add(codeMemberMethod4);
			CodeMemberMethod codeMemberMethod5 = TypedDataSetGenerator.MethodDecl(typeof(DataTable), "Clone", (MemberAttributes)24580);
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.VariableDecl(text2, "cln", TypedDataSetGenerator.Cast(text2, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "Clone", new CodeExpression[0]))));
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("cln"), "InitVars", new CodeExpression[0]));
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("cln")));
			codeTypeDeclaration.Members.Add(codeMemberMethod5);
			CodeMemberMethod codeMemberMethod6 = TypedDataSetGenerator.MethodDecl(typeof(DataTable), "CreateInstance", (MemberAttributes)12292);
			codeMemberMethod6.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.New(text2, new CodeExpression[0])));
			codeTypeDeclaration.Members.Add(codeMemberMethod6);
			CodeMemberMethod codeMemberMethod7 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitClass", MemberAttributes.Private);
			CodeMemberMethod codeMemberMethod8 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			for (int num4 = 0; num4 < table.Columns.Count; num4++)
			{
				DataColumn dataColumn = table.Columns[num4];
				string field = this.TableColumnFieldName(dataColumn);
				CodeExpression left = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), field);
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(left, TypedDataSetGenerator.New(typeof(DataColumn), new CodeExpression[]
				{
					TypedDataSetGenerator.Str(dataColumn.ColumnName),
					TypedDataSetGenerator.TypeOf(this.GetTypeName(dataColumn.DataType)),
					TypedDataSetGenerator.Primitive(null),
					TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MappingType)), (dataColumn.ColumnMapping == MappingType.SimpleContent) ? "SimpleContent" : ((dataColumn.ColumnMapping == MappingType.Attribute) ? "Attribute" : ((dataColumn.ColumnMapping == MappingType.Hidden) ? "Hidden" : "Element")))
				})));
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Columns"), "Add", TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), field)));
			}
			for (int num5 = 0; num5 < table.Constraints.Count; num5++)
			{
				if (table.Constraints[num5] is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = (UniqueConstraint)table.Constraints[num5];
					DataColumn[] columnsReference2 = uniqueConstraint.ColumnsReference;
					CodeExpression[] array2 = new CodeExpression[columnsReference2.Length];
					for (int num6 = 0; num6 < columnsReference2.Length; num6++)
					{
						array2[num6] = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableColumnFieldName(columnsReference2[num6]));
					}
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Constraints"), "Add", TypedDataSetGenerator.New(typeof(UniqueConstraint), new CodeExpression[]
					{
						TypedDataSetGenerator.Str(uniqueConstraint.ConstraintName),
						new CodeArrayCreateExpression(typeof(DataColumn), array2),
						TypedDataSetGenerator.Primitive(uniqueConstraint.IsPrimaryKey)
					})));
				}
			}
			for (int num7 = 0; num7 < table.Columns.Count; num7++)
			{
				DataColumn dataColumn2 = table.Columns[num7];
				string field2 = this.TableColumnFieldName(dataColumn2);
				CodeExpression codeExpression2 = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), field2);
				codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(codeExpression2, TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Columns"), TypedDataSetGenerator.Str(dataColumn2.ColumnName))));
				if (dataColumn2.AutoIncrement)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AutoIncrement"), TypedDataSetGenerator.Primitive(true)));
				}
				if (dataColumn2.AutoIncrementSeed != 0L)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AutoIncrementSeed"), TypedDataSetGenerator.Primitive(dataColumn2.AutoIncrementSeed)));
				}
				if (dataColumn2.AutoIncrementStep != 1L)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AutoIncrementStep"), TypedDataSetGenerator.Primitive(dataColumn2.AutoIncrementStep)));
				}
				if (!dataColumn2.AllowDBNull)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AllowDBNull"), TypedDataSetGenerator.Primitive(false)));
				}
				if (dataColumn2.ReadOnly)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "ReadOnly"), TypedDataSetGenerator.Primitive(true)));
				}
				if (dataColumn2.Unique)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "Unique"), TypedDataSetGenerator.Primitive(true)));
				}
				if (!ADP.IsEmpty(dataColumn2.Prefix))
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "Prefix"), TypedDataSetGenerator.Str(dataColumn2.Prefix)));
				}
				if (dataColumn2._columnUri != null)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "Namespace"), TypedDataSetGenerator.Str(dataColumn2.Namespace)));
				}
				if (dataColumn2.Caption != dataColumn2.ColumnName)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "Caption"), TypedDataSetGenerator.Str(dataColumn2.Caption)));
				}
				if (dataColumn2.DefaultValue != DBNull.Value)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "DefaultValue"), TypedDataSetGenerator.Primitive(dataColumn2.DefaultValue)));
				}
				if (dataColumn2.MaxLength != -1)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "MaxLength"), TypedDataSetGenerator.Primitive(dataColumn2.MaxLength)));
				}
			}
			if (table.ShouldSerializeCaseSensitive())
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Primitive(table.CaseSensitive)));
			}
			if (table.ShouldSerializeLocale())
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.New(typeof(CultureInfo), new CodeExpression[]
				{
					TypedDataSetGenerator.Str(table.Locale.ToString())
				})));
			}
			if (!ADP.IsEmpty(table.Prefix))
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Str(table.Prefix)));
			}
			if (table.tableNamespace != null)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Str(table.Namespace)));
			}
			if (table.MinimumCapacity != 50)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "MinimumCapacity"), TypedDataSetGenerator.Primitive(table.MinimumCapacity)));
			}
			if (table.displayExpression != null)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DisplayExpression"), TypedDataSetGenerator.Str(table.DisplayExpressionInternal)));
			}
			codeTypeDeclaration.Members.Add(codeMemberMethod8);
			codeTypeDeclaration.Members.Add(codeMemberMethod7);
			CodeMemberMethod codeMemberMethod9 = TypedDataSetGenerator.MethodDecl(type, "New" + text, (MemberAttributes)24578);
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(type, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "NewRow"))));
			codeTypeDeclaration.Members.Add(codeMemberMethod9);
			CodeMemberMethod codeMemberMethod10 = TypedDataSetGenerator.MethodDecl(typeof(DataRow), "NewRowFromBuilder", (MemberAttributes)12292);
			codeMemberMethod10.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowBuilder), "builder"));
			codeMemberMethod10.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.New(type, new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("builder")
			})));
			codeTypeDeclaration.Members.Add(codeMemberMethod10);
			CodeMemberMethod codeMemberMethod11 = TypedDataSetGenerator.MethodDecl(typeof(Type), "GetRowType", (MemberAttributes)12292);
			codeMemberMethod11.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.TypeOf(type)));
			codeTypeDeclaration.Members.Add(codeMemberMethod11);
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Changed", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Changing", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Deleted", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Deleting", text));
			CodeMemberMethod codeMemberMethod12 = TypedDataSetGenerator.MethodDecl(typeof(void), "Remove" + text, (MemberAttributes)24578);
			codeMemberMethod12.Parameters.Add(TypedDataSetGenerator.ParameterDecl(type, "row"));
			codeMemberMethod12.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Remove", TypedDataSetGenerator.Argument("row")));
			codeTypeDeclaration.Members.Add(codeMemberMethod12);
			return codeTypeDeclaration;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000532A0 File Offset: 0x000526A0
		private CodeTypeDeclaration CreateTypedRow(DataTable table)
		{
			string text = this.RowClassName(table);
			string type = this.TableClassName(table);
			string text2 = this.TableFieldName(table);
			bool flag = false;
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = text;
			string text3 = this.RowBaseClassName(table);
			if (string.Compare(text3, "DataRow", StringComparison.Ordinal) == 0)
			{
				codeTypeDeclaration.BaseTypes.Add(typeof(DataRow));
			}
			else
			{
				codeTypeDeclaration.BaseTypes.Add(text3);
			}
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(type, text2));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowBuilder), "rb"));
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Argument("rb"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text2), TypedDataSetGenerator.Cast(type, TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"))));
			codeTypeDeclaration.Members.Add(codeConstructor);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					Type dataType = dataColumn.DataType;
					string text4 = this.RowColumnPropertyName(dataColumn);
					string property = this.TableColumnPropertyName(dataColumn);
					CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(dataType, text4, (MemberAttributes)24578);
					CodeStatement codeStatement = TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(this.GetTypeName(dataType), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text2), property))));
					if (dataColumn.AllowDBNull)
					{
						string text5 = (string)dataColumn.ExtendedProperties["nullValue"];
						if (text5 == null || text5 == "_throw")
						{
							codeStatement = TypedDataSetGenerator.Try(codeStatement, TypedDataSetGenerator.Catch(typeof(InvalidCastException), "e", TypedDataSetGenerator.Throw(typeof(StrongTypingException), "StrongTyping_CananotAccessDBNull", "e")));
						}
						else
						{
							CodeExpression codeExpression = null;
							CodeExpression expr;
							if (text5 == "_null")
							{
								if (dataColumn.DataType.IsSubclassOf(typeof(ValueType)))
								{
									this.errorList.Add(Res.GetString("CodeGen_TypeCantBeNull", new object[]
									{
										dataColumn.ColumnName,
										dataColumn.DataType.Name
									}));
									continue;
								}
								expr = TypedDataSetGenerator.Primitive(null);
							}
							else if (text5 == "_empty")
							{
								if (dataColumn.DataType == typeof(string))
								{
									expr = TypedDataSetGenerator.Property(TypedDataSetGenerator.TypeExpr(dataColumn.DataType), "Empty");
								}
								else
								{
									expr = TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(text), text4 + "_nullValue");
									ConstructorInfo constructor = dataColumn.DataType.GetConstructor(new Type[]
									{
										typeof(string)
									});
									if (constructor == null)
									{
										this.errorList.Add(Res.GetString("CodeGen_NoCtor0", new object[]
										{
											dataColumn.ColumnName,
											dataColumn.DataType.Name
										}));
										continue;
									}
									constructor.Invoke(new object[0]);
									codeExpression = TypedDataSetGenerator.New(dataColumn.DataType, new CodeExpression[0]);
								}
							}
							else
							{
								if (!flag)
								{
									table.NewRow();
									flag = true;
								}
								object obj2 = dataColumn.ConvertXmlToObject(text5);
								if (dataColumn.DataType == typeof(char) || dataColumn.DataType == typeof(string) || dataColumn.DataType == typeof(decimal) || dataColumn.DataType == typeof(bool) || dataColumn.DataType == typeof(float) || dataColumn.DataType == typeof(double) || dataColumn.DataType == typeof(sbyte) || dataColumn.DataType == typeof(byte) || dataColumn.DataType == typeof(short) || dataColumn.DataType == typeof(ushort) || dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(uint) || dataColumn.DataType == typeof(long) || dataColumn.DataType == typeof(ulong))
								{
									expr = TypedDataSetGenerator.Primitive(obj2);
								}
								else
								{
									expr = TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(text), text4 + "_nullValue");
									if (dataColumn.DataType == typeof(byte[]))
									{
										codeExpression = TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr(typeof(Convert)), "FromBase64String", TypedDataSetGenerator.Primitive(text5));
									}
									else if (dataColumn.DataType == typeof(DateTime) || dataColumn.DataType == typeof(TimeSpan))
									{
										codeExpression = TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr(dataColumn.DataType), "Parse", TypedDataSetGenerator.Primitive(obj2.ToString()));
									}
									else
									{
										ConstructorInfo constructor2 = dataColumn.DataType.GetConstructor(new Type[]
										{
											typeof(string)
										});
										if (constructor2 == null)
										{
											this.errorList.Add(Res.GetString("CodeGen_NoCtor1", new object[]
											{
												dataColumn.ColumnName,
												dataColumn.DataType.Name
											}));
											continue;
										}
										constructor2.Invoke(new object[]
										{
											text5
										});
										codeExpression = TypedDataSetGenerator.New(dataColumn.DataType, new CodeExpression[]
										{
											TypedDataSetGenerator.Primitive(text5)
										});
									}
								}
							}
							codeStatement = TypedDataSetGenerator.If(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Is" + text4 + "Null"), new CodeStatement[]
							{
								TypedDataSetGenerator.Return(expr)
							}, new CodeStatement[]
							{
								codeStatement
							});
							if (codeExpression != null)
							{
								CodeMemberField codeMemberField = TypedDataSetGenerator.FieldDecl(dataColumn.DataType, text4 + "_nullValue");
								codeMemberField.Attributes = (MemberAttributes)20483;
								codeMemberField.InitExpression = codeExpression;
								codeTypeDeclaration.Members.Add(codeMemberField);
							}
						}
					}
					codeMemberProperty.GetStatements.Add(codeStatement);
					codeMemberProperty.SetStatements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text2), property)), TypedDataSetGenerator.Value()));
					codeTypeDeclaration.Members.Add(codeMemberProperty);
					if (dataColumn.AllowDBNull)
					{
						CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(bool), "Is" + text4 + "Null", (MemberAttributes)24578);
						codeMemberMethod.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "IsNull", TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text2), property))));
						codeTypeDeclaration.Members.Add(codeMemberMethod);
						CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(typeof(void), "Set" + text4 + "Null", (MemberAttributes)24578);
						codeMemberMethod2.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text2), property)), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(Convert)), "DBNull")));
						codeTypeDeclaration.Members.Add(codeMemberMethod2);
					}
				}
			}
			DataRelationCollection childRelations = table.ChildRelations;
			for (int i = 0; i < childRelations.Count; i++)
			{
				DataRelation dataRelation = childRelations[i];
				string type2 = this.RowConcreteClassName(dataRelation.ChildTable);
				CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(type2, 1), this.ChildPropertyName(dataRelation), (MemberAttributes)24578);
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(TypedDataSetGenerator.Type(type2, 1), TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetChildRows", TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ChildRelations"), TypedDataSetGenerator.Str(dataRelation.RelationName))))));
				codeTypeDeclaration.Members.Add(codeMemberMethod3);
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			for (int j = 0; j < parentRelations.Count; j++)
			{
				DataRelation dataRelation2 = parentRelations[j];
				string type3 = this.RowClassName(dataRelation2.ParentTable);
				CodeMemberProperty codeMemberProperty2 = TypedDataSetGenerator.PropertyDecl(type3, this.ParentPropertyName(dataRelation2), (MemberAttributes)24578);
				codeMemberProperty2.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(type3, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetParentRow", TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ParentRelations"), TypedDataSetGenerator.Str(dataRelation2.RelationName))))));
				codeMemberProperty2.SetStatements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "SetParentRow", new CodeExpression[]
				{
					TypedDataSetGenerator.Value(),
					TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ParentRelations"), TypedDataSetGenerator.Str(dataRelation2.RelationName))
				}));
				codeTypeDeclaration.Members.Add(codeMemberProperty2);
			}
			return codeTypeDeclaration;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00053C90 File Offset: 0x00053090
		private CodeTypeDeclaration CreateTypedRowEvent(DataTable table)
		{
			string str = this.RowClassName(table);
			string text = this.TableClassName(table);
			string type = this.RowConcreteClassName(table);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = str + "ChangeEvent";
			codeTypeDeclaration.BaseTypes.Add(typeof(EventArgs));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(type, "eventRow"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataRowAction), "eventAction"));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)24578;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(type, "row"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowAction), "action"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventRow"), TypedDataSetGenerator.Argument("row")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventAction"), TypedDataSetGenerator.Argument("action")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(type, "Row", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventRow")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			codeMemberProperty = TypedDataSetGenerator.PropertyDecl(typeof(DataRowAction), "Action", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventAction")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			return codeTypeDeclaration;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00053E60 File Offset: 0x00053260
		private CodeTypeDelegate CreateTypedRowEventHandler(DataTable table)
		{
			string str = this.RowClassName(table);
			CodeTypeDelegate codeTypeDelegate = new CodeTypeDelegate(str + "ChangeEventHandler");
			codeTypeDelegate.TypeAttributes |= TypeAttributes.Public;
			codeTypeDelegate.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(object), "sender"));
			codeTypeDelegate.Parameters.Add(TypedDataSetGenerator.ParameterDecl(str + "ChangeEvent", "e"));
			return codeTypeDelegate;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00053ED8 File Offset: 0x000532D8
		private CodeTypeDeclaration CreateTypedDataSet(DataSet dataSet)
		{
			string text = this.FixIdName(dataSet.DataSetName);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text);
			codeTypeDeclaration.BaseTypes.Add(typeof(DataSet));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Serializable"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.DesignerCategoryAttribute", TypedDataSetGenerator.Str("code")));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.ToolboxItem", TypedDataSetGenerator.Primitive(true)));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl(typeof(XmlSchemaProviderAttribute).FullName, TypedDataSetGenerator.Primitive("GetTypedDataSetSchema")));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl(typeof(XmlRootAttribute).FullName, TypedDataSetGenerator.Primitive(text)));
			for (int i = 0; i < dataSet.Tables.Count; i++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(this.TableClassName(dataSet.Tables[i]), this.TableFieldName(dataSet.Tables[i])));
			}
			for (int j = 0; j < dataSet.Relations.Count; j++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataRelation), this.RelationFieldName(dataSet.Relations[j])));
			}
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "BeginInit"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "EndInit"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Family;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(SerializationInfo), "info"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(StreamingContext), "context"));
			codeConstructor.BaseConstructorArgs.AddRange(new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			});
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "IsBinarySerialized", new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			}), TypedDataSetGenerator.Primitive(true)), new CodeStatement[]
			{
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars", TypedDataSetGenerator.Primitive(false))),
				TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler1", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler1")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler1")),
				TypedDataSetGenerator.Return()
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(string), "strSchema", TypedDataSetGenerator.Cast("System.String", TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Argument("info"), "GetValue", new CodeExpression[]
			{
				TypedDataSetGenerator.Str("XmlSchema"),
				TypedDataSetGenerator.TypeOf("System.String")
			}))));
			ArrayList arrayList = new ArrayList();
			arrayList.Add(TypedDataSetGenerator.VariableDecl(typeof(DataSet), "ds", TypedDataSetGenerator.New(typeof(DataSet), new CodeExpression[0])));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "ReadXmlSchema", new CodeExpression[]
			{
				TypedDataSetGenerator.New(typeof(XmlTextReader), new CodeExpression[]
				{
					TypedDataSetGenerator.New("System.IO.StringReader", new CodeExpression[]
					{
						TypedDataSetGenerator.Variable("strSchema")
					})
				})
			})));
			for (int k = 0; k < dataSet.Tables.Count; k++)
			{
				arrayList.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[k].TableName)), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[k]), new CodeExpression[]
				{
					TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[k].TableName))
				})))));
			}
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "DataSetName")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Prefix")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Locale")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "CaseSensitive")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "EnforceConstraints")));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Merge", new CodeExpression[]
			{
				TypedDataSetGenerator.Variable("ds"),
				TypedDataSetGenerator.Primitive(false),
				TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MissingSchemaAction)), "Add")
			})));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars")));
			CodeStatement[] array = new CodeStatement[arrayList.Count];
			arrayList.CopyTo(array);
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Variable("strSchema"), TypedDataSetGenerator.Primitive(null)), array, new CodeStatement[]
			{
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "BeginInit")),
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass")),
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "EndInit"))
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetSerializationData", new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(DataSet), "Clone", (MemberAttributes)24580);
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.VariableDecl(text, "cln", TypedDataSetGenerator.Cast(text, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "Clone", new CodeExpression[0]))));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("cln"), "InitVars", new CodeExpression[0]));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("cln")));
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars", new CodeExpression[]
			{
				TypedDataSetGenerator.Primitive(true)
			}));
			codeTypeDeclaration.Members.Add(codeMemberMethod2);
			CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitClass", MemberAttributes.Private);
			CodeMemberMethod codeMemberMethod4 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			codeMemberMethod4.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(bool), "initTable"));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Str(dataSet.DataSetName)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Str(dataSet.Prefix)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Str(dataSet.Namespace)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.New(typeof(CultureInfo), new CodeExpression[]
			{
				TypedDataSetGenerator.Str(dataSet.Locale.ToString())
			})));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Primitive(dataSet.CaseSensitive)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Primitive(dataSet.EnforceConstraints)));
			for (int l = 0; l < dataSet.Tables.Count; l++)
			{
				CodeExpression codeExpression = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataSet.Tables[l]));
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(codeExpression, TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[l]), new CodeExpression[0])));
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", codeExpression));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Assign(codeExpression, TypedDataSetGenerator.Cast(this.TableClassName(dataSet.Tables[l]), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[l].TableName)))));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.Variable("initTable"), TypedDataSetGenerator.Primitive(true)), new CodeStatement[]
				{
					TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(codeExpression, TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(codeExpression, "InitVars")))
				}));
			}
			CodeMemberMethod codeMemberMethod5 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerializeTables", (MemberAttributes)12292);
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
			codeTypeDeclaration.Members.Add(codeMemberMethod5);
			CodeMemberMethod codeMemberMethod6 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerializeRelations", (MemberAttributes)12292);
			codeMemberMethod6.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
			codeTypeDeclaration.Members.Add(codeMemberMethod6);
			CodeMemberMethod codeMemberMethod7 = TypedDataSetGenerator.MethodDecl(typeof(XmlSchemaComplexType), "GetTypedDataSetSchema", (MemberAttributes)24579);
			codeMemberMethod7.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(XmlSchemaSet), "xs"));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(text, "ds", TypedDataSetGenerator.New(text, new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Argument("xs"), "Add", new CodeExpression[]
			{
				TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "GetSchemaSerializable", new CodeExpression[0])
			}));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaComplexType), "type", TypedDataSetGenerator.New(typeof(XmlSchemaComplexType), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaSequence), "sequence", TypedDataSetGenerator.New(typeof(XmlSchemaSequence), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaAny), "any", TypedDataSetGenerator.New(typeof(XmlSchemaAny), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("any"), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("sequence"), "Items"), "Add", new CodeExpression[]
			{
				TypedDataSetGenerator.Variable("any")
			}));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("type"), "Particle"), TypedDataSetGenerator.Variable("sequence")));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("type")));
			codeTypeDeclaration.Members.Add(codeMemberMethod7);
			CodeMemberMethod codeMemberMethod8 = TypedDataSetGenerator.MethodDecl(typeof(void), "ReadXmlSerializable", (MemberAttributes)12292);
			codeMemberMethod8.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(XmlReader), "reader"));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Reset", new CodeExpression[0]));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(DataSet), "ds", TypedDataSetGenerator.New(typeof(DataSet), new CodeExpression[0])));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "ReadXml", new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("reader")
			}));
			for (int m = 0; m < dataSet.Tables.Count; m++)
			{
				codeMemberMethod8.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[m].TableName)), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[m]), new CodeExpression[]
				{
					TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[m].TableName))
				})))));
			}
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "DataSetName")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Prefix")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Locale")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "CaseSensitive")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "EnforceConstraints")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Merge", new CodeExpression[]
			{
				TypedDataSetGenerator.Variable("ds"),
				TypedDataSetGenerator.Primitive(false),
				TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MissingSchemaAction)), "Add")
			}));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"));
			codeTypeDeclaration.Members.Add(codeMemberMethod8);
			CodeMemberMethod codeMemberMethod9 = TypedDataSetGenerator.MethodDecl(typeof(XmlSchema), "GetSchemaSerializable", (MemberAttributes)12292);
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(MemoryStream), "stream", TypedDataSetGenerator.New(typeof(MemoryStream), new CodeExpression[0])));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "WriteXmlSchema", TypedDataSetGenerator.New(typeof(XmlTextWriter), new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("stream"),
				TypedDataSetGenerator.Primitive(null)
			})));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("stream"), "Position"), TypedDataSetGenerator.Primitive(0)));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr("System.Xml.Schema.XmlSchema"), "Read", new CodeExpression[]
			{
				TypedDataSetGenerator.New(typeof(XmlTextReader), new CodeExpression[]
				{
					TypedDataSetGenerator.Argument("stream")
				}),
				TypedDataSetGenerator.Primitive(null)
			})));
			codeTypeDeclaration.Members.Add(codeMemberMethod9);
			CodeExpression codeExpression2 = null;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj2;
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
						CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression(typeof(DataColumn), 0);
						foreach (DataColumn dataColumn in foreignKeyConstraint.Columns)
						{
							codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataColumn.Table)), this.TableColumnPropertyName(dataColumn)));
						}
						CodeArrayCreateExpression codeArrayCreateExpression2 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
						foreach (DataColumn dataColumn2 in foreignKeyConstraint.RelatedColumnsReference)
						{
							codeArrayCreateExpression2.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataColumn2.Table)), this.TableColumnPropertyName(dataColumn2)));
						}
						if (codeExpression2 == null)
						{
							codeMemberMethod3.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(ForeignKeyConstraint), "fkc"));
							codeExpression2 = TypedDataSetGenerator.Variable("fkc");
						}
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(codeExpression2, TypedDataSetGenerator.New(typeof(ForeignKeyConstraint), new CodeExpression[]
						{
							TypedDataSetGenerator.Str(foreignKeyConstraint.ConstraintName),
							codeArrayCreateExpression2,
							codeArrayCreateExpression
						})));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataTable)), "Constraints"), "Add", codeExpression2));
						string field = foreignKeyConstraint.AcceptRejectRule.ToString();
						string field2 = foreignKeyConstraint.DeleteRule.ToString();
						string field3 = foreignKeyConstraint.UpdateRule.ToString();
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AcceptRejectRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.AcceptRejectRule.GetType()), field)));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "DeleteRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.DeleteRule.GetType()), field2)));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "UpdateRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.UpdateRule.GetType()), field3)));
					}
				}
			}
			foreach (object obj3 in dataSet.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj3;
				CodeArrayCreateExpression codeArrayCreateExpression3 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
				string field4 = this.TableFieldName(dataRelation.ParentTable);
				foreach (DataColumn column in dataRelation.ParentColumnsReference)
				{
					codeArrayCreateExpression3.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), field4), this.TableColumnPropertyName(column)));
				}
				CodeArrayCreateExpression codeArrayCreateExpression4 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
				string field5 = this.TableFieldName(dataRelation.ChildTable);
				foreach (DataColumn column2 in dataRelation.ChildColumnsReference)
				{
					codeArrayCreateExpression4.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), field5), this.TableColumnPropertyName(column2)));
				}
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), TypedDataSetGenerator.New(typeof(DataRelation), new CodeExpression[]
				{
					TypedDataSetGenerator.Str(dataRelation.RelationName),
					codeArrayCreateExpression3,
					codeArrayCreateExpression4,
					TypedDataSetGenerator.Primitive(false)
				})));
				if (dataRelation.Nested)
				{
					codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), "Nested"), TypedDataSetGenerator.Primitive(true)));
				}
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "Add", TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation))));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), TypedDataSetGenerator.Str(dataRelation.RelationName))));
			}
			codeTypeDeclaration.Members.Add(codeMemberMethod4);
			codeTypeDeclaration.Members.Add(codeMemberMethod3);
			for (int num4 = 0; num4 < dataSet.Tables.Count; num4++)
			{
				string text2 = this.TablePropertyName(dataSet.Tables[num4]);
				CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(this.TableClassName(dataSet.Tables[num4]), text2, (MemberAttributes)24578);
				codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.Browsable", TypedDataSetGenerator.Primitive(false)));
				codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.DesignerSerializationVisibilityAttribute", TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(DesignerSerializationVisibility)), "Content")));
				codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataSet.Tables[num4]))));
				codeTypeDeclaration.Members.Add(codeMemberProperty);
				CodeMemberMethod codeMemberMethod10 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerialize" + text2, MemberAttributes.Private);
				codeMemberMethod10.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
				codeTypeDeclaration.Members.Add(codeMemberMethod10);
			}
			CodeMemberMethod codeMemberMethod11 = TypedDataSetGenerator.MethodDecl(typeof(void), "SchemaChanged", MemberAttributes.Private);
			codeMemberMethod11.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(object), "sender"));
			codeMemberMethod11.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(CollectionChangeEventArgs), "e"));
			codeMemberMethod11.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Action"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(CollectionChangeAction)), "Remove")), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"))));
			codeTypeDeclaration.Members.Add(codeMemberMethod11);
			bool flag = false;
			CodeMemberMethod codeMemberMethod12 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitExpressions", MemberAttributes.Private);
			foreach (object obj4 in dataSet.Tables)
			{
				DataTable dataTable2 = (DataTable)obj4;
				for (int num5 = 0; num5 < dataTable2.Columns.Count; num5++)
				{
					DataColumn dataColumn3 = dataTable2.Columns[num5];
					CodeExpression exp = TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataTable2)), this.TableColumnPropertyName(dataColumn3));
					if (dataColumn3.Expression.Length > 0)
					{
						flag = true;
						codeMemberMethod12.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(exp, "Expression"), TypedDataSetGenerator.Str(dataColumn3.Expression)));
					}
				}
			}
			if (flag)
			{
				codeTypeDeclaration.Members.Add(codeMemberMethod12);
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitExpressions"));
			}
			return codeTypeDeclaration;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00055B90 File Offset: 0x00054F90
		private static CodeExpression This()
		{
			return new CodeThisReferenceExpression();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00055BA4 File Offset: 0x00054FA4
		private static CodeExpression Base()
		{
			return new CodeBaseReferenceExpression();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00055BB8 File Offset: 0x00054FB8
		private static CodeExpression Value()
		{
			return new CodePropertySetValueReferenceExpression();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00055BCC File Offset: 0x00054FCC
		private static CodeTypeReference Type(string type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00055BE0 File Offset: 0x00054FE0
		private static CodeTypeReference Type(Type type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00055BF4 File Offset: 0x00054FF4
		private static CodeTypeReference Type(string type, int rank)
		{
			return new CodeTypeReference(type, rank);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00055C08 File Offset: 0x00055008
		private static CodeTypeReferenceExpression TypeExpr(Type type)
		{
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00055C1C File Offset: 0x0005501C
		private static CodeTypeReferenceExpression TypeExpr(string type)
		{
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00055C30 File Offset: 0x00055030
		private static CodeExpression Cast(string type, CodeExpression expr)
		{
			return new CodeCastExpression(type, expr);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00055C44 File Offset: 0x00055044
		private static CodeExpression Cast(CodeTypeReference type, CodeExpression expr)
		{
			return new CodeCastExpression(type, expr);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00055C58 File Offset: 0x00055058
		private static CodeExpression TypeOf(string type)
		{
			return new CodeTypeOfExpression(type);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00055C6C File Offset: 0x0005506C
		private static CodeExpression Field(CodeExpression exp, string field)
		{
			return new CodeFieldReferenceExpression(exp, field);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00055C80 File Offset: 0x00055080
		private static CodeExpression Property(CodeExpression exp, string property)
		{
			return new CodePropertyReferenceExpression(exp, property);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00055C94 File Offset: 0x00055094
		private static CodeExpression Argument(string argument)
		{
			return new CodeArgumentReferenceExpression(argument);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00055CA8 File Offset: 0x000550A8
		private static CodeExpression Variable(string variable)
		{
			return new CodeVariableReferenceExpression(variable);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00055CBC File Offset: 0x000550BC
		private static CodeExpression Event(string eventName)
		{
			return new CodeEventReferenceExpression(TypedDataSetGenerator.This(), eventName);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00055CD4 File Offset: 0x000550D4
		private static CodeExpression New(string type, CodeExpression[] parameters)
		{
			return new CodeObjectCreateExpression(type, parameters);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00055CE8 File Offset: 0x000550E8
		private static CodeExpression New(Type type, CodeExpression[] parameters)
		{
			return new CodeObjectCreateExpression(type, parameters);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00055CFC File Offset: 0x000550FC
		private static CodeExpression Primitive(object primitive)
		{
			return new CodePrimitiveExpression(primitive);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00055D10 File Offset: 0x00055110
		private static CodeExpression Str(string str)
		{
			return TypedDataSetGenerator.Primitive(str);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00055D24 File Offset: 0x00055124
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression[] parameters)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, parameters);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00055D3C File Offset: 0x0005513C
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[0]);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00055D58 File Offset: 0x00055158
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression par)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[]
			{
				par
			});
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00055D78 File Offset: 0x00055178
		private static CodeExpression DelegateCall(CodeExpression targetObject, CodeExpression par)
		{
			return new CodeDelegateInvokeExpression(targetObject, new CodeExpression[]
			{
				TypedDataSetGenerator.This(),
				par
			});
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00055DA0 File Offset: 0x000551A0
		private static CodeExpression Indexer(CodeExpression targetObject, CodeExpression indices)
		{
			return new CodeIndexerExpression(targetObject, new CodeExpression[]
			{
				indices
			});
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00055DC0 File Offset: 0x000551C0
		private static CodeBinaryOperatorExpression BinOperator(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			return new CodeBinaryOperatorExpression(left, op, right);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00055DD8 File Offset: 0x000551D8
		private static CodeBinaryOperatorExpression IdNotEQ(CodeExpression left, CodeExpression right)
		{
			return TypedDataSetGenerator.BinOperator(left, CodeBinaryOperatorType.IdentityInequality, right);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00055DF0 File Offset: 0x000551F0
		private static CodeBinaryOperatorExpression EQ(CodeExpression left, CodeExpression right)
		{
			return TypedDataSetGenerator.BinOperator(left, CodeBinaryOperatorType.ValueEquality, right);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00055E08 File Offset: 0x00055208
		private static CodeStatement Stm(CodeExpression expr)
		{
			return new CodeExpressionStatement(expr);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00055E1C File Offset: 0x0005521C
		private static CodeStatement Return(CodeExpression expr)
		{
			return new CodeMethodReturnStatement(expr);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00055E30 File Offset: 0x00055230
		private static CodeStatement Return()
		{
			return new CodeMethodReturnStatement();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00055E44 File Offset: 0x00055244
		private static CodeStatement Assign(CodeExpression left, CodeExpression right)
		{
			return new CodeAssignStatement(left, right);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00055E58 File Offset: 0x00055258
		private static CodeStatement Throw(Type exception, string arg, string inner)
		{
			return new CodeThrowExceptionStatement(TypedDataSetGenerator.New(exception, new CodeExpression[]
			{
				TypedDataSetGenerator.Str(Res.GetString(arg)),
				TypedDataSetGenerator.Variable(inner)
			}));
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00055E90 File Offset: 0x00055290
		private static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms, CodeStatement[] falseStms)
		{
			return new CodeConditionStatement(cond, trueStms, falseStms);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00055EA8 File Offset: 0x000552A8
		private static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms)
		{
			return new CodeConditionStatement(cond, trueStms);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00055EBC File Offset: 0x000552BC
		private static CodeStatement If(CodeExpression cond, CodeStatement trueStm)
		{
			return TypedDataSetGenerator.If(cond, new CodeStatement[]
			{
				trueStm
			});
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00055EDC File Offset: 0x000552DC
		private static CodeMemberField FieldDecl(string type, string name)
		{
			return new CodeMemberField(type, name);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00055EF0 File Offset: 0x000552F0
		private static CodeMemberField FieldDecl(Type type, string name)
		{
			return new CodeMemberField(type, name);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00055F04 File Offset: 0x00055304
		private static CodeMemberMethod Method(CodeTypeReference type, string name, MemberAttributes attributes)
		{
			return new CodeMemberMethod
			{
				ReturnType = type,
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00055F30 File Offset: 0x00055330
		private static CodeMemberMethod MethodDecl(Type type, string name, MemberAttributes attributes)
		{
			return TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(type), name, attributes);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00055F4C File Offset: 0x0005534C
		private static CodeMemberMethod MethodDecl(string type, string name, MemberAttributes attributes)
		{
			return TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(type), name, attributes);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00055F68 File Offset: 0x00055368
		private static CodeMemberProperty PropertyDecl(string type, string name, MemberAttributes attributes)
		{
			return new CodeMemberProperty
			{
				Type = TypedDataSetGenerator.Type(type),
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00055F98 File Offset: 0x00055398
		private static CodeMemberProperty PropertyDecl(Type type, string name, MemberAttributes attributes)
		{
			return new CodeMemberProperty
			{
				Type = TypedDataSetGenerator.Type(type),
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00055FC8 File Offset: 0x000553C8
		private static CodeStatement VariableDecl(Type type, string name)
		{
			return new CodeVariableDeclarationStatement(type, name);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00055FDC File Offset: 0x000553DC
		private static CodeStatement VariableDecl(string type, string name, CodeExpression initExpr)
		{
			return new CodeVariableDeclarationStatement(type, name, initExpr);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00055FF4 File Offset: 0x000553F4
		private static CodeStatement VariableDecl(Type type, string name, CodeExpression initExpr)
		{
			return new CodeVariableDeclarationStatement(type, name, initExpr);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0005600C File Offset: 0x0005540C
		private static CodeMemberEvent EventDecl(string type, string name)
		{
			return new CodeMemberEvent
			{
				Name = name,
				Type = TypedDataSetGenerator.Type(type),
				Attributes = (MemberAttributes)24578
			};
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00056040 File Offset: 0x00055440
		private static CodeParameterDeclarationExpression ParameterDecl(string type, string name)
		{
			return new CodeParameterDeclarationExpression(type, name);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00056054 File Offset: 0x00055454
		private static CodeParameterDeclarationExpression ParameterDecl(Type type, string name)
		{
			return new CodeParameterDeclarationExpression(type, name);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00056068 File Offset: 0x00055468
		private static CodeAttributeDeclaration AttributeDecl(string name)
		{
			return new CodeAttributeDeclaration(name);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0005607C File Offset: 0x0005547C
		private static CodeAttributeDeclaration AttributeDecl(string name, CodeExpression value)
		{
			return new CodeAttributeDeclaration(name, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(value)
			});
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000560A0 File Offset: 0x000554A0
		private static CodeStatement Try(CodeStatement tryStmnt, CodeCatchClause catchClause)
		{
			return new CodeTryCatchFinallyStatement(new CodeStatement[]
			{
				tryStmnt
			}, new CodeCatchClause[]
			{
				catchClause
			});
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000560C8 File Offset: 0x000554C8
		private static CodeCatchClause Catch(Type type, string name, CodeStatement catchStmnt)
		{
			return new CodeCatchClause
			{
				CatchExceptionType = TypedDataSetGenerator.Type(type),
				LocalName = name,
				Statements = 
				{
					catchStmnt
				}
			};
		}

		// Token: 0x040002B6 RID: 694
		private bool useExtendedNaming;

		// Token: 0x040002B7 RID: 695
		private ICodeGenerator codeGen;

		// Token: 0x040002B8 RID: 696
		private ArrayList errorList;

		// Token: 0x040002B9 RID: 697
		private ArrayList conflictingTables;

		// Token: 0x040002BA RID: 698
		private Hashtable lookupIdentifiers;
	}
}
