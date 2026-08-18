using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace System.Data.Design
{
	// Token: 0x0200027A RID: 634
	internal sealed class TypedRowGenerator
	{
		// Token: 0x0600181B RID: 6171 RVA: 0x0008977C File Offset: 0x0008797C
		internal TypedRowGenerator(TypedDataSourceCodeGenerator codeGenerator)
		{
			this.codeGenerator = codeGenerator;
			this.convertXmlToObject = typeof(DataColumn).GetMethod("ConvertXmlToObject", BindingFlags.Instance | BindingFlags.NonPublic, null, CallingConventions.Any, new Type[]
			{
				typeof(string)
			}, null);
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000897C8 File Offset: 0x000879C8
		internal MethodInfo ConvertXmlToObject
		{
			get
			{
				return this.convertXmlToObject;
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000897D0 File Offset: 0x000879D0
		internal void GenerateRows(CodeTypeDeclaration dataSourceClass)
		{
			foreach (object obj in this.codeGenerator.TableHandler.Tables)
			{
				DesignTable table = (DesignTable)obj;
				dataSourceClass.Members.Add(this.GenerateRow(table));
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00089840 File Offset: 0x00087A40
		internal void GenerateTypedRowEventHandlers(CodeTypeDeclaration dataSourceClass)
		{
			if (this.codeGenerator.CodeProvider.Supports(GeneratorSupport.DeclareEvents) && this.codeGenerator.CodeProvider.Supports(GeneratorSupport.DeclareDelegates))
			{
				foreach (object obj in this.codeGenerator.TableHandler.Tables)
				{
					DesignTable table = (DesignTable)obj;
					dataSourceClass.Members.Add(this.GenerateTypedRowEventHandler(table));
				}
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000898E0 File Offset: 0x00087AE0
		internal void GenerateTypedRowEventArgs(CodeTypeDeclaration dataSourceClass)
		{
			if (this.codeGenerator.CodeProvider.Supports(GeneratorSupport.DeclareEvents) && this.codeGenerator.CodeProvider.Supports(GeneratorSupport.DeclareDelegates))
			{
				foreach (object obj in this.codeGenerator.TableHandler.Tables)
				{
					DesignTable designTable = (DesignTable)obj;
					dataSourceClass.Members.Add(this.CreateTypedRowEventArg(designTable));
				}
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00089980 File Offset: 0x00087B80
		private CodeTypeDeclaration CreateTypedRowEventArg(DesignTable designTable)
		{
			if (designTable == null)
			{
				throw new InternalException("DesignTable should not be null.");
			}
			DataTable dataTable = designTable.DataTable;
			string generatorRowClassName = designTable.GeneratorRowClassName;
			string generatorTableClassName = designTable.GeneratorTableClassName;
			string generatorRowClassName2 = designTable.GeneratorRowClassName;
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class(designTable.GeneratorRowEvArgName, false, TypeAttributes.Public);
			codeTypeDeclaration.BaseTypes.Add(CodeGenHelper.GlobalType(typeof(EventArgs)));
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Row event argument class", true));
			codeTypeDeclaration.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.Type(generatorRowClassName2), "eventRow"));
			codeTypeDeclaration.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(DataRowAction)), "eventAction"));
			codeTypeDeclaration.Members.Add(this.EventArgConstructor(generatorRowClassName2));
			CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.Type(generatorRowClassName2), "Row", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), "eventRow")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.GlobalType(typeof(DataRowAction)), "Action", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), "eventAction")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			return codeTypeDeclaration;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00089AEC File Offset: 0x00087CEC
		private CodeTypeDelegate GenerateTypedRowEventHandler(DesignTable table)
		{
			if (table == null)
			{
				throw new InternalException("DesignTable should not be null.");
			}
			string generatorRowClassName = table.GeneratorRowClassName;
			CodeTypeDelegate codeTypeDelegate = new CodeTypeDelegate(table.GeneratorRowEvHandlerName);
			codeTypeDelegate.TypeAttributes |= TypeAttributes.Public;
			codeTypeDelegate.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(object)), "sender"));
			codeTypeDelegate.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.Type(table.GeneratorRowEvArgName), "e"));
			codeTypeDelegate.CustomAttributes.Add(CodeGenHelper.GeneratedCodeAttributeDecl());
			return codeTypeDelegate;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00089B80 File Offset: 0x00087D80
		private CodeTypeDeclaration GenerateRow(DesignTable table)
		{
			if (table == null)
			{
				throw new InternalException("DesignTable should not be null.");
			}
			string generatorRowClassName = table.GeneratorRowClassName;
			string generatorTableClassName = table.GeneratorTableClassName;
			string generatorTableVarName = table.GeneratorTableVarName;
			TypedColumnHandler columnHandler = this.codeGenerator.TableHandler.GetColumnHandler(table.Name);
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class(generatorRowClassName, true, TypeAttributes.Public);
			codeTypeDeclaration.BaseTypes.Add(CodeGenHelper.GlobalType(typeof(DataRow)));
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Represents strongly named DataRow class.", true));
			codeTypeDeclaration.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.Type(generatorTableClassName), generatorTableVarName));
			codeTypeDeclaration.Members.Add(this.RowConstructor(generatorTableClassName, generatorTableVarName));
			columnHandler.AddRowColumnProperties(codeTypeDeclaration);
			columnHandler.AddRowGetRelatedRowsMethods(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00089C48 File Offset: 0x00087E48
		private CodeConstructor RowConstructor(string tableClassName, string tableFieldName)
		{
			CodeConstructor codeConstructor = CodeGenHelper.Constructor((MemberAttributes)4098);
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRowBuilder)), "rb"));
			codeConstructor.BaseConstructorArgs.Add(CodeGenHelper.Argument("rb"));
			codeConstructor.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), tableFieldName), CodeGenHelper.Cast(CodeGenHelper.Type(tableClassName), CodeGenHelper.Property(CodeGenHelper.This(), "Table"))));
			return codeConstructor;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00089CD4 File Offset: 0x00087ED4
		private CodeConstructor EventArgConstructor(string rowConcreteClassName)
		{
			CodeConstructor codeConstructor = CodeGenHelper.Constructor((MemberAttributes)24578);
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.Type(rowConcreteClassName), "row"));
			codeConstructor.Parameters.Add(CodeGenHelper.ParameterDecl(CodeGenHelper.GlobalType(typeof(DataRowAction)), "action"));
			codeConstructor.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), "eventRow"), CodeGenHelper.Argument("row")));
			codeConstructor.Statements.Add(CodeGenHelper.Assign(CodeGenHelper.Field(CodeGenHelper.This(), "eventAction"), CodeGenHelper.Argument("action")));
			return codeConstructor;
		}

		// Token: 0x04000C98 RID: 3224
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000C99 RID: 3225
		private MethodInfo convertXmlToObject;
	}
}
