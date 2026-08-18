using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000066 RID: 102
	public class AcademicTermServiceManager : IAcademicTerm, IService
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0001173C File Offset: 0x0000F93C
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			return new GetCurrentAcademicTermResp
			{
				AcademicTerm = academicTermManager.GetCurrentAcademicTerm().ToDTO()
			};
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011774 File Offset: 0x0000F974
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			IList<AcademicTerm> source = academicTermManager.LoadAcademicTerms(request.IgnoreCache);
			LoadAcademicTermsResp loadAcademicTermsResp = new LoadAcademicTermsResp();
			loadAcademicTermsResp.AcademicTerms = source.ToList<AcademicTerm>().ConvertAll<AcademicTermDTO>((AcademicTerm f) => f.ToDTO());
			return loadAcademicTermsResp;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000117D8 File Offset: 0x0000F9D8
		public GetAcademicTermResp GetAcademicTerm(GetAcademicTermReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			return new GetAcademicTermResp
			{
				AcademicTerm = academicTermManager.GetAcademicTerm(request.Date).ToDTO()
			};
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011814 File Offset: 0x0000FA14
		public ChangeCurrentAcademicTermsResp ChangeCurrentAcademicTerms(ChangeCurrentAcademicTermsReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			academicTermManager.ChangeCurrentAcademicTerms((from g in request.AcademicTermList
			select g.ToDomainObject()).ToList<AcademicTerm>());
			return new ChangeCurrentAcademicTermsResp();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011870 File Offset: 0x0000FA70
		public ValidateAcademicTermListResp ValidateAcademicTermList(ValidateAcademicTermListReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			eSessionListValidationResult validationResult = academicTermManager.ValidateAcademicTermList((from g in request.ProposedTermsList
			select g.ToDomainObject()).ToList<AcademicTerm>());
			return new ValidateAcademicTermListResp
			{
				ValidationResult = validationResult
			};
		}
	}
}
