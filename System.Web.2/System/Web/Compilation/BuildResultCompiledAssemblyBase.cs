using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000814 RID: 2068
	internal abstract class BuildResultCompiledAssemblyBase : BuildResult
	{
		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x0600631A RID: 25370 RVA: 0x0015BA06 File Offset: 0x00159C06
		// (set) Token: 0x0600631B RID: 25371 RVA: 0x0015BA18 File Offset: 0x00159C18
		internal bool UsesExistingAssembly
		{
			get
			{
				return this._flags[131072];
			}
			set
			{
				this._flags[131072] = value;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x0015BA2B File Offset: 0x00159C2B
		internal override bool IsUnloadable
		{
			get
			{
				return this.ResultAssembly == null;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x0600631D RID: 25373
		// (set) Token: 0x0600631E RID: 25374
		internal abstract Assembly ResultAssembly { get; set; }

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x0600631F RID: 25375 RVA: 0x0015BA39 File Offset: 0x00159C39
		internal virtual bool HasResultAssembly
		{
			get
			{
				return this.ResultAssembly != null;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06006320 RID: 25376 RVA: 0x0015BA47 File Offset: 0x00159C47
		protected virtual bool IsGacAssembly
		{
			get
			{
				return this.ResultAssembly.GlobalAssemblyCache;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06006321 RID: 25377 RVA: 0x0015BA54 File Offset: 0x00159C54
		protected virtual string ShortAssemblyName
		{
			get
			{
				return this.ResultAssembly.GetName().Name;
			}
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x0015BA68 File Offset: 0x00159C68
		internal static Assembly GetPreservedAssembly(PreservationFileReader pfr)
		{
			string attribute = pfr.GetAttribute("assembly");
			if (attribute == null)
			{
				return null;
			}
			Assembly result;
			try
			{
				Assembly assembly = Assembly.Load(attribute);
				if (BuildResultCompiledAssemblyBase.AssemblyIsInvalid(assembly))
				{
					throw new InvalidOperationException();
				}
				BuildResultCompiledAssemblyBase.CheckAssemblyIsValid(assembly, new Hashtable());
				result = assembly;
			}
			catch
			{
				pfr.DiskCache.RemoveAssemblyAndRelatedFiles(attribute);
				throw;
			}
			return result;
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0015BACC File Offset: 0x00159CCC
		private static void CheckAssemblyIsValid(Assembly a, Hashtable checkedAssemblies)
		{
			checkedAssemblies.Add(a, null);
			foreach (AssemblyName assemblyRef in a.GetReferencedAssemblies())
			{
				Assembly assembly = Assembly.Load(assemblyRef);
				if (!assembly.GlobalAssemblyCache && BuildResultCompiledAssemblyBase.AssemblyIsInCodegenDir(assembly) && !checkedAssemblies.Contains(assembly))
				{
					if (BuildResultCompiledAssemblyBase.AssemblyIsInvalid(assembly))
					{
						throw new InvalidOperationException();
					}
					BuildResultCompiledAssemblyBase.CheckAssemblyIsValid(assembly, checkedAssemblies);
				}
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x0015BB30 File Offset: 0x00159D30
		internal static bool AssemblyIsInCodegenDir(Assembly a)
		{
			string assemblyCodeBase = Util.GetAssemblyCodeBase(a);
			FileInfo fileInfo = new FileInfo(assemblyCodeBase);
			string a2 = FileUtil.RemoveTrailingDirectoryBackSlash(fileInfo.Directory.FullName);
			if (BuildResultCompiledAssemblyBase.s_codegenDir == null)
			{
				BuildResultCompiledAssemblyBase.s_codegenDir = FileUtil.RemoveTrailingDirectoryBackSlash(HttpRuntime.CodegenDir);
			}
			return string.Equals(a2, BuildResultCompiledAssemblyBase.s_codegenDir, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x0015BB84 File Offset: 0x00159D84
		private static bool AssemblyIsInvalid(Assembly a)
		{
			string assemblyCodeBase = Util.GetAssemblyCodeBase(a);
			return !FileUtil.FileExists(assemblyCodeBase) || DiskBuildResultCache.HasDotDeleteFile(assemblyCodeBase);
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x0015BBA8 File Offset: 0x00159DA8
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			if (this.HasResultAssembly)
			{
				string value;
				if (this.IsGacAssembly)
				{
					value = this.ResultAssembly.FullName;
				}
				else
				{
					value = this.ShortAssemblyName;
				}
				pfw.SetAttribute("assembly", value);
			}
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x0015BBF0 File Offset: 0x00159DF0
		internal override void RemoveOutOfDateResources(PreservationFileReader pfr)
		{
			base.ReadPreservedFlags(pfr);
			if (this.UsesExistingAssembly)
			{
				return;
			}
			string attribute = pfr.GetAttribute("assembly");
			if (attribute != null)
			{
				pfr.DiskCache.RemoveAssemblyAndRelatedFiles(attribute);
			}
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x0015BC28 File Offset: 0x00159E28
		protected override void ComputeHashCode(HashCodeCombiner hashCodeCombiner)
		{
			base.ComputeHashCode(hashCodeCombiner);
			CompilationSection compilationConfig = MTConfigUtil.GetCompilationConfig(base.VirtualPath);
			hashCodeCombiner.AddObject(compilationConfig.RecompilationHash);
		}

		// Token: 0x04003372 RID: 13170
		private static string s_codegenDir;
	}
}
