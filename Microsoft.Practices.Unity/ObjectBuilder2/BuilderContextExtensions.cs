using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003B RID: 59
	public static class BuilderContextExtensions
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00003BB9 File Offset: 0x00001DB9
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		public static TResult NewBuildUp<TResult>(this IBuilderContext context)
		{
			return context.NewBuildUp(null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003BC2 File Offset: 0x00001DC2
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		public static TResult NewBuildUp<TResult>(this IBuilderContext context, string name)
		{
			Guard.ArgumentNotNull(context, "context");
			return (TResult)((object)context.NewBuildUp(NamedTypeBuildKey.Make<TResult>(name)));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static void AddResolverOverrides(this IBuilderContext context, params ResolverOverride[] overrides)
		{
			Guard.ArgumentNotNull(context, "context");
			context.AddResolverOverrides(overrides);
		}
	}
}
