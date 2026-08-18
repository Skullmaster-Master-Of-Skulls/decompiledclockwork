using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200001C RID: 28
	public abstract class BaseValidatingContext<TOptions> : BaseContext<TOptions>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000693A File Offset: 0x00004B3A
		protected BaseValidatingContext(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00006944 File Offset: 0x00004B44
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000694C File Offset: 0x00004B4C
		public bool IsValidated { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00006955 File Offset: 0x00004B55
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000695D File Offset: 0x00004B5D
		public bool HasError { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00006966 File Offset: 0x00004B66
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000696E File Offset: 0x00004B6E
		public string Error { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00006977 File Offset: 0x00004B77
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000697F File Offset: 0x00004B7F
		public string ErrorDescription { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00006988 File Offset: 0x00004B88
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00006990 File Offset: 0x00004B90
		public string ErrorUri { get; private set; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00006999 File Offset: 0x00004B99
		public virtual bool Validated()
		{
			this.IsValidated = true;
			this.HasError = false;
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000069AA File Offset: 0x00004BAA
		public virtual void Rejected()
		{
			this.IsValidated = false;
			this.HasError = false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000069BA File Offset: 0x00004BBA
		public void SetError(string error)
		{
			this.SetError(error, null);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000069C4 File Offset: 0x00004BC4
		public void SetError(string error, string errorDescription)
		{
			this.SetError(error, errorDescription, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000069CF File Offset: 0x00004BCF
		public void SetError(string error, string errorDescription, string errorUri)
		{
			this.Error = error;
			this.ErrorDescription = errorDescription;
			this.ErrorUri = errorUri;
			this.Rejected();
			this.HasError = true;
		}
	}
}
