using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Cards
{
	// Token: 0x02000073 RID: 115
	public interface ICardHubClientManager : IWebService
	{
		// Token: 0x0600035C RID: 860
		Task<CardInfoDTO[]> LoadLoggedInUsersCardsAsync();
	}
}
