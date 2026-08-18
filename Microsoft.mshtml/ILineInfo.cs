using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CB1 RID: 3249
	[InterfaceType(1)]
	[Guid("3050F7E2-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface ILineInfo
	{
		// Token: 0x1700759A RID: 30106
		// (get) Token: 0x06016282 RID: 90754
		[DispId(1001)]
		int x { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700759B RID: 30107
		// (get) Token: 0x06016283 RID: 90755
		[DispId(1002)]
		int baseLine { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700759C RID: 30108
		// (get) Token: 0x06016284 RID: 90756
		[DispId(1003)]
		int textDescent { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700759D RID: 30109
		// (get) Token: 0x06016285 RID: 90757
		[DispId(1004)]
		int textHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700759E RID: 30110
		// (get) Token: 0x06016286 RID: 90758
		[DispId(1005)]
		int lineDirection { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
