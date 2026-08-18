using System;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000080 RID: 128
	public class Vendor
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00024224 File Offset: 0x00023224
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0002423C File Offset: 0x0002323C
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00024248 File Offset: 0x00023248
		public int VendorId
		{
			get
			{
				return this.vendorId;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00024260 File Offset: 0x00023260
		public VendorContactCollection Contacts
		{
			get
			{
				return this.contacts;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00024278 File Offset: 0x00023278
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00024290 File Offset: 0x00023290
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000242A4 File Offset: 0x000232A4
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x000242BC File Offset: 0x000232BC
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000242D0 File Offset: 0x000232D0
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x000242E8 File Offset: 0x000232E8
		public string Note
		{
			get
			{
				return this.note;
			}
			set
			{
				this.note = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000242FC File Offset: 0x000232FC
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00024314 File Offset: 0x00023314
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00024328 File Offset: 0x00023328
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00024340 File Offset: 0x00023340
		public string Phone1
		{
			get
			{
				return this.phone1;
			}
			set
			{
				this.phone1 = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00024354 File Offset: 0x00023354
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0002436C File Offset: 0x0002336C
		public string Phone2
		{
			get
			{
				return this.phone2;
			}
			set
			{
				this.phone2 = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00024380 File Offset: 0x00023380
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00024398 File Offset: 0x00023398
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
				this.SetChanged();
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000243AC File Offset: 0x000233AC
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x000243C4 File Offset: 0x000233C4
		public string WebsiteUrl
		{
			get
			{
				return this.websiteurl;
			}
			set
			{
				this.websiteurl = value;
				this.SetChanged();
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000243D8 File Offset: 0x000233D8
		private void SetChanged()
		{
			if (this.status != ObjectStatus.New)
			{
				this.status = ObjectStatus.Modified;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000243FA File Offset: 0x000233FA
		public Vendor()
		{
			this.contacts = new VendorContactCollection();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00024417 File Offset: 0x00023417
		public void SetDetails(string title, string description, string note, string address, string phone1, string phone2, string email, string websiteurl)
		{
			this.title = title;
			this.description = description;
			this.note = note;
			this.address = address;
			this.phone1 = phone1;
			this.phone2 = phone2;
			this.email = email;
			this.websiteurl = websiteurl;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00024457 File Offset: 0x00023457
		public void Delete()
		{
			this.status = ObjectStatus.Deleted;
		}

		// Token: 0x0400034D RID: 845
		private int vendorId;

		// Token: 0x0400034E RID: 846
		private ObjectStatus status = ObjectStatus.Unknown;

		// Token: 0x0400034F RID: 847
		private string title;

		// Token: 0x04000350 RID: 848
		private string description;

		// Token: 0x04000351 RID: 849
		private string note;

		// Token: 0x04000352 RID: 850
		private string address;

		// Token: 0x04000353 RID: 851
		private string phone1;

		// Token: 0x04000354 RID: 852
		private string phone2;

		// Token: 0x04000355 RID: 853
		private string email;

		// Token: 0x04000356 RID: 854
		private string websiteurl;

		// Token: 0x04000357 RID: 855
		private VendorContactCollection contacts;
	}
}
