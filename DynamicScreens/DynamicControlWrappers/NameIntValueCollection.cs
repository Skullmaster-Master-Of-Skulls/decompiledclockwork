using System;
using System.Collections;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000060 RID: 96
	public class NameIntValueCollection : CollectionBase
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00041510 File Offset: 0x00040510
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00041528 File Offset: 0x00040528
		public NameIntValue SelectedItem
		{
			get
			{
				return this.selectedItem;
			}
			set
			{
				this.selectedItem = value;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00041534 File Offset: 0x00040534
		public override string ToString()
		{
			string result;
			if (this.selectedItem != null)
			{
				result = this.selectedItem.ToString();
			}
			else
			{
				result = "{all staff}";
			}
			return result;
		}

		// Token: 0x17000169 RID: 361
		public NameIntValue this[int index]
		{
			get
			{
				return (NameIntValue)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x1700016A RID: 362
		public NameIntValue this[string name]
		{
			get
			{
				return this.Find(name);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000415B8 File Offset: 0x000405B8
		public int Add(NameIntValue nameIntValue)
		{
			return base.List.Add(nameIntValue);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000415D8 File Offset: 0x000405D8
		public int Add(string name, int val)
		{
			NameIntValue value = new NameIntValue(name, val);
			return base.List.Add(value);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00041600 File Offset: 0x00040600
		private NameIntValue Find(string name)
		{
			string strB = name.ToLower();
			foreach (object obj in base.List)
			{
				NameIntValue nameIntValue = (NameIntValue)obj;
				if (nameIntValue.Name.ToLower().CompareTo(strB) == 0)
				{
					return nameIntValue;
				}
			}
			return null;
		}

		// Token: 0x0400037F RID: 895
		private NameIntValue selectedItem = null;
	}
}
