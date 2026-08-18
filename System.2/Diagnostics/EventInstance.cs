using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004CA RID: 1226
	public class EventInstance
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x000CDA7D File Offset: 0x000CBC7D
		public EventInstance(long instanceId, int categoryId)
		{
			this.CategoryId = categoryId;
			this.InstanceId = instanceId;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000CDA9A File Offset: 0x000CBC9A
		public EventInstance(long instanceId, int categoryId, EventLogEntryType entryType) : this(instanceId, categoryId)
		{
			this.EntryType = entryType;
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000CDAAB File Offset: 0x000CBCAB
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x000CDAB3 File Offset: 0x000CBCB3
		public int CategoryId
		{
			get
			{
				return this._categoryNumber;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryNumber = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000CDAD3 File Offset: 0x000CBCD3
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x000CDADB File Offset: 0x000CBCDB
		public EventLogEntryType EntryType
		{
			get
			{
				return this._entryType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(EventLogEntryType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(EventLogEntryType));
				}
				this._entryType = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000CDB11 File Offset: 0x000CBD11
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000CDB19 File Offset: 0x000CBD19
		public long InstanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				if (value > (long)((ulong)-1) || value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._instanceId = value;
			}
		}

		// Token: 0x04002738 RID: 10040
		private int _categoryNumber;

		// Token: 0x04002739 RID: 10041
		private EventLogEntryType _entryType = EventLogEntryType.Information;

		// Token: 0x0400273A RID: 10042
		private long _instanceId;
	}
}
