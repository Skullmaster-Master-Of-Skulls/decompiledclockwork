using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003E RID: 62
	internal class NonNullItemCollection<T> : Collection<T>
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00008BF0 File Offset: 0x00006DF0
		protected override void InsertItem(int index, T item)
		{
			if (item == null)
			{
				throw FxTrace.Exception.ArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008C12 File Offset: 0x00006E12
		protected override void SetItem(int index, T item)
		{
			if (item == null)
			{
				throw FxTrace.Exception.ArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
