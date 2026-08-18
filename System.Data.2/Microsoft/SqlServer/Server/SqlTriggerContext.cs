using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000051 RID: 81
	public sealed class SqlTriggerContext
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0003E858 File Offset: 0x0003DC58
		internal SqlTriggerContext(TriggerAction triggerAction, bool[] columnsUpdated, SqlXml eventInstanceData)
		{
			this._triggerAction = triggerAction;
			this._columnsUpdated = columnsUpdated;
			this._eventInstanceData = eventInstanceData;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0003E880 File Offset: 0x0003DC80
		public int ColumnCount
		{
			get
			{
				int result = 0;
				if (this._columnsUpdated != null)
				{
					result = this._columnsUpdated.Length;
				}
				return result;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0003E8A4 File Offset: 0x0003DCA4
		public SqlXml EventData
		{
			get
			{
				return this._eventInstanceData;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0003E8B8 File Offset: 0x0003DCB8
		public TriggerAction TriggerAction
		{
			get
			{
				return this._triggerAction;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0003E8CC File Offset: 0x0003DCCC
		public bool IsUpdatedColumn(int columnOrdinal)
		{
			if (this._columnsUpdated != null)
			{
				return this._columnsUpdated[columnOrdinal];
			}
			throw ADP.IndexOutOfRange(columnOrdinal);
		}

		// Token: 0x0400018D RID: 397
		private TriggerAction _triggerAction;

		// Token: 0x0400018E RID: 398
		private bool[] _columnsUpdated;

		// Token: 0x0400018F RID: 399
		private SqlXml _eventInstanceData;
	}
}
