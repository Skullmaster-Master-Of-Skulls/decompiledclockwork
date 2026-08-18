using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCC RID: 4044
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class AjaxSetting
	{
		// Token: 0x06009D01 RID: 40193 RVA: 0x0022F15C File Offset: 0x0022D35C
		public AjaxSetting()
		{
		}

		// Token: 0x06009D02 RID: 40194 RVA: 0x0022F17A File Offset: 0x0022D37A
		public AjaxSetting(string controlID)
		{
			this.AjaxControlID = controlID;
		}

		// Token: 0x170031B5 RID: 12725
		// (get) Token: 0x06009D03 RID: 40195 RVA: 0x0022F19F File Offset: 0x0022D39F
		// (set) Token: 0x06009D04 RID: 40196 RVA: 0x0022F1A7 File Offset: 0x0022D3A7
		public string AjaxControlID
		{
			get
			{
				return this.ajaxControlID;
			}
			set
			{
				this.ajaxControlID = value;
			}
		}

		// Token: 0x170031B6 RID: 12726
		// (get) Token: 0x06009D05 RID: 40197 RVA: 0x0022F1B0 File Offset: 0x0022D3B0
		// (set) Token: 0x06009D06 RID: 40198 RVA: 0x0022F1B8 File Offset: 0x0022D3B8
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		public string EventName
		{
			get
			{
				return this.eventName;
			}
			set
			{
				this.eventName = value;
			}
		}

		// Token: 0x06009D07 RID: 40199 RVA: 0x0022F1C4 File Offset: 0x0022D3C4
		internal string SerializeToJavascript(RadAjaxManager manager)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("InitControlID : \"{0}\",", manager.ResolveClientID(this.AjaxControlID));
			stringBuilder.AppendFormat("UpdatedControls : {0}", this.UpdatedControls.SerializeToJavascript(manager));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x170031B7 RID: 12727
		// (get) Token: 0x06009D08 RID: 40200 RVA: 0x0022F225 File Offset: 0x0022D425
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AjaxUpdatedControlsCollection UpdatedControls
		{
			get
			{
				if (this.updatedControls == null)
				{
					this.updatedControls = new AjaxUpdatedControlsCollection();
				}
				return this.updatedControls;
			}
		}

		// Token: 0x04002C36 RID: 11318
		private string ajaxControlID = string.Empty;

		// Token: 0x04002C37 RID: 11319
		private AjaxUpdatedControlsCollection updatedControls;

		// Token: 0x04002C38 RID: 11320
		private string eventName = string.Empty;
	}
}
