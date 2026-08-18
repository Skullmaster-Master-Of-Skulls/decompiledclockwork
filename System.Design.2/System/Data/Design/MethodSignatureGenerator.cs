using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;

namespace System.Data.Design
{
	// Token: 0x02000250 RID: 592
	public class MethodSignatureGenerator
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0007D1F6 File Offset: 0x0007B3F6
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0007D1FE File Offset: 0x0007B3FE
		public CodeDomProvider CodeProvider
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

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0007D207 File Offset: 0x0007B407
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0007D20F File Offset: 0x0007B40F
		public Type ContainerParameterType
		{
			get
			{
				return this.containerParameterType;
			}
			set
			{
				if (value != typeof(DataSet) && value != typeof(DataTable))
				{
					throw new InternalException("Unsupported container parameter type.");
				}
				this.containerParameterType = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0007D247 File Offset: 0x0007B447
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0007D24F File Offset: 0x0007B44F
		public bool IsGetMethod
		{
			get
			{
				return this.getMethod;
			}
			set
			{
				this.getMethod = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0007D258 File Offset: 0x0007B458
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0007D260 File Offset: 0x0007B460
		public bool PagingMethod
		{
			get
			{
				return this.pagingMethod;
			}
			set
			{
				this.pagingMethod = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0007D269 File Offset: 0x0007B469
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0007D271 File Offset: 0x0007B471
		public ParameterGenerationOption ParameterOption
		{
			get
			{
				return this.parameterOption;
			}
			set
			{
				this.parameterOption = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0007D27A File Offset: 0x0007B47A
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0007D282 File Offset: 0x0007B482
		public string TableClassName
		{
			get
			{
				return this.tableClassName;
			}
			set
			{
				this.tableClassName = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0007D28B File Offset: 0x0007B48B
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0007D293 File Offset: 0x0007B493
		public string DataSetClassName
		{
			get
			{
				return this.datasetClassName;
			}
			set
			{
				this.datasetClassName = value;
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0007D29C File Offset: 0x0007B49C
		public void SetDesignTableContent(string designTableContent)
		{
			DesignDataSource designDataSource = new DesignDataSource();
			StringReader textReader = new StringReader(designTableContent);
			designDataSource.ReadXmlSchema(textReader, null);
			if (designDataSource.DesignTables == null || designDataSource.DesignTables.Count != 1)
			{
				throw new InternalException("Unexpected number of sources in deserialized DataSource.");
			}
			IEnumerator enumerator = designDataSource.DesignTables.GetEnumerator();
			enumerator.MoveNext();
			this.designTable = (DesignTable)enumerator.Current;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0007D304 File Offset: 0x0007B504
		public void SetMethodSourceContent(string methodSourceContent)
		{
			DesignDataSource designDataSource = new DesignDataSource();
			StringReader textReader = new StringReader(methodSourceContent);
			designDataSource.ReadXmlSchema(textReader, null);
			if (designDataSource.Sources == null || designDataSource.Sources.Count != 1)
			{
				throw new InternalException("Unexpected number of sources in deserialized DataSource.");
			}
			IEnumerator enumerator = designDataSource.Sources.GetEnumerator();
			enumerator.MoveNext();
			this.methodSource = (DbSource)enumerator.Current;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0007D36C File Offset: 0x0007B56C
		public string GenerateMethodSignature()
		{
			if (this.codeProvider == null)
			{
				throw new ArgumentException("codeProvider");
			}
			if (this.methodSource == null)
			{
				throw new ArgumentException("MethodSource");
			}
			string value = null;
			CodeTypeDeclaration codeType = this.GenerateMethodWrapper(out value);
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			this.codeProvider.GenerateCodeFromType(codeType, stringWriter, null);
			string text = stringWriter.GetStringBuilder().ToString();
			string[] array = text.Split(Environment.NewLine.ToCharArray());
			foreach (string text2 in array)
			{
				if (text2.Contains(value))
				{
					return text2.Trim().TrimEnd(new char[]
					{
						MethodSignatureGenerator.endOfStatement
					});
				}
			}
			return null;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0007D428 File Offset: 0x0007B628
		private CodeTypeDeclaration GenerateMethodWrapper(out string methodName)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration("Wrapper");
			codeTypeDeclaration.IsInterface = true;
			CodeMemberMethod codeMemberMethod = this.GenerateMethod();
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			methodName = codeMemberMethod.Name;
			return codeTypeDeclaration;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0007D464 File Offset: 0x0007B664
		public CodeMemberMethod GenerateMethod()
		{
			if (this.codeProvider == null)
			{
				throw new ArgumentException("codeProvider");
			}
			if (this.methodSource == null)
			{
				throw new ArgumentException("MethodSource");
			}
			QueryGeneratorBase queryGeneratorBase;
			if (this.methodSource.QueryType == QueryType.Rowset && this.methodSource.CommandOperation == CommandOperation.Select)
			{
				queryGeneratorBase = new QueryGenerator(null);
				queryGeneratorBase.ContainerParameterTypeName = this.GetParameterTypeName();
				queryGeneratorBase.ContainerParameterName = this.GetParameterName();
				queryGeneratorBase.ContainerParameterType = this.containerParameterType;
			}
			else
			{
				queryGeneratorBase = new FunctionGenerator(null);
			}
			queryGeneratorBase.DeclarationOnly = true;
			queryGeneratorBase.CodeProvider = this.codeProvider;
			queryGeneratorBase.MethodSource = this.methodSource;
			queryGeneratorBase.MethodName = this.GetMethodName();
			queryGeneratorBase.ParameterOption = this.parameterOption;
			queryGeneratorBase.GeneratePagingMethod = this.pagingMethod;
			queryGeneratorBase.GenerateGetMethod = this.getMethod;
			return queryGeneratorBase.Generate();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0007D540 File Offset: 0x0007B740
		public CodeTypeDeclaration GenerateUpdatingMethods()
		{
			if (this.designTable == null)
			{
				throw new InternalException("DesignTable should not be null.");
			}
			if (StringUtil.Empty(this.datasetClassName))
			{
				throw new InternalException("DatasetClassName should not be empty.");
			}
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration("wrapper");
			codeTypeDeclaration.IsInterface = true;
			new QueryHandler(this.codeProvider, this.designTable)
			{
				DeclarationsOnly = true
			}.AddUpdateQueriesToDataComponent(codeTypeDeclaration, this.datasetClassName, this.codeProvider);
			return codeTypeDeclaration;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0007D5B7 File Offset: 0x0007B7B7
		private string GetParameterName()
		{
			if (this.containerParameterType == typeof(DataTable))
			{
				return "dataTable";
			}
			return "dataSet";
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0007D5DC File Offset: 0x0007B7DC
		private string GetParameterTypeName()
		{
			if (StringUtil.Empty(this.datasetClassName))
			{
				throw new InternalException("DatasetClassName should not be empty.");
			}
			if (!(this.containerParameterType == typeof(DataTable)))
			{
				return this.datasetClassName;
			}
			if (StringUtil.Empty(this.tableClassName))
			{
				throw new InternalException("TableClassName should not be empty.");
			}
			return CodeGenHelper.GetTypeName(this.codeProvider, this.datasetClassName, this.tableClassName);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0007D650 File Offset: 0x0007B850
		private string GetMethodName()
		{
			if (this.methodSource.QueryType == QueryType.Rowset)
			{
				if (this.getMethod)
				{
					if (this.pagingMethod)
					{
						if (this.methodSource.GeneratorGetMethodNameForPaging != null)
						{
							return this.methodSource.GeneratorGetMethodNameForPaging;
						}
						return this.methodSource.GetMethodName + DataComponentNameHandler.PagingMethodSuffix;
					}
					else
					{
						if (this.methodSource.GeneratorGetMethodName != null)
						{
							return this.methodSource.GeneratorGetMethodName;
						}
						return this.methodSource.GetMethodName;
					}
				}
				else if (this.pagingMethod)
				{
					if (this.methodSource.GeneratorSourceNameForPaging != null)
					{
						return this.methodSource.GeneratorSourceNameForPaging;
					}
					return this.methodSource.Name + DataComponentNameHandler.PagingMethodSuffix;
				}
				else
				{
					if (this.methodSource.GeneratorSourceName != null)
					{
						return this.methodSource.GeneratorSourceName;
					}
					return this.methodSource.Name;
				}
			}
			else
			{
				if (this.methodSource.GeneratorSourceName != null)
				{
					return this.methodSource.GeneratorSourceName;
				}
				return this.methodSource.Name;
			}
		}

		// Token: 0x04000BA1 RID: 2977
		private static readonly char endOfStatement = ';';

		// Token: 0x04000BA2 RID: 2978
		private CodeDomProvider codeProvider;

		// Token: 0x04000BA3 RID: 2979
		private DbSource methodSource;

		// Token: 0x04000BA4 RID: 2980
		private Type containerParameterType = typeof(DataSet);

		// Token: 0x04000BA5 RID: 2981
		private bool pagingMethod;

		// Token: 0x04000BA6 RID: 2982
		private bool getMethod;

		// Token: 0x04000BA7 RID: 2983
		private ParameterGenerationOption parameterOption;

		// Token: 0x04000BA8 RID: 2984
		private string tableClassName;

		// Token: 0x04000BA9 RID: 2985
		private string datasetClassName;

		// Token: 0x04000BAA RID: 2986
		private DesignTable designTable;
	}
}
