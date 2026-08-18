using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020004B9 RID: 1209
	[ComVisible(true)]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup, IUnionSemanticCodeGroup
	{
		// Token: 0x0600303D RID: 12349 RVA: 0x000A5A7E File Offset: 0x000A4A7E
		internal UnionCodeGroup()
		{
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000A5A86 File Offset: 0x000A4A86
		internal UnionCodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet) : base(membershipCondition, permSet)
		{
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000A5A90 File Offset: 0x000A4A90
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy) : base(membershipCondition, policy)
		{
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000A5A9C File Offset: 0x000A4A9C
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			object obj = null;
			if (PolicyManager.CheckMembershipCondition(base.MembershipCondition, evidence, out obj))
			{
				PolicyStatement policyStatement = base.PolicyStatement;
				IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
				bool flag = delayEvaluatedEvidence != null && !delayEvaluatedEvidence.IsVerified;
				if (flag)
				{
					policyStatement.AddDependentEvidence(delayEvaluatedEvidence);
				}
				bool flag2 = false;
				IEnumerator enumerator = base.Children.GetEnumerator();
				while (enumerator.MoveNext() && !flag2)
				{
					PolicyStatement policyStatement2 = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
					if (policyStatement2 != null)
					{
						policyStatement.InplaceUnion(policyStatement2);
						if ((policyStatement2.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
						{
							flag2 = true;
						}
					}
				}
				return policyStatement;
			}
			return null;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000A5B42 File Offset: 0x000A4B42
		PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				return base.PolicyStatement;
			}
			return null;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000A5B68 File Offset: 0x000A4B68
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				CodeGroup codeGroup = this.Copy();
				codeGroup.Children = new ArrayList();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
				return codeGroup;
			}
			return null;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000A5BD8 File Offset: 0x000A4BD8
		public override CodeGroup Copy()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.MembershipCondition = base.MembershipCondition;
			unionCodeGroup.PolicyStatement = base.PolicyStatement;
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				unionCodeGroup.AddChild((CodeGroup)obj);
			}
			return unionCodeGroup;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000A5C43 File Offset: 0x000A4C43
		public override string MergeLogic
		{
			get
			{
				return Environment.GetResourceString("MergeLogic_Union");
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000A5C4F File Offset: 0x000A4C4F
		internal override string GetTypeName()
		{
			return "System.Security.Policy.UnionCodeGroup";
		}
	}
}
