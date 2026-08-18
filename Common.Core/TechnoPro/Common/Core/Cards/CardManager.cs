using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.Cards;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Cards;
using TechnoPro.Common.Public.Entities.Cards.CardInfos;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Cards
{
	// Token: 0x02000122 RID: 290
	public class CardManager : ICardManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00055A03 File Offset: 0x00053C03
		public CardManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00055A15 File Offset: 0x00053C15
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00055A1D File Offset: 0x00053C1D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C41 RID: 3137 RVA: 0x00055A28 File Offset: 0x00053C28
		private IDictionary<eCardType, CardLayout> GetPreferredCardTypesAndLayoutsForUser(int personId)
		{
			bool flag = personId < 1;
			IDictionary<eCardType, CardLayout> result;
			if (flag)
			{
				result = new Dictionary<eCardType, CardLayout>();
			}
			else
			{
				result = (from g in (eCardType[])Enum.GetValues(typeof(eCardType))
				where !g.GetAttribute<CardTypeAttribute>().IsDisabled
				select g).ToDictionary((eCardType g) => g, (eCardType g) => new CardLayout());
			}
			return result;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00055AC8 File Offset: 0x00053CC8
		private IDictionary<eCardType, CardLayout> GetPreferredAndAllowedCardTypesForUser(int personId)
		{
			CardManager.<>c__DisplayClass6_0 CS$<>8__locals1 = new CardManager.<>c__DisplayClass6_0();
			IDictionary<eCardType, CardLayout> preferredCardTypesAndLayoutsForUser = this.GetPreferredCardTypesAndLayoutsForUser(personId);
			CS$<>8__locals1.pm = new PeopleManager(this.OpContext);
			PersonBase personBase = CS$<>8__locals1.pm.LoadPerson(personId);
			CardManager.<>c__DisplayClass6_0 CS$<>8__locals2 = CS$<>8__locals1;
			int[] personGroupids;
			if (personBase == null)
			{
				personGroupids = null;
			}
			else
			{
				List<Group> groups = personBase.Groups;
				if (groups == null)
				{
					personGroupids = null;
				}
				else
				{
					personGroupids = (from g in groups
					select g.GroupId).ToArray<int>();
				}
			}
			CS$<>8__locals2.personGroupids = personGroupids;
			return preferredCardTypesAndLayoutsForUser.Where(delegate(KeyValuePair<eCardType, CardLayout> g)
			{
				eCoreGroup[] array = g.Key.GetAttribute<CardTypeAttribute>().AllowedCoreGroups ?? new eCoreGroup[0];
				return (array.Length != 0 && array[0] == eCoreGroup.Unknown) || CS$<>8__locals1.pm.IsPersonInAtLeastOneCoreGroup(CS$<>8__locals1.personGroupids, array);
			}).ToDictionary((KeyValuePair<eCardType, CardLayout> m) => m.Key, (KeyValuePair<eCardType, CardLayout> m) => m.Value);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00055BA0 File Offset: 0x00053DA0
		[DebuggerStepThrough]
		public Task<CardInfo[]> LoadLoggedInUsersCardsAsync()
		{
			CardManager.<LoadLoggedInUsersCardsAsync>d__7 <LoadLoggedInUsersCardsAsync>d__ = new CardManager.<LoadLoggedInUsersCardsAsync>d__7();
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CardInfo[]>.Create();
			<LoadLoggedInUsersCardsAsync>d__.<>4__this = this;
			<LoadLoggedInUsersCardsAsync>d__.<>1__state = -1;
			<LoadLoggedInUsersCardsAsync>d__.<>t__builder.Start<CardManager.<LoadLoggedInUsersCardsAsync>d__7>(ref <LoadLoggedInUsersCardsAsync>d__);
			return <LoadLoggedInUsersCardsAsync>d__.<>t__builder.Task;
		}
	}
}
