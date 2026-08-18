using System;
using System.Web.Security.Cryptography;

namespace System.Web.UI
{
	// Token: 0x0200028D RID: 653
	public class HiddenFieldPageStatePersister : PageStatePersister
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x000625E9 File Offset: 0x000607E9
		public HiddenFieldPageStatePersister(Page page) : base(page)
		{
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000625F4 File Offset: 0x000607F4
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
				if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(base.Page.ViewStateUserKey))
				{
					Pair pair = (Pair)Util.DeserializeWithAssert(base.StateFormatter2, text, Purpose.WebForms_HiddenFieldPageStatePersister_ClientState);
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

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00062694 File Offset: 0x00060894
		public override void Save()
		{
			if (base.ViewState != null || base.ControlState != null)
			{
				base.Page.ClientState = Util.SerializeWithAssert(base.StateFormatter2, new Pair(base.ViewState, base.ControlState), Purpose.WebForms_HiddenFieldPageStatePersister_ClientState);
			}
		}
	}
}
