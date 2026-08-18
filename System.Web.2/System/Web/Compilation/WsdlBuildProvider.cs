using System;
using System.CodeDom;
using System.IO;
using System.Web.Services.Description;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation
{
	// Token: 0x0200086B RID: 2155
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
	internal class WsdlBuildProvider : BuildProvider
	{
		// Token: 0x0600659B RID: 26011 RVA: 0x00165F74 File Offset: 0x00164174
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			string namespaceFromVirtualPath = Util.GetNamespaceFromVirtualPath(base.VirtualPathObject);
			ServiceDescription serviceDescription;
			using (Stream stream = base.VirtualPathObject.OpenFile())
			{
				try
				{
					serviceDescription = ServiceDescription.Read(stream);
				}
				catch (InvalidOperationException ex)
				{
					XmlException ex2 = ex.InnerException as XmlException;
					if (ex2 != null)
					{
						throw ex2;
					}
					throw;
				}
			}
			ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter();
			serviceDescriptionImporter.CodeGenerator = assemblyBuilder.CodeDomProvider;
			serviceDescriptionImporter.CodeGenerationOptions = (CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync | CodeGenerationOptions.GenerateOldAsync);
			serviceDescriptionImporter.ServiceDescriptions.Add(serviceDescription);
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(namespaceFromVirtualPath);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			serviceDescriptionImporter.Import(codeNamespace, codeCompileUnit);
			assemblyBuilder.AddCodeCompileUnit(this, codeCompileUnit);
		}
	}
}
