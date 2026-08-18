using System;
using System.IO;
using System.IO.Compression;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001836 RID: 6198
	public class RadHiddenFieldPageStatePersister : HiddenFieldPageStatePersister
	{
		// Token: 0x0600F0E0 RID: 61664 RVA: 0x0036BBF1 File Offset: 0x00369DF1
		public RadHiddenFieldPageStatePersister(Page page) : base(page)
		{
		}

		// Token: 0x0600F0E1 RID: 61665 RVA: 0x0036BBFC File Offset: 0x00369DFC
		public override void Load()
		{
			base.Load();
			if (base.ViewState is CompressedPageState)
			{
				base.ViewState = base.StateFormatter.Deserialize(((CompressedPageState)base.ViewState).Decompress());
			}
			if (base.ControlState is CompressedPageState)
			{
				base.ControlState = base.StateFormatter.Deserialize(((CompressedPageState)base.ControlState).Decompress());
			}
		}

		// Token: 0x0600F0E2 RID: 61666 RVA: 0x0036BC6C File Offset: 0x00369E6C
		public override void Save()
		{
			if (!this.ShouldApplyCompressionOnAjax)
			{
				base.Save();
				return;
			}
			if (base.ViewState != null)
			{
				string text = base.StateFormatter.Serialize(base.ViewState);
				if (text.Length > 8192)
				{
					base.ViewState = CompressedPageState.Compress(text);
				}
			}
			if (base.ControlState != null)
			{
				string text2 = base.StateFormatter.Serialize(base.ControlState);
				if (text2.Length > 8192)
				{
					base.ControlState = CompressedPageState.Compress(text2);
				}
			}
			base.Save();
		}

		// Token: 0x170048CD RID: 18637
		// (get) Token: 0x0600F0E3 RID: 61667 RVA: 0x0036BCF4 File Offset: 0x00369EF4
		protected virtual bool ShouldApplyCompressionOnAjax
		{
			get
			{
				ScriptManager current = ScriptManager.GetCurrent(base.Page);
				return current == null || !current.IsInAsyncPostBack || !this.IsResponseCompressed();
			}
		}

		// Token: 0x0600F0E4 RID: 61668 RVA: 0x0036BD24 File Offset: 0x00369F24
		public virtual bool IsResponseCompressed()
		{
			Stream filter = base.Page.Response.Filter;
			return filter is GZipStream || filter is DeflateStream;
		}
	}
}
