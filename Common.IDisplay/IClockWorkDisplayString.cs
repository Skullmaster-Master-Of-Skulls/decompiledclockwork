using System;

namespace TechnoPro.Common.IDisplay
{
	// Token: 0x02000002 RID: 2
	public interface IClockWorkDisplayString<T>
	{
		// Token: 0x06000001 RID: 1
		string GetDisplayString(T t, eDisplayFormatType format, DisplayParameters dispParameters = null);
	}
}
