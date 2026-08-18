using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000718 RID: 1816
	public static class ObservableCollectionExtensions
	{
		// Token: 0x06004977 RID: 18807 RVA: 0x0015F848 File Offset: 0x0015DA48
		public static BindingList<T> ToBindingList<T>(this ObservableCollection<T> source) where T : class
		{
			Check.NotNull<ObservableCollection<T>>(source, "source");
			DbLocalView<T> dbLocalView = source as DbLocalView<T>;
			if (dbLocalView == null)
			{
				return new ObservableBackedBindingList<T>(source);
			}
			return dbLocalView.BindingList;
		}
	}
}
