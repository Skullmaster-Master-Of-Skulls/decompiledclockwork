using System;
using System.Text;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000682 RID: 1666
	internal class Indentation
	{
		// Token: 0x06003D78 RID: 15736 RVA: 0x000FC3DF File Offset: 0x000FA5DF
		internal Indentation(IndentedTextWriter writer, int indent)
		{
			this.writer = writer;
			this.indent = indent;
			this.s = null;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000FC3FC File Offset: 0x000FA5FC
		internal string IndentationString
		{
			get
			{
				if (this.s == null)
				{
					string tabString = this.writer.TabString;
					StringBuilder stringBuilder = new StringBuilder(this.indent * tabString.Length);
					for (int i = 0; i < this.indent; i++)
					{
						stringBuilder.Append(tabString);
					}
					this.s = stringBuilder.ToString();
				}
				return this.s;
			}
		}

		// Token: 0x04002CC2 RID: 11458
		private IndentedTextWriter writer;

		// Token: 0x04002CC3 RID: 11459
		private int indent;

		// Token: 0x04002CC4 RID: 11460
		private string s;
	}
}
