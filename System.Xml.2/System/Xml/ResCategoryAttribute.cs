using System;
using System.ComponentModel;

namespace System.Xml
{
	// Token: 0x0200012B RID: 299
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x000610E1 File Offset: 0x0005F2E1
		public ResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000610EA File Offset: 0x0005F2EA
		protected override string GetLocalizedString(string value)
		{
			return Res.GetString(value);
		}
	}
}
