using System;
using ClockWorkWebAPI;
using EncryptionClassLibrary;

namespace ClockWorkWebAPIWeb.AppBooking
{
	// Token: 0x0200001A RID: 26
	public class AppType
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000106C0 File Offset: 0x0000E8C0
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000106D8 File Offset: 0x0000E8D8
		public bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				this.active = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000106E4 File Offset: 0x0000E8E4
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000106FC File Offset: 0x0000E8FC
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00010714 File Offset: 0x0000E914
		public AppType()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00010728 File Offset: 0x0000E928
		public AppType(string defn, db conn, IEncryption tripleDES)
		{
			string[] array = defn.Split(new char[]
			{
				','
			});
			string text = array[0];
			string text2 = array[1];
			string text3 = array[2];
			string text4 = array[3];
			this.active = (text2.CompareTo("true") == 0);
			string[] array2 = text3.Split(new char[]
			{
				'.'
			});
		}

		// Token: 0x04000082 RID: 130
		private bool active = true;

		// Token: 0x04000083 RID: 131
		private int appTypeId;

		// Token: 0x04000084 RID: 132
		private string title;
	}
}
