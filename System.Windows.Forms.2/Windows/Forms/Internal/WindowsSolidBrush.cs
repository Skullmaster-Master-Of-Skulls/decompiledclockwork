using System;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EE RID: 1262
	internal sealed class WindowsSolidBrush : WindowsBrush
	{
		// Token: 0x06005233 RID: 21043 RVA: 0x00155684 File Offset: 0x00153884
		protected override void CreateBrush()
		{
			IntPtr intPtr = IntSafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(base.Color));
			intPtr == IntPtr.Zero;
			base.NativeHandle = intPtr;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x001556B5 File Offset: 0x001538B5
		public WindowsSolidBrush(DeviceContext dc) : base(dc)
		{
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x001556BE File Offset: 0x001538BE
		public WindowsSolidBrush(DeviceContext dc, Color color) : base(dc, color)
		{
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x001556C8 File Offset: 0x001538C8
		public override object Clone()
		{
			return new WindowsSolidBrush(base.DC, base.Color);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x001556DB File Offset: 0x001538DB
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: Color={1}", new object[]
			{
				base.GetType().Name,
				base.Color
			});
		}
	}
}
