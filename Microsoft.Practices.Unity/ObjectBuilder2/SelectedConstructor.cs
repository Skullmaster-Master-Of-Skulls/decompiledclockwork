using System;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000053 RID: 83
	public class SelectedConstructor : SelectedMemberWithParameters<ConstructorInfo>
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000480A File Offset: 0x00002A0A
		public SelectedConstructor(ConstructorInfo constructor) : base(constructor)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004813 File Offset: 0x00002A13
		public ConstructorInfo Constructor
		{
			get
			{
				return base.MemberInfo;
			}
		}
	}
}
