using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Design
{
	// Token: 0x0200027D RID: 637
	internal sealed class TypedTableHandler
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x0008A19E File Offset: 0x0008839E
		internal TypedTableHandler(TypedDataSourceCodeGenerator codeGenerator, DesignTableCollection tables)
		{
			this.codeGenerator = codeGenerator;
			this.tables = tables;
			this.tableGenerator = new TypedTableGenerator(codeGenerator);
			this.SetColumnHandlers();
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0008A1C6 File Offset: 0x000883C6
		internal DesignTableCollection Tables
		{
			get
			{
				return this.tables;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0008A1CE File Offset: 0x000883CE
		internal TypedColumnHandler GetColumnHandler(string tableName)
		{
			if (tableName == null)
			{
				return null;
			}
			return (TypedColumnHandler)this.columnHandlers[tableName];
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0008A1E8 File Offset: 0x000883E8
		internal void AddPrivateVars(CodeTypeDeclaration dataSourceClass)
		{
			if (this.tables == null)
			{
				return;
			}
			foreach (object obj in this.tables)
			{
				DesignTable designTable = (DesignTable)obj;
				string generatorTableClassName = designTable.GeneratorTableClassName;
				string generatorTableVarName = designTable.GeneratorTableVarName;
				dataSourceClass.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.Type(generatorTableClassName), generatorTableVarName));
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0008A26C File Offset: 0x0008846C
		internal void AddTableProperties(CodeTypeDeclaration dataSourceClass)
		{
			if (this.tables == null)
			{
				return;
			}
			foreach (object obj in this.tables)
			{
				DesignTable designTable = (DesignTable)obj;
				string generatorTableClassName = designTable.GeneratorTableClassName;
				string generatorTablePropName = designTable.GeneratorTablePropName;
				string generatorTableVarName = designTable.GeneratorTableVarName;
				CodeMemberProperty codeMemberProperty = CodeGenHelper.PropertyDecl(CodeGenHelper.Type(generatorTableClassName), generatorTablePropName, (MemberAttributes)24578);
				codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.Browsable", CodeGenHelper.Primitive(false)));
				codeMemberProperty.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerSerializationVisibility", CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DesignerSerializationVisibility)), "Content")));
				codeMemberProperty.GetStatements.Add(CodeGenHelper.Return(CodeGenHelper.Field(CodeGenHelper.This(), generatorTableVarName)));
				dataSourceClass.Members.Add(codeMemberProperty);
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0008A378 File Offset: 0x00088578
		internal void AddTableClasses(CodeTypeDeclaration dataSourceClass)
		{
			this.tableGenerator.GenerateTables(dataSourceClass);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0008A388 File Offset: 0x00088588
		private void SetColumnHandlers()
		{
			this.columnHandlers = new Hashtable();
			foreach (object obj in this.tables)
			{
				DesignTable designTable = (DesignTable)obj;
				this.columnHandlers.Add(designTable.Name, new TypedColumnHandler(designTable, this.codeGenerator));
			}
		}

		// Token: 0x04000C9F RID: 3231
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000CA0 RID: 3232
		private TypedTableGenerator tableGenerator;

		// Token: 0x04000CA1 RID: 3233
		private DesignTableCollection tables;

		// Token: 0x04000CA2 RID: 3234
		private Hashtable columnHandlers;
	}
}
