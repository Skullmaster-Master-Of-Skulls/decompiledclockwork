using System;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000140 RID: 320
	internal sealed class DataTextReader : XmlReader
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x00094284 File Offset: 0x00093684
		internal static XmlReader CreateReader(XmlReader xr)
		{
			return new DataTextReader(xr);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00094298 File Offset: 0x00093698
		private DataTextReader(XmlReader input)
		{
			this._xmlreader = input;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000942B4 File Offset: 0x000936B4
		public override XmlReaderSettings Settings
		{
			get
			{
				return this._xmlreader.Settings;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000942CC File Offset: 0x000936CC
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xmlreader.NodeType;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000942E4 File Offset: 0x000936E4
		public override string Name
		{
			get
			{
				return this._xmlreader.Name;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000942FC File Offset: 0x000936FC
		public override string LocalName
		{
			get
			{
				return this._xmlreader.LocalName;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00094314 File Offset: 0x00093714
		public override string NamespaceURI
		{
			get
			{
				return this._xmlreader.NamespaceURI;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0009432C File Offset: 0x0009372C
		public override string Prefix
		{
			get
			{
				return this._xmlreader.Prefix;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00094344 File Offset: 0x00093744
		public override bool HasValue
		{
			get
			{
				return this._xmlreader.HasValue;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0009435C File Offset: 0x0009375C
		public override string Value
		{
			get
			{
				return this._xmlreader.Value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00094374 File Offset: 0x00093774
		public override int Depth
		{
			get
			{
				return this._xmlreader.Depth;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0009438C File Offset: 0x0009378C
		public override string BaseURI
		{
			get
			{
				return this._xmlreader.BaseURI;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000943A4 File Offset: 0x000937A4
		public override bool IsEmptyElement
		{
			get
			{
				return this._xmlreader.IsEmptyElement;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000943BC File Offset: 0x000937BC
		public override bool IsDefault
		{
			get
			{
				return this._xmlreader.IsDefault;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000943D4 File Offset: 0x000937D4
		public override char QuoteChar
		{
			get
			{
				return this._xmlreader.QuoteChar;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000943EC File Offset: 0x000937EC
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmlreader.XmlSpace;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00094404 File Offset: 0x00093804
		public override string XmlLang
		{
			get
			{
				return this._xmlreader.XmlLang;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0009441C File Offset: 0x0009381C
		public override int AttributeCount
		{
			get
			{
				return this._xmlreader.AttributeCount;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00094434 File Offset: 0x00093834
		public override string GetAttribute(string name)
		{
			return this._xmlreader.GetAttribute(name);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00094450 File Offset: 0x00093850
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0009446C File Offset: 0x0009386C
		public override string GetAttribute(int i)
		{
			return this._xmlreader.GetAttribute(i);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00094488 File Offset: 0x00093888
		public override bool MoveToAttribute(string name)
		{
			return this._xmlreader.MoveToAttribute(name);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000944A4 File Offset: 0x000938A4
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._xmlreader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000944C0 File Offset: 0x000938C0
		public override void MoveToAttribute(int i)
		{
			this._xmlreader.MoveToAttribute(i);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000944DC File Offset: 0x000938DC
		public override bool MoveToFirstAttribute()
		{
			return this._xmlreader.MoveToFirstAttribute();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000944F4 File Offset: 0x000938F4
		public override bool MoveToNextAttribute()
		{
			return this._xmlreader.MoveToNextAttribute();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0009450C File Offset: 0x0009390C
		public override bool MoveToElement()
		{
			return this._xmlreader.MoveToElement();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00094524 File Offset: 0x00093924
		public override bool ReadAttributeValue()
		{
			return this._xmlreader.ReadAttributeValue();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0009453C File Offset: 0x0009393C
		public override bool Read()
		{
			return this._xmlreader.Read();
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00094554 File Offset: 0x00093954
		public override bool EOF
		{
			get
			{
				return this._xmlreader.EOF;
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0009456C File Offset: 0x0009396C
		public override void Close()
		{
			this._xmlreader.Close();
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00094584 File Offset: 0x00093984
		public override ReadState ReadState
		{
			get
			{
				return this._xmlreader.ReadState;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0009459C File Offset: 0x0009399C
		public override void Skip()
		{
			this._xmlreader.Skip();
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000945B4 File Offset: 0x000939B4
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xmlreader.NameTable;
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000945CC File Offset: 0x000939CC
		public override string LookupNamespace(string prefix)
		{
			return this._xmlreader.LookupNamespace(prefix);
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000945E8 File Offset: 0x000939E8
		public override bool CanResolveEntity
		{
			get
			{
				return this._xmlreader.CanResolveEntity;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00094600 File Offset: 0x00093A00
		public override void ResolveEntity()
		{
			this._xmlreader.ResolveEntity();
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00094618 File Offset: 0x00093A18
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._xmlreader.CanReadBinaryContent;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00094630 File Offset: 0x00093A30
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0009464C File Offset: 0x00093A4C
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00094668 File Offset: 0x00093A68
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00094684 File Offset: 0x00093A84
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._xmlreader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000946A0 File Offset: 0x00093AA0
		public override bool CanReadValueChunk
		{
			get
			{
				return this._xmlreader.CanReadValueChunk;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000946B8 File Offset: 0x00093AB8
		public override string ReadString()
		{
			return this._xmlreader.ReadString();
		}

		// Token: 0x0400076B RID: 1899
		private XmlReader _xmlreader;
	}
}
