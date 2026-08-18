using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x020000A4 RID: 164
	public class UnityContainer : IUnityContainer, IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000319 RID: 793 RVA: 0x00009B68 File Offset: 0x00007D68
		// (remove) Token: 0x0600031A RID: 794 RVA: 0x00009BA0 File Offset: 0x00007DA0
		private event EventHandler<RegisterEventArgs> Registering;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600031B RID: 795 RVA: 0x00009BD8 File Offset: 0x00007DD8
		// (remove) Token: 0x0600031C RID: 796 RVA: 0x00009C10 File Offset: 0x00007E10
		private event EventHandler<RegisterInstanceEventArgs> RegisteringInstance;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600031D RID: 797 RVA: 0x00009C48 File Offset: 0x00007E48
		// (remove) Token: 0x0600031E RID: 798 RVA: 0x00009C80 File Offset: 0x00007E80
		private event EventHandler<ChildContainerCreatedEventArgs> ChildContainerCreated;

		// Token: 0x0600031F RID: 799 RVA: 0x00009CB5 File Offset: 0x00007EB5
		public UnityContainer() : this(null)
		{
			this.AddExtension(new UnityDefaultStrategiesExtension());
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009CD0 File Offset: 0x00007ED0
		private UnityContainer(UnityContainer parent)
		{
			this.parent = parent;
			if (parent != null)
			{
				parent.lifetimeContainer.Add(this);
			}
			this.InitializeBuilderState();
			this.Registering += delegate(object param0, RegisterEventArgs param1)
			{
			};
			this.RegisteringInstance += delegate(object param0, RegisterInstanceEventArgs param1)
			{
			};
			this.ChildContainerCreated += delegate(object param0, ChildContainerCreatedEventArgs param1)
			{
			};
			this.AddExtension(new UnityDefaultBehaviorExtension());
			this.AddExtension(new InjectedMembers());
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009D80 File Offset: 0x00007F80
		public IUnityContainer RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(to, "to");
			Guard.ArgumentNotNull(injectionMembers, "injectionMembers");
			if (string.IsNullOrEmpty(name))
			{
				name = null;
			}
			if (from != null && !from.GetTypeInfo().IsGenericType && !to.GetTypeInfo().IsGenericType)
			{
				Guard.TypeIsAssignable(from, to, "from");
			}
			this.Registering(this, new RegisterEventArgs(from, to, name, lifetimeManager));
			if (injectionMembers.Length > 0)
			{
				this.ClearExistingBuildPlan(to, name);
				foreach (InjectionMember injectionMember in injectionMembers)
				{
					injectionMember.AddPolicies(from, to, name, this.policies);
				}
			}
			return this;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009E22 File Offset: 0x00008022
		public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
		{
			Guard.ArgumentNotNull(instance, "instance");
			Guard.ArgumentNotNull(lifetime, "lifetime");
			Guard.InstanceIsAssignable(t, instance, "instance");
			this.RegisteringInstance(this, new RegisterInstanceEventArgs(t, instance, name, lifetime));
			return this;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009E5E File Offset: 0x0000805E
		public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
		{
			return this.DoBuildUp(t, name, resolverOverrides);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009E69 File Offset: 0x00008069
		public IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(t, "t");
			return (IEnumerable<object>)this.Resolve(t.MakeArrayType(), resolverOverrides);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009E88 File Offset: 0x00008088
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Guard class is doing validation")]
		public object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(existing, "existing");
			Guard.InstanceIsAssignable(t, existing, "existing");
			return this.DoBuildUp(t, existing, name, resolverOverrides);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00009EAC File Offset: 0x000080AC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public void Teardown(object o)
		{
			IBuilderContext builderContext = null;
			try
			{
				Guard.ArgumentNotNull(o, "o");
				builderContext = new BuilderContext(this.GetStrategies().Reverse(), this.lifetimeContainer, this.policies, null, o);
				builderContext.Strategies.ExecuteTearDown(builderContext);
			}
			catch (Exception innerException)
			{
				throw new ResolutionFailedException(o.GetType(), null, innerException, builderContext);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00009F14 File Offset: 0x00008114
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public IUnityContainer AddExtension(UnityContainerExtension extension)
		{
			Guard.ArgumentNotNull(this.extensions, "extensions");
			this.extensions.Add(extension);
			extension.InitializeExtension(new UnityContainer.ExtensionContextImpl(this));
			lock (this.cachedStrategiesLock)
			{
				this.cachedStrategies = null;
			}
			return this;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009FA8 File Offset: 0x000081A8
		public object Configure(Type configurationInterface)
		{
			return (from ex in this.extensions
			where configurationInterface.GetTypeInfo().IsAssignableFrom(ex.GetType().GetTypeInfo())
			select ex).FirstOrDefault<UnityContainerExtension>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009FE0 File Offset: 0x000081E0
		public IUnityContainer RemoveAllExtensions()
		{
			List<UnityContainerExtension> list = new List<UnityContainerExtension>(this.extensions);
			list.Reverse();
			foreach (UnityContainerExtension unityContainerExtension in list)
			{
				unityContainerExtension.Remove();
				IDisposable disposable = unityContainerExtension as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.extensions.Clear();
			this.strategies.Clear();
			this.policies.ClearAll();
			this.registeredNames.Clear();
			return this;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000A07C File Offset: 0x0000827C
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Factory method that creates disposable object but does not own its lifetime.")]
		public IUnityContainer CreateChildContainer()
		{
			UnityContainer unityContainer = new UnityContainer(this);
			UnityContainer.ExtensionContextImpl childContext = new UnityContainer.ExtensionContextImpl(unityContainer);
			this.ChildContainerCreated(this, new ChildContainerCreatedEventArgs(childContext));
			return unityContainer;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000A0AA File Offset: 0x000082AA
		public IUnityContainer Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A0B2 File Offset: 0x000082B2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000A0CC File Offset: 0x000082CC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.lifetimeContainer != null)
				{
					this.lifetimeContainer.Dispose();
					this.lifetimeContainer = null;
					if (this.parent != null && this.parent.lifetimeContainer != null)
					{
						this.parent.lifetimeContainer.Remove(this);
					}
				}
				this.extensions.OfType<IDisposable>().ForEach(delegate(IDisposable ex)
				{
					ex.Dispose();
				});
				this.extensions.Clear();
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000A154 File Offset: 0x00008354
		private object DoBuildUp(Type t, string name, IEnumerable<ResolverOverride> resolverOverrides)
		{
			return this.DoBuildUp(t, null, name, resolverOverrides);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000A160 File Offset: 0x00008360
		private object DoBuildUp(Type t, object existing, string name, IEnumerable<ResolverOverride> resolverOverrides)
		{
			IBuilderContext builderContext = null;
			object result;
			try
			{
				builderContext = new BuilderContext(this.GetStrategies(), this.lifetimeContainer, this.policies, new NamedTypeBuildKey(t, name), existing);
				builderContext.AddResolverOverrides(resolverOverrides);
				if (t.GetTypeInfo().IsGenericTypeDefinition)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.CannotResolveOpenGenericType, new object[]
					{
						t.FullName
					}), "t");
				}
				result = builderContext.Strategies.ExecuteBuildUp(builderContext);
			}
			catch (Exception innerException)
			{
				throw new ResolutionFailedException(t, name, innerException, builderContext);
			}
			return result;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A1FC File Offset: 0x000083FC
		private IStrategyChain GetStrategies()
		{
			IStrategyChain strategyChain = this.cachedStrategies;
			if (strategyChain == null)
			{
				lock (this.cachedStrategiesLock)
				{
					if (this.cachedStrategies == null)
					{
						strategyChain = this.strategies.MakeStrategyChain();
						this.cachedStrategies = strategyChain;
					}
					else
					{
						strategyChain = this.cachedStrategies;
					}
				}
			}
			return strategyChain;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A268 File Offset: 0x00008468
		private void InitializeBuilderState()
		{
			this.registeredNames = new NamedTypesRegistry(this.ParentNameRegistry);
			this.extensions = new List<UnityContainerExtension>();
			this.lifetimeContainer = new LifetimeContainer();
			this.strategies = new StagedStrategyChain<UnityBuildStage>(this.ParentStrategies);
			this.buildPlanStrategies = new StagedStrategyChain<UnityBuildStage>(this.ParentBuildPlanStrategies);
			this.policies = new PolicyList(this.ParentPolicies);
			this.policies.Set(new RegisteredNamesPolicy(this.registeredNames), null);
			this.cachedStrategies = null;
			this.cachedStrategiesLock = new object();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000A2F8 File Offset: 0x000084F8
		private StagedStrategyChain<UnityBuildStage> ParentStrategies
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent.strategies;
				}
				return null;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000A30F File Offset: 0x0000850F
		private StagedStrategyChain<UnityBuildStage> ParentBuildPlanStrategies
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent.buildPlanStrategies;
				}
				return null;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000A326 File Offset: 0x00008526
		private PolicyList ParentPolicies
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent.policies;
				}
				return null;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A33D File Offset: 0x0000853D
		private NamedTypesRegistry ParentNameRegistry
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent.registeredNames;
				}
				return null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000A37C File Offset: 0x0000857C
		public IEnumerable<ContainerRegistration> Registrations
		{
			get
			{
				Dictionary<Type, List<string>> allRegisteredNames = new Dictionary<Type, List<string>>();
				this.FillTypeRegistrationDictionary(allRegisteredNames);
				return from type in allRegisteredNames.Keys
				from name in allRegisteredNames[type]
				select new ContainerRegistration(type, name, this.policies);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000A3D8 File Offset: 0x000085D8
		private void ClearExistingBuildPlan(Type typeToInject, string name)
		{
			NamedTypeBuildKey buildKey = new NamedTypeBuildKey(typeToInject, name);
			DependencyResolverTrackerPolicy.RemoveResolvers(this.policies, buildKey);
			this.policies.Set(new OverriddenBuildPlanMarkerPolicy(), buildKey);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A40C File Offset: 0x0000860C
		private void FillTypeRegistrationDictionary(IDictionary<Type, List<string>> typeRegistrations)
		{
			if (this.parent != null)
			{
				this.parent.FillTypeRegistrationDictionary(typeRegistrations);
			}
			foreach (Type type in this.registeredNames.RegisteredTypes)
			{
				if (!typeRegistrations.ContainsKey(type))
				{
					typeRegistrations[type] = new List<string>();
				}
				typeRegistrations[type] = typeRegistrations[type].Concat(this.registeredNames.GetKeys(type)).Distinct<string>().ToList<string>();
			}
		}

		// Token: 0x040000AD RID: 173
		private readonly UnityContainer parent;

		// Token: 0x040000AE RID: 174
		private LifetimeContainer lifetimeContainer;

		// Token: 0x040000AF RID: 175
		private StagedStrategyChain<UnityBuildStage> strategies;

		// Token: 0x040000B0 RID: 176
		private StagedStrategyChain<UnityBuildStage> buildPlanStrategies;

		// Token: 0x040000B1 RID: 177
		private PolicyList policies;

		// Token: 0x040000B2 RID: 178
		private NamedTypesRegistry registeredNames;

		// Token: 0x040000B3 RID: 179
		private List<UnityContainerExtension> extensions;

		// Token: 0x040000B4 RID: 180
		private IStrategyChain cachedStrategies;

		// Token: 0x040000B5 RID: 181
		private object cachedStrategiesLock;

		// Token: 0x020000A5 RID: 165
		private class ExtensionContextImpl : ExtensionContext
		{
			// Token: 0x0600033E RID: 830 RVA: 0x0000A4AC File Offset: 0x000086AC
			public ExtensionContextImpl(UnityContainer container)
			{
				this.container = container;
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A4BB File Offset: 0x000086BB
			public override IUnityContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A4C3 File Offset: 0x000086C3
			public override StagedStrategyChain<UnityBuildStage> Strategies
			{
				get
				{
					return this.container.strategies;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A4D0 File Offset: 0x000086D0
			public override StagedStrategyChain<UnityBuildStage> BuildPlanStrategies
			{
				get
				{
					return this.container.buildPlanStrategies;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x06000342 RID: 834 RVA: 0x0000A4DD File Offset: 0x000086DD
			public override IPolicyList Policies
			{
				get
				{
					return this.container.policies;
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A4EA File Offset: 0x000086EA
			public override ILifetimeContainer Lifetime
			{
				get
				{
					return this.container.lifetimeContainer;
				}
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0000A4F7 File Offset: 0x000086F7
			public override void RegisterNamedType(Type t, string name)
			{
				this.container.registeredNames.RegisterType(t, name);
			}

			// Token: 0x14000007 RID: 7
			// (add) Token: 0x06000345 RID: 837 RVA: 0x0000A50B File Offset: 0x0000870B
			// (remove) Token: 0x06000346 RID: 838 RVA: 0x0000A519 File Offset: 0x00008719
			public override event EventHandler<RegisterEventArgs> Registering
			{
				add
				{
					this.container.Registering += value;
				}
				remove
				{
					this.container.Registering -= value;
				}
			}

			// Token: 0x14000008 RID: 8
			// (add) Token: 0x06000347 RID: 839 RVA: 0x0000A527 File Offset: 0x00008727
			// (remove) Token: 0x06000348 RID: 840 RVA: 0x0000A535 File Offset: 0x00008735
			public override event EventHandler<RegisterInstanceEventArgs> RegisteringInstance
			{
				add
				{
					this.container.RegisteringInstance += value;
				}
				remove
				{
					this.container.RegisteringInstance -= value;
				}
			}

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x06000349 RID: 841 RVA: 0x0000A543 File Offset: 0x00008743
			// (remove) Token: 0x0600034A RID: 842 RVA: 0x0000A551 File Offset: 0x00008751
			public override event EventHandler<ChildContainerCreatedEventArgs> ChildContainerCreated
			{
				add
				{
					this.container.ChildContainerCreated += value;
				}
				remove
				{
					this.container.ChildContainerCreated -= value;
				}
			}

			// Token: 0x040000BD RID: 189
			private readonly UnityContainer container;
		}
	}
}
