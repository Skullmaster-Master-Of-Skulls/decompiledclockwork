using System;

namespace System.Web.UI
{
	// Token: 0x02000047 RID: 71
	internal sealed class ClientUrlResolverWrapper : IClientUrlResolver
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000115AC File Offset: 0x0000F7AC
		public ClientUrlResolverWrapper(Control control)
		{
			this._control = control;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000115BB File Offset: 0x0000F7BB
		string IClientUrlResolver.AppRelativeTemplateSourceDirectory
		{
			get
			{
				return this._control.AppRelativeTemplateSourceDirectory;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000115C8 File Offset: 0x0000F7C8
		string IClientUrlResolver.ResolveClientUrl(string relativeUrl)
		{
			IClientUrlResolver clientUrlResolver = this._control as IClientUrlResolver;
			if (clientUrlResolver != null)
			{
				return clientUrlResolver.ResolveClientUrl(relativeUrl);
			}
			return this._control.ResolveClientUrl(relativeUrl);
		}

		// Token: 0x0400010A RID: 266
		private readonly Control _control;
	}
}
