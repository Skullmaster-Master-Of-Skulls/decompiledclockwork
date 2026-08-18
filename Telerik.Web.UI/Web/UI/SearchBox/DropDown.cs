using System;
using System.Web.UI;

namespace Telerik.Web.UI.SearchBox
{
	// Token: 0x02000EEF RID: 3823
	internal class DropDown : IDisposable
	{
		// Token: 0x060090DC RID: 37084 RVA: 0x00209FC7 File Offset: 0x002081C7
		public DropDown(StateBag viewState)
		{
			this._viewState = viewState;
		}

		// Token: 0x060090DD RID: 37085 RVA: 0x00209FD6 File Offset: 0x002081D6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060090DE RID: 37086 RVA: 0x00209FE5 File Offset: 0x002081E5
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._dropDownSettings != null)
			{
				this._dropDownSettings.Dispose();
			}
		}

		// Token: 0x17002DE0 RID: 11744
		// (get) Token: 0x060090DF RID: 37087 RVA: 0x00209FFD File Offset: 0x002081FD
		internal DropDownSettings DropDownSettings
		{
			get
			{
				if (this._dropDownSettings == null)
				{
					this._dropDownSettings = new DropDownSettings(this._viewState);
				}
				return this._dropDownSettings;
			}
		}

		// Token: 0x04002926 RID: 10534
		private DropDownSettings _dropDownSettings;

		// Token: 0x04002927 RID: 10535
		private StateBag _viewState;
	}
}
