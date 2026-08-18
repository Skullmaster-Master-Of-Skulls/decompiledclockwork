using System;
using System.CodeDom.Compiler;

namespace System.Xml.Xsl
{
	// Token: 0x020002DF RID: 735
	public sealed class XsltSettings
	{
		// Token: 0x06002C09 RID: 11273 RVA: 0x000E8920 File Offset: 0x000E6B20
		public XsltSettings()
		{
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000E892F File Offset: 0x000E6B2F
		public XsltSettings(bool enableDocumentFunction, bool enableScript)
		{
			this.enableDocumentFunction = enableDocumentFunction;
			this.enableScript = enableScript;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000E894C File Offset: 0x000E6B4C
		public static XsltSettings Default
		{
			get
			{
				return new XsltSettings(false, false);
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000E8955 File Offset: 0x000E6B55
		public static XsltSettings TrustedXslt
		{
			get
			{
				return new XsltSettings(true, true);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000E895E File Offset: 0x000E6B5E
		// (set) Token: 0x06002C0E RID: 11278 RVA: 0x000E8966 File Offset: 0x000E6B66
		public bool EnableDocumentFunction
		{
			get
			{
				return this.enableDocumentFunction;
			}
			set
			{
				this.enableDocumentFunction = value;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000E896F File Offset: 0x000E6B6F
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x000E8977 File Offset: 0x000E6B77
		public bool EnableScript
		{
			get
			{
				return this.enableScript;
			}
			set
			{
				this.enableScript = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000E8980 File Offset: 0x000E6B80
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x000E8988 File Offset: 0x000E6B88
		internal bool CheckOnly
		{
			get
			{
				return this.checkOnly;
			}
			set
			{
				this.checkOnly = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000E8991 File Offset: 0x000E6B91
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x000E8999 File Offset: 0x000E6B99
		internal bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000E89A2 File Offset: 0x000E6BA2
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x000E89AA File Offset: 0x000E6BAA
		internal int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000E89B3 File Offset: 0x000E6BB3
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x000E89BB File Offset: 0x000E6BBB
		internal bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000E89C4 File Offset: 0x000E6BC4
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000E89CC File Offset: 0x000E6BCC
		internal TempFileCollection TempFiles
		{
			get
			{
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		// Token: 0x0400133B RID: 4923
		private bool enableDocumentFunction;

		// Token: 0x0400133C RID: 4924
		private bool enableScript;

		// Token: 0x0400133D RID: 4925
		private bool checkOnly;

		// Token: 0x0400133E RID: 4926
		private bool includeDebugInformation;

		// Token: 0x0400133F RID: 4927
		private int warningLevel = -1;

		// Token: 0x04001340 RID: 4928
		private bool treatWarningsAsErrors;

		// Token: 0x04001341 RID: 4929
		private TempFileCollection tempFiles;
	}
}
