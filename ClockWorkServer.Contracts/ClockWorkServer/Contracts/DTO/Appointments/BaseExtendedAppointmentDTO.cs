using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000936 RID: 2358
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseExtendedAppointmentDTO : BaseBasicAppointmentDTO, ICloneable<BaseExtendedAppointmentDTO>, ICloneable
	{
		// Token: 0x06003043 RID: 12355 RVA: 0x000177D8 File Offset: 0x000159D8
		public BaseExtendedAppointmentDTO()
		{
			this.CancelInfo = new AppCancelInfoDTO();
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000177F0 File Offset: 0x000159F0
		public static T GetClone<T>(T newClone, BaseExtendedAppointmentDTO item) where T : BaseExtendedAppointmentDTO
		{
			bool flag = item == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				BaseExtendedAppointmentDTO.CopyBaseExtendedAppointment(item, newClone);
				result = newClone;
			}
			return result;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x00017824 File Offset: 0x00015A24
		public BaseExtendedAppointmentDTO(BaseExtendedAppointmentDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				BaseExtendedAppointmentDTO.CopyBaseExtendedAppointment(item, this);
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0001784C File Offset: 0x00015A4C
		public static void CopyBaseExtendedAppointment(BaseExtendedAppointmentDTO from, BaseExtendedAppointmentDTO to)
		{
			BaseBasicAppointmentDTO.CopyBaseBasicAppointment(from, to);
			to.Memo = from.Memo;
			to.WhoBooked = ((from.WhoBooked == null) ? null : from.WhoBooked.Clone());
			to.DateBooked = from.DateBooked;
			to.ExtraAttendeesCount = from.ExtraAttendeesCount;
			to.Room = ((from.Room == null) ? null : from.Room.Clone());
			to.CancelInfo = ((from.CancelInfo == null) ? null : from.CancelInfo.Clone());
			to.OverrideColour = from.OverrideColour;
			to.ActualStartDateTime = from.ActualStartDateTime;
			to.ActualEndDateTime = from.ActualEndDateTime;
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x00017907 File Offset: 0x00015B07
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x0001790F File Offset: 0x00015B0F
		[DataMember]
		public virtual string Memo { get; set; }

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x00017918 File Offset: 0x00015B18
		// (set) Token: 0x0600304A RID: 12362 RVA: 0x00017920 File Offset: 0x00015B20
		[DataMember]
		public virtual PersonBaseDTO WhoBooked { get; set; }

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x00017929 File Offset: 0x00015B29
		// (set) Token: 0x0600304C RID: 12364 RVA: 0x00017931 File Offset: 0x00015B31
		[DataMember]
		public virtual DateTime DateBooked { get; set; }

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x0001793A File Offset: 0x00015B3A
		// (set) Token: 0x0600304E RID: 12366 RVA: 0x00017942 File Offset: 0x00015B42
		[DataMember]
		public virtual int ExtraAttendeesCount { get; set; }

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0001794B File Offset: 0x00015B4B
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x00017953 File Offset: 0x00015B53
		[DataMember]
		public virtual AppointmentRoomDTO Room { get; set; }

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x0001795C File Offset: 0x00015B5C
		// (set) Token: 0x06003052 RID: 12370 RVA: 0x00017964 File Offset: 0x00015B64
		[DataMember]
		public virtual AppCancelInfoDTO CancelInfo { get; set; }

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0001796D File Offset: 0x00015B6D
		// (set) Token: 0x06003054 RID: 12372 RVA: 0x00017975 File Offset: 0x00015B75
		[DataMember]
		public int? OverrideColour { get; set; }

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x0001797E File Offset: 0x00015B7E
		// (set) Token: 0x06003056 RID: 12374 RVA: 0x00017986 File Offset: 0x00015B86
		[DataMember]
		public DateTime? ActualStartDateTime { get; set; }

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0001798F File Offset: 0x00015B8F
		// (set) Token: 0x06003058 RID: 12376 RVA: 0x00017997 File Offset: 0x00015B97
		[DataMember]
		public DateTime? ActualEndDateTime { get; set; }

		// Token: 0x06003059 RID: 12377 RVA: 0x000179A0 File Offset: 0x00015BA0
		public new BaseExtendedAppointmentDTO Clone()
		{
			return new BaseExtendedAppointmentDTO(this);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000179B8 File Offset: 0x00015BB8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
