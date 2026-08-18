using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Telerik.Web.UI.FileExplorer
{
	// Token: 0x02000EBF RID: 3775
	public class FileExplorerShortcut : StateManager
	{
		// Token: 0x17002D9F RID: 11679
		// (get) Token: 0x06009014 RID: 36884 RVA: 0x002070F0 File Offset: 0x002052F0
		// (set) Token: 0x06009015 RID: 36885 RVA: 0x00207110 File Offset: 0x00205310
		[Description("Gets or sets the shortcut used to bring the focus to the FileExplorer control.")]
		[DefaultValue("")]
		[Category("Accessibility")]
		public string FocusFileExplorer
		{
			get
			{
				return ((string)base.ViewState["FocusFileExplorer"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusFileExplorer"] = value;
			}
		}

		// Token: 0x17002DA0 RID: 11680
		// (get) Token: 0x06009016 RID: 36886 RVA: 0x00207123 File Offset: 0x00205323
		// (set) Token: 0x06009017 RID: 36887 RVA: 0x00207143 File Offset: 0x00205343
		[DefaultValue("")]
		[Category("Accessibility")]
		[Description("Gets or sets the keyboard shortcut used to bring the focus to the TreeView of the FileExplorer control.")]
		public string FocusTreeView
		{
			get
			{
				return ((string)base.ViewState["FocusTreeView"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusTreeView"] = value;
			}
		}

		// Token: 0x17002DA1 RID: 11681
		// (get) Token: 0x06009018 RID: 36888 RVA: 0x00207156 File Offset: 0x00205356
		// (set) Token: 0x06009019 RID: 36889 RVA: 0x00207176 File Offset: 0x00205376
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to bring the focus to the ToolBar of the FileExplorer control.")]
		public string FocusToolBar
		{
			get
			{
				return ((string)base.ViewState["FocusToolBar"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusToolBar"] = value;
			}
		}

		// Token: 0x17002DA2 RID: 11682
		// (get) Token: 0x0600901A RID: 36890 RVA: 0x00207189 File Offset: 0x00205389
		// (set) Token: 0x0600901B RID: 36891 RVA: 0x002071A9 File Offset: 0x002053A9
		[Description("Gets or sets the keyboard shortcut used to bring the focus to the Grid of the FileExplorer control.")]
		[DefaultValue("")]
		[Category("Accessibility")]
		public string FocusGrid
		{
			get
			{
				return ((string)base.ViewState["FocusGrid"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusGrid"] = value;
			}
		}

		// Token: 0x17002DA3 RID: 11683
		// (get) Token: 0x0600901C RID: 36892 RVA: 0x002071BC File Offset: 0x002053BC
		// (set) Token: 0x0600901D RID: 36893 RVA: 0x002071DC File Offset: 0x002053DC
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to bring the focus to the Address of the FileExplorer control.")]
		[Category("Accessibility")]
		public string FocusAddressBar
		{
			get
			{
				return ((string)base.ViewState["FocusAddressBar"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusAddressBar"] = value;
			}
		}

		// Token: 0x17002DA4 RID: 11684
		// (get) Token: 0x0600901E RID: 36894 RVA: 0x002071EF File Offset: 0x002053EF
		// (set) Token: 0x0600901F RID: 36895 RVA: 0x0020720F File Offset: 0x0020540F
		[Category("Accessibility")]
		[DefaultValue("Esc")]
		[Description("Gets or sets the keyboard shortcut used to close the RadWindow that is opened to view/upload/delete/create a file in the FileExplorer control.")]
		public string PopupWindowClose
		{
			get
			{
				return ((string)base.ViewState["PopupWindowClose"]) ?? "Esc";
			}
			set
			{
				base.ViewState["PopupWindowClose"] = value;
			}
		}

		// Token: 0x17002DA5 RID: 11685
		// (get) Token: 0x06009020 RID: 36896 RVA: 0x00207222 File Offset: 0x00205422
		// (set) Token: 0x06009021 RID: 36897 RVA: 0x00207242 File Offset: 0x00205442
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to bring the focus to the Slider used for paging in the Grid of the FileExplorer control.")]
		public string FocusGridPagingSlider
		{
			get
			{
				return ((string)base.ViewState["FocusGridPagingSlider"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["FocusGridPagingSlider"] = value;
			}
		}

		// Token: 0x17002DA6 RID: 11686
		// (get) Token: 0x06009022 RID: 36898 RVA: 0x00207255 File Offset: 0x00205455
		// (set) Token: 0x06009023 RID: 36899 RVA: 0x00207275 File Offset: 0x00205475
		[DefaultValue("Context")]
		[Category("Accessibility")]
		[Description("Gets or sets the keyboard shortcut used to open the context menu.")]
		public string ContextMenu
		{
			get
			{
				return ((string)base.ViewState["ContextMenu"]) ?? "Context";
			}
			set
			{
				base.ViewState["ContextMenu"] = value;
			}
		}

		// Token: 0x17002DA7 RID: 11687
		// (get) Token: 0x06009024 RID: 36900 RVA: 0x00207288 File Offset: 0x00205488
		// (set) Token: 0x06009025 RID: 36901 RVA: 0x002072A8 File Offset: 0x002054A8
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to navigate one view Back of the FileExplorer control.")]
		public string Back
		{
			get
			{
				return ((string)base.ViewState["Back"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Back"] = value;
			}
		}

		// Token: 0x17002DA8 RID: 11688
		// (get) Token: 0x06009026 RID: 36902 RVA: 0x002072BB File Offset: 0x002054BB
		// (set) Token: 0x06009027 RID: 36903 RVA: 0x002072DB File Offset: 0x002054DB
		[Description("Gets or sets the keyboard shortcut used to navigate one view Forward (if possible) of the FileExplorer control.")]
		[Category("Accessibility")]
		[DefaultValue("")]
		public string Forward
		{
			get
			{
				return ((string)base.ViewState["Forward"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Forward"] = value;
			}
		}

		// Token: 0x17002DA9 RID: 11689
		// (get) Token: 0x06009028 RID: 36904 RVA: 0x002072EE File Offset: 0x002054EE
		// (set) Token: 0x06009029 RID: 36905 RVA: 0x0020730E File Offset: 0x0020550E
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to open the selected file or folder.")]
		public string Open
		{
			get
			{
				return ((string)base.ViewState["Open"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Open"] = value;
			}
		}

		// Token: 0x17002DAA RID: 11690
		// (get) Token: 0x0600902A RID: 36906 RVA: 0x00207321 File Offset: 0x00205521
		// (set) Token: 0x0600902B RID: 36907 RVA: 0x00207341 File Offset: 0x00205541
		[DefaultValue("")]
		[Category("Accessibility")]
		[Description("Gets or sets the keyboard shortcut used to refresh the content of the FileExplorer.")]
		public string Refresh
		{
			get
			{
				return ((string)base.ViewState["Refresh"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Refresh"] = value;
			}
		}

		// Token: 0x17002DAB RID: 11691
		// (get) Token: 0x0600902C RID: 36908 RVA: 0x00207354 File Offset: 0x00205554
		// (set) Token: 0x0600902D RID: 36909 RVA: 0x00207374 File Offset: 0x00205574
		[DefaultValue("")]
		[Category("Accessibility")]
		[Description("Gets or sets the keyboard shortcut used to create new folder in the FileExplorer.")]
		public string NewFolder
		{
			get
			{
				return ((string)base.ViewState["NewFolder"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["NewFolder"] = value;
			}
		}

		// Token: 0x17002DAC RID: 11692
		// (get) Token: 0x0600902E RID: 36910 RVA: 0x00207387 File Offset: 0x00205587
		// (set) Token: 0x0600902F RID: 36911 RVA: 0x002073A7 File Offset: 0x002055A7
		[Category("Accessibility")]
		[DefaultValue("DELETE")]
		[Description("Gets or sets the keyboard shortcut used to delete the currently selected file or folder in the FileExplorer control.")]
		public string Delete
		{
			get
			{
				return ((string)base.ViewState["Delete"]) ?? "DELETE";
			}
			set
			{
				base.ViewState["Delete"] = value;
			}
		}

		// Token: 0x17002DAD RID: 11693
		// (get) Token: 0x06009030 RID: 36912 RVA: 0x002073BA File Offset: 0x002055BA
		// (set) Token: 0x06009031 RID: 36913 RVA: 0x002073DA File Offset: 0x002055DA
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("Gets or sets the keyboard shortcut used to upload a new file to the FileExplorer control.")]
		public string UploadFile
		{
			get
			{
				return ((string)base.ViewState["UploadFile"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["UploadFile"] = value;
			}
		}

		// Token: 0x06009032 RID: 36914 RVA: 0x002073F0 File Offset: 0x002055F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			stringBuilder.Append("[");
			PropertyInfo[] properties = typeof(FileExplorerShortcut).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				string text = propertyInfo.GetValue(this, null).ToString();
				if (!string.IsNullOrEmpty(text) && name != "PopupWindowClose")
				{
					if (flag)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(string.Format("['{0}','{1}']", name, text));
					flag = true;
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
