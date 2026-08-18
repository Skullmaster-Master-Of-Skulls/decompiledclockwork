using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FA RID: 250
	public class LocalizedEntryCollection<T> : KeyedCollection<CultureInfo, T> where T : LocalizedEntry
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001AB7D File Offset: 0x00018D7D
		protected override CultureInfo GetKeyForItem(T item)
		{
			return item.Language;
		}
	}
}
