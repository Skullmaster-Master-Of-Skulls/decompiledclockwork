using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace a
{
	// Token: 0x020004F6 RID: 1270
	internal static class bp
	{
		// Token: 0x06002A47 RID: 10823 RVA: 0x000C653C File Offset: 0x000C553C
		public static Task a<a>(this Func<a, Task> A_0, a A_1)
		{
			bp<a>.b b;
			b.c = A_0;
			b.d = A_1;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<bp<a>.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000C658C File Offset: 0x000C558C
		public static Task a<a, b>(this Func<a, b, Task> A_0, a A_1, b A_2)
		{
			bp<a, b>.a a;
			a.c = A_0;
			a.d = A_1;
			a.e = A_2;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<bp<a, b>.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04001D2C RID: 7468
		public const string a = "Sync I/O not supported on this platform. Use async methods.";
	}
}
