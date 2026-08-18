using System;
using System.IO;

namespace System.Xml.Serialization
{
	// Token: 0x020002BC RID: 700
	internal class IndentedWriter
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x0009ED5C File Offset: 0x0009DD5C
		internal IndentedWriter(TextWriter writer, bool compact)
		{
			this.writer = writer;
			this.compact = compact;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0009ED72 File Offset: 0x0009DD72
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0009ED7A File Offset: 0x0009DD7A
		internal int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009ED83 File Offset: 0x0009DD83
		internal void Write(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(s);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0009ED9F File Offset: 0x0009DD9F
		internal void Write(char c)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(c);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0009EDBB File Offset: 0x0009DDBB
		internal void WriteLine(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.WriteLine(s);
			this.needIndent = true;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0009EDDE File Offset: 0x0009DDDE
		internal void WriteLine()
		{
			this.writer.WriteLine();
			this.needIndent = true;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0009EDF4 File Offset: 0x0009DDF4
		internal void WriteIndent()
		{
			this.needIndent = false;
			if (!this.compact)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write("    ");
				}
			}
		}

		// Token: 0x04001459 RID: 5209
		private TextWriter writer;

		// Token: 0x0400145A RID: 5210
		private bool needIndent;

		// Token: 0x0400145B RID: 5211
		private int indentLevel;

		// Token: 0x0400145C RID: 5212
		private bool compact;
	}
}
