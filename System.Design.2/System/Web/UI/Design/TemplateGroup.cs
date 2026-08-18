using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000071 RID: 113
	public class TemplateGroup
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00011D7A File Offset: 0x0000FF7A
		public TemplateGroup(string groupName) : this(groupName, null)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011D84 File Offset: 0x0000FF84
		public TemplateGroup(string groupName, Style groupStyle)
		{
			this._groupName = groupName;
			this._groupStyle = groupStyle;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00011D9A File Offset: 0x0000FF9A
		public bool IsEmpty
		{
			get
			{
				return this._templates == null || this._templates.Count == 0;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		public string GroupName
		{
			get
			{
				return this._groupName;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00011DBC File Offset: 0x0000FFBC
		public Style GroupStyle
		{
			get
			{
				return this._groupStyle;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public TemplateDefinition[] Templates
		{
			get
			{
				if (this._templates == null)
				{
					return TemplateGroup.emptyTemplateDefinitionArray;
				}
				return (TemplateDefinition[])this._templates.ToArray(typeof(TemplateDefinition));
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011DEE File Offset: 0x0000FFEE
		public void AddTemplateDefinition(TemplateDefinition templateDefinition)
		{
			if (this._templates == null)
			{
				this._templates = new ArrayList();
			}
			this._templates.Add(templateDefinition);
		}

		// Token: 0x0400018F RID: 399
		private static TemplateDefinition[] emptyTemplateDefinitionArray = new TemplateDefinition[0];

		// Token: 0x04000190 RID: 400
		private string _groupName;

		// Token: 0x04000191 RID: 401
		private Style _groupStyle;

		// Token: 0x04000192 RID: 402
		private ArrayList _templates;
	}
}
