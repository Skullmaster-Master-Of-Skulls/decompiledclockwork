using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200008E RID: 142
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	public sealed class PageMethodSignatureAttribute : Attribute
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000CC9D File Offset: 0x0000AE9D
		public string FriendlyName
		{
			get
			{
				return this._friendlyName;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
		public string ServicePathProperty
		{
			get
			{
				return this._servicePathProperty;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		public string ServiceMethodProperty
		{
			get
			{
				return this._serviceMethodProperty;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		public string UseContextKeyProperty
		{
			get
			{
				return this._useContextKeyProperty;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000CCBD File Offset: 0x0000AEBD
		public bool IncludeContextParameter
		{
			get
			{
				return !string.IsNullOrEmpty(this._useContextKeyProperty);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000CCCD File Offset: 0x0000AECD
		public PageMethodSignatureAttribute(string friendlyName, string servicePathProperty, string serviceMethodProperty) : this(friendlyName, servicePathProperty, serviceMethodProperty, null)
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000CCD9 File Offset: 0x0000AED9
		public PageMethodSignatureAttribute(string friendlyName, string servicePathProperty, string serviceMethodProperty, string useContextKeyProperty)
		{
			this._friendlyName = friendlyName;
			this._servicePathProperty = servicePathProperty;
			this._serviceMethodProperty = serviceMethodProperty;
			this._useContextKeyProperty = useContextKeyProperty;
		}

		// Token: 0x0400029C RID: 668
		private string _friendlyName;

		// Token: 0x0400029D RID: 669
		private string _servicePathProperty;

		// Token: 0x0400029E RID: 670
		private string _serviceMethodProperty;

		// Token: 0x0400029F RID: 671
		private string _useContextKeyProperty;
	}
}
