using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003D RID: 61
	internal class TestProctorClientBaseProxy : ClientBase<ITestProctor>, ITestProctor, IService
	{
		// Token: 0x06000313 RID: 787 RVA: 0x00009994 File Offset: 0x00007B94
		public TestProctorClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000999F File Offset: 0x00007B9F
		public TestProctorClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000099AC File Offset: 0x00007BAC
		public LoadAllProctorsResp LoadAllProctors(LoadAllProctorsReq Request)
		{
			return base.Channel.LoadAllProctors(Request);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000099CC File Offset: 0x00007BCC
		public CreateProctorResp CreateProctor(CreateProctorReq Request)
		{
			return base.Channel.CreateProctor(Request);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000099EA File Offset: 0x00007BEA
		public void DeleteProctor(DeleteProctorReq Request)
		{
			base.Channel.DeleteProctor(Request);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000099FA File Offset: 0x00007BFA
		public void UpdateProctor(UpdateProctorReq Request)
		{
			base.Channel.UpdateProctor(Request);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009A0C File Offset: 0x00007C0C
		public LoadProctorByIdResp LoadProctorById(LoadProctorByIdReq Request)
		{
			return base.Channel.LoadProctorById(Request);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009A2C File Offset: 0x00007C2C
		public LoadAllReadersResp LoadAllReaders(LoadAllReadersReq Request)
		{
			return base.Channel.LoadAllReaders(Request);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009A4C File Offset: 0x00007C4C
		public LoadAllScribesResp LoadAllScribes(LoadAllScribesReq Request)
		{
			return base.Channel.LoadAllScribes(Request);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009A6C File Offset: 0x00007C6C
		public CreateReaderResp CreateReader(CreateReaderReq Request)
		{
			return base.Channel.CreateReader(Request);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009A8C File Offset: 0x00007C8C
		public CreateScribeResp CreateScribe(CreateScribeReq Request)
		{
			return base.Channel.CreateScribe(Request);
		}
	}
}
