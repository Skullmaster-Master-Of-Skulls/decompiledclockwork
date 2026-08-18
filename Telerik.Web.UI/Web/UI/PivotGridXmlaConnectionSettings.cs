using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE8 RID: 3560
	public class PivotGridXmlaConnectionSettings : StateManager
	{
		// Token: 0x170029C4 RID: 10692
		// (get) Token: 0x0600842D RID: 33837 RVA: 0x001E268C File Offset: 0x001E088C
		// (set) Token: 0x0600842E RID: 33838 RVA: 0x001E26D0 File Offset: 0x001E08D0
		[DefaultValue("")]
		public string DataBase
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["DataBase"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["DataBase"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["DataBase"] = value;
			}
		}

		// Token: 0x170029C5 RID: 10693
		// (get) Token: 0x0600842F RID: 33839 RVA: 0x001E26E4 File Offset: 0x001E08E4
		// (set) Token: 0x06008430 RID: 33840 RVA: 0x001E2728 File Offset: 0x001E0928
		[DefaultValue("")]
		public string Cube
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["Cube"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["Cube"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["Cube"] = value;
			}
		}

		// Token: 0x170029C6 RID: 10694
		// (get) Token: 0x06008431 RID: 33841 RVA: 0x001E273C File Offset: 0x001E093C
		// (set) Token: 0x06008432 RID: 33842 RVA: 0x001E2780 File Offset: 0x001E0980
		[DefaultValue("")]
		public string ServerAddress
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["ServerAddress"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["ServerAddress"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["ServerAddress"] = value;
			}
		}

		// Token: 0x170029C7 RID: 10695
		// (get) Token: 0x06008433 RID: 33843 RVA: 0x001E2793 File Offset: 0x001E0993
		// (set) Token: 0x06008434 RID: 33844 RVA: 0x001E27C2 File Offset: 0x001E09C2
		[DefaultValue(typeof(Encoding), "utf-8")]
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding Encoding
		{
			get
			{
				if (base.ViewState["Encoding"] == null)
				{
					return PivotGridXmlaConnectionSettings.DefaultEncoding;
				}
				return base.ViewState["Encoding"] as Encoding;
			}
			set
			{
				base.ViewState["Encoding"] = value;
			}
		}

		// Token: 0x170029C8 RID: 10696
		// (get) Token: 0x06008435 RID: 33845 RVA: 0x001E27D5 File Offset: 0x001E09D5
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x170029C9 RID: 10697
		// (get) Token: 0x06008436 RID: 33846 RVA: 0x001E27DC File Offset: 0x001E09DC
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public PivotGridXmlaNetworkCredential Credentials
		{
			get
			{
				if (this.xmlaNetWorkCredential == null)
				{
					this.xmlaNetWorkCredential = new PivotGridXmlaNetworkCredential();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.xmlaNetWorkCredential).TrackViewState();
					}
				}
				return this.xmlaNetWorkCredential;
			}
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x001E280C File Offset: 0x001E0A0C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Credentials).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x001E2850 File Offset: 0x001E0A50
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = savedState as object[];
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.Credentials).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x001E2888 File Offset: 0x001E0A88
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.Credentials).TrackViewState();
		}

		// Token: 0x040024B4 RID: 9396
		private PivotGridXmlaNetworkCredential xmlaNetWorkCredential;
	}
}
