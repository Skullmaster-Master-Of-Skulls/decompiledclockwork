using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011B5 RID: 4533
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class InputIncrementSettings
	{
		// Token: 0x0600BA3E RID: 47678 RVA: 0x002977EF File Offset: 0x002959EF
		public InputIncrementSettings(StateBag viewStateOwner)
		{
			this._viewStateOwner = new InputStateBag("inp_inc_", viewStateOwner);
			this._ownerStateBag = viewStateOwner;
		}

		// Token: 0x17003C0B RID: 15371
		// (get) Token: 0x0600BA3F RID: 47679 RVA: 0x0029780F File Offset: 0x00295A0F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public InputStateBag ViewState
		{
			get
			{
				return this._viewStateOwner;
			}
		}

		// Token: 0x17003C0C RID: 15372
		// (get) Token: 0x0600BA40 RID: 47680 RVA: 0x00297817 File Offset: 0x00295A17
		protected StateBag ViewStateOwner
		{
			get
			{
				return this._ownerStateBag;
			}
		}

		// Token: 0x0600BA41 RID: 47681 RVA: 0x0029781F File Offset: 0x00295A1F
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x17003C0D RID: 15373
		// (get) Token: 0x0600BA42 RID: 47682 RVA: 0x00297828 File Offset: 0x00295A28
		// (set) Token: 0x0600BA43 RID: 47683 RVA: 0x00297859 File Offset: 0x00295A59
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Step")]
		[DefaultValue(typeof(double), "1")]
		[Description("")]
		public virtual double Step
		{
			get
			{
				object obj = this.ViewState["Step"];
				if (obj != null)
				{
					return (double)obj;
				}
				return 1.0;
			}
			set
			{
				this.ViewState["Step"] = value;
			}
		}

		// Token: 0x17003C0E RID: 15374
		// (get) Token: 0x0600BA44 RID: 47684 RVA: 0x00297874 File Offset: 0x00295A74
		// (set) Token: 0x0600BA45 RID: 47685 RVA: 0x0029789D File Offset: 0x00295A9D
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(true)]
		public virtual bool InterceptArrowKeys
		{
			get
			{
				object obj = this.ViewState["InterceptArrowKeys"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["InterceptArrowKeys"] = value;
			}
		}

		// Token: 0x17003C0F RID: 15375
		// (get) Token: 0x0600BA46 RID: 47686 RVA: 0x002978B8 File Offset: 0x00295AB8
		// (set) Token: 0x0600BA47 RID: 47687 RVA: 0x002978E1 File Offset: 0x00295AE1
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("")]
		public virtual bool InterceptMouseWheel
		{
			get
			{
				object obj = this.ViewState["InterceptMouseWheel"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["InterceptMouseWheel"] = value;
			}
		}

		// Token: 0x04003138 RID: 12600
		private InputStateBag _viewStateOwner;

		// Token: 0x04003139 RID: 12601
		private StateBag _ownerStateBag;
	}
}
