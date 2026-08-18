using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000EB RID: 235
	public class XmlNodeReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x0003FA23 File Offset: 0x0003EA23
		public XmlNodeReader(XmlNode node)
		{
			this.Init(node);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003FA32 File Offset: 0x0003EA32
		private void Init(XmlNode node)
		{
			this.readerNav = new XmlNodeReaderNavigator(node);
			this.curDepth = 0;
			this.readState = ReadState.Initial;
			this.fEOF = false;
			this.nodeType = XmlNodeType.None;
			this.bResolveEntity = false;
			this.bStartFromDocument = false;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003FA6A File Offset: 0x0003EA6A
		internal bool IsInReadingStates()
		{
			return this.readState == ReadState.Interactive;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003FA75 File Offset: 0x0003EA75
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

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0003FA87 File Offset: 0x0003EA87
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0003FAA2 File Offset: 0x0003EAA2
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

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003FABD File Offset: 0x0003EABD
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

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003FAD8 File Offset: 0x0003EAD8
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

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003FAF3 File Offset: 0x0003EAF3
		public override bool HasValue
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.HasValue;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003FB0A File Offset: 0x0003EB0A
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0003FB25 File Offset: 0x0003EB25
		public override int Depth
		{
			get
			{
				return this.curDepth;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0003FB2D File Offset: 0x0003EB2D
		public override string BaseURI
		{
			get
			{
				return this.readerNav.BaseURI;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003FB3A File Offset: 0x0003EB3A
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0003FB3D File Offset: 0x0003EB3D
		public override bool IsEmptyElement
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsEmptyElement;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003FB54 File Offset: 0x0003EB54
		public override bool IsDefault
		{
			get
			{
				return this.IsInReadingStates() && this.readerNav.IsDefault;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003FB6B File Offset: 0x0003EB6B
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003FB82 File Offset: 0x0003EB82
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

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003FB9D File Offset: 0x0003EB9D
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0003FBB4 File Offset: 0x0003EBB4
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

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003FBD5 File Offset: 0x0003EBD5
		public override string GetAttribute(string name)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			return this.readerNav.GetAttribute(name);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003FBF0 File Offset: 0x0003EBF0
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (!this.IsInReadingStates())
			{
				return null;
			}
			string ns = (namespaceURI == null) ? string.Empty : namespaceURI;
			return this.readerNav.GetAttribute(name, ns);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003FC20 File Offset: 0x0003EC20
		public override string GetAttribute(int attributeIndex)
		{
			if (!this.IsInReadingStates())
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
			return this.readerNav.GetAttribute(attributeIndex);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003FC44 File Offset: 0x0003EC44
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

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003FCC4 File Offset: 0x0003ECC4
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

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003FD50 File Offset: 0x0003ED50
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

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003FDFC File Offset: 0x0003EDFC
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

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003FE80 File Offset: 0x0003EE80
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

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003FF0C File Offset: 0x0003EF0C
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

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003FF93 File Offset: 0x0003EF93
		public override bool Read()
		{
			return this.Read(false);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003FF9C File Offset: 0x0003EF9C
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

		// Token: 0x06000E61 RID: 3681 RVA: 0x00040074 File Offset: 0x0003F074
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
				return this.ReadForward(fSkipChildren);
			}
			else
			{
				if (this.readerNav.NodeType == XmlNodeType.EntityReference && this.bResolveEntity)
				{
					this.readerNav.MoveToFirstChild();
					this.nodeType = this.readerNav.NodeType;
					this.curDepth++;
					this.bResolveEntity = false;
					return true;
				}
				return this.ReadForward(fSkipChildren);
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000401C2 File Offset: 0x0003F1C2
		private void SetEndOfFile()
		{
			this.fEOF = true;
			this.readState = ReadState.EndOfFile;
			this.nodeType = XmlNodeType.None;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000401D9 File Offset: 0x0003F1D9
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

		// Token: 0x06000E64 RID: 3684 RVA: 0x00040214 File Offset: 0x0003F214
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

		// Token: 0x06000E65 RID: 3685 RVA: 0x000402D0 File Offset: 0x0003F2D0
		private void ReSetReadingMarks()
		{
			this.readerNav.ResetMove(ref this.curDepth, ref this.nodeType);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000402E9 File Offset: 0x0003F2E9
		public override bool EOF
		{
			get
			{
				return this.readState != ReadState.Closed && this.fEOF;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000402FC File Offset: 0x0003F2FC
		public override void Close()
		{
			this.readState = ReadState.Closed;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00040305 File Offset: 0x0003F305
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0004030D File Offset: 0x0003F30D
		public override void Skip()
		{
			this.Read(true);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00040317 File Offset: 0x0003F317
		public override string ReadString()
		{
			if (this.NodeType == XmlNodeType.EntityReference && this.bResolveEntity && !this.Read())
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
			return base.ReadString();
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00040348 File Offset: 0x0003F348
		public override bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00040353 File Offset: 0x0003F353
		public override XmlNameTable NameTable
		{
			get
			{
				return this.readerNav.NameTable;
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00040360 File Offset: 0x0003F360
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

		// Token: 0x06000E6E RID: 3694 RVA: 0x00040392 File Offset: 0x0003F392
		public override void ResolveEntity()
		{
			if (!this.IsInReadingStates() || this.nodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Xnr_ResolveEntity"));
			}
			this.bResolveEntity = true;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000403BC File Offset: 0x0003F3BC
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x000403F1 File Offset: 0x0003F3F1
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000403F4 File Offset: 0x0003F3F4
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

		// Token: 0x06000E72 RID: 3698 RVA: 0x00040444 File Offset: 0x0003F444
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

		// Token: 0x06000E73 RID: 3699 RVA: 0x00040494 File Offset: 0x0003F494
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

		// Token: 0x06000E74 RID: 3700 RVA: 0x000404E4 File Offset: 0x0003F4E4
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

		// Token: 0x06000E75 RID: 3701 RVA: 0x00040534 File Offset: 0x0003F534
		private void FinishReadBinary()
		{
			this.bInReadBinary = false;
			this.readBinaryHelper.Finish();
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00040548 File Offset: 0x0003F548
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerNav.GetNamespacesInScope(scope);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00040556 File Offset: 0x0003F556
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerNav.LookupPrefix(namespaceName);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00040564 File Offset: 0x0003F564
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

		// Token: 0x0400099A RID: 2458
		private XmlNodeReaderNavigator readerNav;

		// Token: 0x0400099B RID: 2459
		private XmlNodeType nodeType;

		// Token: 0x0400099C RID: 2460
		private int curDepth;

		// Token: 0x0400099D RID: 2461
		private ReadState readState;

		// Token: 0x0400099E RID: 2462
		private bool fEOF;

		// Token: 0x0400099F RID: 2463
		private bool bResolveEntity;

		// Token: 0x040009A0 RID: 2464
		private bool bStartFromDocument;

		// Token: 0x040009A1 RID: 2465
		private bool bInReadBinary;

		// Token: 0x040009A2 RID: 2466
		private ReadContentAsBinaryHelper readBinaryHelper;
	}
}
