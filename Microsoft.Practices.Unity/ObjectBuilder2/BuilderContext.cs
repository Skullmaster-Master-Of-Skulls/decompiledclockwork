using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000038 RID: 56
	public class BuilderContext : IBuilderContext
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003865 File Offset: 0x00001A65
		protected BuilderContext()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003878 File Offset: 0x00001A78
		public BuilderContext(IStrategyChain chain, ILifetimeContainer lifetime, IPolicyList policies, NamedTypeBuildKey originalBuildKey, object existing)
		{
			this.chain = chain;
			this.lifetime = lifetime;
			this.originalBuildKey = originalBuildKey;
			this.BuildKey = originalBuildKey;
			this.persistentPolicies = policies;
			this.policies = new PolicyList(this.persistentPolicies);
			this.Existing = existing;
			this.resolverOverrides = new CompositeResolverOverride();
			this.ownsOverrides = true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000038E8 File Offset: 0x00001AE8
		public BuilderContext(IStrategyChain chain, ILifetimeContainer lifetime, IPolicyList persistentPolicies, IPolicyList transientPolicies, NamedTypeBuildKey buildKey, object existing)
		{
			this.chain = chain;
			this.lifetime = lifetime;
			this.persistentPolicies = persistentPolicies;
			this.policies = transientPolicies;
			this.originalBuildKey = buildKey;
			this.BuildKey = buildKey;
			this.Existing = existing;
			this.resolverOverrides = new CompositeResolverOverride();
			this.ownsOverrides = true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003950 File Offset: 0x00001B50
		protected BuilderContext(IStrategyChain chain, ILifetimeContainer lifetime, IPolicyList persistentPolicies, IPolicyList transientPolicies, NamedTypeBuildKey buildKey, CompositeResolverOverride resolverOverrides)
		{
			this.chain = chain;
			this.lifetime = lifetime;
			this.persistentPolicies = persistentPolicies;
			this.policies = transientPolicies;
			this.originalBuildKey = buildKey;
			this.BuildKey = buildKey;
			this.Existing = null;
			this.resolverOverrides = resolverOverrides;
			this.ownsOverrides = false;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000039B1 File Offset: 0x00001BB1
		public IStrategyChain Strategies
		{
			get
			{
				return this.chain;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000039B9 File Offset: 0x00001BB9
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000039C1 File Offset: 0x00001BC1
		public NamedTypeBuildKey BuildKey { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000039CA File Offset: 0x00001BCA
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000039D2 File Offset: 0x00001BD2
		public object Existing { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000039DB File Offset: 0x00001BDB
		public ILifetimeContainer Lifetime
		{
			get
			{
				return this.lifetime;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000039E3 File Offset: 0x00001BE3
		public NamedTypeBuildKey OriginalBuildKey
		{
			get
			{
				return this.originalBuildKey;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000039EB File Offset: 0x00001BEB
		public IPolicyList PersistentPolicies
		{
			get
			{
				return this.persistentPolicies;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000039F3 File Offset: 0x00001BF3
		public IPolicyList Policies
		{
			get
			{
				return this.policies;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000039FB File Offset: 0x00001BFB
		public IRecoveryStack RecoveryStack
		{
			get
			{
				return this.recoveryStack;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003A03 File Offset: 0x00001C03
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003A0B File Offset: 0x00001C0B
		public bool BuildComplete { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003A14 File Offset: 0x00001C14
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003A1C File Offset: 0x00001C1C
		public object CurrentOperation { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003A25 File Offset: 0x00001C25
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003A2D File Offset: 0x00001C2D
		public IBuilderContext ChildContext { get; private set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00003A38 File Offset: 0x00001C38
		public void AddResolverOverrides(IEnumerable<ResolverOverride> newOverrides)
		{
			if (!this.ownsOverrides)
			{
				CompositeResolverOverride newOverrides2 = this.resolverOverrides;
				this.resolverOverrides = new CompositeResolverOverride();
				this.resolverOverrides.AddRange(newOverrides2);
				this.ownsOverrides = true;
			}
			this.resolverOverrides.AddRange(newOverrides);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003A7E File Offset: 0x00001C7E
		public IDependencyResolverPolicy GetOverriddenResolver(Type dependencyType)
		{
			return this.resolverOverrides.GetResolver(this, dependencyType);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003A90 File Offset: 0x00001C90
		public object NewBuildUp(NamedTypeBuildKey newBuildKey)
		{
			this.ChildContext = new BuilderContext(this.chain, this.lifetime, this.persistentPolicies, this.policies, newBuildKey, this.resolverOverrides);
			object result = this.ChildContext.Strategies.ExecuteBuildUp(this.ChildContext);
			this.ChildContext = null;
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003AE8 File Offset: 0x00001CE8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public object NewBuildUp(NamedTypeBuildKey newBuildKey, Action<IBuilderContext> childCustomizationBlock)
		{
			Guard.ArgumentNotNull(childCustomizationBlock, "childCustomizationBlock");
			this.ChildContext = new BuilderContext(this.chain, this.lifetime, this.persistentPolicies, this.policies, newBuildKey, this.resolverOverrides);
			childCustomizationBlock(this.ChildContext);
			object result = this.ChildContext.Strategies.ExecuteBuildUp(this.ChildContext);
			this.ChildContext = null;
			return result;
		}

		// Token: 0x04000026 RID: 38
		private readonly IStrategyChain chain;

		// Token: 0x04000027 RID: 39
		private readonly ILifetimeContainer lifetime;

		// Token: 0x04000028 RID: 40
		private readonly IRecoveryStack recoveryStack = new RecoveryStack();

		// Token: 0x04000029 RID: 41
		private readonly NamedTypeBuildKey originalBuildKey;

		// Token: 0x0400002A RID: 42
		private readonly IPolicyList persistentPolicies;

		// Token: 0x0400002B RID: 43
		private readonly IPolicyList policies;

		// Token: 0x0400002C RID: 44
		private CompositeResolverOverride resolverOverrides;

		// Token: 0x0400002D RID: 45
		private bool ownsOverrides;
	}
}
