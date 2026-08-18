using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000081 RID: 129
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.DynamicPopulateBehavior", "DynamicPopulate")]
	[Designer(typeof(DynamicPopulateExtenderDesigner))]
	[TargetControlType(typeof(HtmlControl))]
	[ToolboxBitmap(typeof(Accessor), "DynamicPopulate.bmp")]
	[TargetControlType(typeof(WebControl))]
	public class DynamicPopulateExtender : ExtenderControlBase
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000C8CE File Offset: 0x0000AACE
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientPropertyName("clearContentsDuringUpdate")]
		[ExtenderControlProperty]
		public bool ClearContentsDuringUpdate
		{
			get
			{
				return base.GetPropertyValue<bool>("ClearContentsDuringUpdate", true);
			}
			set
			{
				base.SetPropertyValue<bool>("ClearContentsDuringUpdate", value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000C8DC File Offset: 0x0000AADC
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000C8EE File Offset: 0x0000AAEE
		[ClientPropertyName("contextKey")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[Category("Behavior")]
		public string ContextKey
		{
			get
			{
				return base.GetPropertyValue<string>("ContextKey", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ContextKey", value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000C90E File Offset: 0x0000AB0E
		[ExtenderControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("populateTriggerID")]
		[IDReferenceProperty(typeof(Control))]
		public string PopulateTriggerControlID
		{
			get
			{
				return base.GetPropertyValue<string>("PopulateTriggerControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopulateTriggerControlID", value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000C91C File Offset: 0x0000AB1C
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000C92E File Offset: 0x0000AB2E
		[ExtenderControlProperty]
		[Category("Behavior")]
		[ClientPropertyName("serviceMethod")]
		[DefaultValue("")]
		public string ServiceMethod
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceMethod", string.Empty);
			}
			set
			{
				if (!string.IsNullOrEmpty(this.CustomScript))
				{
					throw new InvalidOperationException("ServiceMethod can not be set if a CustomScript is set.");
				}
				base.SetPropertyValue<string>("ServiceMethod", value);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000C954 File Offset: 0x0000AB54
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000C966 File Offset: 0x0000AB66
		[ClientPropertyName("servicePath")]
		[UrlProperty]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[TypeConverter(typeof(ServicePathConverter))]
		public string ServicePath
		{
			get
			{
				return base.GetPropertyValue<string>("ServicePath", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ServicePath", value);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000C974 File Offset: 0x0000AB74
		private bool ShouldSerializeServicePath()
		{
			return !string.IsNullOrEmpty(this.ServiceMethod);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000C984 File Offset: 0x0000AB84
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000C996 File Offset: 0x0000AB96
		[Category("Behavior")]
		[ClientPropertyName("updatingCssClass")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string UpdatingCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("UpdatingCss", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UpdatingCss", value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientPropertyName("customScript")]
		[ExtenderControlProperty]
		public string CustomScript
		{
			get
			{
				return base.GetPropertyValue<string>("CustomScript", string.Empty);
			}
			set
			{
				if (!string.IsNullOrEmpty(this.ServiceMethod))
				{
					throw new InvalidOperationException("CustomScript can not be set if a ServiceMethod is set.");
				}
				base.SetPropertyValue<string>("CustomScript", value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		[Category("Behavior")]
		[DefaultValue(false)]
		[ClientPropertyName("cacheDynamicResults")]
		[ExtenderControlProperty]
		public bool CacheDynamicResults
		{
			get
			{
				return base.GetPropertyValue<bool>("CacheDynamicResults", false);
			}
			set
			{
				base.SetPropertyValue<bool>("CacheDynamicResults", value);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		protected override bool CheckIfValid(bool throwException)
		{
			if (!string.IsNullOrEmpty(this.CustomScript) || !string.IsNullOrEmpty(this.ServiceMethod))
			{
				return base.CheckIfValid(throwException);
			}
			if (throwException)
			{
				throw new InvalidOperationException("CustomScript or ServiceMethod must be set.");
			}
			return false;
		}
	}
}
