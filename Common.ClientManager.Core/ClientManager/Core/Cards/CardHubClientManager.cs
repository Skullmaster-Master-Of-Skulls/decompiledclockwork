using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos;
using TechnoPro.Common.ClientManager.ICore.Cards;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.Core.Cards
{
	// Token: 0x0200007A RID: 122
	public class CardHubClientManager : ICardHubClientManager, IWebService
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0001482C File Offset: 0x00012A2C
		[DebuggerStepThrough]
		public Task<CardInfoDTO[]> LoadLoggedInUsersCardsAsync()
		{
			CardHubClientManager.<LoadLoggedInUsersCardsAsync>d__0 <LoadLoggedInUsersCardsAsync>d__ = new CardHubClientManager.<LoadLoggedInUsersCardsAsync>d__0();
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CardInfoDTO[]>.Create();
			<LoadLoggedInUsersCardsAsync>d__.<>4__this = this;
			<LoadLoggedInUsersCardsAsync>d__.<>1__state = -1;
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder.Start<CardHubClientManager.<LoadLoggedInUsersCardsAsync>d__0>(ref <LoadLoggedInUsersCardsAsync>d__);
			return <LoadLoggedInUsersCardsAsync>d__.<>t__builder.Task;
		}
	}
}
