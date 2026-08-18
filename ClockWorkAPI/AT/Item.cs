using System;
using System.Text;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000056 RID: 86
	public class Item
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00017390 File Offset: 0x00016390
		public ItemCategoryCollection Categories
		{
			get
			{
				return this.categories;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000173A8 File Offset: 0x000163A8
		public Item()
		{
			this.vendorItems = new VendorItemCollection();
			this.categories = new ItemCategoryCollection();
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000173D0 File Offset: 0x000163D0
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x000173E8 File Offset: 0x000163E8
		public ObjectStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000173F4 File Offset: 0x000163F4
		private void SetChanged()
		{
			if (this.status != ObjectStatus.New)
			{
				this.status = ObjectStatus.Modified;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00017418 File Offset: 0x00016418
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00017430 File Offset: 0x00016430
		public string Title
		{
			get
			{
				return this.itemTitle;
			}
			set
			{
				this.itemTitle = value;
				this.SetChanged();
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00017444 File Offset: 0x00016444
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0001745C File Offset: 0x0001645C
		public string Description
		{
			get
			{
				return this.itemDescription;
			}
			set
			{
				this.itemDescription = value;
				this.SetChanged();
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00017470 File Offset: 0x00016470
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00017488 File Offset: 0x00016488
		public string Note
		{
			get
			{
				return this.itemNote;
			}
			set
			{
				this.itemNote = value;
				this.SetChanged();
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001749C File Offset: 0x0001649C
		public VendorItemCollection VendorItems
		{
			get
			{
				return this.vendorItems;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000174B4 File Offset: 0x000164B4
		public void SetDetails(string title, string description, string note)
		{
			this.itemTitle = title;
			this.itemDescription = description;
			this.itemNote = note;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000174CC File Offset: 0x000164CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.itemTitle);
			stringBuilder.AppendLine("Description: " + this.itemDescription);
			stringBuilder.AppendLine("Note: " + this.itemNote);
			stringBuilder.AppendLine("VendorItems: " + this.vendorItems.ToString());
			stringBuilder.AppendLine("Categories: " + this.categories.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040001CA RID: 458
		private ObjectStatus status = ObjectStatus.Unknown;

		// Token: 0x040001CB RID: 459
		private string itemTitle;

		// Token: 0x040001CC RID: 460
		private string itemDescription;

		// Token: 0x040001CD RID: 461
		private string itemNote;

		// Token: 0x040001CE RID: 462
		private VendorItemCollection vendorItems;

		// Token: 0x040001CF RID: 463
		private ItemCategoryCollection categories;
	}
}
