using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001FB RID: 507
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x00064C4C File Offset: 0x00062E4C
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00064C6C File Offset: 0x00062E6C
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					target.DangerousRelease();
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					this._Target = target;
					base.SetHandle(new IntPtr(0));
				}
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00064CD4 File Offset: 0x00062ED4
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this._Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this._Target = null;
			return true;
		}

		// Token: 0x04001555 RID: 5461
		internal SafeFreeCredentials _Target;
	}
}
