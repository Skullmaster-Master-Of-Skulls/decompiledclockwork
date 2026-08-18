using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000109 RID: 265
	internal class AbsoluteUriCollection : Collection<Uri>
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001F300 File Offset: 0x0001D500
		protected override void InsertItem(int index, Uri item)
		{
			if (null == item || !item.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item", SR.GetString("ID0013"));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001F335 File Offset: 0x0001D535
		protected override void SetItem(int index, Uri item)
		{
			if (null == item || !item.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item", SR.GetString("ID0013"));
			}
			base.SetItem(index, item);
		}
	}
}
