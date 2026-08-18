using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000169 RID: 361
	[ToolboxItem(false)]
	[TargetControlType(typeof(Rating))]
	[ClientScriptResource("Sys.Extended.UI.RatingBehavior", "Rating")]
	public class RatingExtender : ExtenderControlBase
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x00018C9E File Offset: 0x00016E9E
		public RatingExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00018CAD File Offset: 0x00016EAD
		[ClientPropertyName("_isServerControl")]
		[ExtenderControlProperty(true, true)]
		public bool IsServerControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00018CB0 File Offset: 0x00016EB0
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00018CBE File Offset: 0x00016EBE
		[ClientPropertyName("autoPostBack")]
		[Browsable(false)]
		[ExtenderControlProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool AutoPostBack
		{
			get
			{
				return base.GetPropertyValue<bool>("AutoPostback", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AutoPostback", value);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00018CCC File Offset: 0x00016ECC
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00018CF4 File Offset: 0x00016EF4
		[ClientPropertyName("rating")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int Rating
		{
			get
			{
				string text = base.ClientState;
				if (text == null)
				{
					text = "0";
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.ClientState = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00018D08 File Offset: 0x00016F08
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00018D1A File Offset: 0x00016F1A
		[DefaultValue("")]
		[ClientPropertyName("callbackID")]
		[ExtenderControlProperty]
		public string CallbackID
		{
			get
			{
				return base.GetPropertyValue<string>("CallbackID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CallbackID", value);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00018D28 File Offset: 0x00016F28
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00018D3A File Offset: 0x00016F3A
		[DefaultValue("")]
		[ClientPropertyName("tag")]
		[ExtenderControlProperty]
		public string Tag
		{
			get
			{
				return base.GetPropertyValue<string>("Tag", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("Tag", value);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00018D48 File Offset: 0x00016F48
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00018D56 File Offset: 0x00016F56
		[ClientPropertyName("ratingDirection")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int RatingDirection
		{
			get
			{
				return base.GetPropertyValue<int>("RatingDirection", 0);
			}
			set
			{
				base.SetPropertyValue<int>("RatingDirection", value);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00018D64 File Offset: 0x00016F64
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00018D72 File Offset: 0x00016F72
		[ClientPropertyName("maxRating")]
		[ExtenderControlProperty]
		[DefaultValue(5)]
		public int MaxRating
		{
			get
			{
				return base.GetPropertyValue<int>("MaxRating", 5);
			}
			set
			{
				base.SetPropertyValue<int>("MaxRating", value);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00018D80 File Offset: 0x00016F80
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00018D92 File Offset: 0x00016F92
		[DefaultValue("")]
		[RequiredProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("starCssClass")]
		public string StarCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("StarCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("StarCssClass", value);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00018DA0 File Offset: 0x00016FA0
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00018DAE File Offset: 0x00016FAE
		[ExtenderControlProperty]
		[ClientPropertyName("readOnly")]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return base.GetPropertyValue<bool>("ReadOnly", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ReadOnly", value);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00018DBC File Offset: 0x00016FBC
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00018DCE File Offset: 0x00016FCE
		[RequiredProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("filledStarCssClass")]
		[DefaultValue("")]
		public string FilledStarCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("FilledStarCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("FilledStarCssClass", value);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00018DDC File Offset: 0x00016FDC
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00018DEE File Offset: 0x00016FEE
		[DefaultValue("")]
		[RequiredProperty]
		[ClientPropertyName("emptyStarCssClass")]
		[ExtenderControlProperty]
		public string EmptyStarCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("EmptyStarCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("EmptyStarCssClass", value);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00018DFC File Offset: 0x00016FFC
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00018E0E File Offset: 0x0001700E
		[ExtenderControlProperty]
		[DefaultValue("")]
		[RequiredProperty]
		[ClientPropertyName("waitingStarCssClass")]
		public string WaitingStarCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("WaitingStarCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("WaitingStarCssClass", value);
			}
		}
	}
}
