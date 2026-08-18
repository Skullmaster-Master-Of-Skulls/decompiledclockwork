using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000125 RID: 293
	public class ClientExportManagerPdfSettings
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x0002D0AD File Offset: 0x0002B2AD
		public ClientExportManagerPdfSettings()
		{
			this.PaperSize = "auto";
			this.Date = DateTime.Now;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002D0CB File Offset: 0x0002B2CB
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0002D0D3 File Offset: 0x0002B2D3
		public string FileName { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		public string ProxyURL { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002D0ED File Offset: 0x0002B2ED
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002D0F5 File Offset: 0x0002B2F5
		public string PaperSize { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002D0FE File Offset: 0x0002B2FE
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0002D106 File Offset: 0x0002B306
		public bool Landscape { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002D10F File Offset: 0x0002B30F
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0002D117 File Offset: 0x0002B317
		public string MarginTop { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002D120 File Offset: 0x0002B320
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0002D128 File Offset: 0x0002B328
		public string MarginBottom { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0002D131 File Offset: 0x0002B331
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0002D139 File Offset: 0x0002B339
		public string MarginLeft { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0002D142 File Offset: 0x0002B342
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0002D14A File Offset: 0x0002B34A
		public string MarginRight { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002D153 File Offset: 0x0002B353
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0002D15B File Offset: 0x0002B35B
		public string Title { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002D164 File Offset: 0x0002B364
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0002D16C File Offset: 0x0002B36C
		public string Author { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002D175 File Offset: 0x0002B375
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0002D17D File Offset: 0x0002B37D
		public string Subject { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002D186 File Offset: 0x0002B386
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0002D18E File Offset: 0x0002B38E
		public string Keywords { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002D197 File Offset: 0x0002B397
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0002D19F File Offset: 0x0002B39F
		public string Creator { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		public string PageBreakSelector { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0002D1B9 File Offset: 0x0002B3B9
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0002D1C1 File Offset: 0x0002B3C1
		public DateTime Date { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002D1CA File Offset: 0x0002B3CA
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0002D1E5 File Offset: 0x0002B3E5
		public Dictionary<string, string> Fonts
		{
			get
			{
				if (this._fonts == null)
				{
					this._fonts = new Dictionary<string, string>();
				}
				return this._fonts;
			}
			set
			{
				this._fonts = value;
			}
		}

		// Token: 0x040002F1 RID: 753
		private Dictionary<string, string> _fonts;
	}
}
