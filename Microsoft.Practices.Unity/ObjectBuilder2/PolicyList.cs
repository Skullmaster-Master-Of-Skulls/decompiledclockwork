using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000040 RID: 64
	public class PolicyList : IPolicyList
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00003E07 File Offset: 0x00002007
		public PolicyList() : this(null)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003E10 File Offset: 0x00002010
		public PolicyList(IPolicyList innerPolicyList)
		{
			this.innerPolicyList = (innerPolicyList ?? new PolicyList.NullPolicyList());
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003E43 File Offset: 0x00002043
		public int Count
		{
			get
			{
				return this.policies.Count;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003E50 File Offset: 0x00002050
		public void Clear(Type policyInterface, object buildKey)
		{
			lock (this.lockObject)
			{
				Dictionary<PolicyList.PolicyKey, IBuilderPolicy> dictionary = this.ClonePolicies();
				dictionary.Remove(new PolicyList.PolicyKey(policyInterface, buildKey));
				this.policies = dictionary;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003EA8 File Offset: 0x000020A8
		public void ClearAll()
		{
			lock (this.lockObject)
			{
				this.policies = new Dictionary<PolicyList.PolicyKey, IBuilderPolicy>(PolicyList.PolicyKeyEqualityComparer.Default);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003EF4 File Offset: 0x000020F4
		public void ClearDefault(Type policyInterface)
		{
			this.Clear(policyInterface, null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003F00 File Offset: 0x00002100
		public IBuilderPolicy Get(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList)
		{
			Type buildType;
			PolicyList.TryGetType(buildKey, out buildType);
			IBuilderPolicy result;
			if ((result = this.GetPolicyForKey(policyInterface, buildKey, localOnly, out containingPolicyList)) == null && (result = this.GetPolicyForOpenGenericKey(policyInterface, buildKey, buildType, localOnly, out containingPolicyList)) == null && (result = this.GetPolicyForType(policyInterface, buildType, localOnly, out containingPolicyList)) == null)
			{
				result = (this.GetPolicyForOpenGenericType(policyInterface, buildType, localOnly, out containingPolicyList) ?? this.GetDefaultForPolicy(policyInterface, localOnly, out containingPolicyList));
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003F5D File Offset: 0x0000215D
		private IBuilderPolicy GetPolicyForKey(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList)
		{
			if (buildKey != null)
			{
				return this.GetNoDefault(policyInterface, buildKey, localOnly, out containingPolicyList);
			}
			containingPolicyList = null;
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003F73 File Offset: 0x00002173
		private IBuilderPolicy GetPolicyForOpenGenericKey(Type policyInterface, object buildKey, Type buildType, bool localOnly, out IPolicyList containingPolicyList)
		{
			if (buildType != null && buildType.GetTypeInfo().IsGenericType)
			{
				return this.GetNoDefault(policyInterface, PolicyList.ReplaceType(buildKey, buildType.GetGenericTypeDefinition()), localOnly, out containingPolicyList);
			}
			containingPolicyList = null;
			return null;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003FA2 File Offset: 0x000021A2
		private IBuilderPolicy GetPolicyForType(Type policyInterface, Type buildType, bool localOnly, out IPolicyList containingPolicyList)
		{
			if (buildType != null)
			{
				return this.GetNoDefault(policyInterface, buildType, localOnly, out containingPolicyList);
			}
			containingPolicyList = null;
			return null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003FB8 File Offset: 0x000021B8
		private IBuilderPolicy GetPolicyForOpenGenericType(Type policyInterface, Type buildType, bool localOnly, out IPolicyList containingPolicyList)
		{
			if (buildType != null && buildType.GetTypeInfo().IsGenericType)
			{
				return this.GetNoDefault(policyInterface, buildType.GetGenericTypeDefinition(), localOnly, out containingPolicyList);
			}
			containingPolicyList = null;
			return null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003FE0 File Offset: 0x000021E0
		private IBuilderPolicy GetDefaultForPolicy(Type policyInterface, bool localOnly, out IPolicyList containingPolicyList)
		{
			return this.GetNoDefault(policyInterface, null, localOnly, out containingPolicyList);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003FEC File Offset: 0x000021EC
		public IBuilderPolicy GetNoDefault(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList)
		{
			containingPolicyList = null;
			IBuilderPolicy result;
			if (this.policies.TryGetValue(new PolicyList.PolicyKey(policyInterface, buildKey), out result))
			{
				containingPolicyList = this;
				return result;
			}
			if (localOnly)
			{
				return null;
			}
			return this.innerPolicyList.GetNoDefault(policyInterface, buildKey, false, out containingPolicyList);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004030 File Offset: 0x00002230
		public void Set(Type policyInterface, IBuilderPolicy policy, object buildKey)
		{
			lock (this.lockObject)
			{
				Dictionary<PolicyList.PolicyKey, IBuilderPolicy> dictionary = this.ClonePolicies();
				dictionary[new PolicyList.PolicyKey(policyInterface, buildKey)] = policy;
				this.policies = dictionary;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004088 File Offset: 0x00002288
		public void SetDefault(Type policyInterface, IBuilderPolicy policy)
		{
			this.Set(policyInterface, policy, null);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004093 File Offset: 0x00002293
		private Dictionary<PolicyList.PolicyKey, IBuilderPolicy> ClonePolicies()
		{
			return new Dictionary<PolicyList.PolicyKey, IBuilderPolicy>(this.policies, PolicyList.PolicyKeyEqualityComparer.Default);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000040A8 File Offset: 0x000022A8
		private static bool TryGetType(object buildKey, out Type type)
		{
			type = (buildKey as Type);
			if (type == null)
			{
				NamedTypeBuildKey namedTypeBuildKey = buildKey as NamedTypeBuildKey;
				if (namedTypeBuildKey != null)
				{
					type = namedTypeBuildKey.Type;
				}
			}
			return type != null;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000040E4 File Offset: 0x000022E4
		private static object ReplaceType(object buildKey, Type newType)
		{
			Type type = buildKey as Type;
			if (type != null)
			{
				return newType;
			}
			NamedTypeBuildKey namedTypeBuildKey = buildKey as NamedTypeBuildKey;
			if (namedTypeBuildKey != null)
			{
				return new NamedTypeBuildKey(newType, namedTypeBuildKey.Name);
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.CannotExtractTypeFromBuildKey, new object[]
			{
				buildKey
			}), "buildKey");
		}

		// Token: 0x04000034 RID: 52
		private readonly IPolicyList innerPolicyList;

		// Token: 0x04000035 RID: 53
		private readonly object lockObject = new object();

		// Token: 0x04000036 RID: 54
		private Dictionary<PolicyList.PolicyKey, IBuilderPolicy> policies = new Dictionary<PolicyList.PolicyKey, IBuilderPolicy>(PolicyList.PolicyKeyEqualityComparer.Default);

		// Token: 0x02000041 RID: 65
		private class NullPolicyList : IPolicyList
		{
			// Token: 0x0600011E RID: 286 RVA: 0x0000413F File Offset: 0x0000233F
			public void Clear(Type policyInterface, object buildKey)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00004146 File Offset: 0x00002346
			public void ClearAll()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000120 RID: 288 RVA: 0x0000414D File Offset: 0x0000234D
			public void ClearDefault(Type policyInterface)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00004154 File Offset: 0x00002354
			public IBuilderPolicy Get(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList)
			{
				containingPolicyList = null;
				return null;
			}

			// Token: 0x06000122 RID: 290 RVA: 0x0000415B File Offset: 0x0000235B
			public IBuilderPolicy GetNoDefault(Type policyInterface, object buildKey, bool localOnly, out IPolicyList containingPolicyList)
			{
				containingPolicyList = null;
				return null;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004162 File Offset: 0x00002362
			public void Set(Type policyInterface, IBuilderPolicy policy, object buildKey)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00004169 File Offset: 0x00002369
			public void SetDefault(Type policyInterface, IBuilderPolicy policy)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x02000042 RID: 66
		private struct PolicyKey
		{
			// Token: 0x06000126 RID: 294 RVA: 0x00004178 File Offset: 0x00002378
			public PolicyKey(Type policyType, object buildKey)
			{
				this.PolicyType = policyType;
				this.BuildKey = buildKey;
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00004188 File Offset: 0x00002388
			public override bool Equals(object obj)
			{
				return obj != null && obj.GetType() == typeof(PolicyList.PolicyKey) && this == (PolicyList.PolicyKey)obj;
			}

			// Token: 0x06000128 RID: 296 RVA: 0x000041B2 File Offset: 0x000023B2
			public override int GetHashCode()
			{
				return PolicyList.PolicyKey.SafeGetHashCode(this.PolicyType) * 37 + PolicyList.PolicyKey.SafeGetHashCode(this.BuildKey);
			}

			// Token: 0x06000129 RID: 297 RVA: 0x000041CE File Offset: 0x000023CE
			public static bool operator ==(PolicyList.PolicyKey left, PolicyList.PolicyKey right)
			{
				return left.PolicyType == right.PolicyType && object.Equals(left.BuildKey, right.BuildKey);
			}

			// Token: 0x0600012A RID: 298 RVA: 0x000041F5 File Offset: 0x000023F5
			public static bool operator !=(PolicyList.PolicyKey left, PolicyList.PolicyKey right)
			{
				return !(left == right);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00004201 File Offset: 0x00002401
			private static int SafeGetHashCode(object obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			// Token: 0x04000037 RID: 55
			public readonly object BuildKey;

			// Token: 0x04000038 RID: 56
			public readonly Type PolicyType;
		}

		// Token: 0x02000043 RID: 67
		private class PolicyKeyEqualityComparer : IEqualityComparer<PolicyList.PolicyKey>
		{
			// Token: 0x0600012C RID: 300 RVA: 0x0000420E File Offset: 0x0000240E
			public bool Equals(PolicyList.PolicyKey x, PolicyList.PolicyKey y)
			{
				return x.PolicyType == y.PolicyType && object.Equals(x.BuildKey, y.BuildKey);
			}

			// Token: 0x0600012D RID: 301 RVA: 0x00004235 File Offset: 0x00002435
			public int GetHashCode(PolicyList.PolicyKey obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x04000039 RID: 57
			public static readonly PolicyList.PolicyKeyEqualityComparer Default = new PolicyList.PolicyKeyEqualityComparer();
		}
	}
}
