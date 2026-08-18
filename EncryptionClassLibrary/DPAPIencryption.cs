using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000006 RID: 6
	public class DPAPIencryption
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003200 File Offset: 0x00001400
		public static string ProtectData(string p_clearText, string p_entropy)
		{
			return DPAPIencryption.protectData(p_clearText, p_entropy, DateTime.Now.ToString(), 1, DPAPIencryption.CipherFormat.Base64);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003228 File Offset: 0x00001428
		public static string ProtectData(string p_clearText, string p_entropy, string p_description, int p_flags, DPAPIencryption.CipherFormat p_format)
		{
			return DPAPIencryption.protectData(p_clearText, p_entropy, p_description, p_flags, p_format);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003248 File Offset: 0x00001448
		public static string GetEntropy()
		{
			return "clockworkmike";
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003260 File Offset: 0x00001460
		private static string protectData(string p_clearText, string p_entropy, string p_description, int p_flags, DPAPIencryption.CipherFormat p_format)
		{
			string result = null;
			byte[] array = null;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB2;
			DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB3;
			try
			{
				bool flag = p_clearText != null && p_clearText.Length > 0;
				if (!flag)
				{
					return result;
				}
				byte[] bytes = Encoding.Unicode.GetBytes(p_clearText);
				bool flag2 = p_entropy != null && p_entropy.Length > 0;
				if (!flag2)
				{
					return result;
				}
				byte[] bytes2 = Encoding.Unicode.GetBytes(p_entropy);
				data_BLOB = default(DPAPIencryption.PInvoke.DATA_BLOB);
				data_BLOB.cbData = bytes.Length;
				data_BLOB.pbData = Marshal.AllocHGlobal(data_BLOB.cbData);
				Marshal.Copy(bytes, 0, data_BLOB.pbData, data_BLOB.cbData);
				data_BLOB2 = default(DPAPIencryption.PInvoke.DATA_BLOB);
				data_BLOB2.cbData = bytes2.Length;
				data_BLOB2.pbData = Marshal.AllocHGlobal(data_BLOB2.cbData);
				Marshal.Copy(bytes2, 0, data_BLOB2.pbData, data_BLOB2.cbData);
				cryptprotect_PROMPTSTRUCT = default(DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT);
				cryptprotect_PROMPTSTRUCT.cbSize = Marshal.SizeOf(typeof(DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT));
				cryptprotect_PROMPTSTRUCT.dwPromptFlags = 0;
				cryptprotect_PROMPTSTRUCT.hwndApp = DPAPIencryption.PInvoke.NullPtr;
				cryptprotect_PROMPTSTRUCT.szPrompt = null;
				data_BLOB3 = default(DPAPIencryption.PInvoke.DATA_BLOB);
			}
			catch
			{
				return result;
			}
			try
			{
				bool flag3 = DPAPIencryption.PInvoke.CryptProtectData(ref data_BLOB, p_description, ref data_BLOB2, DPAPIencryption.PInvoke.NullPtr, ref cryptprotect_PROMPTSTRUCT, p_flags, ref data_BLOB3);
				if (flag3)
				{
					array = new byte[data_BLOB3.cbData];
					Marshal.Copy(data_BLOB3.pbData, array, 0, data_BLOB3.cbData);
				}
			}
			finally
			{
				DPAPIencryption.PInvoke.LocalFree(data_BLOB3.pbData);
			}
			bool flag4 = array != null;
			if (flag4)
			{
				result = Convert.ToBase64String(array);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003438 File Offset: 0x00001638
		public static string UnProtectData(string p_cipherText, string p_entropy)
		{
			string result = null;
			byte[] array = null;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB2;
			DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT;
			DPAPIencryption.PInvoke.DATA_BLOB data_BLOB3;
			try
			{
				bool flag = p_cipherText != null && p_cipherText.Length > 0;
				if (!flag)
				{
					return result;
				}
				byte[] array2 = Convert.FromBase64String(p_cipherText);
				bool flag2 = p_entropy != null && p_entropy.Length > 0;
				if (!flag2)
				{
					return result;
				}
				byte[] bytes = Encoding.Unicode.GetBytes(p_entropy);
				data_BLOB = default(DPAPIencryption.PInvoke.DATA_BLOB);
				data_BLOB.cbData = array2.Length;
				data_BLOB.pbData = Marshal.AllocHGlobal(data_BLOB.cbData);
				Marshal.Copy(array2, 0, data_BLOB.pbData, data_BLOB.cbData);
				data_BLOB2 = default(DPAPIencryption.PInvoke.DATA_BLOB);
				data_BLOB2.cbData = bytes.Length;
				data_BLOB2.pbData = Marshal.AllocHGlobal(data_BLOB2.cbData);
				Marshal.Copy(bytes, 0, data_BLOB2.pbData, data_BLOB2.cbData);
				cryptprotect_PROMPTSTRUCT = default(DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT);
				cryptprotect_PROMPTSTRUCT.cbSize = Marshal.SizeOf(typeof(DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT));
				cryptprotect_PROMPTSTRUCT.dwPromptFlags = 0;
				cryptprotect_PROMPTSTRUCT.hwndApp = DPAPIencryption.PInvoke.NullPtr;
				cryptprotect_PROMPTSTRUCT.szPrompt = null;
				data_BLOB3 = default(DPAPIencryption.PInvoke.DATA_BLOB);
			}
			catch
			{
				return result;
			}
			try
			{
				bool flag3 = DPAPIencryption.PInvoke.CryptUnprotectData(ref data_BLOB, null, ref data_BLOB2, DPAPIencryption.PInvoke.NullPtr, ref cryptprotect_PROMPTSTRUCT, 65, ref data_BLOB3);
				if (flag3)
				{
					array = new byte[data_BLOB3.cbData];
					Marshal.Copy(data_BLOB3.pbData, array, 0, data_BLOB3.cbData);
				}
			}
			finally
			{
				DPAPIencryption.PInvoke.LocalFree(data_BLOB3.pbData);
			}
			bool flag4 = array != null;
			if (flag4)
			{
				result = Encoding.Unicode.GetString(array);
			}
			return result;
		}

		// Token: 0x0400000C RID: 12
		public const int PromptOnProtect = 2;

		// Token: 0x0400000D RID: 13
		public const int PromptOnUnprotect = 1;

		// Token: 0x0400000E RID: 14
		public const int UIForbidden = 1;

		// Token: 0x0400000F RID: 15
		public const int LocalMachine = 4;

		// Token: 0x04000010 RID: 16
		public const int PromptStrong = 8;

		// Token: 0x04000011 RID: 17
		public const int Audit = 16;

		// Token: 0x04000012 RID: 18
		public const int VerifyProtection = 64;

		// Token: 0x04000013 RID: 19
		public const int CredSync = 8;

		// Token: 0x04000014 RID: 20
		public const int NoRecovery = 32;

		// Token: 0x0200001B RID: 27
		public enum CipherFormat
		{
			// Token: 0x0400003E RID: 62
			Base64,
			// Token: 0x0400003F RID: 63
			Binary
		}

		// Token: 0x0200001C RID: 28
		internal class PInvoke
		{
			// Token: 0x060000BE RID: 190
			[DllImport("crypt32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern bool CryptProtectData(ref DPAPIencryption.PInvoke.DATA_BLOB dataIn, string szDataDescr, ref DPAPIencryption.PInvoke.DATA_BLOB optionalEntropy, IntPtr pvReserved, ref DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DPAPIencryption.PInvoke.DATA_BLOB pDataOut);

			// Token: 0x060000BF RID: 191
			[DllImport("crypt32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern bool CryptUnprotectData(ref DPAPIencryption.PInvoke.DATA_BLOB dataIn, StringBuilder ppszDataDescr, ref DPAPIencryption.PInvoke.DATA_BLOB optionalEntropy, IntPtr pvReserved, ref DPAPIencryption.PInvoke.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DPAPIencryption.PInvoke.DATA_BLOB pDataOut);

			// Token: 0x060000C0 RID: 192
			[DllImport("kernel32")]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x04000040 RID: 64
			public static IntPtr NullPtr = (IntPtr)0;

			// Token: 0x02000023 RID: 35
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct DATA_BLOB
			{
				// Token: 0x04000051 RID: 81
				public int cbData;

				// Token: 0x04000052 RID: 82
				public IntPtr pbData;
			}

			// Token: 0x02000024 RID: 36
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct CRYPTPROTECT_PROMPTSTRUCT
			{
				// Token: 0x04000053 RID: 83
				public int cbSize;

				// Token: 0x04000054 RID: 84
				public int dwPromptFlags;

				// Token: 0x04000055 RID: 85
				public IntPtr hwndApp;

				// Token: 0x04000056 RID: 86
				public string szPrompt;
			}
		}
	}
}
