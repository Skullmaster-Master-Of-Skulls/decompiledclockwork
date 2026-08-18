using System;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class RegisteredField
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00012646 File Offset: 0x00010846
		public RegisteredField()
		{
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00012659 File Offset: 0x00010859
		public RegisteredField(string name, Control control)
		{
			this._name = name;
			this._control = control;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001267A File Offset: 0x0001087A
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00012682 File Offset: 0x00010882
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001268B File Offset: 0x0001088B
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00012693 File Offset: 0x00010893
		public Control Control
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
			}
		}

		// Token: 0x040002F7 RID: 759
		private string _name = string.Empty;

		// Token: 0x040002F8 RID: 760
		[NonSerialized]
		private Control _control;
	}
}
