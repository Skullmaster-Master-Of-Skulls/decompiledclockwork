using System;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x02000011 RID: 17
	internal class ObxmlDocHeader : ObxmlStateObject
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003510 File Offset: 0x00001710
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003518 File Offset: 0x00001718
		internal int Flags { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000352C File Offset: 0x0000172C
		internal string Version { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003538 File Offset: 0x00001738
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003540 File Offset: 0x00001740
		internal string Standalone { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000354C File Offset: 0x0000174C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003554 File Offset: 0x00001754
		internal string Encoding { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003560 File Offset: 0x00001760
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003568 File Offset: 0x00001768
		internal bool XmlDecl { get; set; }

		// Token: 0x0600009D RID: 157 RVA: 0x00003574 File Offset: 0x00001774
		internal ObxmlDocHeader()
		{
			this.ClearStateObject();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003584 File Offset: 0x00001784
		internal bool IsHeaderSubVersion1
		{
			get
			{
				return (this.Flags & ObxmlInstructionFormat.VERSION_MASK) > 0;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003598 File Offset: 0x00001798
		internal string GetHeaderString()
		{
			string str = string.Empty;
			if (this.IsHeaderSubVersion1)
			{
				str = ObxmlDocHeader.sHeaderVersion1;
			}
			else
			{
				str = ObxmlDocHeader.sHeaderVersion;
			}
			if (ConfigBaseClass.m_XMLTypeUseHeaderEncodingFromServer && !string.IsNullOrEmpty(this.Encoding))
			{
				str = str + " encoding=\"" + this.Encoding + "\"";
			}
			else
			{
				str += " encoding=\"UTF-16\"";
			}
			if (!string.IsNullOrEmpty(this.Standalone))
			{
				str += " standalone='yes'";
			}
			return str + ObxmlDocHeader.sHeaderTagEnd;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003620 File Offset: 0x00001820
		internal override void ClearStateObject()
		{
			this.Version = null;
			this.Standalone = null;
			this.Encoding = null;
			this.XmlDecl = false;
		}

		// Token: 0x0400008B RID: 139
		internal static readonly string sHeaderVersion = "<?xml version=\"1.0\"";

		// Token: 0x0400008C RID: 140
		internal static readonly string sHeaderTagEnd = "?>\n";

		// Token: 0x0400008D RID: 141
		internal static readonly string sHeaderVersion1 = "<?xml version=\"1.1\"";
	}
}
