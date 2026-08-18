using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000F9 RID: 249
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x00221C28 File Offset: 0x00221028
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00221C48 File Offset: 0x00221048
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00221C68 File Offset: 0x00221068
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00221C88 File Offset: 0x00221088
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00221CA8 File Offset: 0x002210A8
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00221CC8 File Offset: 0x002210C8
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00221CE8 File Offset: 0x002210E8
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00221D08 File Offset: 0x00221108
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00221D28 File Offset: 0x00221128
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00221D48 File Offset: 0x00221148
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00221D68 File Offset: 0x00221168
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00221D88 File Offset: 0x00221188
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00221DA8 File Offset: 0x002211A8
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00221DC8 File Offset: 0x002211C8
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00221DE8 File Offset: 0x002211E8
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00221E08 File Offset: 0x00221208
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00221E28 File Offset: 0x00221228
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00221E48 File Offset: 0x00221248
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00221E68 File Offset: 0x00221268
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00221E88 File Offset: 0x00221288
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00221EA8 File Offset: 0x002212A8
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00221EC8 File Offset: 0x002212C8
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00221EE8 File Offset: 0x002212E8
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00221F08 File Offset: 0x00221308
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00221F28 File Offset: 0x00221328
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00221F48 File Offset: 0x00221348
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00221F68 File Offset: 0x00221368
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00221F88 File Offset: 0x00221388
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00221FA8 File Offset: 0x002213A8
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00221FC8 File Offset: 0x002213C8
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00221FE8 File Offset: 0x002213E8
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00222008 File Offset: 0x00221408
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00222028 File Offset: 0x00221428
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00222048 File Offset: 0x00221448
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00222068 File Offset: 0x00221468
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00222088 File Offset: 0x00221488
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x002220A8 File Offset: 0x002214A8
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x002220C8 File Offset: 0x002214C8
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x002220E8 File Offset: 0x002214E8
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00222108 File Offset: 0x00221508
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00222128 File Offset: 0x00221528
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00222148 File Offset: 0x00221548
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00222168 File Offset: 0x00221568
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00222188 File Offset: 0x00221588
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x04000A90 RID: 2704
		private XmlReader _xmlreader;
	}
}
