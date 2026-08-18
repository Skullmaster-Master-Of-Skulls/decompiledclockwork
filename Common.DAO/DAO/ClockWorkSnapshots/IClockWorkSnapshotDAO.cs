using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkSnapshot;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.DAO.ClockWorkSnapshots
{
	// Token: 0x02000079 RID: 121
	public interface IClockWorkSnapshotDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600030D RID: 781
		BinaryFile GetClockWorkSnapshot(eSnapshotDataGroup DataGroups);

		// Token: 0x0600030E RID: 782
		ClockWorkSnapshotRestoreResult RestoreClockWorkSnapshot(BinaryFile Snapshot, eSnapshotDataGroup DataGroups, bool AllowRestoreToDatabaseWithMoreThanOneUser = false);
	}
}
