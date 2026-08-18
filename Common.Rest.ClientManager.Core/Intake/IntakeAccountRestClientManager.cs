using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.ClientManager.ICore.Intake;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Intake
{
	// Token: 0x0200004A RID: 74
	public class IntakeAccountRestClientManager : BearerTokenRestProxy<IIntakeAccountClientManager>, IIntakeAccountClientManager, IWebService
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000816C File Offset: 0x0000636C
		public IntakeAccountRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008176 File Offset: 0x00006376
		public IntakeAccountRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008181 File Offset: 0x00006381
		public int CreateNewIntakeAccount(IntakeUserAccountDTO UserAccount)
		{
			return base.Post<IntakeUserAccountDTO, int>(UserAccount, "intakeaccount");
		}
	}
}
