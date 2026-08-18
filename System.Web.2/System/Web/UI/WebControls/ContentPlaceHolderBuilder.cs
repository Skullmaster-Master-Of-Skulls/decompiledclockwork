using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A3 RID: 931
	internal class ContentPlaceHolderBuilder : ControlBuilder
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x00090C44 File Offset: 0x0008EE44
		internal string Name
		{
			get
			{
				return this._templateName;
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x00090C4C File Offset: 0x0008EE4C
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			this._contentPlaceHolderID = ID;
			if (parser.FInDesigner)
			{
				base.Init(parser, parentBuilder, type, tagName, ID, attribs);
				return;
			}
			if (string.IsNullOrEmpty(ID))
			{
				throw new HttpException(SR.GetString("Control_Missing_Attribute", new object[]
				{
					"ID",
					type.Name
				}));
			}
			this._templateName = ID;
			MasterPageParser masterPageParser = parser as MasterPageParser;
			if (masterPageParser == null)
			{
				throw new HttpException(SR.GetString("ContentPlaceHolder_only_in_master"));
			}
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
			if (masterPageParser.PlaceHolderList.Contains(this.Name))
			{
				throw new HttpException(SR.GetString("ContentPlaceHolder_duplicate_contentPlaceHolderID", new object[]
				{
					this.Name
				}));
			}
			masterPageParser.PlaceHolderList.Add(this.Name);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00090D20 File Offset: 0x0008EF20
		public override object BuildObject()
		{
			MasterPage masterPage = base.TemplateControl as MasterPage;
			ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)base.BuildObject();
			if (this.PageProvidesMatchingContent(masterPage))
			{
				ITemplate template = (ITemplate)masterPage.ContentTemplates[this._contentPlaceHolderID];
				masterPage.InstantiateInContentPlaceHolder(contentPlaceHolder, template);
			}
			return contentPlaceHolder;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x00090D70 File Offset: 0x0008EF70
		internal override void BuildChildren(object parentObj)
		{
			MasterPage masterPage = base.TemplateControl as MasterPage;
			if (this.PageProvidesMatchingContent(masterPage))
			{
				return;
			}
			base.BuildChildren(parentObj);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x00090D9A File Offset: 0x0008EF9A
		private bool PageProvidesMatchingContent(MasterPage masterPage)
		{
			return masterPage != null && masterPage.ContentTemplates != null && masterPage.ContentTemplates.Contains(this._contentPlaceHolderID);
		}

		// Token: 0x04001F39 RID: 7993
		private string _contentPlaceHolderID;

		// Token: 0x04001F3A RID: 7994
		private string _templateName;
	}
}
