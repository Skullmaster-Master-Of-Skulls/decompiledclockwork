using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000615 RID: 1557
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class SerializationStore : IDisposable
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060038FB RID: 14587
		public abstract ICollection Errors { get; }

		// Token: 0x060038FC RID: 14588
		public abstract void Close();

		// Token: 0x060038FD RID: 14589
		public abstract void Save(Stream stream);

		// Token: 0x060038FE RID: 14590 RVA: 0x000F2607 File Offset: 0x000F0807
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000F2610 File Offset: 0x000F0810
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
