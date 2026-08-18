using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000029 RID: 41
	public static class PolicyListExtensions
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000032B4 File Offset: 0x000014B4
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static void Clear<TPolicyInterface>(this IPolicyList policies, object buildKey) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			policies.Clear(typeof(TPolicyInterface), buildKey);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000032D2 File Offset: 0x000014D2
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static void ClearDefault<TPolicyInterface>(this IPolicyList policies) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			policies.ClearDefault(typeof(TPolicyInterface));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000032EF File Offset: 0x000014EF
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		public static TPolicyInterface Get<TPolicyInterface>(this IPolicyList policies, object buildKey) where TPolicyInterface : IBuilderPolicy
		{
			return (TPolicyInterface)((object)policies.Get(typeof(TPolicyInterface), buildKey, false));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003308 File Offset: 0x00001508
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		public static TPolicyInterface Get<TPolicyInterface>(this IPolicyList policies, object buildKey, out IPolicyList containingPolicyList) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			return (TPolicyInterface)((object)policies.Get(typeof(TPolicyInterface), buildKey, false, out containingPolicyList));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000332D File Offset: 0x0000152D
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Back compat with ObjectBuilder")]
		public static IBuilderPolicy Get(this IPolicyList policies, Type policyInterface, object buildKey)
		{
			return policies.Get(policyInterface, buildKey, false);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003338 File Offset: 0x00001538
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Returning the builder policy and populating the policy list")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static IBuilderPolicy Get(this IPolicyList policies, Type policyInterface, object buildKey, out IPolicyList containingPolicyList)
		{
			Guard.ArgumentNotNull(policies, "policies");
			return policies.Get(policyInterface, buildKey, false, out containingPolicyList);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000334F File Offset: 0x0000154F
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		public static TPolicyInterface Get<TPolicyInterface>(this IPolicyList policies, object buildKey, bool localOnly) where TPolicyInterface : IBuilderPolicy
		{
			return (TPolicyInterface)((object)policies.Get(typeof(TPolicyInterface), buildKey, localOnly));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003368 File Offset: 0x00001568
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Returning the builder policy and populating the policy list")]
		public static TPolicyInterface Get<TPolicyInterface>(this IPolicyList policies, object buildKey, bool localOnly, out IPolicyList containingPolicyList) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			return (TPolicyInterface)((object)policies.Get(typeof(TPolicyInterface), buildKey, localOnly, out containingPolicyList));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003390 File Offset: 0x00001590
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static IBuilderPolicy Get(this IPolicyList policies, Type policyInterface, object buildKey, bool localOnly)
		{
			Guard.ArgumentNotNull(policies, "policies");
			IPolicyList policyList;
			return policies.Get(policyInterface, buildKey, localOnly, out policyList);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000033B3 File Offset: 0x000015B3
		public static TPolicyInterface GetNoDefault<TPolicyInterface>(this IPolicyList policies, object buildKey, bool localOnly) where TPolicyInterface : IBuilderPolicy
		{
			return (TPolicyInterface)((object)policies.GetNoDefault(typeof(TPolicyInterface), buildKey, localOnly));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000033CC File Offset: 0x000015CC
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Returning the builder policy and populating the policy list")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static TPolicyInterface GetNoDefault<TPolicyInterface>(this IPolicyList policies, object buildKey, bool localOnly, out IPolicyList containingPolicyList) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			return (TPolicyInterface)((object)policies.GetNoDefault(typeof(TPolicyInterface), buildKey, localOnly, out containingPolicyList));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000033F4 File Offset: 0x000015F4
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static IBuilderPolicy GetNoDefault(this IPolicyList policies, Type policyInterface, object buildKey, bool localOnly)
		{
			Guard.ArgumentNotNull(policies, "policies");
			IPolicyList policyList;
			return policies.GetNoDefault(policyInterface, buildKey, localOnly, out policyList);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003417 File Offset: 0x00001617
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Checked with Guard class")]
		public static void Set<TPolicyInterface>(this IPolicyList policies, TPolicyInterface policy, object buildKey) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			policies.Set(typeof(TPolicyInterface), policy, buildKey);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000343B File Offset: 0x0000163B
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static void SetDefault<TPolicyInterface>(this IPolicyList policies, TPolicyInterface policy) where TPolicyInterface : IBuilderPolicy
		{
			Guard.ArgumentNotNull(policies, "policies");
			policies.SetDefault(typeof(TPolicyInterface), policy);
		}
	}
}
