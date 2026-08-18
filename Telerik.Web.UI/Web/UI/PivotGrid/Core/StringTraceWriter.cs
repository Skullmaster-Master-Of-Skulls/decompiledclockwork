using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CA4 RID: 3236
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Will fix soon.")]
	public class StringTraceWriter : IPivotTraceWriter
	{
		// Token: 0x06007972 RID: 31090 RVA: 0x001BE8AF File Offset: 0x001BCAAF
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor", Justification = "Will fix soon.")]
		public StringTraceWriter()
		{
			this.writer = new StringWriter();
			this.writer.NewLine = string.Empty;
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x001BE8D2 File Offset: 0x001BCAD2
		public void WriteLine(string text)
		{
			this.writer.WriteLine(text);
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001BE8E0 File Offset: 0x001BCAE0
		public override string ToString()
		{
			return this.writer.ToString();
		}

		// Token: 0x0400212D RID: 8493
		private StringWriter writer;
	}
}
