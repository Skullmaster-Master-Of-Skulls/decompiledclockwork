using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000033 RID: 51
	public class AlternateContactRestClientManager : BearerTokenRestProxy<IAlternateContactClientManager>, IAlternateContactClientManager, IWebService
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00006947 File Offset: 0x00004B47
		public AlternateContactRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006951 File Offset: 0x00004B51
		public AlternateContactRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000695C File Offset: 0x00004B5C
		public int CreateAlternateContact(AlternateContactDTO AltContact)
		{
			return base.Post<AlternateContactDTO, int>(AltContact, "alternatecontact");
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000696A File Offset: 0x00004B6A
		public AlternateContactDTO LoadAlternateContactById(int AlternateContactId)
		{
			return base.Get<AlternateContactDTO>(string.Format("alternatecontact/alternatecontactid/{0}", AlternateContactId), true);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006983 File Offset: 0x00004B83
		public AlternateContactDTO LoadAlternateContactByEmployeeId(string EmployeeId)
		{
			return base.Get<AlternateContactDTO>(string.Format("alternatecontact/employeeid/{0}", EmployeeId), true);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006997 File Offset: 0x00004B97
		public IList<AlternateContactDTO> LoadAlternateContactsByCourse(int LuCourseId)
		{
			return base.GetMany<AlternateContactDTO>(string.Format("alternatecontact/lucourseid/{0}", LuCourseId), true);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000069B0 File Offset: 0x00004BB0
		public IList<AlternateContactDTO> LoadAlternateContactsBySearchString(string SearchString)
		{
			return base.GetMany<AlternateContactDTO>(string.Format("alternatecontact/matching?searchstring={0}", SearchString), true);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000069C4 File Offset: 0x00004BC4
		public void UpdateAlternateContact(AlternateContactDTO AltContact)
		{
			base.Put<AlternateContactDTO>(AltContact, "alternatecontact");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000069D2 File Offset: 0x00004BD2
		public void DeleteAlternateContact(int AlternateContactId)
		{
			base.Delete(string.Format("alternatecontact/alternatecontactid/{0}", AlternateContactId));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000069EA File Offset: 0x00004BEA
		public AlternateContactDTO LoadAlternateContactByUsername(string Username)
		{
			return base.Get<AlternateContactDTO>(string.Format("alternatecontact/username/{0}", Username), true);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006A00 File Offset: 0x00004C00
		public void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId)
		{
			AssignAlternateContactToCourseReq assignAlternateContactToCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignAlternateContactToCourseReq>();
			assignAlternateContactToCourseReq.AlternateContactId = AlternateContactId;
			assignAlternateContactToCourseReq.LuCourseId = LuCourseId;
			base.Post<AssignAlternateContactToCourseReq>(assignAlternateContactToCourseReq, "alternatecontact/assigntocourse");
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006A32 File Offset: 0x00004C32
		public void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId)
		{
			base.Delete(string.Format("alternatecontact/removefromcourse/alternatecontactid/{0}/lucourseid/{1}", AlternateContactId, LuCourseId));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006A50 File Offset: 0x00004C50
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId)
		{
			return base.GetMany<DateTime>(string.Format("alternatecontact/uniquecourseregistrationstartdates/alternatecontactid/{0}", AlternateContactId), true);
		}
	}
}
