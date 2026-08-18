using System;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000066 RID: 102
	public class SelectedMethod : SelectedMemberWithParameters<MethodInfo>
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000664B File Offset: 0x0000484B
		public SelectedMethod(MethodInfo method) : base(method)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006654 File Offset: 0x00004854
		public MethodInfo Method
		{
			get
			{
				return base.MemberInfo;
			}
		}
	}
}
