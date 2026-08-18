using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000012 RID: 18
	internal class ExtensionFile : ExternalFile
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003A24 File Offset: 0x00001C24
		public ExtensionFile()
		{
			this.m_Name = string.Empty;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A37 File Offset: 0x00001C37
		public ExtensionFile(string name, string fileName, byte[] content) : base(fileName)
		{
			this.Name = name;
			this.m_ContentBuffer = content;
			base.IsExistingFile = false;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003A55 File Offset: 0x00001C55
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003A5D File Offset: 0x00001C5D
		[XmlIgnore]
		public byte[] ContentBuffer
		{
			get
			{
				return this.m_ContentBuffer;
			}
			set
			{
				this.m_ContentBuffer = value;
				base.ErrorInLoading = null;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003A6D File Offset: 0x00001C6D
		internal bool IsBufferValid
		{
			get
			{
				return this.m_ContentBuffer != null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003A78 File Offset: 0x00001C78
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003A80 File Offset: 0x00001C80
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Name = value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003A97 File Offset: 0x00001C97
		internal void CleanUpContent()
		{
			base.ErrorInLoading = null;
			this.m_ContentBuffer = null;
		}

		// Token: 0x04000040 RID: 64
		private string m_Name;

		// Token: 0x04000041 RID: 65
		private byte[] m_ContentBuffer;
	}
}
