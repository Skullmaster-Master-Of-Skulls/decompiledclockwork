using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000025 RID: 37
	public abstract class BuilderStrategy : IBuilderStrategy
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003160 File Offset: 0x00001360
		public virtual void PreBuildUp(IBuilderContext context)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003162 File Offset: 0x00001362
		public virtual void PostBuildUp(IBuilderContext context)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003164 File Offset: 0x00001364
		public virtual void PreTearDown(IBuilderContext context)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003166 File Offset: 0x00001366
		public virtual void PostTearDown(IBuilderContext context)
		{
		}
	}
}
