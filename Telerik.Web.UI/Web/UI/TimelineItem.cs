using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI.Timeline;

namespace Telerik.Web.UI
{
	// Token: 0x02000932 RID: 2354
	[Serializable]
	public class TimelineItem : StateManager, IItem, IAttributeAccessor
	{
		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x00110093 File Offset: 0x0010E293
		// (set) Token: 0x0600594E RID: 22862 RVA: 0x0011009B File Offset: 0x0010E29B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		[Themeable(false)]
		public bool HasAttributes { get; private set; }

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x0600594F RID: 22863 RVA: 0x001100A4 File Offset: 0x0010E2A4
		// (set) Token: 0x06005950 RID: 22864 RVA: 0x001100AC File Offset: 0x0010E2AC
		[Browsable(false)]
		public RadTimeline Owner { get; set; }

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x001100B5 File Offset: 0x0010E2B5
		// (set) Token: 0x06005952 RID: 22866 RVA: 0x001100D1 File Offset: 0x0010E2D1
		internal Dictionary<string, object> TemplateData
		{
			get
			{
				return (Dictionary<string, object>)(base.ViewState["TemplateData"] ?? null);
			}
			set
			{
				base.ViewState["TemplateData"] = value;
			}
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x001100E4 File Offset: 0x0010E2E4
		void IItem.DataBind()
		{
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x00110170 File Offset: 0x0010E370
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (!string.IsNullOrEmpty(this.Owner.DataDescriptionField))
			{
				this.Description = properties.GetPropertyValue(dataItem, this.Owner.DataDescriptionField).ToString();
			}
			if (!string.IsNullOrEmpty(this.Owner.DataTitleField))
			{
				this.Title = properties.GetPropertyValue(dataItem, this.Owner.DataTitleField).ToString();
			}
			if (!string.IsNullOrEmpty(this.Owner.DataSubtitleField))
			{
				this.Subtitle = properties.GetPropertyValue(dataItem, this.Owner.DataSubtitleField).ToString();
			}
			string propertyName = string.IsNullOrEmpty(this.Owner.DataDateField) ? "Date" : this.Owner.DataDateField;
			this.Date = Convert.ToDateTime(properties.GetPropertyValue(dataItem, propertyName));
			if (!string.IsNullOrEmpty(this.Owner.DataActionsField))
			{
				IEnumerable<object> enumerable = properties.GetPropertyValue(dataItem, this.Owner.DataActionsField) as IEnumerable<object>;
				if (enumerable != null)
				{
					List<TimelineItemAction> entities = (from x in enumerable
					select new TimelineItemAction
					{
						Owner = this,
						Text = x.GetType().GetProperty("Text").GetValue(x, null).ToString(),
						Url = x.GetType().GetProperty("Url").GetValue(x, null).ToString()
					}).ToList<TimelineItemAction>();
					this.Actions.AddRange(entities);
				}
			}
			if (!string.IsNullOrEmpty(this.Owner.DataImagesField))
			{
				IEnumerable<object> enumerable2 = properties.GetPropertyValue(dataItem, this.Owner.DataImagesField) as IEnumerable<object>;
				if (enumerable2 != null)
				{
					List<TimelineItemImage> entities2 = (from x in enumerable2
					select new TimelineItemImage
					{
						Src = DataBinder.GetPropertyValue(x, "Src", null)
					}).ToList<TimelineItemImage>();
					this.Images.AddRange(entities2);
				}
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x00110302 File Offset: 0x0010E502
		IList IItem.Children
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x00110308 File Offset: 0x0010E508
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			this.LoadChildViewState(array[1]);
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x00110330 File Offset: 0x0010E530
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveChildViewState()
			};
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x00110357 File Offset: 0x0010E557
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackChildViewState();
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x00110368 File Offset: 0x0010E568
		protected void LoadChildViewState(object viewState)
		{
			if (!(viewState is object[]))
			{
				base.LoadViewState(viewState);
				return;
			}
			((IStateManager)this.Actions).LoadViewState(viewState);
			((IStateManager)this.Images).LoadViewState(viewState);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x001103A0 File Offset: 0x0010E5A0
		protected object SaveChildViewState()
		{
			return new object[]
			{
				((IStateManager)this.Actions).SaveViewState(),
				((IStateManager)this.Images).SaveViewState()
			};
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x001103D1 File Offset: 0x0010E5D1
		protected void TrackChildViewState()
		{
			((IStateManager)this.Actions).TrackViewState();
			((IStateManager)this.Images).TrackViewState();
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001103E9 File Offset: 0x0010E5E9
		public string GetAttribute(string key)
		{
			return this.Attributes[key];
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x001103F7 File Offset: 0x0010E5F7
		public void SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x0600595E RID: 22878 RVA: 0x00110406 File Offset: 0x0010E606
		// (set) Token: 0x0600595F RID: 22879 RVA: 0x00110422 File Offset: 0x0010E622
		[Browsable(false)]
		public Dictionary<string, string> Attributes
		{
			get
			{
				return (Dictionary<string, string>)(base.ViewState["Attributes"] ?? null);
			}
			set
			{
				base.ViewState["Attributes"] = value;
			}
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06005960 RID: 22880 RVA: 0x00110435 File Offset: 0x0010E635
		// (set) Token: 0x06005961 RID: 22881 RVA: 0x00110455 File Offset: 0x0010E655
		[DefaultValue("")]
		public virtual string ID
		{
			get
			{
				return (string)(base.ViewState["ID"] ?? string.Empty);
			}
			internal set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06005962 RID: 22882 RVA: 0x00110468 File Offset: 0x0010E668
		// (set) Token: 0x06005963 RID: 22883 RVA: 0x00110488 File Offset: 0x0010E688
		[DefaultValue("")]
		public virtual string Description
		{
			get
			{
				return (string)(base.ViewState["Description"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06005964 RID: 22884 RVA: 0x0011049B File Offset: 0x0010E69B
		// (set) Token: 0x06005965 RID: 22885 RVA: 0x001104C0 File Offset: 0x0010E6C0
		[DefaultValue("")]
		public virtual DateTime Date
		{
			get
			{
				return (DateTime)(base.ViewState["Date"] ?? DateTime.Now);
			}
			set
			{
				base.ViewState["Date"] = value;
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06005966 RID: 22886 RVA: 0x001104D8 File Offset: 0x0010E6D8
		// (set) Token: 0x06005967 RID: 22887 RVA: 0x001104F8 File Offset: 0x0010E6F8
		[DefaultValue("")]
		public virtual string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06005968 RID: 22888 RVA: 0x0011050B File Offset: 0x0010E70B
		// (set) Token: 0x06005969 RID: 22889 RVA: 0x0011052B File Offset: 0x0010E72B
		[DefaultValue("")]
		public virtual string Subtitle
		{
			get
			{
				return (string)(base.ViewState["Subtitle"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Subtitle"] = value;
			}
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x0600596A RID: 22890 RVA: 0x0011053E File Offset: 0x0010E73E
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TimelineItemActionsCollection Actions
		{
			get
			{
				if (this._actions == null)
				{
					this._actions = new TimelineItemActionsCollection(this);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._actions).TrackViewState();
					}
				}
				return this._actions;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x0600596B RID: 22891 RVA: 0x0011056D File Offset: 0x0010E76D
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TimelineItemImagesCollection Images
		{
			get
			{
				if (this._images == null)
				{
					this._images = new TimelineItemImagesCollection(this);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._images).TrackViewState();
					}
				}
				return this._images;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x0600596C RID: 22892 RVA: 0x0011059C File Offset: 0x0010E79C
		// (set) Token: 0x0600596D RID: 22893 RVA: 0x001105A4 File Offset: 0x0010E7A4
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x0600596E RID: 22894 RVA: 0x001105AD File Offset: 0x0010E7AD
		// (set) Token: 0x0600596F RID: 22895 RVA: 0x001105CE File Offset: 0x0010E7CE
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether the item is visible or not.")]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				this.TrackViewState();
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x001105EC File Offset: 0x0010E7EC
		public TimelineItem()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00110607 File Offset: 0x0010E807
		public TimelineItem(DateTime date) : this()
		{
			this.Date = date;
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x00110616 File Offset: 0x0010E816
		public TimelineItem(DateTime date, string title) : this(date)
		{
			this.Title = title;
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00110626 File Offset: 0x0010E826
		public TimelineItem(RadTimeline owner) : this()
		{
			this.Owner = owner;
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x00110638 File Offset: 0x0010E838
		public int CompareTo(object obj)
		{
			TimelineItem timelineItem = obj as TimelineItem;
			if (timelineItem == null)
			{
				throw new ArgumentException();
			}
			TimelineItem timelineItem2 = timelineItem;
			return DateTime.Compare(this.Date, timelineItem2.Date);
		}

		// Token: 0x040015B2 RID: 5554
		private TimelineItemActionsCollection _actions;

		// Token: 0x040015B3 RID: 5555
		private TimelineItemImagesCollection _images;
	}
}
