using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000055 RID: 85
	public sealed class SqlTriggerContext
	{
		// Token: 0x06000398 RID: 920 RVA: 0x001E27E8 File Offset: 0x001E1BE8
		internal SqlTriggerContext(TriggerAction triggerAction, bool[] columnsUpdated, SqlXml eventInstanceData)
		{
			this._triggerAction = triggerAction;
			this._columnsUpdated = columnsUpdated;
			this._eventInstanceData = eventInstanceData;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000399 RID: 921 RVA: 0x001E2818 File Offset: 0x001E1C18
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600039A RID: 922 RVA: 0x001E2848 File Offset: 0x001E1C48
		public SqlXml EventData
		{
			get
			{
				return this._eventInstanceData;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600039B RID: 923 RVA: 0x001E2868 File Offset: 0x001E1C68
		public TriggerAction TriggerAction
		{
			get
			{
				return this._triggerAction;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x001E2888 File Offset: 0x001E1C88
		public bool IsUpdatedColumn(int columnOrdinal)
		{
			if (this._columnsUpdated != null)
			{
				return this._columnsUpdated[columnOrdinal];
			}
			throw ADP.IndexOutOfRange(columnOrdinal);
		}

		// Token: 0x0400064C RID: 1612
		private TriggerAction _triggerAction;

		// Token: 0x0400064D RID: 1613
		private bool[] _columnsUpdated;

		// Token: 0x0400064E RID: 1614
		private SqlXml _eventInstanceData;
	}
}
