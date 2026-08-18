using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000007 RID: 7
	internal class BookSearchClientBaseProxy : ClientBase<IBookSearch>, IBookSearch, IService
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002840 File Offset: 0x00000A40
		public BookSearchClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000284B File Offset: 0x00000A4B
		public BookSearchClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002858 File Offset: 0x00000A58
		public Task<SearchForVolumesResp> SearchForVolumesAsync(SearchForVolumesReq request)
		{
			return base.Channel.SearchForVolumesAsync(request);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002878 File Offset: 0x00000A78
		public SearchForVolumesResp SearchForVolumes(SearchForVolumesReq request)
		{
			return base.Channel.SearchForVolumes(request);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002898 File Offset: 0x00000A98
		public Task<GetVolumeByISBNResp> GetVolumeByISBNAsync(GetVolumeByISBNReq request)
		{
			return base.Channel.GetVolumeByISBNAsync(request);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028B8 File Offset: 0x00000AB8
		public GetVolumeByISBNResp GetVolumeByISBN(GetVolumeByISBNReq request)
		{
			return base.Channel.GetVolumeByISBN(request);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028D8 File Offset: 0x00000AD8
		public Task<GetVolumeByIdResp> GetVolumeByIdAsync(GetVolumeByIdReq request)
		{
			return base.Channel.GetVolumeByIdAsync(request);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028F8 File Offset: 0x00000AF8
		public GetVolumeByIdResp GetVolumeById(GetVolumeByIdReq request)
		{
			return base.Channel.GetVolumeById(request);
		}
	}
}
