using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Cards;
using TechnoPro.Common.Public.Entities.Cards.CardInfos;

namespace TechnoPro.Common.Core.Cards.CardLoaders
{
	// Token: 0x02000124 RID: 292
	public interface ICardLoader
	{
		// Token: 0x06000C48 RID: 3144
		Task<CardInfo> LoadCardInfo(OperationContext opContext, CardLayout cardLayout, int studentPersonId);
	}
}
