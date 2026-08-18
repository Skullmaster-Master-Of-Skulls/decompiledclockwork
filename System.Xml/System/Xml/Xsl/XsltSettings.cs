using System;
using System.CodeDom.Compiler;

namespace System.Xml.Xsl
{
	// Token: 0x0200017C RID: 380
	public sealed class XsltSettings
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x00056A8F File Offset: 0x00055A8F
		public XsltSettings()
		{
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00056A9E File Offset: 0x00055A9E
		public XsltSettings(bool enableDocumentFunction, bool enableScript)
		{
			this.enableDocumentFunction = enableDocumentFunction;
			this.enableScript = enableScript;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00056ABB File Offset: 0x00055ABB
		public static XsltSettings Default
		{
			get
			{
				return new XsltSettings(false, false);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00056AC4 File Offset: 0x00055AC4
		public static XsltSettings TrustedXslt
		{
			get
			{
				return new XsltSettings(true, true);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00056ACD File Offset: 0x00055ACD
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00056AD5 File Offset: 0x00055AD5
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00056ADE File Offset: 0x00055ADE
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x00056AE6 File Offset: 0x00055AE6
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

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00056AEF File Offset: 0x00055AEF
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x00056AF7 File Offset: 0x00055AF7
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

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00056B00 File Offset: 0x00055B00
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x00056B08 File Offset: 0x00055B08
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00056B11 File Offset: 0x00055B11
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x00056B19 File Offset: 0x00055B19
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00056B22 File Offset: 0x00055B22
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00056B2A File Offset: 0x00055B2A
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00056B33 File Offset: 0x00055B33
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00056B3B File Offset: 0x00055B3B
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

		// Token: 0x04000C4E RID: 3150
		private bool enableDocumentFunction;

		// Token: 0x04000C4F RID: 3151
		private bool enableScript;

		// Token: 0x04000C50 RID: 3152
		private bool checkOnly;

		// Token: 0x04000C51 RID: 3153
		private bool includeDebugInformation;

		// Token: 0x04000C52 RID: 3154
		private int warningLevel = -1;

		// Token: 0x04000C53 RID: 3155
		private bool treatWarningsAsErrors;

		// Token: 0x04000C54 RID: 3156
		private TempFileCollection tempFiles;
	}
}
