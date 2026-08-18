using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class OldUserSetting : BusinessBase<int>
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000100CC File Offset: 0x0000E2CC
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SettingIdOrSettingGroupId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000100E4 File Offset: 0x0000E2E4
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x000100EC File Offset: 0x0000E2EC
		public eSettingCode SettingCode { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000100F5 File Offset: 0x0000E2F5
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x000100FD File Offset: 0x0000E2FD
		public string StringVal { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00010106 File Offset: 0x0000E306
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001010E File Offset: 0x0000E30E
		public int IntVal { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00010117 File Offset: 0x0000E317
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001011F File Offset: 0x0000E31F
		public eDataItemModificationStatus ModificationStatus { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00010128 File Offset: 0x0000E328
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00010130 File Offset: 0x0000E330
		public int PersonOrGroupId { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00010139 File Offset: 0x0000E339
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00010141 File Offset: 0x0000E341
		public eOldUserSettingType SettingType { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001014A File Offset: 0x0000E34A
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00010152 File Offset: 0x0000E352
		public int OrderNum { get; set; }
	}
}
