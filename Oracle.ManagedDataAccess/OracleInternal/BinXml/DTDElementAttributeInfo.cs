using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000015 RID: 21
	internal class DTDElementAttributeInfo : ObxmlStateObject
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000037EC File Offset: 0x000019EC
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000037F4 File Offset: 0x000019F4
		internal string ElementName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003800 File Offset: 0x00001A00
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003808 File Offset: 0x00001A08
		internal string AttributeName { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003814 File Offset: 0x00001A14
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000381C File Offset: 0x00001A1C
		internal bool AttributeStringExpanded { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003828 File Offset: 0x00001A28
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003830 File Offset: 0x00001A30
		internal string AttributeString { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000383C File Offset: 0x00001A3C
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003844 File Offset: 0x00001A44
		internal string AttributeType { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003850 File Offset: 0x00001A50
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003858 File Offset: 0x00001A58
		internal string AttributeMode { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003864 File Offset: 0x00001A64
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000386C File Offset: 0x00001A6C
		internal string AttributeValue
		{
			get
			{
				return this.m_attributeValue;
			}
			set
			{
				this.m_attributeValue = value.ReplaceXmlChars();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000387C File Offset: 0x00001A7C
		internal DTDElementAttributeInfo()
		{
			this.ClearStateObject();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000388C File Offset: 0x00001A8C
		internal DTDElementAttributeInfo(string elementName, string attributeName, string type, string mode, string value)
		{
			this.AttributeStringExpanded = true;
			this.ElementName = elementName;
			this.AttributeName = attributeName;
			this.AttributeType = type;
			this.AttributeMode = mode;
			this.AttributeValue = value;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000038C0 File Offset: 0x00001AC0
		internal void SplitAttrString(string attributeType)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal override void ClearStateObject()
		{
			this.ElementName = null;
			this.AttributeName = null;
			this.AttributeStringExpanded = true;
			this.AttributeString = null;
			this.AttributeType = null;
			this.AttributeMode = null;
			this.AttributeValue = null;
		}

		// Token: 0x040000A1 RID: 161
		private string m_attributeValue;
	}
}
