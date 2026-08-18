using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// Token: 0x02000002 RID: 2
internal class Certificate
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static byte[] CreateSelfSignCertificatePfx(string x500, DateTime startTime, DateTime endTime)
	{
		return Certificate.CreateSelfSignCertificatePfx(x500, startTime, endTime, null);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
	public static byte[] CreateSelfSignCertificatePfx(string x500, DateTime startTime, DateTime endTime, string insecurePassword)
	{
		SecureString secureString = null;
		byte[] result;
		try
		{
			if (!string.IsNullOrEmpty(insecurePassword))
			{
				secureString = new SecureString();
				foreach (char c in insecurePassword)
				{
					secureString.AppendChar(c);
				}
				secureString.MakeReadOnly();
			}
			result = Certificate.CreateSelfSignCertificatePfx(x500, startTime, endTime, secureString);
		}
		finally
		{
			if (secureString != null)
			{
				secureString.Dispose();
			}
		}
		return result;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020CC File Offset: 0x000002CC
	public static byte[] CreateSelfSignCertificatePfx(string x500, DateTime startTime, DateTime endTime, SecureString password)
	{
		if (x500 == null)
		{
			x500 = "";
		}
		Certificate.SystemTime systemTime = Certificate.ToSystemTime(startTime);
		Certificate.SystemTime systemTime2 = Certificate.ToSystemTime(endTime);
		string text = Guid.NewGuid().ToString();
		GCHandle gchandle = default(GCHandle);
		IntPtr zero = IntPtr.Zero;
		IntPtr zero2 = IntPtr.Zero;
		IntPtr intPtr = IntPtr.Zero;
		IntPtr intPtr2 = IntPtr.Zero;
		IntPtr zero3 = IntPtr.Zero;
		IntPtr intPtr3 = IntPtr.Zero;
		RuntimeHelpers.PrepareConstrainedRegions();
		byte[] array2;
		try
		{
			Certificate.Check(Certificate.NativeMethods.CryptAcquireContextW(out zero, text, null, 1, 8));
			Certificate.Check(Certificate.NativeMethods.CryptGenKey(zero, 1, 1, out zero2));
			int num = 0;
			gchandle = GCHandle.Alloc(x500, GCHandleType.Pinned);
			IntPtr ptr;
			if (!Certificate.NativeMethods.CertStrToNameW(65537, gchandle.AddrOfPinnedObject(), 3, IntPtr.Zero, null, ref num, out ptr))
			{
				throw new ArgumentException(Marshal.PtrToStringUni(ptr));
			}
			byte[] array = new byte[num];
			if (!Certificate.NativeMethods.CertStrToNameW(65537, gchandle.AddrOfPinnedObject(), 3, IntPtr.Zero, array, ref num, out ptr))
			{
				throw new ArgumentException(Marshal.PtrToStringUni(ptr));
			}
			gchandle.Free();
			gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			Certificate.CryptoApiBlob cryptoApiBlob = new Certificate.CryptoApiBlob(array.Length, gchandle.AddrOfPinnedObject());
			Certificate.CryptKeyProviderInformation cryptKeyProviderInformation = default(Certificate.CryptKeyProviderInformation);
			cryptKeyProviderInformation.ContainerName = text;
			cryptKeyProviderInformation.ProviderType = 1;
			cryptKeyProviderInformation.KeySpec = 1;
			intPtr = Certificate.NativeMethods.CertCreateSelfSignCertificate(zero, ref cryptoApiBlob, 0, ref cryptKeyProviderInformation, IntPtr.Zero, ref systemTime, ref systemTime2, IntPtr.Zero);
			Certificate.Check(intPtr != IntPtr.Zero);
			gchandle.Free();
			intPtr2 = Certificate.NativeMethods.CertOpenStore("Memory", 0, IntPtr.Zero, 8192, IntPtr.Zero);
			Certificate.Check(intPtr2 != IntPtr.Zero);
			Certificate.Check(Certificate.NativeMethods.CertAddCertificateContextToStore(intPtr2, intPtr, 1, out zero3));
			Certificate.NativeMethods.CertSetCertificateContextProperty(zero3, 2, 0, ref cryptKeyProviderInformation);
			if (password != null)
			{
				intPtr3 = Marshal.SecureStringToCoTaskMemUnicode(password);
			}
			Certificate.CryptoApiBlob cryptoApiBlob2 = default(Certificate.CryptoApiBlob);
			Certificate.Check(Certificate.NativeMethods.PFXExportCertStoreEx(intPtr2, ref cryptoApiBlob2, intPtr3, IntPtr.Zero, 7));
			array2 = new byte[cryptoApiBlob2.DataLength];
			gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
			cryptoApiBlob2.Data = gchandle.AddrOfPinnedObject();
			Certificate.Check(Certificate.NativeMethods.PFXExportCertStoreEx(intPtr2, ref cryptoApiBlob2, intPtr3, IntPtr.Zero, 7));
			gchandle.Free();
		}
		finally
		{
			if (intPtr3 != IntPtr.Zero)
			{
				Marshal.ZeroFreeCoTaskMemUnicode(intPtr3);
			}
			if (gchandle.IsAllocated)
			{
				gchandle.Free();
			}
			if (intPtr != IntPtr.Zero)
			{
				Certificate.NativeMethods.CertFreeCertificateContext(intPtr);
			}
			if (zero3 != IntPtr.Zero)
			{
				Certificate.NativeMethods.CertFreeCertificateContext(zero3);
			}
			if (intPtr2 != IntPtr.Zero)
			{
				Certificate.NativeMethods.CertCloseStore(intPtr2, 0);
			}
			if (zero2 != IntPtr.Zero)
			{
				Certificate.NativeMethods.CryptDestroyKey(zero2);
			}
			if (zero != IntPtr.Zero)
			{
				Certificate.NativeMethods.CryptReleaseContext(zero, 0);
				Certificate.NativeMethods.CryptAcquireContextW(out zero, text, null, 1, 16);
			}
		}
		return array2;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000023BC File Offset: 0x000005BC
	private static Certificate.SystemTime ToSystemTime(DateTime dateTime)
	{
		long num = dateTime.ToFileTime();
		Certificate.SystemTime result;
		Certificate.Check(Certificate.NativeMethods.FileTimeToSystemTime(ref num, out result));
		return result;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000023E0 File Offset: 0x000005E0
	private static void Check(bool nativeCallSucceeded)
	{
		if (!nativeCallSucceeded)
		{
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}
	}

	// Token: 0x02000018 RID: 24
	private struct SystemTime
	{
		// Token: 0x04000052 RID: 82
		public short Year;

		// Token: 0x04000053 RID: 83
		public short Month;

		// Token: 0x04000054 RID: 84
		public short DayOfWeek;

		// Token: 0x04000055 RID: 85
		public short Day;

		// Token: 0x04000056 RID: 86
		public short Hour;

		// Token: 0x04000057 RID: 87
		public short Minute;

		// Token: 0x04000058 RID: 88
		public short Second;

		// Token: 0x04000059 RID: 89
		public short Milliseconds;
	}

	// Token: 0x02000019 RID: 25
	private struct CryptoApiBlob
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004F20 File Offset: 0x00003120
		public CryptoApiBlob(int dataLength, IntPtr data)
		{
			this.DataLength = dataLength;
			this.Data = data;
		}

		// Token: 0x0400005A RID: 90
		public int DataLength;

		// Token: 0x0400005B RID: 91
		public IntPtr Data;
	}

	// Token: 0x0200001A RID: 26
	private struct CryptKeyProviderInformation
	{
		// Token: 0x0400005C RID: 92
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ContainerName;

		// Token: 0x0400005D RID: 93
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProviderName;

		// Token: 0x0400005E RID: 94
		public int ProviderType;

		// Token: 0x0400005F RID: 95
		public int Flags;

		// Token: 0x04000060 RID: 96
		public int ProviderParameterCount;

		// Token: 0x04000061 RID: 97
		public IntPtr ProviderParameters;

		// Token: 0x04000062 RID: 98
		public int KeySpec;
	}

	// Token: 0x0200001B RID: 27
	private static class NativeMethods
	{
		// Token: 0x0600008C RID: 140
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FileTimeToSystemTime([In] ref long fileTime, out Certificate.SystemTime systemTime);

		// Token: 0x0600008D RID: 141
		[DllImport("AdvApi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptAcquireContextW(out IntPtr providerContext, [MarshalAs(UnmanagedType.LPWStr)] string container, [MarshalAs(UnmanagedType.LPWStr)] string provider, int providerType, int flags);

		// Token: 0x0600008E RID: 142
		[DllImport("AdvApi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptReleaseContext(IntPtr providerContext, int flags);

		// Token: 0x0600008F RID: 143
		[DllImport("AdvApi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGenKey(IntPtr providerContext, int algorithmId, int flags, out IntPtr cryptKeyHandle);

		// Token: 0x06000090 RID: 144
		[DllImport("AdvApi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptDestroyKey(IntPtr cryptKeyHandle);

		// Token: 0x06000091 RID: 145
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertStrToNameW(int certificateEncodingType, IntPtr x500, int strType, IntPtr reserved, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] encoded, ref int encodedLength, out IntPtr errorString);

		// Token: 0x06000092 RID: 146
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr CertCreateSelfSignCertificate(IntPtr providerHandle, [In] ref Certificate.CryptoApiBlob subjectIssuerBlob, int flags, [In] ref Certificate.CryptKeyProviderInformation keyProviderInformation, IntPtr signatureAlgorithm, [In] ref Certificate.SystemTime startTime, [In] ref Certificate.SystemTime endTime, IntPtr extensions);

		// Token: 0x06000093 RID: 147
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertFreeCertificateContext(IntPtr certificateContext);

		// Token: 0x06000094 RID: 148
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr CertOpenStore([MarshalAs(UnmanagedType.LPStr)] string storeProvider, int messageAndCertificateEncodingType, IntPtr cryptProvHandle, int flags, IntPtr parameters);

		// Token: 0x06000095 RID: 149
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertCloseStore(IntPtr certificateStoreHandle, int flags);

		// Token: 0x06000096 RID: 150
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertAddCertificateContextToStore(IntPtr certificateStoreHandle, IntPtr certificateContext, int addDisposition, out IntPtr storeContextPtr);

		// Token: 0x06000097 RID: 151
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertSetCertificateContextProperty(IntPtr certificateContext, int propertyId, int flags, [In] ref Certificate.CryptKeyProviderInformation data);

		// Token: 0x06000098 RID: 152
		[DllImport("Crypt32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PFXExportCertStoreEx(IntPtr certificateStoreHandle, ref Certificate.CryptoApiBlob pfxBlob, IntPtr password, IntPtr reserved, int flags);
	}
}
