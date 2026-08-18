using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.ClientManager.ICore.Cases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Cases
{
	// Token: 0x02000064 RID: 100
	public class CasesRestClientManager : BearerTokenRestProxy<ICasesClientManager>, ICasesClientManager, IWebService
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000B83B File Offset: 0x00009A3B
		public CasesRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B845 File Offset: 0x00009A45
		public CasesRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B850 File Offset: 0x00009A50
		public IList<CaseForDisplayDTO> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn)
		{
			return base.GetMany<CaseForDisplayDTO>(string.Format("cases/fordisplayforstudent/pid/{0}/screennum/{1}/controlidsfordynamicformsummaryitems/{2}", PersonId, ScreenNum, controlIdsToAddToColumn.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000B875 File Offset: 0x00009A75
		public CaseDTO LoadCaseById(int InfoPcId, int ScreenNum)
		{
			return base.Get<CaseDTO>(string.Format("cases/infopcid/{0}/screennum/{1}", InfoPcId, ScreenNum), true);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B894 File Offset: 0x00009A94
		public int CreateCase(CaseDTO Case)
		{
			return base.Post<CaseDTO, int>(Case, "cases");
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B8A2 File Offset: 0x00009AA2
		public void DeleteCase(int InfoPcId)
		{
			base.Delete(string.Format("cases/id/{0}", InfoPcId));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B8BA File Offset: 0x00009ABA
		public void UpdateCase(CaseDTO Case)
		{
			base.Put<CaseDTO>(Case, "cases");
		}
	}
}
