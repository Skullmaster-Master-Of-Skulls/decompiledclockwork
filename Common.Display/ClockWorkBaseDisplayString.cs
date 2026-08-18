using System;
using TechnoPro.Common.IDisplay;

namespace TechnoPro.Common.Display
{
	// Token: 0x02000002 RID: 2
	public abstract class ClockWorkBaseDisplayString<T> : IClockWorkDisplayString<T>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string GetDisplayString(T t, eDisplayFormatType format, DisplayParameters dispParameters = null)
		{
			if (format == eDisplayFormatType.PlainText)
			{
				return this.GetPlainTextDisplayString(t, dispParameters);
			}
			if (format == eDisplayFormatType.Html)
			{
				return this.GetHtmlDisplayString(t, dispParameters);
			}
			return this.GetPlainTextDisplayString(t, dispParameters);
		}

		// Token: 0x06000002 RID: 2
		protected abstract string GetHtmlDisplayString(T t, DisplayParameters parameters = null);

		// Token: 0x06000003 RID: 3
		protected abstract string GetPlainTextDisplayString(T t, DisplayParameters parameters = null);
	}
}
