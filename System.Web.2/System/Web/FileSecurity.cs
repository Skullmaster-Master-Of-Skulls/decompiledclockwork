using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200006F RID: 111
	internal sealed class FileSecurity
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0000A878 File Offset: 0x00008A78
		internal static byte[] GetDacl(string filename)
		{
			if (HostingEnvironment.FcnSkipReadAndCacheDacls)
			{
				return FileSecurity.s_nullDacl;
			}
			int num = 512;
			byte[] array = new byte[num];
			int fileSecurity = UnsafeNativeMethods.GetFileSecurity(filename, 7, array, array.Length, ref num);
			if (fileSecurity != 0)
			{
				if (num == 0)
				{
					return FileSecurity.s_nullDacl;
				}
				Array.Resize<byte>(ref array, num);
			}
			else
			{
				int num2 = HttpException.HResultFromLastError(Marshal.GetLastWin32Error());
				if (num2 != -2147024774)
				{
					return null;
				}
				array = new byte[num];
				if (UnsafeNativeMethods.GetFileSecurity(filename, 7, array, array.Length, ref num) == 0)
				{
					return null;
				}
			}
			byte[] array2 = (byte[])FileSecurity.s_interned[array];
			if (array2 == null)
			{
				object syncRoot = FileSecurity.s_interned.SyncRoot;
				lock (syncRoot)
				{
					array2 = (byte[])FileSecurity.s_interned[array];
					if (array2 == null)
					{
						array2 = array;
						FileSecurity.s_interned[array2] = array2;
					}
				}
			}
			return array2;
		}

		// Token: 0x040001FF RID: 511
		private const int DACL_INFORMATION = 7;

		// Token: 0x04000200 RID: 512
		private static Hashtable s_interned = new Hashtable(0, 1f, new FileSecurity.DaclComparer());

		// Token: 0x04000201 RID: 513
		private static byte[] s_nullDacl = new byte[0];

		// Token: 0x020008C5 RID: 2245
		private class DaclComparer : IEqualityComparer
		{
			// Token: 0x060067D0 RID: 26576 RVA: 0x00170AC4 File Offset: 0x0016ECC4
			private int Compare(byte[] a, byte[] b)
			{
				int num = a.Length - b.Length;
				int num2 = 0;
				while (num == 0 && num2 < a.Length)
				{
					num = (int)(a[num2] - b[num2]);
					num2++;
				}
				return num;
			}

			// Token: 0x060067D1 RID: 26577 RVA: 0x00170AF4 File Offset: 0x0016ECF4
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null && y == null)
				{
					return true;
				}
				if (x == null || y == null)
				{
					return false;
				}
				byte[] array = x as byte[];
				byte[] array2 = y as byte[];
				return array != null && array2 != null && this.Compare(array, array2) == 0;
			}

			// Token: 0x060067D2 RID: 26578 RVA: 0x00170B34 File Offset: 0x0016ED34
			int IEqualityComparer.GetHashCode(object obj)
			{
				byte[] array = (byte[])obj;
				HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
				foreach (byte b in array)
				{
					hashCodeCombiner.AddObject(b);
				}
				return hashCodeCombiner.CombinedHash32;
			}
		}
	}
}
