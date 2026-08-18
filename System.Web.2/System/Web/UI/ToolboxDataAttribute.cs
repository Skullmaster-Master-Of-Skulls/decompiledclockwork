using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200031A RID: 794
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ToolboxDataAttribute : Attribute
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x0007A699 File Offset: 0x00078899
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0007A6A1 File Offset: 0x000788A1
		public ToolboxDataAttribute(string data)
		{
			this.data = data;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0007A6BB File Offset: 0x000788BB
		public override int GetHashCode()
		{
			if (this.Data == null)
			{
				return 0;
			}
			return this.Data.GetHashCode();
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0007A6D2 File Offset: 0x000788D2
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is ToolboxDataAttribute && StringUtil.EqualsIgnoreCase(((ToolboxDataAttribute)obj).Data, this.data));
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0007A6FD File Offset: 0x000788FD
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxDataAttribute.Default);
		}

		// Token: 0x04001D66 RID: 7526
		public static readonly ToolboxDataAttribute Default = new ToolboxDataAttribute(string.Empty);

		// Token: 0x04001D67 RID: 7527
		private string data = string.Empty;
	}
}
