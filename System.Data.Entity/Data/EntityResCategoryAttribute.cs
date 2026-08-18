using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00006112 File Offset: 0x00004312
		public EntityResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000611B File Offset: 0x0000431B
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
