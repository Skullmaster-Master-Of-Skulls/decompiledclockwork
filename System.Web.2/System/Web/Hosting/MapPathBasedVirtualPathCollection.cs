using System;
using System.Collections;

namespace System.Web.Hosting
{
	// Token: 0x020007D3 RID: 2003
	internal class MapPathBasedVirtualPathCollection : MarshalByRefObject, IEnumerable
	{
		// Token: 0x0600601B RID: 24603 RVA: 0x0014C0CF File Offset: 0x0014A2CF
		internal MapPathBasedVirtualPathCollection(VirtualPath virtualPath, RequestedEntryType requestedEntryType)
		{
			this._virtualPath = virtualPath;
			this._requestedEntryType = requestedEntryType;
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x0014C0E5 File Offset: 0x0014A2E5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MapPathBasedVirtualPathEnumerator(this._virtualPath, this._requestedEntryType);
		}

		// Token: 0x0400323D RID: 12861
		private VirtualPath _virtualPath;

		// Token: 0x0400323E RID: 12862
		private RequestedEntryType _requestedEntryType;
	}
}
