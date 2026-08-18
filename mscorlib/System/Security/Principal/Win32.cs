using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000948 RID: 2376
	internal sealed class Win32
	{
		// Token: 0x060055B9 RID: 21945 RVA: 0x001371D8 File Offset: 0x001361D8
		static Win32()
		{
			Win32Native.OSVERSIONINFO osversioninfo = new Win32Native.OSVERSIONINFO();
			if (!Win32Native.GetVersionEx(osversioninfo))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfo.PlatformId != 2 || osversioninfo.MajorVersion < 5)
			{
				Win32._LsaApisSupported = false;
				Win32._LsaLookupNames2Supported = false;
				Win32._ConvertStringSidToSidSupported = false;
				Win32._WellKnownSidApisSupported = false;
				return;
			}
			Win32._ConvertStringSidToSidSupported = true;
			Win32._LsaApisSupported = true;
			if (osversioninfo.MajorVersion > 5 || osversioninfo.MinorVersion > 0)
			{
				Win32._LsaLookupNames2Supported = true;
				Win32._WellKnownSidApisSupported = true;
				return;
			}
			Win32._LsaLookupNames2Supported = false;
			Win32Native.OSVERSIONINFOEX osversioninfoex = new Win32Native.OSVERSIONINFOEX();
			if (!Win32Native.GetVersionEx(osversioninfoex))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfoex.ServicePackMajor < 3)
			{
				Win32._WellKnownSidApisSupported = false;
				return;
			}
			Win32._WellKnownSidApisSupported = true;
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00137297 File Offset: 0x00136297
		private Win32()
		{
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060055BB RID: 21947 RVA: 0x0013729F File Offset: 0x0013629F
		internal static bool SddlConversionSupported
		{
			get
			{
				return Win32._ConvertStringSidToSidSupported;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060055BC RID: 21948 RVA: 0x001372A6 File Offset: 0x001362A6
		internal static bool LsaApisSupported
		{
			get
			{
				return Win32._LsaApisSupported;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060055BD RID: 21949 RVA: 0x001372AD File Offset: 0x001362AD
		internal static bool LsaLookupNames2Supported
		{
			get
			{
				return Win32._LsaLookupNames2Supported;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x001372B4 File Offset: 0x001362B4
		internal static bool WellKnownSidApisSupported
		{
			get
			{
				return Win32._WellKnownSidApisSupported;
			}
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x001372BC File Offset: 0x001362BC
		internal static SafeLsaPolicyHandle LsaOpenPolicy(string systemName, PolicyRights rights)
		{
			if (!Win32.LsaApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
			}
			Win32Native.LSA_OBJECT_ATTRIBUTES lsa_OBJECT_ATTRIBUTES;
			lsa_OBJECT_ATTRIBUTES.Length = Marshal.SizeOf(typeof(Win32Native.LSA_OBJECT_ATTRIBUTES));
			lsa_OBJECT_ATTRIBUTES.RootDirectory = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.ObjectName = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.Attributes = 0;
			lsa_OBJECT_ATTRIBUTES.SecurityDescriptor = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.SecurityQualityOfService = IntPtr.Zero;
			SafeLsaPolicyHandle result;
			uint num;
			if ((num = Win32Native.LsaOpenPolicy(systemName, ref lsa_OBJECT_ATTRIBUTES, (int)rights, out result)) == 0U)
			{
				return result;
			}
			if (num == 3221225506U)
			{
				throw new UnauthorizedAccessException();
			}
			if (num == 3221225626U || num == 3221225495U)
			{
				throw new OutOfMemoryException();
			}
			int errorCode = Win32Native.LsaNtStatusToWinError((int)num);
			throw new SystemException(Win32Native.GetMessage(errorCode));
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00137378 File Offset: 0x00136378
		internal static byte[] ConvertIntPtrSidToByteArraySid(IntPtr binaryForm)
		{
			byte b = Marshal.ReadByte(binaryForm, 0);
			if (b != SecurityIdentifier.Revision)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), "binaryForm");
			}
			byte b2 = Marshal.ReadByte(binaryForm, 1);
			if (b2 < 0 || b2 > SecurityIdentifier.MaxSubAuthorities)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", new object[]
				{
					SecurityIdentifier.MaxSubAuthorities
				}), "binaryForm");
			}
			int num = (int)(8 + b2 * 4);
			byte[] array = new byte[num];
			Marshal.Copy(binaryForm, array, 0, num);
			return array;
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x00137404 File Offset: 0x00136404
		internal static int CreateSidFromString(string stringSid, out byte[] resultSid)
		{
			IntPtr zero = IntPtr.Zero;
			if (!Win32.SddlConversionSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
			}
			int lastWin32Error;
			try
			{
				if (1 != Win32Native.ConvertStringSidToSid(stringSid, out zero))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					goto IL_44;
				}
				resultSid = Win32.ConvertIntPtrSidToByteArraySid(zero);
			}
			finally
			{
				Win32Native.LocalFree(zero);
			}
			return 0;
			IL_44:
			resultSid = null;
			return lastWin32Error;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0013746C File Offset: 0x0013646C
		internal static int CreateWellKnownSid(WellKnownSidType sidType, SecurityIdentifier domainSid, out byte[] resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			resultSid = new byte[maxBinaryLength];
			if (Win32Native.CreateWellKnownSid((int)sidType, (domainSid == null) ? null : domainSid.BinaryForm, resultSid, ref maxBinaryLength) != 0)
			{
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001374C8 File Offset: 0x001364C8
		internal static bool IsEqualDomainSid(SecurityIdentifier sid1, SecurityIdentifier sid2)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			if (sid1 == null || sid2 == null)
			{
				return false;
			}
			byte[] array = new byte[sid1.BinaryLength];
			sid1.GetBinaryForm(array, 0);
			byte[] array2 = new byte[sid2.BinaryLength];
			sid2.GetBinaryForm(array2, 0);
			bool flag;
			return Win32Native.IsEqualDomainSid(array, array2, out flag) != 0 && flag;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00137538 File Offset: 0x00136538
		internal static int GetWindowsAccountDomainSid(SecurityIdentifier sid, out SecurityIdentifier resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			byte[] array2 = new byte[maxBinaryLength];
			if (Win32Native.GetWindowsAccountDomainSid(array, array2, ref maxBinaryLength) != 0)
			{
				resultSid = new SecurityIdentifier(array2, 0);
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0013759C File Offset: 0x0013659C
		internal static bool IsWellKnownSid(SecurityIdentifier sid, WellKnownSidType type)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			return Win32Native.IsWellKnownSid(array, (int)type) != 0;
		}

		// Token: 0x060055C6 RID: 21958
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ImpersonateLoggedOnUser(SafeTokenHandle hToken);

		// Token: 0x060055C7 RID: 21959
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int OpenThreadToken(TokenAccessLevels dwDesiredAccess, WinSecurityContext OpenAs, out SafeTokenHandle phThreadToken);

		// Token: 0x060055C8 RID: 21960
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int RevertToSelf();

		// Token: 0x060055C9 RID: 21961
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int SetThreadToken(SafeTokenHandle hToken);

		// Token: 0x04002CD4 RID: 11476
		internal const int FALSE = 0;

		// Token: 0x04002CD5 RID: 11477
		internal const int TRUE = 1;

		// Token: 0x04002CD6 RID: 11478
		private static bool _LsaApisSupported;

		// Token: 0x04002CD7 RID: 11479
		private static bool _LsaLookupNames2Supported;

		// Token: 0x04002CD8 RID: 11480
		private static bool _ConvertStringSidToSidSupported;

		// Token: 0x04002CD9 RID: 11481
		private static bool _WellKnownSidApisSupported;
	}
}
