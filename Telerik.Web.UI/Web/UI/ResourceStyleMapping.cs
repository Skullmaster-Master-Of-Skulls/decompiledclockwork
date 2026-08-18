using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001A1C RID: 6684
	[DefaultProperty("ApplyCssClass")]
	public class ResourceStyleMapping : StateManager
	{
		// Token: 0x0601038C RID: 66444 RVA: 0x003A0644 File Offset: 0x0039E844
		public ResourceStyleMapping()
		{
		}

		// Token: 0x0601038D RID: 66445 RVA: 0x003A064C File Offset: 0x0039E84C
		public ResourceStyleMapping(string type, string key, string applyCssClass) : this(type, key, string.Empty, applyCssClass)
		{
		}

		// Token: 0x0601038E RID: 66446 RVA: 0x003A065C File Offset: 0x0039E85C
		public ResourceStyleMapping(string type, string key, string text, string applyCssClass)
		{
			this.Type = type;
			this.Key = key;
			this.Text = text;
			this.ApplyCssClass = applyCssClass;
		}

		// Token: 0x17004E8D RID: 20109
		// (get) Token: 0x0601038F RID: 66447 RVA: 0x003A0681 File Offset: 0x0039E881
		// (set) Token: 0x06010390 RID: 66448 RVA: 0x003A06A1 File Offset: 0x0039E8A1
		public string Key
		{
			get
			{
				return (string)(base.ViewState["Key"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x17004E8E RID: 20110
		// (get) Token: 0x06010391 RID: 66449 RVA: 0x003A06B4 File Offset: 0x0039E8B4
		// (set) Token: 0x06010392 RID: 66450 RVA: 0x003A06D4 File Offset: 0x0039E8D4
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17004E8F RID: 20111
		// (get) Token: 0x06010393 RID: 66451 RVA: 0x003A06E7 File Offset: 0x0039E8E7
		// (set) Token: 0x06010394 RID: 66452 RVA: 0x003A0707 File Offset: 0x0039E907
		public string Type
		{
			get
			{
				return (string)(base.ViewState["Type"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17004E90 RID: 20112
		// (get) Token: 0x06010395 RID: 66453 RVA: 0x003A071A File Offset: 0x0039E91A
		// (set) Token: 0x06010396 RID: 66454 RVA: 0x003A073A File Offset: 0x0039E93A
		public string ApplyCssClass
		{
			get
			{
				return (string)(base.ViewState["ApplyCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ApplyCssClass"] = value;
			}
		}

		// Token: 0x17004E91 RID: 20113
		// (get) Token: 0x06010397 RID: 66455 RVA: 0x003A074D File Offset: 0x0039E94D
		// (set) Token: 0x06010398 RID: 66456 RVA: 0x003A0772 File Offset: 0x0039E972
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		public virtual Color BackColor
		{
			get
			{
				return (Color)(base.ViewState["BackColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BackColor"] = value;
			}
		}

		// Token: 0x17004E92 RID: 20114
		// (get) Token: 0x06010399 RID: 66457 RVA: 0x003A078A File Offset: 0x0039E98A
		// (set) Token: 0x0601039A RID: 66458 RVA: 0x003A07AF File Offset: 0x0039E9AF
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		public virtual Color BorderColor
		{
			get
			{
				return (Color)(base.ViewState["BorderColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BorderColor"] = value;
			}
		}
	}
}
