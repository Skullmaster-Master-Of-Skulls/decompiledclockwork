using System;
using System.CodeDom;

namespace System.Data.Design
{
	// Token: 0x0200025D RID: 605
	internal sealed class RelationHandler
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x0008140D File Offset: 0x0007F60D
		internal RelationHandler(TypedDataSourceCodeGenerator codeGenerator, DesignRelationCollection relations)
		{
			this.codeGenerator = codeGenerator;
			this.relations = relations;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00081423 File Offset: 0x0007F623
		internal DesignRelationCollection Relations
		{
			get
			{
				return this.relations;
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0008142C File Offset: 0x0007F62C
		internal void AddPrivateVars(CodeTypeDeclaration dataSourceClass)
		{
			if (dataSourceClass == null)
			{
				throw new InternalException("DataSource CodeTypeDeclaration should not be null.");
			}
			if (this.relations == null)
			{
				return;
			}
			foreach (object obj in this.relations)
			{
				DesignRelation designRelation = (DesignRelation)obj;
				if (designRelation.DataRelation != null)
				{
					string generatorRelationVarName = designRelation.GeneratorRelationVarName;
					dataSourceClass.Members.Add(CodeGenHelper.FieldDecl(CodeGenHelper.GlobalType(typeof(DataRelation)), generatorRelationVarName));
				}
			}
		}

		// Token: 0x04000BE1 RID: 3041
		private TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000BE2 RID: 3042
		private DesignRelationCollection relations;
	}
}
