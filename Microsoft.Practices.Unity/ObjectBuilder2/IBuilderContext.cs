using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000037 RID: 55
	public interface IBuilderContext
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C0 RID: 192
		IStrategyChain Strategies { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C1 RID: 193
		ILifetimeContainer Lifetime { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C2 RID: 194
		NamedTypeBuildKey OriginalBuildKey { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		NamedTypeBuildKey BuildKey { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C5 RID: 197
		IPolicyList PersistentPolicies { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C6 RID: 198
		IPolicyList Policies { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C7 RID: 199
		IRecoveryStack RecoveryStack { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C8 RID: 200
		// (set) Token: 0x060000C9 RID: 201
		object Existing { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CA RID: 202
		// (set) Token: 0x060000CB RID: 203
		bool BuildComplete { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CD RID: 205
		object CurrentOperation { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CE RID: 206
		IBuilderContext ChildContext { get; }

		// Token: 0x060000CF RID: 207
		void AddResolverOverrides(IEnumerable<ResolverOverride> newOverrides);

		// Token: 0x060000D0 RID: 208
		IDependencyResolverPolicy GetOverriddenResolver(Type dependencyType);

		// Token: 0x060000D1 RID: 209
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		object NewBuildUp(NamedTypeBuildKey newBuildKey);

		// Token: 0x060000D2 RID: 210
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		object NewBuildUp(NamedTypeBuildKey newBuildKey, Action<IBuilderContext> childCustomizationBlock);
	}
}
