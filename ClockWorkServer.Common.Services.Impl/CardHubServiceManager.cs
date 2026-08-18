using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000029 RID: 41
	public class CardHubServiceManager : ICardHub, IService
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00008F84 File Offset: 0x00007184
		[DebuggerStepThrough]
		public Task<LoadLoggedInUsersCardsResp> LoadLoggedInUsersCardsAsync(LoadLoggedInUsersCardsReq Request)
		{
			CardHubServiceManager.<LoadLoggedInUsersCardsAsync>d__0 <LoadLoggedInUsersCardsAsync>d__ = new CardHubServiceManager.<LoadLoggedInUsersCardsAsync>d__0();
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadLoggedInUsersCardsResp>.Create();
			<LoadLoggedInUsersCardsAsync>d__.<>4__this = this;
			<LoadLoggedInUsersCardsAsync>d__.Request = Request;
			<LoadLoggedInUsersCardsAsync>d__.<>1__state = -1;
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder.Start<CardHubServiceManager.<LoadLoggedInUsersCardsAsync>d__0>(ref <LoadLoggedInUsersCardsAsync>d__);
			return <LoadLoggedInUsersCardsAsync>d__.<>t__builder.Task;
		}
	}
}
