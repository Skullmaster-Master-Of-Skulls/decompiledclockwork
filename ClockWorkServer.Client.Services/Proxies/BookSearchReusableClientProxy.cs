using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000006 RID: 6
	public class BookSearchReusableClientProxy : WCFTokenBasedReusableClientProxy<IBookSearch>, IBookSearch, IService
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000026D6 File Offset: 0x000008D6
		public BookSearchReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026E1 File Offset: 0x000008E1
		public BookSearchReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026F0 File Offset: 0x000008F0
		public Task<SearchForVolumesResp> SearchForVolumesAsync(SearchForVolumesReq request)
		{
			return this.WrapServiceMethod<Task<SearchForVolumesResp>>(() => this.Proxy.SearchForVolumesAsync(request));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002728 File Offset: 0x00000928
		public SearchForVolumesResp SearchForVolumes(SearchForVolumesReq request)
		{
			return this.WrapServiceMethod<SearchForVolumesResp>(() => this.Proxy.SearchForVolumes(request));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002760 File Offset: 0x00000960
		public Task<GetVolumeByISBNResp> GetVolumeByISBNAsync(GetVolumeByISBNReq request)
		{
			return this.WrapServiceMethod<Task<GetVolumeByISBNResp>>(() => this.Proxy.GetVolumeByISBNAsync(request));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002798 File Offset: 0x00000998
		public GetVolumeByISBNResp GetVolumeByISBN(GetVolumeByISBNReq request)
		{
			return this.WrapServiceMethod<GetVolumeByISBNResp>(() => this.Proxy.GetVolumeByISBN(request));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027D0 File Offset: 0x000009D0
		public Task<GetVolumeByIdResp> GetVolumeByIdAsync(GetVolumeByIdReq request)
		{
			return this.WrapServiceMethod<Task<GetVolumeByIdResp>>(() => this.Proxy.GetVolumeByIdAsync(request));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002808 File Offset: 0x00000A08
		public GetVolumeByIdResp GetVolumeById(GetVolumeByIdReq request)
		{
			return this.WrapServiceMethod<GetVolumeByIdResp>(() => this.Proxy.GetVolumeById(request));
		}
	}
}
