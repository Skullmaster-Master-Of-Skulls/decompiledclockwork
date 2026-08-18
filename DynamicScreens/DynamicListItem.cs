using System;
using System.Data;

namespace DynamicScreens
{
	// Token: 0x0200003E RID: 62
	public class DynamicListItem
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000330B4 File Offset: 0x000320B4
		public ModificationType HowModified
		{
			get
			{
				return this.howModified;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000330CC File Offset: 0x000320CC
		public DynamicListItem()
		{
			this.lookupListId = DynamicListItem.newLookupListId--;
			this.lookupText = "New item";
			this.orderNum = 1000;
			this.lookupValue = "";
			this.visible = true;
			this.children = "";
			this.howModified = ModificationType.Added;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00033138 File Offset: 0x00032138
		public DynamicListItem(DataRow dr)
		{
			this.lookupListId = ((dr["lookuplistid"] == DBNull.Value) ? 0 : ((int)dr["lookuplistid"]));
			this.lookupText = ((dr["lookuptext"] == DBNull.Value) ? "" : ((string)dr["lookuptext"]));
			this.orderNum = ((dr["ordernum"] == DBNull.Value) ? 0 : ((int)dr["ordernum"]));
			this.lookupValue = ((dr["lookupvalue"] == DBNull.Value) ? "" : ((string)dr["lookupvalue"]));
			this.visible = (dr["visible"] == DBNull.Value || Convert.ToBoolean(dr["visible"]));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00033234 File Offset: 0x00032234
		public DynamicListItem(int lookupListId, string lookupText, int orderNum, string lookupValue, string children)
		{
			this.lookupListId = lookupListId;
			this.orderNum = orderNum;
			this.lookupText = lookupText;
			this.lookupValue = lookupValue;
			this.children = children;
			this.visible = true;
			this.howModified = ModificationType.Added;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00033284 File Offset: 0x00032284
		public int LookupListId
		{
			get
			{
				return this.lookupListId;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0003329C File Offset: 0x0003229C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000332B4 File Offset: 0x000322B4
		public string LookupText
		{
			get
			{
				return this.lookupText;
			}
			set
			{
				this.lookupText = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000332E0 File Offset: 0x000322E0
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000332F8 File Offset: 0x000322F8
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00033324 File Offset: 0x00032324
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0003333C File Offset: 0x0003233C
		public string LookupValue
		{
			get
			{
				return this.lookupValue;
			}
			set
			{
				this.lookupValue = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00033368 File Offset: 0x00032368
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00033380 File Offset: 0x00032380
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000333AC File Offset: 0x000323AC
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000333C4 File Offset: 0x000323C4
		public string Children
		{
			get
			{
				return this.children;
			}
			set
			{
				this.children = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000333F0 File Offset: 0x000323F0
		public override string ToString()
		{
			return this.lookupText;
		}

		// Token: 0x0400028A RID: 650
		private int lookupListId;

		// Token: 0x0400028B RID: 651
		private string lookupText;

		// Token: 0x0400028C RID: 652
		private int orderNum;

		// Token: 0x0400028D RID: 653
		private string lookupValue;

		// Token: 0x0400028E RID: 654
		private bool visible;

		// Token: 0x0400028F RID: 655
		private string children;

		// Token: 0x04000290 RID: 656
		private ModificationType howModified = ModificationType.Unchanged;

		// Token: 0x04000291 RID: 657
		private static int newLookupListId = -1;
	}
}
