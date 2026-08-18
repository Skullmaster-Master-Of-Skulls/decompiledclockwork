using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Design;
using System.Data.Services.Client;
using System.Data.Services.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web.Compilation.WCFModel;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Resources;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x0200000B RID: 11
	[SecurityCritical]
	public class WCFBuildProvider : BuildProvider
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002B3C File Offset: 0x00000D3C
		[SecuritySafeCritical]
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			VirtualDirectory virtualDirectory = this.GetVirtualDirectory(base.VirtualPath);
			foreach (object obj in virtualDirectory.Files)
			{
				VirtualFile virtualFile = (VirtualFile)obj;
				string extension = Path.GetExtension(virtualFile.VirtualPath);
				if (extension.Equals(".svcmap", StringComparison.OrdinalIgnoreCase))
				{
					string mapFilePath = HostingEnvironment.MapPath(virtualFile.VirtualPath);
					CodeCompileUnit compileUnit = this.GenerateCodeFromServiceMapFile(mapFilePath);
					assemblyBuilder.AddCodeCompileUnit(this, compileUnit);
				}
				else if (extension.Equals(".datasvcmap", StringComparison.OrdinalIgnoreCase) && BuildManager.TargetFramework.Version.Major < 4)
				{
					string mapFilePath2 = HostingEnvironment.MapPath(virtualFile.VirtualPath);
					this.GenerateCodeFromDataServiceMapFile(mapFilePath2, assemblyBuilder);
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C14 File Offset: 0x00000E14
		private void GenerateCodeFromDataServiceMapFile(string mapFilePath, AssemblyBuilder assemblyBuilder)
		{
			try
			{
				assemblyBuilder.AddAssemblyReference(typeof(DataServiceContext).Assembly);
				DataSvcMapFileLoader dataSvcMapFileLoader = new DataSvcMapFileLoader(mapFilePath);
				DataSvcMapFile dataSvcMapFile = dataSvcMapFileLoader.LoadMapFile() as DataSvcMapFile;
				if (dataSvcMapFile.MetadataList[0].ErrorInLoading != null)
				{
					throw dataSvcMapFile.MetadataList[0].ErrorInLoading;
				}
				string content = dataSvcMapFile.MetadataList[0].Content;
				EntityClassGenerator entityClassGenerator = new EntityClassGenerator(LanguageOption.GenerateCSharpCode);
				using (TextWriter textWriter = assemblyBuilder.CreateCodeFile(this))
				{
					entityClassGenerator.GenerateCode(XmlReader.Create(new StringReader(content)), textWriter, this.GetGeneratedNamespace());
					textWriter.Flush();
				}
			}
			catch (Exception ex)
			{
				string text = ex.Message;
				text = string.Format(CultureInfo.CurrentCulture, "{0}: {1}", new object[]
				{
					Path.GetFileName(mapFilePath),
					text
				});
				throw new InvalidOperationException(text, ex);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D18 File Offset: 0x00000F18
		private CodeCompileUnit GenerateCodeFromServiceMapFile(string mapFilePath)
		{
			CodeCompileUnit targetCompileUnit;
			try
			{
				string generatedNamespace = this.GetGeneratedNamespace();
				SvcMapFileLoader svcMapFileLoader = new SvcMapFileLoader(mapFilePath);
				SvcMapFile svcMapFile = svcMapFileLoader.LoadMapFile() as SvcMapFile;
				WCFBuildProvider.HandleProxyGenerationErrors(svcMapFile.LoadErrors);
				CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("c#");
				VSWCFServiceContractGenerator vswcfserviceContractGenerator = VSWCFServiceContractGenerator.GenerateCodeAndConfiguration(svcMapFile, this.GetToolConfig(svcMapFile, mapFilePath), codeDomProvider, generatedNamespace, null, null, new WCFBuildProvider.ImportExtensionServiceProvider(), new WCFBuildProvider.TypeResolver(), 196613, typeof(TypedDataSetSchemaImporterExtensionFx35));
				string referenceDisplayName = string.IsNullOrEmpty(generatedNamespace) ? Path.GetFileName(mapFilePath) : generatedNamespace;
				WCFBuildProvider.VerifyGeneratedCodeAndHandleErrors(referenceDisplayName, svcMapFile, vswcfserviceContractGenerator.TargetCompileUnit, vswcfserviceContractGenerator.ImportErrors, vswcfserviceContractGenerator.ProxyGenerationErrors);
				targetCompileUnit = vswcfserviceContractGenerator.TargetCompileUnit;
			}
			catch (Exception ex)
			{
				string text = ex.Message;
				text = string.Format(CultureInfo.CurrentCulture, "{0}: {1}", new object[]
				{
					Path.GetFileName(mapFilePath),
					text
				});
				throw new InvalidOperationException(text, ex);
			}
			return targetCompileUnit;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E0C File Offset: 0x0000100C
		private static void HandleProxyGenerationErrors(IEnumerable errors)
		{
			foreach (object obj in errors)
			{
				ProxyGenerationError proxyGenerationError = (ProxyGenerationError)obj;
				if (!proxyGenerationError.IsWarning && proxyGenerationError.ErrorGeneratorState != ProxyGenerationError.GeneratorState.GenerateCode)
				{
					throw new InvalidOperationException(WCFBuildProvider.ConvertToBuildProviderErrorMessage(proxyGenerationError));
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E78 File Offset: 0x00001078
		private static void CollectErrorMessages(IEnumerable errors, StringBuilder collectedMessages)
		{
			foreach (object obj in errors)
			{
				ProxyGenerationError proxyGenerationError = (ProxyGenerationError)obj;
				if (!proxyGenerationError.IsWarning)
				{
					if (collectedMessages.Length > 0)
					{
						collectedMessages.Append(Environment.NewLine);
					}
					collectedMessages.Append(WCFBuildProvider.ConvertToBuildProviderErrorMessage(proxyGenerationError));
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EF0 File Offset: 0x000010F0
		private static string ConvertToBuildProviderErrorMessage(ProxyGenerationError generationError)
		{
			string text = generationError.Message;
			if (!string.IsNullOrEmpty(generationError.MetadataFile))
			{
				if (generationError.LineNumber < 0)
				{
					text = string.Format(CultureInfo.CurrentCulture, "'{0}': {1}", new object[]
					{
						generationError.MetadataFile,
						text
					});
				}
				else if (generationError.LinePosition < 0)
				{
					text = string.Format(CultureInfo.CurrentCulture, "'{0}' ({1}): {2}", new object[]
					{
						generationError.MetadataFile,
						generationError.LineNumber,
						text
					});
				}
				else
				{
					text = string.Format(CultureInfo.CurrentCulture, "'{0}' ({1},{2}): {3}", new object[]
					{
						generationError.MetadataFile,
						generationError.LineNumber,
						generationError.LinePosition,
						text
					});
				}
			}
			return text;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FC0 File Offset: 0x000011C0
		private static void VerifyGeneratedCodeAndHandleErrors(string referenceDisplayName, SvcMapFile mapFile, CodeCompileUnit generatedCode, IEnumerable importErrors, IEnumerable generatorErrors)
		{
			WCFBuildProvider.HandleProxyGenerationErrors(importErrors);
			WCFBuildProvider.HandleProxyGenerationErrors(generatorErrors);
			if (mapFile.MetadataList.Count > 0 && mapFile.ClientOptions.ServiceContractMappingList.Count == 0 && !WCFBuildProvider.IsAnyTypeGenerated(generatedCode))
			{
				StringBuilder stringBuilder = new StringBuilder();
				WCFBuildProvider.CollectErrorMessages(importErrors, stringBuilder);
				WCFBuildProvider.CollectErrorMessages(generatorErrors, stringBuilder);
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_FailedToGenerateCode, new object[]
				{
					referenceDisplayName,
					stringBuilder.ToString()
				}));
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003040 File Offset: 0x00001240
		private static bool IsAnyTypeGenerated(CodeCompileUnit compileUnit)
		{
			if (compileUnit != null)
			{
				foreach (object obj in compileUnit.Namespaces)
				{
					CodeNamespace codeNamespace = (CodeNamespace)obj;
					if (codeNamespace.Types.Count > 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030AC File Offset: 0x000012AC
		private VirtualDirectory GetVirtualDirectory(string virtualPath)
		{
			return HostingEnvironment.VirtualPathProvider.GetDirectory(base.VirtualPath);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030C0 File Offset: 0x000012C0
		private string GetGeneratedNamespace()
		{
			string webRefDirectoryVirtualPath = WCFBuildProvider.GetWebRefDirectoryVirtualPath();
			string virtualPath = base.VirtualPath;
			if (virtualPath == null)
			{
				throw new InvalidOperationException();
			}
			return WCFBuildProvider.CalculateGeneratedNamespace(webRefDirectoryVirtualPath, virtualPath);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030EC File Offset: 0x000012EC
		private static string CalculateGeneratedNamespace(string webReferencesRootVirtualPath, string virtualPath)
		{
			webReferencesRootVirtualPath = VirtualPathUtility.AppendTrailingSlash(webReferencesRootVirtualPath);
			virtualPath = VirtualPathUtility.AppendTrailingSlash(virtualPath);
			if (webReferencesRootVirtualPath.Length == virtualPath.Length)
			{
				return string.Empty;
			}
			virtualPath = VirtualPathUtility.RemoveTrailingSlash(virtualPath).Substring(webReferencesRootVirtualPath.Length);
			string[] array = virtualPath.Split(new char[]
			{
				'/'
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = WCFBuildProvider.MakeValidTypeNameFromString(array[i]);
			}
			return string.Join(".", array);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003168 File Offset: 0x00001368
		private static string GetAppDomainAppVirtualPath()
		{
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (appDomainAppVirtualPath == null)
			{
				throw new InvalidOperationException();
			}
			return VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(appDomainAppVirtualPath));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000318F File Offset: 0x0000138F
		private static string GetWebRefDirectoryVirtualPath()
		{
			return VirtualPathUtility.Combine(WCFBuildProvider.GetAppDomainAppVirtualPath(), "App_WebReferences\\");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031A0 File Offset: 0x000013A0
		internal static string MakeValidTypeNameFromString(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < typeName.Length; i++)
			{
				if (i == 0 && char.IsDigit(typeName[0]))
				{
					stringBuilder.Append('_');
				}
				if (char.IsLetterOrDigit(typeName[i]))
				{
					stringBuilder.Append(typeName[i]);
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			string text = stringBuilder.ToString();
			if (text.Equals("_", StringComparison.Ordinal))
			{
				text = "__";
			}
			return text;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003234 File Offset: 0x00001434
		private Configuration GetToolConfig(SvcMapFile mapFile, string mapFilePath)
		{
			string text = null;
			if (mapFile != null && mapFilePath != null)
			{
				foreach (ExtensionFile extensionFile in mapFile.Extensions)
				{
					if (string.Equals(extensionFile.Name, "Reference.config", StringComparison.Ordinal))
					{
						text = extensionFile.FileName;
					}
				}
			}
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			VirtualDirectoryMapping mapping;
			if (text != null)
			{
				mapping = new VirtualDirectoryMapping(Path.GetDirectoryName(mapFilePath), true, text);
			}
			else
			{
				mapping = new VirtualDirectoryMapping(HostingEnvironment.ApplicationPhysicalPath, true);
			}
			webConfigurationFileMap.VirtualDirectories.Add("/", mapping);
			return WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, "/", HostingEnvironment.SiteName);
		}

		// Token: 0x0400001A RID: 26
		internal const string WebRefDirectoryName = "App_WebReferences";

		// Token: 0x0400001B RID: 27
		internal const string SvcMapExtension = ".svcmap";

		// Token: 0x0400001C RID: 28
		internal const string DataSvcMapExtension = ".datasvcmap";

		// Token: 0x0400001D RID: 29
		private const string TOOL_CONFIG_ITEM_NAME = "Reference.config";

		// Token: 0x0400001E RID: 30
		private const int FRAMEWORK_VERSION_35 = 196613;

		// Token: 0x02000127 RID: 295
		private class TypeResolver : IContractGeneratorReferenceTypeLoader
		{
			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00036F44 File Offset: 0x00035144
			private IEnumerable<Assembly> ReferencedAssemblies
			{
				get
				{
					if (this._referencedAssemblies == null)
					{
						ICollection referencedAssemblies = BuildManager.GetReferencedAssemblies();
						this._referencedAssemblies = new Assembly[referencedAssemblies.Count];
						referencedAssemblies.CopyTo(this._referencedAssemblies, 0);
					}
					return this._referencedAssemblies;
				}
			}

			// Token: 0x06000F40 RID: 3904 RVA: 0x00036F83 File Offset: 0x00035183
			[SecuritySafeCritical]
			Type IContractGeneratorReferenceTypeLoader.LoadType(string typeName)
			{
				return BuildManager.GetType(typeName, true);
			}

			// Token: 0x06000F41 RID: 3905 RVA: 0x00036F8C File Offset: 0x0003518C
			[SecuritySafeCritical]
			Assembly IContractGeneratorReferenceTypeLoader.LoadAssembly(string assemblyName)
			{
				AssemblyName reference = new AssemblyName(assemblyName);
				foreach (Assembly assembly in this.ReferencedAssemblies)
				{
					if (AssemblyName.ReferenceMatchesDefinition(reference, assembly.GetName()))
					{
						return assembly;
					}
				}
				throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_FailedToLoadAssembly, new object[]
				{
					assemblyName
				}));
			}

			// Token: 0x06000F42 RID: 3906 RVA: 0x0003700C File Offset: 0x0003520C
			[SecuritySafeCritical]
			void IContractGeneratorReferenceTypeLoader.LoadAllAssemblies(out IEnumerable<Assembly> loadedAssemblies, out IEnumerable<Exception> loadingErrors)
			{
				loadedAssemblies = this.ReferencedAssemblies;
				loadingErrors = new Exception[0];
			}

			// Token: 0x04000453 RID: 1107
			private Assembly[] _referencedAssemblies;
		}

		// Token: 0x02000128 RID: 296
		private class ImportExtensionServiceProvider : IServiceProvider
		{
			// Token: 0x06000F44 RID: 3908 RVA: 0x0001B314 File Offset: 0x00019514
			[SecuritySafeCritical]
			public object GetService(Type serviceType)
			{
				return null;
			}
		}
	}
}
