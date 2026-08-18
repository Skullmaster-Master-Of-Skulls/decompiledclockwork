using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web
{
	// Token: 0x020000FD RID: 253
	internal class StringResourceManager
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x000030B5 File Offset: 0x000012B5
		private StringResourceManager()
		{
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002BC84 File Offset: 0x00029E84
		internal unsafe static string ResourceToString(IntPtr pv, int offset, int size)
		{
			return new string((sbyte*)((void*)pv), offset, size, Encoding.UTF8);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002BC98 File Offset: 0x00029E98
		internal static SafeStringResource ReadSafeStringResource(Type t)
		{
			if (HttpRuntime.CodegenDirInternal != null)
			{
				InternalSecurityPermissions.PathDiscovery(HttpRuntime.CodegenDirInternal).Assert();
			}
			string fullyQualifiedName = t.Module.FullyQualifiedName;
			IntPtr intPtr = UnsafeNativeMethods.GetModuleHandle(fullyQualifiedName);
			if (intPtr == IntPtr.Zero)
			{
				intPtr = Marshal.GetHINSTANCE(t.Module);
				if (intPtr == IntPtr.Zero)
				{
					throw new HttpException(SR.GetString("Resource_problem", new object[]
					{
						"GetModuleHandle",
						HttpException.HResultFromLastError(Marshal.GetLastWin32Error()).ToString(CultureInfo.InvariantCulture)
					}));
				}
			}
			IntPtr intPtr2 = UnsafeNativeMethods.FindResource(intPtr, (IntPtr)101, (IntPtr)3771);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new HttpException(SR.GetString("Resource_problem", new object[]
				{
					"FindResource",
					HttpException.HResultFromLastError(Marshal.GetLastWin32Error()).ToString(CultureInfo.InvariantCulture)
				}));
			}
			int num = UnsafeNativeMethods.SizeofResource(intPtr, intPtr2);
			IntPtr intPtr3 = UnsafeNativeMethods.LoadResource(intPtr, intPtr2);
			if (intPtr3 == IntPtr.Zero)
			{
				throw new HttpException(SR.GetString("Resource_problem", new object[]
				{
					"LoadResource",
					HttpException.HResultFromLastError(Marshal.GetLastWin32Error()).ToString(CultureInfo.InvariantCulture)
				}));
			}
			IntPtr intPtr4 = UnsafeNativeMethods.LockResource(intPtr3);
			if (intPtr4 == IntPtr.Zero)
			{
				throw new HttpException(SR.GetString("Resource_problem", new object[]
				{
					"LockResource",
					HttpException.HResultFromLastError(Marshal.GetLastWin32Error()).ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (!UnsafeNativeMethods.IsValidResource(intPtr, intPtr4, num))
			{
				throw new InvalidOperationException();
			}
			return new SafeStringResource(intPtr4, num);
		}

		// Token: 0x040005D6 RID: 1494
		internal const int RESOURCE_TYPE = 3771;

		// Token: 0x040005D7 RID: 1495
		internal const int RESOURCE_ID = 101;
	}
}
