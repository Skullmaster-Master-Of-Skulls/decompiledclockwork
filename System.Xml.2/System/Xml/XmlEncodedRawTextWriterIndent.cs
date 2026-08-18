using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000CF RID: 207
	internal class XmlEncodedRawTextWriterIndent : XmlEncodedRawTextWriter
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0001F133 File Offset: 0x0001D333
		public XmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001F144 File Offset: 0x0001D344
		public XmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001F158 File Offset: 0x0001D358
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

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001F19A File Offset: 0x0001D39A
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
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

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001F215 File Offset: 0x0001D415
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

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001F249 File Offset: 0x0001D449
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001F254 File Offset: 0x0001D454
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

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
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

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001F313 File Offset: 0x0001D513
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001F32C File Offset: 0x0001D52C
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001F33C File Offset: 0x0001D53C
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001F361 File Offset: 0x0001D561
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001F387 File Offset: 0x0001D587
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001F397 File Offset: 0x0001D597
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001F3A7 File Offset: 0x0001D5A7
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001F3EA File Offset: 0x0001D5EA
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001F40C File Offset: 0x0001D60C
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001F420 File Offset: 0x0001D620
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

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001F554 File Offset: 0x0001D754
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32 <WriteStartElementAsync>d__;
			<WriteStartElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync>d__.<>4__this = this;
			<WriteStartElementAsync>d__.prefix = prefix;
			<WriteStartElementAsync>d__.localName = localName;
			<WriteStartElementAsync>d__.ns = ns;
			<WriteStartElementAsync>d__.<>1__state = -1;
			<WriteStartElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32>(ref <WriteStartElementAsync>d__);
			return <WriteStartElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33 <WriteEndElementAsync>d__;
			<WriteEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndElementAsync>d__.<>4__this = this;
			<WriteEndElementAsync>d__.prefix = prefix;
			<WriteEndElementAsync>d__.localName = localName;
			<WriteEndElementAsync>d__.ns = ns;
			<WriteEndElementAsync>d__.<>1__state = -1;
			<WriteEndElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33>(ref <WriteEndElementAsync>d__);
			return <WriteEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001F60C File Offset: 0x0001D80C
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34 <WriteFullEndElementAsync>d__;
			<WriteFullEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteFullEndElementAsync>d__.<>4__this = this;
			<WriteFullEndElementAsync>d__.prefix = prefix;
			<WriteFullEndElementAsync>d__.localName = localName;
			<WriteFullEndElementAsync>d__.ns = ns;
			<WriteFullEndElementAsync>d__.<>1__state = -1;
			<WriteFullEndElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34>(ref <WriteFullEndElementAsync>d__);
			return <WriteFullEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001F668 File Offset: 0x0001D868
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35 <WriteStartAttributeAsync>d__;
			<WriteStartAttributeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync>d__.<>4__this = this;
			<WriteStartAttributeAsync>d__.prefix = prefix;
			<WriteStartAttributeAsync>d__.localName = localName;
			<WriteStartAttributeAsync>d__.ns = ns;
			<WriteStartAttributeAsync>d__.<>1__state = -1;
			<WriteStartAttributeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35>(ref <WriteStartAttributeAsync>d__);
			return <WriteStartAttributeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001F6C3 File Offset: 0x0001D8C3
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		public override Task WriteCommentAsync(string text)
		{
			XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001F728 File Offset: 0x0001D928
		public override Task WriteProcessingInstructionAsync(string target, string text)
		{
			XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.target = target;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001F77B File Offset: 0x0001D97B
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001F791 File Offset: 0x0001D991
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001F7A7 File Offset: 0x0001D9A7
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001F7BE File Offset: 0x0001D9BE
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001F7EA File Offset: 0x0001D9EA
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001F802 File Offset: 0x0001DA02
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001F81A File Offset: 0x0001DA1A
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001F830 File Offset: 0x0001DA30
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001F848 File Offset: 0x0001DA48
		private Task WriteIndentAsync()
		{
			XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48 <WriteIndentAsync>d__;
			<WriteIndentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIndentAsync>d__.<>4__this = this;
			<WriteIndentAsync>d__.<>1__state = -1;
			<WriteIndentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48>(ref <WriteIndentAsync>d__);
			return <WriteIndentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400031B RID: 795
		protected int indentLevel;

		// Token: 0x0400031C RID: 796
		protected bool newLineOnAttributes;

		// Token: 0x0400031D RID: 797
		protected string indentChars;

		// Token: 0x0400031E RID: 798
		protected bool mixedContent;

		// Token: 0x0400031F RID: 799
		private BitStack mixedContentStack;

		// Token: 0x04000320 RID: 800
		protected ConformanceLevel conformanceLevel;
	}
}
