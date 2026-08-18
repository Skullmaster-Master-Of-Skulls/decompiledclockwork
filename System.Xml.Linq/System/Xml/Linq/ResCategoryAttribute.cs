using System;
using System.ComponentModel;

namespace System.Xml.Linq
{
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000B736 File Offset: 0x00009936
		public ResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B73F File Offset: 0x0000993F
		protected override string GetLocalizedString(string value)
		{
			return Res.GetString(value);
		}
	}
}
