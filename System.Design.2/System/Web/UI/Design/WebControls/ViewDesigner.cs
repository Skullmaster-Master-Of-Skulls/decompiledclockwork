using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000131 RID: 305
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ViewDesigner : ContainerControlDesigner
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		public ViewDesigner()
		{
			base.FrameStyleInternal.Width = Unit.Percentage(100.0);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000477D0 File Offset: 0x000459D0
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(View));
			base.Initialize(component);
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool NoWrap
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000477E9 File Offset: 0x000459E9
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			return this.GetDesignTimeHtmlHelper(true, regions);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000477F3 File Offset: 0x000459F3
		public override string GetDesignTimeHtml()
		{
			return this.GetDesignTimeHtmlHelper(false, null);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00047800 File Offset: 0x00045A00
		private string GetDesignTimeHtmlHelper(bool useRegions, DesignerRegionCollection regions)
		{
			View view = (View)base.Component;
			if (!(view.Parent is MultiView))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(View), typeof(MultiView));
			}
			if (useRegions)
			{
				return base.GetDesignTimeHtml(regions);
			}
			return base.GetDesignTimeHtml();
		}
	}
}
