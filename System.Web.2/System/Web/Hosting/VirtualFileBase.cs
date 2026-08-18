using System;

namespace System.Web.Hosting
{
	// Token: 0x020007EC RID: 2028
	public abstract class VirtualFileBase : MarshalByRefObject
	{
		// Token: 0x060060DB RID: 24795 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x060060DC RID: 24796 RVA: 0x0014E331 File Offset: 0x0014C531
		public virtual string Name
		{
			get
			{
				return this._virtualPath.FileName;
			}
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x060060DD RID: 24797 RVA: 0x0014E33E File Offset: 0x0014C53E
		public string VirtualPath
		{
			get
			{
				return this._virtualPath.VirtualPathString;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x060060DE RID: 24798 RVA: 0x0014E34B File Offset: 0x0014C54B
		internal VirtualPath VirtualPathObject
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x060060DF RID: 24799
		public abstract bool IsDirectory { get; }

		// Token: 0x0400326A RID: 12906
		internal VirtualPath _virtualPath;
	}
}
