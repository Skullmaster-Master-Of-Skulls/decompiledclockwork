using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000044 RID: 68
	internal sealed class ConfigXmlComment : XmlComment, IConfigErrorInfo
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00012488 File Offset: 0x00010688
		public ConfigXmlComment(string filename, int line, string comment, XmlDocument doc) : base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000124A1 File Offset: 0x000106A1
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000124A9 File Offset: 0x000106A9
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000124B4 File Offset: 0x000106B4
		public override XmlNode CloneNode(bool deep)
		{
			XmlNode xmlNode = base.CloneNode(deep);
			ConfigXmlComment configXmlComment = xmlNode as ConfigXmlComment;
			if (configXmlComment != null)
			{
				configXmlComment._line = this._line;
				configXmlComment._filename = this._filename;
			}
			return xmlNode;
		}

		// Token: 0x0400022E RID: 558
		private int _line;

		// Token: 0x0400022F RID: 559
		private string _filename;
	}
}
