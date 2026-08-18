using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000026 RID: 38
	public class HierarchicalLifetimeStrategy : BuilderStrategy
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003170 File Offset: 0x00001370
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Lifetime manager aligns with lifetime of container and is disposed when container is disposed.")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			IPolicyList objA;
			ILifetimePolicy lifetimePolicy = context.PersistentPolicies.Get(context.BuildKey, out objA);
			if (lifetimePolicy is HierarchicalLifetimeManager && !object.ReferenceEquals(objA, context.PersistentPolicies))
			{
				HierarchicalLifetimeManager hierarchicalLifetimeManager = new HierarchicalLifetimeManager
				{
					InUse = true
				};
				context.PersistentPolicies.Set(hierarchicalLifetimeManager, context.BuildKey);
				context.Lifetime.Add(hierarchicalLifetimeManager);
			}
		}
	}
}
