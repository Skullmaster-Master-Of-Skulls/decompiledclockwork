using System;
using System.IO;

namespace System.Xml.Serialization
{
	// Token: 0x02000143 RID: 323
	internal class IndentedWriter
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x00067020 File Offset: 0x00065220
		internal IndentedWriter(TextWriter writer, bool compact)
		{
			this.writer = writer;
			this.compact = compact;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00067036 File Offset: 0x00065236
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x0006703E File Offset: 0x0006523E
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

		// Token: 0x06001716 RID: 5910 RVA: 0x00067047 File Offset: 0x00065247
		internal void Write(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(s);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00067063 File Offset: 0x00065263
		internal void Write(char c)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(c);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0006707F File Offset: 0x0006527F
		internal void WriteLine(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.WriteLine(s);
			this.needIndent = true;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000670A2 File Offset: 0x000652A2
		internal void WriteLine()
		{
			this.writer.WriteLine();
			this.needIndent = true;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000670B8 File Offset: 0x000652B8
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

		// Token: 0x04000ABE RID: 2750
		private TextWriter writer;

		// Token: 0x04000ABF RID: 2751
		private bool needIndent;

		// Token: 0x04000AC0 RID: 2752
		private int indentLevel;

		// Token: 0x04000AC1 RID: 2753
		private bool compact;
	}
}
