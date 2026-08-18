using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Core.Legacy;
using TechnoPro.Common.Core.Mappers.Legacy.ServiceProvider;
using TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005C RID: 92
	public class LegacyServiceProviderServiceManager : ILegacyServiceProvider, IService
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public LoadRequestDetailNotesAndSpecialInstructionsResp LoadRequestDetailNotesAndSpecialInstructions(LoadRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			LegacyRequestDetailNotesAndSpecialInstructions legacyRequestDetailNotesAndSpecialInstructions = legacyServiceProviderManager.LoadRequestDetailNotesAndSpecialInstructions(Request.RequestId);
			return new LoadRequestDetailNotesAndSpecialInstructionsResp
			{
				DetailNotesAndSpecialInstructions = ((legacyRequestDetailNotesAndSpecialInstructions != null) ? legacyRequestDetailNotesAndSpecialInstructions.ToDTO() : null)
			};
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public UpdateRequestDetailNotesAndSpecialInstructionsResp UpdateRequestDetailNotesAndSpecialInstructions(UpdateRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			ILegacyServiceProviderManager legacyServiceProviderManager2 = legacyServiceProviderManager;
			LegacyRequestDetailNotesAndSpecialInstructionsDTO notesAndSpecialInstructions = Request.NotesAndSpecialInstructions;
			legacyServiceProviderManager2.UpdateRequestDetailNotesAndSpecialInstructions((notesAndSpecialInstructions != null) ? notesAndSpecialInstructions.ToDomainObject() : null);
			return new UpdateRequestDetailNotesAndSpecialInstructionsResp();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FE34 File Offset: 0x0000E034
		public UpdateServiceProviderRequestResp UpdateServiceProviderRequest(UpdateServiceProviderRequestReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			ILegacyServiceProviderManager legacyServiceProviderManager2 = legacyServiceProviderManager;
			LegacyServiceProviderRequestDetail requestDetail;
			if (Request == null)
			{
				requestDetail = null;
			}
			else
			{
				LegacyServiceProviderRequestDetailDTO requestDetail2 = Request.RequestDetail;
				requestDetail = ((requestDetail2 != null) ? requestDetail2.ToDomainObject() : null);
			}
			legacyServiceProviderManager2.UpdateRequest(requestDetail);
			return new UpdateServiceProviderRequestResp();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FE78 File Offset: 0x0000E078
		public UpdateServiceProviderRequestNotesResp UpdateServiceProviderRequestNotes(UpdateServiceProviderRequestNotesReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			legacyServiceProviderManager.UpdateRequestNotes((Request != null) ? Request.RequestId : 0, (Request != null) ? Request.Notes : null);
			return new UpdateServiceProviderRequestNotesResp();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public UpdateServiceProviderResp UpdateServiceProvider(UpdateServiceProviderReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			ILegacyServiceProviderManager legacyServiceProviderManager2 = legacyServiceProviderManager;
			ServiceProvider provider;
			if (Request == null)
			{
				provider = null;
			}
			else
			{
				ServiceProviderDTO provider2 = Request.Provider;
				provider = ((provider2 != null) ? provider2.ToDomainObject() : null);
			}
			legacyServiceProviderManager2.UpdateProvider(provider);
			return new UpdateServiceProviderResp();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FF00 File Offset: 0x0000E100
		public CreateServiceProviderResp CreateServiceProvider(CreateServiceProviderReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			ILegacyServiceProviderManager legacyServiceProviderManager2 = legacyServiceProviderManager;
			ServiceProvider provider;
			if (Request == null)
			{
				provider = null;
			}
			else
			{
				ServiceProviderDTO provider2 = Request.Provider;
				provider = ((provider2 != null) ? provider2.ToDomainObject() : null);
			}
			int serviceProviderId = legacyServiceProviderManager2.CreateProvider(provider);
			return new CreateServiceProviderResp
			{
				ServiceProviderId = serviceProviderId
			};
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public LoadProviderResp LoadProvider(LoadProviderReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			ServiceProvider serviceProvider = legacyServiceProviderManager.LoadProvider((Request != null) ? Request.ServiceProviderId : 0);
			return new LoadProviderResp
			{
				Provider = ((serviceProvider != null) ? serviceProvider.ToDTO() : null)
			};
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FF98 File Offset: 0x0000E198
		public LoadProviderIdByStudentNumberResp LoadProviderIdByStudentNumber(LoadProviderIdByStudentNumberReq Request)
		{
			ILegacyServiceProviderManager legacyServiceProviderManager = new LegacyServiceProviderManager(Request.GetOperationContext());
			int serviceProviderId = legacyServiceProviderManager.LoadProviderIdByStudentNumber((Request != null) ? Request.StudentNumber : null);
			return new LoadProviderIdByStudentNumberResp
			{
				ServiceProviderId = serviceProviderId
			};
		}
	}
}
