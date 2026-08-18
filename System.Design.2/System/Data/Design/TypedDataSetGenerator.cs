using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Data.Design
{
	// Token: 0x02000271 RID: 625
	public sealed class TypedDataSetGenerator
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x000885FF File Offset: 0x000867FF
		public static ICollection<Assembly> ReferencedAssemblies
		{
			get
			{
				return TypedDataSetGenerator.referencedAssemblies;
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0000362F File Offset: 0x0000182F
		private TypedDataSetGenerator()
		{
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00088606 File Offset: 0x00086806
		public static string GetProviderName(string inputFileContent)
		{
			return TypedDataSetGenerator.GetProviderName(inputFileContent, null);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00088610 File Offset: 0x00086810
		public static string GetProviderName(string inputFileContent, string tableName)
		{
			if (inputFileContent == null || inputFileContent.Length == 0)
			{
				throw new ArgumentException(SR.GetString("CG_DataSetGeneratorFail_InputFileEmpty"));
			}
			StringReader textReader = new StringReader(inputFileContent);
			DesignDataSource designDataSource = new DesignDataSource();
			try
			{
				designDataSource.ReadXmlSchema(textReader, null);
			}
			catch (Exception ex)
			{
				string @string = SR.GetString("CG_DataSetGeneratorFail_UnableToConvertToDataSet", new object[]
				{
					TypedDataSetGenerator.CreateExceptionMessage(ex)
				});
				throw new Exception(@string, ex);
			}
			if (tableName == null || tableName.Length == 0)
			{
				if (designDataSource.DefaultConnection != null)
				{
					return designDataSource.DefaultConnection.Provider;
				}
			}
			else
			{
				DesignTable designTable = designDataSource.DesignTables[tableName];
				if (designTable != null)
				{
					return designTable.Connection.Provider;
				}
			}
			return null;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000886C4 File Offset: 0x000868C4
		public static string Generate(DataSet dataSet, CodeNamespace codeNamespace, CodeDomProvider codeProvider)
		{
			if (codeProvider == null)
			{
				throw new ArgumentNullException("codeProvider");
			}
			if (dataSet == null)
			{
				throw new ArgumentException(SR.GetString("CG_DataSetGeneratorFail_DatasetNull"));
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			dataSet.WriteXmlSchema(stringWriter);
			StringBuilder stringBuilder = stringWriter.GetStringBuilder();
			return TypedDataSetGenerator.Generate(stringBuilder.ToString(), null, codeNamespace, codeProvider);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0008871C File Offset: 0x0008691C
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, DbProviderFactory specifiedFactory)
		{
			if (specifiedFactory != null)
			{
				ProviderManager.ActiveFactoryContext = specifiedFactory;
			}
			try
			{
				TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider);
			}
			finally
			{
				ProviderManager.ActiveFactoryContext = null;
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00088758 File Offset: 0x00086958
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders)
		{
			TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, customDBProviders, TypedDataSetGenerator.GenerateOption.None);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00088766 File Offset: 0x00086966
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option)
		{
			TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, customDBProviders, option, null);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00088776 File Offset: 0x00086976
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace)
		{
			TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, customDBProviders, option, dataSetNamespace, null);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00088788 File Offset: 0x00086988
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace, string basePath)
		{
			if (customDBProviders != null)
			{
				ProviderManager.CustomDBProviders = customDBProviders;
			}
			try
			{
				TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, option, dataSetNamespace, basePath);
			}
			finally
			{
				ProviderManager.CustomDBProviders = null;
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000887CC File Offset: 0x000869CC
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider)
		{
			return TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, TypedDataSetGenerator.GenerateOption.None);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000887D8 File Offset: 0x000869D8
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option)
		{
			return TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, option, null);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000887E6 File Offset: 0x000869E6
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace)
		{
			return TypedDataSetGenerator.Generate(inputFileContent, compileUnit, mainNamespace, codeProvider, option, dataSetNamespace, null);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000887F8 File Offset: 0x000869F8
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option, string dataSetNamespace, string basePath)
		{
			if (inputFileContent == null || inputFileContent.Length == 0)
			{
				throw new ArgumentException(SR.GetString("CG_DataSetGeneratorFail_InputFileEmpty"));
			}
			if (mainNamespace == null)
			{
				throw new ArgumentException(SR.GetString("CG_DataSetGeneratorFail_CodeNamespaceNull"));
			}
			if (codeProvider == null)
			{
				throw new ArgumentNullException("codeProvider");
			}
			StringReader textReader = new StringReader(inputFileContent);
			DesignDataSource designDataSource = new DesignDataSource();
			try
			{
				designDataSource.ReadXmlSchema(textReader, basePath);
			}
			catch (Exception ex)
			{
				string @string = SR.GetString("CG_DataSetGeneratorFail_UnableToConvertToDataSet", new object[]
				{
					TypedDataSetGenerator.CreateExceptionMessage(ex)
				});
				throw new Exception(@string, ex);
			}
			return TypedDataSetGenerator.GenerateInternal(designDataSource, compileUnit, mainNamespace, codeProvider, option, dataSetNamespace);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0008889C File Offset: 0x00086A9C
		internal static string GenerateInternal(DesignDataSource designDS, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption generateOption, string dataSetNamespace)
		{
			if (StringUtil.Empty(designDS.Name))
			{
				designDS.Name = "DataSet1";
			}
			try
			{
				TypedDataSourceCodeGenerator typedDataSourceCodeGenerator = new TypedDataSourceCodeGenerator();
				typedDataSourceCodeGenerator.CodeProvider = codeProvider;
				typedDataSourceCodeGenerator.GenerateSingleNamespace = false;
				if (mainNamespace == null)
				{
					mainNamespace = new CodeNamespace();
				}
				if (compileUnit == null)
				{
					compileUnit = new CodeCompileUnit();
					compileUnit.Namespaces.Add(mainNamespace);
				}
				typedDataSourceCodeGenerator.GenerateDataSource(designDS, compileUnit, mainNamespace, dataSetNamespace, generateOption);
				foreach (string nameSpace in TypedDataSetGenerator.imports)
				{
					mainNamespace.Imports.Add(new CodeNamespaceImport(nameSpace));
				}
			}
			catch (Exception ex)
			{
				string @string = SR.GetString("CG_DataSetGeneratorFail_FailToGenerateCode", new object[]
				{
					TypedDataSetGenerator.CreateExceptionMessage(ex)
				});
				throw new Exception(@string, ex);
			}
			ArrayList arrayList = new ArrayList(TypedDataSetGenerator.fixedReferences);
			arrayList.AddRange(TypedDataSourceCodeGenerator.GetProviderAssemblies(designDS));
			if ((generateOption & TypedDataSetGenerator.GenerateOption.LinqOverTypedDatasets) == TypedDataSetGenerator.GenerateOption.LinqOverTypedDatasets)
			{
				Assembly assembly = TypedDataSetGenerator.EntityAssembly;
				if (assembly != null)
				{
					arrayList.Add(assembly);
				}
			}
			TypedDataSetGenerator.referencedAssemblies = (Assembly[])arrayList.ToArray(typeof(Assembly));
			foreach (Assembly assembly2 in TypedDataSetGenerator.referencedAssemblies)
			{
				compileUnit.ReferencedAssemblies.Add(assembly2.GetName().Name + ".dll");
			}
			return designDS.GeneratorDataSetName;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00088A04 File Offset: 0x00086C04
		private static Assembly EntityAssembly
		{
			get
			{
				if (TypedDataSetGenerator.entityAssembly == null)
				{
					try
					{
						TypedDataSetGenerator.entityAssembly = Assembly.Load(TypedDataSetGenerator.LINQOverTDSAssemblyName);
					}
					catch
					{
					}
				}
				return TypedDataSetGenerator.entityAssembly;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00088A48 File Offset: 0x00086C48
		private static string CreateExceptionMessage(Exception e)
		{
			string text = (e.Message != null) ? e.Message : string.Empty;
			for (Exception innerException = e.InnerException; innerException != null; innerException = innerException.InnerException)
			{
				string message = innerException.Message;
				if (message != null && message.Length > 0)
				{
					text = text + " " + message;
				}
			}
			return text;
		}

		// Token: 0x04000C6E RID: 3182
		private static Assembly systemAssembly = Assembly.GetAssembly(typeof(Uri));

		// Token: 0x04000C6F RID: 3183
		private static Assembly dataAssembly = Assembly.GetAssembly(typeof(SqlDataAdapter));

		// Token: 0x04000C70 RID: 3184
		private static Assembly xmlAssembly = Assembly.GetAssembly(typeof(XmlSchemaType));

		// Token: 0x04000C71 RID: 3185
		private static Assembly[] fixedReferences = new Assembly[]
		{
			TypedDataSetGenerator.systemAssembly,
			TypedDataSetGenerator.dataAssembly,
			TypedDataSetGenerator.xmlAssembly
		};

		// Token: 0x04000C72 RID: 3186
		private static Assembly[] referencedAssemblies = null;

		// Token: 0x04000C73 RID: 3187
		private static Assembly entityAssembly;

		// Token: 0x04000C74 RID: 3188
		private static string LINQOverTDSAssemblyName = "System.Data.DataSetExtensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C75 RID: 3189
		private static string[] imports = new string[0];

		// Token: 0x020004C2 RID: 1218
		[Flags]
		public enum GenerateOption
		{
			// Token: 0x04001EBA RID: 7866
			None = 0,
			// Token: 0x04001EBB RID: 7867
			HierarchicalUpdate = 1,
			// Token: 0x04001EBC RID: 7868
			LinqOverTypedDatasets = 2
		}
	}
}
