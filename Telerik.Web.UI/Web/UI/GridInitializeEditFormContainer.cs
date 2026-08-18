using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200112C RID: 4396
	public class GridInitializeEditFormContainer : GridItemEventInfo
	{
		// Token: 0x0600B367 RID: 45927 RVA: 0x00271697 File Offset: 0x0026F897
		public GridInitializeEditFormContainer(Control formContainer)
		{
			this._formContainer = formContainer;
		}

		// Token: 0x170039F7 RID: 14839
		// (get) Token: 0x0600B368 RID: 45928 RVA: 0x002716A6 File Offset: 0x0026F8A6
		// (set) Token: 0x0600B369 RID: 45929 RVA: 0x002716AE File Offset: 0x0026F8AE
		public Control FormContainer
		{
			get
			{
				return this._formContainer;
			}
			set
			{
				this._formContainer = value;
			}
		}

		// Token: 0x04002F38 RID: 12088
		private Control _formContainer;
	}
}
