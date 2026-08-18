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
	// Token: 0x020007FD RID: 2045
	internal abstract class BaseTemplateBuildProvider : InternalBuildProvider
	{
		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06006196 RID: 24982 RVA: 0x001522E8 File Offset: 0x001504E8
		internal TemplateParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x06006197 RID: 24983 RVA: 0x001522E8 File Offset: 0x001504E8
		internal override IAssemblyDependencyParser AssemblyDependencyParser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x06006198 RID: 24984
		protected abstract TemplateParser CreateParser();

		// Token: 0x06006199 RID: 24985
		internal abstract BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser);

		// Token: 0x0600619A RID: 24986 RVA: 0x001522F0 File Offset: 0x001504F0
		protected internal override CodeCompileUnit GetCodeCompileUnit(out IDictionary linePragmasTable)
		{
			Type codeDomProviderType = this._parser.CompilerType.CodeDomProviderType;
			CodeDomProvider codeDomProvider = CompilationUtil.CreateCodeDomProviderNonPublic(codeDomProviderType);
			BaseCodeDomTreeGenerator baseCodeDomTreeGenerator = this.CreateCodeDomTreeGenerator(this._parser);
			baseCodeDomTreeGenerator.SetDesignerMode();
			CodeCompileUnit codeDomTree = baseCodeDomTreeGenerator.GetCodeDomTree(codeDomProvider, new StringResourceBuilder(), base.VirtualPathObject);
			linePragmasTable = baseCodeDomTreeGenerator.LinePragmasTable;
			return codeDomTree;
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x0600619B RID: 24987 RVA: 0x00152344 File Offset: 0x00150544
		public override CompilerType CodeCompilerType
		{
			get
			{
				this._parser = this.CreateParser();
				if (this.IgnoreParseErrors)
				{
					this._parser.IgnoreParseErrors = true;
				}
				if (base.IgnoreControlProperties)
				{
					this._parser.IgnoreControlProperties = true;
				}
				if (!base.ThrowOnFirstParseError)
				{
					this._parser.ThrowOnFirstParseError = false;
				}
				this._parser.Parse(base.ReferencedAssemblies, base.VirtualPathObject);
				if (!this.Parser.RequiresCompilation)
				{
					return null;
				}
				return this._parser.CompilerType;
			}
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x001523CA File Offset: 0x001505CA
		internal override ICollection GetCompileWithDependencies()
		{
			if (this._parser.CodeFileVirtualPath == null)
			{
				return null;
			}
			return new SingleObjectCollection(this._parser.CodeFileVirtualPath);
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x001523F4 File Offset: 0x001505F4
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			if (!this.Parser.RequiresCompilation)
			{
				return;
			}
			BaseCodeDomTreeGenerator baseCodeDomTreeGenerator = this.CreateCodeDomTreeGenerator(this._parser);
			CodeCompileUnit codeDomTree = baseCodeDomTreeGenerator.GetCodeDomTree(assemblyBuilder.CodeDomProvider, assemblyBuilder.StringResourceBuilder, base.VirtualPathObject);
			if (codeDomTree != null)
			{
				if (this._parser.AssemblyDependencies != null)
				{
					foreach (object obj in ((IEnumerable)this._parser.AssemblyDependencies))
					{
						Assembly a = (Assembly)obj;
						assemblyBuilder.AddAssemblyReference(a, codeDomTree);
					}
				}
				assemblyBuilder.AddCodeCompileUnit(this, codeDomTree);
			}
			this._instantiatableFullTypeName = baseCodeDomTreeGenerator.GetInstantiatableFullTypeName();
			if (this._instantiatableFullTypeName != null)
			{
				assemblyBuilder.GenerateTypeFactory(this._instantiatableFullTypeName, codeDomTree);
			}
			this._intermediateFullTypeName = baseCodeDomTreeGenerator.GetIntermediateFullTypeName();
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x001524D0 File Offset: 0x001506D0
		public override Type GetGeneratedType(CompilerResults results)
		{
			return this.GetGeneratedType(results, false);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x001524DC File Offset: 0x001506DC
		internal Type GetGeneratedType(CompilerResults results, bool useDelayLoadTypeIfEnabled)
		{
			if (!this.Parser.RequiresCompilation)
			{
				return null;
			}
			string text;
			if (this._instantiatableFullTypeName == null)
			{
				if (!(this.Parser.CodeFileVirtualPath != null))
				{
					return this.Parser.BaseType;
				}
				text = this._intermediateFullTypeName;
			}
			else
			{
				text = this._instantiatableFullTypeName;
			}
			Type result;
			if (useDelayLoadTypeIfEnabled && DelayLoadType.Enabled)
			{
				string fileName = Path.GetFileName(results.PathToAssembly);
				string assemblyNameFromFileName = Util.GetAssemblyNameFromFileName(fileName);
				result = new DelayLoadType(assemblyNameFromFileName, text);
			}
			else
			{
				result = results.CompiledAssembly.GetType(text);
			}
			return result;
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x00152564 File Offset: 0x00150764
		internal override BuildResultCompiledType CreateBuildResult(Type t)
		{
			return new BuildResultCompiledTemplateType(t);
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x060061A1 RID: 24993 RVA: 0x0015256C File Offset: 0x0015076C
		public override ICollection VirtualPathDependencies
		{
			get
			{
				return this._parser.SourceDependencies;
			}
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x0015257C File Offset: 0x0015077C
		internal override ICollection GetGeneratedTypeNames()
		{
			if (this._parser.GeneratedClassName == null && this._parser.BaseTypeName == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			if (this._parser.GeneratedClassName != null)
			{
				arrayList.Add(this._parser.GeneratedClassName);
			}
			if (this._parser.BaseTypeName != null)
			{
				arrayList.Add(Util.MakeFullTypeName(this._parser.BaseTypeNamespace, this._parser.BaseTypeName));
			}
			return arrayList;
		}

		// Token: 0x040032BE RID: 12990
		private TemplateParser _parser;

		// Token: 0x040032BF RID: 12991
		private string _instantiatableFullTypeName;

		// Token: 0x040032C0 RID: 12992
		private string _intermediateFullTypeName;
	}
}
