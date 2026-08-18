using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000BC3 RID: 3011
	public abstract class ExpressionBase
	{
		// Token: 0x06007355 RID: 29525 RVA: 0x001AFD76 File Offset: 0x001ADF76
		public ExpressionBase(string modelID)
		{
			this._modelID = modelID;
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x06007356 RID: 29526 RVA: 0x001AFD85 File Offset: 0x001ADF85
		// (set) Token: 0x06007357 RID: 29527 RVA: 0x001AFD8D File Offset: 0x001ADF8D
		[DefaultValue("")]
		[Description(" Gets or sets the model id to whom this filters applies")]
		[Category("Behavior")]
		public string DataModelID
		{
			get
			{
				return this._modelID;
			}
			set
			{
				this._modelID = value;
			}
		}

		// Token: 0x04001F40 RID: 8000
		private string _modelID;
	}
}
