using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000049 RID: 73
	public class BuildKeyMappingPolicy : IBuildKeyMappingPolicy, IBuilderPolicy
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000439D File Offset: 0x0000259D
		public BuildKeyMappingPolicy(NamedTypeBuildKey newBuildKey)
		{
			this.newBuildKey = newBuildKey;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000043AC File Offset: 0x000025AC
		public NamedTypeBuildKey Map(NamedTypeBuildKey buildKey, IBuilderContext context)
		{
			return this.newBuildKey;
		}

		// Token: 0x04000041 RID: 65
		private readonly NamedTypeBuildKey newBuildKey;
	}
}
