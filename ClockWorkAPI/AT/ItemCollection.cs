using System;
using System.Collections;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000070 RID: 112
	public class ItemCollection : CollectionBase
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001EA58 File Offset: 0x0001DA58
		public ItemCollection()
		{
			this.categories = new ItemCategoryCollection();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001EA70 File Offset: 0x0001DA70
		public int Add(Item item)
		{
			return base.List.Add(item);
		}

		// Token: 0x1700023D RID: 573
		public Item this[int index]
		{
			get
			{
				return (Item)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001EAC4 File Offset: 0x0001DAC4
		public ItemCategoryCollection Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001EADC File Offset: 0x0001DADC
		public void Parse(string text, out ItemCategory itemCategory, out Item item, out string unknownPortion)
		{
			string[] array = text.Split(new char[]
			{
				'.'
			});
			itemCategory = null;
			item = null;
			unknownPortion = null;
			string str = "";
			for (int i = 0; i < array.Length; i++)
			{
				string str2 = array[i];
				string text2 = str + ((i > 0) ? "." : "") + str2;
				ItemCategory itemCategory2 = this.categories.FindIterative(text2, false);
				if (itemCategory2 != null)
				{
					itemCategory = itemCategory2;
					str = text2;
				}
				else
				{
					unknownPortion = "";
					for (int j = i; j < array.Length; j++)
					{
						unknownPortion = unknownPortion + ((j > i) ? "." : "") + array[j];
					}
					if (unknownPortion.Length > 0 && unknownPortion.IndexOf('.') < 0)
					{
						item = null;
						foreach (object obj in base.List)
						{
							Item item2 = (Item)obj;
							if (item2.Title.ToLower().Trim().CompareTo(unknownPortion.Trim().ToLower()) == 0)
							{
								item = item2;
								unknownPortion = null;
							}
						}
					}
					else
					{
						item = null;
					}
				}
			}
		}

		// Token: 0x040002FD RID: 765
		private ItemCategoryCollection categories;
	}
}
