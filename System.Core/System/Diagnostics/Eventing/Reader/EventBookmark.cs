using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002AA RID: 682
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventBookmark : ISerializable
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x0005AD1F File Offset: 0x00058F1F
		internal EventBookmark(string bookmarkText)
		{
			if (bookmarkText == null)
			{
				throw new ArgumentNullException("bookmarkText");
			}
			this.bookmark = bookmarkText;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005AD3C File Offset: 0x00058F3C
		protected EventBookmark(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.bookmark = info.GetString("BookmarkText");
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0005AD63 File Offset: 0x00058F63
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005AD6D File Offset: 0x00058F6D
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("BookmarkText", this.bookmark);
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0005AD8E File Offset: 0x00058F8E
		internal string BookmarkText
		{
			get
			{
				return this.bookmark;
			}
		}

		// Token: 0x04000C1A RID: 3098
		private string bookmark;
	}
}
