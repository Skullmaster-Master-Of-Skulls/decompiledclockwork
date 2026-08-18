using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000467 RID: 1127
	internal class CellLabel
	{
		// Token: 0x0600295D RID: 10589 RVA: 0x000C8329 File Offset: 0x000C6529
		internal CellLabel(CellLabel source)
		{
			this.m_startLineNumber = source.m_startLineNumber;
			this.m_startLinePosition = source.m_startLinePosition;
			this.m_sourceLocation = source.m_sourceLocation;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000C8355 File Offset: 0x000C6555
		internal CellLabel(MappingFragment fragmentInfo) : this(fragmentInfo.StartLineNumber, fragmentInfo.StartLinePosition, fragmentInfo.SourceLocation)
		{
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000C836F File Offset: 0x000C656F
		internal CellLabel(int startLineNumber, int startLinePosition, string sourceLocation)
		{
			this.m_startLineNumber = startLineNumber;
			this.m_startLinePosition = startLinePosition;
			this.m_sourceLocation = sourceLocation;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x000C838C File Offset: 0x000C658C
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000C8394 File Offset: 0x000C6594
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000C839C File Offset: 0x000C659C
		internal string SourceLocation
		{
			get
			{
				return this.m_sourceLocation;
			}
		}

		// Token: 0x04000F65 RID: 3941
		private readonly int m_startLineNumber;

		// Token: 0x04000F66 RID: 3942
		private readonly int m_startLinePosition;

		// Token: 0x04000F67 RID: 3943
		private readonly string m_sourceLocation;
	}
}
