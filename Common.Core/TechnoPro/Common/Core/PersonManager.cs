using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core
{
	// Token: 0x02000021 RID: 33
	public class PersonManager : IPersonManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000669E File Offset: 0x0000489E
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000066A6 File Offset: 0x000048A6
		public IPersonDAO dao { get; set; }

		// Token: 0x0600010E RID: 270 RVA: 0x000066AF File Offset: 0x000048AF
		public PersonManager()
		{
			this.dao = new PersonDAO(this.OpContext);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000066CB File Offset: 0x000048CB
		public PersonManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PersonDAO(opContext);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000066EA File Offset: 0x000048EA
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000066F2 File Offset: 0x000048F2
		public OperationContext OpContext { get; set; }

		// Token: 0x06000112 RID: 274 RVA: 0x000066FC File Offset: 0x000048FC
		public List<Person> GetPersonsByGroup(int groupid)
		{
			return this.dao.GetPersonsByGroup(groupid);
		}
	}
}
