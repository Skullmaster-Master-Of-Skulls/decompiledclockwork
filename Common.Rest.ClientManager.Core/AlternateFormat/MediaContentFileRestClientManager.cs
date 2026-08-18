using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000086 RID: 134
	public class MediaContentFileRestClientManager : BearerTokenRestProxy<IMediaContentFileClientManager>, IMediaContentFileClientManager, IWebService
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0000F085 File Offset: 0x0000D285
		public MediaContentFileRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000F08F File Offset: 0x0000D28F
		public MediaContentFileRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000F09A File Offset: 0x0000D29A
		public MediaContentFileWithoutDataDTO CreateMediaContentFileInfo(MediaContentFileWithoutDataDTO fileInfo)
		{
			return base.Post<MediaContentFileWithoutDataDTO, MediaContentFileWithoutDataDTO>(fileInfo, "mediacontentfile");
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public async Task<MediaContentFileWithoutDataDTO> CreateMediaContentFileInfoAsync(MediaContentFileWithoutDataDTO fileInfo)
		{
			return await this.PostAsync<MediaContentFileWithoutDataDTO, MediaContentFileWithoutDataDTO>(fileInfo, "mediacontentfile").ConfigureAwait(false);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
		public IList<MediaContentFileWithoutDataDTO> LoadAllMediaContenFileSortedByMediaContentFile()
		{
			return base.GetMany<MediaContentFileWithoutDataDTO>("mediacontentfile/sortedbymediacontentfile", true);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000F103 File Offset: 0x0000D303
		public IList<MediaContentFileWithoutDataDTO> LoadAllMediaContentFileSortedByMediaContent()
		{
			return base.GetMany<MediaContentFileWithoutDataDTO>("mediacontentfile/sortedbymediacontent", true);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000F111 File Offset: 0x0000D311
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByLanguage(eMediaContentLanguage language)
		{
			return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/language/{0}", language), true);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000F12A File Offset: 0x0000D32A
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0)
		{
			return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/contentid/{0}?studentid={1}", mediaContentId, studentId), true);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000F149 File Offset: 0x0000D349
		public IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> LoadMediaContentFileByStudentId(int studentId)
		{
			return base.GetMany<StudentMediaContentFileWithProofOfPurchaseInfoDTO>(string.Format("mediacontentfile/studentid/{0}", studentId), true);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000F162 File Offset: 0x0000D362
		public void UpdateMediaContentFileWithoutData(MediaContentFileWithoutDataDTO mediaContentFile)
		{
			base.Put<MediaContentFileWithoutDataDTO>(mediaContentFile, "mediacontentfile");
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000F170 File Offset: 0x0000D370
		public async Task DeleteMediaContentFileAsync(FileIdentifierDTO mediaContentFileId)
		{
			if (mediaContentFileId.FileUniqueId != null)
			{
				await this.DeleteAsync(string.Format("mediacontentfile/fileid/{0}", mediaContentFileId.FileUniqueId.Value)).ConfigureAwait(false);
			}
			else
			{
				await this.DeleteAsync(string.Format("mediacontentfile/legacyfileid/{0}/source/{1}", mediaContentFileId.LegacyId, mediaContentFileId.Source)).ConfigureAwait(false);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000F1BD File Offset: 0x0000D3BD
		public IList<MediaContentFileWithoutDataDTO> GetMediaContentFileMatching(string searchText, int lucourseid = 0)
		{
			if (lucourseid <= 0)
			{
				return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/matching?searchtext={0}", searchText), true);
			}
			return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/matching?searchtext={0}&lucourseid={1}", searchText, lucourseid), true);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000F1EE File Offset: 0x0000D3EE
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			if (studentId <= 0)
			{
				return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/mediacontentperformatid/{0}", mediaContentPerFormatId), true);
			}
			return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/mediacontentperformatid/{0}?studentid={1}", mediaContentPerFormatId, studentId), true);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000F22C File Offset: 0x0000D42C
		public async Task<IList<MediaContentFileWithoutDataDTO>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0)
		{
			IList<MediaContentFileWithoutDataDTO> result;
			if (studentId > 0)
			{
				result = await this.GetManyAsync<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/mediacontentperformatid/{0}?studentid={1}", mediaContentPerFormatId, studentId), true).ConfigureAwait(false);
			}
			else
			{
				result = await this.GetManyAsync<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/mediacontentperformatid/{0}", mediaContentPerFormatId), true).ConfigureAwait(false);
			}
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000F284 File Offset: 0x0000D484
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0)
		{
			if (studentId <= 0)
			{
				return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/contentid/{0}/format/{1}", mediaContentId, mediaContentFormat), true);
			}
			return base.GetMany<MediaContentFileWithoutDataDTO>(string.Format("mediacontentfile/contentid/{0}/format/{1}?studentid={2}", mediaContentId, mediaContentFormat, studentId), true);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		public async Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate)
		{
			return await this.GetManyAsync<StudentMediaContentFileWithProofOfPurchaseInfoDTO>(string.Format("mediacontentfile/available/studentid/{0}/start/{1}/end/{2}", studentId, startDate, endDate), true).ConfigureAwait(false);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000F338 File Offset: 0x0000D538
		public async Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate)
		{
			return await this.GetManyAsync<StudentMediaContentFileWithProofOfPurchaseInfoDTO>(string.Format("mediacontentfile/availablefiles/{0:guid}studentid/{1:int}/start/{2}/end/{3}", new object[]
			{
				mediaContentId,
				studentId,
				startDate,
				endDate
			}), true).ConfigureAwait(false);
		}
	}
}
