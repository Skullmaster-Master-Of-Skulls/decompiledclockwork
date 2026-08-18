using System;
using System.Security.Permissions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001838 RID: 6200
	public class RadSessionPageStatePersister : SessionPageStatePersister
	{
		// Token: 0x0600F0E7 RID: 61671 RVA: 0x0036BD79 File Offset: 0x00369F79
		public RadSessionPageStatePersister(Page page) : base(page)
		{
		}

		// Token: 0x0600F0E8 RID: 61672 RVA: 0x0036BD82 File Offset: 0x00369F82
		public override void Load()
		{
			base.Load();
			if (base.ViewState is CompressedPageState)
			{
				base.ViewState = base.StateFormatter.Deserialize(((CompressedPageState)base.ViewState).Decompress());
			}
		}

		// Token: 0x0600F0E9 RID: 61673 RVA: 0x0036BDB8 File Offset: 0x00369FB8
		public override void Save()
		{
			bool flag = SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
			if (base.ViewState != null && flag)
			{
				string text = base.StateFormatter.Serialize(base.ViewState);
				if (text.Length > 8192)
				{
					base.ViewState = CompressedPageState.Compress(text);
				}
			}
			base.Save();
		}
	}
}
