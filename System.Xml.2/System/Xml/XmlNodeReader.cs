using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200011A RID: 282
	public class XmlNodeReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x00052DF0 File Offset: 0x00050FF0
		public XmlNodeReader(XmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.readerNav = new XmlNodeReaderNavigator(node);
			this.curDepth = 0;
			this.readState = ReadState.Initial;
			this.fEOF = false;
			this.nodeType = XmlNodeType.None;
			this.bResolveEntity = false;
			this.bStartFromDocument = false;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00052E47 File Offset: 0x00051047
		internal bool IsInReadingStates()
		{
			return this.readState == ReadState.Interactive;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00052E52 File Offset: 0x00051052
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlNodeType.None;
				}
				return this.nodeType;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00052E64 File Offset: 0x00051064
		public override string Name
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Name;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00052E7F File Offset: 0x0005107F
		public override string LocalName
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.LocalName;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00052E9A File Offset: 0x0005109A
		public override string NamespaceURI
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.NamespaceURI;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00052EB5 File Offset: 0x000510B5
		public override string Prefix
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Prefix;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00052ED0 File Offset: 0x000510D0
		public override bool HasValue
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.HasValue;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00052EE7 File Offset: 0x000510E7
		public override string Value
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.Value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00052F02 File Offset: 0x00051102
		public override int Depth
		{
			get
			{
				return this.curDepth;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00052F0A File Offset: 0x0005110A
		public override string BaseURI
		{
			get
			{
				return this.readerNav.BaseURI;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00052F17 File Offset: 0x00051117
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00052F1A File Offset: 0x0005111A
		public override bool IsEmptyElement
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsEmptyElement;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00052F31 File Offset: 0x00051131
		public override bool IsDefault
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsDefault;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00052F48 File Offset: 0x00051148
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return XmlSpace.None;
				}
				return this.readerNav.XmlSpace;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00052F5F File Offset: 0x0005115F
		public override string XmlLang
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return string.Empty;
				}
				return this.readerNav.XmlLang;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00052F7A File Offset: 0x0005117A
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (!this.IsInReadingStates())
				{
					return null;
				}
				return this.readerNav.SchemaInfo;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00052F91 File Offset: 0x00051191
		public override int AttributeCount
		{
			get
			{
				if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
				{
					return 0;
				}
				return this.readerNav.AttributeCount;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00052FB2 File Offset: 0x000511B2
		public override string GetAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			return this.readerNav.GetAttribute(name);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00052FCC File Offset: 0x000511CC
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string ns = (namespaceURI == null) ? string.Empty : namespaceURI;
			return this.readerNav.GetAttribute(name, ns);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00052FFC File Offset: 0x000511FC
		public override string GetAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			return this.readerNav.GetAttribute(attributeIndex);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00053020 File Offset: 0x00051220
		public override bool MoveToAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.readerNav.MoveToAttribute(name))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000530A0 File Offset: 0x000512A0
		public override bool MoveToAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			string namespaceURI2 = (namespaceURI == null) ? string.Empty : namespaceURI;
			if (this.readerNav.MoveToAttribute(name, namespaceURI2))
			{
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0005312C File Offset: 0x0005132C
		public override void MoveToAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			try
			{
				if (this.AttributeCount <= 0)
				{
					throw new ArgumentOutOfRangeException("attributeIndex");
				}
				this.readerNav.MoveToAttribute(attributeIndex);
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
			}
			catch
			{
				this.readerNav.RollBackMove(ref this.curDepth);
				throw;
			}
			this.curDepth++;
			this.nodeType = this.readerNav.NodeType;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000531D8 File Offset: 0x000513D8
		public override bool MoveToFirstAttribute()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
			if (this.AttributeCount > 0)
			{
				this.readerNav.MoveToAttribute(0);
				this.curDepth++;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0005325C File Offset: 0x0005145C
		public override bool MoveToNextAttribute()
		{
			if (!this.IsInReadingStates() || this.nodeType == XmlNodeType.EndElement)
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToNextAttribute(ref this.curDepth))
			{
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000532E8 File Offset: 0x000514E8
		public override bool MoveToElement()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			this.readerNav.LogMove(this.curDepth);
			this.readerNav.ResetToAttribute(ref this.curDepth);
			if (this.readerNav.MoveToElement())
			{
				this.curDepth--;
				this.nodeType = this.readerNav.NodeType;
				if (this.bInReadBinary)
				{
					this.FinishReadBinary();
				}
				return true;
			}
			this.readerNav.RollBackMove(ref this.curDepth);
			return false;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0005336F File Offset: 0x0005156F
		public override bool Read()
		{
			return this.Read(false);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00053378 File Offset: 0x00051578
		private bool Read(bool fSkipChildren)
		{
			if (this.fEOF)
			{
				return false;
			}
			if (this.readState == ReadState.Initial)
			{
				if (this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment)
				{
					this.bStartFromDocument = true;
					if (!this.ReadNextNode(fSkipChildren))
					{
						this.readState = ReadState.Error;
						return false;
					}
				}
				this.ReSetReadingMarks();
				this.readState = ReadState.Interactive;
				this.nodeType = this.readerNav.NodeType;
				this.curDepth = 0;
				return true;
			}
			if (this.bInReadBinary)
			{
				this.FinishReadBinary();
			}
			if (this.readerNav.CreatedOnAttribute)
			{
				return false;
			}
			this.ReSetReadingMarks();
			bool flag = this.ReadNextNode(fSkipChildren);
			if (flag)
			{
				return true;
			}
			if (this.readState == ReadState.Initial || this.readState == ReadState.Interactive)
			{
				this.readState = ReadState.Error;
			}
			if (this.readState == ReadState.EndOfFile)
			{
				this.nodeType = XmlNodeType.None;
			}
			return false;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00053450 File Offset: 0x00051650
		private bool ReadNextNode(bool fSkipChildren)
		{
			if (this.readState != ReadState.Interactive && this.readState != ReadState.Initial)
			{
				this.nodeType = XmlNodeType.None;
				return false;
			}
			bool flag = !fSkipChildren;
			XmlNodeType xmlNodeType = this.readerNav.NodeType;
			flag = (flag && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.EndEntity && (xmlNodeType == XmlNodeType.Element || (xmlNodeType == XmlNodeType.EntityReference && this.bResolveEntity) || ((this.readerNav.NodeType == XmlNodeType.Document || this.readerNav.NodeType == XmlNodeType.DocumentFragment) && this.readState == ReadState.Initial)));
			if (flag)
			{
				if (this.readerNav.MoveToFirstChild())
				{
					this.nodeType = this.readerNav.NodeType;
					this.curDepth++;
					if (this.bResolveEntity)
					{
						this.bResolveEntity = false;
					}
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
				{
					this.nodeType = XmlNodeType.EndElement;
					return true;
				}
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					this.bResolveEntity = false;
					this.nodeType = XmlNodeType.EndEntity;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
			else
			{
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					if (this.readerNav.MoveToFirstChild())
					{
						this.nodeType = this.readerNav.NodeType;
						this.curDepth++;
					}
					else
					{
						this.nodeType = XmlNodeType.EndEntity;
					}
					this.bResolveEntity = false;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000535D3 File Offset: 0x000517D3
		private void SetEndOfFile()
		{
			this.fEOF = true;
			this.readState = ReadState.EndOfFile;
			this.nodeType = XmlNodeType.None;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000535EA File Offset: 0x000517EA
		private bool ReadAtZeroLevel(bool fSkipChildren)
		{
			if (!fSkipChildren && this.nodeType != XmlNodeType.EndElement && this.readerNav.NodeType == XmlNodeType.Element && !this.readerNav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			this.SetEndOfFile();
			return false;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00053628 File Offset: 0x00051828
		private bool ReadForward(bool fSkipChildren)
		{
			if (this.readState == ReadState.Error)
			{
				return false;
			}
			if (!this.bStartFromDocument && this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (this.readerNav.MoveToNext())
			{
				this.nodeType = this.readerNav.NodeType;
				return true;
			}
			if (this.curDepth == 0)
			{
				return this.ReadAtZeroLevel(fSkipChildren);
			}
			if (!this.readerNav.MoveToParent())
			{
				return false;
			}
			if (this.readerNav.NodeType == XmlNodeType.Element)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndElement;
				return true;
			}
			if (this.readerNav.NodeType == XmlNodeType.EntityReference)
			{
				this.curDepth--;
				this.nodeType = XmlNodeType.EndEntity;
				return true;
			}
			return true;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000536E4 File Offset: 0x000518E4
		private void ReSetReadingMarks()
		{
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000536FD File Offset: 0x000518FD
		public override bool EOF
		{
			get
			{
				return this.readState != ReadState.Closed && this.fEOF;
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00053710 File Offset: 0x00051910
		public override void Close()
		{
			this.readState = ReadState.Closed;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00053719 File Offset: 0x00051919
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00053721 File Offset: 0x00051921
		public override void Skip()
		{
			this.Read(true);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005372B File Offset: 0x0005192B
		public override string ReadString()
		{
			if (this.NodeType == XmlNodeType.EntityReference && this.bResolveEntity && !this.Read())
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
			return base.ReadString();
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0005375C File Offset: 0x0005195C
		public override bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00053767 File Offset: 0x00051967
		public override XmlNameTable NameTable
		{
			get
			{
				return this.readerNav.NameTable;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00053774 File Offset: 0x00051974
		public override string LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000537A6 File Offset: 0x000519A6
		public override void ResolveEntity()
		{
			if (!this.IsInReadingStates() || this.nodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Xnr_ResolveEntity"));
			}
			this.bResolveEntity = true;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x000537D0 File Offset: 0x000519D0
		public override bool ReadAttributeValue()
		{
			if (!this.IsInReadingStates())
			{
				return false;
			}
			if (this.readerNav.ReadAttributeValue(ref this.curDepth, ref this.bResolveEntity, ref this.nodeType))
			{
				this.bInReadBinary = false;
				return true;
			}
			return false;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00053805 File Offset: 0x00051A05
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00053808 File Offset: 0x00051A08
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return result;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00053858 File Offset: 0x00051A58
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return result;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000538A8 File Offset: 0x00051AA8
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.bInReadBinary = true;
			return result;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000538F8 File Offset: 0x00051AF8
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.readState != ReadState.Interactive)
			{
				return 0;
			}
			if (!this.bInReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			this.bInReadBinary = false;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.bInReadBinary = true;
			return result;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00053948 File Offset: 0x00051B48
		private void FinishReadBinary()
		{
			this.bInReadBinary = false;
			this.readBinaryHelper.Finish();
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0005395C File Offset: 0x00051B5C
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerNav.GetNamespacesInScope(scope);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0005396A File Offset: 0x00051B6A
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerNav.LookupPrefix(namespaceName);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00053978 File Offset: 0x00051B78
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (!this.IsInReadingStates())
			{
				return this.readerNav.DefaultLookupNamespace(prefix);
			}
			string text = this.readerNav.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.readerNav.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000539BD File Offset: 0x00051BBD
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.readerNav.Document.DtdSchemaInfo;
			}
		}

		// Token: 0x0400057A RID: 1402
		private XmlNodeReaderNavigator readerNav;

		// Token: 0x0400057B RID: 1403
		private XmlNodeType nodeType;

		// Token: 0x0400057C RID: 1404
		private int curDepth;

		// Token: 0x0400057D RID: 1405
		private ReadState readState;

		// Token: 0x0400057E RID: 1406
		private bool fEOF;

		// Token: 0x0400057F RID: 1407
		private bool bResolveEntity;

		// Token: 0x04000580 RID: 1408
		private bool bStartFromDocument;

		// Token: 0x04000581 RID: 1409
		private bool bInReadBinary;

		// Token: 0x04000582 RID: 1410
		private ReadContentAsBinaryHelper readBinaryHelper;
	}
}
