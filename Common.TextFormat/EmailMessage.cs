using System;
using System.Collections.Generic;

namespace TechnoPro.Common.TextFormat.Adapters
{
	// Token: 0x02000005 RID: 5
	public class EmailMessage
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000038B1 File Offset: 0x00001AB1
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000038B9 File Offset: 0x00001AB9
		public string To { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000038C2 File Offset: 0x00001AC2
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000038CA File Offset: 0x00001ACA
		public string From { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000038D3 File Offset: 0x00001AD3
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000038DB File Offset: 0x00001ADB
		public string Cc { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000038EC File Offset: 0x00001AEC
		public string Bcc { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000038F5 File Offset: 0x00001AF5
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000038FD File Offset: 0x00001AFD
		public IList<string> Attachments { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003906 File Offset: 0x00001B06
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000390E File Offset: 0x00001B0E
		public string Subject { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003917 File Offset: 0x00001B17
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000391F File Offset: 0x00001B1F
		public string Body { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003928 File Offset: 0x00001B28
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00003930 File Offset: 0x00001B30
		public string BodyHtml { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003939 File Offset: 0x00001B39
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00003941 File Offset: 0x00001B41
		public int BodyType { get; set; }
	}
}
