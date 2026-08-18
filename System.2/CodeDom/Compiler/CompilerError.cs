using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000675 RID: 1653
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerError
	{
		// Token: 0x06003CCC RID: 15564 RVA: 0x000FAFC7 File Offset: 0x000F91C7
		public CompilerError()
		{
			this.line = 0;
			this.column = 0;
			this.errorNumber = string.Empty;
			this.errorText = string.Empty;
			this.fileName = string.Empty;
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000FAFFE File Offset: 0x000F91FE
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.line = line;
			this.column = column;
			this.errorNumber = errorNumber;
			this.errorText = errorText;
			this.fileName = fileName;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000FB02B File Offset: 0x000F922B
		// (set) Token: 0x06003CCF RID: 15567 RVA: 0x000FB033 File Offset: 0x000F9233
		public int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000FB03C File Offset: 0x000F923C
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000FB044 File Offset: 0x000F9244
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000FB04D File Offset: 0x000F924D
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x000FB055 File Offset: 0x000F9255
		public string ErrorNumber
		{
			get
			{
				return this.errorNumber;
			}
			set
			{
				this.errorNumber = value;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000FB05E File Offset: 0x000F925E
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x000FB066 File Offset: 0x000F9266
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000FB06F File Offset: 0x000F926F
		// (set) Token: 0x06003CD7 RID: 15575 RVA: 0x000FB077 File Offset: 0x000F9277
		public bool IsWarning
		{
			get
			{
				return this.warning;
			}
			set
			{
				this.warning = value;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x000FB080 File Offset: 0x000F9280
		// (set) Token: 0x06003CD9 RID: 15577 RVA: 0x000FB088 File Offset: 0x000F9288
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000FB094 File Offset: 0x000F9294
		public override string ToString()
		{
			if (this.FileName.Length > 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[]
				{
					this.FileName,
					this.Line,
					this.Column,
					this.IsWarning ? "warning" : "error",
					this.ErrorNumber,
					this.ErrorText
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", new object[]
			{
				this.IsWarning ? "warning" : "error",
				this.ErrorNumber,
				this.ErrorText
			});
		}

		// Token: 0x04002C78 RID: 11384
		private int line;

		// Token: 0x04002C79 RID: 11385
		private int column;

		// Token: 0x04002C7A RID: 11386
		private string errorNumber;

		// Token: 0x04002C7B RID: 11387
		private bool warning;

		// Token: 0x04002C7C RID: 11388
		private string errorText;

		// Token: 0x04002C7D RID: 11389
		private string fileName;
	}
}
