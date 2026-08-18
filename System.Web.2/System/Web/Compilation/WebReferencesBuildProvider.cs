using System;
using System.CodeDom;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Hosting;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Web.UI;
using System.Web.Util;
using System.Xml.Serialization;

namespace System.Web.Compilation
{
	// Token: 0x0200086A RID: 2154
	internal class WebReferencesBuildProvider : BuildProvider
	{
		// Token: 0x06006599 RID: 26009 RVA: 0x00165D09 File Offset: 0x00163F09
		internal WebReferencesBuildProvider(VirtualDirectory vdir)
		{
			this._vdir = vdir;
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x00165D18 File Offset: 0x00163F18
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			if (!WebReferencesBuildProvider.s_triedToGetWebRefType)
			{
				WebReferencesBuildProvider.s_indigoWebRefProviderType = BuildManager.GetType("System.Web.Compilation.WCFBuildProvider", false);
				WebReferencesBuildProvider.s_triedToGetWebRefType = true;
			}
			if (WebReferencesBuildProvider.s_indigoWebRefProviderType != null)
			{
				BuildProvider buildProvider = (BuildProvider)HttpRuntime.CreateNonPublicInstance(WebReferencesBuildProvider.s_indigoWebRefProviderType);
				buildProvider.SetVirtualPath(base.VirtualPathObject);
				buildProvider.GenerateCode(assemblyBuilder);
			}
			VirtualPath webRefDirectoryVirtualPath = HttpRuntime.WebRefDirectoryVirtualPath;
			string text = this._vdir.VirtualPath;
			string text2;
			if (webRefDirectoryVirtualPath.VirtualPathString.Length == text.Length)
			{
				text2 = string.Empty;
			}
			else
			{
				text = UrlPath.RemoveSlashFromPathIfNeeded(text);
				text = text.Substring(webRefDirectoryVirtualPath.VirtualPathString.Length);
				string[] array = text.Split(new char[]
				{
					'/'
				});
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Util.MakeValidTypeNameFromString(array[i]);
				}
				text2 = string.Join(".", array);
			}
			CodeNamespace codeNamespace = new CodeNamespace(text2);
			WebReferenceCollection webReferenceCollection = new WebReferenceCollection();
			bool flag = false;
			foreach (object obj in this._vdir.Files)
			{
				VirtualFile virtualFile = (VirtualFile)obj;
				string text3 = UrlPath.GetExtension(virtualFile.VirtualPath);
				text3 = text3.ToLower(CultureInfo.InvariantCulture);
				if (text3 == ".discomap")
				{
					string topLevelFilename = HostingEnvironment.MapPath(virtualFile.VirtualPath);
					DiscoveryClientProtocol discoveryClientProtocol = new DiscoveryClientProtocol();
					discoveryClientProtocol.AllowAutoRedirect = true;
					discoveryClientProtocol.Credentials = CredentialCache.DefaultCredentials;
					discoveryClientProtocol.ReadAll(topLevelFilename);
					WebReference webReference = new WebReference(discoveryClientProtocol.Documents, codeNamespace);
					string str = Path.ChangeExtension(UrlPath.GetFileName(virtualFile.VirtualPath), null);
					string appSettingUrlKey = text2 + "." + str;
					WebReference webReference2 = new WebReference(discoveryClientProtocol.Documents, codeNamespace, webReference.ProtocolName, appSettingUrlKey, null);
					webReferenceCollection.Add(webReference2);
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			WebReferenceOptions webReferenceOptions = new WebReferenceOptions();
			webReferenceOptions.CodeGenerationOptions = (CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync | CodeGenerationOptions.GenerateOldAsync);
			webReferenceOptions.Style = ServiceDescriptionImportStyle.Client;
			webReferenceOptions.Verbose = true;
			StringCollection stringCollection = ServiceDescriptionImporter.GenerateWebReferences(webReferenceCollection, assemblyBuilder.CodeDomProvider, codeCompileUnit, webReferenceOptions);
			assemblyBuilder.AddCodeCompileUnit(this, codeCompileUnit);
		}

		// Token: 0x04003440 RID: 13376
		private VirtualDirectory _vdir;

		// Token: 0x04003441 RID: 13377
		private const string IndigoWebRefProviderTypeName = "System.Web.Compilation.WCFBuildProvider";

		// Token: 0x04003442 RID: 13378
		private static Type s_indigoWebRefProviderType;

		// Token: 0x04003443 RID: 13379
		private static bool s_triedToGetWebRefType;
	}
}
