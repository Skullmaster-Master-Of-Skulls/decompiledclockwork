using System;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000479 RID: 1145
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ToolboxDataAttribute : Attribute
	{
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000E811F File Offset: 0x000E711F
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000E8127 File Offset: 0x000E7127
		public ToolboxDataAttribute(string data)
		{
			this.data = data;
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000E8141 File Offset: 0x000E7141
		public override int GetHashCode()
		{
			if (this.Data == null)
			{
				return 0;
			}
			return this.Data.GetHashCode();
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000E8158 File Offset: 0x000E7158
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is ToolboxDataAttribute && StringUtil.EqualsIgnoreCase(((ToolboxDataAttribute)obj).Data, this.data));
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000E8183 File Offset: 0x000E7183
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxDataAttribute.Default);
		}

		// Token: 0x0400255A RID: 9562
		public static readonly ToolboxDataAttribute Default = new ToolboxDataAttribute(string.Empty);

		// Token: 0x0400255B RID: 9563
		private string data = string.Empty;
	}
}
