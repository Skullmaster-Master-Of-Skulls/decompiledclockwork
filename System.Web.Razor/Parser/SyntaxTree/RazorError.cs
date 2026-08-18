using System;
using System.Globalization;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x0200008B RID: 139
	public class RazorError : IEquatable<RazorError>
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00016B05 File Offset: 0x00014D05
		public RazorError(string message, SourceLocation location) : this(message, location, 1)
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00016B10 File Offset: 0x00014D10
		public RazorError(string message, int absoluteIndex, int lineIndex, int columnIndex) : this(message, new SourceLocation(absoluteIndex, lineIndex, columnIndex))
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00016B22 File Offset: 0x00014D22
		public RazorError(string message, SourceLocation location, int length)
		{
			this.Message = message;
			this.Location = location;
			this.Length = length;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00016B3F File Offset: 0x00014D3F
		public RazorError(string message, int absoluteIndex, int lineIndex, int columnIndex, int length) : this(message, new SourceLocation(absoluteIndex, lineIndex, columnIndex), length)
		{
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00016B53 File Offset: 0x00014D53
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00016B5B File Offset: 0x00014D5B
		public string Message { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00016B64 File Offset: 0x00014D64
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00016B6C File Offset: 0x00014D6C
		public SourceLocation Location { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00016B75 File Offset: 0x00014D75
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00016B7D File Offset: 0x00014D7D
		public int Length { get; private set; }

		// Token: 0x060005DB RID: 1499 RVA: 0x00016B88 File Offset: 0x00014D88
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Error @ {0}({2}) - [{1}]", new object[]
			{
				this.Location,
				this.Message,
				this.Length
			});
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00016BD4 File Offset: 0x00014DD4
		public override bool Equals(object obj)
		{
			RazorError razorError = obj as RazorError;
			return razorError != null && this.Equals(razorError);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00016BFC File Offset: 0x00014DFC
		public bool Equals(RazorError other)
		{
			return string.Equals(other.Message, this.Message, StringComparison.Ordinal) && this.Location.Equals(other.Location);
		}
	}
}
