using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000023 RID: 35
	internal class PointOfContactClientBaseProxy : ClientBase<IPointOfContact>, IPointOfContact, IService
	{
		// Token: 0x060001FE RID: 510 RVA: 0x000071D8 File Offset: 0x000053D8
		public PointOfContactClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000071E3 File Offset: 0x000053E3
		public PointOfContactClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000071F0 File Offset: 0x000053F0
		public CreatePointOfContactResp CreatePointOfContact(CreatePointOfContactReq Request)
		{
			return base.Channel.CreatePointOfContact(Request);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000720E File Offset: 0x0000540E
		public void DeletePointOfContact(DeletePointOfContactReq Request)
		{
			base.Channel.DeletePointOfContact(Request);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007220 File Offset: 0x00005420
		public LoadPointOfContactByIdResp LoadPointOfContactById(LoadPointOfContactByIdReq Request)
		{
			return base.Channel.LoadPointOfContactById(Request);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000723E File Offset: 0x0000543E
		public void UpdatePointOfContact(UpdatePointOfContactReq Request)
		{
			base.Channel.UpdatePointOfContact(Request);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007250 File Offset: 0x00005450
		public SaveEmailAsPointOfContactResp SaveEmailAsPointOfContact(SaveEmailAsPointOfContactReq Request)
		{
			return base.Channel.SaveEmailAsPointOfContact(Request);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007270 File Offset: 0x00005470
		public CreatePointOfContactFromMessageResp CreatePointOfContactFromMessage(CreatePointOfContactFromMessageReq Request)
		{
			return base.Channel.CreatePointOfContactFromMessage(Request);
		}
	}
}
