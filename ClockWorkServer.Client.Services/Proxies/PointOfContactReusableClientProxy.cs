using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000022 RID: 34
	public class PointOfContactReusableClientProxy : WCFTokenBasedReusableClientProxy<IPointOfContact>, IPointOfContact, IService
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000706E File Offset: 0x0000526E
		public PointOfContactReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007079 File Offset: 0x00005279
		public PointOfContactReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007088 File Offset: 0x00005288
		public CreatePointOfContactResp CreatePointOfContact(CreatePointOfContactReq Request)
		{
			return this.WrapServiceMethod<CreatePointOfContactResp>(() => this.Proxy.CreatePointOfContact(Request));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000070C0 File Offset: 0x000052C0
		public void DeletePointOfContact(DeletePointOfContactReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeletePointOfContact(Request);
			});
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000070F8 File Offset: 0x000052F8
		public LoadPointOfContactByIdResp LoadPointOfContactById(LoadPointOfContactByIdReq Request)
		{
			return this.WrapServiceMethod<LoadPointOfContactByIdResp>(() => this.Proxy.LoadPointOfContactById(Request));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007130 File Offset: 0x00005330
		public void UpdatePointOfContact(UpdatePointOfContactReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdatePointOfContact(Request);
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007168 File Offset: 0x00005368
		public SaveEmailAsPointOfContactResp SaveEmailAsPointOfContact(SaveEmailAsPointOfContactReq Request)
		{
			return this.WrapServiceMethod<SaveEmailAsPointOfContactResp>(() => this.Proxy.SaveEmailAsPointOfContact(Request));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000071A0 File Offset: 0x000053A0
		public CreatePointOfContactFromMessageResp CreatePointOfContactFromMessage(CreatePointOfContactFromMessageReq Request)
		{
			return this.WrapServiceMethod<CreatePointOfContactFromMessageResp>(() => this.Proxy.CreatePointOfContactFromMessage(Request));
		}
	}
}
