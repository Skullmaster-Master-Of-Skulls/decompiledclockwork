using System;
using System.Collections;

namespace System.Web.Hosting
{
	// Token: 0x020002B0 RID: 688
	internal class MapPathBasedVirtualPathCollection : MarshalByRefObject, IEnumerable
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x0009A0EF File Offset: 0x000990EF
		internal MapPathBasedVirtualPathCollection(VirtualPath virtualPath, RequestedEntryType requestedEntryType)
		{
			this._virtualPath = virtualPath;
			this._requestedEntryType = requestedEntryType;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0009A105 File Offset: 0x00099105
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x0009A108 File Offset: 0x00099108
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MapPathBasedVirtualPathEnumerator(this._virtualPath, this._requestedEntryType);
		}

		// Token: 0x04001C23 RID: 7203
		private VirtualPath _virtualPath;

		// Token: 0x04001C24 RID: 7204
		private RequestedEntryType _requestedEntryType;
	}
}
