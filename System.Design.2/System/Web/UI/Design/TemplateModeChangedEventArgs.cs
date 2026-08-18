using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000073 RID: 115
	public class TemplateModeChangedEventArgs : EventArgs
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00012157 File Offset: 0x00010357
		public TemplateModeChangedEventArgs(TemplateGroup newTemplateGroup)
		{
			this._newTemplateGroup = newTemplateGroup;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00012166 File Offset: 0x00010366
		public TemplateGroup NewTemplateGroup
		{
			get
			{
				return this._newTemplateGroup;
			}
		}

		// Token: 0x04000194 RID: 404
		private TemplateGroup _newTemplateGroup;
	}
}
