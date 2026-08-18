using System;
using System.CodeDom;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000863 RID: 2147
	internal sealed class SourceFileBuildProvider : InternalBuildProvider
	{
		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x00164DF2 File Offset: 0x00162FF2
		public override CompilerType CodeCompilerType
		{
			get
			{
				return CompilationUtil.GetCompilerInfoFromVirtualPath(base.VirtualPathObject);
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x00164E00 File Offset: 0x00163000
		private void EnsureCodeCompileUnit()
		{
			if (this._snippetCompileUnit == null)
			{
				string value = Util.StringFromVirtualPath(base.VirtualPathObject);
				this._snippetCompileUnit = new CodeSnippetCompileUnit(value);
				this._snippetCompileUnit.LinePragma = BaseCodeDomTreeGenerator.CreateCodeLinePragmaHelper(base.VirtualPath, 1);
			}
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x00164E44 File Offset: 0x00163044
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			this.EnsureCodeCompileUnit();
			assemblyBuilder.AddCodeCompileUnit(this, this._snippetCompileUnit);
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x00164E59 File Offset: 0x00163059
		protected internal override CodeCompileUnit GetCodeCompileUnit(out IDictionary linePragmasTable)
		{
			this.EnsureCodeCompileUnit();
			linePragmasTable = new Hashtable();
			linePragmasTable[1] = this._snippetCompileUnit.LinePragma;
			return this._snippetCompileUnit;
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x00164E86 File Offset: 0x00163086
		// (set) Token: 0x06006571 RID: 25969 RVA: 0x00164E8E File Offset: 0x0016308E
		internal BuildProvider OwningBuildProvider
		{
			get
			{
				return this._owningBuildProvider;
			}
			set
			{
				this._owningBuildProvider = value;
			}
		}

		// Token: 0x04003436 RID: 13366
		private CodeSnippetCompileUnit _snippetCompileUnit;

		// Token: 0x04003437 RID: 13367
		private BuildProvider _owningBuildProvider;
	}
}
