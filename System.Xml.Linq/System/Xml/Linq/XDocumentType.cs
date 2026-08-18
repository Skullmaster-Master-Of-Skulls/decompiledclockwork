using System;

namespace System.Xml.Linq
{
	// Token: 0x02000029 RID: 41
	[__DynamicallyInvokable]
	public class XDocumentType : XNode
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000089D1 File Offset: 0x00006BD1
		[__DynamicallyInvokable]
		public XDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			this.name = XmlConvert.VerifyName(name);
			this.publicId = publicId;
			this.systemId = systemId;
			this.internalSubset = internalSubset;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000089FC File Offset: 0x00006BFC
		[__DynamicallyInvokable]
		public XDocumentType(XDocumentType other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.publicId = other.publicId;
			this.systemId = other.systemId;
			this.internalSubset = other.internalSubset;
			this.dtdInfo = other.dtdInfo;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008A5C File Offset: 0x00006C5C
		internal XDocumentType(XmlReader r)
		{
			this.name = r.Name;
			this.publicId = r.GetAttribute("PUBLIC");
			this.systemId = r.GetAttribute("SYSTEM");
			this.internalSubset = r.Value;
			this.dtdInfo = r.DtdInfo;
			r.Read();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008ABC File Offset: 0x00006CBC
		internal XDocumentType(string name, string publicId, string systemId, string internalSubset, IDtdInfo dtdInfo) : this(name, publicId, systemId, internalSubset)
		{
			this.dtdInfo = dtdInfo;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008AD1 File Offset: 0x00006CD1
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00008ADC File Offset: 0x00006CDC
		[__DynamicallyInvokable]
		public string InternalSubset
		{
			[__DynamicallyInvokable]
			get
			{
				return this.internalSubset;
			}
			[__DynamicallyInvokable]
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.internalSubset = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008B0D File Offset: 0x00006D0D
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00008B18 File Offset: 0x00006D18
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				value = XmlConvert.VerifyName(value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Name);
				this.name = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00008B51 File Offset: 0x00006D51
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008B55 File Offset: 0x00006D55
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00008B60 File Offset: 0x00006D60
		[__DynamicallyInvokable]
		public string PublicId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.publicId;
			}
			[__DynamicallyInvokable]
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.publicId = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008B91 File Offset: 0x00006D91
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008B9C File Offset: 0x00006D9C
		[__DynamicallyInvokable]
		public string SystemId
		{
			[__DynamicallyInvokable]
			get
			{
				return this.systemId;
			}
			[__DynamicallyInvokable]
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.systemId = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008BCD File Offset: 0x00006DCD
		internal IDtdInfo DtdInfo
		{
			get
			{
				return this.dtdInfo;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008BD5 File Offset: 0x00006DD5
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008C03 File Offset: 0x00006E03
		internal override XNode CloneNode()
		{
			return new XDocumentType(this);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008C0C File Offset: 0x00006E0C
		internal override bool DeepEquals(XNode node)
		{
			XDocumentType xdocumentType = node as XDocumentType;
			return xdocumentType != null && this.name == xdocumentType.name && this.publicId == xdocumentType.publicId && this.systemId == xdocumentType.SystemId && this.internalSubset == xdocumentType.internalSubset;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008C70 File Offset: 0x00006E70
		internal override int GetDeepHashCode()
		{
			return this.name.GetHashCode() ^ ((this.publicId != null) ? this.publicId.GetHashCode() : 0) ^ ((this.systemId != null) ? this.systemId.GetHashCode() : 0) ^ ((this.internalSubset != null) ? this.internalSubset.GetHashCode() : 0);
		}

		// Token: 0x040000A8 RID: 168
		private string name;

		// Token: 0x040000A9 RID: 169
		private string publicId;

		// Token: 0x040000AA RID: 170
		private string systemId;

		// Token: 0x040000AB RID: 171
		private string internalSubset;

		// Token: 0x040000AC RID: 172
		private IDtdInfo dtdInfo;
	}
}
