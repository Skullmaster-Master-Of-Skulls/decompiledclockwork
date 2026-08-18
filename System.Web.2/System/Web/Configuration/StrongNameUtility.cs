using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Runtime.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x02000759 RID: 1881
	internal class StrongNameUtility
	{
		// Token: 0x06005AB1 RID: 23217 RVA: 0x000030B5 File Offset: 0x000012B5
		private StrongNameUtility()
		{
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0013BA88 File Offset: 0x00139C88
		internal static bool GenerateStrongNameFile(string filename)
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			bool flag = StrongNameHelpers.StrongNameKeyGen(null, 0, out zero, out num);
			if (!flag || zero == IntPtr.Zero)
			{
				throw Marshal.GetExceptionForHR(StrongNameHelpers.StrongNameErrorInfo());
			}
			try
			{
				if (num <= 0 || num > 2147483647)
				{
					throw new InvalidOperationException(SR.GetString("Browser_InvalidStrongNameKey"));
				}
				byte[] array = new byte[num];
				Marshal.Copy(zero, array, 0, num);
				using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(array);
					}
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					StrongNameHelpers.StrongNameFreeBuffer(zero);
				}
			}
			return true;
		}
	}
}
