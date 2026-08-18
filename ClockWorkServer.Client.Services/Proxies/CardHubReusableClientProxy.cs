using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200005B RID: 91
	public class CardHubReusableClientProxy : WCFTokenBasedReusableClientProxy<ICardHub>, ICardHub, IService
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000C0C6 File Offset: 0x0000A2C6
		public CardHubReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C0D1 File Offset: 0x0000A2D1
		public CardHubReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		[DebuggerStepThrough]
		public Task<LoadLoggedInUsersCardsResp> LoadLoggedInUsersCardsAsync(LoadLoggedInUsersCardsReq Request)
		{
			CardHubReusableClientProxy.<LoadLoggedInUsersCardsAsync>d__2 <LoadLoggedInUsersCardsAsync>d__ = new CardHubReusableClientProxy.<LoadLoggedInUsersCardsAsync>d__2();
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadLoggedInUsersCardsResp>.Create();
			<LoadLoggedInUsersCardsAsync>d__.<>4__this = this;
			<LoadLoggedInUsersCardsAsync>d__.Request = Request;
			<LoadLoggedInUsersCardsAsync>d__.<>1__state = -1;
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder.Start<CardHubReusableClientProxy.<LoadLoggedInUsersCardsAsync>d__2>(ref <LoadLoggedInUsersCardsAsync>d__);
			return <LoadLoggedInUsersCardsAsync>d__.<>t__builder.Task;
		}
	}
}
