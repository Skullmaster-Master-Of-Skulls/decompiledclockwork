using System;
using System.Drawing;

namespace Telerik.Charting
{
	// Token: 0x020016E5 RID: 5861
	public class ChartException : ApplicationException
	{
		// Token: 0x0600E3AA RID: 58282 RVA: 0x0032778D File Offset: 0x0032598D
		public ChartException(string message) : base(message)
		{
		}

		// Token: 0x0600E3AB RID: 58283 RVA: 0x00327796 File Offset: 0x00325996
		public ChartException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600E3AC RID: 58284 RVA: 0x003277A0 File Offset: 0x003259A0
		internal static string WrappedByWidth(ChartGraphics graphics, string text, Font font, float width)
		{
			return RenderEngine.PrepareForVerticalOverflow(graphics, text, font, width);
		}
	}
}
