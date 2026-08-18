using System;
using System.Collections;
using System.Data;
using ClockWorkWebAPI;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000010 RID: 16
	public class DynamicControlLayoutHelper
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public DynamicControlLayoutHelper(db conn)
		{
			this.lookupLists = new DataSet();
			this.useFrench = false;
			this.conn = conn;
			this.extenderSets = new ArrayList();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000E50E File Offset: 0x0000C70E
		public DynamicControlLayoutHelper()
		{
			this.lookupLists = new DataSet();
			this.useFrench = false;
			this.conn = null;
			this.extenderSets = new ArrayList();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000E53C File Offset: 0x0000C73C
		public void AddExtenderSet(ExtenderType etype, string controlId, string extenderId)
		{
			this.extenderSets.Add(new ExtenderSet(etype, controlId, extenderId));
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000E554 File Offset: 0x0000C754
		public ArrayList ExtenderSets
		{
			get
			{
				return this.extenderSets;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000E56C File Offset: 0x0000C76C
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000E584 File Offset: 0x0000C784
		public bool AllControlsAreDisabled
		{
			get
			{
				return this.allControlsAreDisabled;
			}
			set
			{
				this.allControlsAreDisabled = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000E590 File Offset: 0x0000C790
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public DataSet LookupLists
		{
			get
			{
				return this.lookupLists;
			}
			set
			{
				this.lookupLists = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public bool UseFrench
		{
			get
			{
				return this.useFrench;
			}
			set
			{
				this.useFrench = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		public db Conn
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x0400006F RID: 111
		private DataSet lookupLists;

		// Token: 0x04000070 RID: 112
		private bool useFrench;

		// Token: 0x04000071 RID: 113
		private db conn;

		// Token: 0x04000072 RID: 114
		private bool allControlsAreDisabled;

		// Token: 0x04000073 RID: 115
		private ArrayList extenderSets;
	}
}
