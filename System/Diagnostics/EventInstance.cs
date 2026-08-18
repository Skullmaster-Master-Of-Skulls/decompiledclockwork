using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200074D RID: 1869
	public class EventInstance
	{
		// Token: 0x060038FF RID: 14591 RVA: 0x000F0851 File Offset: 0x000EF851
		public EventInstance(long instanceId, int categoryId)
		{
			this.CategoryId = categoryId;
			this.InstanceId = instanceId;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000F086E File Offset: 0x000EF86E
		public EventInstance(long instanceId, int categoryId, EventLogEntryType entryType) : this(instanceId, categoryId)
		{
			this.EntryType = entryType;
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000F087F File Offset: 0x000EF87F
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x000F0887 File Offset: 0x000EF887
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000F08A7 File Offset: 0x000EF8A7
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x000F08AF File Offset: 0x000EF8AF
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

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000F08E5 File Offset: 0x000EF8E5
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x000F08ED File Offset: 0x000EF8ED
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

		// Token: 0x04003281 RID: 12929
		private int _categoryNumber;

		// Token: 0x04003282 RID: 12930
		private EventLogEntryType _entryType = EventLogEntryType.Information;

		// Token: 0x04003283 RID: 12931
		private long _instanceId;
	}
}
