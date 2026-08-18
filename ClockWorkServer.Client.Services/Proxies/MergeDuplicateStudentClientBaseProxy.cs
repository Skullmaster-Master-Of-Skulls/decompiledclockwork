using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F0 RID: 240
	internal class MergeDuplicateStudentClientBaseProxy : ClientBase<IMergeDuplicateStudent>, IMergeDuplicateStudent, IService
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x00017B54 File Offset: 0x00015D54
		public MergeDuplicateStudentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00017B5F File Offset: 0x00015D5F
		public MergeDuplicateStudentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00017B6C File Offset: 0x00015D6C
		public FindPotentialDuplicateStudentsResp FindPotentialDuplicateStudents(FindPotentialDuplicateStudentsReq Request)
		{
			return base.Channel.FindPotentialDuplicateStudents(Request);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00017B8C File Offset: 0x00015D8C
		public LoadDuplicateStudentPreviewInfoResp LoadDuplicateStudentPreviewInfo(LoadDuplicateStudentPreviewInfoReq Request)
		{
			return base.Channel.LoadDuplicateStudentPreviewInfo(Request);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00017BAC File Offset: 0x00015DAC
		public MergeDuplicateStudentsResp MergeDuplicateStudents(MergeDuplicateStudentsReq Request)
		{
			return base.Channel.MergeDuplicateStudents(Request);
		}
	}
}
