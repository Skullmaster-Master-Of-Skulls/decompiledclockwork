using System;
using System.IO;
using System.Text;
using System.Xml;
using OracleInternal.I18N;

namespace OracleInternal.BinXml
{
	// Token: 0x0200000A RID: 10
	internal sealed class ObxmlTextStream : XmlTextWriter
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003238 File Offset: 0x00001438
		internal ObxmlTextStream(StringBuilder sb) : base(new StringWriter(sb))
		{
			base.Formatting = Formatting.Indented;
			base.Indentation = 2;
			base.IndentChar = ' ';
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000325C File Offset: 0x0000145C
		internal void AppendCData(string cdata)
		{
			this.WriteCData(cdata);
			this.Flush();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000326C File Offset: 0x0000146C
		internal void AppendComment(string comment)
		{
			this.WriteComment(comment);
			this.Flush();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000327C File Offset: 0x0000147C
		internal void AppendData(byte[] data)
		{
			char[] array = new char[data.Length / 2];
			int count = array.Length;
			Conv.GetInstance(2000).ConvertBytesToChars(data, 0, data.Length, array, 0, ref count, true);
			this.WriteChars(array, 0, count);
			this.Flush();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000032C4 File Offset: 0x000014C4
		internal void Clear()
		{
			this.Close();
		}
	}
}
