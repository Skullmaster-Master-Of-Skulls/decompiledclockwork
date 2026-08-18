using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000059 RID: 89
	public class DynamicMethodBuildPlan : IBuildPlanPolicy, IBuilderPolicy
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00005887 File Offset: 0x00003A87
		public DynamicMethodBuildPlan(DynamicBuildPlanMethod buildMethod)
		{
			this.buildMethod = buildMethod;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005896 File Offset: 0x00003A96
		public void BuildUp(IBuilderContext context)
		{
			this.buildMethod(context);
		}

		// Token: 0x04000058 RID: 88
		private DynamicBuildPlanMethod buildMethod;
	}
}
