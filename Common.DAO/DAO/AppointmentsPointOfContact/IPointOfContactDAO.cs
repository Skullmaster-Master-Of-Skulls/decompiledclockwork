using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.Common.DAO.AppointmentsPointOfContact
{
	// Token: 0x020000A2 RID: 162
	public interface IPointOfContactDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000431 RID: 1073
		int CreatePointOfContact(PointOfContact PointOfContact, int screenNumToSaveNotesTo = 0, int rtfTextBoxCidToSaveNotesTo = 0);

		// Token: 0x06000432 RID: 1074
		void UpdatePointOfContact(PointOfContact PointOfContact);
	}
}
