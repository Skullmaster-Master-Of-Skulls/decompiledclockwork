using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000D1 RID: 209
	[__DynamicallyInvokable]
	public class XmlParserContext
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x000201CC File Offset: 0x0001E3CC
		[__DynamicallyInvokable]
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace) : this(nt, nsMgr, null, null, null, null, string.Empty, xmlLang, xmlSpace)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000201F0 File Offset: 0x0001E3F0
		[__DynamicallyInvokable]
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace, Encoding enc) : this(nt, nsMgr, null, null, null, null, string.Empty, xmlLang, xmlSpace, enc)
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00020214 File Offset: 0x0001E414
		[__DynamicallyInvokable]
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace) : this(nt, nsMgr, docTypeName, pubId, sysId, internalSubset, baseURI, xmlLang, xmlSpace, null)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00020238 File Offset: 0x0001E438
		[__DynamicallyInvokable]
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc)
		{
			if (nsMgr != null)
			{
				if (nt == null)
				{
					this._nt = nsMgr.NameTable;
				}
				else
				{
					if (nt != nsMgr.NameTable)
					{
						throw new XmlException("Xml_NotSameNametable", string.Empty);
					}
					this._nt = nt;
				}
			}
			else
			{
				this._nt = nt;
			}
			this._nsMgr = nsMgr;
			this._docTypeName = ((docTypeName == null) ? string.Empty : docTypeName);
			this._pubId = ((pubId == null) ? string.Empty : pubId);
			this._sysId = ((sysId == null) ? string.Empty : sysId);
			this._internalSubset = ((internalSubset == null) ? string.Empty : internalSubset);
			this._baseURI = ((baseURI == null) ? string.Empty : baseURI);
			this._xmlLang = ((xmlLang == null) ? string.Empty : xmlLang);
			this._xmlSpace = xmlSpace;
			this._encoding = enc;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00020351 File Offset: 0x0001E551
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x00020359 File Offset: 0x0001E559
		[__DynamicallyInvokable]
		public XmlNameTable NameTable
		{
			[__DynamicallyInvokable]
			get
			{
				return this._nt;
			}
			[__DynamicallyInvokable]
			set
			{
				this._nt = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00020362 File Offset: 0x0001E562
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0002036A File Offset: 0x0001E56A
		[__DynamicallyInvokable]
		public XmlNamespaceManager NamespaceManager
		{
			[__DynamicallyInvokable]
			get
			{
				return this._nsMgr;
			}
			[__DynamicallyInvokable]
			set
			{
				this._nsMgr = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00020373 File Offset: 0x0001E573
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0002037B File Offset: 0x0001E57B
		[__DynamicallyInvokable]
		public string DocTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._docTypeName;
			}
			[__DynamicallyInvokable]
			set
			{
				this._docTypeName = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0002038E File Offset: 0x0001E58E
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00020396 File Offset: 0x0001E596
		[__DynamicallyInvokable]
		public string PublicId
		{
			[__DynamicallyInvokable]
			get
			{
				return this._pubId;
			}
			[__DynamicallyInvokable]
			set
			{
				this._pubId = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x000203A9 File Offset: 0x0001E5A9
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x000203B1 File Offset: 0x0001E5B1
		[__DynamicallyInvokable]
		public string SystemId
		{
			[__DynamicallyInvokable]
			get
			{
				return this._sysId;
			}
			[__DynamicallyInvokable]
			set
			{
				this._sysId = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000203C4 File Offset: 0x0001E5C4
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x000203CC File Offset: 0x0001E5CC
		[__DynamicallyInvokable]
		public string BaseURI
		{
			[__DynamicallyInvokable]
			get
			{
				return this._baseURI;
			}
			[__DynamicallyInvokable]
			set
			{
				this._baseURI = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x000203DF File Offset: 0x0001E5DF
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x000203E7 File Offset: 0x0001E5E7
		[__DynamicallyInvokable]
		public string InternalSubset
		{
			[__DynamicallyInvokable]
			get
			{
				return this._internalSubset;
			}
			[__DynamicallyInvokable]
			set
			{
				this._internalSubset = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x000203FA File Offset: 0x0001E5FA
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00020402 File Offset: 0x0001E602
		[__DynamicallyInvokable]
		public string XmlLang
		{
			[__DynamicallyInvokable]
			get
			{
				return this._xmlLang;
			}
			[__DynamicallyInvokable]
			set
			{
				this._xmlLang = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00020415 File Offset: 0x0001E615
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0002041D File Offset: 0x0001E61D
		[__DynamicallyInvokable]
		public XmlSpace XmlSpace
		{
			[__DynamicallyInvokable]
			get
			{
				return this._xmlSpace;
			}
			[__DynamicallyInvokable]
			set
			{
				this._xmlSpace = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00020426 File Offset: 0x0001E626
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0002042E File Offset: 0x0001E62E
		[__DynamicallyInvokable]
		public Encoding Encoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this._encoding;
			}
			[__DynamicallyInvokable]
			set
			{
				this._encoding = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00020437 File Offset: 0x0001E637
		internal bool HasDtdInfo
		{
			get
			{
				return this._internalSubset != string.Empty || this._pubId != string.Empty || this._sysId != string.Empty;
			}
		}

		// Token: 0x04000328 RID: 808
		private XmlNameTable _nt;

		// Token: 0x04000329 RID: 809
		private XmlNamespaceManager _nsMgr;

		// Token: 0x0400032A RID: 810
		private string _docTypeName = string.Empty;

		// Token: 0x0400032B RID: 811
		private string _pubId = string.Empty;

		// Token: 0x0400032C RID: 812
		private string _sysId = string.Empty;

		// Token: 0x0400032D RID: 813
		private string _internalSubset = string.Empty;

		// Token: 0x0400032E RID: 814
		private string _xmlLang = string.Empty;

		// Token: 0x0400032F RID: 815
		private XmlSpace _xmlSpace;

		// Token: 0x04000330 RID: 816
		private string _baseURI = string.Empty;

		// Token: 0x04000331 RID: 817
		private Encoding _encoding;
	}
}
