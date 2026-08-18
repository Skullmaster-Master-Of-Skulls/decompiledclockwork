using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class AppointmentInsertException : Exception
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0006E5B0 File Offset: 0x0006C7B0
		public AppointmentInsertException()
		{
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0006E5BA File Offset: 0x0006C7BA
		public AppointmentInsertException(string msg) : base(msg)
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0006E5C5 File Offset: 0x0006C7C5
		public AppointmentInsertException(string msg, Exception inner) : base(msg, inner)
		{
		}
	}
}
