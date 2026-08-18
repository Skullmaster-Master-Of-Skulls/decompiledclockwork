using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003F RID: 63
	public interface IPolicyList
	{
		// Token: 0x06000105 RID: 261
		void Clear(Type policyInterface, object buildKey);

		// Token: 0x06000106 RID: 262
		void ClearAll();

		// Token: 0x06000107 RID: 263
		void ClearDefault(Type policyInterface);

		// Token: 0x06000108 RID: 264
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		IBuilderPolicy Get(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList);

		// Token: 0x06000109 RID: 265
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Backwards compatibility with ObjectBuilder")]
		IBuilderPolicy GetNoDefault(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList);

		// Token: 0x0600010A RID: 266
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "Backwards compatibility with ObjectBuilder")]
		void Set(Type policyInterface, IBuilderPolicy policy, object buildKey);

		// Token: 0x0600010B RID: 267
		void SetDefault(Type policyInterface, IBuilderPolicy policy);
	}
}
