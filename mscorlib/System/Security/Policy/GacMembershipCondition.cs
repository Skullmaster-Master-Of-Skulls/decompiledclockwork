using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x020004BF RID: 1215
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IConstantMembershipCondition, IReportMatchMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06003092 RID: 12434 RVA: 0x000A6848 File Offset: 0x000A5848
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000A6860 File Offset: 0x000A5860
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				if (obj is GacInstalled)
				{
					usedEvidence = obj;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000A689B File Offset: 0x000A589B
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000A68A2 File Offset: 0x000A58A2
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000A68AB File Offset: 0x000A58AB
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000A68B8 File Offset: 0x000A58B8
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000A68F8 File Offset: 0x000A58F8
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

		// Token: 0x06003099 RID: 12441 RVA: 0x000A692C File Offset: 0x000A592C
		public override bool Equals(object o)
		{
			return o is GacMembershipCondition;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000A6946 File Offset: 0x000A5946
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000A6949 File Offset: 0x000A5949
		public override string ToString()
		{
			return Environment.GetResourceString("GAC_ToString");
		}
	}
}
