using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000071 RID: 113
	public class LifetimeStrategy : BuilderStrategy
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00006AB8 File Offset: 0x00004CB8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class.")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			if (context.Existing == null)
			{
				ILifetimePolicy lifetimePolicy = this.GetLifetimePolicy(context);
				IRequiresRecovery requiresRecovery = lifetimePolicy as IRequiresRecovery;
				if (requiresRecovery != null)
				{
					context.RecoveryStack.Add(requiresRecovery);
				}
				object value = lifetimePolicy.GetValue();
				if (value != null)
				{
					context.Existing = value;
					context.BuildComplete = true;
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006B10 File Offset: 0x00004D10
		public override void PostBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			ILifetimePolicy lifetimePolicy = this.GetLifetimePolicy(context);
			lifetimePolicy.SetValue(context.Existing);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006B3C File Offset: 0x00004D3C
		private ILifetimePolicy GetLifetimePolicy(IBuilderContext context)
		{
			ILifetimePolicy lifetimePolicy = context.Policies.GetNoDefault(context.BuildKey, false);
			if (lifetimePolicy == null && context.BuildKey.Type.GetTypeInfo().IsGenericType)
			{
				lifetimePolicy = this.GetLifetimePolicyForGenericType(context);
			}
			if (lifetimePolicy == null)
			{
				lifetimePolicy = new TransientLifetimeManager();
				context.PersistentPolicies.Set(lifetimePolicy, context.BuildKey);
			}
			return lifetimePolicy;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006B9C File Offset: 0x00004D9C
		private ILifetimePolicy GetLifetimePolicyForGenericType(IBuilderContext context)
		{
			Type type = context.BuildKey.Type;
			object buildKey = new NamedTypeBuildKey(type.GetGenericTypeDefinition(), context.BuildKey.Name);
			IPolicyList policies;
			ILifetimeFactoryPolicy lifetimeFactoryPolicy = context.Policies.Get(buildKey, out policies);
			if (lifetimeFactoryPolicy != null)
			{
				ILifetimePolicy lifetimePolicy = lifetimeFactoryPolicy.CreateLifetimePolicy();
				lock (this.genericLifetimeManagerLock)
				{
					ILifetimePolicy lifetimePolicy2 = policies.GetNoDefault(context.BuildKey, false);
					if (lifetimePolicy2 == null)
					{
						policies.Set(lifetimePolicy, context.BuildKey);
						lifetimePolicy2 = lifetimePolicy;
					}
					return lifetimePolicy2;
				}
			}
			return null;
		}

		// Token: 0x0400006B RID: 107
		private readonly object genericLifetimeManagerLock = new object();
	}
}
