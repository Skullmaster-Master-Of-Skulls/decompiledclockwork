using System;
using System.Globalization;
using System.IO;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000015 RID: 21
	public static class ObjectInfo
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000053C4 File Offset: 0x000035C4
		public static HelperResult Print(object value, int depth = 10, int enumerationLength = 1000)
		{
			if (depth < 0)
			{
				throw new ArgumentOutOfRangeException("depth", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (enumerationLength <= 0)
			{
				throw new ArgumentOutOfRangeException("enumerationLength", string.Format(CultureInfo.InvariantCulture, CommonResources.Argument_Must_Be_GreaterThan, new object[]
				{
					0
				}));
			}
			HtmlObjectPrinter printer = new HtmlObjectPrinter(depth, enumerationLength);
			return new HelperResult(delegate(TextWriter writer)
			{
				printer.WriteTo(value, writer);
			});
		}

		// Token: 0x04000045 RID: 69
		private const int DefaultRecursionLimit = 10;

		// Token: 0x04000046 RID: 70
		private const int DefaultEnumerationLimit = 1000;
	}
}
