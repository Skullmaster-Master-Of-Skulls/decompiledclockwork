using System;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.Query.Dynamic
{
	// Token: 0x0200003E RID: 62
	public class ParseException : Exception
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		public ParseException(string message, int position) : base(message)
		{
			this.position = position;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, AtlasWeb.ParseException_ParseExceptionFormat, new object[]
			{
				this.Message,
				this.position
			});
		}

		// Token: 0x040000E6 RID: 230
		private int position;
	}
}
