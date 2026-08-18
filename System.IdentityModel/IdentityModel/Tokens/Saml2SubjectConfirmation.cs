using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000146 RID: 326
	public class Saml2SubjectConfirmation
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0002B7B4 File Offset: 0x000299B4
		public Saml2SubjectConfirmation(Uri method) : this(method, null)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002B7C0 File Offset: 0x000299C0
		public Saml2SubjectConfirmation(Uri method, Saml2SubjectConfirmationData data)
		{
			if (null == method)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("method");
			}
			if (!method.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("method", SR.GetString("ID0013"));
			}
			this.method = method;
			this.data = data;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002B81C File Offset: 0x00029A1C
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0002B824 File Offset: 0x00029A24
		public Uri Method
		{
			get
			{
				return this.method;
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
				this.method = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002B873 File Offset: 0x00029A73
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0002B87B File Offset: 0x00029A7B
		public Saml2NameIdentifier NameIdentifier
		{
			get
			{
				return this.nameId;
			}
			set
			{
				this.nameId = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002B884 File Offset: 0x00029A84
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0002B88C File Offset: 0x00029A8C
		public Saml2SubjectConfirmationData SubjectConfirmationData
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x04000B6F RID: 2927
		private Saml2SubjectConfirmationData data;

		// Token: 0x04000B70 RID: 2928
		private Uri method;

		// Token: 0x04000B71 RID: 2929
		private Saml2NameIdentifier nameId;
	}
}
