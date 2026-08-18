using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200053D RID: 1341
	[ToolboxItem(false)]
	public class ErrorWebPart : ProxyWebPart, ITrackingPersonalizable
	{
		// Token: 0x06004480 RID: 17536 RVA: 0x000E3034 File Offset: 0x000E1234
		public ErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID) : base(originalID, originalTypeName, originalPath, genericWebPartID)
		{
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x000E3041 File Offset: 0x000E1241
		// (set) Token: 0x06004482 RID: 17538 RVA: 0x000E3057 File Offset: 0x000E1257
		public string ErrorMessage
		{
			get
			{
				if (this._errorMessage == null)
				{
					return string.Empty;
				}
				return this._errorMessage;
			}
			set
			{
				this._errorMessage = value;
			}
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000E3060 File Offset: 0x000E1260
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			WebPartZoneBase zone = base.Zone;
			if (zone != null && !zone.ErrorStyle.IsEmpty)
			{
				zone.ErrorStyle.AddAttributesToRender(writer, this);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x000E3098 File Offset: 0x000E1298
		protected virtual void EndLoadPersonalization()
		{
			this.AllowEdit = false;
			this.ChromeState = PartChromeState.Normal;
			this.Hidden = false;
			this.AllowHide = false;
			this.AllowMinimize = false;
			this.ExportMode = WebPartExportMode.None;
			this.AuthorizationFilter = string.Empty;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x000E30D0 File Offset: 0x000E12D0
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string errorMessage = this.ErrorMessage;
			if (!string.IsNullOrEmpty(errorMessage))
			{
				writer.WriteEncodedText(SR.GetString("ErrorWebPart_ErrorText", new object[]
				{
					errorMessage
				}));
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x000097B7 File Offset: 0x000079B7
		bool ITrackingPersonalizable.TracksChanges
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x00006164 File Offset: 0x00004364
		void ITrackingPersonalizable.BeginLoad()
		{
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x00006164 File Offset: 0x00004364
		void ITrackingPersonalizable.BeginSave()
		{
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x000E3106 File Offset: 0x000E1306
		void ITrackingPersonalizable.EndLoad()
		{
			this.EndLoadPersonalization();
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x00006164 File Offset: 0x00004364
		void ITrackingPersonalizable.EndSave()
		{
		}

		// Token: 0x04002637 RID: 9783
		private string _errorMessage;
	}
}
