using System;
using System.CodeDom.Compiler;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x0200026A RID: 618
	internal sealed class TableAdapterManagerNameHandler
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x00084D3C File Offset: 0x00082F3C
		public TableAdapterManagerNameHandler(CodeDomProvider provider)
		{
			this.codePrivider = provider;
			this.languageCaseInsensitive = ((this.codePrivider.LanguageOptions & LanguageOptions.CaseInsensitive) == LanguageOptions.CaseInsensitive);
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00084D64 File Offset: 0x00082F64
		private MemberNameValidator TableAdapterManagerValidator
		{
			get
			{
				if (this.tableAdapterManagerValidator == null)
				{
					this.tableAdapterManagerValidator = new MemberNameValidator(new string[]
					{
						"SelfReferenceComparer",
						"UpdateAll",
						"SortSelfReferenceRows",
						"MatchTableAdapterConnection",
						"_connection",
						"Connection",
						"_backupDataSetBeforeUpdate",
						"BackupDataSetBeforeUpdate",
						"TableAdapterInstanceCount",
						"UpdateOrder",
						"_updateOrder",
						"UpdateOrderOption",
						"UpdateUpdatedRows",
						"UpdateInsertedRows",
						"UpdateDeletedRows",
						"GetRealUpdatedRows"
					}, this.codePrivider, this.languageCaseInsensitive);
				}
				return this.tableAdapterManagerValidator;
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00084E27 File Offset: 0x00083027
		internal string GetNewMemberName(string memberName)
		{
			return this.TableAdapterManagerValidator.GetNewMemberName(memberName);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00084E35 File Offset: 0x00083035
		internal string GetTableAdapterPropName(string className)
		{
			return this.GetNewMemberName(className);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00084E40 File Offset: 0x00083040
		internal string GetTableAdapterVarName(string propName)
		{
			propName = "_" + char.ToLower(propName[0], CultureInfo.InvariantCulture).ToString() + propName.Remove(0, 1);
			return this.GetNewMemberName(propName);
		}

		// Token: 0x04000C11 RID: 3089
		internal const string TableAdapterManagerClassName = "TableAdapterManager";

		// Token: 0x04000C12 RID: 3090
		internal const string SelfRefComparerClass = "SelfReferenceComparer";

		// Token: 0x04000C13 RID: 3091
		internal const string UpdateAllMethod = "UpdateAll";

		// Token: 0x04000C14 RID: 3092
		internal const string SortSelfRefRowsMethod = "SortSelfReferenceRows";

		// Token: 0x04000C15 RID: 3093
		internal const string MatchTAConnectionMethod = "MatchTableAdapterConnection";

		// Token: 0x04000C16 RID: 3094
		internal const string UpdateAllRevertConnectionsVar = "revertConnections";

		// Token: 0x04000C17 RID: 3095
		internal const string ConnectionVar = "_connection";

		// Token: 0x04000C18 RID: 3096
		internal const string ConnectionProperty = "Connection";

		// Token: 0x04000C19 RID: 3097
		internal const string BackupDataSetBeforeUpdateVar = "_backupDataSetBeforeUpdate";

		// Token: 0x04000C1A RID: 3098
		internal const string BackupDataSetBeforeUpdateProperty = "BackupDataSetBeforeUpdate";

		// Token: 0x04000C1B RID: 3099
		internal const string TableAdapterInstanceCountProperty = "TableAdapterInstanceCount";

		// Token: 0x04000C1C RID: 3100
		internal const string UpdateOrderOptionProperty = "UpdateOrder";

		// Token: 0x04000C1D RID: 3101
		internal const string UpdateOrderOptionVar = "_updateOrder";

		// Token: 0x04000C1E RID: 3102
		internal const string UpdateOrderOptionEnum = "UpdateOrderOption";

		// Token: 0x04000C1F RID: 3103
		internal const string UpdateOrderOptionEnumIUD = "InsertUpdateDelete";

		// Token: 0x04000C20 RID: 3104
		internal const string UpdateOrderOptionEnumUID = "UpdateInsertDelete";

		// Token: 0x04000C21 RID: 3105
		internal const string UpdateUpdatedRowsMethod = "UpdateUpdatedRows";

		// Token: 0x04000C22 RID: 3106
		internal const string UpdateInsertedRowsMethod = "UpdateInsertedRows";

		// Token: 0x04000C23 RID: 3107
		internal const string UpdateDeletedRowsMethod = "UpdateDeletedRows";

		// Token: 0x04000C24 RID: 3108
		internal const string GetRealUpdatedRowsMethod = "GetRealUpdatedRows";

		// Token: 0x04000C25 RID: 3109
		private MemberNameValidator tableAdapterManagerValidator;

		// Token: 0x04000C26 RID: 3110
		private bool languageCaseInsensitive;

		// Token: 0x04000C27 RID: 3111
		private CodeDomProvider codePrivider;
	}
}
