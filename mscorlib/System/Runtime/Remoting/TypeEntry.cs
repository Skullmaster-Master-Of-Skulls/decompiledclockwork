using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075D RID: 1885
	[ComVisible(true)]
	public class TypeEntry
	{
		// Token: 0x0600431D RID: 17181 RVA: 0x000E56AA File Offset: 0x000E46AA
		protected TypeEntry()
		{
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x000E56B2 File Offset: 0x000E46B2
		// (set) Token: 0x0600431F RID: 17183 RVA: 0x000E56BA File Offset: 0x000E46BA
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x000E56C3 File Offset: 0x000E46C3
		// (set) Token: 0x06004321 RID: 17185 RVA: 0x000E56CB File Offset: 0x000E46CB
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000E56D4 File Offset: 0x000E46D4
		internal void CacheRemoteAppEntry(RemoteAppEntry entry)
		{
			this._cachedRemoteAppEntry = entry;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000E56DD File Offset: 0x000E46DD
		internal RemoteAppEntry GetRemoteAppEntry()
		{
			return this._cachedRemoteAppEntry;
		}

		// Token: 0x040021C4 RID: 8644
		private string _typeName;

		// Token: 0x040021C5 RID: 8645
		private string _assemblyName;

		// Token: 0x040021C6 RID: 8646
		private RemoteAppEntry _cachedRemoteAppEntry;
	}
}
