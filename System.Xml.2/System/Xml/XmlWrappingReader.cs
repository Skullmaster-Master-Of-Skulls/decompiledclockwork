using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000E2 RID: 226
	internal class XmlWrappingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003FB55 File Offset: 0x0003DD55
		internal XmlWrappingReader(XmlReader baseReader)
		{
			this.reader = baseReader;
			this.readerAsIXmlLineInfo = (baseReader as IXmlLineInfo);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0003FB70 File Offset: 0x0003DD70
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.reader.Settings;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0003FB7D File Offset: 0x0003DD7D
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0003FB8A File Offset: 0x0003DD8A
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0003FB97 File Offset: 0x0003DD97
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0003FBB1 File Offset: 0x0003DDB1
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003FBBE File Offset: 0x0003DDBE
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0003FBCB File Offset: 0x0003DDCB
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0003FBE5 File Offset: 0x0003DDE5
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003FBF2 File Offset: 0x0003DDF2
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0003FBFF File Offset: 0x0003DDFF
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003FC0C File Offset: 0x0003DE0C
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0003FC19 File Offset: 0x0003DE19
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003FC26 File Offset: 0x0003DE26
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0003FC33 File Offset: 0x0003DE33
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003FC40 File Offset: 0x0003DE40
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0003FC4D File Offset: 0x0003DE4D
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003FC5A File Offset: 0x0003DE5A
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0003FC67 File Offset: 0x0003DE67
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0003FC74 File Offset: 0x0003DE74
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003FC81 File Offset: 0x0003DE81
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003FC8E File Offset: 0x0003DE8E
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003FC9B File Offset: 0x0003DE9B
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003FCA9 File Offset: 0x0003DEA9
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003FCC6 File Offset: 0x0003DEC6
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003FCE3 File Offset: 0x0003DEE3
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003FCF1 File Offset: 0x0003DEF1
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003FCFE File Offset: 0x0003DEFE
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003FD0B File Offset: 0x0003DF0B
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003FD18 File Offset: 0x0003DF18
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003FD25 File Offset: 0x0003DF25
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003FD32 File Offset: 0x0003DF32
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003FD3F File Offset: 0x0003DF3F
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003FD4D File Offset: 0x0003DF4D
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003FD5A File Offset: 0x0003DF5A
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003FD67 File Offset: 0x0003DF67
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo != null && this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0003FD7E File Offset: 0x0003DF7E
		public virtual int LineNumber
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0003FD95 File Offset: 0x0003DF95
		public virtual int LinePosition
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.reader.DtdInfo;
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003FDB9 File Offset: 0x0003DFB9
		public override Task<string> GetValueAsync()
		{
			return this.reader.GetValueAsync();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003FDC6 File Offset: 0x0003DFC6
		public override Task<bool> ReadAsync()
		{
			return this.reader.ReadAsync();
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003FDD3 File Offset: 0x0003DFD3
		public override Task SkipAsync()
		{
			return this.reader.SkipAsync();
		}

		// Token: 0x0400043E RID: 1086
		protected XmlReader reader;

		// Token: 0x0400043F RID: 1087
		protected IXmlLineInfo readerAsIXmlLineInfo;
	}
}
