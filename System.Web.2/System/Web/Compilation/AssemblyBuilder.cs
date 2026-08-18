using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.UI;
using System.Web.Util;
using System.Xml;
using System.Xml.Schema;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace System.Web.Compilation
{
	// Token: 0x020007F5 RID: 2037
	public class AssemblyBuilder
	{
		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06006102 RID: 24834 RVA: 0x0014E99D File Offset: 0x0014CB9D
		internal ICollection BuildProviders
		{
			get
			{
				return this._buildProviders.Values;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06006103 RID: 24835 RVA: 0x0014E9AA File Offset: 0x0014CBAA
		internal Type CodeDomProviderType
		{
			get
			{
				return this._compilerType.CodeDomProviderType;
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x0014E9B7 File Offset: 0x0014CBB7
		internal StringResourceBuilder StringResourceBuilder
		{
			get
			{
				if (this._stringResourceBuilder == null)
				{
					this._stringResourceBuilder = new StringResourceBuilder();
				}
				return this._stringResourceBuilder;
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x06006105 RID: 24837 RVA: 0x0014E9D2 File Offset: 0x0014CBD2
		// (set) Token: 0x06006106 RID: 24838 RVA: 0x0014E9DA File Offset: 0x0014CBDA
		internal string CultureName
		{
			get
			{
				return this._cultureName;
			}
			set
			{
				this._cultureName = value;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06006107 RID: 24839 RVA: 0x0014E9E4 File Offset: 0x0014CBE4
		private string OutputAssemblyName
		{
			get
			{
				if (this._outputAssemblyName == null)
				{
					string basePath = this._tempFiles.BasePath;
					string fileName = Path.GetFileName(basePath);
					this._outputAssemblyName = "App_Web_" + fileName;
				}
				return this._outputAssemblyName;
			}
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x0014EA24 File Offset: 0x0014CC24
		internal bool ContainsTypeNames(ICollection typeNames)
		{
			if (this._registeredTypeNames != null && typeNames != null)
			{
				foreach (object obj in typeNames)
				{
					string o = (string)obj;
					if (this._registeredTypeNames.Contains(o))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x0014EA94 File Offset: 0x0014CC94
		internal void AddTypeNames(ICollection typeNames)
		{
			if (typeNames == null)
			{
				return;
			}
			if (this._registeredTypeNames == null)
			{
				this._registeredTypeNames = new CaseInsensitiveStringSet();
			}
			this._registeredTypeNames.AddCollection(typeNames);
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x0014EABC File Offset: 0x0014CCBC
		internal AssemblyBuilder(CompilationSection compConfig, ICollection referencedAssemblies, CompilerType compilerType, string outputAssemblyName)
		{
			this._compConfig = compConfig;
			this._outputAssemblyName = outputAssemblyName;
			this._initialReferencedAssemblies = AssemblySet.Create(referencedAssemblies);
			this._compilerType = compilerType.Clone();
			if (BuildManager.PrecompilingWithDebugInfo)
			{
				this._compilerType.CompilerParameters.IncludeDebugInformation = true;
			}
			else if (BuildManager.PrecompilingForDeployment)
			{
				this._compilerType.CompilerParameters.IncludeDebugInformation = false;
			}
			else if (DeploymentSection.RetailInternal)
			{
				this._compilerType.CompilerParameters.IncludeDebugInformation = false;
			}
			else if (this._compConfig.AssemblyPostProcessorTypeInternal != null)
			{
				this._compilerType.CompilerParameters.IncludeDebugInformation = true;
			}
			this._tempFiles.KeepFiles = this._compilerType.CompilerParameters.IncludeDebugInformation;
			this._codeProvider = CompilationUtil.CreateCodeDomProviderNonPublic(this._compilerType.CodeDomProviderType);
			this._maxBatchSize = this._compConfig.MaxBatchSize;
			this._maxBatchGeneratedFileSize = (long)(this._compConfig.MaxBatchGeneratedFileSize * 1024);
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0014EBED File Offset: 0x0014CDED
		public void AddAssemblyReference(Assembly a)
		{
			if (this._additionalReferencedAssemblies == null)
			{
				this._additionalReferencedAssemblies = new AssemblySet();
			}
			this._additionalReferencedAssemblies.Add(a);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x0014EC0E File Offset: 0x0014CE0E
		internal void AddAssemblyReference(Assembly a, CodeCompileUnit ccu)
		{
			this.AddAssemblyReference(a);
			Util.AddAssemblyToStringCollection(a, ccu.ReferencedAssemblies);
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x0014EC24 File Offset: 0x0014CE24
		internal virtual TextWriter CreateCodeFile(BuildProvider buildProvider, out string filename)
		{
			string tempFilePhysicalPathWithAssert = this.GetTempFilePhysicalPathWithAssert(this._codeProvider.FileExtension);
			filename = tempFilePhysicalPathWithAssert;
			if (buildProvider != null)
			{
				if (this._buildProviderToSourceFileMap == null)
				{
					this._buildProviderToSourceFileMap = new Hashtable();
				}
				this._buildProviderToSourceFileMap[buildProvider] = tempFilePhysicalPathWithAssert;
				buildProvider.SetContributedCode();
			}
			this._sourceFiles.Add(tempFilePhysicalPathWithAssert);
			return this.CreateCodeFileWithAssert(tempFilePhysicalPathWithAssert);
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0014EC84 File Offset: 0x0014CE84
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private StreamWriter CreateCodeFileWithAssert(string generatedFilePath)
		{
			Stream stream = new FileStream(generatedFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
			return new StreamWriter(stream, Encoding.UTF8);
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0014ECA8 File Offset: 0x0014CEA8
		public TextWriter CreateCodeFile(BuildProvider buildProvider)
		{
			string text;
			return this.CreateCodeFile(buildProvider, out text);
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06006110 RID: 24848 RVA: 0x0014ECBE File Offset: 0x0014CEBE
		internal bool IsBatchFull
		{
			get
			{
				return this._sourceFiles.Count >= this._maxBatchSize || this._totalFileLength >= this._maxBatchGeneratedFileSize;
			}
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x0014ECE8 File Offset: 0x0014CEE8
		public void AddCodeCompileUnit(BuildProvider buildProvider, CodeCompileUnit compileUnit)
		{
			this.AddChecksumPragma(buildProvider, compileUnit);
			Util.AddAssembliesToStringCollection(this._initialReferencedAssemblies, compileUnit.ReferencedAssemblies);
			Util.AddAssembliesToStringCollection(this._additionalReferencedAssemblies, compileUnit.ReferencedAssemblies);
			string text;
			using (new ProcessImpersonationContext())
			{
				TextWriter textWriter = this.CreateCodeFile(buildProvider, out text);
				try
				{
					this._codeProvider.GenerateCodeFromCompileUnit(compileUnit, textWriter, null);
				}
				finally
				{
					textWriter.Flush();
					textWriter.Close();
				}
			}
			if (text != null)
			{
				this._totalFileLength += this.GetFileLengthWithAssert(text);
			}
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x0014ED8C File Offset: 0x0014CF8C
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.Read)]
		private long GetFileLengthWithAssert(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			return fileInfo.Length;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x0014EDA6 File Offset: 0x0014CFA6
		public void GenerateTypeFactory(string typeName)
		{
			if (this._objectFactoryGenerator == null)
			{
				this._objectFactoryGenerator = new ObjectFactoryCodeDomTreeGenerator(this.OutputAssemblyName);
			}
			this._objectFactoryGenerator.AddFactoryMethod(typeName, null);
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x0014EDCE File Offset: 0x0014CFCE
		internal void GenerateTypeFactory(string typeName, CodeCompileUnit ccu)
		{
			if (this._objectFactoryGenerator == null)
			{
				this._objectFactoryGenerator = new ObjectFactoryCodeDomTreeGenerator(this.OutputAssemblyName);
			}
			this._objectFactoryGenerator.AddFactoryMethod(typeName, ccu);
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x0014EDF8 File Offset: 0x0014CFF8
		public Stream CreateEmbeddedResource(BuildProvider buildProvider, string name)
		{
			if (!Util.IsValidFileName(name))
			{
				throw new ArgumentException(null, name);
			}
			string codegenResourceDir = BuildManager.CodegenResourceDir;
			string text = Path.Combine(codegenResourceDir, name);
			this.CreateTempResourceDirectoryIfNecessary();
			this._tempFiles.AddFile(text, this._tempFiles.KeepFiles);
			if (this._embeddedResourceFiles == null)
			{
				this._embeddedResourceFiles = new StringSet();
			}
			this._embeddedResourceFiles.Add(text);
			InternalSecurityPermissions.FileWriteAccess(codegenResourceDir).Assert();
			return File.OpenWrite(text);
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0014EE70 File Offset: 0x0014D070
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void CreateTempResourceDirectoryIfNecessary()
		{
			string codegenResourceDir = BuildManager.CodegenResourceDir;
			if (!FileUtil.DirectoryExists(codegenResourceDir))
			{
				Directory.CreateDirectory(codegenResourceDir);
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06006117 RID: 24855 RVA: 0x0014EE92 File Offset: 0x0014D092
		public CodeDomProvider CodeDomProvider
		{
			get
			{
				return this._codeProvider;
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06006118 RID: 24856 RVA: 0x0014EE9C File Offset: 0x0014D09C
		private string TempFilePhysicalPathPrefix
		{
			get
			{
				if (this._tempFilePhysicalPathPrefix == null)
				{
					this._tempFilePhysicalPathPrefix = Path.Combine(this._tempFiles.TempDir, this.OutputAssemblyName) + ".";
					if (this.CultureName != null)
					{
						this._tempFilePhysicalPathPrefix = this._tempFilePhysicalPathPrefix + this.CultureName + "_";
					}
				}
				return this._tempFilePhysicalPathPrefix;
			}
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x0014EF04 File Offset: 0x0014D104
		public string GetTempFilePhysicalPath(string extension)
		{
			string text;
			if (!string.IsNullOrEmpty(extension) && extension[0] == '.')
			{
				string tempFilePhysicalPathPrefix = this.TempFilePhysicalPathPrefix;
				int fileCount = this._fileCount;
				this._fileCount = fileCount + 1;
				text = tempFilePhysicalPathPrefix + fileCount.ToString() + extension;
			}
			else
			{
				string tempFilePhysicalPathPrefix2 = this.TempFilePhysicalPathPrefix;
				int fileCount = this._fileCount;
				this._fileCount = fileCount + 1;
				text = tempFilePhysicalPathPrefix2 + fileCount.ToString() + "." + extension;
			}
			this._tempFiles.AddFile(text, this._tempFiles.KeepFiles);
			InternalSecurityPermissions.PathDiscovery(text).Demand();
			return text;
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x0014EF96 File Offset: 0x0014D196
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal string GetTempFilePhysicalPathWithAssert(string extension)
		{
			return this.GetTempFilePhysicalPath(extension);
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x0014EFA0 File Offset: 0x0014D1A0
		private void AddCompileWithBuildProvider(VirtualPath virtualPath, BuildProvider owningBuildProvider)
		{
			BuildProvider buildProvider = BuildManager.CreateBuildProvider(virtualPath, this._compConfig, this._initialReferencedAssemblies, true);
			buildProvider.SetNoBuildResult();
			SourceFileBuildProvider sourceFileBuildProvider = buildProvider as SourceFileBuildProvider;
			if (sourceFileBuildProvider != null)
			{
				sourceFileBuildProvider.OwningBuildProvider = owningBuildProvider;
			}
			this.AddBuildProvider(buildProvider);
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0014EFE0 File Offset: 0x0014D1E0
		internal virtual void AddBuildProvider(BuildProvider buildProvider)
		{
			object key = buildProvider;
			bool flag = false;
			if (this._compConfig.FolderLevelBuildProviders != null)
			{
				Type type = buildProvider.GetType();
				flag = this._compConfig.FolderLevelBuildProviders.IsFolderLevelBuildProvider(type);
			}
			if (buildProvider.VirtualPath != null && !flag)
			{
				key = buildProvider.VirtualPath;
				if (this._buildProviders.ContainsKey(key))
				{
					return;
				}
			}
			this._buildProviders[key] = buildProvider;
			try
			{
				buildProvider.GenerateCode(this);
			}
			catch (XmlException ex)
			{
				throw new HttpParseException(ex.Message, null, buildProvider.VirtualPath, null, ex.LineNumber);
			}
			catch (XmlSchemaException ex2)
			{
				throw new HttpParseException(ex2.Message, null, buildProvider.VirtualPath, null, ex2.LineNumber);
			}
			catch (Exception ex3)
			{
				throw new HttpParseException(ex3.Message, ex3, buildProvider.VirtualPath, null, 1);
			}
			InternalBuildProvider internalBuildProvider = buildProvider as InternalBuildProvider;
			if (internalBuildProvider != null)
			{
				ICollection compileWithDependencies = internalBuildProvider.GetCompileWithDependencies();
				if (compileWithDependencies != null)
				{
					foreach (object obj in compileWithDependencies)
					{
						VirtualPath virtualPath = (VirtualPath)obj;
						if (!this._buildProviders.ContainsKey(virtualPath.VirtualPathString))
						{
							this.AddCompileWithBuildProvider(virtualPath, internalBuildProvider);
						}
					}
				}
			}
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x0014F144 File Offset: 0x0014D344
		private void AddAssemblyCultureAttribute()
		{
			if (this.CultureName == null)
			{
				return;
			}
			CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyCultureAttribute)), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(this.CultureName))
			});
			this.AddAssemblyAttribute(declaration);
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x0014F190 File Offset: 0x0014D390
		private void AddAspNetGeneratedCodeAttribute()
		{
			this.AddAssemblyAttribute(new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)))
			{
				Arguments = 
				{
					new CodeAttributeArgument(new CodePrimitiveExpression("ASP.NET")),
					new CodeAttributeArgument(new CodePrimitiveExpression(VersionInfo.SystemWebVersion))
				}
			});
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0014F1F0 File Offset: 0x0014D3F0
		private void AddAllowPartiallyTrustedCallersAttribute()
		{
			if (BuildManager.CompileWithAllowPartiallyTrustedCallersAttribute)
			{
				CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(AllowPartiallyTrustedCallersAttribute)));
				this.AddAssemblyAttribute(declaration);
			}
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0014F220 File Offset: 0x0014D420
		private void AddAssemblyKeyFileAttribute()
		{
			if (!string.IsNullOrEmpty(BuildManager.StrongNameKeyFile))
			{
				CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyKeyFileAttribute)), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(BuildManager.StrongNameKeyFile))
				});
				this.AddAssemblyAttribute(declaration);
			}
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x0014F270 File Offset: 0x0014D470
		private void AddAssemblyKeyContainerAttribute()
		{
			if (!string.IsNullOrEmpty(BuildManager.StrongNameKeyContainer))
			{
				CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyKeyNameAttribute)), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(BuildManager.StrongNameKeyContainer))
				});
				this.AddAssemblyAttribute(declaration);
			}
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x0014F2C0 File Offset: 0x0014D4C0
		private void AddAssemblyDelaySignAttribute()
		{
			if (BuildManager.CompileWithDelaySignAttribute)
			{
				CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(AssemblyDelaySignAttribute)), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(true))
				});
				this.AddAssemblyAttribute(declaration);
			}
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0014F30C File Offset: 0x0014D50C
		private void AddSecurityRulesAttribute()
		{
			if (MultiTargetingUtil.IsTargetFramework20 || MultiTargetingUtil.IsTargetFramework35)
			{
				return;
			}
			TrustSection trust = RuntimeConfig.GetAppConfig().Trust;
			Type typeFromHandle = typeof(SecurityRulesAttribute);
			Type typeFromHandle2 = typeof(SecurityRuleSet);
			CodeAttributeDeclaration declaration;
			if (trust.LegacyCasModel)
			{
				SecurityRuleSet securityRuleSet = SecurityRuleSet.Level1;
				string name = Enum.GetName(typeFromHandle2, securityRuleSet);
				CodeFieldReferenceExpression value = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeFromHandle2), name);
				declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeFromHandle), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(value)
				});
				this.AddAssemblyAttribute(declaration);
				return;
			}
			SecurityRuleSet securityRuleSet2 = SecurityRuleSet.Level2;
			string name2 = Enum.GetName(typeFromHandle2, securityRuleSet2);
			CodeFieldReferenceExpression value2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeFromHandle2), name2);
			declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeFromHandle), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(value2)
			});
			this.AddAssemblyAttribute(declaration);
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x0014F3DC File Offset: 0x0014D5DC
		private void AddTargetFrameworkAttribute()
		{
			if (MultiTargetingUtil.TargetFrameworkVersion.Major >= 4)
			{
				CodeAttributeDeclaration declaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(TargetFrameworkAttribute)), new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(BuildManager.TargetFramework.FullName))
				});
				this.AddAssemblyAttribute(declaration);
			}
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0014F42F File Offset: 0x0014D62F
		private void AddAssemblyAttribute(CodeAttributeDeclaration declaration)
		{
			if (this._miscCodeCompileUnit == null)
			{
				this._miscCodeCompileUnit = new CodeCompileUnit();
			}
			this._miscCodeCompileUnit.AssemblyCustomAttributes.Add(declaration);
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0014F456 File Offset: 0x0014D656
		private void GenerateMiscCodeCompileUnit()
		{
			if (this._miscCodeCompileUnit == null)
			{
				return;
			}
			this.AddCodeCompileUnit(null, this._miscCodeCompileUnit);
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x0014F470 File Offset: 0x0014D670
		private void AddChecksumPragma(BuildProvider buildProvider, CodeCompileUnit compileUnit)
		{
			if (buildProvider == null || buildProvider.VirtualPath == null)
			{
				return;
			}
			if (!this._compilerType.CompilerParameters.IncludeDebugInformation)
			{
				return;
			}
			string text = HostingEnvironment.MapPathInternal(buildProvider.VirtualPath);
			if (!File.Exists(text))
			{
				return;
			}
			string name = BinaryCompatibility.Current.TargetsAtLeastFramework472 ? "SHA256" : "SHA1";
			CodeChecksumPragma codeChecksumPragma = new CodeChecksumPragma
			{
				ChecksumAlgorithmId = (BinaryCompatibility.Current.TargetsAtLeastFramework472 ? AssemblyBuilder.s_codeChecksumSha256Id : AssemblyBuilder.s_codeChecksumSha1Id)
			};
			if (this._compConfig.UrlLinePragmas)
			{
				codeChecksumPragma.FileName = ErrorFormatter.MakeHttpLinePragma(buildProvider.VirtualPathObject.VirtualPathString);
			}
			else
			{
				codeChecksumPragma.FileName = text;
			}
			using (Stream stream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (HashAlgorithm hashAlgorithm = (HashAlgorithm)CryptoConfig.CreateFromName(name))
				{
					codeChecksumPragma.ChecksumData = hashAlgorithm.ComputeHash(stream);
				}
			}
			compileUnit.StartDirectives.Add(codeChecksumPragma);
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x0014F580 File Offset: 0x0014D780
		internal CompilerParameters GetCompilerParameters()
		{
			CompilerParameters compilerParameters = this._compilerType.CompilerParameters;
			string text = this._tempFiles.TempDir;
			if (this.CultureName != null)
			{
				text = Path.Combine(text, this.CultureName);
				Directory.CreateDirectory(text);
				compilerParameters.OutputAssembly = Path.Combine(text, this.OutputAssemblyName + ".resources.dll");
			}
			else
			{
				compilerParameters.OutputAssembly = Path.Combine(text, this.OutputAssemblyName + ".dll");
			}
			if (File.Exists(compilerParameters.OutputAssembly))
			{
				Util.RemoveOrRenameFile(compilerParameters.OutputAssembly);
			}
			compilerParameters.TempFiles = this._tempFiles;
			if (this._stringResourceBuilder != null && this._stringResourceBuilder.HasStrings)
			{
				string text2 = this._tempFiles.AddExtension("res");
				this._stringResourceBuilder.CreateResourceFile(text2);
				compilerParameters.Win32Resource = text2;
			}
			if (this._embeddedResourceFiles != null)
			{
				foreach (object obj in ((IEnumerable)this._embeddedResourceFiles))
				{
					string value = (string)obj;
					compilerParameters.EmbeddedResources.Add(value);
				}
			}
			if (this._additionalReferencedAssemblies != null)
			{
				foreach (object obj2 in ((IEnumerable)this._additionalReferencedAssemblies))
				{
					Assembly o = (Assembly)obj2;
					this._initialReferencedAssemblies.Add(o);
				}
			}
			Util.AddAssembliesToStringCollection(this._initialReferencedAssemblies, compilerParameters.ReferencedAssemblies);
			AssemblyBuilder.FixUpCompilerParameters(this._compConfig, this._compilerType.CodeDomProviderType, compilerParameters);
			return compilerParameters;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x0014F740 File Offset: 0x0014D940
		private static void AddVBGlobalNamespaceImports(CompilerParameters compilParams)
		{
			if (AssemblyBuilder.s_vbImportsString == null)
			{
				PagesSection pagesAppConfig = MTConfigUtil.GetPagesAppConfig();
				if (pagesAppConfig.Namespaces == null)
				{
					AssemblyBuilder.s_vbImportsString = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("/imports:");
					bool flag = false;
					if (pagesAppConfig.Namespaces.AutoImportVBNamespace)
					{
						stringBuilder.Append("Microsoft.VisualBasic");
						flag = true;
					}
					foreach (object obj in pagesAppConfig.Namespaces)
					{
						NamespaceInfo namespaceInfo = (NamespaceInfo)obj;
						if (flag)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(namespaceInfo.Namespace);
						flag = true;
					}
					AssemblyBuilder.s_vbImportsString = stringBuilder.ToString();
				}
			}
			if (AssemblyBuilder.s_vbImportsString.Length > 0)
			{
				if (compilParams.CompilerOptions == null)
				{
					compilParams.CompilerOptions = AssemblyBuilder.s_vbImportsString;
					return;
				}
				compilParams.CompilerOptions = AssemblyBuilder.s_vbImportsString + " " + compilParams.CompilerOptions;
			}
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x0014F850 File Offset: 0x0014DA50
		private static void AddVBMyFlags(CompilerParameters compilParams)
		{
			if (compilParams.CompilerOptions == null)
			{
				compilParams.CompilerOptions = "/define:_MYTYPE=\\\"Web\\\"";
				return;
			}
			compilParams.CompilerOptions = "/define:_MYTYPE=\\\"Web\\\" " + compilParams.CompilerOptions;
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x0014F87C File Offset: 0x0014DA7C
		internal static void FixUpCompilerParameters(CompilationSection compilationSection, Type codeDomProviderType, CompilerParameters compilParams)
		{
			if (BuildManagerHost.InClientBuildManager && !MultiTargetingUtil.IsTargetFramework20 && !MultiTargetingUtil.IsTargetFramework35)
			{
				string coreAssemblyFileName;
				AssemblyResolver.GetPathToReferenceAssembly(typeof(string).Assembly, out coreAssemblyFileName);
				compilParams.CoreAssemblyFileName = coreAssemblyFileName;
			}
			bool flag = !BuildManagerHost.InClientBuildManager && compilationSection.DisableObsoleteWarnings;
			if (codeDomProviderType == typeof(CSharpCodeProvider))
			{
				List<string> list = new List<string>(5);
				list.AddRange(new string[]
				{
					"1659",
					"1699",
					"1701"
				});
				if (flag)
				{
					list.Add("612");
					list.Add("618");
				}
				CodeDomUtility.PrependCompilerOption(compilParams, "/nowarn:" + string.Join(";", list));
			}
			else if (codeDomProviderType == typeof(VBCodeProvider))
			{
				List<string> list2 = new List<string>(3);
				AssemblyBuilder.AddVBGlobalNamespaceImports(compilParams);
				AssemblyBuilder.AddVBMyFlags(compilParams);
				if (MultiTargetingUtil.TargetFrameworkVersion >= MultiTargetingUtil.Version35)
				{
					list2.Add("41008");
				}
				if (flag)
				{
					list2.Add("40000");
					list2.Add("40008");
				}
				if (list2.Count > 0)
				{
					CodeDomUtility.PrependCompilerOption(compilParams, "/nowarn:" + string.Join(",", list2));
				}
			}
			AssemblyBuilder.ProcessProviderOptions(codeDomProviderType, compilParams);
			AssemblyBuilder.FixTreatWarningsAsErrors(codeDomProviderType, compilParams);
			if (BuildManager.PrecompilingWithCodeAnalysisSymbol)
			{
				CodeDomUtility.PrependCompilerOption(compilParams, "/define:CODE_ANALYSIS");
			}
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x0014F9E0 File Offset: 0x0014DBE0
		internal static void FixTreatWarningsAsErrors(Type codeDomProviderType, CompilerParameters compilParams)
		{
			if (codeDomProviderType != typeof(CSharpCodeProvider) && codeDomProviderType != typeof(VBCodeProvider))
			{
				return;
			}
			if (CultureInfo.InvariantCulture.CompareInfo.IndexOf(compilParams.CompilerOptions, "/warnaserror", CompareOptions.IgnoreCase) >= 0)
			{
				compilParams.TreatWarningsAsErrors = false;
			}
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x0014FA38 File Offset: 0x0014DC38
		private static void ProcessProviderOptions(Type codeDomProviderType, CompilerParameters compilParams)
		{
			IDictionary<string, string> providerOptions = CompilationUtil.GetProviderOptions(codeDomProviderType);
			if (providerOptions == null)
			{
				return;
			}
			if (codeDomProviderType == typeof(VBCodeProvider) || codeDomProviderType == typeof(CSharpCodeProvider))
			{
				AssemblyBuilder.ProcessBooleanProviderOption("WarnAsError", "/warnaserror+", "/warnaserror-", providerOptions, compilParams);
			}
			if (codeDomProviderType == null || !CompilationUtil.IsCompilerVersion35OrAbove(codeDomProviderType))
			{
				return;
			}
			if (codeDomProviderType == typeof(VBCodeProvider))
			{
				AssemblyBuilder.ProcessBooleanProviderOption("OptionInfer", "/optionInfer+", "/optionInfer-", providerOptions, compilParams);
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0014FAC4 File Offset: 0x0014DCC4
		private static void ProcessBooleanProviderOption(string providerOptionName, string trueCompilerOption, string falseCompilerOption, IDictionary<string, string> providerOptions, CompilerParameters compilParams)
		{
			if (providerOptions == null || compilParams == null)
			{
				return;
			}
			string value = null;
			if (!providerOptions.TryGetValue(providerOptionName, out value))
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ConfigurationErrorsException(SR.GetString("Property_NullOrEmpty", new object[]
				{
					"system.codedom/compilers/compiler/ProviderOption/" + providerOptionName
				}));
			}
			bool flag;
			if (!bool.TryParse(value, out flag))
			{
				throw new ConfigurationErrorsException(SR.GetString("Value_must_be_boolean", new object[]
				{
					"system.codedom/compilers/compiler/ProviderOption/" + providerOptionName
				}));
			}
			if (flag)
			{
				CodeDomUtility.AppendCompilerOption(compilParams, trueCompilerOption);
				return;
			}
			CodeDomUtility.AppendCompilerOption(compilParams, falseCompilerOption);
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0014FB58 File Offset: 0x0014DD58
		internal CompilerResults Compile()
		{
			if (this._sourceFiles.Count == 0 && this._embeddedResourceFiles == null)
			{
				return null;
			}
			if (this._objectFactoryGenerator != null)
			{
				this._miscCodeCompileUnit = this._objectFactoryGenerator.CodeCompileUnit;
			}
			this.AddAssemblyCultureAttribute();
			this.AddAspNetGeneratedCodeAttribute();
			this.AddAllowPartiallyTrustedCallersAttribute();
			this.AddAssemblyDelaySignAttribute();
			this.AddAssemblyKeyFileAttribute();
			this.AddAssemblyKeyContainerAttribute();
			this.AddSecurityRulesAttribute();
			this.AddTargetFrameworkAttribute();
			this.GenerateMiscCodeCompileUnit();
			CompilerParameters compilerParameters = this.GetCompilerParameters();
			string[] array = new string[this._sourceFiles.Count];
			this._sourceFiles.CopyTo(array, 0);
			PerfCounters.IncrementCounter(AppPerfCounter.COMPILATIONS);
			WebBaseEvent.RaiseSystemEvent(this, 1003);
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null && EtwTrace.IsTraceEnabled(5, 1))
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_COMPILE_ENTER, httpContext.WorkerRequest);
			}
			CompilerResults compilerResults = null;
			try
			{
				try
				{
					using (new ProcessImpersonationContext())
					{
						compilerResults = this._codeProvider.CompileAssemblyFromFile(compilerParameters, array);
					}
				}
				finally
				{
					if (EtwTrace.IsTraceEnabled(5, 1) && httpContext != null)
					{
						string text = null;
						if (this._buildProviders.Count < 20)
						{
							IDictionaryEnumerator enumerator = this._buildProviders.GetEnumerator();
							while (enumerator.MoveNext())
							{
								if (text != null)
								{
									text += ",";
								}
								string str = text;
								object key = enumerator.Key;
								text = str + ((key != null) ? key.ToString() : null);
							}
						}
						else
						{
							text = string.Format(CultureInfo.InstalledUICulture, SR.Resources.GetString("Etw_Batch_Compilation", CultureInfo.InstalledUICulture), new object[]
							{
								this._buildProviders.Count
							});
						}
						string @string;
						if (compilerResults != null && (compilerResults.NativeCompilerReturnValue != 0 || compilerResults.Errors.HasErrors))
						{
							@string = SR.Resources.GetString("Etw_Failure", CultureInfo.InstalledUICulture);
						}
						else
						{
							@string = SR.Resources.GetString("Etw_Success", CultureInfo.InstalledUICulture);
						}
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_COMPILE_LEAVE, httpContext.WorkerRequest, text, @string);
					}
				}
			}
			catch
			{
				throw;
			}
			Type assemblyPostProcessorTypeInternal = this._compConfig.AssemblyPostProcessorTypeInternal;
			if (assemblyPostProcessorTypeInternal != null)
			{
				using (IAssemblyPostProcessor assemblyPostProcessor = (IAssemblyPostProcessor)HttpRuntime.FastCreatePublicInstance(assemblyPostProcessorTypeInternal))
				{
					assemblyPostProcessor.PostProcessAssembly(compilerResults.PathToAssembly);
				}
			}
			WebBaseEvent.RaiseSystemEvent(this, 1004);
			if (compilerResults != null)
			{
				this.InvalidateInvalidAssembly(compilerResults, compilerParameters);
				this.FixUpLinePragmas(compilerResults);
				if (compilerResults.Errors.HasErrors)
				{
					foreach (object obj in this.BuildProviders)
					{
						BuildProvider buildProvider = (BuildProvider)obj;
						buildProvider.ProcessCompileErrors(compilerResults);
					}
				}
				if (BuildManager.CBMCallback != null)
				{
					foreach (object obj2 in compilerResults.Errors)
					{
						CompilerError error = (CompilerError)obj2;
						BuildManager.CBMCallback.ReportCompilerError(error);
					}
				}
				if (compilerResults.NativeCompilerReturnValue != 0 || compilerResults.Errors.HasErrors)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_COMPILING);
					PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
					throw new HttpCompileException(compilerResults, this.GetErrorSourceFileContents(compilerResults));
				}
			}
			return compilerResults;
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x0014FF18 File Offset: 0x0014E118
		private void InvalidateInvalidAssembly(CompilerResults results, CompilerParameters compilParams)
		{
			if (results == null || !results.Errors.HasErrors)
			{
				return;
			}
			foreach (object obj in results.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				if (!compilerError.IsWarning && StringUtil.EqualsIgnoreCase(compilerError.ErrorNumber, "CS0016"))
				{
					if (this.CultureName != null)
					{
						string tempDir = this._tempFiles.TempDir;
						string fileName = Path.Combine(tempDir, this.OutputAssemblyName + ".dll");
						DiskBuildResultCache.TryDeleteFile(new FileInfo(fileName));
					}
					DiskBuildResultCache.TryDeleteFile(compilParams.OutputAssembly);
				}
			}
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x0014FFDC File Offset: 0x0014E1DC
		private void FixUpLinePragmas(CompilerResults results)
		{
			CompilerError compilerError = null;
			for (int i = results.Errors.Count - 1; i >= 0; i--)
			{
				CompilerError compilerError2 = results.Errors[i];
				string text = ErrorFormatter.ResolveHttpFileName(compilerError2.FileName);
				if (File.Exists(text))
				{
					compilerError2.FileName = text;
					if (compilerError2.Line == 912304 || (compilerError2.Line == 912305 && compilerError2.ErrorText != null && compilerError2.ErrorText.IndexOf("FrameworkInitialize", StringComparison.OrdinalIgnoreCase) >= 0))
					{
						compilerError = compilerError2;
						results.Errors.RemoveAt(i);
					}
					else if (compilerError2.Line > 912304 && compilerError2.Line < 912354)
					{
						results.Errors.RemoveAt(i);
					}
				}
			}
			if (compilerError != null)
			{
				string text2 = Util.StringFromFile(compilerError.FileName);
				int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, "partial class", CompareOptions.IgnoreCase);
				if (num >= 0)
				{
					compilerError.Line = Util.LineCount(text2, 0, num) + 1;
				}
				else
				{
					compilerError.Line = 1;
				}
				compilerError.ErrorText = SR.GetString("Bad_Base_Class_In_Code_File");
				compilerError.ErrorNumber = "ASPNET";
				results.Errors.Insert(0, compilerError);
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x00150110 File Offset: 0x0014E310
		private string GetErrorSourceFileContents(CompilerResults results)
		{
			if (!results.Errors.HasErrors)
			{
				return null;
			}
			string fileName = results.Errors[0].FileName;
			BuildProvider buildProviderFromLinePragma = this.GetBuildProviderFromLinePragma(fileName);
			if (buildProviderFromLinePragma != null)
			{
				return this.GetGeneratedSourceFromBuildProvider(buildProviderFromLinePragma);
			}
			return Util.StringFromFileIfExists(fileName);
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x00150158 File Offset: 0x0014E358
		internal string GetGeneratedSourceFromBuildProvider(BuildProvider buildProvider)
		{
			string path = (string)this._buildProviderToSourceFileMap[buildProvider];
			return Util.StringFromFileIfExists(path);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x00150180 File Offset: 0x0014E380
		internal BuildProvider GetBuildProviderFromLinePragma(string linePragma)
		{
			BuildProvider buildProvider = this.GetBuildProviderFromLinePragmaInternal(linePragma);
			SourceFileBuildProvider sourceFileBuildProvider = buildProvider as SourceFileBuildProvider;
			if (sourceFileBuildProvider != null)
			{
				buildProvider = sourceFileBuildProvider.OwningBuildProvider;
			}
			return buildProvider;
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x001501A8 File Offset: 0x0014E3A8
		private BuildProvider GetBuildProviderFromLinePragmaInternal(string linePragma)
		{
			if (this._buildProviderToSourceFileMap == null)
			{
				return null;
			}
			string virtualPathFromHttpLinePragma = ErrorFormatter.GetVirtualPathFromHttpLinePragma(linePragma);
			foreach (object obj in this.BuildProviders)
			{
				BuildProvider buildProvider = (BuildProvider)obj;
				if (buildProvider.VirtualPath != null)
				{
					if (virtualPathFromHttpLinePragma != null)
					{
						if (StringUtil.EqualsIgnoreCase(virtualPathFromHttpLinePragma, buildProvider.VirtualPath))
						{
							return buildProvider;
						}
					}
					else
					{
						string s = HostingEnvironment.MapPathInternal(buildProvider.VirtualPath);
						if (StringUtil.EqualsIgnoreCase(linePragma, s))
						{
							return buildProvider;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0400326E RID: 12910
		private CompilationSection _compConfig;

		// Token: 0x0400326F RID: 12911
		private static readonly Guid s_codeChecksumSha1Id = new Guid(4279768812U, 43614, 19728, 135, 247, 111, 73, 99, 131, 52, 96);

		// Token: 0x04003270 RID: 12912
		private static readonly Guid s_codeChecksumSha256Id = new Guid(2284441615U, 4536, 16915, 135, 139, 119, 14, 133, 151, 172, 22);

		// Token: 0x04003271 RID: 12913
		private Hashtable _buildProviders = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003272 RID: 12914
		private StringSet _sourceFiles = new StringSet();

		// Token: 0x04003273 RID: 12915
		private CodeCompileUnit _miscCodeCompileUnit;

		// Token: 0x04003274 RID: 12916
		private StringSet _embeddedResourceFiles;

		// Token: 0x04003275 RID: 12917
		private AssemblySet _initialReferencedAssemblies;

		// Token: 0x04003276 RID: 12918
		private AssemblySet _additionalReferencedAssemblies;

		// Token: 0x04003277 RID: 12919
		internal CodeDomProvider _codeProvider;

		// Token: 0x04003278 RID: 12920
		private Hashtable _buildProviderToSourceFileMap;

		// Token: 0x04003279 RID: 12921
		private CompilerType _compilerType;

		// Token: 0x0400327A RID: 12922
		private ObjectFactoryCodeDomTreeGenerator _objectFactoryGenerator;

		// Token: 0x0400327B RID: 12923
		private StringResourceBuilder _stringResourceBuilder;

		// Token: 0x0400327C RID: 12924
		private TempFileCollection _tempFiles = new TempFileCollection(HttpRuntime.CodegenDirInternal);

		// Token: 0x0400327D RID: 12925
		private int _fileCount;

		// Token: 0x0400327E RID: 12926
		private string _cultureName;

		// Token: 0x0400327F RID: 12927
		private string _outputAssemblyName;

		// Token: 0x04003280 RID: 12928
		private int _maxBatchSize;

		// Token: 0x04003281 RID: 12929
		private long _maxBatchGeneratedFileSize;

		// Token: 0x04003282 RID: 12930
		private long _totalFileLength;

		// Token: 0x04003283 RID: 12931
		private CaseInsensitiveStringSet _registeredTypeNames;

		// Token: 0x04003284 RID: 12932
		private string _tempFilePhysicalPathPrefix;

		// Token: 0x04003285 RID: 12933
		private static string s_vbImportsString;

		// Token: 0x04003286 RID: 12934
		private const string MySupport = "/define:_MYTYPE=\\\"Web\\\"";
	}
}
