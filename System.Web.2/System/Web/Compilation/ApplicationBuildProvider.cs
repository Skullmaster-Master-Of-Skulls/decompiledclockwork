using System;
using System.CodeDom.Compiler;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x020007F2 RID: 2034
	internal class ApplicationBuildProvider : BaseTemplateBuildProvider
	{
		// Token: 0x060060F5 RID: 24821 RVA: 0x0014E6E0 File Offset: 0x0014C8E0
		internal static BuildResultCompiledGlobalAsaxType GetGlobalAsaxBuildResult(bool isPrecompiledApp)
		{
			string cacheKey = "App_global.asax";
			BuildResultCompiledGlobalAsaxType buildResultCompiledGlobalAsaxType = BuildManager.GetBuildResultFromCache(cacheKey) as BuildResultCompiledGlobalAsaxType;
			if (buildResultCompiledGlobalAsaxType != null)
			{
				return buildResultCompiledGlobalAsaxType;
			}
			if (isPrecompiledApp)
			{
				return null;
			}
			VirtualPath globalAsaxVirtualPath = BuildManager.GlobalAsaxVirtualPath;
			if (!globalAsaxVirtualPath.FileExists())
			{
				return null;
			}
			ApplicationBuildProvider applicationBuildProvider = new ApplicationBuildProvider();
			applicationBuildProvider.SetVirtualPath(globalAsaxVirtualPath);
			DateTime utcNow = DateTime.UtcNow;
			BuildProvidersCompiler buildProvidersCompiler = new BuildProvidersCompiler(globalAsaxVirtualPath, BuildManager.GenerateRandomAssemblyName("App_global.asax"));
			buildProvidersCompiler.SetBuildProviders(new SingleObjectCollection(applicationBuildProvider));
			CompilerResults results = buildProvidersCompiler.PerformBuild();
			buildResultCompiledGlobalAsaxType = (BuildResultCompiledGlobalAsaxType)applicationBuildProvider.GetBuildResult(results);
			buildResultCompiledGlobalAsaxType.CacheToMemory = false;
			BuildManager.CacheBuildResult(cacheKey, buildResultCompiledGlobalAsaxType, utcNow);
			return buildResultCompiledGlobalAsaxType;
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x0014E775 File Offset: 0x0014C975
		protected override TemplateParser CreateParser()
		{
			return new ApplicationFileParser();
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x0014E77C File Offset: 0x0014C97C
		internal override BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser)
		{
			return new ApplicationFileCodeDomTreeGenerator((ApplicationFileParser)parser);
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x0014E78C File Offset: 0x0014C98C
		internal override BuildResultCompiledType CreateBuildResult(Type t)
		{
			BuildResultCompiledGlobalAsaxType buildResultCompiledGlobalAsaxType = new BuildResultCompiledGlobalAsaxType(t);
			if (base.Parser.ApplicationObjects != null || base.Parser.SessionObjects != null)
			{
				buildResultCompiledGlobalAsaxType.HasAppOrSessionObjects = true;
			}
			return buildResultCompiledGlobalAsaxType;
		}
	}
}
