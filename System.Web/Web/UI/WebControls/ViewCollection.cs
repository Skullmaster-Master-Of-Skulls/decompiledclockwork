using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005F0 RID: 1520
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ViewCollection : ControlCollection
	{
		// Token: 0x06004B30 RID: 19248 RVA: 0x00132E44 File Offset: 0x00131E44
		public ViewCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00132E4D File Offset: 0x00131E4D
		public override void Add(Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException(SR.GetString("ViewCollection_must_contain_view"));
			}
			base.Add(v);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00132E6E File Offset: 0x00131E6E
		public override void AddAt(int index, Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException(SR.GetString("ViewCollection_must_contain_view"));
			}
			base.AddAt(index, v);
		}

		// Token: 0x170012D3 RID: 4819
		public View this[int i]
		{
			get
			{
				return (View)base[i];
			}
		}
	}
}
