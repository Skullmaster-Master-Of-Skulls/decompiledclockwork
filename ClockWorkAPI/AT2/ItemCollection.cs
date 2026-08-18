using System;
using System.Collections;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI.AT2
{
	// Token: 0x02000057 RID: 87
	public class ItemCollection : CollectionBase
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00017556 File Offset: 0x00016556
		public ItemCollection()
		{
			this.deletedItems = new ArrayList();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001756C File Offset: 0x0001656C
		public void Remove(Item item)
		{
			Item item2 = null;
			foreach (object obj in base.List)
			{
				Item item3 = (Item)obj;
				if (item3 == item)
				{
					item2 = item3;
					break;
				}
			}
			if (item2 != null)
			{
				base.List.Remove(item2);
				if (item2.Status != ObjectStatus.New)
				{
					this.deletedItems.Add(item2);
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017614 File Offset: 0x00016614
		public int Add(Item item)
		{
			return base.List.Add(item);
		}

		// Token: 0x170001FF RID: 511
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

		// Token: 0x060004FC RID: 1276 RVA: 0x00017668 File Offset: 0x00016668
		public Item FindItem(int itemId)
		{
			foreach (object obj in base.List)
			{
				Item item = (Item)obj;
				if (item.ItemId == itemId)
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000176E4 File Offset: 0x000166E4
		public Item[] Find(string searchString, params string[] categoryParent)
		{
			string text = "";
			for (int i = 0; i < categoryParent.Length; i++)
			{
				if (categoryParent[i].Length > 0)
				{
					if (text.Length > 0)
					{
						text += ".";
					}
					text += categoryParent[i];
				}
			}
			ArrayList arrayList = new ArrayList();
			string value = searchString.ToLower().Trim();
			foreach (object obj in base.List)
			{
				Item item = (Item)obj;
				if (text.Length < 1 || item.Category.IndexOf(text) == 0)
				{
					if (item.Title.ToLower().Trim().IndexOf(value) >= 0)
					{
						arrayList.Add(value);
					}
				}
			}
			Item[] array = new Item[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (Item)arrayList[i];
			}
			return array;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00017848 File Offset: 0x00016848
		public void Load(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT itemid,category,title,vendor,cost,whoadded,dateadded,description FROM at2_lookupitem WHERE isactive='1' ORDER BY category,title";
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Item item = new Item(dr);
				this.Add(item);
			}
		}

		// Token: 0x040001D0 RID: 464
		private ArrayList deletedItems;
	}
}
