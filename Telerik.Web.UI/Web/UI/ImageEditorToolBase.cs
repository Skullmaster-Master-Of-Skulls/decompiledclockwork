using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000E9D RID: 3741
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ImageEditorToolBase : StateManager
	{
		// Token: 0x06008EA9 RID: 36521 RVA: 0x00202875 File Offset: 0x00200A75
		public ImageEditorToolBase()
		{
		}

		// Token: 0x17002D28 RID: 11560
		// (get) Token: 0x06008EAA RID: 36522 RVA: 0x0020287D File Offset: 0x00200A7D
		// (set) Token: 0x06008EAB RID: 36523 RVA: 0x0020289E File Offset: 0x00200A9E
		[DefaultValue(false)]
		[Category("Appearance")]
		[Description("Gets or sets a bool value that indicates whether the tool is a separator.")]
		public virtual bool IsSeparator
		{
			get
			{
				return (bool)(base.ViewState["IsSeparator"] ?? false);
			}
			set
			{
				base.ViewState["IsSeparator"] = value;
			}
		}

		// Token: 0x06008EAC RID: 36524 RVA: 0x002028B8 File Offset: 0x00200AB8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06008EAD RID: 36525 RVA: 0x002028D8 File Offset: 0x00200AD8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
		}
	}
}
