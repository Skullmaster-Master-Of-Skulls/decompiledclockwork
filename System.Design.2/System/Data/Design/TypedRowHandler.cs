using System;
using System.CodeDom;

namespace System.Data.Design
{
	// Token: 0x0200027B RID: 635
	internal sealed class TypedRowHandler
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x00089D82 File Offset: 0x00087F82
		internal TypedRowHandler(TypedDataSourceCodeGenerator codeGenerator, DesignTableCollection tables)
		{
			this.codeGenerator = codeGenerator;
			this.tables = tables;
			this.rowGenerator = new TypedRowGenerator(codeGenerator);
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00089DA4 File Offset: 0x00087FA4
		internal TypedRowGenerator RowGenerator
		{
			get
			{
				return this.rowGenerator;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00089DAC File Offset: 0x00087FAC
		internal void AddTypedRowEvents(CodeTypeDeclaration dataTableClass, string tableName)
		{
			DesignTable designTable = this.codeGenerator.TableHandler.Tables[tableName];
			string generatorRowClassName = designTable.GeneratorRowClassName;
			string generatorRowEvHandlerName = designTable.GeneratorRowEvHandlerName;
			dataTableClass.Members.Add(CodeGenHelper.EventDecl(generatorRowEvHandlerName, designTable.GeneratorRowChangingName));
			dataTableClass.Members.Add(CodeGenHelper.EventDecl(generatorRowEvHandlerName, designTable.GeneratorRowChangedName));
			dataTableClass.Members.Add(CodeGenHelper.EventDecl(generatorRowEvHandlerName, designTable.GeneratorRowDeletingName));
			dataTableClass.Members.Add(CodeGenHelper.EventDecl(generatorRowEvHandlerName, designTable.GeneratorRowDeletedName));
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00089E3E File Offset: 0x0008803E
		internal void AddTypedRows(CodeTypeDeclaration dataSourceClass)
		{
			this.rowGenerator.GenerateRows(dataSourceClass);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00089E4C File Offset: 0x0008804C
		internal void AddTypedRowEventHandlers(CodeTypeDeclaration dataSourceClass)
		{
			this.rowGenerator.GenerateTypedRowEventHandlers(dataSourceClass);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00089E5A File Offset: 0x0008805A
		internal void AddTypedRowEventArgs(CodeTypeDeclaration dataSourceClass)
		{
			this.rowGenerator.GenerateTypedRowEventArgs(dataSourceClass);
		}

		// Token: 0x04000C9A RID: 3226
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000C9B RID: 3227
		private DesignTableCollection tables;

		// Token: 0x04000C9C RID: 3228
		private TypedRowGenerator rowGenerator;
	}
}
