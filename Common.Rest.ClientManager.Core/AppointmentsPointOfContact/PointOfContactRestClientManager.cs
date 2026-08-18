using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsPointOfContact
{
	// Token: 0x02000081 RID: 129
	public class PointOfContactRestClientManager : BearerTokenRestProxy<IPointOfContactClientManager>, IPointOfContactClientManager, IWebService
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000DEDD File Offset: 0x0000C0DD
		public PointOfContactRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000DEE7 File Offset: 0x0000C0E7
		public PointOfContactRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000DEF2 File Offset: 0x0000C0F2
		public int CreatePointOfContact(PointOfContactDTO PointOfContact)
		{
			return base.Post<PointOfContactDTO, int>(PointOfContact, "pointofcontact");
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000DF00 File Offset: 0x0000C100
		public void UpdatePointOfContact(PointOfContactDTO PointOfContact)
		{
			base.Put<PointOfContactDTO>(PointOfContact, "pointofcontact");
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000DF10 File Offset: 0x0000C110
		public int SaveEmailAsPointOfContact(int StudentPersonId, int StaffPersonId, TPMailMessageDTO MailMessage, ePointOfContactContext PocContext)
		{
			SaveEmailAsPointOfContactReq saveEmailAsPointOfContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveEmailAsPointOfContactReq>();
			saveEmailAsPointOfContactReq.StudentPersonId = StudentPersonId;
			saveEmailAsPointOfContactReq.StaffPersonId = StaffPersonId;
			saveEmailAsPointOfContactReq.MailMessage = MailMessage;
			saveEmailAsPointOfContactReq.PocContext = PocContext;
			return base.Post<SaveEmailAsPointOfContactReq, int>(saveEmailAsPointOfContactReq, "pointofcontact/saveemailaspointofcontact");
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000DF51 File Offset: 0x0000C151
		public void DeletePointOfContact(int AppointmentId)
		{
			base.Delete(string.Format("pointofcontact/appid/{0}", AppointmentId));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000DF69 File Offset: 0x0000C169
		public PointOfContactDTO LoadPointOfContactById(int AppointmentId)
		{
			return base.Get<PointOfContactDTO>(string.Format("pointofcontact/appid/{0}", AppointmentId), true);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000DF84 File Offset: 0x0000C184
		public int CreatePointOfContactFromMessage(ePointOfContactContext PocContext, int StudentPersonId, string PlainTextMessage)
		{
			CreatePointOfContactFromMessageReq createPointOfContactFromMessageReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePointOfContactFromMessageReq>();
			createPointOfContactFromMessageReq.PocContext = PocContext;
			createPointOfContactFromMessageReq.StudentPersonId = StudentPersonId;
			createPointOfContactFromMessageReq.PlainTextMessage = PlainTextMessage;
			return base.Post<CreatePointOfContactFromMessageReq, int>(createPointOfContactFromMessageReq, "pointofcontact/createfrommessage");
		}
	}
}
