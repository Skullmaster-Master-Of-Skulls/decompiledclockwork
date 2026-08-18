using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000045 RID: 69
	public class BuilderAwareStrategy : BuilderStrategy
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00004334 File Offset: 0x00002534
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			IBuilderAware builderAware = context.Existing as IBuilderAware;
			if (builderAware != null)
			{
				builderAware.OnBuiltUp(context.BuildKey);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004368 File Offset: 0x00002568
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class")]
		public override void PreTearDown(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			IBuilderAware builderAware = context.Existing as IBuilderAware;
			if (builderAware != null)
			{
				builderAware.OnTearingDown();
			}
		}
	}
}
