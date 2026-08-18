using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000DE RID: 222
	internal class XmlUtf8RawTextWriterIndent : XmlUtf8RawTextWriter
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x0003A257 File Offset: 0x00038457
		public XmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003A268 File Offset: 0x00038468
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = base.Settings;
				settings.ReadOnly = false;
				settings.Indent = true;
				settings.IndentChars = this.indentChars;
				settings.NewLineOnAttributes = this.newLineOnAttributes;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003A2AA File Offset: 0x000384AA
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003A2D4 File Offset: 0x000384D4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003A325 File Offset: 0x00038525
		internal override void StartElementContent()
		{
			if (this.indentLevel == 1 && this.conformanceLevel == ConformanceLevel.Document)
			{
				this.mixedContent = false;
			}
			else
			{
				this.mixedContent = this.mixedContentStack.PeekBit();
			}
			base.StartElementContent();
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003A359 File Offset: 0x00038559
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003A364 File Offset: 0x00038564
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003A3C4 File Offset: 0x000385C4
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003A423 File Offset: 0x00038623
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003A43C File Offset: 0x0003863C
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003A44C File Offset: 0x0003864C
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003A471 File Offset: 0x00038671
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003A497 File Offset: 0x00038697
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003A4A7 File Offset: 0x000386A7
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003A4B7 File Offset: 0x000386B7
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003A4C8 File Offset: 0x000386C8
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003A4D8 File Offset: 0x000386D8
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003A4E8 File Offset: 0x000386E8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003A4FA File Offset: 0x000386FA
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003A50C File Offset: 0x0003870C
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003A51C File Offset: 0x0003871C
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003A530 File Offset: 0x00038730
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
			this.mixedContentStack = new BitStack();
			if (this.checkCharacters)
			{
				if (this.newLineOnAttributes)
				{
					base.ValidateContentChars(this.indentChars, "IndentChars", true);
					base.ValidateContentChars(this.newLineChars, "NewLineChars", true);
					return;
				}
				base.ValidateContentChars(this.indentChars, "IndentChars", false);
				if (this.newLineHandling != NewLineHandling.Replace)
				{
					base.ValidateContentChars(this.newLineChars, "NewLineChars", false);
				}
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0003A5C8 File Offset: 0x000387C8
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003A600 File Offset: 0x00038800
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003A664 File Offset: 0x00038864
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31 <WriteStartElementAsync>d__;
			<WriteStartElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync>d__.<>4__this = this;
			<WriteStartElementAsync>d__.prefix = prefix;
			<WriteStartElementAsync>d__.localName = localName;
			<WriteStartElementAsync>d__.ns = ns;
			<WriteStartElementAsync>d__.<>1__state = -1;
			<WriteStartElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31>(ref <WriteStartElementAsync>d__);
			return <WriteStartElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003A6C0 File Offset: 0x000388C0
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32 <WriteEndElementAsync>d__;
			<WriteEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndElementAsync>d__.<>4__this = this;
			<WriteEndElementAsync>d__.prefix = prefix;
			<WriteEndElementAsync>d__.localName = localName;
			<WriteEndElementAsync>d__.ns = ns;
			<WriteEndElementAsync>d__.<>1__state = -1;
			<WriteEndElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32>(ref <WriteEndElementAsync>d__);
			return <WriteEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003A71C File Offset: 0x0003891C
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33 <WriteFullEndElementAsync>d__;
			<WriteFullEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteFullEndElementAsync>d__.<>4__this = this;
			<WriteFullEndElementAsync>d__.prefix = prefix;
			<WriteFullEndElementAsync>d__.localName = localName;
			<WriteFullEndElementAsync>d__.ns = ns;
			<WriteFullEndElementAsync>d__.<>1__state = -1;
			<WriteFullEndElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33>(ref <WriteFullEndElementAsync>d__);
			return <WriteFullEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003A778 File Offset: 0x00038978
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34 <WriteStartAttributeAsync>d__;
			<WriteStartAttributeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync>d__.<>4__this = this;
			<WriteStartAttributeAsync>d__.prefix = prefix;
			<WriteStartAttributeAsync>d__.localName = localName;
			<WriteStartAttributeAsync>d__.ns = ns;
			<WriteStartAttributeAsync>d__.<>1__state = -1;
			<WriteStartAttributeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34>(ref <WriteStartAttributeAsync>d__);
			return <WriteStartAttributeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003A7D3 File Offset: 0x000389D3
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003A7EC File Offset: 0x000389EC
		public override Task WriteCommentAsync(string text)
		{
			XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003A838 File Offset: 0x00038A38
		public override Task WriteProcessingInstructionAsync(string target, string text)
		{
			XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.target = target;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003A88B File Offset: 0x00038A8B
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003A8A1 File Offset: 0x00038AA1
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003A8B7 File Offset: 0x00038AB7
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003A8CE File Offset: 0x00038ACE
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003A8E4 File Offset: 0x00038AE4
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003A8FA File Offset: 0x00038AFA
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003A912 File Offset: 0x00038B12
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003A92A File Offset: 0x00038B2A
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003A940 File Offset: 0x00038B40
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003A958 File Offset: 0x00038B58
		private Task WriteIndentAsync()
		{
			XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47 <WriteIndentAsync>d__;
			<WriteIndentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIndentAsync>d__.<>4__this = this;
			<WriteIndentAsync>d__.<>1__state = -1;
			<WriteIndentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47>(ref <WriteIndentAsync>d__);
			return <WriteIndentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000407 RID: 1031
		protected int indentLevel;

		// Token: 0x04000408 RID: 1032
		protected bool newLineOnAttributes;

		// Token: 0x04000409 RID: 1033
		protected string indentChars;

		// Token: 0x0400040A RID: 1034
		protected bool mixedContent;

		// Token: 0x0400040B RID: 1035
		private BitStack mixedContentStack;

		// Token: 0x0400040C RID: 1036
		protected ConformanceLevel conformanceLevel;
	}
}
