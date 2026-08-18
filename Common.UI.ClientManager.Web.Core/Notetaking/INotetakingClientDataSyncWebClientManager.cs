using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.Common.UI.Web.Entity.Notetaking;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking
{
	// Token: 0x0200000D RID: 13
	public interface INotetakingClientDataSyncWebClientManager
	{
		// Token: 0x06000028 RID: 40
		NotetakerWithExternalCoursesDTO GetNotetakerAndCourseInfo(bool ignoreCache, object currentPageObj, out GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo);
	}
}
