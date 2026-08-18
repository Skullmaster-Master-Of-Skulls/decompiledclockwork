using System;

namespace System.Web.UI
{
	// Token: 0x02000065 RID: 101
	public sealed class RegisteredHiddenField
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00013B0A File Offset: 0x00011D0A
		internal RegisteredHiddenField(Control control, string hiddenFieldName, string hiddenFieldInitialValue)
		{
			this._control = control;
			this._name = hiddenFieldName;
			this._initialValue = hiddenFieldInitialValue;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00013B27 File Offset: 0x00011D27
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00013B2F File Offset: 0x00011D2F
		public string InitialValue
		{
			get
			{
				return this._initialValue;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00013B37 File Offset: 0x00011D37
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0400015D RID: 349
		private Control _control;

		// Token: 0x0400015E RID: 350
		private string _name;

		// Token: 0x0400015F RID: 351
		private string _initialValue;
	}
}
