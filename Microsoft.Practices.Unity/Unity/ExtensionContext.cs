using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007C RID: 124
	public abstract class ExtensionContext
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000229 RID: 553
		public abstract IUnityContainer Container { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600022A RID: 554
		public abstract StagedStrategyChain<UnityBuildStage> Strategies { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600022B RID: 555
		public abstract StagedStrategyChain<UnityBuildStage> BuildPlanStrategies { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600022C RID: 556
		public abstract IPolicyList Policies { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600022D RID: 557
		public abstract ILifetimeContainer Lifetime { get; }

		// Token: 0x0600022E RID: 558
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public abstract void RegisterNamedType(Type t, string name);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600022F RID: 559
		// (remove) Token: 0x06000230 RID: 560
		public abstract event EventHandler<RegisterEventArgs> Registering;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000231 RID: 561
		// (remove) Token: 0x06000232 RID: 562
		public abstract event EventHandler<RegisterInstanceEventArgs> RegisteringInstance;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000233 RID: 563
		// (remove) Token: 0x06000234 RID: 564
		public abstract event EventHandler<ChildContainerCreatedEventArgs> ChildContainerCreated;
	}
}
