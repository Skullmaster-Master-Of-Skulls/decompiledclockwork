using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092F RID: 2351
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppShowTimeAsTypeDTO : ICloneable<AppShowTimeAsTypeDTO>, ICloneable
	{
		// Token: 0x06002FCF RID: 12239 RVA: 0x000036BD File Offset: 0x000018BD
		public AppShowTimeAsTypeDTO()
		{
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00016E48 File Offset: 0x00015048
		public AppShowTimeAsTypeDTO(AppShowTimeAsTypeDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.AppointmentShowTimeAsId = item.AppointmentShowTimeAsId;
				this.AppCode = item.AppCode;
				this.Title = item.Title;
				this.ColourArgB = item.ColourArgB;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x00016E9B File Offset: 0x0001509B
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x00016EA3 File Offset: 0x000150A3
		[DataMember]
		public int AppointmentShowTimeAsId { get; set; }

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x00016EAC File Offset: 0x000150AC
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x00016EB4 File Offset: 0x000150B4
		[DataMember]
		public int AppCode { get; set; }

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x00016EBD File Offset: 0x000150BD
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x00016EC5 File Offset: 0x000150C5
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x00016ECE File Offset: 0x000150CE
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x00016ED6 File Offset: 0x000150D6
		[DataMember]
		public int? ColourArgB { get; set; }

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x00016EE0 File Offset: 0x000150E0
		public bool IsTentative
		{
			get
			{
				return this.AppCode == -1;
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x00016EFC File Offset: 0x000150FC
		public void SetIsTentative(bool newIsTentative)
		{
			if (newIsTentative)
			{
				bool flag = !this.IsTentative;
				if (flag)
				{
					this.AppCode = -1;
				}
			}
			else
			{
				bool isTentative = this.IsTentative;
				if (isTentative)
				{
					this.AppCode = 0;
				}
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x00016F3C File Offset: 0x0001513C
		public AppShowTimeAsTypeDTO Clone()
		{
			return new AppShowTimeAsTypeDTO(this);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00016F54 File Offset: 0x00015154
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
