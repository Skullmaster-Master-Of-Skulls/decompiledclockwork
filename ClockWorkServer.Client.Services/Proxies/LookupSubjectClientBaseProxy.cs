using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DA RID: 218
	internal class LookupSubjectClientBaseProxy : ClientBase<ILookupSubject>, ILookupSubject, IService
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x00016450 File Offset: 0x00014650
		public LookupSubjectClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001645B File Offset: 0x0001465B
		public LookupSubjectClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00016468 File Offset: 0x00014668
		public LoadLookupSubjectResp LoadLookupSubject(LoadLookupSubjectReq Request)
		{
			return base.Channel.LoadLookupSubject(Request);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00016488 File Offset: 0x00014688
		public LoadLookupSubjectByIdResp LoadLookupSubjectById(LoadLookupSubjectByIdReq Request)
		{
			return base.Channel.LoadLookupSubjectById(Request);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000164A8 File Offset: 0x000146A8
		public LoadLookupSubjectBySubjectCodeResp LoadLookupSubjectBySubjectCode(LoadLookupSubjectBySubjectCodeReq Request)
		{
			return base.Channel.LoadLookupSubjectBySubjectCode(Request);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000164C8 File Offset: 0x000146C8
		public LoadLookupSubjectBySubjectDescriptionResp LoadLookupSubjectBySubjectDescription(LoadLookupSubjectBySubjectDescriptionReq Request)
		{
			return base.Channel.LoadLookupSubjectBySubjectDescription(Request);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000164E8 File Offset: 0x000146E8
		public LoadLookupSubjectsBySessionResp LoadLookupSubjectsBySession(LoadLookupSubjectsBySessionReq Request)
		{
			return base.Channel.LoadLookupSubjectsBySession(Request);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00016508 File Offset: 0x00014708
		public SaveSubjectResp SaveSubject(SaveSubjectReq Request)
		{
			return base.Channel.SaveSubject(Request);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00016528 File Offset: 0x00014728
		public LoadAllLookupSubjectsResp LoadAllLookupSubjects(LoadAllLookupSubjectsReq Request)
		{
			return base.Channel.LoadAllLookupSubjects(Request);
		}
	}
}
