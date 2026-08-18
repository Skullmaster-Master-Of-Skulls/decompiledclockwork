using System;
using System.IO;

namespace System.Web.Razor.Text
{
	// Token: 0x0200006B RID: 107
	public class TextDocumentReader : TextReader, ITextDocument, ITextBuffer
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00012935 File Offset: 0x00010B35
		public TextDocumentReader(ITextDocument source)
		{
			this.Document = source;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00012944 File Offset: 0x00010B44
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0001294C File Offset: 0x00010B4C
		internal ITextDocument Document { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00012955 File Offset: 0x00010B55
		public SourceLocation Location
		{
			get
			{
				return this.Document.Location;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00012962 File Offset: 0x00010B62
		public int Length
		{
			get
			{
				return this.Document.Length;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001296F File Offset: 0x00010B6F
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001297C File Offset: 0x00010B7C
		public int Position
		{
			get
			{
				return this.Document.Position;
			}
			set
			{
				this.Document.Position = value;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001298A File Offset: 0x00010B8A
		public override int Read()
		{
			return this.Document.Read();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00012997 File Offset: 0x00010B97
		public override int Peek()
		{
			return this.Document.Peek();
		}
	}
}
