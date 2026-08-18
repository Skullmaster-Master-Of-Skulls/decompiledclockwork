using System;
using System.Globalization;
using System.IO;
using System.Web.Resources;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000013 RID: 19
	internal class ExternalFile
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public ExternalFile()
		{
			this.m_FileName = string.Empty;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003ABA File Offset: 0x00001CBA
		public ExternalFile(string fileName)
		{
			this.FileName = fileName;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003AC9 File Offset: 0x00001CC9
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003AD1 File Offset: 0x00001CD1
		[XmlIgnore]
		public Exception ErrorInLoading
		{
			get
			{
				return this.m_ErrorInLoading;
			}
			set
			{
				this.m_ErrorInLoading = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003ADA File Offset: 0x00001CDA
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003AE2 File Offset: 0x00001CE2
		[XmlAttribute]
		public string FileName
		{
			get
			{
				return this.m_FileName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!ExternalFile.IsLocalFileName(value))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_InvalidFileName, new object[]
					{
						value
					}));
				}
				this.m_FileName = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B20 File Offset: 0x00001D20
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003B28 File Offset: 0x00001D28
		[XmlIgnore]
		public bool IsExistingFile
		{
			get
			{
				return this.m_IsExistingFile;
			}
			set
			{
				this.m_IsExistingFile = value;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003B34 File Offset: 0x00001D34
		public static bool IsLocalFileName(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && fileName.IndexOfAny(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar,
				Path.VolumeSeparatorChar
			}) < 0;
		}

		// Token: 0x04000042 RID: 66
		private string m_FileName;

		// Token: 0x04000043 RID: 67
		private bool m_IsExistingFile;

		// Token: 0x04000044 RID: 68
		private Exception m_ErrorInLoading;
	}
}
