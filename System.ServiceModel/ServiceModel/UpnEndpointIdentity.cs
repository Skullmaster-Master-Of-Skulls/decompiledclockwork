using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel.ComIntegration;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000BB RID: 187
	[__DynamicallyInvokable]
	public class UpnEndpointIdentity : EndpointIdentity
	{
		// Token: 0x06000336 RID: 822 RVA: 0x000127DF File Offset: 0x000109DF
		[__DynamicallyInvokable]
		public UpnEndpointIdentity(string upnName)
		{
			if (upnName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("upnName");
			}
			base.Initialize(Claim.CreateUpnClaim(upnName));
			this.hasUpnSidBeenComputed = false;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00012818 File Offset: 0x00010A18
		public UpnEndpointIdentity(Claim identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			if (!identity.ClaimType.Equals(ClaimTypes.Upn))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("UnrecognizedClaimTypeForIdentity", new object[]
				{
					identity.ClaimType,
					ClaimTypes.Upn
				}));
			}
			base.Initialize(identity);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001288E File Offset: 0x00010A8E
		internal UpnEndpointIdentity(WindowsIdentity windowsIdentity)
		{
			if (windowsIdentity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsIdentity");
			}
			this.windowsIdentity = windowsIdentity;
			this.upnSid = windowsIdentity.User;
			this.hasUpnSidBeenComputed = true;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000128D0 File Offset: 0x00010AD0
		internal override void EnsureIdentityClaim()
		{
			if (this.windowsIdentity != null)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.windowsIdentity != null)
					{
						base.Initialize(Claim.CreateUpnClaim(this.GetUpnFromWindowsIdentity(this.windowsIdentity)));
						this.windowsIdentity.Dispose();
						this.windowsIdentity = null;
					}
				}
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00012944 File Offset: 0x00010B44
		private string GetUpnFromWindowsIdentity(WindowsIdentity windowsIdentity)
		{
			string text = null;
			string text2 = null;
			try
			{
				text = windowsIdentity.Name;
				if (this.IsMachineJoinedToDomain())
				{
					text2 = this.GetUpnFromDownlevelName(text);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			return text2 ?? text;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00012998 File Offset: 0x00010B98
		private bool IsMachineJoinedToDomain()
		{
			IntPtr zero = IntPtr.Zero;
			bool result;
			try
			{
				int num = SafeNativeMethods.DsGetDcName(null, null, IntPtr.Zero, null, 16U, out zero);
				result = (num != 1355);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SafeNativeMethods.NetApiBufferFree(zero);
				}
			}
			return result;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000129F4 File Offset: 0x00010BF4
		private string GetUpnFromDownlevelName(string downlevelName)
		{
			if (downlevelName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("downlevelName");
			}
			int num = downlevelName.IndexOf('\\');
			if (num < 0 || num == 0 || num == downlevelName.Length - 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("DownlevelNameCannotMapToUpn", new object[]
				{
					downlevelName
				})));
			}
			string input = downlevelName.Substring(0, num + 1);
			string str = downlevelName.Substring(num + 1);
			uint capacity = 50U;
			StringBuilder stringBuilder = new StringBuilder((int)capacity);
			if (!SafeNativeMethods.TranslateName(input, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new Win32Exception(lastWin32Error));
				}
				stringBuilder = new StringBuilder((int)capacity);
				if (!SafeNativeMethods.TranslateName(input, EXTENDED_NAME_FORMAT.NameSamCompatible, EXTENDED_NAME_FORMAT.NameCanonical, stringBuilder, out capacity))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new Win32Exception(lastWin32Error));
				}
			}
			stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
			string str2 = stringBuilder.ToString();
			return str + "@" + str2;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012AF9 File Offset: 0x00010CF9
		internal override void WriteContentsTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteElementString(XD.AddressingDictionary.Upn, XD.AddressingDictionary.IdentityExtensionNamespace, (string)base.IdentityClaim.Resource);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012B38 File Offset: 0x00010D38
		internal SecurityIdentifier GetUpnSid()
		{
			if (!this.hasUpnSidBeenComputed)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					string text = (string)base.IdentityClaim.Resource;
					if (!this.hasUpnSidBeenComputed)
					{
						try
						{
							NTAccount ntaccount = new NTAccount(text);
							this.upnSid = (ntaccount.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							if (ex is NullReferenceException)
							{
								throw;
							}
							SecurityTraceRecordHelper.TraceSpnToSidMappingFailure(text, ex);
						}
						finally
						{
							this.hasUpnSidBeenComputed = true;
						}
					}
				}
			}
			return this.upnSid;
		}

		// Token: 0x04000970 RID: 2416
		private SecurityIdentifier upnSid;

		// Token: 0x04000971 RID: 2417
		private bool hasUpnSidBeenComputed;

		// Token: 0x04000972 RID: 2418
		private WindowsIdentity windowsIdentity;

		// Token: 0x04000973 RID: 2419
		private object thisLock = new object();
	}
}
