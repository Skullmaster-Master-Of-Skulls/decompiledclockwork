using System;
using System.ComponentModel;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core
{
	// Token: 0x0200039D RID: 925
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600217A RID: 8570 RVA: 0x0009D9AA File Offset: 0x0009BBAA
		public EntityResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0009D9B3 File Offset: 0x0009BBB3
		protected override string GetLocalizedString(string value)
		{
			return EntityRes.GetString(value);
		}
	}
}
