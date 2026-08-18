using System;
using System.CodeDom;
using System.Collections;
using System.Data.Design;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x0200086C RID: 2156
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
	internal class XsdBuildProvider : BuildProvider
	{
		// Token: 0x0600659D RID: 26013 RVA: 0x0016603C File Offset: 0x0016423C
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			string namespaceFromVirtualPath = Util.GetNamespaceFromVirtualPath(base.VirtualPathObject);
			XmlDocument xmlDocument = new XmlDocument();
			using (Stream stream = base.OpenStream())
			{
				xmlDocument.Load(stream);
			}
			string outerXml = xmlDocument.OuterXml;
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(namespaceFromVirtualPath);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			bool flag = CompilationUtil.IsCompilerVersion35OrAbove(assemblyBuilder.CodeDomProvider.GetType());
			if (flag)
			{
				TypedDataSetGenerator.GenerateOption generateOption = TypedDataSetGenerator.GenerateOption.None;
				generateOption |= TypedDataSetGenerator.GenerateOption.HierarchicalUpdate;
				generateOption |= TypedDataSetGenerator.GenerateOption.LinqOverTypedDatasets;
				Hashtable customDBProviders = null;
				TypedDataSetGenerator.Generate(outerXml, codeCompileUnit, codeNamespace, assemblyBuilder.CodeDomProvider, customDBProviders, generateOption);
			}
			else
			{
				TypedDataSetGenerator.Generate(outerXml, codeCompileUnit, codeNamespace, assemblyBuilder.CodeDomProvider);
			}
			if (TypedDataSetGenerator.ReferencedAssemblies != null)
			{
				bool flag2 = CompilationUtil.IsCompilerVersion35(assemblyBuilder.CodeDomProvider.GetType());
				foreach (Assembly assembly in TypedDataSetGenerator.ReferencedAssemblies)
				{
					if (flag2)
					{
						AssemblyName name = assembly.GetName();
						if (name.Name == "System.Data.DataSetExtensions")
						{
							name.Version = new Version(3, 5, 0, 0);
							CompilationSection.RecordAssembly(name.FullName, assembly);
						}
					}
					assemblyBuilder.AddAssemblyReference(assembly);
				}
			}
			assemblyBuilder.AddCodeCompileUnit(this, codeCompileUnit);
		}
	}
}
