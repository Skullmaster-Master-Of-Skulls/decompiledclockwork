using System;
using System.Collections;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI.AT
{
	// Token: 0x020000A1 RID: 161
	public class ATFactory
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0003041C File Offset: 0x0002F41C
		public ItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00030434 File Offset: 0x0002F434
		public VendorCollection Vendors
		{
			get
			{
				return this.vendors;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00030458 File Offset: 0x0002F458
		public void LoadData(UnivDataAdapter da)
		{
			this.vendors = ATFactory.LoadVendors(da);
			da.SelectCommand.CommandText = "SELECT \ti.itemid,i.groupmembershipcode,i.itemtitle,i.itemdescription,i.itemnote,\r\n\tvi.vendoritemid,vi.cost,vi.tax1,vi.tax2,vi.salecost,vi.saleexpirydate,vi.shippingcost,vi.vendorid,\r\n\tv.vendortitle,v.vendordescription,v.vendornote,v.vendoraddress,v.vendorphone1,v.vendorphone2,v.vendoremail,v.vendorwebsiteurl,\r\n\tvc.vendorcontactid,vc.contactname,vc.contacttitle,vc.contactphone1,vc.contactphone2,vc.contactemail,vc.contactnote,vc.contactusername,vc.contactpass,\r\nFROM\tAT_item i LEFT JOIN AT_vendoritem vi ON vi.itemid=i.itemid\r\n\tLEFT JOIN AT_vendor v ON v.vendorid=vi.vendorid\r\n\tLEFT JOIN AT_vendorcontact vc ON vc.vendorid=v.vendorid \r\nWHERE i.isactive='1'\r\nORDER BY i.itemtitle";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			this.items = new ItemCollection();
			this.vendors = new VendorCollection();
			int num;
			for (int i = 0; i < dataTable.Rows.Count; i = num)
			{
				ArrayList groupOfRows = ATFactory.GetGroupOfRows(dataTable, null, i, "itemid", out num);
				DataRow dr = (DataRow)groupOfRows[0];
				Item item = new Item();
				item.SetDetails(ATFactory.SafeRowString(dr, "itemtitle"), ATFactory.SafeRowString(dr, "itemdescription"), ATFactory.SafeRowString(dr, "itemnote"));
				item.Status = ObjectStatus.Unmodified;
				string text = ATFactory.SafeRowString(dr, "groupmembershipcode");
				string[] array = text.Split(new char[]
				{
					','
				});
				foreach (string text2 in array)
				{
					ItemCategory category = this.items.Categories.AddIterative(text);
					item.Categories.Add(category);
				}
				foreach (object obj in groupOfRows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow["vendorid"] == DBNull.Value)
					{
						break;
					}
					int vendorId = (int)dataRow["vendorid"];
					Vendor vendor = this.vendors.FindVendor(vendorId);
					if (vendor != null)
					{
						VendorItem vendorItem = new VendorItem(item, vendor);
						item.VendorItems.Add(vendorItem);
					}
				}
				this.items.Add(item);
			}
			ItemCategory itemCategory = this.items.Categories.AddIterative("Apple.Software");
			ItemCategory itemCategory2 = this.items.Categories.AddIterative("Apple.Hardware");
			ItemCategory itemCategory3 = this.items.Categories.AddIterative("Windows.Software");
			ItemCategory itemCategory4 = this.items.Categories.AddIterative("Windows.Hardware");
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000306C4 File Offset: 0x0002F6C4
		private static string SafeRowString(DataRow dr, string colName)
		{
			return (dr[colName] == DBNull.Value) ? "" : ((string)dr[colName]);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000306F8 File Offset: 0x0002F6F8
		private static ArrayList GetGroupOfRows(DataTable t, ArrayList rows0, int startIndex, string intColname, out int startIndexNextGroup)
		{
			ArrayList arrayList = new ArrayList();
			DataRow dataRow = (t != null) ? t.Rows[startIndex] : ((DataRow)rows0[startIndex]);
			bool flag = dataRow[intColname] == DBNull.Value;
			int num = flag ? 0 : ((int)dataRow[intColname]);
			int i = startIndex + 1;
			int num2 = (t != null) ? t.Rows.Count : rows0.Count;
			while (i < num2)
			{
				DataRow dataRow2 = (t != null) ? t.Rows[i] : ((DataRow)rows0[i]);
				if (flag)
				{
					if (dataRow2[intColname] != DBNull.Value)
					{
						break;
					}
					arrayList.Add(dataRow2);
				}
				else
				{
					if (dataRow2[intColname] == DBNull.Value)
					{
						break;
					}
					if ((int)dataRow2[intColname] != num)
					{
						break;
					}
					arrayList.Add(dataRow2);
				}
				i++;
			}
			startIndexNextGroup = i - 1;
			return arrayList;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0003081C File Offset: 0x0002F81C
		public static Vendor CreateNewVendor(string title, string description, string note, string address, string phone1, string phone2, string email, string websiteurl)
		{
			Vendor vendor = new Vendor();
			vendor.SetDetails(title, description, note, address, phone1, phone2, email, websiteurl);
			vendor.Status = ObjectStatus.New;
			return vendor;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00030850 File Offset: 0x0002F850
		public static VendorCollection LoadVendors(UnivDataAdapter da)
		{
			VendorCollection vendorCollection = new VendorCollection();
			da.SelectCommand.CommandText = "SELECT v.vendorid,v.vendortitle,v.vendordescription,v.vendornote,v.vendoraddress,v.vendorphone1,v.vendorphone2,v.vendoremail,v.vendorwebsiteurl,\r\nvc.vendorcontactid,vc.contactname,vc.contacttitle,vc.contactphone1,vc.contactphone2,vc.contactemail,vc.contactnote,vc.contactusername,vc.contactpass\r\nFROM    AT_vendor v LEFT JOIN AT_vendorcontact vc ON vc.vendorid=v.vendorid WHERE v.isactive='1'";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			int num;
			for (int i = 0; i < dataTable.Rows.Count; i = num)
			{
				ArrayList groupOfRows = ATFactory.GetGroupOfRows(dataTable, null, i, "vendorid", out num);
				DataRow dr = (DataRow)groupOfRows[0];
				Vendor vendor = new Vendor();
				vendor.SetDetails(ATFactory.SafeRowString(dr, "vendortitle"), ATFactory.SafeRowString(dr, "vendordescription"), ATFactory.SafeRowString(dr, "vendornote"), ATFactory.SafeRowString(dr, "vendoraddress"), ATFactory.SafeRowString(dr, "vendorphone1"), ATFactory.SafeRowString(dr, "vendorphone2"), ATFactory.SafeRowString(dr, "vendoremail"), ATFactory.SafeRowString(dr, "websiteurl"));
				vendor.Status = ObjectStatus.Unmodified;
				foreach (object obj in groupOfRows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow["vendorcontactid"] == DBNull.Value)
					{
						break;
					}
					int vendorContactId = (int)dataRow["vendorcontactid"];
					VendorContact vendorContact = vendorCollection.FindVendorContact(vendorContactId);
					if (vendorContact == null)
					{
						vendorContact = new VendorContact();
						vendorContact.SetDetails(vendorContactId, ATFactory.SafeRowString(dataRow, "contactname"), ATFactory.SafeRowString(dataRow, "contacttitle"), ATFactory.SafeRowString(dataRow, "contactphone1"), ATFactory.SafeRowString(dataRow, "contactphone2"), ATFactory.SafeRowString(dataRow, "contactemail"), ATFactory.SafeRowString(dataRow, "contactnote"), ATFactory.SafeRowString(dataRow, "contactusername"), ATFactory.SafeRowString(dataRow, "contactpass"));
						vendorContact.Status = ObjectStatus.Unmodified;
						vendorCollection.Contacts.Add(vendorContact);
					}
					vendor.Contacts.Add(vendorContact);
				}
				vendorCollection.Add(vendor);
			}
			return vendorCollection;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00030AA4 File Offset: 0x0002FAA4
		public static void SaveChanges(UnivDataAdapter da, VendorCollection vendors)
		{
		}

		// Token: 0x0400040F RID: 1039
		private ItemCollection items;

		// Token: 0x04000410 RID: 1040
		private VendorCollection vendors;
	}
}
