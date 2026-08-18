using System;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x0200021E RID: 542
	internal sealed class DataComponentNameHandler
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000720F4 File Offset: 0x000702F4
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x000720FC File Offset: 0x000702FC
		internal bool GlobalSources
		{
			get
			{
				return this.globalSources;
			}
			set
			{
				this.globalSources = value;
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00072105 File Offset: 0x00070305
		internal void GenerateMemberNames(DesignTable designTable, CodeDomProvider codeProvider, bool languageCaseInsensitive, ArrayList problemList)
		{
			this.languageCaseInsensitive = languageCaseInsensitive;
			this.validator = new MemberNameValidator(null, codeProvider, this.languageCaseInsensitive);
			this.validator.UseSuffix = true;
			this.AddReservedNames();
			this.ProcessMemberNames(designTable);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0007213C File Offset: 0x0007033C
		private void AddReservedNames()
		{
			this.validator.GetNewMemberName(DataComponentNameHandler.initMethodName);
			this.validator.GetNewMemberName(DataComponentNameHandler.deleteMethodName);
			this.validator.GetNewMemberName(DataComponentNameHandler.insertMethodName);
			this.validator.GetNewMemberName(DataComponentNameHandler.updateMethodName);
			this.validator.GetNewMemberName(DataComponentNameHandler.adapterVariableName);
			this.validator.GetNewMemberName(DataComponentNameHandler.adapterPropertyName);
			this.validator.GetNewMemberName(DataComponentNameHandler.initAdapter);
			this.validator.GetNewMemberName(DataComponentNameHandler.selectCmdCollectionVariableName);
			this.validator.GetNewMemberName(DataComponentNameHandler.selectCmdCollectionPropertyName);
			this.validator.GetNewMemberName(DataComponentNameHandler.initCmdCollection);
			this.validator.GetNewMemberName(DataComponentNameHandler.defaultConnectionVariableName);
			this.validator.GetNewMemberName(DataComponentNameHandler.defaultConnectionPropertyName);
			this.validator.GetNewMemberName(DataComponentNameHandler.transactionVariableName);
			this.validator.GetNewMemberName(DataComponentNameHandler.transactionPropertyName);
			this.validator.GetNewMemberName(DataComponentNameHandler.initConnection);
			this.validator.GetNewMemberName(DataComponentNameHandler.clearBeforeFillVariableName);
			this.validator.GetNewMemberName(DataComponentNameHandler.clearBeforeFillPropertyName);
			this.validator.GetNewMemberName("TableAdapterManager");
			this.validator.GetNewMemberName("UpdateAll");
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0007228C File Offset: 0x0007048C
		private void ProcessMemberNames(DesignTable designTable)
		{
			this.ProcessClassName(designTable);
			if (!this.GlobalSources && designTable.MainSource != null)
			{
				this.ProcessSourceName((DbSource)designTable.MainSource);
			}
			if (designTable.Sources != null)
			{
				foreach (object obj in designTable.Sources)
				{
					Source source = (Source)obj;
					this.ProcessSourceName((DbSource)source);
				}
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0007231C File Offset: 0x0007051C
		internal void ProcessClassName(DesignTable table)
		{
			if (!StringUtil.EqualValue(table.DataAccessorName, table.UserDataComponentName, this.languageCaseInsensitive) || StringUtil.Empty(table.GeneratorDataComponentClassName))
			{
				table.GeneratorDataComponentClassName = this.validator.GenerateIdName(table.DataAccessorName);
				return;
			}
			table.GeneratorDataComponentClassName = this.validator.GenerateIdName(table.GeneratorDataComponentClassName);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00072384 File Offset: 0x00070584
		internal void ProcessSourceName(DbSource source)
		{
			bool flag = !StringUtil.EqualValue(source.Name, source.UserSourceName, this.languageCaseInsensitive);
			bool flag2 = !StringUtil.EqualValue(source.GetMethodName, source.UserGetMethodName, this.languageCaseInsensitive);
			if (source.GenerateMethods == GenerateMethodTypes.Fill || source.GenerateMethods == GenerateMethodTypes.Both)
			{
				if (flag || StringUtil.Empty(source.GeneratorSourceName))
				{
					source.GeneratorSourceName = this.validator.GenerateIdName(source.Name);
				}
				else
				{
					source.GeneratorSourceName = this.validator.GenerateIdName(source.GeneratorSourceName);
				}
			}
			if (source.QueryType == QueryType.Rowset && (source.GenerateMethods == GenerateMethodTypes.Get || source.GenerateMethods == GenerateMethodTypes.Both))
			{
				if (flag2 || StringUtil.Empty(source.GeneratorGetMethodName))
				{
					source.GeneratorGetMethodName = this.validator.GenerateIdName(source.GetMethodName);
				}
				else
				{
					source.GeneratorGetMethodName = this.validator.GenerateIdName(source.GeneratorGetMethodName);
				}
			}
			if (source.QueryType == QueryType.Rowset && source.GeneratePagingMethods)
			{
				if (source.GenerateMethods == GenerateMethodTypes.Fill || source.GenerateMethods == GenerateMethodTypes.Both)
				{
					if (flag || StringUtil.Empty(source.GeneratorSourceNameForPaging))
					{
						source.GeneratorSourceNameForPaging = this.validator.GenerateIdName(source.Name + DataComponentNameHandler.pagingMethodSuffix);
					}
					else
					{
						source.GeneratorSourceNameForPaging = this.validator.GenerateIdName(source.GeneratorSourceNameForPaging);
					}
				}
				if (source.GenerateMethods == GenerateMethodTypes.Get || source.GenerateMethods == GenerateMethodTypes.Both)
				{
					if (flag2 || StringUtil.Empty(source.GeneratorGetMethodNameForPaging))
					{
						source.GeneratorGetMethodNameForPaging = this.validator.GenerateIdName(source.GetMethodName + DataComponentNameHandler.pagingMethodSuffix);
						return;
					}
					source.GeneratorGetMethodNameForPaging = this.validator.GenerateIdName(source.GeneratorGetMethodNameForPaging);
				}
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00072540 File Offset: 0x00070740
		internal static string DeleteMethodName
		{
			get
			{
				return DataComponentNameHandler.deleteMethodName;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00072547 File Offset: 0x00070747
		internal static string UpdateMethodName
		{
			get
			{
				return DataComponentNameHandler.updateMethodName;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0007254E File Offset: 0x0007074E
		internal static string InsertMethodName
		{
			get
			{
				return DataComponentNameHandler.insertMethodName;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00072555 File Offset: 0x00070755
		internal static string AdapterVariableName
		{
			get
			{
				return DataComponentNameHandler.adapterVariableName;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0007255C File Offset: 0x0007075C
		internal static string AdapterPropertyName
		{
			get
			{
				return DataComponentNameHandler.adapterPropertyName;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00072563 File Offset: 0x00070763
		internal static string InitAdapter
		{
			get
			{
				return DataComponentNameHandler.initAdapter;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0007256A File Offset: 0x0007076A
		internal static string SelectCmdCollectionVariableName
		{
			get
			{
				return DataComponentNameHandler.selectCmdCollectionVariableName;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00072571 File Offset: 0x00070771
		internal static string SelectCmdCollectionPropertyName
		{
			get
			{
				return DataComponentNameHandler.selectCmdCollectionPropertyName;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00072578 File Offset: 0x00070778
		internal static string InitCmdCollection
		{
			get
			{
				return DataComponentNameHandler.initCmdCollection;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0007257F File Offset: 0x0007077F
		internal static string DefaultConnectionVariableName
		{
			get
			{
				return DataComponentNameHandler.defaultConnectionVariableName;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00072586 File Offset: 0x00070786
		internal static string DefaultConnectionPropertyName
		{
			get
			{
				return DataComponentNameHandler.defaultConnectionPropertyName;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0007258D File Offset: 0x0007078D
		internal static string TransactionPropertyName
		{
			get
			{
				return DataComponentNameHandler.transactionPropertyName;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00072594 File Offset: 0x00070794
		internal static string TransactionVariableName
		{
			get
			{
				return DataComponentNameHandler.transactionVariableName;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0007259B File Offset: 0x0007079B
		internal static string InitConnection
		{
			get
			{
				return DataComponentNameHandler.initConnection;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000725A2 File Offset: 0x000707A2
		internal static string PagingMethodSuffix
		{
			get
			{
				return DataComponentNameHandler.pagingMethodSuffix;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000725A9 File Offset: 0x000707A9
		internal static string ClearBeforeFillVariableName
		{
			get
			{
				return DataComponentNameHandler.clearBeforeFillVariableName;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x000725B0 File Offset: 0x000707B0
		internal static string ClearBeforeFillPropertyName
		{
			get
			{
				return DataComponentNameHandler.clearBeforeFillPropertyName;
			}
		}

		// Token: 0x04000AB8 RID: 2744
		private MemberNameValidator validator;

		// Token: 0x04000AB9 RID: 2745
		private bool languageCaseInsensitive;

		// Token: 0x04000ABA RID: 2746
		private bool globalSources;

		// Token: 0x04000ABB RID: 2747
		private static readonly string pagingMethodSuffix = "Page";

		// Token: 0x04000ABC RID: 2748
		private static readonly string initMethodName = "InitClass";

		// Token: 0x04000ABD RID: 2749
		private static readonly string deleteMethodName = "Delete";

		// Token: 0x04000ABE RID: 2750
		private static readonly string insertMethodName = "Insert";

		// Token: 0x04000ABF RID: 2751
		private static readonly string updateMethodName = "Update";

		// Token: 0x04000AC0 RID: 2752
		private static readonly string adapterVariableName = "_adapter";

		// Token: 0x04000AC1 RID: 2753
		private static readonly string adapterPropertyName = "Adapter";

		// Token: 0x04000AC2 RID: 2754
		private static readonly string initAdapter = "InitAdapter";

		// Token: 0x04000AC3 RID: 2755
		private static readonly string selectCmdCollectionVariableName = "_commandCollection";

		// Token: 0x04000AC4 RID: 2756
		private static readonly string selectCmdCollectionPropertyName = "CommandCollection";

		// Token: 0x04000AC5 RID: 2757
		private static readonly string initCmdCollection = "InitCommandCollection";

		// Token: 0x04000AC6 RID: 2758
		private static readonly string defaultConnectionVariableName = "_connection";

		// Token: 0x04000AC7 RID: 2759
		private static readonly string defaultConnectionPropertyName = "Connection";

		// Token: 0x04000AC8 RID: 2760
		private static readonly string transactionVariableName = "_transaction";

		// Token: 0x04000AC9 RID: 2761
		private static readonly string transactionPropertyName = "Transaction";

		// Token: 0x04000ACA RID: 2762
		private static readonly string initConnection = "InitConnection";

		// Token: 0x04000ACB RID: 2763
		private static readonly string clearBeforeFillVariableName = "_clearBeforeFill";

		// Token: 0x04000ACC RID: 2764
		private static readonly string clearBeforeFillPropertyName = "ClearBeforeFill";
	}
}
