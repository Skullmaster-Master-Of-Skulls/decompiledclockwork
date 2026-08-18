using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E4 RID: 484
	internal sealed class ComPlusServerSecurity : IContextSecurityPerimeter, IServerSecurity, IDisposable
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x00037EF0 File Offset: 0x000360F0
		public ComPlusServerSecurity(WindowsIdentity clientIdentity, bool shouldUseCallContext)
		{
			if (clientIdentity == null)
			{
				throw Fx.AssertAndThrow("NULL Identity");
			}
			if (IntPtr.Zero == clientIdentity.Token)
			{
				throw Fx.AssertAndThrow("Token handle cannot be zero");
			}
			this.shouldUseCallContext = shouldUseCallContext;
			this.clientIdentity = clientIdentity;
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this);
			try
			{
				this.oldSecurityObject = SafeNativeMethods.CoSwitchCallContext(iunknownForObject);
			}
			catch
			{
				Marshal.Release(iunknownForObject);
				throw;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00037F78 File Offset: 0x00036178
		~ComPlusServerSecurity()
		{
			this.Dispose(false);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00037FA8 File Offset: 0x000361A8
		public bool GetPerimeterFlag()
		{
			return this.shouldUseCallContext;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00037FB0 File Offset: 0x000361B0
		public void SetPerimeterFlag(bool flag)
		{
			this.shouldUseCallContext = flag;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00037FBC File Offset: 0x000361BC
		public void QueryBlanket(IntPtr authnSvc, IntPtr authzSvc, IntPtr serverPrincipalName, IntPtr authnLevel, IntPtr impLevel, IntPtr clientPrincipalName, IntPtr Capabilities)
		{
			if (authnSvc != IntPtr.Zero)
			{
				uint val = uint.MaxValue;
				string authenticationType = this.clientIdentity.AuthenticationType;
				if (authenticationType.ToUpperInvariant() == "NTLM")
				{
					val = 10U;
				}
				else if (authenticationType.ToUpperInvariant() == "KERBEROS")
				{
					val = 16U;
				}
				else if (authenticationType.ToUpperInvariant() == "NEGOTIATE")
				{
					val = 9U;
				}
				Marshal.WriteInt32(authnSvc, (int)val);
			}
			if (authzSvc != IntPtr.Zero)
			{
				Marshal.WriteInt32(authzSvc, 0);
			}
			if (serverPrincipalName != IntPtr.Zero)
			{
				IntPtr val2 = Marshal.StringToCoTaskMemUni(SecurityUtils.GetProcessIdentity().Name);
				Marshal.WriteIntPtr(serverPrincipalName, val2);
			}
			if (authnLevel != IntPtr.Zero)
			{
				Marshal.WriteInt32(authnLevel, 0);
			}
			if (impLevel != IntPtr.Zero)
			{
				Marshal.WriteInt32(impLevel, 0);
			}
			if (clientPrincipalName != IntPtr.Zero)
			{
				IntPtr val3 = Marshal.StringToCoTaskMemUni(this.clientIdentity.Name);
				Marshal.WriteIntPtr(clientPrincipalName, val3);
			}
			if (Capabilities != IntPtr.Zero)
			{
				Marshal.WriteInt32(Capabilities, 0);
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000380D0 File Offset: 0x000362D0
		public int ImpersonateClient()
		{
			int result = HR.E_FAIL;
			try
			{
				this.impersonateContext = WindowsIdentity.Impersonate(this.clientIdentity.Token);
				this.isImpersonating = true;
				result = HR.S_OK;
			}
			catch (SecurityException)
			{
				result = HR.RPC_NT_BINDING_HAS_NO_AUTH;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003813C File Offset: 0x0003633C
		public int RevertToSelf()
		{
			int result = HR.E_FAIL;
			if (this.isImpersonating)
			{
				try
				{
					this.impersonateContext.Undo();
					this.isImpersonating = false;
					result = HR.S_OK;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
			return result;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00038190 File Offset: 0x00036390
		public bool IsImpersonating()
		{
			return this.isImpersonating;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00038198 File Offset: 0x00036398
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000381A8 File Offset: 0x000363A8
		public void Dispose(bool disposing)
		{
			this.RevertToSelf();
			IntPtr intPtr = SafeNativeMethods.CoSwitchCallContext(this.oldSecurityObject);
			if (IntPtr.Zero == intPtr)
			{
				DiagnosticUtility.FailFast("Security Context was should not be null");
			}
			if (Marshal.GetObjectForIUnknown(intPtr) != this)
			{
				DiagnosticUtility.FailFast("Security Context was modified from underneath us");
			}
			Marshal.Release(intPtr);
			if (disposing)
			{
				this.clientIdentity = null;
				if (this.impersonateContext != null)
				{
					this.impersonateContext.Dispose();
				}
			}
		}

		// Token: 0x040017CA RID: 6090
		private WindowsIdentity clientIdentity;

		// Token: 0x040017CB RID: 6091
		private IntPtr oldSecurityObject = IntPtr.Zero;

		// Token: 0x040017CC RID: 6092
		private WindowsImpersonationContext impersonateContext;

		// Token: 0x040017CD RID: 6093
		private bool isImpersonating;

		// Token: 0x040017CE RID: 6094
		private bool shouldUseCallContext;

		// Token: 0x040017CF RID: 6095
		private const uint RPC_C_AUTHN_GSS_NEGOTIATE = 9U;

		// Token: 0x040017D0 RID: 6096
		private const uint RPC_C_AUTHN_WINNT = 10U;

		// Token: 0x040017D1 RID: 6097
		private const uint RPC_C_AUTHN_GSS_KERBEROS = 16U;

		// Token: 0x040017D2 RID: 6098
		private const uint RPC_C_AUTHN_DEFAULT = 4294967295U;

		// Token: 0x040017D3 RID: 6099
		private const uint RPC_C_AUTHZ_NONE = 0U;

		// Token: 0x040017D4 RID: 6100
		private const uint RPC_C_AUTHN_LEVEL_DEFAULT = 0U;

		// Token: 0x040017D5 RID: 6101
		private const uint RPC_C_AUTHN_LEVEL_NONE = 1U;

		// Token: 0x040017D6 RID: 6102
		private const uint RPC_C_AUTHN_LEVEL_CONNECT = 2U;

		// Token: 0x040017D7 RID: 6103
		private const uint RPC_C_AUTHN_LEVEL_CALL = 3U;

		// Token: 0x040017D8 RID: 6104
		private const uint RPC_C_AUTHN_LEVEL_PKT = 4U;

		// Token: 0x040017D9 RID: 6105
		private const uint RPC_C_AUTHN_LEVEL_PKT_INTEGRITY = 5U;

		// Token: 0x040017DA RID: 6106
		private const uint RPC_C_AUTHN_LEVEL_PKT_PRIVACY = 6U;
	}
}
