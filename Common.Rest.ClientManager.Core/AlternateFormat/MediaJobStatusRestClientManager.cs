using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000089 RID: 137
	public class MediaJobStatusRestClientManager : BearerTokenRestProxy<IMediaJobStatusClientManager>, IMediaJobStatusClientManager, IWebService
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0000FE85 File Offset: 0x0000E085
		public MediaJobStatusRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000FE8F File Offset: 0x0000E08F
		public MediaJobStatusRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000FE9A File Offset: 0x0000E09A
		public int CreateMediaJobStatus(MediaJobStatusDTO jobStatus)
		{
			return base.Post<MediaJobStatusDTO, int>(jobStatus, "mediajobstatus");
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public MediaJobStatusDTO GetMediaJobStatusByName(string jobStatusName)
		{
			return base.Get<MediaJobStatusDTO>(string.Format("mediajobstatus/{0}", jobStatusName), true);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public IList<MediaJobStatusDTO> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup)
		{
			return base.GetMany<MediaJobStatusDTO>(string.Format("mediajobstatus/bygroup/{0}", statusGroup), true);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000FED5 File Offset: 0x0000E0D5
		public IList<MediaJobStatusDTO> GetAllMediaJobStatus()
		{
			return base.GetMany<MediaJobStatusDTO>("mediajobstatus", true);
		}
	}
}
