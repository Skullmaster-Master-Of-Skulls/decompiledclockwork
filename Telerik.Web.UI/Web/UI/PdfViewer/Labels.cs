using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000666 RID: 1638
	public class Labels : StateManager, IDefaultCheck
	{
		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000C2E1E File Offset: 0x000C101E
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000C2E3E File Offset: 0x000C103E
		[DefaultValue("File name")]
		public string FileName
		{
			get
			{
				return (string)(base.ViewState["FileName"] ?? "File name");
			}
			set
			{
				base.ViewState["FileName"] = value;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000C2E51 File Offset: 0x000C1051
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x000C2E71 File Offset: 0x000C1071
		[DefaultValue("Save as")]
		public string SaveAsType
		{
			get
			{
				return (string)(base.ViewState["SaveAsType"] ?? "Save as");
			}
			set
			{
				base.ViewState["SaveAsType"] = value;
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000C2E84 File Offset: 0x000C1084
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x000C2EA4 File Offset: 0x000C10A4
		[DefaultValue("Page")]
		public string Page
		{
			get
			{
				return (string)(base.ViewState["Page"] ?? "Page");
			}
			set
			{
				base.ViewState["Page"] = value;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000C2EB7 File Offset: 0x000C10B7
		public bool IsDefault
		{
			get
			{
				return this.FileName == "File name" && this.SaveAsType == "Save as" && this.Page == "Page";
			}
		}
	}
}
