using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// Token: 0x0200000D RID: 13
internal class ADALNativeWrapper
{
	// Token: 0x0600009A RID: 154 RVA: 0x00004420 File Offset: 0x00003820
	private static _GUID ToGUID(ValueType guid)
	{
		ref byte byte& = ref ((Guid)guid).ToByteArray()[0];
		_GUID result;
		cpblk(ref result, ref byte&, 16);
		return result;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000444C File Offset: 0x0000384C
	internal static int ADALInitialize()
	{
		return <Module>.SNISecADALInitialize();
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00004460 File Offset: 0x00003860
	private unsafe static byte[] ADALGetAccessToken(string username, IntPtr password, string stsURL, string servicePrincipalName, ValueType correlationId, string clientId, bool* fWindowsIntegrated, ref long fileTime)
	{
		byte condition;
		if (!(username != null) && *fWindowsIntegrated == 0)
		{
			condition = 0;
		}
		else
		{
			condition = 1;
		}
		Debug.Assert(condition != 0, "User name is null and its not windows integrated authentication.");
		byte condition2;
		if (!(password != IntPtr.Zero) && *fWindowsIntegrated == 0)
		{
			condition2 = 0;
		}
		else
		{
			condition2 = 1;
		}
		Debug.Assert(condition2 != 0, "Password is null and its not windows integrated authentication.");
		Debug.Assert(stsURL != null, "stsURL is null.");
		Debug.Assert(servicePrincipalName != null, "ServicePrincipalName is null.");
		Debug.Assert(clientId != null, "Ado ClientId is null.");
		byte condition3 = (correlationId != Guid.Empty) ? 1 : 0;
		Debug.Assert(condition3 != 0, "CorrelationId is Guid::Empty.");
		ref ushort uint16_u0020modopt(IsConst)& = null;
		ushort* ptr = null;
		if (*fWindowsIntegrated == 0)
		{
			ref byte ptr2 = username;
			if (ref ptr2 != null)
			{
				ptr2 = (long)RuntimeHelpers.OffsetToStringData + ref ptr2;
			}
			uint16_u0020modopt(IsConst)& = ref ptr2;
			ptr = (ushort*)password.ToPointer();
		}
		ref byte ptr3 = stsURL;
		if (ref ptr3 != null)
		{
			ptr3 = (long)RuntimeHelpers.OffsetToStringData + ref ptr3;
		}
		ref ushort uint16_u0020modopt(IsConst)&2 = ref ptr3;
		ref byte ptr4 = servicePrincipalName;
		if (ref ptr4 != null)
		{
			ptr4 = (long)RuntimeHelpers.OffsetToStringData + ref ptr4;
		}
		ref ushort uint16_u0020modopt(IsConst)&3 = ref ptr4;
		ref byte ptr5 = clientId;
		if (ref ptr5 != null)
		{
			ptr5 = (long)RuntimeHelpers.OffsetToStringData + ref ptr5;
		}
		ref ushort uint16_u0020modopt(IsConst)&4 = ref ptr5;
		ushort* ptr6 = null;
		ushort* ptr7 = null;
		uint num = 0;
		uint num2 = 0;
		uint status = 0;
		uint state = 0;
		_FILETIME filetime;
		initblk(ref filetime, 0, 8L);
		_GUID guid = ADALNativeWrapper.ToGUID(correlationId);
		byte[] result;
		try
		{
			uint num3 = <Module>.SNISecADALGetAccessToken(ref uint16_u0020modopt(IsConst)&, ptr, ref uint16_u0020modopt(IsConst)&2, ref uint16_u0020modopt(IsConst)&3, ref guid, ref uint16_u0020modopt(IsConst)&4, fWindowsIntegrated, &ptr6, ref num, &ptr7, ref num2, ref status, ref state, ref filetime);
			if (num3 != null)
			{
				byte condition4 = (ptr6 == null) ? 1 : 0;
				Debug.Assert(condition4 != 0, "pToken is not null in error case.");
				byte condition5 = (num == 0) ? 1 : 0;
				Debug.Assert(condition5 != 0, "Token length is not 0 in error case.");
				string message = string.Empty;
				if (ptr7 != null)
				{
					message = Marshal.PtrToStringUni((IntPtr)((void*)ptr7), num2);
				}
				throw new AdalException(message, num3, status, state);
			}
			byte condition6 = (ptr6 != null) ? 1 : 0;
			Debug.Assert(condition6 != 0, "pToken is null.");
			byte condition7 = (num > 0) ? 1 : 0;
			Debug.Assert(condition7 != 0, "token length is less than or equal to 0.");
			byte condition8 = (ptr7 == null) ? 1 : 0;
			Debug.Assert(condition8 != 0, "pErrorDescription is not null");
			byte condition9 = (num2 == 0) ? 1 : 0;
			Debug.Assert(condition9 != 0, "ErrorDescription length is not 0.");
			IntPtr source = (IntPtr)((void*)ptr6);
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			fileTime = (long)((ulong)(*(ref filetime + 4)) * 4294967296UL + filetime);
			result = array;
		}
		finally
		{
			if (ptr6 != null)
			{
				<Module>.delete[]((void*)ptr6);
			}
			if (ptr7 != null)
			{
				<Module>.delete[]((void*)ptr7);
			}
		}
		return result;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00004758 File Offset: 0x00003B58
	internal static byte[] ADALGetAccessToken(string username, string password, string stsURL, string servicePrincipalName, ValueType correlationId, string clientId, ref long fileTime)
	{
		Debug.Assert(password != null, "Password is null.");
		IntPtr intPtr = IntPtr.Zero;
		byte[] result;
		try
		{
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(password);
			intPtr = intPtr2;
			bool flag = 0;
			result = ADALNativeWrapper.ADALGetAccessToken(username, intPtr2, stsURL, servicePrincipalName, correlationId, clientId, ref flag, ref fileTime);
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
		return result;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000046C8 File Offset: 0x00003AC8
	internal static byte[] ADALGetAccessToken(string username, SecureString password, string stsURL, string servicePrincipalName, ValueType correlationId, string clientId, ref long fileTime)
	{
		byte condition = (password != null) ? 1 : 0;
		Debug.Assert(condition != 0, "Password from SecureString is null.");
		IntPtr intPtr = IntPtr.Zero;
		byte[] result;
		try
		{
			IntPtr intPtr2 = Marshal.SecureStringToGlobalAllocUnicode(password);
			intPtr = intPtr2;
			Debug.Assert(intPtr2 != IntPtr.Zero, "clearPassword is Intptr::Zero.");
			bool flag = 0;
			result = ADALNativeWrapper.ADALGetAccessToken(username, intPtr2, stsURL, servicePrincipalName, correlationId, clientId, ref flag, ref fileTime);
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
		}
		return result;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000047D0 File Offset: 0x00003BD0
	internal static byte[] ADALGetAccessTokenForWindowsIntegrated(string stsURL, string servicePrincipalName, ValueType correlationId, string clientId, ref long fileTime)
	{
		bool flag = 1;
		return ADALNativeWrapper.ADALGetAccessToken(null, IntPtr.Zero, stsURL, servicePrincipalName, correlationId, clientId, ref flag, ref fileTime);
	}
}
