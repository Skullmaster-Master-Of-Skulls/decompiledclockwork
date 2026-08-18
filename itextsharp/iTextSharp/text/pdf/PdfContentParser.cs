using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000280 RID: 640
	public class PdfContentParser
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x0008C6E7 File Offset: 0x0008B6E7
		public PdfContentParser(PRTokeniser tokeniser)
		{
			this.tokeniser = tokeniser;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008C6F8 File Offset: 0x0008B6F8
		public List<PdfObject> Parse(List<PdfObject> ls)
		{
			if (ls == null)
			{
				ls = new List<PdfObject>();
			}
			else
			{
				ls.Clear();
			}
			PdfObject pdfObject;
			while ((pdfObject = this.ReadPRObject()) != null)
			{
				ls.Add(pdfObject);
				if (pdfObject.Type == 200)
				{
					break;
				}
			}
			return ls;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008C73A File Offset: 0x0008B73A
		public PRTokeniser GetTokeniser()
		{
			return this.tokeniser;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0008C74B File Offset: 0x0008B74B
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x0008C742 File Offset: 0x0008B742
		public PRTokeniser Tokeniser
		{
			get
			{
				return this.tokeniser;
			}
			set
			{
				this.tokeniser = value;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008C754 File Offset: 0x0008B754
		public PdfDictionary ReadDictionary()
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			while (this.NextValidToken())
			{
				if (this.tokeniser.TokenType == PRTokeniser.TokType.END_DIC)
				{
					return pdfDictionary;
				}
				if (this.tokeniser.TokenType != PRTokeniser.TokType.NAME)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("dictionary.key.is.not.a.name"));
				}
				PdfName key = new PdfName(this.tokeniser.StringValue, false);
				PdfObject pdfObject = this.ReadPRObject();
				int type = pdfObject.Type;
				if (-type == 8)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("unexpected.gt.gt"));
				}
				if (-type == 6)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("unexpected.close.bracket"));
				}
				pdfDictionary.Put(key, pdfObject);
			}
			throw new IOException(MessageLocalization.GetComposedMessage("unexpected.end.of.file"));
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008C804 File Offset: 0x0008B804
		public PdfArray ReadArray()
		{
			PdfArray pdfArray = new PdfArray();
			for (;;)
			{
				PdfObject pdfObject = this.ReadPRObject();
				int type = pdfObject.Type;
				if (-type == 6)
				{
					return pdfArray;
				}
				if (-type == 8)
				{
					break;
				}
				pdfArray.Add(pdfObject);
			}
			throw new IOException(MessageLocalization.GetComposedMessage("unexpected.gt.gt"));
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008C84C File Offset: 0x0008B84C
		public PdfObject ReadPRObject()
		{
			if (!this.NextValidToken())
			{
				return null;
			}
			PRTokeniser.TokType tokenType = this.tokeniser.TokenType;
			switch (tokenType)
			{
			case PRTokeniser.TokType.NUMBER:
				return new PdfNumber(this.tokeniser.StringValue);
			case PRTokeniser.TokType.STRING:
				return new PdfString(this.tokeniser.StringValue, null).SetHexWriting(this.tokeniser.IsHexString());
			case PRTokeniser.TokType.NAME:
				return new PdfName(this.tokeniser.StringValue, false);
			case PRTokeniser.TokType.START_ARRAY:
				return this.ReadArray();
			case PRTokeniser.TokType.START_DIC:
				return this.ReadDictionary();
			case PRTokeniser.TokType.OTHER:
				return new PdfLiteral(200, this.tokeniser.StringValue);
			}
			return new PdfLiteral((int)(-(int)tokenType), this.tokeniser.StringValue);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008C922 File Offset: 0x0008B922
		public bool NextValidToken()
		{
			while (this.tokeniser.NextToken())
			{
				if (this.tokeniser.TokenType != PRTokeniser.TokType.COMMENT)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001055 RID: 4181
		public const int COMMAND_TYPE = 200;

		// Token: 0x04001056 RID: 4182
		private PRTokeniser tokeniser;
	}
}
