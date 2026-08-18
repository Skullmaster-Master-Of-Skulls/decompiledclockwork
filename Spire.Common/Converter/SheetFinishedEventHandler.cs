using System;

namespace Spire.Xls.Converter
{
	// Token: 0x02000027 RID: 39
	public sealed class SheetFinishedEventHandler : MulticastDelegate
	{
		// Token: 0x06000109 RID: 265
		public extern SheetFinishedEventHandler(object @object, IntPtr method);

		// Token: 0x0600010A RID: 266
		public extern void Invoke(object sender, SheetFinishedEventArgs args);

		// Token: 0x0600010B RID: 267
		public extern IAsyncResult BeginInvoke(object sender, SheetFinishedEventArgs args, AsyncCallback callback, object @object);

		// Token: 0x0600010C RID: 268
		public extern void EndInvoke(IAsyncResult result);

		// Token: 0x0600010D RID: 269 RVA: 0x00015020 File Offset: 0x00013220
		internal static string b(string A_0, int A_1)
		{
			char[] array = A_0.ToCharArray();
			int num = 1261399990 + A_1;
			int num3;
			int num2;
			if ((num2 = (num3 = 0)) < 1)
			{
				goto IL_47;
			}
			IL_14:
			int num5;
			int num4 = num5 = num2;
			char[] array2 = array;
			int num6 = num5;
			char c = array[num5];
			byte b = (byte)((int)(c & 'ÿ') ^ num++);
			byte b2 = (byte)((int)(c >> 8) ^ num++);
			byte b3 = b2;
			b2 = b;
			b = b3;
			array2[num6] = (ushort)((int)b2 << 8 | (int)b);
			num3 = num4 + 1;
			IL_47:
			if ((num2 = num3) >= array.Length)
			{
				return string.Intern(new string(array));
			}
			goto IL_14;
		}
	}
}
