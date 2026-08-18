using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkSnapshot;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.ICore.ClockWorkSnapshots
{
	// Token: 0x020000B1 RID: 177
	public interface IClockWorkSnapshotManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000551 RID: 1361
		BinaryFile GetClockWorkSnapshot(eSnapshotDataGroup DataGroups);

		// Token: 0x06000552 RID: 1362
		ClockWorkSnapshotRestoreResult RestoreClockWorkSnapshot(BinaryFile Snapshot, eSnapshotDataGroup DataGroups, bool AllowRestoreToDatabaseWithMoreThanOneUser = false);
	}
}
