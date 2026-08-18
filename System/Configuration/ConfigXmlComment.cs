using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F1 RID: 1777
	internal sealed class ConfigXmlComment : XmlComment, IConfigErrorInfo
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x000E9FA8 File Offset: 0x000E8FA8
		public ConfigXmlComment(string filename, int line, string comment, XmlDocument doc) : base(comment, doc)
		{
			this._line = line;
			this._filename = filename;
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000E9FC1 File Offset: 0x000E8FC1
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000E9FC9 File Offset: 0x000E8FC9
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000E9FD4 File Offset: 0x000E8FD4
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

		// Token: 0x040031B4 RID: 12724
		private int _line;

		// Token: 0x040031B5 RID: 12725
		private string _filename;
	}
}
