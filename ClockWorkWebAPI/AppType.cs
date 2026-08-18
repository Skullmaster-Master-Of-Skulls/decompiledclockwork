using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000C RID: 12
	public class AppType
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000053B7 File Offset: 0x000035B7
		public AppType(int appTypeId, int availabilityGroupId, string text, int colourInt)
		{
			this.appTypeId = appTypeId;
			this.availabilityGroupId = availabilityGroupId;
			this.text = text;
			this.colourInt = colourInt;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000053E0 File Offset: 0x000035E0
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000053F8 File Offset: 0x000035F8
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005404 File Offset: 0x00003604
		public int AvailabilityGroupId
		{
			get
			{
				return this.availabilityGroupId;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000541C File Offset: 0x0000361C
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00005434 File Offset: 0x00003634
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005440 File Offset: 0x00003640
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00005458 File Offset: 0x00003658
		public int ColourInt
		{
			get
			{
				return this.colourInt;
			}
			set
			{
				this.colourInt = value;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005464 File Offset: 0x00003664
		public static AppType FindAppType(List<AppType> appTypes, string text)
		{
			foreach (AppType appType in appTypes)
			{
				bool flag = appType.text.CompareTo(text) == 0;
				if (flag)
				{
					return appType;
				}
			}
			return null;
		}

		// Token: 0x0400002E RID: 46
		private int appTypeId;

		// Token: 0x0400002F RID: 47
		private int availabilityGroupId;

		// Token: 0x04000030 RID: 48
		private string text;

		// Token: 0x04000031 RID: 49
		private int colourInt;
	}
}
