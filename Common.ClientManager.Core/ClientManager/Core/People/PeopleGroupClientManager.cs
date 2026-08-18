using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x0200002F RID: 47
	public class PeopleGroupClientManager : IPeopleGroupClientManager, IWebService
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000862C File Offset: 0x0000682C
		public IList<PersonBaseDTO> LoadUsersByGroupTitle(string GroupTitle, string AlternateGroupTitle = null)
		{
			LoadUsersByGroupTitleReq loadUsersByGroupTitleReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUsersByGroupTitleReq>();
			loadUsersByGroupTitleReq.AlternateGroupTitle = AlternateGroupTitle;
			loadUsersByGroupTitleReq.GroupTitle = GroupTitle;
			return ClientServiceFactory.GetClientInstance<IPeopleGroup>().LoadUsersByGroupTitle(loadUsersByGroupTitleReq).Users;
		}
	}
}
