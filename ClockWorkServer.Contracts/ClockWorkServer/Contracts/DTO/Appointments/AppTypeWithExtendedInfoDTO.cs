using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000933 RID: 2355
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppTypeWithExtendedInfoDTO : AppTypeDTO, ICloneable<AppTypeWithExtendedInfoDTO>, ICloneable
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x000171A0 File Offset: 0x000153A0
		public AppTypeWithExtendedInfoDTO()
		{
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000171AC File Offset: 0x000153AC
		public AppTypeWithExtendedInfoDTO(AppTypeWithExtendedInfoDTO item)
		{
			AppTypeDTO.CloneBaseAppType<AppTypeWithExtendedInfoDTO, AppTypeWithExtendedInfoDTO>(item, this);
			bool flag = item == null;
			if (!flag)
			{
				this.IsBackground = item.IsBackground;
				this.DefaultOverrideColourArgb = item.DefaultOverrideColourArgb;
				this.DefaultIconIndex = item.DefaultIconIndex;
				this.ShowInHighlights = item.ShowInHighlights;
				this.PerAppScreenNumsForTabs = item.PerAppScreenNumsForTabs;
				this.PerJustAppScreenNum = item.PerJustAppScreenNum;
				this.IconIndex = item.IconIndex;
				this.ClientGroupIds = item.ClientGroupIds;
				this.RequiresRoom = item.RequiresRoom;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x00017248 File Offset: 0x00015448
		// (set) Token: 0x06002FFE RID: 12286 RVA: 0x00017250 File Offset: 0x00015450
		[DataMember]
		public bool IsBackground { get; set; }

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x00017259 File Offset: 0x00015459
		// (set) Token: 0x06003000 RID: 12288 RVA: 0x00017261 File Offset: 0x00015461
		[DataMember]
		public int DefaultOverrideColourArgb { get; set; }

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x0001726A File Offset: 0x0001546A
		// (set) Token: 0x06003002 RID: 12290 RVA: 0x00017272 File Offset: 0x00015472
		[DataMember]
		public int DefaultIconIndex { get; set; }

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x0001727B File Offset: 0x0001547B
		// (set) Token: 0x06003004 RID: 12292 RVA: 0x00017283 File Offset: 0x00015483
		[DataMember]
		public bool ShowInHighlights { get; set; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x0001728C File Offset: 0x0001548C
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x00017294 File Offset: 0x00015494
		[DataMember]
		public IList<int> PerAppScreenNumsForTabs { get; set; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x0001729D File Offset: 0x0001549D
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x000172A5 File Offset: 0x000154A5
		[DataMember]
		public int PerJustAppScreenNum { get; set; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000172AE File Offset: 0x000154AE
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x000172B6 File Offset: 0x000154B6
		[DataMember]
		public int IconIndex { get; set; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000172BF File Offset: 0x000154BF
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x000172C7 File Offset: 0x000154C7
		[DataMember]
		public IList<int> ClientGroupIds { get; set; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000172D0 File Offset: 0x000154D0
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x000172D8 File Offset: 0x000154D8
		[DataMember]
		public bool RequiresRoom { get; set; }

		// Token: 0x0600300F RID: 12303 RVA: 0x000172E4 File Offset: 0x000154E4
		public new AppTypeWithExtendedInfoDTO Clone()
		{
			return new AppTypeWithExtendedInfoDTO(this);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000172FC File Offset: 0x000154FC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
