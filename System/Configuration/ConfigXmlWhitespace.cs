using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F6 RID: 1782
	internal sealed class ConfigXmlWhitespace : XmlWhitespace, IConfigErrorInfo
	{
		// Token: 0x0600370B RID: 14091 RVA: 0x000EA30C File Offset: 0x000E930C
		public ConfigXmlWhitespace(string filename, int line, string comment, XmlDocument doc) : base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000EA325 File Offset: 0x000E9325
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x000EA32D File Offset: 0x000E932D
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000EA338 File Offset: 0x000E9338
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlWhitespace configXmlWhitespace = xmlNode as ConfigXmlWhitespace;
			if (configXmlWhitespace != null)
			{
				configXmlWhitespace._line = this._line;
				configXmlWhitespace._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x040031BF RID: 12735
		private int _line;

		// Token: 0x040031C0 RID: 12736
		private string _filename;
	}
}
