using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006C6 RID: 1734
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ErrorWebPart : ProxyWebPart, ITrackingPersonalizable
	{
		// Token: 0x06005556 RID: 21846 RVA: 0x00159F15 File Offset: 0x00158F15
		public ErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID) : base(originalID, originalTypeName, originalPath, genericWebPartID)
		{
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06005557 RID: 21847 RVA: 0x00159F22 File Offset: 0x00158F22
		// (set) Token: 0x06005558 RID: 21848 RVA: 0x00159F38 File Offset: 0x00158F38
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

		// Token: 0x06005559 RID: 21849 RVA: 0x00159F44 File Offset: 0x00158F44
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			WebPartZoneBase zone = base.Zone;
			if (zone != null && !zone.ErrorStyle.IsEmpty)
			{
				zone.ErrorStyle.AddAttributesToRender(writer, this);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00159F7C File Offset: 0x00158F7C
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

		// Token: 0x0600555B RID: 21851 RVA: 0x00159FB4 File Offset: 0x00158FB4
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

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x0600555C RID: 21852 RVA: 0x00159FEC File Offset: 0x00158FEC
		bool ITrackingPersonalizable.TracksChanges
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x00159FEF File Offset: 0x00158FEF
		void ITrackingPersonalizable.BeginLoad()
		{
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x00159FF1 File Offset: 0x00158FF1
		void ITrackingPersonalizable.BeginSave()
		{
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00159FF3 File Offset: 0x00158FF3
		void ITrackingPersonalizable.EndLoad()
		{
			this.EndLoadPersonalization();
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x00159FFB File Offset: 0x00158FFB
		void ITrackingPersonalizable.EndSave()
		{
		}

		// Token: 0x04002F1A RID: 12058
		private string _errorMessage;
	}
}
