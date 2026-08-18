using System;
using System.Globalization;
using System.IO;

namespace System.Web.WebPages
{
	// Token: 0x02000084 RID: 132
	public class HelperResult : IHtmlString
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000CE94 File Offset: 0x0000B094
		public HelperResult(Action<TextWriter> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this._action = action;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000CEB1 File Offset: 0x0000B0B1
		public string ToHtmlString()
		{
			return this.ToString();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this._action(stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CF04 File Offset: 0x0000B104
		public void WriteTo(TextWriter writer)
		{
			this._action(writer);
		}

		// Token: 0x04000126 RID: 294
		private readonly Action<TextWriter> _action;
	}
}
