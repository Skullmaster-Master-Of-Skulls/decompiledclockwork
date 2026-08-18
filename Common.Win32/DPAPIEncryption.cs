using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000005 RID: 5
	public class DPAPIEncryption
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002580 File Offset: 0x00000780
		public static string ProtectData(string p_clearText, string p_entropy)
		{
			return DPAPIEncryption.protectData(p_clearText, p_entropy, DateTime.Now.ToString(), 1, DPAPIEncryption.CipherFormat.Base64);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025A3 File Offset: 0x000007A3
		public static string ProtectData(string p_clearText, string p_entropy, string p_description, int p_flags, DPAPIEncryption.CipherFormat p_format)
		{
			return DPAPIEncryption.protectData(p_clearText, p_entropy, p_description, p_flags, p_format);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025B0 File Offset: 0x000007B0
		public static string GetEntropy()
		{
			return "clockworkmike";
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025B8 File Offset: 0x000007B8
		private static string protectData(string p_clearText, string p_entropy, string p_description, int p_flags, DPAPIEncryption.CipherFormat p_format)
		{
			string result = null;
			byte[] array = null;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB2;
			DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB3;
			try
			{
				if (p_clearText == null || p_clearText.Length <= 0)
				{
					return result;
				}
				byte[] bytes = Encoding.Unicode.GetBytes(p_clearText);
				if (p_entropy == null || p_entropy.Length <= 0)
				{
					return result;
				}
				byte[] bytes2 = Encoding.Unicode.GetBytes(p_entropy);
				data_BLOB = default(DPAPIEncryption.PInvoke.DATA_BLOB);
				data_BLOB.cbData = bytes.Length;
				data_BLOB.pbData = Marshal.AllocHGlobal(data_BLOB.cbData);
				Marshal.Copy(bytes, 0, data_BLOB.pbData, data_BLOB.cbData);
				data_BLOB2 = default(DPAPIEncryption.PInvoke.DATA_BLOB);
				data_BLOB2.cbData = bytes2.Length;
				data_BLOB2.pbData = Marshal.AllocHGlobal(data_BLOB2.cbData);
				Marshal.Copy(bytes2, 0, data_BLOB2.pbData, data_BLOB2.cbData);
				cryptprotect_PROMPTSTRUCT = default(DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT);
				cryptprotect_PROMPTSTRUCT.cbSize = Marshal.SizeOf(typeof(DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT));
				cryptprotect_PROMPTSTRUCT.dwPromptFlags = 0;
				cryptprotect_PROMPTSTRUCT.hwndApp = DPAPIEncryption.PInvoke.NullPtr;
				cryptprotect_PROMPTSTRUCT.szPrompt = null;
				data_BLOB3 = default(DPAPIEncryption.PInvoke.DATA_BLOB);
			}
			catch
			{
				return result;
			}
			try
			{
				if (DPAPIEncryption.PInvoke.CryptProtectData(ref data_BLOB, p_description, ref data_BLOB2, DPAPIEncryption.PInvoke.NullPtr, ref cryptprotect_PROMPTSTRUCT, p_flags, ref data_BLOB3))
				{
					array = new byte[data_BLOB3.cbData];
					Marshal.Copy(data_BLOB3.pbData, array, 0, data_BLOB3.cbData);
				}
			}
			finally
			{
				DPAPIEncryption.PInvoke.LocalFree(data_BLOB3.pbData);
			}
			if (array != null)
			{
				result = Convert.ToBase64String(array);
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002744 File Offset: 0x00000944
		public static string UnProtectData(string p_cipherText, string p_entropy)
		{
			string result = null;
			byte[] array = null;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB2;
			DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT;
			DPAPIEncryption.PInvoke.DATA_BLOB data_BLOB3;
			try
			{
				if (p_cipherText == null || p_cipherText.Length <= 0)
				{
					return result;
				}
				byte[] array2 = Convert.FromBase64String(p_cipherText);
				if (p_entropy == null || p_entropy.Length <= 0)
				{
					return result;
				}
				byte[] bytes = Encoding.Unicode.GetBytes(p_entropy);
				data_BLOB = default(DPAPIEncryption.PInvoke.DATA_BLOB);
				data_BLOB.cbData = array2.Length;
				data_BLOB.pbData = Marshal.AllocHGlobal(data_BLOB.cbData);
				Marshal.Copy(array2, 0, data_BLOB.pbData, data_BLOB.cbData);
				data_BLOB2 = default(DPAPIEncryption.PInvoke.DATA_BLOB);
				data_BLOB2.cbData = bytes.Length;
				data_BLOB2.pbData = Marshal.AllocHGlobal(data_BLOB2.cbData);
				Marshal.Copy(bytes, 0, data_BLOB2.pbData, data_BLOB2.cbData);
				cryptprotect_PROMPTSTRUCT = default(DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT);
				cryptprotect_PROMPTSTRUCT.cbSize = Marshal.SizeOf(typeof(DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT));
				cryptprotect_PROMPTSTRUCT.dwPromptFlags = 0;
				cryptprotect_PROMPTSTRUCT.hwndApp = DPAPIEncryption.PInvoke.NullPtr;
				cryptprotect_PROMPTSTRUCT.szPrompt = null;
				data_BLOB3 = default(DPAPIEncryption.PInvoke.DATA_BLOB);
			}
			catch
			{
				return result;
			}
			try
			{
				if (DPAPIEncryption.PInvoke.CryptUnprotectData(ref data_BLOB, null, ref data_BLOB2, DPAPIEncryption.PInvoke.NullPtr, ref cryptprotect_PROMPTSTRUCT, 65, ref data_BLOB3))
				{
					array = new byte[data_BLOB3.cbData];
					Marshal.Copy(data_BLOB3.pbData, array, 0, data_BLOB3.cbData);
				}
			}
			finally
			{
				DPAPIEncryption.PInvoke.LocalFree(data_BLOB3.pbData);
			}
			if (array != null)
			{
				result = Encoding.Unicode.GetString(array);
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		public const int PromptOnProtect = 2;

		// Token: 0x04000002 RID: 2
		public const int PromptOnUnprotect = 1;

		// Token: 0x04000003 RID: 3
		public const int UIForbidden = 1;

		// Token: 0x04000004 RID: 4
		public const int LocalMachine = 4;

		// Token: 0x04000005 RID: 5
		public const int PromptStrong = 8;

		// Token: 0x04000006 RID: 6
		public const int Audit = 16;

		// Token: 0x04000007 RID: 7
		public const int VerifyProtection = 64;

		// Token: 0x04000008 RID: 8
		public const int CredSync = 8;

		// Token: 0x04000009 RID: 9
		public const int NoRecovery = 32;

		// Token: 0x0200001C RID: 28
		public enum CipherFormat
		{
			// Token: 0x04000064 RID: 100
			Base64,
			// Token: 0x04000065 RID: 101
			Binary
		}

		// Token: 0x0200001D RID: 29
		internal class PInvoke
		{
			// Token: 0x06000099 RID: 153
			[DllImport("crypt32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern bool CryptProtectData(ref DPAPIEncryption.PInvoke.DATA_BLOB dataIn, string szDataDescr, ref DPAPIEncryption.PInvoke.DATA_BLOB optionalEntropy, IntPtr pvReserved, ref DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DPAPIEncryption.PInvoke.DATA_BLOB pDataOut);

			// Token: 0x0600009A RID: 154
			[DllImport("crypt32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern bool CryptUnprotectData(ref DPAPIEncryption.PInvoke.DATA_BLOB dataIn, StringBuilder ppszDataDescr, ref DPAPIEncryption.PInvoke.DATA_BLOB optionalEntropy, IntPtr pvReserved, ref DPAPIEncryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DPAPIEncryption.PInvoke.DATA_BLOB pDataOut);

			// Token: 0x0600009B RID: 155
			[DllImport("kernel32")]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x04000066 RID: 102
			public static IntPtr NullPtr = (IntPtr)0;

			// Token: 0x0200001F RID: 31
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct DATA_BLOB
			{
				// Token: 0x04000069 RID: 105
				public int cbData;

				// Token: 0x0400006A RID: 106
				public IntPtr pbData;
			}

			// Token: 0x02000020 RID: 32
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct CRYPTPROTECT_PROMPTSTRUCT
			{
				// Token: 0x0400006B RID: 107
				public int cbSize;

				// Token: 0x0400006C RID: 108
				public int dwPromptFlags;

				// Token: 0x0400006D RID: 109
				public IntPtr hwndApp;

				// Token: 0x0400006E RID: 110
				public string szPrompt;
			}
		}
	}
}
