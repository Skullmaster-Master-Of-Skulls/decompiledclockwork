using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000045 RID: 69
	public class AcademicTermClientManager : IAcademicTermClientManager, IWebService
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public AcademicTermDTO GetCurrentAcademicTerm()
		{
			GetCurrentAcademicTermReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCurrentAcademicTermReq>();
			return ClientServiceFactory.GetClientInstance<IAcademicTerm>().GetCurrentAcademicTerm(request).AcademicTerm;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public IList<AcademicTermDTO> LoadAcademicTerms(bool ignoreCache = false)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IList<AcademicTermDTO> list = ignoreCache ? null : clientCache.AllAcademicTerms;
			bool flag = list == null;
			if (flag)
			{
				LoadAcademicTermsReq loadAcademicTermsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAcademicTermsReq>();
				loadAcademicTermsReq.IgnoreCache = ignoreCache;
				list = (clientCache.AllAcademicTerms = ClientServiceFactory.GetClientInstance<IAcademicTerm>().LoadAcademicTerms(loadAcademicTermsReq).AcademicTerms);
			}
			return list;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BD00 File Offset: 0x00009F00
		public AcademicTermDTO GetAcademicTerm(DateTime date)
		{
			GetAcademicTermReq getAcademicTermReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAcademicTermReq>();
			getAcademicTermReq.Date = date;
			return ClientServiceFactory.GetClientInstance<IAcademicTerm>().GetAcademicTerm(getAcademicTermReq).AcademicTerm;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BD38 File Offset: 0x00009F38
		public eSessionListValidationResult ValidateAcademicTermList(IList<AcademicTermDTO> list)
		{
			ValidateAcademicTermListReq validateAcademicTermListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ValidateAcademicTermListReq>();
			validateAcademicTermListReq.ProposedTermsList = list;
			return ClientServiceFactory.GetClientInstance<IAcademicTerm>().ValidateAcademicTermList(validateAcademicTermListReq).ValidationResult;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BD70 File Offset: 0x00009F70
		public void ChangeCurrentAcademicTerms(IList<AcademicTermDTO> newAcademicTermList)
		{
			ChangeCurrentAcademicTermsReq changeCurrentAcademicTermsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeCurrentAcademicTermsReq>();
			changeCurrentAcademicTermsReq.AcademicTermList = newAcademicTermList;
			ClientServiceFactory.GetClientInstance<IAcademicTerm>().ChangeCurrentAcademicTerms(changeCurrentAcademicTermsReq);
		}
	}
}
