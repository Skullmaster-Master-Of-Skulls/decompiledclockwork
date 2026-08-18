using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Xml.Serialization;

namespace System.Data.Design
{
	// Token: 0x02000279 RID: 633
	internal sealed class TypedDataSourceCodeGenerator
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x000890AC File Offset: 0x000872AC
		internal TypedDataSourceCodeGenerator()
		{
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000890BF File Offset: 0x000872BF
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x000890C7 File Offset: 0x000872C7
		internal CodeDomProvider CodeProvider
		{
			get
			{
				return this.codeProvider;
			}
			set
			{
				this.codeProvider = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000890D0 File Offset: 0x000872D0
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000890D8 File Offset: 0x000872D8
		internal IDictionary UserData
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000890E1 File Offset: 0x000872E1
		internal string DataSourceName
		{
			get
			{
				return this.designDataSource.GeneratorDataSetName;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000890EE File Offset: 0x000872EE
		internal ArrayList ProblemList
		{
			get
			{
				return this.problemList;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000890F6 File Offset: 0x000872F6
		internal TypedTableHandler TableHandler
		{
			get
			{
				return this.tableHandler;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000890FE File Offset: 0x000872FE
		internal RelationHandler RelationHandler
		{
			get
			{
				return this.relationHandler;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00089106 File Offset: 0x00087306
		internal TypedRowHandler RowHandler
		{
			get
			{
				return this.rowHandler;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0008910E File Offset: 0x0008730E
		internal bool GenerateExtendedProperties
		{
			get
			{
				return this.generateExtendedProperties;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00089116 File Offset: 0x00087316
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x0008911E File Offset: 0x0008731E
		internal bool GenerateSingleNamespace
		{
			get
			{
				return this.generateSingleNamespace;
			}
			set
			{
				this.generateSingleNamespace = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00089127 File Offset: 0x00087327
		internal TypedDataSetGenerator.GenerateOption GenerateOptions
		{
			get
			{
				return this.generateOption;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0008912F File Offset: 0x0008732F
		internal string DataSetNamespace
		{
			get
			{
				return this.dataSetNamespace;
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00089138 File Offset: 0x00087338
		internal void GenerateDataSource(DesignDataSource dtDataSource, CodeCompileUnit codeCompileUnit, CodeNamespace mainNamespace, string dataSetNamespace, TypedDataSetGenerator.GenerateOption generateOption)
		{
			this.designDataSource = dtDataSource;
			this.generateOption = generateOption;
			this.dataSetNamespace = dataSetNamespace;
			bool flag = (generateOption & TypedDataSetGenerator.GenerateOption.HierarchicalUpdate) == TypedDataSetGenerator.GenerateOption.HierarchicalUpdate;
			flag = (flag && dtDataSource.EnableTableAdapterManager);
			this.AddUserData(codeCompileUnit);
			CodeTypeDeclaration codeTypeDeclaration = this.CreateDataSourceDeclaration(dtDataSource);
			mainNamespace.Types.Add(codeTypeDeclaration);
			bool flag2 = CodeGenHelper.SupportsMultipleNamespaces(this.codeProvider);
			CodeNamespace codeNamespace = null;
			if (!this.GenerateSingleNamespace && flag2)
			{
				string text = this.CreateAdaptersNamespace(dtDataSource.GeneratorDataSetName);
				if (!StringUtil.Empty(mainNamespace.Name))
				{
					text = mainNamespace.Name + "." + text;
				}
				codeNamespace = new CodeNamespace(text);
			}
			DataComponentGenerator dataComponentGenerator = new DataComponentGenerator(this);
			bool flag3 = false;
			foreach (object obj in dtDataSource.DesignTables)
			{
				DesignTable designTable = (DesignTable)obj;
				if (designTable.TableType == TableType.RadTable)
				{
					flag3 = true;
					designTable.PropertyCache = new DesignTable.CodeGenPropertyCache(designTable);
					CodeTypeDeclaration codeTypeDeclaration2 = dataComponentGenerator.GenerateDataComponent(designTable, false, flag);
					if (this.GenerateSingleNamespace)
					{
						mainNamespace.Types.Add(codeTypeDeclaration2);
					}
					else if (flag2)
					{
						codeNamespace.Types.Add(codeTypeDeclaration2);
					}
					else
					{
						codeTypeDeclaration2.Name = codeTypeDeclaration.Name + codeTypeDeclaration2.Name;
						mainNamespace.Types.Add(codeTypeDeclaration2);
					}
				}
			}
			flag = (flag && flag3);
			if (dtDataSource.Sources != null && dtDataSource.Sources.Count > 0)
			{
				DesignTable designTable2 = new DesignTable();
				designTable2.TableType = TableType.RadTable;
				designTable2.MainSource = null;
				designTable2.GeneratorDataComponentClassName = dtDataSource.GeneratorFunctionsComponentClassName;
				foreach (object obj2 in dtDataSource.Sources)
				{
					Source s = (Source)obj2;
					designTable2.Sources.Add(s);
				}
				CodeTypeDeclaration codeTypeDeclaration3 = dataComponentGenerator.GenerateDataComponent(designTable2, true, flag);
				if (this.GenerateSingleNamespace)
				{
					mainNamespace.Types.Add(codeTypeDeclaration3);
				}
				else if (flag2)
				{
					codeNamespace.Types.Add(codeTypeDeclaration3);
				}
				else
				{
					codeTypeDeclaration3.Name = codeTypeDeclaration.Name + codeTypeDeclaration3.Name;
					mainNamespace.Types.Add(codeTypeDeclaration3);
				}
			}
			if (codeNamespace != null && codeNamespace.Types.Count > 0)
			{
				codeCompileUnit.Namespaces.Add(codeNamespace);
			}
			if (flag)
			{
				TableAdapterManagerGenerator tableAdapterManagerGenerator = new TableAdapterManagerGenerator(this);
				CodeTypeDeclaration codeTypeDeclaration4 = tableAdapterManagerGenerator.GenerateAdapterManager(this.designDataSource, codeTypeDeclaration);
				if (this.GenerateSingleNamespace)
				{
					mainNamespace.Types.Add(codeTypeDeclaration4);
					return;
				}
				if (flag2)
				{
					codeNamespace.Types.Add(codeTypeDeclaration4);
					return;
				}
				codeTypeDeclaration4.Name = codeTypeDeclaration.Name + codeTypeDeclaration4.Name;
				mainNamespace.Types.Add(codeTypeDeclaration4);
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008943C File Offset: 0x0008763C
		private void AddUserData(CodeCompileUnit compileUnit)
		{
			if (this.UserData != null)
			{
				foreach (object key in this.UserData.Keys)
				{
					compileUnit.UserData.Add(key, this.userData[key]);
				}
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000894B0 File Offset: 0x000876B0
		private CodeTypeDeclaration CreateDataSourceDeclaration(DesignDataSource dtDataSource)
		{
			if (dtDataSource.Name == null)
			{
				throw new DataSourceGeneratorException("DataSource name cannot be null.");
			}
			NameHandler nameHandler = new NameHandler(this.codeProvider);
			nameHandler.GenerateMemberNames(dtDataSource, this.problemList);
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class(dtDataSource.GeneratorDataSetName, true, dtDataSource.Modifier);
			codeTypeDeclaration.BaseTypes.Add(CodeGenHelper.GlobalType(typeof(DataSet)));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.Serializable"));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerCategoryAttribute", CodeGenHelper.Str("code")));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.ToolboxItem", CodeGenHelper.Primitive(true)));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(XmlSchemaProviderAttribute).FullName, CodeGenHelper.Primitive("GetTypedDataSetSchema")));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(XmlRootAttribute).FullName, CodeGenHelper.Primitive(dtDataSource.GeneratorDataSetName)));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(HelpKeywordAttribute).FullName, CodeGenHelper.Str("vs.data.DataSet")));
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Represents a strongly typed in-memory cache of data.", true));
			this.tableHandler = new TypedTableHandler(this, dtDataSource.DesignTables);
			this.relationHandler = new RelationHandler(this, dtDataSource.DesignRelations);
			this.rowHandler = new TypedRowHandler(this, dtDataSource.DesignTables);
			DatasetMethodGenerator datasetMethodGenerator = new DatasetMethodGenerator(this, dtDataSource);
			this.tableHandler.AddPrivateVars(codeTypeDeclaration);
			this.tableHandler.AddTableProperties(codeTypeDeclaration);
			this.relationHandler.AddPrivateVars(codeTypeDeclaration);
			datasetMethodGenerator.AddMethods(codeTypeDeclaration);
			this.rowHandler.AddTypedRowEventHandlers(codeTypeDeclaration);
			this.tableHandler.AddTableClasses(codeTypeDeclaration);
			this.rowHandler.AddTypedRows(codeTypeDeclaration);
			this.rowHandler.AddTypedRowEventArgs(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000896A0 File Offset: 0x000878A0
		internal static ArrayList GetProviderAssemblies(DesignDataSource designDS)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in designDS.DesignConnections)
			{
				IDesignConnection designConnection = (IDesignConnection)obj;
				IDbConnection dbConnection = designConnection.CreateEmptyDbConnection();
				if (dbConnection != null)
				{
					Assembly assembly = dbConnection.GetType().Assembly;
					if (!arrayList.Contains(assembly))
					{
						arrayList.Add(assembly);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00089728 File Offset: 0x00087928
		private string CreateAdaptersNamespace(string generatorDataSetName)
		{
			if (generatorDataSetName.StartsWith("[", StringComparison.Ordinal) && generatorDataSetName.EndsWith("]", StringComparison.Ordinal))
			{
				generatorDataSetName = generatorDataSetName.Substring(1, generatorDataSetName.Length - 2);
			}
			return MemberNameValidator.GenerateIdName(generatorDataSetName + "TableAdapters", this.CodeProvider, false);
		}

		// Token: 0x04000C8D RID: 3213
		private DesignDataSource designDataSource;

		// Token: 0x04000C8E RID: 3214
		private CodeDomProvider codeProvider;

		// Token: 0x04000C8F RID: 3215
		private ArrayList problemList = new ArrayList();

		// Token: 0x04000C90 RID: 3216
		private TypedTableHandler tableHandler;

		// Token: 0x04000C91 RID: 3217
		private RelationHandler relationHandler;

		// Token: 0x04000C92 RID: 3218
		private TypedRowHandler rowHandler;

		// Token: 0x04000C93 RID: 3219
		private bool generateExtendedProperties;

		// Token: 0x04000C94 RID: 3220
		private IDictionary userData;

		// Token: 0x04000C95 RID: 3221
		private bool generateSingleNamespace;

		// Token: 0x04000C96 RID: 3222
		private TypedDataSetGenerator.GenerateOption generateOption;

		// Token: 0x04000C97 RID: 3223
		private string dataSetNamespace;
	}
}
