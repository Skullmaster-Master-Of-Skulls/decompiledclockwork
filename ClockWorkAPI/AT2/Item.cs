using System;
using System.Data;
using System.Text;
using UnivOleDb;

namespace ClockWorkAPI.AT2
{
	// Token: 0x0200001C RID: 28
	public class Item
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006E2C File Offset: 0x00005E2C
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00006E54 File Offset: 0x00005E54
		public string Category
		{
			get
			{
				return (this.category == null) ? "" : this.category;
			}
			set
			{
				if (this.category.CompareTo(value) != 0)
				{
					this.category = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006E88 File Offset: 0x00005E88
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006EA0 File Offset: 0x00005EA0
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (this.title.CompareTo(value) != 0)
				{
					this.title = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006ED4 File Offset: 0x00005ED4
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006EEC File Offset: 0x00005EEC
		public string Vendor
		{
			get
			{
				return this.vendor;
			}
			set
			{
				if (this.vendor.CompareTo(value) != 0)
				{
					this.vendor = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006F20 File Offset: 0x00005F20
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006F38 File Offset: 0x00005F38
		public decimal Cost
		{
			get
			{
				return this.cost;
			}
			set
			{
				if (this.cost != value)
				{
					this.cost = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006F6C File Offset: 0x00005F6C
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006F84 File Offset: 0x00005F84
		public int WhoAdded
		{
			get
			{
				return this.whoAdded;
			}
			set
			{
				if (this.whoAdded != value)
				{
					this.whoAdded = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006FB0 File Offset: 0x00005FB0
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006FC8 File Offset: 0x00005FC8
		public DateTime DateAdded
		{
			get
			{
				return this.dateAdded;
			}
			set
			{
				if (this.dateAdded != value)
				{
					this.dateAdded = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006FFC File Offset: 0x00005FFC
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007014 File Offset: 0x00006014
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				if (this.description.CompareTo(value) != 0)
				{
					this.description = value;
					this.SetModifiedStatus();
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007048 File Offset: 0x00006048
		public int ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00007060 File Offset: 0x00006060
		public string[] Categories
		{
			get
			{
				return this.category.Split(new char[]
				{
					'.'
				});
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000708C File Offset: 0x0000608C
		private void SetModifiedStatus()
		{
			if (this.status != ObjectStatus.New)
			{
				this.status = ObjectStatus.Modified;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000070B0 File Offset: 0x000060B0
		public Item()
		{
			this.category = "";
			this.status = ObjectStatus.Unmodified;
			this.title = "";
			this.vendor = "";
			this.whoAdded = 0;
			this.cost = 0m;
			this.dateAdded = DateTime.Now;
			this.description = "";
			this.itemId = 0;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007120 File Offset: 0x00006120
		public Item(DataRow dr)
		{
			this.category = dr["category"].ToString();
			this.title = dr["title"].ToString();
			this.vendor = dr["vendor"].ToString();
			this.whoAdded = ((dr["whoadded"] == DBNull.Value) ? 0 : ((int)dr["whoadded"]));
			this.cost = (decimal)dr["cost"];
			this.dateAdded = (DateTime)dr["dateadded"];
			this.description = (string)dr["description"];
			this.status = ObjectStatus.Unmodified;
			this.itemId = (int)dr["itemid"];
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007202 File Offset: 0x00006202
		public void AcceptChanges()
		{
			this.status = ObjectStatus.Unmodified;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000720C File Offset: 0x0000620C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007224 File Offset: 0x00006224
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

		// Token: 0x06000103 RID: 259 RVA: 0x00007230 File Offset: 0x00006230
		public static Item CreateNew(string category, string title, string vendor, decimal cost, int whoAdded)
		{
			return new Item
			{
				Category = category,
				Title = title,
				Vendor = vendor,
				Cost = cost,
				WhoAdded = whoAdded,
				DateAdded = DateTime.Now,
				Description = "",
				Status = ObjectStatus.New
			};
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007294 File Offset: 0x00006294
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.title);
			stringBuilder.Append(" [");
			stringBuilder.Append(this.vendor);
			stringBuilder.Append(" | ");
			stringBuilder.Append(this.cost.ToString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007304 File Offset: 0x00006304
		public void WriteAsNewToDatabase(UnivDataAdapter da, int whoAdded)
		{
			da.SelectCommand.CommandText = "INSERT INTO at2_lookupitem (category,title,vendor,cost,whoadded,dateadded,description,isactive) VALUES (@category,@title,@vendor,@cost,@whoadded,getdate(),@description,1)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@category", this.category);
			da.SelectCommand.Parameters.Add("@title", this.title);
			da.SelectCommand.Parameters.Add("@vendor", this.vendor);
			da.SelectCommand.Parameters.Add("@cost", this.cost);
			da.SelectCommand.Parameters.Add("@whoadded", whoAdded);
			da.SelectCommand.Parameters.Add("@description", this.description);
			DataTable dataTable = new DataTable();
			this.itemId = da.FillReturnIdentity(dataTable, "itemid", "at2_lookupitem");
			this.status = ObjectStatus.Unmodified;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007408 File Offset: 0x00006408
		public void WriteAsUpdateToDatabase(UnivDataAdapter da, int whoModified)
		{
			da.SelectCommand.CommandText = "UPDATE at2_lookupitem SET category=@category,title=@title,vendor=@vendor,cost=@cost,description=@description WHERE itemid=@itemid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@category", this.category);
			da.SelectCommand.Parameters.Add("@title", this.title);
			da.SelectCommand.Parameters.Add("@vendor", this.vendor);
			da.SelectCommand.Parameters.Add("@cost", this.cost);
			da.SelectCommand.Parameters.Add("@description", this.description);
			da.SelectCommand.Parameters.Add("@itemid", this.itemId);
			da.Fill(new DataTable());
			this.status = ObjectStatus.Unmodified;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007500 File Offset: 0x00006500
		public void WriteAsDeleteToDatabase(UnivDataAdapter da, int whoDeleted)
		{
			da.SelectCommand.CommandText = "DELETE FROM at2_lookupitem WHERE itemid=@itemid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@itemid", this.itemId);
			da.Fill(new DataTable());
			this.status = ObjectStatus.Unknown;
		}

		// Token: 0x0400008C RID: 140
		private string category;

		// Token: 0x0400008D RID: 141
		private string title;

		// Token: 0x0400008E RID: 142
		private string vendor;

		// Token: 0x0400008F RID: 143
		private decimal cost;

		// Token: 0x04000090 RID: 144
		private int whoAdded;

		// Token: 0x04000091 RID: 145
		private string description;

		// Token: 0x04000092 RID: 146
		private DateTime dateAdded;

		// Token: 0x04000093 RID: 147
		private ObjectStatus status;

		// Token: 0x04000094 RID: 148
		private int itemId;
	}
}
