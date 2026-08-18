using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI
{
	// Token: 0x02000D9E RID: 3486
	public abstract class PivotGridField : StateManager, IComparable, IDisposable, INamingContainer
	{
		// Token: 0x1700290F RID: 10511
		// (get) Token: 0x060081DE RID: 33246 RVA: 0x001DA188 File Offset: 0x001D8388
		// (set) Token: 0x060081DF RID: 33247 RVA: 0x001DA1B5 File Offset: 0x001D83B5
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual string DataFormatString
		{
			get
			{
				object obj = base.ViewState["DataFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		// Token: 0x17002910 RID: 10512
		// (get) Token: 0x060081E0 RID: 33248 RVA: 0x001DA1C8 File Offset: 0x001D83C8
		// (set) Token: 0x060081E1 RID: 33249 RVA: 0x001DA206 File Offset: 0x001D8406
		internal List<string> FlatChildOlapInfoNames
		{
			get
			{
				if (base.ViewState["_FCOI"] == null)
				{
					base.ViewState["_FCOI"] = new List<string>();
				}
				return (List<string>)base.ViewState["_FCOI"];
			}
			set
			{
				base.ViewState["_FCOI"] = value;
			}
		}

		// Token: 0x17002911 RID: 10513
		// (get) Token: 0x060081E2 RID: 33250 RVA: 0x001DA21C File Offset: 0x001D841C
		// (set) Token: 0x060081E3 RID: 33251 RVA: 0x001DA25C File Offset: 0x001D845C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PivotGridFieldRenderingControl RenderingControl
		{
			get
			{
				if (this.renderingControl == null)
				{
					this.renderingControl = new PivotGridFieldRenderingControl(this, -1)
					{
						ID = "rc_" + this.UniqueName
					};
				}
				return this.renderingControl;
			}
			set
			{
				this.renderingControl = value;
			}
		}

		// Token: 0x17002912 RID: 10514
		// (get) Token: 0x060081E4 RID: 33252 RVA: 0x001DA265 File Offset: 0x001D8465
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string FieldType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17002913 RID: 10515
		// (get) Token: 0x060081E5 RID: 33253 RVA: 0x001DA272 File Offset: 0x001D8472
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PivotGridFieldZoneType ZoneType
		{
			get
			{
				if (this is PivotGridRowField)
				{
					return PivotGridFieldZoneType.Row;
				}
				if (this is PivotGridColumnField)
				{
					return PivotGridFieldZoneType.Column;
				}
				if (this is PivotGridAggregateField)
				{
					return PivotGridFieldZoneType.Aggregate;
				}
				if (this is PivotGridReportFilterField)
				{
					return PivotGridFieldZoneType.Filter;
				}
				return PivotGridFieldZoneType.Row;
			}
		}

		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x060081E6 RID: 33254 RVA: 0x001DA2A0 File Offset: 0x001D84A0
		// (set) Token: 0x060081E7 RID: 33255 RVA: 0x001DA2CE File Offset: 0x001D84CE
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Gets or sets if the field will be hidden which exlude it from the pivot table calculations.")]
		public bool IsHidden
		{
			get
			{
				object obj = base.ViewState["IsHidden"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["IsHidden"] = value;
			}
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x060081E8 RID: 33256 RVA: 0x001DA2E8 File Offset: 0x001D84E8
		// (set) Token: 0x060081E9 RID: 33257 RVA: 0x001DA316 File Offset: 0x001D8516
		[DefaultValue(PivotGridSortOrder.Ascending)]
		[NotifyParentProperty(true)]
		public PivotGridSortOrder SortOrder
		{
			get
			{
				object obj = base.ViewState["SortOrder"];
				if (obj == null)
				{
					obj = PivotGridSortOrder.Ascending;
				}
				return (PivotGridSortOrder)obj;
			}
			set
			{
				base.ViewState["SortOrder"] = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x060081EA RID: 33258 RVA: 0x001DA334 File Offset: 0x001D8534
		// (set) Token: 0x060081EB RID: 33259 RVA: 0x001DA35D File Offset: 0x001D855D
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		[Description("Gets or sets the order indexes for fields displayed within the same zone.")]
		[Category("Appearance")]
		[Localizable(true)]
		public virtual int ZoneIndex
		{
			get
			{
				object obj = base.ViewState["ZoneIndex"];
				if (obj != null)
				{
					return Convert.ToInt32(obj);
				}
				return 0;
			}
			set
			{
				base.ViewState["ZoneIndex"] = value;
			}
		}

		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x060081EC RID: 33260 RVA: 0x001DA375 File Offset: 0x001D8575
		// (set) Token: 0x060081ED RID: 33261 RVA: 0x001DA390 File Offset: 0x001D8590
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue("")]
		public virtual string UniqueName
		{
			get
			{
				if (string.IsNullOrEmpty(this.uniqueName))
				{
					this.GetUniqueName();
				}
				return this.uniqueName;
			}
			set
			{
				if (Regex.IsMatch(value, "\\s"))
				{
					throw new ArgumentException("UniqueName cannot contain spaces");
				}
				if (this.IsUniqueName(value))
				{
					this.uniqueName = value;
					return;
				}
				throw new Exception("Duplicate unique names for RadPivotGrid's fields are not allowed.");
			}
		}

		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x060081EE RID: 33262 RVA: 0x001DA3C8 File Offset: 0x001D85C8
		// (set) Token: 0x060081EF RID: 33263 RVA: 0x001DA3F5 File Offset: 0x001D85F5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["DataField"] = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x060081F0 RID: 33264 RVA: 0x001DA410 File Offset: 0x001D8610
		// (set) Token: 0x060081F1 RID: 33265 RVA: 0x001DA43D File Offset: 0x001D863D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Caption
		{
			get
			{
				object obj = base.ViewState["Caption"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["Caption"] = value;
			}
		}

		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x060081F2 RID: 33266 RVA: 0x001DA450 File Offset: 0x001D8650
		// (set) Token: 0x060081F3 RID: 33267 RVA: 0x001DA47D File Offset: 0x001D867D
		[Category("Behavior")]
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string TotalFormatString
		{
			get
			{
				object obj = base.ViewState["TotalFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["TotalFormatString"] = value;
			}
		}

		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x060081F4 RID: 33268 RVA: 0x001DA490 File Offset: 0x001D8690
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("Pivot grid field's cells style")]
		[Category("Style")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual Style CellStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new Style();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x060081F5 RID: 33269 RVA: 0x001DA4BE File Offset: 0x001D86BE
		// (set) Token: 0x060081F6 RID: 33270 RVA: 0x001DA4C6 File Offset: 0x001D86C6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadPivotGrid Owner { get; internal set; }

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x060081F7 RID: 33271 RVA: 0x001DA4CF File Offset: 0x001D86CF
		// (set) Token: 0x060081F8 RID: 33272 RVA: 0x001DA4D7 File Offset: 0x001D86D7
		internal FieldInfoNode FieldInfoNode { get; set; }

		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x060081F9 RID: 33273 RVA: 0x001DA4E0 File Offset: 0x001D86E0
		// (set) Token: 0x060081FA RID: 33274 RVA: 0x001DA509 File Offset: 0x001D8709
		internal int DescriptorIndex
		{
			get
			{
				object obj = base.ViewState["DescriptorIndex"];
				if (obj != null)
				{
					return Convert.ToInt32(obj);
				}
				return 0;
			}
			set
			{
				base.ViewState["DescriptorIndex"] = value;
			}
		}

		// Token: 0x060081FB RID: 33275
		protected abstract void OnDescriptionInfoChanged();

		// Token: 0x060081FC RID: 33276 RVA: 0x001DA521 File Offset: 0x001D8721
		public void SetDescriptionInfo()
		{
			this.OnDescriptionInfoChanged();
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x001DA54C File Offset: 0x001D874C
		public void Show()
		{
			if (this.IsHidden)
			{
				int zoneIndex = 0;
				IEnumerable<PivotGridField> source = from f in this.Owner.Fields
				where f.ZoneType == this.ZoneType && !f.IsHidden
				select f;
				if (source.Count<PivotGridField>() > 0)
				{
					zoneIndex = source.Max((PivotGridField f) => f.ZoneIndex) + 1;
				}
				this.ZoneIndex = zoneIndex;
				this.IsHidden = false;
			}
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x060081FE RID: 33278 RVA: 0x001DA5C4 File Offset: 0x001D87C4
		// (set) Token: 0x060081FF RID: 33279 RVA: 0x001DA5CC File Offset: 0x001D87CC
		internal int Levels { get; set; }

		// Token: 0x06008200 RID: 33280 RVA: 0x001DA5D5 File Offset: 0x001D87D5
		internal void SetOwner(RadPivotGrid owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x001DA5DE File Offset: 0x001D87DE
		private void GetUniqueName()
		{
			this.uniqueName = this.GenerateUniqueName();
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x001DA5EC File Offset: 0x001D87EC
		internal void EnsureUniqueName()
		{
			if (this.uniqueName == null)
			{
				this.uniqueName = this.GenerateUniqueName();
				return;
			}
			if (!this.IsUniqueName(this.uniqueName))
			{
				throw new Exception("Duplicate unique names for RadPivotGrid's fields are not allowed.");
			}
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x001DA61C File Offset: 0x001D881C
		protected virtual string GenerateUniqueName()
		{
			return this.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x001DA62C File Offset: 0x001D882C
		protected string GenerateUniqueNameBase(string Base)
		{
			string text = (!string.IsNullOrEmpty(Base)) ? Base : "column";
			text = Regex.Replace(text, "\\s", string.Empty);
			string text2 = text;
			if (this.Owner != null)
			{
				for (int i = 0; i < 500; i++)
				{
					text2 = text + ((i != 0) ? i.ToString(CultureInfo.InvariantCulture) : string.Empty);
					if (this.IsUniqueName(text2))
					{
						break;
					}
				}
			}
			return text2;
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x001DA69C File Offset: 0x001D889C
		protected bool IsUniqueName(string testName)
		{
			if (this.Owner != null)
			{
				foreach (PivotGridField pivotGridField in this.Owner.Fields)
				{
					if (this != pivotGridField && pivotGridField.uniqueName == testName)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x001DA72C File Offset: 0x001D892C
		public int GetLevel()
		{
			int num = 0;
			if (this.Owner != null)
			{
				IOrderedEnumerable<PivotGridField> orderedEnumerable = from f in this.Owner.Fields
				where f.ZoneType == this.ZoneType && !f.IsHidden
				orderby f.ZoneIndex
				select f;
				using (IEnumerator<PivotGridField> enumerator = orderedEnumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PivotGridField pivotGridField = enumerator.Current;
						if (this == pivotGridField)
						{
							break;
						}
						num++;
					}
					return num;
				}
			}
			num = -1;
			return num;
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x001DA7D0 File Offset: 0x001D89D0
		public void ClearRenderControl()
		{
			this.renderingControl = null;
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x001DA7D9 File Offset: 0x001D89D9
		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008209 RID: 33289 RVA: 0x001DA7E0 File Offset: 0x001D89E0
		protected override object SaveViewState()
		{
			object obj = (this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null;
			base.SaveViewState();
			return new object[]
			{
				base.SaveViewState(),
				obj,
				this.uniqueName
			};
		}

		// Token: 0x0600820A RID: 33290 RVA: 0x001DA82C File Offset: 0x001D8A2C
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.CellStyle).LoadViewState(array[1]);
				if (array[2] != null)
				{
					this.uniqueName = (string)array[2];
					return;
				}
			}
			else
			{
				base.LoadViewState(state);
			}
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x001DA875 File Offset: 0x001D8A75
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
		}

		// Token: 0x0600820C RID: 33292 RVA: 0x001DA890 File Offset: 0x001D8A90
		internal virtual void CopyBaseProperties(PivotGridField field)
		{
			this.Caption = field.Caption;
			this.CellStyle.CopyFrom(field.CellStyle);
			this.DataField = field.DataField;
			this.DataFormatString = field.DataFormatString;
			this.SortOrder = field.SortOrder;
			this.TotalFormatString = field.TotalFormatString;
			this.UniqueName = field.UniqueName;
			this.ZoneIndex = field.ZoneIndex;
			this.IsHidden = field.IsHidden;
		}

		// Token: 0x0600820D RID: 33293 RVA: 0x001DA90E File Offset: 0x001D8B0E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600820E RID: 33294 RVA: 0x001DA91D File Offset: 0x001D8B1D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.headerStyle != null)
			{
				this.headerStyle.Dispose();
			}
		}

		// Token: 0x040023CA RID: 9162
		private string uniqueName;

		// Token: 0x040023CB RID: 9163
		private Style headerStyle;

		// Token: 0x040023CC RID: 9164
		private PivotGridFieldRenderingControl renderingControl;
	}
}
