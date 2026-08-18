using System;

namespace System.Xml
{
	// Token: 0x02000070 RID: 112
	internal class XmlCharCheckingReader : XmlWrappingReader
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x000147B4 File Offset: 0x000137B4
		internal XmlCharCheckingReader(XmlReader reader, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, bool prohibitDtd) : base(reader)
		{
			this.state = XmlCharCheckingReader.State.Initial;
			this.checkCharacters = checkCharacters;
			this.ignoreWhitespace = ignoreWhitespace;
			this.ignoreComments = ignoreComments;
			this.ignorePis = ignorePis;
			this.prohibitDtd = prohibitDtd;
			this.lastNodeType = XmlNodeType.None;
			if (checkCharacters)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001480C File Offset: 0x0001380C
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
				if (this.prohibitDtd)
				{
					xmlReaderSettings.ProhibitDtd = true;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001488A File Offset: 0x0001388A
		public override bool MoveToAttribute(string name)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000148A7 File Offset: 0x000138A7
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000148C5 File Offset: 0x000138C5
		public override void MoveToAttribute(int i)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000148E2 File Offset: 0x000138E2
		public override bool MoveToFirstAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000148FE File Offset: 0x000138FE
		public override bool MoveToNextAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001491A File Offset: 0x0001391A
		public override bool MoveToElement()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00014938 File Offset: 0x00013938
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
					if (this.prohibitDtd)
					{
						this.Throw("Xml_DtdIsProhibited", string.Empty);
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
				if (this.prohibitDtd)
				{
					this.Throw("Xml_DtdIsProhibited", string.Empty);
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
					int index;
					if (attribute != null && (index = this.xmlCharType.IsPublicId(attribute)) >= 0)
					{
						this.Throw("Xml_InvalidCharacter", XmlException.BuildCharExceptionStr(attribute[index]));
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00014CB8 File Offset: 0x00013CB8
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XmlCharCheckingReader.State.Initial:
					return ReadState.Initial;
				case XmlCharCheckingReader.State.Error:
					return ReadState.Error;
				}
				return this.reader.ReadState;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00014CF3 File Offset: 0x00013CF3
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00014D00 File Offset: 0x00013D00
		public override bool ReadAttributeValue()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00014D1C File Offset: 0x00013D1C
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00014D20 File Offset: 0x00013D20
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

		// Token: 0x060004CE RID: 1230 RVA: 0x00014DB0 File Offset: 0x00013DB0
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

		// Token: 0x060004CF RID: 1231 RVA: 0x00014E40 File Offset: 0x00013E40
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
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

		// Token: 0x060004D0 RID: 1232 RVA: 0x00014ED0 File Offset: 0x00013ED0
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
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

		// Token: 0x060004D1 RID: 1233 RVA: 0x00014F5D File Offset: 0x00013F5D
		private void Throw(string res, string arg)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, arg, null);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00014F6E File Offset: 0x00013F6E
		private void Throw(string res, string[] args)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, args, null);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014F80 File Offset: 0x00013F80
		private void CheckWhitespace(string value)
		{
			int index;
			if ((index = this.xmlCharType.IsOnlyWhitespaceWithPos(value)) != -1)
			{
				this.Throw("Xml_InvalidWhitespaceCharacter", XmlException.BuildCharExceptionStr(this.reader.Value[index]));
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00014FC0 File Offset: 0x00013FC0
		private void ValidateQName(string name)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(name, out text, out text2);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00014FD8 File Offset: 0x00013FD8
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015018 File Offset: 0x00014018
		private void CheckCharacters(string value)
		{
			XmlConvert.VerifyCharData(value, ExceptionType.XmlException);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00015021 File Offset: 0x00014021
		private void FinishReadBinary()
		{
			this.state = XmlCharCheckingReader.State.Interactive;
			if (this.readBinaryHelper != null)
			{
				this.readBinaryHelper.Finish();
			}
		}

		// Token: 0x040005E8 RID: 1512
		private XmlCharCheckingReader.State state;

		// Token: 0x040005E9 RID: 1513
		private bool checkCharacters;

		// Token: 0x040005EA RID: 1514
		private bool ignoreWhitespace;

		// Token: 0x040005EB RID: 1515
		private bool ignoreComments;

		// Token: 0x040005EC RID: 1516
		private bool ignorePis;

		// Token: 0x040005ED RID: 1517
		private bool prohibitDtd;

		// Token: 0x040005EE RID: 1518
		private XmlNodeType lastNodeType;

		// Token: 0x040005EF RID: 1519
		private XmlCharType xmlCharType;

		// Token: 0x040005F0 RID: 1520
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x02000071 RID: 113
		private enum State
		{
			// Token: 0x040005F2 RID: 1522
			Initial,
			// Token: 0x040005F3 RID: 1523
			InReadBinary,
			// Token: 0x040005F4 RID: 1524
			Error,
			// Token: 0x040005F5 RID: 1525
			Interactive
		}
	}
}
