using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003C RID: 60
	public class TestProctorReusableClientProxy : WCFTokenBasedReusableClientProxy<ITestProctor>, ITestProctor, IService
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00009782 File Offset: 0x00007982
		public TestProctorReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000978D File Offset: 0x0000798D
		public TestProctorReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000979C File Offset: 0x0000799C
		public LoadAllProctorsResp LoadAllProctors(LoadAllProctorsReq Request)
		{
			return this.WrapServiceMethod<LoadAllProctorsResp>(() => this.Proxy.LoadAllProctors(Request));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000097D4 File Offset: 0x000079D4
		public CreateProctorResp CreateProctor(CreateProctorReq Request)
		{
			return this.WrapServiceMethod<CreateProctorResp>(() => this.Proxy.CreateProctor(Request));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000980C File Offset: 0x00007A0C
		public void DeleteProctor(DeleteProctorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteProctor(Request);
			});
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009844 File Offset: 0x00007A44
		public void UpdateProctor(UpdateProctorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateProctor(Request);
			});
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000987C File Offset: 0x00007A7C
		public LoadProctorByIdResp LoadProctorById(LoadProctorByIdReq Request)
		{
			return this.WrapServiceMethod<LoadProctorByIdResp>(() => this.Proxy.LoadProctorById(Request));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000098B4 File Offset: 0x00007AB4
		public LoadAllReadersResp LoadAllReaders(LoadAllReadersReq Request)
		{
			return this.WrapServiceMethod<LoadAllReadersResp>(() => this.Proxy.LoadAllReaders(Request));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000098EC File Offset: 0x00007AEC
		public LoadAllScribesResp LoadAllScribes(LoadAllScribesReq Request)
		{
			return this.WrapServiceMethod<LoadAllScribesResp>(() => this.Proxy.LoadAllScribes(Request));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009924 File Offset: 0x00007B24
		public CreateReaderResp CreateReader(CreateReaderReq Request)
		{
			return this.WrapServiceMethod<CreateReaderResp>(() => this.Proxy.CreateReader(Request));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000995C File Offset: 0x00007B5C
		public CreateScribeResp CreateScribe(CreateScribeReq Request)
		{
			return this.WrapServiceMethod<CreateScribeResp>(() => this.Proxy.CreateScribe(Request));
		}
	}
}
