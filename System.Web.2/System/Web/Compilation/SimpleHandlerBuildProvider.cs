using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000860 RID: 2144
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal abstract class SimpleHandlerBuildProvider : InternalBuildProvider
	{
		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x00164C52 File Offset: 0x00162E52
		internal override IAssemblyDependencyParser AssemblyDependencyParser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x0600655D RID: 25949
		protected abstract SimpleWebHandlerParser CreateParser();

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x00164C5C File Offset: 0x00162E5C
		public override CompilerType CodeCompilerType
		{
			get
			{
				this._parser = this.CreateParser();
				this._parser.SetBuildProvider(this);
				this._parser.IgnoreParseErrors = this.IgnoreParseErrors;
				this._parser.Parse(base.ReferencedAssemblies);
				return this._parser.CompilerType;
			}
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x00164CB0 File Offset: 0x00162EB0
		protected internal override CodeCompileUnit GetCodeCompileUnit(out IDictionary linePragmasTable)
		{
			CodeCompileUnit codeModel = this._parser.GetCodeModel();
			linePragmasTable = this._parser.GetLinePragmasTable();
			return codeModel;
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x00164CD8 File Offset: 0x00162ED8
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			CodeCompileUnit codeModel = this._parser.GetCodeModel();
			if (codeModel == null)
			{
				return;
			}
			assemblyBuilder.AddCodeCompileUnit(this, codeModel);
			if (this._parser.AssemblyDependencies != null)
			{
				foreach (object obj in this._parser.AssemblyDependencies)
				{
					Assembly a = (Assembly)obj;
					assemblyBuilder.AddAssemblyReference(a, codeModel);
				}
			}
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x00164D5C File Offset: 0x00162F5C
		public override Type GetGeneratedType(CompilerResults results)
		{
			Type typeToCache;
			if (this._parser.HasInlineCode)
			{
				typeToCache = this._parser.GetTypeToCache(results.CompiledAssembly);
			}
			else
			{
				typeToCache = this._parser.GetTypeToCache(null);
			}
			return typeToCache;
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x00164D98 File Offset: 0x00162F98
		public override ICollection VirtualPathDependencies
		{
			get
			{
				return this._parser.SourceDependencies;
			}
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x00164DA5 File Offset: 0x00162FA5
		internal CompilerType GetDefaultCompilerTypeForLanguageInternal(string language)
		{
			return base.GetDefaultCompilerTypeForLanguage(language);
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x00164DAE File Offset: 0x00162FAE
		internal CompilerType GetDefaultCompilerTypeInternal()
		{
			return base.GetDefaultCompilerType();
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00164DB6 File Offset: 0x00162FB6
		internal TextReader OpenReaderInternal()
		{
			return base.OpenReader();
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x00164DBE File Offset: 0x00162FBE
		internal override ICollection GetGeneratedTypeNames()
		{
			return new SingleObjectCollection(this._parser.TypeName);
		}

		// Token: 0x04003435 RID: 13365
		private SimpleWebHandlerParser _parser;
	}
}
