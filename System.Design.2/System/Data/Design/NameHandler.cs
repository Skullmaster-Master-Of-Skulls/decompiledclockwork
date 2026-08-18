using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Reflection;

namespace System.Data.Design
{
	// Token: 0x02000251 RID: 593
	internal sealed class NameHandler
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x0007D772 File Offset: 0x0007B972
		internal NameHandler(CodeDomProvider codeProvider)
		{
			if (codeProvider == null)
			{
				throw new ArgumentException("codeProvider");
			}
			NameHandler.codeProvider = codeProvider;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0007D790 File Offset: 0x0007B990
		internal void GenerateMemberNames(DesignDataSource dataSource, ArrayList problemList)
		{
			if (dataSource == null || NameHandler.codeProvider == null)
			{
				throw new InternalException("DesignDataSource or/and CodeDomProvider parameters are null.");
			}
			NameHandler.InitLookupIdentifiers();
			this.dataSourceHandler = new DataSourceNameHandler();
			this.dataSourceHandler.GenerateMemberNames(dataSource, NameHandler.codeProvider, this.languageCaseInsensitive, problemList);
			foreach (object obj in dataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				DataTableNameHandler dataTableNameHandler = new DataTableNameHandler();
				dataTableNameHandler.GenerateMemberNames(designTable, NameHandler.codeProvider, this.languageCaseInsensitive, problemList);
				DataComponentNameHandler dataComponentNameHandler = new DataComponentNameHandler();
				dataComponentNameHandler.GenerateMemberNames(designTable, NameHandler.codeProvider, this.languageCaseInsensitive, problemList);
			}
			if (dataSource.Sources != null && dataSource.Sources.Count > 0)
			{
				DesignTable designTable2 = new DesignTable();
				designTable2.TableType = TableType.RadTable;
				designTable2.DataAccessorName = dataSource.FunctionsComponentName;
				designTable2.UserDataComponentName = dataSource.UserFunctionsComponentName;
				designTable2.GeneratorDataComponentClassName = dataSource.GeneratorFunctionsComponentClassName;
				foreach (object obj2 in dataSource.Sources)
				{
					Source s = (Source)obj2;
					designTable2.Sources.Add(s);
				}
				new DataComponentNameHandler
				{
					GlobalSources = true
				}.GenerateMemberNames(designTable2, NameHandler.codeProvider, this.languageCaseInsensitive, problemList);
				dataSource.GeneratorFunctionsComponentClassName = designTable2.GeneratorDataComponentClassName;
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0007D930 File Offset: 0x0007BB30
		internal static string FixIdName(string inVarName)
		{
			if (NameHandler.lookupIdentifiers == null)
			{
				NameHandler.InitLookupIdentifiers();
			}
			string text = (string)NameHandler.lookupIdentifiers[inVarName];
			if (text == null)
			{
				text = MemberNameValidator.GenerateIdName(inVarName, NameHandler.codeProvider, false);
				while (NameHandler.lookupIdentifiers.ContainsValue(text))
				{
					text = "_" + text;
				}
				NameHandler.lookupIdentifiers[inVarName] = text;
			}
			return text;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0007D994 File Offset: 0x0007BB94
		private static void InitLookupIdentifiers()
		{
			NameHandler.lookupIdentifiers = new Hashtable();
			PropertyInfo[] properties = typeof(DataRow).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				NameHandler.lookupIdentifiers[propertyInfo.Name] = "_" + propertyInfo.Name;
			}
		}

		// Token: 0x04000BAB RID: 2987
		private const string FunctionsTableName = "Queries";

		// Token: 0x04000BAC RID: 2988
		private DataSourceNameHandler dataSourceHandler;

		// Token: 0x04000BAD RID: 2989
		private static CodeDomProvider codeProvider;

		// Token: 0x04000BAE RID: 2990
		private bool languageCaseInsensitive;

		// Token: 0x04000BAF RID: 2991
		private static Hashtable lookupIdentifiers;
	}
}
