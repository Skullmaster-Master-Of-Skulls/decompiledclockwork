using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsPointOfContact
{
	// Token: 0x02000091 RID: 145
	public interface IPointOfContactClientManager : IWebService
	{
		// Token: 0x06000452 RID: 1106
		int CreatePointOfContact(PointOfContactDTO PointOfContact);

		// Token: 0x06000453 RID: 1107
		void UpdatePointOfContact(PointOfContactDTO PointOfContact);

		// Token: 0x06000454 RID: 1108
		int SaveEmailAsPointOfContact(int StudentPersonId, int StaffPersonId, TPMailMessageDTO MailMessage, ePointOfContactContext PocContext);

		// Token: 0x06000455 RID: 1109
		void DeletePointOfContact(int AppointmentId);

		// Token: 0x06000456 RID: 1110
		PointOfContactDTO LoadPointOfContactById(int AppointmentId);

		// Token: 0x06000457 RID: 1111
		int CreatePointOfContactFromMessage(ePointOfContactContext PocContext, int StudentPersonId, string PlainTextMessage);
	}
}
