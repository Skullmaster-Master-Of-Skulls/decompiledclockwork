using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsPointOfContact
{
	// Token: 0x02000097 RID: 151
	public class PointOfContactClientManager : IPointOfContactClientManager, IWebService
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x000183FC File Offset: 0x000165FC
		public int CreatePointOfContact(PointOfContactDTO PointOfContact)
		{
			CreatePointOfContactReq createPointOfContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePointOfContactReq>();
			createPointOfContactReq.PointOfContact = PointOfContact;
			return ClientServiceFactory.GetClientInstance<IPointOfContact>().CreatePointOfContact(createPointOfContactReq).AppointmentId;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00018434 File Offset: 0x00016634
		public void UpdatePointOfContact(PointOfContactDTO PointOfContact)
		{
			UpdatePointOfContactReq updatePointOfContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePointOfContactReq>();
			updatePointOfContactReq.PointOfContact = PointOfContact;
			ClientServiceFactory.GetClientInstance<IPointOfContact>().UpdatePointOfContact(updatePointOfContactReq);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00018464 File Offset: 0x00016664
		public void DeletePointOfContact(int AppointmentId)
		{
			DeletePointOfContactReq deletePointOfContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeletePointOfContactReq>();
			deletePointOfContactReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IPointOfContact>().DeletePointOfContact(deletePointOfContactReq);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00018494 File Offset: 0x00016694
		public PointOfContactDTO LoadPointOfContactById(int AppointmentId)
		{
			LoadPointOfContactByIdReq loadPointOfContactByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPointOfContactByIdReq>();
			loadPointOfContactByIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IPointOfContact>().LoadPointOfContactById(loadPointOfContactByIdReq).PointOfContact;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000184CC File Offset: 0x000166CC
		public int SaveEmailAsPointOfContact(int StudentPersonId, int StaffPersonId, TPMailMessageDTO MailMessage, ePointOfContactContext PocContext)
		{
			SaveEmailAsPointOfContactReq saveEmailAsPointOfContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveEmailAsPointOfContactReq>();
			saveEmailAsPointOfContactReq.StudentPersonId = StudentPersonId;
			saveEmailAsPointOfContactReq.StaffPersonId = StaffPersonId;
			saveEmailAsPointOfContactReq.MailMessage = MailMessage;
			saveEmailAsPointOfContactReq.PocContext = PocContext;
			return ClientServiceFactory.GetClientInstance<IPointOfContact>().SaveEmailAsPointOfContact(saveEmailAsPointOfContactReq).AppointmentId;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001851C File Offset: 0x0001671C
		public int CreatePointOfContactFromMessage(ePointOfContactContext PocContext, int StudentPersonId, string PlainTextMessage)
		{
			CreatePointOfContactFromMessageReq createPointOfContactFromMessageReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePointOfContactFromMessageReq>();
			createPointOfContactFromMessageReq.PocContext = PocContext;
			createPointOfContactFromMessageReq.StudentPersonId = StudentPersonId;
			createPointOfContactFromMessageReq.PlainTextMessage = PlainTextMessage;
			return ClientServiceFactory.GetClientInstance<IPointOfContact>().CreatePointOfContactFromMessage(createPointOfContactFromMessageReq).NewAppointmentId;
		}
	}
}
