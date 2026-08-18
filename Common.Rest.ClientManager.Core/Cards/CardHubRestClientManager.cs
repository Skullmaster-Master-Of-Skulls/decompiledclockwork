using System;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos;
using TechnoPro.Common.ClientManager.ICore.Cards;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Cards
{
	// Token: 0x02000065 RID: 101
	public class CardHubRestClientManager : BearerTokenRestProxy<ICardHubClientManager>, ICardHubClientManager, IWebService
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public CardHubRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B8D2 File Offset: 0x00009AD2
		public CardHubRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public async Task<CardInfoDTO[]> LoadLoggedInUsersCardsAsync()
		{
			return (await this.GetManyAsync<CardInfoDTO>("cardhub/loggedinuserscards", true).ConfigureAwait(false)).ToArray<CardInfoDTO>();
		}
	}
}
