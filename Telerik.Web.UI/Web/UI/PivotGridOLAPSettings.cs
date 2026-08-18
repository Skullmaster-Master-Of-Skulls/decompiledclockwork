using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF9 RID: 3577
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridOLAPSettings : StateManager
	{
		// Token: 0x060084C9 RID: 33993 RVA: 0x001E4BC9 File Offset: 0x001E2DC9
		public PivotGridOLAPSettings(RadPivotGrid ownerGrid)
		{
			this.owner = ownerGrid;
		}

		// Token: 0x170029FE RID: 10750
		// (get) Token: 0x060084CA RID: 33994 RVA: 0x001E4BD8 File Offset: 0x001E2DD8
		// (set) Token: 0x060084CB RID: 33995 RVA: 0x001E4C10 File Offset: 0x001E2E10
		[DefaultValue(typeof(PivotGridOlapProviderType), "None")]
		public PivotGridOlapProviderType ProviderType
		{
			get
			{
				PivotGridOlapProviderType result = PivotGridOlapProviderType.None;
				if (base.ViewState["ProviderType"] != null)
				{
					result = (PivotGridOlapProviderType)base.ViewState["ProviderType"];
				}
				return result;
			}
			set
			{
				base.ViewState["ProviderType"] = value;
			}
		}

		// Token: 0x170029FF RID: 10751
		// (get) Token: 0x060084CC RID: 33996 RVA: 0x001E4C28 File Offset: 0x001E2E28
		// (set) Token: 0x060084CD RID: 33997 RVA: 0x001E4C57 File Offset: 0x001E2E57
		[DefaultValue("")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public virtual string ConnectionString
		{
			get
			{
				if (base.ViewState["ConnectionString"] == null)
				{
					return string.Empty;
				}
				return base.ViewState["ConnectionString"].ToString();
			}
			set
			{
				base.ViewState["ConnectionString"] = value;
			}
		}

		// Token: 0x17002A00 RID: 10752
		// (get) Token: 0x060084CE RID: 33998 RVA: 0x001E4C6C File Offset: 0x001E2E6C
		// (set) Token: 0x060084CF RID: 33999 RVA: 0x001E4C99 File Offset: 0x001E2E99
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value that indicates the total number of distinct items shown into the set condition filter.")]
		[DefaultValue(1000)]
		[Category("Appearance")]
		public virtual int SetConditionListCapacity
		{
			get
			{
				object obj = base.ViewState["SetConditionListCapacity"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1000;
			}
			set
			{
				base.ViewState["SetConditionListCapacity"] = value;
			}
		}

		// Token: 0x17002A01 RID: 10753
		// (get) Token: 0x060084D0 RID: 34000 RVA: 0x001E4CB1 File Offset: 0x001E2EB1
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Olap")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridAdomdConnectionSettings AdomdConnectionSettings
		{
			get
			{
				if (this.adomdConnectionSettings == null)
				{
					this.adomdConnectionSettings = new PivotGridAdomdConnectionSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.adomdConnectionSettings).TrackViewState();
					}
				}
				return this.adomdConnectionSettings;
			}
		}

		// Token: 0x17002A02 RID: 10754
		// (get) Token: 0x060084D1 RID: 34001 RVA: 0x001E4CDF File Offset: 0x001E2EDF
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Olap")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridXmlaConnectionSettings XmlaConnectionSettings
		{
			get
			{
				if (this.xmlaConnectionSettings == null)
				{
					this.xmlaConnectionSettings = new PivotGridXmlaConnectionSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.xmlaConnectionSettings).TrackViewState();
					}
				}
				return this.xmlaConnectionSettings;
			}
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x001E4D10 File Offset: 0x001E2F10
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.AdomdConnectionSettings).LoadViewState(array[num++]);
				((IStateManager)this.XmlaConnectionSettings).LoadViewState(array[num++]);
			}
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x001E4D5C File Offset: 0x001E2F5C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.AdomdConnectionSettings).SaveViewState(),
				((IStateManager)this.XmlaConnectionSettings).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x001E4DB0 File Offset: 0x001E2FB0
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.AdomdConnectionSettings).TrackViewState();
			((IStateManager)this.XmlaConnectionSettings).TrackViewState();
		}

		// Token: 0x040024FF RID: 9471
		private readonly RadPivotGrid owner;

		// Token: 0x04002500 RID: 9472
		private PivotGridAdomdConnectionSettings adomdConnectionSettings;

		// Token: 0x04002501 RID: 9473
		private PivotGridXmlaConnectionSettings xmlaConnectionSettings;
	}
}
