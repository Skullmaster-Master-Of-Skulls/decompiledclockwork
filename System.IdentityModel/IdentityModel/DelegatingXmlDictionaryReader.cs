using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000035 RID: 53
	public class DelegatingXmlDictionaryReader : XmlDictionaryReader
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000080E4 File Offset: 0x000062E4
		protected DelegatingXmlDictionaryReader()
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000080EC File Offset: 0x000062EC
		protected void InitializeInnerReader(XmlDictionaryReader innerReader)
		{
			if (innerReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerReader");
			}
			this._innerReader = innerReader;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008108 File Offset: 0x00006308
		protected XmlDictionaryReader InnerReader
		{
			get
			{
				return this._innerReader;
			}
		}

		// Token: 0x17000058 RID: 88
		public override string this[int i]
		{
			get
			{
				return this._innerReader[i];
			}
		}

		// Token: 0x17000059 RID: 89
		public override string this[string name]
		{
			get
			{
				return this._innerReader[name];
			}
		}

		// Token: 0x1700005A RID: 90
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this._innerReader[name, namespaceURI];
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000813B File Offset: 0x0000633B
		public override int AttributeCount
		{
			get
			{
				return this._innerReader.AttributeCount;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008148 File Offset: 0x00006348
		public override string BaseURI
		{
			get
			{
				return this._innerReader.BaseURI;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008155 File Offset: 0x00006355
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008162 File Offset: 0x00006362
		public override bool EOF
		{
			get
			{
				return this._innerReader.EOF;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000816F File Offset: 0x0000636F
		public override bool HasValue
		{
			get
			{
				return this._innerReader.HasValue;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000817C File Offset: 0x0000637C
		public override bool IsDefault
		{
			get
			{
				return this._innerReader.IsDefault;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008189 File Offset: 0x00006389
		public override bool IsEmptyElement
		{
			get
			{
				return this._innerReader.IsEmptyElement;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008196 File Offset: 0x00006396
		public override string LocalName
		{
			get
			{
				return this._innerReader.LocalName;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000081A3 File Offset: 0x000063A3
		public override string Name
		{
			get
			{
				return this._innerReader.Name;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000081B0 File Offset: 0x000063B0
		public override string NamespaceURI
		{
			get
			{
				return this._innerReader.NamespaceURI;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000081BD File Offset: 0x000063BD
		public override XmlNameTable NameTable
		{
			get
			{
				return this._innerReader.NameTable;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000081CA File Offset: 0x000063CA
		public override XmlNodeType NodeType
		{
			get
			{
				return this._innerReader.NodeType;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000081D7 File Offset: 0x000063D7
		public override string Prefix
		{
			get
			{
				return this._innerReader.Prefix;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000081E4 File Offset: 0x000063E4
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000081F1 File Offset: 0x000063F1
		public override ReadState ReadState
		{
			get
			{
				return this._innerReader.ReadState;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000081FE File Offset: 0x000063FE
		public override string Value
		{
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000820B File Offset: 0x0000640B
		public override Type ValueType
		{
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008218 File Offset: 0x00006418
		public override string XmlLang
		{
			get
			{
				return this._innerReader.XmlLang;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008225 File Offset: 0x00006425
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._innerReader.XmlSpace;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008232 File Offset: 0x00006432
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000823F File Offset: 0x0000643F
		public override string GetAttribute(int i)
		{
			return this._innerReader.GetAttribute(i);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000824D File Offset: 0x0000644D
		public override string GetAttribute(string name)
		{
			return this._innerReader.GetAttribute(name);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000825B File Offset: 0x0000645B
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this._innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000826A File Offset: 0x0000646A
		public override string LookupNamespace(string prefix)
		{
			return this._innerReader.LookupNamespace(prefix);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008278 File Offset: 0x00006478
		public override void MoveToAttribute(int i)
		{
			this._innerReader.MoveToAttribute(i);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008286 File Offset: 0x00006486
		public override bool MoveToAttribute(string name)
		{
			return this._innerReader.MoveToAttribute(name);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008294 File Offset: 0x00006494
		public override bool MoveToAttribute(string name, string ns)
		{
			return this._innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000082A3 File Offset: 0x000064A3
		public override bool MoveToElement()
		{
			return this._innerReader.MoveToElement();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000082B0 File Offset: 0x000064B0
		public override bool MoveToFirstAttribute()
		{
			return this._innerReader.MoveToFirstAttribute();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000082BD File Offset: 0x000064BD
		public override bool MoveToNextAttribute()
		{
			return this._innerReader.MoveToNextAttribute();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000082CA File Offset: 0x000064CA
		public override bool Read()
		{
			return this._innerReader.Read();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000082D7 File Offset: 0x000064D7
		public override bool ReadAttributeValue()
		{
			return this._innerReader.ReadAttributeValue();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000082E4 File Offset: 0x000064E4
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this._innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000082F4 File Offset: 0x000064F4
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this._innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008304 File Offset: 0x00006504
		public override UniqueId ReadContentAsUniqueId()
		{
			return this._innerReader.ReadContentAsUniqueId();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008311 File Offset: 0x00006511
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this._innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008321 File Offset: 0x00006521
		public override void ResolveEntity()
		{
			this._innerReader.ResolveEntity();
		}

		// Token: 0x0400012F RID: 303
		private XmlDictionaryReader _innerReader;
	}
}
