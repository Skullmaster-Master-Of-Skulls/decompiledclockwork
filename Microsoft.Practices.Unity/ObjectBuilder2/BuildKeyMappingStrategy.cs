using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200004A RID: 74
	public class BuildKeyMappingStrategy : BuilderStrategy
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000043B4 File Offset: 0x000025B4
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			IBuildKeyMappingPolicy buildKeyMappingPolicy = context.Policies.Get(context.BuildKey);
			if (buildKeyMappingPolicy != null)
			{
				context.BuildKey = buildKeyMappingPolicy.Map(context.BuildKey, context);
			}
		}
	}
}
