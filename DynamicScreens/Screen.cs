using System;
using System.Data;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace DynamicScreens
{
	// Token: 0x0200007F RID: 127
	public class Screen
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0004879C File Offset: 0x0004779C
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x000487B4 File Offset: 0x000477B4
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000487C0 File Offset: 0x000477C0
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000487D8 File Offset: 0x000477D8
		public string ScreenTitle
		{
			get
			{
				return this.screenTitle;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000487F0 File Offset: 0x000477F0
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00048807 File Offset: 0x00047807
		public bool ShowAsButton { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00048810 File Offset: 0x00047810
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00048827 File Offset: 0x00047827
		public bool IsActive { get; set; }

		// Token: 0x06000611 RID: 1553 RVA: 0x00048830 File Offset: 0x00047830
		public Screen(int screenNum, string screenTitle)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = eDynamicFormType.PerStudent;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0004885B File Offset: 0x0004785B
		public Screen(int screenNum, string screenTitle, eDynamicFormType screenType)
		{
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = screenType;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00048888 File Offset: 0x00047888
		public Screen(int screenNum, string screenTitle, int screenType)
		{
			eDynamicFormType eDynamicFormType = (eDynamicFormType)(Enum.IsDefined(typeof(eDynamicFormType), screenType) ? screenType : 0);
			this.screenNum = screenNum;
			this.screenTitle = screenTitle;
			this.screenDescription = "";
			this.screenType = eDynamicFormType;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000488DC File Offset: 0x000478DC
		public Screen(DataRow dr)
		{
			this.screenType = eDynamicFormType.PerStudent;
			DataTable table = dr.Table;
			if (dr != null)
			{
				this.screenNum = ((dr["screennum"] != DBNull.Value) ? ((int)dr["screennum"]) : -1);
				this.screenTitle = ((dr["description"] != DBNull.Value) ? ((string)dr["description"]) : "");
				this.screenDescription = "";
				this.ShowAsButton = (table.Columns.Contains("showasbutton") && dr["showasbutton"] != DBNull.Value && Convert.ToBoolean(dr["showasbutton"]));
				this.IsActive = (table.Columns.Contains("isactive") && dr["isactive"] != DBNull.Value && Convert.ToBoolean(dr["isactive"]));
				if (table.Columns.Contains("typecode"))
				{
					try
					{
						int num = (dr["typecode"] == DBNull.Value) ? 0 : ((int)dr["typecode"]);
						if (Enum.IsDefined(typeof(eDynamicFormType), num))
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

		// Token: 0x0400039D RID: 925
		private eDynamicFormType screenType;

		// Token: 0x0400039E RID: 926
		private int screenNum;

		// Token: 0x0400039F RID: 927
		private string screenTitle;

		// Token: 0x040003A0 RID: 928
		private string screenDescription;
	}
}
