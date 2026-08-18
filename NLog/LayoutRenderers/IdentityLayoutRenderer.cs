using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D5 RID: 213
	[LayoutRenderer("identity")]
	public class IdentityLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0000DE4B File Offset: 0x0000C04B
		public IdentityLayoutRenderer()
		{
			this.Name = true;
			this.AuthType = true;
			this.IsAuthenticated = true;
			this.Separator = ":";
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000DE73 File Offset: 0x0000C073
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0000DE7B File Offset: 0x0000C07B
		[DefaultValue(":")]
		public string Separator { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000DE84 File Offset: 0x0000C084
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000DE8C File Offset: 0x0000C08C
		[DefaultValue(true)]
		public bool Name { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000DE95 File Offset: 0x0000C095
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000DE9D File Offset: 0x0000C09D
		[DefaultValue(true)]
		public bool AuthType { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000DEA6 File Offset: 0x0000C0A6
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000DEAE File Offset: 0x0000C0AE
		[DefaultValue(true)]
		public bool IsAuthenticated { get; set; }

		// Token: 0x0600063F RID: 1599 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal != null)
			{
				IIdentity identity = currentPrincipal.Identity;
				if (identity != null)
				{
					string value = string.Empty;
					if (this.IsAuthenticated)
					{
						builder.Append(value);
						value = this.Separator;
						if (identity.IsAuthenticated)
						{
							builder.Append("auth");
						}
						else
						{
							builder.Append("notauth");
						}
					}
					if (this.AuthType)
					{
						builder.Append(value);
						value = this.Separator;
						builder.Append(identity.AuthenticationType);
					}
					if (this.Name)
					{
						builder.Append(value);
						builder.Append(identity.Name);
					}
				}
			}
		}
	}
}
