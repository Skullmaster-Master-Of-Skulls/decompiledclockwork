using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000131 RID: 305
	public class Saml2Action
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x00024154 File Offset: 0x00022354
		public Saml2Action(string value, Uri actionNamespace)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (null == actionNamespace)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actionNamespace");
			}
			if (!actionNamespace.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("actionNamespace", SR.GetString("ID0013"));
			}
			this.actionNamespace = actionNamespace;
			this.value = value;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000241C8 File Offset: 0x000223C8
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x000241D0 File Offset: 0x000223D0
		public Uri Namespace
		{
			get
			{
				return this.actionNamespace;
			}
			set
			{
				if (null == value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (!value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this.actionNamespace = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0002421F File Offset: 0x0002241F
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00024227 File Offset: 0x00022427
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.value = value;
			}
		}

		// Token: 0x04000B25 RID: 2853
		private Uri actionNamespace;

		// Token: 0x04000B26 RID: 2854
		private string value;
	}
}
