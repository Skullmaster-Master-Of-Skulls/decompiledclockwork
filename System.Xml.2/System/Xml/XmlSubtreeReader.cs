using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000D6 RID: 214
	internal sealed class XmlSubtreeReader : XmlWrappingReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00023070 File Offset: 0x00021270
		internal XmlSubtreeReader(XmlReader reader) : base(reader)
		{
			this.initialDepth = reader.Depth;
			this.state = XmlSubtreeReader.State.Initial;
			this.nsManager = new XmlNamespaceManager(reader.NameTable);
			this.xmlns = reader.NameTable.Add("xmlns");
			this.xmlnsUri = reader.NameTable.Add("http://www.w3.org/2000/xmlns/");
			this.tmpNode = new XmlSubtreeReader.NodeData();
			this.tmpNode.Set(XmlNodeType.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
			this.SetCurrentNode(this.tmpNode);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002311E File Offset: 0x0002131E
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NodeType;
				}
				return this.curNode.type;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002313F File Offset: 0x0002133F
		public override string Name
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Name;
				}
				return this.curNode.name;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00023160 File Offset: 0x00021360
		public override string LocalName
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.LocalName;
				}
				return this.curNode.localName;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00023181 File Offset: 0x00021381
		public override string NamespaceURI
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.NamespaceURI;
				}
				return this.curNode.namespaceUri;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000231A2 File Offset: 0x000213A2
		public override string Prefix
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Prefix;
				}
				return this.curNode.prefix;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000231C3 File Offset: 0x000213C3
		public override string Value
		{
			get
			{
				if (!this.useCurNode)
				{
					return this.reader.Value;
				}
				return this.curNode.value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000231E4 File Offset: 0x000213E4
		public override int Depth
		{
			get
			{
				int num = this.reader.Depth - this.initialDepth;
				if (this.curNsAttr != -1)
				{
					if (this.curNode.type == XmlNodeType.Text)
					{
						num += 2;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00023226 File Offset: 0x00021426
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00023233 File Offset: 0x00021433
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00023240 File Offset: 0x00021440
		public override bool EOF
		{
			get
			{
				return this.state == XmlSubtreeReader.State.EndOfFile || this.state == XmlSubtreeReader.State.Closed;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00023256 File Offset: 0x00021456
		public override ReadState ReadState
		{
			get
			{
				if (this.reader.ReadState == ReadState.Error)
				{
					return ReadState.Error;
				}
				if (this.state <= XmlSubtreeReader.State.Closed)
				{
					return (ReadState)this.state;
				}
				return ReadState.Interactive;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00023279 File Offset: 0x00021479
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00023286 File Offset: 0x00021486
		public override int AttributeCount
		{
			get
			{
				if (!this.InAttributeActiveState)
				{
					return 0;
				}
				return this.reader.AttributeCount + this.nsAttrCount;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000232A4 File Offset: 0x000214A4
		public override string GetAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00023304 File Offset: 0x00021504
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.InAttributeActiveState)
			{
				return null;
			}
			string attribute = this.reader.GetAttribute(name, namespaceURI);
			if (attribute != null)
			{
				return attribute;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && namespaceURI == this.xmlnsUri)
				{
					return this.nsAttributes[i].value;
				}
			}
			return null;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00023374 File Offset: 0x00021574
		public override string GetAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				return this.reader.GetAttribute(i);
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				return this.nsAttributes[i - attributeCount].value;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000233D8 File Offset: 0x000215D8
		public override bool MoveToAttribute(string name)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].name)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00023440 File Offset: 0x00021640
		public override bool MoveToAttribute(string name, string ns)
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToAttribute(name, ns))
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				return true;
			}
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (name == this.nsAttributes[i].localName && ns == this.xmlnsUri)
				{
					this.MoveToNsAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000234B4 File Offset: 0x000216B4
		public override void MoveToAttribute(int i)
		{
			if (!this.InAttributeActiveState)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			int attributeCount = this.reader.AttributeCount;
			if (i < attributeCount)
			{
				this.reader.MoveToAttribute(i);
				this.curNsAttr = -1;
				this.useCurNode = false;
				return;
			}
			if (i - attributeCount < this.nsAttrCount)
			{
				this.MoveToNsAttribute(i - attributeCount);
				return;
			}
			throw new ArgumentOutOfRangeException("i");
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002351E File Offset: 0x0002171E
		public override bool MoveToFirstAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.reader.MoveToFirstAttribute())
			{
				this.useCurNode = false;
				return true;
			}
			if (this.nsAttrCount > 0)
			{
				this.MoveToNsAttribute(0);
				return true;
			}
			return false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00023554 File Offset: 0x00021754
		public override bool MoveToNextAttribute()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1 && this.reader.MoveToNextAttribute())
			{
				return true;
			}
			if (this.curNsAttr + 1 < this.nsAttrCount)
			{
				this.MoveToNsAttribute(this.curNsAttr + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000235A4 File Offset: 0x000217A4
		public override bool MoveToElement()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			this.useCurNode = false;
			if (this.curNsAttr >= 0)
			{
				this.curNsAttr = -1;
				return true;
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000235D4 File Offset: 0x000217D4
		public override bool ReadAttributeValue()
		{
			if (!this.InAttributeActiveState)
			{
				return false;
			}
			if (this.curNsAttr == -1)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.curNode.type == XmlNodeType.Text)
			{
				return false;
			}
			this.tmpNode.type = XmlNodeType.Text;
			this.tmpNode.value = this.curNode.value;
			this.SetCurrentNode(this.tmpNode);
			return true;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00023640 File Offset: 0x00021840
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
				return true;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return false;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_E5;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_E5;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				return this.FinishReadElementContentAsBinary() && this.Read();
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				return this.FinishReadContentAsBinary() && this.Read();
			default:
				return false;
			}
			IL_54:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth && (this.reader.NodeType == XmlNodeType.EndElement || (this.reader.NodeType == XmlNodeType.Element && this.reader.IsEmptyElement)))
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			if (this.reader.Read())
			{
				this.ProcessNamespaces();
				return true;
			}
			this.SetEmptyNode();
			return false;
			IL_E5:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_54;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00023768 File Offset: 0x00021968
		public override void Close()
		{
			if (this.state == XmlSubtreeReader.State.Closed)
			{
				return;
			}
			try
			{
				if (this.state != XmlSubtreeReader.State.EndOfFile)
				{
					this.reader.MoveToElement();
					if (this.reader.Depth == this.initialDepth && this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
					{
						this.reader.Read();
					}
					while (this.reader.Depth > this.initialDepth && this.reader.Read())
					{
					}
				}
			}
			catch
			{
			}
			finally
			{
				this.curNsAttr = -1;
				this.useCurNode = false;
				this.state = XmlSubtreeReader.State.Closed;
				this.SetEmptyNode();
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002382C File Offset: 0x00021A2C
		public override void Skip()
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
				this.Read();
				return;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.Error:
				return;
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
				this.nsManager.PopScope();
				goto IL_11A;
			case XmlSubtreeReader.State.ClearNsAttributes:
				goto IL_11A;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				if (this.FinishReadElementContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				if (this.FinishReadContentAsBinary())
				{
					this.Skip();
					return;
				}
				return;
			default:
				return;
			}
			IL_42:
			this.curNsAttr = -1;
			this.useCurNode = false;
			this.reader.MoveToElement();
			if (this.reader.Depth == this.initialDepth)
			{
				if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement && this.reader.Read())
				{
					while (this.reader.NodeType != XmlNodeType.EndElement && this.reader.Depth > this.initialDepth)
					{
						this.reader.Skip();
					}
				}
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return;
			}
			if (this.reader.NodeType == XmlNodeType.Element && !this.reader.IsEmptyElement)
			{
				this.nsManager.PopScope();
			}
			this.reader.Skip();
			this.ProcessNamespaces();
			return;
			IL_11A:
			this.nsAttrCount = 0;
			this.state = XmlSubtreeReader.State.Interactive;
			goto IL_42;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00023988 File Offset: 0x00021B88
		public override object ReadContentAsObject()
		{
			object result;
			try
			{
				this.InitReadContentAsType("ReadContentAsObject");
				object obj = this.reader.ReadContentAsObject();
				this.FinishReadContentAsType();
				result = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000239D4 File Offset: 0x00021BD4
		public override bool ReadContentAsBoolean()
		{
			bool result;
			try
			{
				this.InitReadContentAsType("ReadContentAsBoolean");
				bool flag = this.reader.ReadContentAsBoolean();
				this.FinishReadContentAsType();
				result = flag;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00023A20 File Offset: 0x00021C20
		public override DateTime ReadContentAsDateTime()
		{
			DateTime result;
			try
			{
				this.InitReadContentAsType("ReadContentAsDateTime");
				DateTime dateTime = this.reader.ReadContentAsDateTime();
				this.FinishReadContentAsType();
				result = dateTime;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00023A6C File Offset: 0x00021C6C
		public override double ReadContentAsDouble()
		{
			double result;
			try
			{
				this.InitReadContentAsType("ReadContentAsDouble");
				double num = this.reader.ReadContentAsDouble();
				this.FinishReadContentAsType();
				result = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00023AB8 File Offset: 0x00021CB8
		public override float ReadContentAsFloat()
		{
			float result;
			try
			{
				this.InitReadContentAsType("ReadContentAsFloat");
				float num = this.reader.ReadContentAsFloat();
				this.FinishReadContentAsType();
				result = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00023B04 File Offset: 0x00021D04
		public override decimal ReadContentAsDecimal()
		{
			decimal result;
			try
			{
				this.InitReadContentAsType("ReadContentAsDecimal");
				decimal num = this.reader.ReadContentAsDecimal();
				this.FinishReadContentAsType();
				result = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00023B50 File Offset: 0x00021D50
		public override int ReadContentAsInt()
		{
			int result;
			try
			{
				this.InitReadContentAsType("ReadContentAsInt");
				int num = this.reader.ReadContentAsInt();
				this.FinishReadContentAsType();
				result = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00023B9C File Offset: 0x00021D9C
		public override long ReadContentAsLong()
		{
			long result;
			try
			{
				this.InitReadContentAsType("ReadContentAsLong");
				long num = this.reader.ReadContentAsLong();
				this.FinishReadContentAsType();
				result = num;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00023BE8 File Offset: 0x00021DE8
		public override string ReadContentAsString()
		{
			string result;
			try
			{
				this.InitReadContentAsType("ReadContentAsString");
				string text = this.reader.ReadContentAsString();
				this.FinishReadContentAsType();
				result = text;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00023C34 File Offset: 0x00021E34
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			object result;
			try
			{
				this.InitReadContentAsType("ReadContentAs");
				object obj = this.reader.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadContentAsType();
				result = obj;
			}
			catch
			{
				this.state = XmlSubtreeReader.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00023C80 File Offset: 0x00021E80
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.reader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00023C90 File Offset: 0x00021E90
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBase64;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is Base64Decoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new Base64Decoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBase64(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			case XmlSubtreeReader.State.ReadContentAsBase64:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00023E30 File Offset: 0x00022030
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBase64))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBase64(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00023F24 File Offset: 0x00022124
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				this.state = XmlSubtreeReader.State.ReadContentAsBinHex;
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				case XmlNodeType.Attribute:
					if (this.curNsAttr != -1 && this.reader.CanReadBinaryContent)
					{
						this.CheckBuffer(buffer, index, count);
						if (count == 0)
						{
							return 0;
						}
						if (this.nsIncReadOffset == 0)
						{
							if (this.binDecoder != null && this.binDecoder is BinHexDecoder)
							{
								this.binDecoder.Reset();
							}
							else
							{
								this.binDecoder = new BinHexDecoder();
							}
						}
						if (this.nsIncReadOffset == this.curNode.value.Length)
						{
							return 0;
						}
						this.binDecoder.SetNextOutputBuffer(buffer, index, count);
						this.nsIncReadOffset += this.binDecoder.Decode(this.curNode.value, this.nsIncReadOffset, this.curNode.value.Length - this.nsIncReadOffset);
						return this.binDecoder.DecodedCount;
					}
					break;
				case XmlNodeType.Text:
					break;
				default:
					if (nodeType != XmlNodeType.EndElement)
					{
						return 0;
					}
					return 0;
				}
				return this.reader.ReadContentAsBinHex(buffer, index, count);
			}
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num == 0)
			{
				this.state = XmlSubtreeReader.State.Interactive;
				this.ProcessNamespaces();
			}
			return num;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000240C4 File Offset: 0x000222C4
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (!this.InitReadElementContentAsBinary(XmlSubtreeReader.State.ReadElementContentAsBinHex))
				{
					return 0;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingBinaryContentMethods"));
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
				break;
			default:
				return 0;
			}
			int num = this.reader.ReadContentAsBinHex(buffer, index, count);
			if (num > 0 || count == 0)
			{
				return num;
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
			}
			else
			{
				this.Read();
			}
			return 0;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000241B6 File Offset: 0x000223B6
		public override bool CanReadValueChunk
		{
			get
			{
				return this.reader.CanReadValueChunk;
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000241C4 File Offset: 0x000223C4
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return 0;
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (this.curNsAttr != -1 && this.reader.CanReadValueChunk)
				{
					this.CheckBuffer(buffer, index, count);
					int num = this.curNode.value.Length - this.nsIncReadOffset;
					if (num > count)
					{
						num = count;
					}
					if (num > 0)
					{
						this.curNode.value.CopyTo(this.nsIncReadOffset, buffer, index, num);
					}
					this.nsIncReadOffset += num;
					return num;
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingReadValueChunkWithBinary"));
			default:
				return 0;
			}
			return this.reader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002429B File Offset: 0x0002249B
		public override string LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this).LookupNamespace(prefix);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000242A4 File Offset: 0x000224A4
		protected override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000242AC File Offset: 0x000224AC
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LineNumber;
					}
				}
				return 0;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000242D8 File Offset: 0x000224D8
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (!this.useCurNode)
				{
					IXmlLineInfo xmlLineInfo = this.reader as IXmlLineInfo;
					if (xmlLineInfo != null)
					{
						return xmlLineInfo.LinePosition;
					}
				}
				return 0;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00024304 File Offset: 0x00022504
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.reader is IXmlLineInfo;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00024314 File Offset: 0x00022514
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (!this.InNamespaceActiveState)
			{
				return new Dictionary<string, string>();
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00024330 File Offset: 0x00022530
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00024348 File Offset: 0x00022548
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (!this.InNamespaceActiveState)
			{
				return null;
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00024360 File Offset: 0x00022560
		private void ProcessNamespaces()
		{
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.EndElement)
				{
					return;
				}
				this.state = XmlSubtreeReader.State.PopNamespaceScope;
			}
			else
			{
				this.nsManager.PushScope();
				string text = this.reader.Prefix;
				string namespaceURI = this.reader.NamespaceURI;
				if (this.nsManager.LookupNamespace(text) != namespaceURI)
				{
					this.AddNamespace(text, namespaceURI);
				}
				if (this.reader.MoveToFirstAttribute())
				{
					do
					{
						text = this.reader.Prefix;
						namespaceURI = this.reader.NamespaceURI;
						if (Ref.Equal(namespaceURI, this.xmlnsUri))
						{
							if (text.Length == 0)
							{
								this.nsManager.AddNamespace(string.Empty, this.reader.Value);
								this.RemoveNamespace(string.Empty, this.xmlns);
							}
							else
							{
								text = this.reader.LocalName;
								this.nsManager.AddNamespace(text, this.reader.Value);
								this.RemoveNamespace(this.xmlns, text);
							}
						}
						else if (text.Length != 0 && this.nsManager.LookupNamespace(text) != namespaceURI)
						{
							this.AddNamespace(text, namespaceURI);
						}
					}
					while (this.reader.MoveToNextAttribute());
					this.reader.MoveToElement();
				}
				if (this.reader.IsEmptyElement)
				{
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
					return;
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000244C0 File Offset: 0x000226C0
		private void AddNamespace(string prefix, string ns)
		{
			this.nsManager.AddNamespace(prefix, ns);
			int num = this.nsAttrCount;
			this.nsAttrCount = num + 1;
			int num2 = num;
			if (this.nsAttributes == null)
			{
				this.nsAttributes = new XmlSubtreeReader.NodeData[this.InitialNamespaceAttributeCount];
			}
			if (num2 == this.nsAttributes.Length)
			{
				XmlSubtreeReader.NodeData[] destinationArray = new XmlSubtreeReader.NodeData[this.nsAttributes.Length * 2];
				Array.Copy(this.nsAttributes, 0, destinationArray, 0, num2);
				this.nsAttributes = destinationArray;
			}
			if (this.nsAttributes[num2] == null)
			{
				this.nsAttributes[num2] = new XmlSubtreeReader.NodeData();
			}
			if (prefix.Length == 0)
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, this.xmlns, string.Empty, this.xmlns, this.xmlnsUri, ns);
			}
			else
			{
				this.nsAttributes[num2].Set(XmlNodeType.Attribute, prefix, this.xmlns, this.reader.NameTable.Add(this.xmlns + ":" + prefix), this.xmlnsUri, ns);
			}
			this.state = XmlSubtreeReader.State.ClearNsAttributes;
			this.curNsAttr = -1;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000245C8 File Offset: 0x000227C8
		private void RemoveNamespace(string prefix, string localName)
		{
			for (int i = 0; i < this.nsAttrCount; i++)
			{
				if (Ref.Equal(prefix, this.nsAttributes[i].prefix) && Ref.Equal(localName, this.nsAttributes[i].localName))
				{
					if (i < this.nsAttrCount - 1)
					{
						XmlSubtreeReader.NodeData nodeData = this.nsAttributes[i];
						this.nsAttributes[i] = this.nsAttributes[this.nsAttrCount - 1];
						this.nsAttributes[this.nsAttrCount - 1] = nodeData;
					}
					this.nsAttrCount--;
					return;
				}
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002465D File Offset: 0x0002285D
		private void MoveToNsAttribute(int index)
		{
			this.reader.MoveToElement();
			this.curNsAttr = index;
			this.nsIncReadOffset = 0;
			this.SetCurrentNode(this.nsAttributes[index]);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00024688 File Offset: 0x00022888
		private bool InitReadElementContentAsBinary(XmlSubtreeReader.State binaryState)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
			}
			bool isEmptyElement = this.IsEmptyElement;
			if (!this.Read() || isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			if (nodeType != XmlNodeType.EndElement)
			{
				this.state = binaryState;
				return true;
			}
			this.ProcessNamespaces();
			this.Read();
			return false;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00024720 File Offset: 0x00022920
		private bool FinishReadElementContentAsBinary()
		{
			byte[] buffer = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadElementContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(buffer, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(buffer, 0, 256) > 0)
				{
				}
			}
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("Xml_InvalidNodeType", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return this.Read();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000247DC File Offset: 0x000229DC
		private bool FinishReadContentAsBinary()
		{
			byte[] buffer = new byte[256];
			if (this.state == XmlSubtreeReader.State.ReadContentAsBase64)
			{
				while (this.reader.ReadContentAsBase64(buffer, 0, 256) > 0)
				{
				}
			}
			else
			{
				while (this.reader.ReadContentAsBinHex(buffer, 0, 256) > 0)
				{
				}
			}
			this.state = XmlSubtreeReader.State.Interactive;
			this.ProcessNamespaces();
			if (this.reader.Depth == this.initialDepth)
			{
				this.state = XmlSubtreeReader.State.EndOfFile;
				this.SetEmptyNode();
				return false;
			}
			return true;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002485A File Offset: 0x00022A5A
		private bool InAttributeActiveState
		{
			get
			{
				return (98 & 1 << (int)this.state) != 0;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002486D File Offset: 0x00022A6D
		private bool InNamespaceActiveState
		{
			get
			{
				return (2018 & 1 << (int)this.state) != 0;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00024883 File Offset: 0x00022A83
		private void SetEmptyNode()
		{
			this.tmpNode.type = XmlNodeType.None;
			this.tmpNode.value = string.Empty;
			this.curNode = this.tmpNode;
			this.useCurNode = true;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000248B4 File Offset: 0x00022AB4
		private void SetCurrentNode(XmlSubtreeReader.NodeData node)
		{
			this.curNode = node;
			this.useCurNode = true;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000248C4 File Offset: 0x00022AC4
		private void InitReadContentAsType(string methodName)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				throw new InvalidOperationException(Res.GetString("Xml_ClosedOrErrorReader"));
			case XmlSubtreeReader.State.Interactive:
				return;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				return;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingReadValueChunkWithBinary"));
			default:
				throw base.CreateReadContentAsException(methodName);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00024938 File Offset: 0x00022B38
		private void FinishReadContentAsType()
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return;
					}
					this.state = XmlSubtreeReader.State.PopNamespaceScope;
				}
				return;
			}
			this.ProcessNamespaces();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00024968 File Offset: 0x00022B68
		private void CheckBuffer(Array buffer, int index, int count)
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
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000249B7 File Offset: 0x00022BB7
		public override Task<string> GetValueAsync()
		{
			if (this.useCurNode)
			{
				return Task.FromResult<string>(this.curNode.value);
			}
			return this.reader.GetValueAsync();
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000249E0 File Offset: 0x00022BE0
		public override Task<bool> ReadAsync()
		{
			XmlSubtreeReader.<ReadAsync>d__104 <ReadAsync>d__;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadAsync>d__104>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00024A24 File Offset: 0x00022C24
		public override Task SkipAsync()
		{
			XmlSubtreeReader.<SkipAsync>d__105 <SkipAsync>d__;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XmlSubtreeReader.<SkipAsync>d__105>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00024A68 File Offset: 0x00022C68
		public override Task<object> ReadContentAsObjectAsync()
		{
			XmlSubtreeReader.<ReadContentAsObjectAsync>d__106 <ReadContentAsObjectAsync>d__;
			<ReadContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsObjectAsync>d__.<>4__this = this;
			<ReadContentAsObjectAsync>d__.<>1__state = -1;
			<ReadContentAsObjectAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadContentAsObjectAsync>d__106>(ref <ReadContentAsObjectAsync>d__);
			return <ReadContentAsObjectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00024AAC File Offset: 0x00022CAC
		public override Task<string> ReadContentAsStringAsync()
		{
			XmlSubtreeReader.<ReadContentAsStringAsync>d__107 <ReadContentAsStringAsync>d__;
			<ReadContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadContentAsStringAsync>d__.<>4__this = this;
			<ReadContentAsStringAsync>d__.<>1__state = -1;
			<ReadContentAsStringAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadContentAsStringAsync>d__107>(ref <ReadContentAsStringAsync>d__);
			return <ReadContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00024AF0 File Offset: 0x00022CF0
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XmlSubtreeReader.<ReadContentAsAsync>d__108 <ReadContentAsAsync>d__;
			<ReadContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsAsync>d__.<>4__this = this;
			<ReadContentAsAsync>d__.returnType = returnType;
			<ReadContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadContentAsAsync>d__.<>1__state = -1;
			<ReadContentAsAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadContentAsAsync>d__108>(ref <ReadContentAsAsync>d__);
			return <ReadContentAsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00024B44 File Offset: 0x00022D44
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlSubtreeReader.<ReadContentAsBase64Async>d__109 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XmlSubtreeReader.<ReadContentAsBase64Async>d__109>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00024BA0 File Offset: 0x00022DA0
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlSubtreeReader.<ReadElementContentAsBase64Async>d__110 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XmlSubtreeReader.<ReadElementContentAsBase64Async>d__110>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00024BFC File Offset: 0x00022DFC
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlSubtreeReader.<ReadContentAsBinHexAsync>d__111 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadContentAsBinHexAsync>d__111>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00024C58 File Offset: 0x00022E58
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlSubtreeReader.<ReadElementContentAsBinHexAsync>d__112 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlSubtreeReader.<ReadElementContentAsBinHexAsync>d__112>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00024CB4 File Offset: 0x00022EB4
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			switch (this.state)
			{
			case XmlSubtreeReader.State.Initial:
			case XmlSubtreeReader.State.Error:
			case XmlSubtreeReader.State.EndOfFile:
			case XmlSubtreeReader.State.Closed:
				return Task.FromResult<int>(0);
			case XmlSubtreeReader.State.Interactive:
				break;
			case XmlSubtreeReader.State.PopNamespaceScope:
			case XmlSubtreeReader.State.ClearNsAttributes:
				if (this.curNsAttr != -1 && this.reader.CanReadValueChunk)
				{
					this.CheckBuffer(buffer, index, count);
					int num = this.curNode.value.Length - this.nsIncReadOffset;
					if (num > count)
					{
						num = count;
					}
					if (num > 0)
					{
						this.curNode.value.CopyTo(this.nsIncReadOffset, buffer, index, num);
					}
					this.nsIncReadOffset += num;
					return Task.FromResult<int>(num);
				}
				break;
			case XmlSubtreeReader.State.ReadElementContentAsBase64:
			case XmlSubtreeReader.State.ReadElementContentAsBinHex:
			case XmlSubtreeReader.State.ReadContentAsBase64:
			case XmlSubtreeReader.State.ReadContentAsBinHex:
				throw new InvalidOperationException(Res.GetString("Xml_MixingReadValueChunkWithBinary"));
			default:
				return Task.FromResult<int>(0);
			}
			return this.reader.ReadValueChunkAsync(buffer, index, count);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00024D9C File Offset: 0x00022F9C
		private Task<bool> InitReadElementContentAsBinaryAsync(XmlSubtreeReader.State binaryState)
		{
			XmlSubtreeReader.<InitReadElementContentAsBinaryAsync>d__114 <InitReadElementContentAsBinaryAsync>d__;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<InitReadElementContentAsBinaryAsync>d__.binaryState = binaryState;
			<InitReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlSubtreeReader.<InitReadElementContentAsBinaryAsync>d__114>(ref <InitReadElementContentAsBinaryAsync>d__);
			return <InitReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00024DE8 File Offset: 0x00022FE8
		private Task<bool> FinishReadElementContentAsBinaryAsync()
		{
			XmlSubtreeReader.<FinishReadElementContentAsBinaryAsync>d__115 <FinishReadElementContentAsBinaryAsync>d__;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<FinishReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlSubtreeReader.<FinishReadElementContentAsBinaryAsync>d__115>(ref <FinishReadElementContentAsBinaryAsync>d__);
			return <FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00024E2C File Offset: 0x0002302C
		private Task<bool> FinishReadContentAsBinaryAsync()
		{
			XmlSubtreeReader.<FinishReadContentAsBinaryAsync>d__116 <FinishReadContentAsBinaryAsync>d__;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<FinishReadContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlSubtreeReader.<FinishReadContentAsBinaryAsync>d__116>(ref <FinishReadContentAsBinaryAsync>d__);
			return <FinishReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000354 RID: 852
		private const int AttributeActiveStates = 98;

		// Token: 0x04000355 RID: 853
		private const int NamespaceActiveStates = 2018;

		// Token: 0x04000356 RID: 854
		private int initialDepth;

		// Token: 0x04000357 RID: 855
		private XmlSubtreeReader.State state;

		// Token: 0x04000358 RID: 856
		private XmlNamespaceManager nsManager;

		// Token: 0x04000359 RID: 857
		private XmlSubtreeReader.NodeData[] nsAttributes;

		// Token: 0x0400035A RID: 858
		private int nsAttrCount;

		// Token: 0x0400035B RID: 859
		private int curNsAttr = -1;

		// Token: 0x0400035C RID: 860
		private string xmlns;

		// Token: 0x0400035D RID: 861
		private string xmlnsUri;

		// Token: 0x0400035E RID: 862
		private int nsIncReadOffset;

		// Token: 0x0400035F RID: 863
		private IncrementalReadDecoder binDecoder;

		// Token: 0x04000360 RID: 864
		private bool useCurNode;

		// Token: 0x04000361 RID: 865
		private XmlSubtreeReader.NodeData curNode;

		// Token: 0x04000362 RID: 866
		private XmlSubtreeReader.NodeData tmpNode;

		// Token: 0x04000363 RID: 867
		internal int InitialNamespaceAttributeCount = 4;

		// Token: 0x0200035C RID: 860
		private class NodeData
		{
			// Token: 0x06002E54 RID: 11860 RVA: 0x000F6ABE File Offset: 0x000F4CBE
			internal NodeData()
			{
			}

			// Token: 0x06002E55 RID: 11861 RVA: 0x000F6AC6 File Offset: 0x000F4CC6
			internal void Set(XmlNodeType nodeType, string localName, string prefix, string name, string namespaceUri, string value)
			{
				this.type = nodeType;
				this.localName = localName;
				this.prefix = prefix;
				this.name = name;
				this.namespaceUri = namespaceUri;
				this.value = value;
			}

			// Token: 0x0400165E RID: 5726
			internal XmlNodeType type;

			// Token: 0x0400165F RID: 5727
			internal string localName;

			// Token: 0x04001660 RID: 5728
			internal string prefix;

			// Token: 0x04001661 RID: 5729
			internal string name;

			// Token: 0x04001662 RID: 5730
			internal string namespaceUri;

			// Token: 0x04001663 RID: 5731
			internal string value;
		}

		// Token: 0x0200035D RID: 861
		private enum State
		{
			// Token: 0x04001665 RID: 5733
			Initial,
			// Token: 0x04001666 RID: 5734
			Interactive,
			// Token: 0x04001667 RID: 5735
			Error,
			// Token: 0x04001668 RID: 5736
			EndOfFile,
			// Token: 0x04001669 RID: 5737
			Closed,
			// Token: 0x0400166A RID: 5738
			PopNamespaceScope,
			// Token: 0x0400166B RID: 5739
			ClearNsAttributes,
			// Token: 0x0400166C RID: 5740
			ReadElementContentAsBase64,
			// Token: 0x0400166D RID: 5741
			ReadElementContentAsBinHex,
			// Token: 0x0400166E RID: 5742
			ReadContentAsBase64,
			// Token: 0x0400166F RID: 5743
			ReadContentAsBinHex
		}
	}
}
