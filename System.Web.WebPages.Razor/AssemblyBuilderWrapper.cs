using System;
using System.CodeDom;
using System.Web.Compilation;

namespace System.Web.WebPages.Razor
{
	// Token: 0x02000004 RID: 4
	internal sealed class AssemblyBuilderWrapper : IAssemblyBuilder
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002225 File Offset: 0x00000425
		public AssemblyBuilderWrapper(AssemblyBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			this.InnerBuilder = builder;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002242 File Offset: 0x00000442
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000224A File Offset: 0x0000044A
		internal AssemblyBuilder InnerBuilder { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002253 File Offset: 0x00000453
		public void AddCodeCompileUnit(BuildProvider buildProvider, CodeCompileUnit compileUnit)
		{
			this.InnerBuilder.AddCodeCompileUnit(buildProvider, compileUnit);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002262 File Offset: 0x00000462
		public void GenerateTypeFactory(string typeName)
		{
			this.InnerBuilder.GenerateTypeFactory(typeName);
		}
	}
}
