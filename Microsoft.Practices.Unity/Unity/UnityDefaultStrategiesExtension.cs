using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity
{
	// Token: 0x020000A8 RID: 168
	public class UnityDefaultStrategiesExtension : UnityContainerExtension
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000A800 File Offset: 0x00008A00
		protected override void Initialize()
		{
			base.Context.Strategies.AddNew<BuildKeyMappingStrategy>(UnityBuildStage.TypeMapping);
			base.Context.Strategies.AddNew<HierarchicalLifetimeStrategy>(UnityBuildStage.Lifetime);
			base.Context.Strategies.AddNew<LifetimeStrategy>(UnityBuildStage.Lifetime);
			base.Context.Strategies.AddNew<ArrayResolutionStrategy>(UnityBuildStage.Creation);
			base.Context.Strategies.AddNew<BuildPlanStrategy>(UnityBuildStage.Creation);
			base.Context.BuildPlanStrategies.AddNew<DynamicMethodConstructorStrategy>(UnityBuildStage.Creation);
			base.Context.BuildPlanStrategies.AddNew<DynamicMethodPropertySetterStrategy>(UnityBuildStage.Initialization);
			base.Context.BuildPlanStrategies.AddNew<DynamicMethodCallStrategy>(UnityBuildStage.Initialization);
			base.Context.Policies.SetDefault(new DefaultUnityConstructorSelectorPolicy());
			base.Context.Policies.SetDefault(new DefaultUnityPropertySelectorPolicy());
			base.Context.Policies.SetDefault(new DefaultUnityMethodSelectorPolicy());
			base.Context.Policies.SetDefault(new DynamicMethodBuildPlanCreatorPolicy(base.Context.BuildPlanStrategies));
			base.Context.Policies.Set(new DeferredResolveBuildPlanPolicy(), typeof(Func<>));
			base.Context.Policies.Set(new PerResolveLifetimeManager(), typeof(Func<>));
			base.Context.Policies.Set(new LazyDynamicMethodBuildPlanCreatorPolicy(), typeof(Lazy<>));
		}
	}
}
