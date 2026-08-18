using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.ClientManager.ICore.Campus;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Campus
{
	// Token: 0x02000066 RID: 102
	public class CampusRestClientManager : BearerTokenRestProxy<ICampusClientManager>, ICampusClientManager, IWebService
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000B925 File Offset: 0x00009B25
		public CampusRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B92F File Offset: 0x00009B2F
		public CampusRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B93A File Offset: 0x00009B3A
		public IList<SchoolCampusDTO> GetCampusList()
		{
			return base.GetMany<SchoolCampusDTO>("campus", true);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B948 File Offset: 0x00009B48
		public int CreateCampus(SchoolCampusDTO campus)
		{
			return base.Post<SchoolCampusDTO, int>(campus, "campus");
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000B956 File Offset: 0x00009B56
		public void UpdateCampus(SchoolCampusDTO campus)
		{
			base.Put<SchoolCampusDTO>(campus, "campus");
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B964 File Offset: 0x00009B64
		public void DeleteCampus(int campusId)
		{
			base.Delete(string.Format("campus/id/{0}", campusId));
		}
	}
}
