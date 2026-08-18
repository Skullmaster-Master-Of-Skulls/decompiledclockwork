using System;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000DD RID: 221
	public class LegacyServiceProviderManager : ILegacyServiceProviderManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x00038DD3 File Offset: 0x00036FD3
		public LegacyServiceProviderManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00038DE5 File Offset: 0x00036FE5
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00038DED File Offset: 0x00036FED
		public OperationContext OpContext { get; set; }

		// Token: 0x06000876 RID: 2166 RVA: 0x00038DF8 File Offset: 0x00036FF8
		public LegacyRequestDetailNotesAndSpecialInstructions LoadRequestDetailNotesAndSpecialInstructions(int RequestId)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			return legacyServiceProviderDAO.LoadRequestDetailNotesAndSpecialInstructions(RequestId);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00038E20 File Offset: 0x00037020
		public void UpdateRequest(LegacyServiceProviderRequestDetail RequestDetail)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			legacyServiceProviderDAO.UpdateRequest(RequestDetail);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00038E44 File Offset: 0x00037044
		public void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructions notesAndSpecialInstructions)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			legacyServiceProviderDAO.UpdateRequestDetailNotesAndSpecialInstructions(notesAndSpecialInstructions);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00038E68 File Offset: 0x00037068
		public void UpdateRequestNotes(int RequestId, string notes)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			legacyServiceProviderDAO.UpdateRequestNotes(RequestId, notes);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00038E8C File Offset: 0x0003708C
		public void UpdateProvider(ServiceProvider provider)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			legacyServiceProviderDAO.UpdateProvider(provider);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00038EB0 File Offset: 0x000370B0
		public int CreateProvider(ServiceProvider provider)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			return legacyServiceProviderDAO.CreateProvider(provider);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00038ED8 File Offset: 0x000370D8
		public ServiceProvider LoadProvider(int serviceProviderId)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			return legacyServiceProviderDAO.LoadProvider(serviceProviderId);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00038F00 File Offset: 0x00037100
		public int LoadProviderIdByStudentNumber(string snum)
		{
			ILegacyServiceProviderDAO legacyServiceProviderDAO = new LegacyServiceProviderDAO(this.OpContext);
			return legacyServiceProviderDAO.LoadProviderIdByStudentNumber(snum);
		}
	}
}
