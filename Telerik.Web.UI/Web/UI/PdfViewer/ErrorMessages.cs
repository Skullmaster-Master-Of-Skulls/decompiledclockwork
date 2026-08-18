using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000660 RID: 1632
	public class ErrorMessages : StateManager, IDefaultCheck
	{
		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000C28DE File Offset: 0x000C0ADE
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000C28FE File Offset: 0x000C0AFE
		[DefaultValue("Only pdf files allowed.")]
		public string NotSupported
		{
			get
			{
				return (string)(base.ViewState["NotSupported"] ?? "Only pdf files allowed.");
			}
			set
			{
				base.ViewState["NotSupported"] = value;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000C2911 File Offset: 0x000C0B11
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x000C2931 File Offset: 0x000C0B31
		[DefaultValue("PDF file fails to process.")]
		public string ParseError
		{
			get
			{
				return (string)(base.ViewState["ParseError"] ?? "PDF file fails to process.");
			}
			set
			{
				base.ViewState["ParseError"] = value;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000C2944 File Offset: 0x000C0B44
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000C2964 File Offset: 0x000C0B64
		[DefaultValue("File is not found.")]
		public string NotFound
		{
			get
			{
				return (string)(base.ViewState["NotFound"] ?? "File is not found.");
			}
			set
			{
				base.ViewState["NotFound"] = value;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x000C2977 File Offset: 0x000C0B77
		public bool IsDefault
		{
			get
			{
				return this.NotSupported == "Only pdf files allowed." && this.ParseError == "PDF file fails to process." && this.NotFound == "File is not found.";
			}
		}
	}
}
