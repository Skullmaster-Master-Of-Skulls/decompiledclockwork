using System;

namespace System.Web.Compilation
{
	// Token: 0x02000812 RID: 2066
	internal class BuildResultCompileError : BuildResult
	{
		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x0015B96B File Offset: 0x00159B6B
		internal HttpCompileException CompileException
		{
			get
			{
				return this._compileException;
			}
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0015B973 File Offset: 0x00159B73
		internal BuildResultCompileError(VirtualPath virtualPath, HttpCompileException compileException)
		{
			base.VirtualPath = virtualPath;
			this._compileException = compileException;
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool CacheToDisk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x0015B98C File Offset: 0x00159B8C
		internal override DateTime MemoryCacheExpiration
		{
			get
			{
				return DateTime.UtcNow.AddSeconds(10.0);
			}
		}

		// Token: 0x04003370 RID: 13168
		private HttpCompileException _compileException;
	}
}
