using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001954 RID: 6484
	public class RadDataPagerButtonField : RadDataPagerButtonFieldBase
	{
		// Token: 0x17004BD1 RID: 19409
		// (get) Token: 0x0600FAE5 RID: 64229 RVA: 0x0038813C File Offset: 0x0038633C
		// (set) Token: 0x0600FAE6 RID: 64230 RVA: 0x00388165 File Offset: 0x00386365
		[TypeConverter(typeof(EnumConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue("PrevNext")]
		public PagerButtonFieldType FieldType
		{
			get
			{
				object obj = base.ViewState["PagerButtonFieldType"];
				if (obj != null)
				{
					return (PagerButtonFieldType)obj;
				}
				return PagerButtonFieldType.PrevNext;
			}
			set
			{
				base.ViewState["PagerButtonFieldType"] = value;
			}
		}

		// Token: 0x17004BD2 RID: 19410
		// (get) Token: 0x0600FAE7 RID: 64231 RVA: 0x0038817D File Offset: 0x0038637D
		// (set) Token: 0x0600FAE8 RID: 64232 RVA: 0x003881B3 File Offset: 0x003863B3
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string NextButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["NextButtonText"], string.Empty) ?? base.Owner.Localization.NextButtonText;
			}
			set
			{
				base.ViewState["NextButtonText"] = value;
			}
		}

		// Token: 0x17004BD3 RID: 19411
		// (get) Token: 0x0600FAE9 RID: 64233 RVA: 0x003881C6 File Offset: 0x003863C6
		// (set) Token: 0x0600FAEA RID: 64234 RVA: 0x003881FC File Offset: 0x003863FC
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public string PrevButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["PrevButtonText"], string.Empty) ?? base.Owner.Localization.PrevButtonText;
			}
			set
			{
				base.ViewState["PrevButtonText"] = value;
			}
		}

		// Token: 0x17004BD4 RID: 19412
		// (get) Token: 0x0600FAEB RID: 64235 RVA: 0x0038820F File Offset: 0x0038640F
		// (set) Token: 0x0600FAEC RID: 64236 RVA: 0x00388245 File Offset: 0x00386445
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string FirstButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["FirstButtonText"], string.Empty) ?? base.Owner.Localization.FirstButtonText;
			}
			set
			{
				base.ViewState["FirstButtonText"] = value;
			}
		}

		// Token: 0x17004BD5 RID: 19413
		// (get) Token: 0x0600FAED RID: 64237 RVA: 0x00388258 File Offset: 0x00386458
		// (set) Token: 0x0600FAEE RID: 64238 RVA: 0x0038828E File Offset: 0x0038648E
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public string LastButtonText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["LastButtonText"], string.Empty) ?? base.Owner.Localization.LastButtonText;
			}
			set
			{
				base.ViewState["LastButtonText"] = value;
			}
		}

		// Token: 0x17004BD6 RID: 19414
		// (get) Token: 0x0600FAEF RID: 64239 RVA: 0x003882A4 File Offset: 0x003864A4
		// (set) Token: 0x0600FAF0 RID: 64240 RVA: 0x003882E2 File Offset: 0x003864E2
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string FirstButtonImageUrl
		{
			get
			{
				string result = string.Empty;
				object obj = base.ViewState["FirstButtonImageUrl"];
				if (obj != null)
				{
					result = base.ViewState["FirstButtonImageUrl"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["FirstButtonImageUrl"] = value;
			}
		}

		// Token: 0x17004BD7 RID: 19415
		// (get) Token: 0x0600FAF1 RID: 64241 RVA: 0x003882F8 File Offset: 0x003864F8
		// (set) Token: 0x0600FAF2 RID: 64242 RVA: 0x00388336 File Offset: 0x00386536
		[UrlProperty]
		[NotifyParentProperty(true)]
		public virtual string LastButtonImageUrl
		{
			get
			{
				string result = string.Empty;
				object obj = base.ViewState["LastButtonImageUrl"];
				if (obj != null)
				{
					result = base.ViewState["LastButtonImageUrl"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["LastButtonImageUrl"] = value;
			}
		}

		// Token: 0x17004BD8 RID: 19416
		// (get) Token: 0x0600FAF3 RID: 64243 RVA: 0x0038834C File Offset: 0x0038654C
		// (set) Token: 0x0600FAF4 RID: 64244 RVA: 0x0038838A File Offset: 0x0038658A
		[NotifyParentProperty(true)]
		[UrlProperty]
		public virtual string PrevButtonImageUrl
		{
			get
			{
				string result = string.Empty;
				object obj = base.ViewState["PrevButtonImageUrl"];
				if (obj != null)
				{
					result = base.ViewState["PrevButtonImageUrl"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["PrevButtonImageUrl"] = value;
			}
		}

		// Token: 0x17004BD9 RID: 19417
		// (get) Token: 0x0600FAF5 RID: 64245 RVA: 0x003883A0 File Offset: 0x003865A0
		// (set) Token: 0x0600FAF6 RID: 64246 RVA: 0x003883DE File Offset: 0x003865DE
		[UrlProperty]
		[NotifyParentProperty(true)]
		public virtual string NextButtonImageUrl
		{
			get
			{
				string result = string.Empty;
				object obj = base.ViewState["NextButtonImageUrl"];
				if (obj != null)
				{
					result = base.ViewState["NextButtonImageUrl"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["NextButtonImageUrl"] = value;
			}
		}

		// Token: 0x17004BDA RID: 19418
		// (get) Token: 0x0600FAF7 RID: 64247 RVA: 0x003883F4 File Offset: 0x003865F4
		// (set) Token: 0x0600FAF8 RID: 64248 RVA: 0x0038841E File Offset: 0x0038661E
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		public int PageButtonCount
		{
			get
			{
				object obj = base.ViewState["PageButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["PageButtonCount"] = value;
			}
		}

		// Token: 0x0600FAF9 RID: 64249 RVA: 0x00388445 File Offset: 0x00386645
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			this.CreateFieldControls(inItem);
		}

		// Token: 0x0600FAFA RID: 64250 RVA: 0x00388450 File Offset: 0x00386650
		protected virtual Control CreatePrevButton(PagerFieldButtonType buttonType)
		{
			WebControl webControl = base.CreateButtonField(buttonType, string.Empty, this.PrevButtonText, "Page", "Prev", "rdpPagePrev", this.PrevButtonImageUrl, "Previous Page");
			webControl.ID = "PrevButton";
			return webControl;
		}

		// Token: 0x0600FAFB RID: 64251 RVA: 0x00388498 File Offset: 0x00386698
		protected virtual Control CreatenNextButton(PagerFieldButtonType buttonType)
		{
			WebControl webControl = base.CreateButtonField(buttonType, string.Empty, this.NextButtonText, "Page", "Next", "rdpPageNext", this.NextButtonImageUrl, "Next Page");
			webControl.ID = "NextButton";
			return webControl;
		}

		// Token: 0x0600FAFC RID: 64252 RVA: 0x003884E0 File Offset: 0x003866E0
		protected virtual Control CreatenFirstButton(PagerFieldButtonType buttonType)
		{
			WebControl webControl = base.CreateButtonField(buttonType, string.Empty, this.FirstButtonText, "Page", "First", "rdpPageFirst", this.FirstButtonImageUrl, "First Page");
			webControl.ID = "FirstButton";
			return webControl;
		}

		// Token: 0x0600FAFD RID: 64253 RVA: 0x00388528 File Offset: 0x00386728
		protected virtual Control CreatenLastButton(PagerFieldButtonType buttonType)
		{
			WebControl webControl = base.CreateButtonField(buttonType, string.Empty, this.LastButtonText, "Page", "Last", "rdpPageLast", this.LastButtonImageUrl, "Last Page");
			webControl.ID = "LastButton";
			return webControl;
		}

		// Token: 0x0600FAFE RID: 64254 RVA: 0x0038856E File Offset: 0x0038676E
		internal static bool IsInRange(int value, int compareValue, int offset)
		{
			return value >= compareValue - offset && value <= compareValue + offset;
		}

		// Token: 0x0600FAFF RID: 64255 RVA: 0x00388584 File Offset: 0x00386784
		protected virtual Control CreateNumericButton(PagerFieldButtonType buttonType, string text, int commandArgument, int startIndex, int endIndex)
		{
			int currentPageIndex = base.Owner.CurrentPageIndex;
			string text2 = (currentPageIndex == commandArgument) ? "rdpCurrentPage" : string.Empty;
			if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				int offset = (currentPageIndex == startIndex || currentPageIndex == endIndex - 1) ? 2 : 1;
				if (text == "...")
				{
					text2 = RadDataPagerButtonField.AppendStyle("rdpSkipPages", text2);
				}
				else if (startIndex > -1 && endIndex > -1 && !RadDataPagerButtonField.IsInRange(commandArgument, currentPageIndex, offset))
				{
					text2 = RadDataPagerButtonField.AppendStyle("rdpHiddentItem", text2);
				}
			}
			return base.CreateButtonField(buttonType, text, string.Empty, "Page", commandArgument.ToString(), text2, string.Empty, commandArgument.ToString());
		}

		// Token: 0x0600FB00 RID: 64256 RVA: 0x0038863E File Offset: 0x0038683E
		private static string AppendStyle(string newClass, string originalClass)
		{
			if (!string.IsNullOrEmpty(originalClass))
			{
				return originalClass;
			}
			return string.Format("{0} {1}", originalClass, newClass);
		}

		// Token: 0x0600FB01 RID: 64257 RVA: 0x00388658 File Offset: 0x00386858
		private void CreateFieldControls(RadDataPagerFieldItem itemContainer)
		{
			switch (this.FieldType)
			{
			case PagerButtonFieldType.Next:
				itemContainer.Controls.Add(this.CreatenNextButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.Prev:
				itemContainer.Controls.Add(this.CreatePrevButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.First:
				itemContainer.Controls.Add(this.CreatenFirstButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.Last:
				itemContainer.Controls.Add(this.CreatenLastButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.PrevNext:
				itemContainer.Controls.Add(this.CreatePrevButton(PagerFieldButtonType.PushButton));
				itemContainer.Controls.Add(this.CreatenNextButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.FirstPrev:
				itemContainer.Controls.Add(this.CreatenFirstButton(PagerFieldButtonType.PushButton));
				itemContainer.Controls.Add(this.CreatePrevButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.NextLast:
				itemContainer.Controls.Add(this.CreatenNextButton(PagerFieldButtonType.PushButton));
				itemContainer.Controls.Add(this.CreatenLastButton(PagerFieldButtonType.PushButton));
				return;
			case PagerButtonFieldType.Numeric:
			{
				PagerFieldButtonType buttonType = PagerFieldButtonType.LinkButton;
				int num = (base.Owner.CurrentPageIndex + 1) / this.PageButtonCount + (((base.Owner.CurrentPageIndex + 1) % this.PageButtonCount == 0) ? 0 : 1) - 1;
				num = Math.Max(num, 0) * this.PageButtonCount;
				if (base.Owner.CurrentPageIndex + 1 > this.PageButtonCount)
				{
					itemContainer.Controls.Add(this.CreateNumericButton(buttonType, "...", num - 1, -1, -1));
				}
				int num2 = Math.Min(num + this.PageButtonCount, base.Owner.PageCount);
				for (int i = num; i < num2; i++)
				{
					itemContainer.Controls.Add(this.CreateNumericButton(buttonType, (i + 1).ToString(), i, num, num2));
				}
				if (num2 < base.Owner.PageCount)
				{
					itemContainer.Controls.Add(this.CreateNumericButton(buttonType, "...", num2, -1, -1));
				}
				if (base.Owner.PageCount == 0)
				{
					itemContainer.Controls.Add(this.CreateNumericButton(buttonType, "1", 0, -1, -1));
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x17004BDB RID: 19419
		// (get) Token: 0x0600FB02 RID: 64258 RVA: 0x0038885C File Offset: 0x00386A5C
		// (set) Token: 0x0600FB03 RID: 64259 RVA: 0x00388885 File Offset: 0x00386A85
		public bool TrimXs
		{
			get
			{
				object obj = base.ViewState["TXS"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["TXS"] = value;
			}
		}

		// Token: 0x17004BDC RID: 19420
		// (get) Token: 0x0600FB04 RID: 64260 RVA: 0x003888A0 File Offset: 0x00386AA0
		// (set) Token: 0x0600FB05 RID: 64261 RVA: 0x003888C9 File Offset: 0x00386AC9
		public bool TrimSm
		{
			get
			{
				object obj = base.ViewState["TSM"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["TSM"] = value;
			}
		}

		// Token: 0x17004BDD RID: 19421
		// (get) Token: 0x0600FB06 RID: 64262 RVA: 0x003888E4 File Offset: 0x00386AE4
		// (set) Token: 0x0600FB07 RID: 64263 RVA: 0x0038890D File Offset: 0x00386B0D
		public bool TrimMd
		{
			get
			{
				object obj = base.ViewState["TMD"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["TMD"] = value;
			}
		}

		// Token: 0x17004BDE RID: 19422
		// (get) Token: 0x0600FB08 RID: 64264 RVA: 0x00388928 File Offset: 0x00386B28
		// (set) Token: 0x0600FB09 RID: 64265 RVA: 0x00388951 File Offset: 0x00386B51
		public bool TrimLg
		{
			get
			{
				object obj = base.ViewState["TLG"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["TLG"] = value;
			}
		}

		// Token: 0x17004BDF RID: 19423
		// (get) Token: 0x0600FB0A RID: 64266 RVA: 0x0038896C File Offset: 0x00386B6C
		// (set) Token: 0x0600FB0B RID: 64267 RVA: 0x00388995 File Offset: 0x00386B95
		public bool TrimXl
		{
			get
			{
				object obj = base.ViewState["TXL"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["TXL"] = value;
			}
		}

		// Token: 0x04004760 RID: 18272
		private const string AdaptiveHorizontalIndentClass = "rdpHiddentItem";

		// Token: 0x04004761 RID: 18273
		private const string AdaptiveSkipPagesClass = "rdpSkipPages";
	}
}
