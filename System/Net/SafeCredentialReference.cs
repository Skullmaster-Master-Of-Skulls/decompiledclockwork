using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200051F RID: 1311
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x06002859 RID: 10329 RVA: 0x000A6250 File Offset: 0x000A5250
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000A6270 File Offset: 0x000A5270
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

		// Token: 0x0600285B RID: 10331 RVA: 0x000A62D8 File Offset: 0x000A52D8
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

		// Token: 0x04002785 RID: 10117
		internal SafeFreeCredentials _Target;
	}
}
