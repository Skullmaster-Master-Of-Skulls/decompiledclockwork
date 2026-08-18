using System;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000070 RID: 112
	public class ScriptControlDescriptor : ScriptComponentDescriptor
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x00013F7D File Offset: 0x0001217D
		public ScriptControlDescriptor(string type, string elementID) : base(type, elementID)
		{
			base.RegisterDispose = false;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000146FA File Offset: 0x000128FA
		public override string ClientID
		{
			get
			{
				return this.ElementID;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00013FBA File Offset: 0x000121BA
		public string ElementID
		{
			get
			{
				return base.ElementIDInternal;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00014702 File Offset: 0x00012902
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001470A File Offset: 0x0001290A
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptControlDescriptor_IDNotSettable, new object[]
				{
					"ID",
					typeof(ScriptControlDescriptor).FullName
				}));
			}
		}
	}
}
