using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D1 RID: 209
	public class AlternateContactManager : IAlternateContactManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0003709D File Offset: 0x0003529D
		public AlternateContactManager(OperationContext opContext)
		{
			this.dao = new AlternateContactDAO(opContext);
			this.OpContext = opContext;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000370BB File Offset: 0x000352BB
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x000370C3 File Offset: 0x000352C3
		public OperationContext OpContext { get; set; }

		// Token: 0x060007D6 RID: 2006 RVA: 0x000370CC File Offset: 0x000352CC
		public int CreateAlternateContact(AlternateContact AltContact)
		{
			return this.dao.CreateAlternateContact(AltContact);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000370EC File Offset: 0x000352EC
		public AlternateContact LoadAlternateContactById(int AlternateContactId)
		{
			return this.dao.LoadAlternateContactById(AlternateContactId);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003710C File Offset: 0x0003530C
		public IList<AlternateContact> LoadAlternateContactsByCourse(int LuCourseId)
		{
			return this.dao.LoadAlternateContactsByCourse(LuCourseId);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0003712C File Offset: 0x0003532C
		public IList<AlternateContact> LoadAlternateContactsBySearchString(string SearchString)
		{
			return this.dao.LoadAlternateContactsBySearchString(SearchString);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0003714A File Offset: 0x0003534A
		public void UpdateAlternateContact(AlternateContact AltContact)
		{
			this.dao.UpdateAlternateContact(AltContact);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0003715A File Offset: 0x0003535A
		public void DeleteAlternateContact(int AlternateContactId)
		{
			this.dao.DeleteAlternateContact(AlternateContactId);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003716C File Offset: 0x0003536C
		public AlternateContact LoadAlternateContactByUsername(string Username)
		{
			return this.dao.LoadAlternateContactByUsername(Username);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003718A File Offset: 0x0003538A
		public void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId)
		{
			this.dao.AssignAlternateContactToCourse(AlternateContactId, LuCourseId);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0003719B File Offset: 0x0003539B
		public void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId)
		{
			this.dao.RemoveAlternateContactFromCourse(AlternateContactId, LuCourseId);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000371AC File Offset: 0x000353AC
		public AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId)
		{
			return this.dao.LoadAlternateContactByEmployeeId(EmployeeId);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000371CC File Offset: 0x000353CC
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId)
		{
			return this.dao.GetUniqueCourseRegistrationStartDatesByAlternateContact(AlternateContactId);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000371EC File Offset: 0x000353EC
		public AlternateContact LoadAlternateContactByEmail(string Email)
		{
			return this.dao.LoadAlternateContactByEmail(Email);
		}

		// Token: 0x04000174 RID: 372
		private IAlternateContactDAO dao;
	}
}
