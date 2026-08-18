using System;
using System.Configuration.Internal;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000079 RID: 121
	internal class PropertySourceInfo
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x000193DF File Offset: 0x000175DF
		internal PropertySourceInfo(XmlReader reader)
		{
			this._fileName = this.GetFilename(reader);
			this._lineNumber = this.GetLineNumber(reader);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00019404 File Offset: 0x00017604
		internal string FileName
		{
			get
			{
				string text = this._fileName;
				try
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				catch (SecurityException)
				{
					text = Path.GetFileName(this._fileName);
					if (text == null)
					{
						text = string.Empty;
					}
				}
				return text;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00019450 File Offset: 0x00017650
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00019458 File Offset: 0x00017658
		private string GetFilename(XmlReader reader)
		{
			IConfigErrorInfo configErrorInfo = reader as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.Filename;
			}
			return "";
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001947C File Offset: 0x0001767C
		private int GetLineNumber(XmlReader reader)
		{
			IConfigErrorInfo configErrorInfo = reader as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.LineNumber;
			}
			return 0;
		}

		// Token: 0x040002C8 RID: 712
		private string _fileName;

		// Token: 0x040002C9 RID: 713
		private int _lineNumber;
	}
}
