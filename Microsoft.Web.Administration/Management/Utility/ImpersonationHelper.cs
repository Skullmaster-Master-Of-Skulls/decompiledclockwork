using System;
using System.Security.Principal;

namespace Microsoft.Web.Management.Utility
{
	// Token: 0x02000089 RID: 137
	internal class ImpersonationHelper : IDisposable
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00009FA9 File Offset: 0x00008FA9
		public static IDisposable ImpersonateProcessIdentity()
		{
			return ImpersonationHelper.ImpersonateUser(IntPtr.Zero);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009FB8 File Offset: 0x00008FB8
		public static IDisposable ImpersonateUser(IntPtr userToken)
		{
			ImpersonationHelper impersonationHelper = ImpersonationHelper.Create();
			impersonationHelper.Impersonate(userToken);
			return impersonationHelper;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00009FD3 File Offset: 0x00008FD3
		public static ImpersonationHelper Create()
		{
			return new ImpersonationHelper();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00009FDA File Offset: 0x00008FDA
		private ImpersonationHelper()
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00009FE2 File Offset: 0x00008FE2
		public void Impersonate(IntPtr userToken)
		{
			this.Revert();
			this._impersonationContext = WindowsIdentity.Impersonate(userToken);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009FF6 File Offset: 0x00008FF6
		public void Revert()
		{
			if (this._impersonationContext != null)
			{
				this._impersonationContext.Undo();
				this._impersonationContext.Dispose();
				this._impersonationContext = null;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A01D File Offset: 0x0000901D
		public void Dispose()
		{
			this.Revert();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400017A RID: 378
		private WindowsImpersonationContext _impersonationContext;
	}
}
