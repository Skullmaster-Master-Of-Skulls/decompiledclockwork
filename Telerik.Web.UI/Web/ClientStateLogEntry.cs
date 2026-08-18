using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web
{
	// Token: 0x02001AF6 RID: 6902
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ClientStateLogEntry
	{
		// Token: 0x17005144 RID: 20804
		// (get) Token: 0x06010B3E RID: 68414 RVA: 0x003B80D3 File Offset: 0x003B62D3
		// (set) Token: 0x06010B3F RID: 68415 RVA: 0x003B80DB File Offset: 0x003B62DB
		public ClientStateLogEntryType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17005145 RID: 20805
		// (get) Token: 0x06010B40 RID: 68416 RVA: 0x003B80E4 File Offset: 0x003B62E4
		// (set) Token: 0x06010B41 RID: 68417 RVA: 0x003B80EC File Offset: 0x003B62EC
		public IDictionary<string, object> Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x17005146 RID: 20806
		// (get) Token: 0x06010B42 RID: 68418 RVA: 0x003B80F5 File Offset: 0x003B62F5
		// (set) Token: 0x06010B43 RID: 68419 RVA: 0x003B80FD File Offset: 0x003B62FD
		public string Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x04004A8B RID: 19083
		private ClientStateLogEntryType _type;

		// Token: 0x04004A8C RID: 19084
		private IDictionary<string, object> _data;

		// Token: 0x04004A8D RID: 19085
		private string _index;
	}
}
