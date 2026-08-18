using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AC RID: 172
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ContentDesigner : ControlDesigner
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001902C File Offset: 0x0001722C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ContentDesigner.ContentDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00019059 File Offset: 0x00017259
		private IContentResolutionService ContentResolutionService
		{
			get
			{
				if (this._contentResolutionService == null)
				{
					this._contentResolutionService = (IContentResolutionService)this.GetService(typeof(IContentResolutionService));
				}
				return this._contentResolutionService;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00019084 File Offset: 0x00017284
		private void ClearRegion()
		{
			if (this.ContentResolutionService != null && this.GetContentDefinition() != null)
			{
				this.ContentResolutionService.SetContentDesignerState(this.GetContentDefinition().ContentPlaceHolderID, ContentDesignerState.ShowDefaultContent);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000190AD File Offset: 0x000172AD
		private void CreateBlankContent()
		{
			if (this.ContentResolutionService != null && this.GetContentDefinition() != null)
			{
				this.ContentResolutionService.SetContentDesignerState(this.GetContentDefinition().ContentPlaceHolderID, ContentDesignerState.ShowUserContent);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000190D8 File Offset: 0x000172D8
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			EditableDesignerRegion region = new EditableDesignerRegion(this, "Content");
			regions.Add(region);
			Font captionFont = SystemFonts.CaptionFont;
			Color controlText = SystemColors.ControlText;
			Color control = SystemColors.Control;
			string text = base.Component.GetType().Name + " - " + base.Component.Site.Name;
			return string.Format(CultureInfo.InvariantCulture, "<table cellspacing=0 cellpadding=0 style=\"border:1px solid black; width:100%; height:200px\">\r\n            <tr>\r\n              <td style=\"width:100%; height:25px; font-family:Tahoma; font-size:{2}pt; color:{3}; background-color:{4}; padding:5px; border-bottom:1px solid black;\">\r\n                &nbsp;{0}\r\n              </td>\r\n            </tr>\r\n            <tr>\r\n              <td style=\"width:100%; height:175px; vertical-align:top;\" {1}=\"0\">\r\n              </td>\r\n            </tr>\r\n          </table>", new object[]
			{
				text,
				DesignerRegion.DesignerRegionAttributeName,
				captionFont.SizeInPoints,
				ColorTranslator.ToHtml(controlText),
				ColorTranslator.ToHtml(control)
			});
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00019179 File Offset: 0x00017379
		public override string GetPersistenceContent()
		{
			return this._content;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00019184 File Offset: 0x00017384
		private ContentDefinition GetContentDefinition()
		{
			if (this._contentDefinition == null)
			{
				try
				{
					ContentDefinition contentDefinition = (ContentDefinition)this.ContentResolutionService.ContentDefinitions[((Content)base.Component).ContentPlaceHolderID];
					this._contentDefinition = new ContentDefinition(contentDefinition.ContentPlaceHolderID, contentDefinition.DefaultContent, contentDefinition.DefaultDesignTimeHtml);
				}
				catch
				{
				}
			}
			return this._contentDefinition;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000191F8 File Offset: 0x000173F8
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			if (this._content == null)
			{
				this._content = base.Tag.GetContent();
			}
			if (this._content == null)
			{
				return string.Empty;
			}
			return this._content;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00019227 File Offset: 0x00017427
		protected override void PreFilterEvents(IDictionary events)
		{
			events.Clear();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00019230 File Offset: 0x00017430
		protected override void PostFilterProperties(IDictionary properties)
		{
			base.PostFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["ID"];
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["ContentPlaceHolderID"];
			properties.Clear();
			ContentDesignerState contentDesignerState = ContentDesignerState.ShowDefaultContent;
			ContentDefinition contentDefinition = this.GetContentDefinition();
			if (this.ContentResolutionService != null && contentDefinition != null)
			{
				contentDesignerState = this.ContentResolutionService.GetContentDesignerState(contentDefinition.ContentPlaceHolderID);
			}
			propertyDescriptor = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
			{
				(contentDesignerState == ContentDesignerState.ShowDefaultContent) ? ReadOnlyAttribute.Yes : ReadOnlyAttribute.No
			});
			properties.Add("ID", propertyDescriptor);
			propertyDescriptor2 = TypeDescriptor.CreateProperty(propertyDescriptor2.ComponentType, propertyDescriptor2, new Attribute[]
			{
				ReadOnlyAttribute.Yes
			});
			properties.Add("ContentPlaceHolderID", propertyDescriptor2);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000192EA File Offset: 0x000174EA
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			if (string.Compare(this._content, content, StringComparison.Ordinal) != 0)
			{
				this._content = content;
				base.Tag.SetDirty(true);
			}
		}

		// Token: 0x04000285 RID: 645
		private const string _designtimeHTML = "<table cellspacing=0 cellpadding=0 style=\"border:1px solid black; width:100%; height:200px\">\r\n            <tr>\r\n              <td style=\"width:100%; height:25px; font-family:Tahoma; font-size:{2}pt; color:{3}; background-color:{4}; padding:5px; border-bottom:1px solid black;\">\r\n                &nbsp;{0}\r\n              </td>\r\n            </tr>\r\n            <tr>\r\n              <td style=\"width:100%; height:175px; vertical-align:top;\" {1}=\"0\">\r\n              </td>\r\n            </tr>\r\n          </table>";

		// Token: 0x04000286 RID: 646
		private string _content;

		// Token: 0x04000287 RID: 647
		private ContentDefinition _contentDefinition;

		// Token: 0x04000288 RID: 648
		private IContentResolutionService _contentResolutionService;

		// Token: 0x04000289 RID: 649
		private const string _idProperty = "ID";

		// Token: 0x0400028A RID: 650
		private const string _contentPlaceHolderIDProperty = "ContentPlaceHolderID";

		// Token: 0x020003DA RID: 986
		private class ContentDesignerActionList : DesignerActionList
		{
			// Token: 0x06002718 RID: 10008 RVA: 0x000F11CD File Offset: 0x000EF3CD
			public ContentDesignerActionList(ContentDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06002719 RID: 10009 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x0600271A RID: 10010 RVA: 0x00003937 File Offset: 0x00001B37
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x0600271B RID: 10011 RVA: 0x000F11E2 File Offset: 0x000EF3E2
			public void ClearRegion()
			{
				this._parent.ClearRegion();
			}

			// Token: 0x0600271C RID: 10012 RVA: 0x000F11EF File Offset: 0x000EF3EF
			public void CreateBlankContent()
			{
				this._parent.CreateBlankContent();
			}

			// Token: 0x0600271D RID: 10013 RVA: 0x000F11FC File Offset: 0x000EF3FC
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				ContentDesignerState contentDesignerState = ContentDesignerState.ShowDefaultContent;
				if (this._parent.ContentResolutionService != null && this._parent.GetContentDefinition() != null)
				{
					contentDesignerState = this._parent.ContentResolutionService.GetContentDesignerState(this._parent.GetContentDefinition().ContentPlaceHolderID);
				}
				if (contentDesignerState == ContentDesignerState.ShowDefaultContent)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "CreateBlankContent", SR.GetString("Content_CreateBlankContent"), string.Empty, string.Empty, true)
					{
						ShowInSourceView = false
					});
				}
				else if (ContentDesignerState.ShowUserContent == contentDesignerState)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ClearRegion", SR.GetString("Content_ClearRegion"), string.Empty, string.Empty, true));
				}
				return designerActionItemCollection;
			}

			// Token: 0x04001C21 RID: 7201
			private ContentDesigner _parent;
		}
	}
}
