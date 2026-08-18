using System;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000046 RID: 70
	internal sealed class ConfigXmlReader : XmlTextReader, IConfigErrorInfo
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00012554 File Offset: 0x00010754
		internal ConfigXmlReader(string rawXml, string filename, int lineOffset) : this(rawXml, filename, lineOffset, false)
		{
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00012560 File Offset: 0x00010760
		internal ConfigXmlReader(string rawXml, string filename, int lineOffset, bool lineNumberIsConstant) : base(new StringReader(rawXml))
		{
			this._rawXml = rawXml;
			this._filename = filename;
			this._lineOffset = lineOffset;
			this._lineNumberIsConstant = lineNumberIsConstant;
			base.DtdProcessing = DtdProcessing.Ignore;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00012592 File Offset: 0x00010792
		internal ConfigXmlReader Clone()
		{
			return new ConfigXmlReader(this._rawXml, this._filename, this._lineOffset, this._lineNumberIsConstant);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000125B1 File Offset: 0x000107B1
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				if (this._lineNumberIsConstant)
				{
					return this._lineOffset;
				}
				if (this._lineOffset > 0)
				{
					return base.LineNumber + (this._lineOffset - 1);
				}
				return base.LineNumber;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000125E1 File Offset: 0x000107E1
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000125E9 File Offset: 0x000107E9
		internal string RawXml
		{
			get
			{
				return this._rawXml;
			}
		}

		// Token: 0x04000232 RID: 562
		private string _rawXml;

		// Token: 0x04000233 RID: 563
		private int _lineOffset;

		// Token: 0x04000234 RID: 564
		private string _filename;

		// Token: 0x04000235 RID: 565
		private bool _lineNumberIsConstant;
	}
}
