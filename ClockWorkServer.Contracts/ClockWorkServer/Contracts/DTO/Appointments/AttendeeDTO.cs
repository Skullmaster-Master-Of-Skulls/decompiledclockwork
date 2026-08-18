using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000934 RID: 2356
	[DataContract(Namespace = "http://tpro.ca")]
	public class AttendeeDTO : ICloneable<AttendeeDTO>, ICloneable
	{
		// Token: 0x06003011 RID: 12305 RVA: 0x000036BD File Offset: 0x000018BD
		public AttendeeDTO()
		{
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00017314 File Offset: 0x00015514
		public AttendeeDTO(AttendeeDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Tag = item.Tag;
				this.AttendeeId = item.AttendeeId;
				this.Person = ((item.Person == null) ? null : item.Person.Clone());
				this.IsNoShow = item.IsNoShow;
				this.MiscCode = item.MiscCode;
				this.Attendee = item.Attendee;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x00017391 File Offset: 0x00015591
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x00017399 File Offset: 0x00015599
		public object Tag { get; set; }

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000173A2 File Offset: 0x000155A2
		// (set) Token: 0x06003016 RID: 12310 RVA: 0x000173AA File Offset: 0x000155AA
		[DataMember]
		public int AttendeeId { get; set; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000173B3 File Offset: 0x000155B3
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x000173BB File Offset: 0x000155BB
		[DataMember]
		public PersonBaseDTO Person { get; set; }

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000173C4 File Offset: 0x000155C4
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x000173CC File Offset: 0x000155CC
		[DataMember]
		public bool IsNoShow { get; set; }

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000173D5 File Offset: 0x000155D5
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x000173DD File Offset: 0x000155DD
		[DataMember]
		public int MiscCode { get; set; }

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000173E8 File Offset: 0x000155E8
		public int PrimaryGroupId
		{
			get
			{
				bool flag = this.Person == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)this.Person.CoreGroup;
				}
				return result;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x00017416 File Offset: 0x00015616
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x0001741E File Offset: 0x0001561E
		public object Attendee { get; set; }

		// Token: 0x06003020 RID: 12320 RVA: 0x00017428 File Offset: 0x00015628
		public AttendeeDTO Clone()
		{
			return new AttendeeDTO(this);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x00017440 File Offset: 0x00015640
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
