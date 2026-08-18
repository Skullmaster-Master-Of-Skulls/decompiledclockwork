using System;

namespace System.Web.UI
{
	// Token: 0x02000064 RID: 100
	public sealed class RegisteredExpandoAttribute
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00013AB5 File Offset: 0x00011CB5
		internal RegisteredExpandoAttribute(Control control, string controlId, string name, string value, bool encode)
		{
			this._control = control;
			this._controlId = controlId;
			this._name = name;
			this._value = value;
			this._encode = encode;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00013AE2 File Offset: 0x00011CE2
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00013AEA File Offset: 0x00011CEA
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00013AF2 File Offset: 0x00011CF2
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00013AFA File Offset: 0x00011CFA
		public string ControlId
		{
			get
			{
				return this._controlId;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00013B02 File Offset: 0x00011D02
		public bool Encode
		{
			get
			{
				return this._encode;
			}
		}

		// Token: 0x04000158 RID: 344
		private Control _control;

		// Token: 0x04000159 RID: 345
		private string _name;

		// Token: 0x0400015A RID: 346
		private string _value;

		// Token: 0x0400015B RID: 347
		private string _controlId;

		// Token: 0x0400015C RID: 348
		private bool _encode;
	}
}
