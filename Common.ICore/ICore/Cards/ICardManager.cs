using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Cards.CardInfos;

namespace TechnoPro.Common.ICore.Cards
{
	// Token: 0x020000D7 RID: 215
	public interface ICardManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006A8 RID: 1704
		Task<CardInfo[]> LoadLoggedInUsersCardsAsync();
	}
}
