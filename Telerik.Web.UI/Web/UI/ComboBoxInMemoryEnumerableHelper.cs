using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000A42 RID: 2626
	internal class ComboBoxInMemoryEnumerableHelper : ComboBoxEnumerableHelper
	{
		// Token: 0x0600645C RID: 25692 RVA: 0x00178888 File Offset: 0x00176A88
		public override int GetCount(IEnumerable source)
		{
			ICollection collection = source as ICollection;
			if (collection != null)
			{
				return collection.Count;
			}
			Array array = source as Array;
			if (array != null)
			{
				return array.Length;
			}
			int num = 0;
			foreach (object obj in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x00178BD4 File Offset: 0x00176DD4
		public override IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize)
		{
			startIndex = Math.Max(startIndex, 0);
			if (enumerable is IList)
			{
				IList list = (IList)enumerable;
				int itemCounter = 0;
				for (int i = startIndex; i < list.Count; i++)
				{
					yield return list[i];
					itemCounter++;
					if (pageSize == itemCounter)
					{
						break;
					}
				}
			}
			else
			{
				int index = 0;
				foreach (object item in enumerable)
				{
					if (index < startIndex)
					{
						index++;
					}
					else
					{
						yield return item;
						index++;
						if (pageSize + startIndex == index)
						{
							yield break;
						}
					}
				}
			}
			yield break;
		}
	}
}
