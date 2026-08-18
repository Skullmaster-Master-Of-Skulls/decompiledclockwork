using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000032 RID: 50
	public class AcademicTermRestClientManager : BearerTokenRestProxy<IAcademicTermClientManager>, IAcademicTermClientManager, IWebService
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000068D1 File Offset: 0x00004AD1
		public AcademicTermRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000068DB File Offset: 0x00004ADB
		public AcademicTermRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000068E6 File Offset: 0x00004AE6
		public AcademicTermDTO GetCurrentAcademicTerm()
		{
			return base.Get<AcademicTermDTO>("session/currentacademicterm", true);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000068F4 File Offset: 0x00004AF4
		public IList<AcademicTermDTO> LoadAcademicTerms()
		{
			return base.GetMany<AcademicTermDTO>("session/academicterms", true);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006902 File Offset: 0x00004B02
		public AcademicTermDTO GetAcademicTerm(DateTime date)
		{
			return base.Get<AcademicTermDTO>(string.Format("session/academicterm/date/{0}", date), true);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000691C File Offset: 0x00004B1C
		public void ChangeCurrentAcademicTerms(IList<AcademicTermListItem> newAcademicTermList)
		{
			ChangeCurrentAcademicTermsReq changeCurrentAcademicTermsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeCurrentAcademicTermsReq>();
			changeCurrentAcademicTermsReq.AcademicTermList = newAcademicTermList;
			base.Post<ChangeCurrentAcademicTermsReq>(changeCurrentAcademicTermsReq, "session/changecurrentacademicterms");
		}
	}
}
