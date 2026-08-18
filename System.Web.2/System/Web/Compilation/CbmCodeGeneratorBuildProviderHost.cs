using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x020007F6 RID: 2038
	internal class CbmCodeGeneratorBuildProviderHost : AssemblyBuilder
	{
		// Token: 0x06006137 RID: 24887 RVA: 0x001502C4 File Offset: 0x0014E4C4
		internal CbmCodeGeneratorBuildProviderHost(CompilationSection compConfig, ICollection referencedAssemblies, CompilerType compilerType, string generatedFilesDir, string outputAssemblyName) : base(compConfig, referencedAssemblies, compilerType, outputAssemblyName)
		{
			if (Directory.Exists(generatedFilesDir))
			{
				foreach (object obj in ((IEnumerable)FileEnumerator.Create(generatedFilesDir)))
				{
					FileData fileData = (FileData)obj;
					if (!fileData.IsDirectory)
					{
						File.Delete(fileData.FullName);
					}
				}
			}
			Directory.CreateDirectory(generatedFilesDir);
			this._generatedFilesDir = generatedFilesDir;
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x00150350 File Offset: 0x0014E550
		internal override TextWriter CreateCodeFile(BuildProvider buildProvider, out string filename)
		{
			string text = BuildManager.GetCacheKeyFromVirtualPath(buildProvider.VirtualPathObject);
			text = Path.Combine(this._generatedFilesDir, text);
			text = FileUtil.TruncatePathIfNeeded(text, 10);
			text = text + "." + this._codeProvider.FileExtension;
			filename = text;
			BuildManager.GenerateFileTable[buildProvider.VirtualPathObject.VirtualPathStringNoTrailingSlash] = text;
			Stream stream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read);
			return new StreamWriter(stream, Encoding.UTF8);
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x001503C4 File Offset: 0x0014E5C4
		internal override void AddBuildProvider(BuildProvider buildProvider)
		{
			if (buildProvider is SourceFileBuildProvider)
			{
				return;
			}
			base.AddBuildProvider(buildProvider);
		}

		// Token: 0x04003287 RID: 12935
		private string _generatedFilesDir;
	}
}
