using System;
using System.DirectoryServices.Interop;
using System.Runtime.InteropServices;
using System.Text;

namespace System.DirectoryServices
{
	// Token: 0x02000047 RID: 71
	internal class COMExceptionHelper
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00007D78 File Offset: 0x00006D78
		internal static Exception CreateFormattedComException(int hr)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			int num = SafeNativeMethods.FormatMessageW(12800, 0, hr, 0, stringBuilder, stringBuilder.Capacity + 1, 0);
			string message;
			if (num != 0)
			{
				message = stringBuilder.ToString(0, num);
			}
			else
			{
				message = Res.GetString("DSUnknown", new object[]
				{
					Convert.ToString(hr, 16)
				});
			}
			return COMExceptionHelper.CreateFormattedComException(new COMException(message, hr));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007DE8 File Offset: 0x00006DE8
		internal static Exception CreateFormattedComException(COMException e)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			StringBuilder nameBuffer = new StringBuilder();
			int num = 0;
			SafeNativeMethods.ADsGetLastError(out num, stringBuilder, 256, nameBuffer, 0);
			if (num != 0)
			{
				return new DirectoryServicesCOMException(stringBuilder.ToString(), num, e);
			}
			return e;
		}
	}
}
