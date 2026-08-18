using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Cards;
using TechnoPro.Common.Public.Entities.Cards.CardInfos;

namespace TechnoPro.Common.Core.Cards.CardLoaders
{
	// Token: 0x02000123 RID: 291
	public class CardLoaderVetsApplicationsStudent : ICardLoader
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x00055CA8 File Offset: 0x00053EA8
		[DebuggerStepThrough]
		public Task<CardInfo> LoadCardInfo(OperationContext opContext, CardLayout cardLayout, int studentPersonId)
		{
			CardLoaderVetsApplicationsStudent.<LoadCardInfo>d__0 <LoadCardInfo>d__ = new CardLoaderVetsApplicationsStudent.<LoadCardInfo>d__0();
			<LoadCardInfo>d__.<>t__builder = AsyncTaskMethodBuilder<CardInfo>.Create();
			<LoadCardInfo>d__.<>4__this = this;
			<LoadCardInfo>d__.opContext = opContext;
			<LoadCardInfo>d__.cardLayout = cardLayout;
			<LoadCardInfo>d__.studentPersonId = studentPersonId;
			<LoadCardInfo>d__.<>1__state = -1;
			<LoadCardInfo>d__.<>t__builder.Start<CardLoaderVetsApplicationsStudent.<LoadCardInfo>d__0>(ref <LoadCardInfo>d__);
			return <LoadCardInfo>d__.<>t__builder.Task;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00055D04 File Offset: 0x00053F04
		[DebuggerStepThrough]
		private Task<CardInfoVetsApplicationsStudent> LoadStudentVeteranCardInfoAsync(OperationContext opContext, CardLayout cardLayout, int studentPersonId)
		{
			CardLoaderVetsApplicationsStudent.<LoadStudentVeteranCardInfoAsync>d__1 <LoadStudentVeteranCardInfoAsync>d__ = new CardLoaderVetsApplicationsStudent.<LoadStudentVeteranCardInfoAsync>d__1();
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CardInfoVetsApplicationsStudent>.Create();
			<LoadStudentVeteranCardInfoAsync>d__.<>4__this = this;
			<LoadStudentVeteranCardInfoAsync>d__.opContext = opContext;
			<LoadStudentVeteranCardInfoAsync>d__.cardLayout = cardLayout;
			<LoadStudentVeteranCardInfoAsync>d__.studentPersonId = studentPersonId;
			<LoadStudentVeteranCardInfoAsync>d__.<>1__state = -1;
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Start<CardLoaderVetsApplicationsStudent.<LoadStudentVeteranCardInfoAsync>d__1>(ref <LoadStudentVeteranCardInfoAsync>d__);
			return <LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Task;
		}
	}
}
