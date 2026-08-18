using System;
using System.Data;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000065 RID: 101
	public class Screen
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00022340 File Offset: 0x00020540
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00022358 File Offset: 0x00020558
		public ScreenType ScreenType
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

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00022364 File Offset: 0x00020564
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0002237C File Offset: 0x0002057C
		public string ScreenTitle
		{
			get
			{
				return this.screenTitle;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00022394 File Offset: 0x00020594
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0002239C File Offset: 0x0002059C
		public bool ShowAsButton { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000223A5 File Offset: 0x000205A5
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000223AD File Offset: 0x000205AD
		public bool IsActive { get; set; }

		// Token: 0x06000505 RID: 1285 RVA: 0x000223B6 File Offset: 0x000205B6
		public Screen(int screenNum, string screenTitle)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = ScreenType.ScreenType_PerStudent;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000223E0 File Offset: 0x000205E0
		public Screen(int screenNum, string screenTitle, ScreenType screenType)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = screenType;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002240C File Offset: 0x0002060C
		public Screen(DataRow dr)
		{
			this.screenType = ScreenType.ScreenType_PerStudent;
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
						this.screenType = (ScreenType)num;
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

		// Token: 0x04000297 RID: 663
		private ScreenType screenType;

		// Token: 0x04000298 RID: 664
		private int screenNum;

		// Token: 0x04000299 RID: 665
		private string screenTitle;

		// Token: 0x0400029A RID: 666
		private string screenDescription;
	}
}
