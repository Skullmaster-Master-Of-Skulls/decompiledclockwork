using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x0200012F RID: 303
	public class AppointmentShowTimeAsManager : IAppointmentShowTimeAsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x0005938E File Offset: 0x0005758E
		public AppointmentShowTimeAsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentShowTimeAsDAO(opContext);
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000593AC File Offset: 0x000575AC
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x000593B4 File Offset: 0x000575B4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CCE RID: 3278 RVA: 0x000593C0 File Offset: 0x000575C0
		public IList<AppShowTimeAsType> LoadAllShowTimeAsTypes()
		{
			return this.dao.LoadAllShowTimeAsTypes();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000593E0 File Offset: 0x000575E0
		public AppShowTimeAsType LoadShowTimeAsTypeByAppCode(int AppCode)
		{
			return this.dao.LoadShowTimeAsTypeByAppCode(AppCode);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000593FE File Offset: 0x000575FE
		public void DeleteShowTimeAsTypeByAppCode(int AppCode)
		{
			this.dao.DeleteShowTimeAsTypeByAppCode(AppCode);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0005940E File Offset: 0x0005760E
		public void UpdateShowTimeAsType(AppShowTimeAsType ShowTimeAsType)
		{
			this.dao.UpdateShowTimeAsType(ShowTimeAsType);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00059420 File Offset: 0x00057620
		public int CreateShowTimeAsType(AppShowTimeAsType ShowTimeAsType)
		{
			return this.dao.CreateShowTimeAsType(ShowTimeAsType);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00059440 File Offset: 0x00057640
		public AppShowTimeAsType LoadShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			return this.dao.LoadShowTimeAsTypeById(AppointmentShowTimeAsId);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0005945E File Offset: 0x0005765E
		public void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			this.dao.DeleteShowTimeAsTypeById(AppointmentShowTimeAsId);
		}

		// Token: 0x0400026B RID: 619
		private IAppointmentShowTimeAsDAO dao;
	}
}
