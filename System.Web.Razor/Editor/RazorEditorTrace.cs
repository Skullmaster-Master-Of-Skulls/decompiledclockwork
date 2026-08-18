using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.Razor.Resources;

namespace System.Web.Razor.Editor
{
	// Token: 0x0200000C RID: 12
	internal static class RazorEditorTrace
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003040 File Offset: 0x00001240
		private static bool IsEnabled()
		{
			if (RazorEditorTrace._enabled == null)
			{
				bool flag;
				if (bool.TryParse(Environment.GetEnvironmentVariable("RAZOR_EDITOR_TRACE"), out flag))
				{
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, RazorResources.Trace_Startup, new object[]
					{
						flag ? RazorResources.Trace_Enabled : RazorResources.Trace_Disabled
					}));
					RazorEditorTrace._enabled = new bool?(flag);
				}
				else
				{
					RazorEditorTrace._enabled = new bool?(false);
				}
			}
			return RazorEditorTrace._enabled.Value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030BC File Offset: 0x000012BC
		[Conditional("EDITOR_TRACING")]
		public static void TraceLine(string format, params object[] args)
		{
			if (RazorEditorTrace.IsEnabled())
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, RazorResources.Trace_Format, new object[]
				{
					string.Format(CultureInfo.CurrentCulture, format, args)
				}));
			}
		}

		// Token: 0x04000021 RID: 33
		private static bool? _enabled;
	}
}
