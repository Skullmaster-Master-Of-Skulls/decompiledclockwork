using System;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200016D RID: 365
	[ClientScriptResource("Sys.Extended.UI.DragDropWatcher", "DropWatcher")]
	[RequiredScript(typeof(DragDropScripts))]
	[TargetControlType(typeof(BulletedList))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxItem(false)]
	public class DropWatcherExtender : ExtenderControlBase
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00018E5C File Offset: 0x0001705C
		private string DataTypeName
		{
			get
			{
				return "HTML_" + this.Parent.ID;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00018E73 File Offset: 0x00017073
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00018E7B File Offset: 0x0001707B
		[ExtenderControlProperty]
		[ClientPropertyName("acceptedDataTypes")]
		[Browsable(false)]
		public string AcceptedDataTypes
		{
			get
			{
				return this.DataTypeName;
			}
			set
			{
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00018E7D File Offset: 0x0001707D
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00018E8F File Offset: 0x0001708F
		[ClientPropertyName("argReplaceString")]
		[ExtenderControlProperty]
		public string ArgReplaceString
		{
			get
			{
				return base.GetPropertyValue<string>("ArgReplaceString", "");
			}
			set
			{
				base.SetPropertyValue<string>("ArgReplaceString", value);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00018E9D File Offset: 0x0001709D
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00018EAF File Offset: 0x000170AF
		[ExtenderControlProperty]
		[ClientPropertyName("argSuccessString")]
		public string ArgSuccessString
		{
			get
			{
				return base.GetPropertyValue<string>("ArgSuccessString", "");
			}
			set
			{
				base.SetPropertyValue<string>("ArgSuccessString", value);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00018EBD File Offset: 0x000170BD
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00018ECF File Offset: 0x000170CF
		[ExtenderControlProperty]
		[ClientPropertyName("argErrorString")]
		public string ArgErrorString
		{
			get
			{
				return base.GetPropertyValue<string>("ArgErrorString", "");
			}
			set
			{
				base.SetPropertyValue<string>("ArgErrorString", value);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00018EDD File Offset: 0x000170DD
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00018EEF File Offset: 0x000170EF
		[ClientPropertyName("argContextString")]
		[ExtenderControlProperty]
		public string ArgContextString
		{
			get
			{
				return base.GetPropertyValue<string>("ArgContextString", "");
			}
			set
			{
				base.SetPropertyValue<string>("ArgContextString", value);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00018EFD File Offset: 0x000170FD
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00018F0F File Offset: 0x0001710F
		[ExtenderControlProperty]
		[ClientPropertyName("callbackCssStyle")]
		public string CallbackCssStyle
		{
			get
			{
				return base.GetPropertyValue<string>("CallbackCssStyle", "");
			}
			set
			{
				base.SetPropertyValue<string>("CallbackCssStyle", value);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00018F1D File Offset: 0x0001711D
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00018F25 File Offset: 0x00017125
		[ClientPropertyName("dragDataType")]
		[Browsable(false)]
		[ExtenderControlProperty]
		public string DataType
		{
			get
			{
				return this.DataTypeName;
			}
			set
			{
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00018F27 File Offset: 0x00017127
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00018F2A File Offset: 0x0001712A
		[ExtenderControlProperty]
		[Browsable(false)]
		[ClientPropertyName("dragMode")]
		public int DragMode
		{
			get
			{
				return 1;
			}
			set
			{
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00018F2C File Offset: 0x0001712C
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00018F3E File Offset: 0x0001713E
		[IDReferenceProperty(typeof(Control))]
		[ExtenderControlProperty]
		[ElementReference]
		[ClientPropertyName("dropCueTemplate")]
		public string DropLayoutElement
		{
			get
			{
				return base.GetPropertyValue<string>("DropLayoutElement", "");
			}
			set
			{
				base.SetPropertyValue<string>("DropLayoutElement", value);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00018F4C File Offset: 0x0001714C
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00018F5E File Offset: 0x0001715E
		[ClientPropertyName("postbackCode")]
		[ExtenderControlProperty]
		public string PostBackCode
		{
			get
			{
				return base.GetPropertyValue<string>("PostbackCode", "");
			}
			set
			{
				base.SetPropertyValue<string>("PostbackCode", value);
			}
		}
	}
}
