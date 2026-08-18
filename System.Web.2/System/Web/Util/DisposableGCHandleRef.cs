using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001E4 RID: 484
	internal class DisposableGCHandleRef<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x0004A910 File Offset: 0x00048B10
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public DisposableGCHandleRef(T t)
		{
			this._handle = GCHandle.Alloc(t);
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0004A929 File Offset: 0x00048B29
		public T Target
		{
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				return (T)((object)this._handle.Target);
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0004A93B File Offset: 0x00048B3B
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public void Dispose()
		{
			this.Target.Dispose();
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
		}

		// Token: 0x04001730 RID: 5936
		private GCHandle _handle;
	}
}
