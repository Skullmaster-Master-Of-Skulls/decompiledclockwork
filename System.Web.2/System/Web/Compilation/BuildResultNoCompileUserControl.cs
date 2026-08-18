using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200081D RID: 2077
	internal class BuildResultNoCompileUserControl : BuildResultNoCompileTemplateControl
	{
		// Token: 0x06006366 RID: 25446 RVA: 0x0015C3B0 File Offset: 0x0015A5B0
		internal BuildResultNoCompileUserControl(Type baseType, TemplateParser parser) : base(baseType, parser)
		{
			UserControlParser userControlParser = (UserControlParser)parser;
			OutputCacheParameters outputCacheParameters = userControlParser.OutputCacheParameters;
			if (outputCacheParameters != null && outputCacheParameters.Duration > 0)
			{
				this._cachingAttribute = new PartialCachingAttribute(outputCacheParameters.Duration, outputCacheParameters.VaryByParam, outputCacheParameters.VaryByControl, outputCacheParameters.VaryByCustom, outputCacheParameters.SqlDependency, userControlParser.FSharedPartialCaching);
				this._cachingAttribute.ProviderName = userControlParser.Provider;
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06006367 RID: 25447 RVA: 0x0015C41F File Offset: 0x0015A61F
		internal PartialCachingAttribute CachingAttribute
		{
			get
			{
				return this._cachingAttribute;
			}
		}

		// Token: 0x04003383 RID: 13187
		private PartialCachingAttribute _cachingAttribute;
	}
}
