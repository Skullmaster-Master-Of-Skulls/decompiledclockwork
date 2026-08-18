using System;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000819 RID: 2073
	internal class BuildResultCompiledTemplateType : BuildResultCompiledType
	{
		// Token: 0x06006353 RID: 25427 RVA: 0x0015C09F File Offset: 0x0015A29F
		public BuildResultCompiledTemplateType()
		{
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x0015C0A7 File Offset: 0x0015A2A7
		public BuildResultCompiledTemplateType(Type t) : base(t)
		{
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x0012E48B File Offset: 0x0012C68B
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCompiledTemplateType;
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x0015C0B0 File Offset: 0x0015A2B0
		protected override void ComputeHashCode(HashCodeCombiner hashCodeCombiner)
		{
			base.ComputeHashCode(hashCodeCombiner);
			PagesSection pagesConfig = MTConfigUtil.GetPagesConfig(base.VirtualPath);
			hashCodeCombiner.AddObject(Util.GetRecompilationHash(pagesConfig));
		}
	}
}
