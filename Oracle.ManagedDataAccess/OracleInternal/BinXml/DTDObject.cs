using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000013 RID: 19
	internal class DTDObject : ObxmlStateObject
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003660 File Offset: 0x00001860
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003668 File Offset: 0x00001868
		internal DTDObjectTypes DTDObjectType { get; set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003674 File Offset: 0x00001874
		internal DTDObject()
		{
			this.ClearStateObject();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003684 File Offset: 0x00001884
		internal DTDObject(DTDObjectTypes objectType)
		{
			this.DTDObjectType = objectType;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003694 File Offset: 0x00001894
		internal DTDObject(DTDObjectTypes objectType, string objectName)
		{
			this.DTDObjectType = objectType;
			this.ObjectName = objectName;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000036B4 File Offset: 0x000018B4
		internal string ObjectName { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000036C0 File Offset: 0x000018C0
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000036C8 File Offset: 0x000018C8
		internal string PublicId { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000036D4 File Offset: 0x000018D4
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000036DC File Offset: 0x000018DC
		internal string SystemId { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000036E8 File Offset: 0x000018E8
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000036F0 File Offset: 0x000018F0
		internal string ObjectValue
		{
			get
			{
				return this.m_objectValue;
			}
			set
			{
				this.m_objectValue = value.ReplaceXmlChars();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003700 File Offset: 0x00001900
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003708 File Offset: 0x00001908
		internal string Note { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003714 File Offset: 0x00001914
		internal bool IsEntity
		{
			get
			{
				return this.DTDObjectType == DTDObjectTypes.Entity || this.DTDObjectType == DTDObjectTypes.PartialEntity;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000372C File Offset: 0x0000192C
		internal bool IsParsedEntity
		{
			get
			{
				return this.IsEntity && !string.IsNullOrEmpty(this.ObjectValue);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003748 File Offset: 0x00001948
		internal bool IsUnparsedEntity
		{
			get
			{
				return this.IsEntity && !this.IsParsedEntity && !string.IsNullOrEmpty(this.Note);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000376C File Offset: 0x0000196C
		internal bool IsExternalEntity
		{
			get
			{
				return this.IsEntity && (!this.IsParsedEntity && !this.IsUnparsedEntity) && !string.IsNullOrEmpty(this.Note);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003798 File Offset: 0x00001998
		internal override void ClearStateObject()
		{
			this.DTDObjectType = DTDObjectTypes.DTD;
		}

		// Token: 0x04000099 RID: 153
		private string m_objectValue;
	}
}
