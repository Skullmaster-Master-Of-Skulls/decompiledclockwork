using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000CB RID: 203
	internal class XmlCharCheckingReader : XmlWrappingReader
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x000197E0 File Offset: 0x000179E0
		internal XmlCharCheckingReader(XmlReader reader, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing) : base(reader)
		{
			this.state = XmlCharCheckingReader.State.Initial;
			this.checkCharacters = checkCharacters;
			this.ignoreWhitespace = ignoreWhitespace;
			this.ignoreComments = ignoreComments;
			this.ignorePis = ignorePis;
			this.dtdProcessing = dtdProcessing;
			this.lastNodeType = XmlNodeType.None;
			if (checkCharacters)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00019838 File Offset: 0x00017A38
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.reader.Settings;
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				else
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (this.checkCharacters)
				{
					xmlReaderSettings.CheckCharacters = true;
				}
				if (this.ignoreWhitespace)
				{
					xmlReaderSettings.IgnoreWhitespace = true;
				}
				if (this.ignoreComments)
				{
					xmlReaderSettings.IgnoreComments = true;
				}
				if (this.ignorePis)
				{
					xmlReaderSettings.IgnoreProcessingInstructions = true;
				}
				if (this.dtdProcessing != (DtdProcessing)(-1))
				{
					xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000198BC File Offset: 0x00017ABC
		public override bool MoveToAttribute(string name)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000198D9 File Offset: 0x00017AD9
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000198F7 File Offset: 0x00017AF7
		public override void MoveToAttribute(int i)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00019914 File Offset: 0x00017B14
		public override bool MoveToFirstAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00019930 File Offset: 0x00017B30
		public override bool MoveToNextAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001994C File Offset: 0x00017B4C
		public override bool MoveToElement()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00019968 File Offset: 0x00017B68
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlCharCheckingReader.State.Initial:
				this.state = XmlCharCheckingReader.State.Interactive;
				if (this.reader.ReadState != ReadState.Initial)
				{
					goto IL_55;
				}
				break;
			case XmlCharCheckingReader.State.InReadBinary:
				this.FinishReadBinary();
				this.state = XmlCharCheckingReader.State.Interactive;
				break;
			case XmlCharCheckingReader.State.Error:
				return false;
			case XmlCharCheckingReader.State.Interactive:
				break;
			default:
				return false;
			}
			if (!this.reader.Read())
			{
				return false;
			}
			IL_55:
			XmlNodeType nodeType = this.reader.NodeType;
			if (!this.checkCharacters)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					if (this.ignorePis)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Comment:
					if (this.ignoreComments)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.DocumentType:
					if (this.dtdProcessing == DtdProcessing.Prohibit)
					{
						this.Throw("Xml_DtdIsProhibitedEx", string.Empty);
					}
					else if (this.dtdProcessing == DtdProcessing.Ignore)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Whitespace:
					if (this.ignoreWhitespace)
					{
						return this.Read();
					}
					break;
				}
				return true;
			}
			switch (nodeType)
			{
			case XmlNodeType.Element:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
					if (this.reader.MoveToFirstAttribute())
					{
						do
						{
							this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
							this.CheckCharacters(this.reader.Value);
						}
						while (this.reader.MoveToNextAttribute());
						this.reader.MoveToElement();
					}
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.EntityReference:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
				}
				break;
			case XmlNodeType.ProcessingInstruction:
				if (this.ignorePis)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.Comment:
				if (this.ignoreComments)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.DocumentType:
				if (this.dtdProcessing == DtdProcessing.Prohibit)
				{
					this.Throw("Xml_DtdIsProhibitedEx", string.Empty);
				}
				else if (this.dtdProcessing == DtdProcessing.Ignore)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
					string attribute = this.reader.GetAttribute("SYSTEM");
					if (attribute != null)
					{
						this.CheckCharacters(attribute);
					}
					attribute = this.reader.GetAttribute("PUBLIC");
					int invCharIndex;
					if (attribute != null && (invCharIndex = this.xmlCharType.IsPublicId(attribute)) >= 0)
					{
						this.Throw("Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(attribute, invCharIndex));
					}
				}
				break;
			case XmlNodeType.Whitespace:
				if (this.ignoreWhitespace)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.SignificantWhitespace:
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.EndElement:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
				}
				break;
			}
			this.lastNodeType = nodeType;
			return true;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00019CFC File Offset: 0x00017EFC
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XmlCharCheckingReader.State.Initial:
					if (this.reader.ReadState != ReadState.Closed)
					{
						return ReadState.Initial;
					}
					return ReadState.Closed;
				case XmlCharCheckingReader.State.Error:
					return ReadState.Error;
				}
				return this.reader.ReadState;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00019D47 File Offset: 0x00017F47
		public override bool ReadAttributeValue()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00019D63 File Offset: 0x00017F63
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00019D68 File Offset: 0x00017F68
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00019E88 File Offset: 0x00018088
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00019F54 File Offset: 0x00018154
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001A020 File Offset: 0x00018220
		private void Throw(string res, string arg)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, arg, null);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001A031 File Offset: 0x00018231
		private void Throw(string res, string[] args)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, args, null);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A044 File Offset: 0x00018244
		private void CheckWhitespace(string value)
		{
			int invCharIndex;
			if ((invCharIndex = this.xmlCharType.IsOnlyWhitespaceWithPos(value)) != -1)
			{
				this.Throw("Xml_InvalidWhitespaceCharacter", XmlException.BuildCharExceptionArgs(value, invCharIndex));
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A074 File Offset: 0x00018274
		private void ValidateQName(string name)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(name, out text, out text2);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001A08C File Offset: 0x0001828C
		private void ValidateQName(string prefix, string localName)
		{
			try
			{
				if (prefix.Length > 0)
				{
					ValidateNames.ParseNCNameThrow(prefix);
				}
				ValidateNames.ParseNCNameThrow(localName);
			}
			catch
			{
				this.state = XmlCharCheckingReader.State.Error;
				throw;
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001A0CC File Offset: 0x000182CC
		private void CheckCharacters(string value)
		{
			XmlConvert.VerifyCharData(value, ExceptionType.ArgumentException, ExceptionType.XmlException);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A0D6 File Offset: 0x000182D6
		private void FinishReadBinary()
		{
			this.state = XmlCharCheckingReader.State.Interactive;
			if (this.readBinaryHelper != null)
			{
				this.readBinaryHelper.Finish();
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A0F4 File Offset: 0x000182F4
		public override Task<bool> ReadAsync()
		{
			XmlCharCheckingReader.<ReadAsync>d__36 <ReadAsync>d__;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadAsync>d__36>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001A138 File Offset: 0x00018338
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadContentAsBase64Async>d__37 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadContentAsBase64Async>d__37>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001A194 File Offset: 0x00018394
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001A24C File Offset: 0x0001844C
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001A2A8 File Offset: 0x000184A8
		private Task FinishReadBinaryAsync()
		{
			XmlCharCheckingReader.<FinishReadBinaryAsync>d__41 <FinishReadBinaryAsync>d__;
			<FinishReadBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadBinaryAsync>d__.<>4__this = this;
			<FinishReadBinaryAsync>d__.<>1__state = -1;
			<FinishReadBinaryAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<FinishReadBinaryAsync>d__41>(ref <FinishReadBinaryAsync>d__);
			return <FinishReadBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040002E8 RID: 744
		private XmlCharCheckingReader.State state;

		// Token: 0x040002E9 RID: 745
		private bool checkCharacters;

		// Token: 0x040002EA RID: 746
		private bool ignoreWhitespace;

		// Token: 0x040002EB RID: 747
		private bool ignoreComments;

		// Token: 0x040002EC RID: 748
		private bool ignorePis;

		// Token: 0x040002ED RID: 749
		private DtdProcessing dtdProcessing;

		// Token: 0x040002EE RID: 750
		private XmlNodeType lastNodeType;

		// Token: 0x040002EF RID: 751
		private XmlCharType xmlCharType;

		// Token: 0x040002F0 RID: 752
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x02000324 RID: 804
		private enum State
		{
			// Token: 0x040014DD RID: 5341
			Initial,
			// Token: 0x040014DE RID: 5342
			InReadBinary,
			// Token: 0x040014DF RID: 5343
			Error,
			// Token: 0x040014E0 RID: 5344
			Interactive
		}
	}
}
