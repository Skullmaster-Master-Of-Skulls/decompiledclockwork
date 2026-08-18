using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013F RID: 319
	public class SittingManager : ISittingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x00069E0B File Offset: 0x0006800B
		public SittingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new SittingDAO(opContext);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00069E29 File Offset: 0x00068029
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x00069E31 File Offset: 0x00068031
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E1B RID: 3611 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateSitting(Sitting Sitting)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteSitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateSitting(Sitting Sitting)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000072EA File Offset: 0x000054EA
		public Sitting LoadSittingById(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00069E3C File Offset: 0x0006803C
		public IList<Sitting> LoadSittingsByDate(DateTime Day)
		{
			DateTime date = Day.Date;
			DateTime endDate = date.AddDays(1.0).AddMinutes(-1.0);
			return this.LoadSittingsByDateRange(date, endDate);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00069E80 File Offset: 0x00068080
		public IList<Sitting> LoadSittingsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadSittings(StartDate, EndDate);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00069EA0 File Offset: 0x000680A0
		public void ClearSittingOnAppointment(params int[] AppointmentIds)
		{
			bool flag = AppointmentIds == null;
			if (!flag)
			{
				foreach (int appointmentId in AppointmentIds)
				{
					this.dao.ClearSittingOnAppointment(appointmentId);
				}
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00069EDC File Offset: 0x000680DC
		public void SetSittingOnAppointment(IDictionary<int, int> AppointmentIdWithSittingIds)
		{
			foreach (KeyValuePair<int, int> keyValuePair in AppointmentIdWithSittingIds)
			{
				this.dao.SetSittingOnAppointment(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0400029C RID: 668
		private ISittingDAO dao;
	}
}
