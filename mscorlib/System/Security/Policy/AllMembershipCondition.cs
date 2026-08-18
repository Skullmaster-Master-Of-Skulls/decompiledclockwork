using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000493 RID: 1171
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IConstantMembershipCondition, IReportMatchMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002E6A RID: 11882 RVA: 0x0009CB2C File Offset: 0x0009BB2C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0009CB44 File Offset: 0x0009BB44
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return true;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0009CB4A File Offset: 0x0009BB4A
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0009CB51 File Offset: 0x0009BB51
		public override string ToString()
		{
			return Environment.GetResourceString("All_ToString");
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0009CB5D File Offset: 0x0009BB5D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x0009CB66 File Offset: 0x0009BB66
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0009CB70 File Offset: 0x0009BB70
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.AllMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x0009CBAA File Offset: 0x0009BBAA
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x0009CBDC File Offset: 0x0009BBDC
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0009CBE7 File Offset: 0x0009BBE7
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}
	}
}
