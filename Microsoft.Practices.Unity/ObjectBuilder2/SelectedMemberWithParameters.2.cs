using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000052 RID: 82
	public class SelectedMemberWithParameters<TMemberInfoType> : SelectedMemberWithParameters
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000047F3 File Offset: 0x000029F3
		protected SelectedMemberWithParameters(TMemberInfoType memberInfo)
		{
			this.memberInfo = memberInfo;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004802 File Offset: 0x00002A02
		protected TMemberInfoType MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x04000045 RID: 69
		private TMemberInfoType memberInfo;
	}
}
