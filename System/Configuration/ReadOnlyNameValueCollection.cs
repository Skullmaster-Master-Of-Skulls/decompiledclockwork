using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000703 RID: 1795
	internal class ReadOnlyNameValueCollection : NameValueCollection
	{
		// Token: 0x06003756 RID: 14166 RVA: 0x000EB492 File Offset: 0x000EA492
		internal ReadOnlyNameValueCollection(IEqualityComparer equalityComparer) : base(equalityComparer)
		{
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000EB49B File Offset: 0x000EA49B
		internal ReadOnlyNameValueCollection(ReadOnlyNameValueCollection value) : base(value)
		{
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000EB4A4 File Offset: 0x000EA4A4
		internal void SetReadOnly()
		{
			base.IsReadOnly = true;
		}
	}
}
