using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200081E RID: 2078
	public abstract class ViewSettings : ObjectWithState
	{
		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x000F126E File Offset: 0x000EF46E
		internal IScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x000F1276 File Offset: 0x000EF476
		internal ViewSettings(IScheduler owner, string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
			this._owner = owner;
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x000F1287 File Offset: 0x000EF487
		internal bool ReadOnlyResolved
		{
			get
			{
				if (base.ViewState["ReadOnly"] == null)
				{
					return this.Owner.ReadOnly;
				}
				return this.ReadOnly;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x000F12AD File Offset: 0x000EF4AD
		internal virtual bool ShowDateHeadersResolved
		{
			get
			{
				if (base.ViewState["ShowDateHeaders"] == null)
				{
					return this.Owner.ShowDateHeaders;
				}
				return this.ShowDateHeaders;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x000F12D4 File Offset: 0x000EF4D4
		// (set) Token: 0x06004CBD RID: 19645 RVA: 0x000F12FD File Offset: 0x000EF4FD
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("readOnly")]
		[Description("Make the view read-only.")]
		[NotifyParentProperty(true)]
		public bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x000F1320 File Offset: 0x000EF520
		// (set) Token: 0x06004CBF RID: 19647 RVA: 0x000F1349 File Offset: 0x000EF549
		[Category("Appearance")]
		[Description("Controls the visibility of the date headers for the current view")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool ShowDateHeaders
		{
			get
			{
				object obj = base.ViewState["ShowDateHeaders"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowDateHeaders"] = value;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x000F1364 File Offset: 0x000EF564
		// (set) Token: 0x06004CC1 RID: 19649 RVA: 0x000F138D File Offset: 0x000EF58D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Controls the visibility of the tab for the current view in the view chooser")]
		public virtual bool UserSelectable
		{
			get
			{
				object obj = base.ViewState["UserSelectable"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["UserSelectable"] = value;
			}
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x000F13A5 File Offset: 0x000EF5A5
		internal virtual JavaScriptConverter GetConverter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x000F13AC File Offset: 0x000EF5AC
		internal void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			JavaScriptConverter converter = this.GetConverter();
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				converter
			});
			IDictionary<string, object> dictionary = converter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}

		// Token: 0x0400133F RID: 4927
		private readonly IScheduler _owner;
	}
}
