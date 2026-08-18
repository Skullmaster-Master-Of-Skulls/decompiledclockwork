using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Text;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F9 RID: 249
	[LayoutRenderer("windows-identity")]
	public class WindowsIdentityLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x0000FBD6 File Offset: 0x0000DDD6
		public WindowsIdentityLayoutRenderer()
		{
			this.UserName = true;
			this.Domain = true;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		[DefaultValue(true)]
		public bool Domain { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000FBFD File Offset: 0x0000DDFD
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x0000FC05 File Offset: 0x0000DE05
		[DefaultValue(true)]
		public bool UserName { get; set; }

		// Token: 0x0600070D RID: 1805 RVA: 0x0000FC10 File Offset: 0x0000DE10
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			if (current != null)
			{
				string value = string.Empty;
				if (this.UserName)
				{
					if (this.Domain)
					{
						value = current.Name;
					}
					else
					{
						int num = current.Name.LastIndexOf('\\');
						if (num >= 0)
						{
							value = current.Name.Substring(num + 1);
						}
						else
						{
							value = current.Name;
						}
					}
				}
				else
				{
					if (!this.Domain)
					{
						return;
					}
					int num2 = current.Name.IndexOf('\\');
					if (num2 >= 0)
					{
						value = current.Name.Substring(0, num2);
					}
					else
					{
						value = current.Name;
					}
				}
				builder.Append(value);
			}
		}
	}
}
