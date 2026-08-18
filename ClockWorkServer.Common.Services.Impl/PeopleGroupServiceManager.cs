using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000076 RID: 118
	public class PeopleGroupServiceManager : IPeopleGroup, IService
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x000149A0 File Offset: 0x00012BA0
		public LoadUsersByGroupTitleResp LoadUsersByGroupTitle(LoadUsersByGroupTitleReq Request)
		{
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(Request.GetOperationContext());
			IList<PersonBase> list = peopleGroupManager.LoadUsersByGroupTitle(Request.GroupTitle, Request.AlternateGroupTitle);
			LoadUsersByGroupTitleResp loadUsersByGroupTitleResp = new LoadUsersByGroupTitleResp();
			IList<PersonBaseDTO> users;
			if (list != null)
			{
				users = list.ToList<PersonBase>().ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			}
			else
			{
				users = null;
			}
			loadUsersByGroupTitleResp.Users = users;
			return loadUsersByGroupTitleResp;
		}
	}
}
