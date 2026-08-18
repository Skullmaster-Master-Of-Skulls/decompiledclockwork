using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.MailMergeTemplates
{
	// Token: 0x020001C6 RID: 454
	public class SnapshotEmailTemplates
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000146E5 File Offset: 0x000128E5
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x000146ED File Offset: 0x000128ED
		public int TemplateId { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000146F6 File Offset: 0x000128F6
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x000146FE File Offset: 0x000128FE
		public int TemplateGroupId { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00014707 File Offset: 0x00012907
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0001470F File Offset: 0x0001290F
		public string TemplateName { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00014718 File Offset: 0x00012918
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00014720 File Offset: 0x00012920
		public string eFrom { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00014729 File Offset: 0x00012929
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00014731 File Offset: 0x00012931
		public string eTo { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001473A File Offset: 0x0001293A
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00014742 File Offset: 0x00012942
		public string eCc { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001474B File Offset: 0x0001294B
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00014753 File Offset: 0x00012953
		public string eBcc { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001475C File Offset: 0x0001295C
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00014764 File Offset: 0x00012964
		public string eAttachments { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001476D File Offset: 0x0001296D
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00014775 File Offset: 0x00012975
		public string eBody { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0001477E File Offset: 0x0001297E
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00014786 File Offset: 0x00012986
		public string eBodyPdf { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0001478F File Offset: 0x0001298F
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00014797 File Offset: 0x00012997
		public string eMisc { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000147A0 File Offset: 0x000129A0
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x000147A8 File Offset: 0x000129A8
		public int eMode { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000147B1 File Offset: 0x000129B1
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x000147B9 File Offset: 0x000129B9
		public string BlankReplacements { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000147C2 File Offset: 0x000129C2
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x000147CA File Offset: 0x000129CA
		public string WarningIfMissingCodes { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x000147D3 File Offset: 0x000129D3
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x000147DB File Offset: 0x000129DB
		public int WhoCreated { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x000147E4 File Offset: 0x000129E4
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x000147EC File Offset: 0x000129EC
		public DateTime DateCreated { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000147F5 File Offset: 0x000129F5
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x000147FD File Offset: 0x000129FD
		public int WhoLastModified { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00014806 File Offset: 0x00012A06
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0001480E File Offset: 0x00012A0E
		public DateTime DateLastModified { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00014817 File Offset: 0x00012A17
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0001481F File Offset: 0x00012A1F
		public bool IsActive { get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00014828 File Offset: 0x00012A28
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00014830 File Offset: 0x00012A30
		public int BodyType { get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00014839 File Offset: 0x00012A39
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00014841 File Offset: 0x00012A41
		public int MessageDeliveryMethod { get; set; }
	}
}
