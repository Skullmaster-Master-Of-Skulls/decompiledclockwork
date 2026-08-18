using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000054 RID: 84
	[RequiredScript(typeof(DynamicPopulateExtender))]
	public abstract class DynamicPopulateExtenderControlBase : AnimationExtenderControlBase
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009A53 File Offset: 0x00007C53
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00009A65 File Offset: 0x00007C65
		[DefaultValue("")]
		[IDReferenceProperty(typeof(WebControl))]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[ClientPropertyName("dynamicControlID")]
		public string DynamicControlID
		{
			get
			{
				return base.GetPropertyValue<string>("DynamicControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DynamicControlID", value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009A73 File Offset: 0x00007C73
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009A85 File Offset: 0x00007C85
		[Category("Behavior")]
		[ExtenderControlProperty]
		[ClientPropertyName("dynamicContextKey")]
		[DefaultValue("")]
		public string DynamicContextKey
		{
			get
			{
				return base.GetPropertyValue<string>("DynamicContextKey", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DynamicContextKey", value);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009A93 File Offset: 0x00007C93
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00009AA5 File Offset: 0x00007CA5
		[Category("Behavior")]
		[TypeConverter(typeof(ServicePathConverter))]
		[ExtenderControlProperty]
		[UrlProperty]
		[ClientPropertyName("dynamicServicePath")]
		public string DynamicServicePath
		{
			get
			{
				return base.GetPropertyValue<string>("DynamicServicePath", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DynamicServicePath", value);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009AB3 File Offset: 0x00007CB3
		private bool ShouldSerializeServicePath()
		{
			return !string.IsNullOrEmpty(this.DynamicServiceMethod);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009AC3 File Offset: 0x00007CC3
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00009AD5 File Offset: 0x00007CD5
		[Category("Behavior")]
		[ClientPropertyName("dynamicServiceMethod")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string DynamicServiceMethod
		{
			get
			{
				return base.GetPropertyValue<string>("DynamicServiceMethod", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DynamicServiceMethod", value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009AE3 File Offset: 0x00007CE3
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00009AF1 File Offset: 0x00007CF1
		[Category("Behavior")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("cacheDynamicResults")]
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

		// Token: 0x060002F1 RID: 753 RVA: 0x00009B00 File Offset: 0x00007D00
		public override void EnsureValid()
		{
			base.EnsureValid();
			if (!string.IsNullOrEmpty(this.DynamicControlID) || !string.IsNullOrEmpty(this.DynamicContextKey) || !string.IsNullOrEmpty(this.DynamicServicePath) || !string.IsNullOrEmpty(this.DynamicServiceMethod))
			{
				if (string.IsNullOrEmpty(this.DynamicControlID))
				{
					throw new ArgumentException("DynamicControlID must be set");
				}
				if (string.IsNullOrEmpty(this.DynamicServiceMethod))
				{
					throw new ArgumentException("DynamicServiceMethod must be set");
				}
			}
		}
	}
}
