using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000062 RID: 98
	internal class FactoryDelegateBuildPlanPolicy : IBuildPlanPolicy, IBuilderPolicy
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000634C File Offset: 0x0000454C
		public FactoryDelegateBuildPlanPolicy(Func<IUnityContainer, Type, string, object> factory)
		{
			this.factory = factory;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000635C File Offset: 0x0000455C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public void BuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			if (context.Existing == null)
			{
				IUnityContainer arg = context.NewBuildUp<IUnityContainer>();
				context.Existing = this.factory(arg, context.BuildKey.Type, context.BuildKey.Name);
				DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
			}
		}

		// Token: 0x04000064 RID: 100
		private readonly Func<IUnityContainer, Type, string, object> factory;
	}
}
