using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Mappers.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000011 RID: 17
	public class PointOfContactServiceManager : IPointOfContact, IService
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000057E8 File Offset: 0x000039E8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000057FC File Offset: 0x000039FC
		public CreatePointOfContactResp CreatePointOfContact(CreatePointOfContactReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			int appointmentId = pointOfContactManager.CreatePointOfContact(false, Request.PointOfContact.ToDomainObject());
			return new CreatePointOfContactResp
			{
				AppointmentId = appointmentId
			};
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000583C File Offset: 0x00003A3C
		public void UpdatePointOfContact(UpdatePointOfContactReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			pointOfContactManager.UpdatePointOfContact(false, Request.PointOfContact.ToDomainObject());
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000586C File Offset: 0x00003A6C
		public void DeletePointOfContact(DeletePointOfContactReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			pointOfContactManager.DeletePointOfContact(false, Request.AppointmentId);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005894 File Offset: 0x00003A94
		public LoadPointOfContactByIdResp LoadPointOfContactById(LoadPointOfContactByIdReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			PointOfContact poc = pointOfContactManager.LoadPointOfContactById(Request.AppointmentId);
			return new LoadPointOfContactByIdResp
			{
				PointOfContact = poc.ToDTO()
			};
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000058D4 File Offset: 0x00003AD4
		public SaveEmailAsPointOfContactResp SaveEmailAsPointOfContact(SaveEmailAsPointOfContactReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			int appointmentId = pointOfContactManager.SaveEmailAsPointOfContact(false, Request.StudentPersonId, Request.StaffPersonId, Request.MailMessage.ToDomainObject(), Request.PocContext);
			return new SaveEmailAsPointOfContactResp
			{
				AppointmentId = appointmentId
			};
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005924 File Offset: 0x00003B24
		public CreatePointOfContactFromMessageResp CreatePointOfContactFromMessage(CreatePointOfContactFromMessageReq Request)
		{
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(Request.GetOperationContext());
			int newAppointmentId = pointOfContactManager.CreatePointOfContactFromMessage(Request.PocContext, Request.StudentPersonId, Request.PlainTextMessage);
			return new CreatePointOfContactFromMessageResp
			{
				NewAppointmentId = newAppointmentId
			};
		}
	}
}
