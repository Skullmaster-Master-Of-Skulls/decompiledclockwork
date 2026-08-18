using System;

namespace System.Web.UI
{
	// Token: 0x02000323 RID: 803
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ValidationPropertyAttribute : Attribute
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x0007C774 File Offset: 0x0007A974
		public ValidationPropertyAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0007C783 File Offset: 0x0007A983
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04001D7A RID: 7546
		private readonly string name;
	}
}
