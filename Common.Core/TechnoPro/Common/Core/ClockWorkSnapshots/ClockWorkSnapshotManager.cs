using System;
using TechnoPro.Common.DAO.ClockWorkSnapshots;
using TechnoPro.Common.DAO.Impl.ClockWorkSnapshots;
using TechnoPro.Common.ICore.ClockWorkSnapshots;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkSnapshot;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Core.ClockWorkSnapshots
{
	// Token: 0x0200011C RID: 284
	public class ClockWorkSnapshotManager : IClockWorkSnapshotManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00054257 File Offset: 0x00052457
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0005425F File Offset: 0x0005245F
		public IClockWorkSnapshotDAO dao { get; set; }

		// Token: 0x06000C0A RID: 3082 RVA: 0x00054268 File Offset: 0x00052468
		public ClockWorkSnapshotManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ClockWorkSnapshotDAO(opContext);
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00054287 File Offset: 0x00052487
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0005428F File Offset: 0x0005248F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C0D RID: 3085 RVA: 0x00054298 File Offset: 0x00052498
		public BinaryFile GetClockWorkSnapshot(eSnapshotDataGroup DataGroups)
		{
			return this.dao.GetClockWorkSnapshot(DataGroups);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000542B8 File Offset: 0x000524B8
		public ClockWorkSnapshotRestoreResult RestoreClockWorkSnapshot(BinaryFile Snapshot, eSnapshotDataGroup DataGroups, bool AllowRestoreToDatabaseWithMoreThanOneUser = false)
		{
			return this.dao.RestoreClockWorkSnapshot(Snapshot, DataGroups, AllowRestoreToDatabaseWithMoreThanOneUser);
		}
	}
}
