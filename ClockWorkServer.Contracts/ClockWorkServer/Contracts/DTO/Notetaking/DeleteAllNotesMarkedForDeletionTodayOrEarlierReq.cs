using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200041D RID: 1053
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAllNotesMarkedForDeletionTodayOrEarlierReq : BaseReportMessageReq
	{
	}
}
