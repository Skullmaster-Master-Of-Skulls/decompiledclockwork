using System;
using System.Runtime.Serialization;

namespace WebGrease
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	internal class BuildWorkflowException : WorkflowException
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00044F21 File Offset: 0x00043121
		public BuildWorkflowException()
		{
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00044F29 File Offset: 0x00043129
		public BuildWorkflowException(string message) : base(message)
		{
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00044F32 File Offset: 0x00043132
		public BuildWorkflowException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00044F3C File Offset: 0x0004313C
		public BuildWorkflowException(string message, string subcategory, string errorCode, string helpKeyword, string file, int lineNumber, int columnNumber, int endLineNumber, int endColumnNumber, Exception inner) : base(message, inner)
		{
			this.HasDetailedError = true;
			this.Subcategory = subcategory;
			this.ErrorCode = errorCode;
			this.HelpKeyword = helpKeyword;
			this.File = file;
			this.LineNumber = lineNumber;
			this.ColumnNumber = columnNumber;
			this.EndLineNumber = endLineNumber;
			this.EndColumnNumber = endColumnNumber;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00044F97 File Offset: 0x00043197
		protected BuildWorkflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00044FA1 File Offset: 0x000431A1
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00044FA9 File Offset: 0x000431A9
		public bool HasDetailedError { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00044FB2 File Offset: 0x000431B2
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00044FBA File Offset: 0x000431BA
		public string Subcategory { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00044FC3 File Offset: 0x000431C3
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00044FCB File Offset: 0x000431CB
		public string ErrorCode { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00044FD4 File Offset: 0x000431D4
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x00044FDC File Offset: 0x000431DC
		public string HelpKeyword { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00044FE5 File Offset: 0x000431E5
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00044FED File Offset: 0x000431ED
		public string File { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00044FF6 File Offset: 0x000431F6
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00044FFE File Offset: 0x000431FE
		public int LineNumber { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00045007 File Offset: 0x00043207
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0004500F File Offset: 0x0004320F
		public int ColumnNumber { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00045018 File Offset: 0x00043218
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x00045020 File Offset: 0x00043220
		public int EndLineNumber { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00045029 File Offset: 0x00043229
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00045031 File Offset: 0x00043231
		public int EndColumnNumber { get; set; }

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0004503C File Offset: 0x0004323C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("HasDetailedError", this.HasDetailedError);
			info.AddValue("Subcategory", this.Subcategory);
			info.AddValue("ErrorCode", this.ErrorCode);
			info.AddValue("HelpKeyword", this.HelpKeyword);
			info.AddValue("File", this.File);
			info.AddValue("LineNumber", this.LineNumber);
			info.AddValue("ColumnNumber", this.ColumnNumber);
			info.AddValue("EndLineNumber", this.EndLineNumber);
			info.AddValue("EndColumnNumber", this.EndColumnNumber);
			base.GetObjectData(info, context);
		}
	}
}
