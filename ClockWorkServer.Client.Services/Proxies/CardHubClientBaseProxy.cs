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
	// Token: 0x0200005C RID: 92
	internal class CardHubClientBaseProxy : ClientBase<ICardHub>, ICardHub, IService
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000C12B File Offset: 0x0000A32B
		public CardHubClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C136 File Offset: 0x0000A336
		public CardHubClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C144 File Offset: 0x0000A344
		[DebuggerStepThrough]
		public Task<LoadLoggedInUsersCardsResp> LoadLoggedInUsersCardsAsync(LoadLoggedInUsersCardsReq Request)
		{
			CardHubClientBaseProxy.<LoadLoggedInUsersCardsAsync>d__2 <LoadLoggedInUsersCardsAsync>d__ = new CardHubClientBaseProxy.<LoadLoggedInUsersCardsAsync>d__2();
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadLoggedInUsersCardsResp>.Create();
			<LoadLoggedInUsersCardsAsync>d__.<>4__this = this;
			<LoadLoggedInUsersCardsAsync>d__.Request = Request;
			<LoadLoggedInUsersCardsAsync>d__.<>1__state = -1;
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder.Start<CardHubClientBaseProxy.<LoadLoggedInUsersCardsAsync>d__2>(ref <LoadLoggedInUsersCardsAsync>d__);
			return <LoadLoggedInUsersCardsAsync>d__.<>t__builder.Task;
		}
	}
}
