using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003F8 RID: 1016
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HiddenFieldPageStatePersister : PageStatePersister
	{
		// Token: 0x06003237 RID: 12855 RVA: 0x000DC58D File Offset: 0x000DB58D
		public HiddenFieldPageStatePersister(Page page) : base(page)
		{
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000DC598 File Offset: 0x000DB598
		public override void Load()
		{
			if (base.Page.RequestValueCollection == null)
			{
				return;
			}
			string text = null;
			try
			{
				text = base.Page.RequestViewStateString;
				if (!string.IsNullOrEmpty(text))
				{
					Pair pair = (Pair)Util.DeserializeWithAssert(base.StateFormatter, text);
					base.ViewState = pair.First;
					base.ControlState = pair.Second;
				}
			}
			catch (Exception ex)
			{
				if (ex.InnerException is ViewStateException)
				{
					throw;
				}
				ViewStateException.ThrowViewStateError(ex, text);
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000DC620 File Offset: 0x000DB620
		public override void Save()
		{
			if (base.ViewState != null || base.ControlState != null)
			{
				base.Page.ClientState = Util.SerializeWithAssert(base.StateFormatter, new Pair(base.ViewState, base.ControlState));
			}
		}
	}
}
