using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	internal sealed class ParseException : Exception
	{
		// Token: 0x06001E4C RID: 7756 RVA: 0x0005EAEC File Offset: 0x0005CCEC
		public ParseException(string message, int position) : base(message)
		{
			this.position = position;
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0005EB04 File Offset: 0x0005CD04
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public override string ToString()
		{
			return string.Format("{0} (at index {1})", this.Message, this.position);
		}

		// Token: 0x04000786 RID: 1926
		private int position;
	}
}
