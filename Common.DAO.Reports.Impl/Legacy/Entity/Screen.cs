using System;
using System.Data;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000025 RID: 37
	public class Screen
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00029AF4 File Offset: 0x00027CF4
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00029B0C File Offset: 0x00027D0C
		public eDynamicFormType ScreenType
		{
			get
			{
				return this.screenType;
			}
			set
			{
				this.screenType = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00029B18 File Offset: 0x00027D18
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00029B30 File Offset: 0x00027D30
		public string ScreenTitle
		{
			get
			{
				return this.screenTitle;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00029B48 File Offset: 0x00027D48
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00029B50 File Offset: 0x00027D50
		public bool ShowAsButton { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00029B59 File Offset: 0x00027D59
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00029B61 File Offset: 0x00027D61
		public bool IsActive { get; set; }

		// Token: 0x0600028E RID: 654 RVA: 0x00029B6A File Offset: 0x00027D6A
		public Screen(int screenNum, string screenTitle)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = eDynamicFormType.PerStudent;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00029B94 File Offset: 0x00027D94
		public Screen(int screenNum, string screenTitle, eDynamicFormType screenType)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = screenType;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00029BC0 File Offset: 0x00027DC0
		public Screen(int screenNum, string screenTitle, int screenType)
		{
			eDynamicFormType eDynamicFormType = (eDynamicFormType)(Enum.IsDefined(typeof(eDynamicFormType), screenType) ? screenType : 0);
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = eDynamicFormType;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00029C14 File Offset: 0x00027E14
		public Screen(DataRow dr)
		{
			this.screenType = eDynamicFormType.PerStudent;
			DataTable table = dr.Table;
			bool flag = dr != null;
			if (flag)
			{
				this.screenNum = ((dr["screennum"] != DBNull.Value) ? ((int)dr["screennum"]) : -1);
				this.screenTitle = ((dr["description"] != DBNull.Value) ? ((string)dr["description"]) : "");
				this.screenDescription = "";
				this.ShowAsButton = (table.Columns.Contains("showasbutton") && dr["showasbutton"] != DBNull.Value && Convert.ToBoolean(dr["showasbutton"]));
				this.IsActive = (table.Columns.Contains("isactive") && dr["isactive"] != DBNull.Value && Convert.ToBoolean(dr["isactive"]));
				bool flag2 = table.Columns.Contains("typecode");
				if (flag2)
				{
					try
					{
						int num = (dr["typecode"] == DBNull.Value) ? 0 : ((int)dr["typecode"]);
						bool flag3 = Enum.IsDefined(typeof(eDynamicFormType), num);
						if (flag3)
						{
							this.screenType = (eDynamicFormType)num;
						}
					}
					catch
					{
					}
				}
			}
			else
			{
				this.screenNum = -1;
				this.screenTitle = "";
				this.screenDescription = "";
			}
		}

		// Token: 0x040000F5 RID: 245
		private eDynamicFormType screenType;

		// Token: 0x040000F6 RID: 246
		private int screenNum;

		// Token: 0x040000F7 RID: 247
		private string screenTitle;

		// Token: 0x040000F8 RID: 248
		private string screenDescription;
	}
}
