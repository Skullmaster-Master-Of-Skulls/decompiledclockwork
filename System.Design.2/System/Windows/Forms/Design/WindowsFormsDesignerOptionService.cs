using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200036E RID: 878
	public class WindowsFormsDesignerOptionService : DesignerOptionService
	{
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000E04A0 File Offset: 0x000DE6A0
		public virtual DesignerOptions CompatibilityOptions
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptions();
				}
				return this._options;
			}
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000E04BC File Offset: 0x000DE6BC
		protected override void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
			if (options.Parent == null)
			{
				DesignerOptions compatibilityOptions = this.CompatibilityOptions;
				if (compatibilityOptions != null)
				{
					base.CreateOptionCollection(options, "DesignerOptions", compatibilityOptions);
				}
			}
		}

		// Token: 0x04001A47 RID: 6727
		private DesignerOptions _options;
	}
}
