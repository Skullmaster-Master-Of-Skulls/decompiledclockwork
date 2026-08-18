using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001A RID: 26
	public class CampusManager : ICampusManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004527 File Offset: 0x00002727
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000452F File Offset: 0x0000272F
		private ICampusDAO CampusDAO { get; set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00004538 File Offset: 0x00002738
		public CampusManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.CampusDAO = new CampusDAO(opContext);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004557 File Offset: 0x00002757
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000455F File Offset: 0x0000275F
		public OperationContext OpContext { get; set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00004568 File Offset: 0x00002768
		public IList<SchoolCampus> GetCampusList()
		{
			return this.CampusDAO.GetCampusList();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004588 File Offset: 0x00002788
		public int CreateCampus(SchoolCampus campus)
		{
			return this.CampusDAO.CreateCampus(campus);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000045A6 File Offset: 0x000027A6
		public void UpdateCampus(SchoolCampus campus)
		{
			this.CampusDAO.UpdateCampus(campus);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000045B6 File Offset: 0x000027B6
		public void DeleteCampus(int campusId)
		{
			this.CampusDAO.DeleteCampus(campusId);
		}
	}
}
