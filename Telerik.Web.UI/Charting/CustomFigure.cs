using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001783 RID: 6019
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class CustomFigure : StateManagedObject
	{
		// Token: 0x17004720 RID: 18208
		// (get) Token: 0x0600EAC2 RID: 60098 RVA: 0x0035790E File Offset: 0x00355B0E
		// (set) Token: 0x0600EAC3 RID: 60099 RVA: 0x0035792E File Offset: 0x00355B2E
		[SkinnableProperty]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "CustomRectangle");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17004721 RID: 18209
		// (get) Token: 0x0600EAC4 RID: 60100 RVA: 0x00357941 File Offset: 0x00355B41
		// (set) Token: 0x0600EAC5 RID: 60101 RVA: 0x00357961 File Offset: 0x00355B61
		[SkinnableProperty]
		[Editor(typeof(CustomShapeEditor), typeof(UITypeEditor))]
		public string Description
		{
			get
			{
				return (string)(base.ViewState["Description"] ?? "20,20,200,100:20,20,False,0,0,0,0,0:220,20,False,0,0,0,0,0:220,120,False,0,0,0,0,0:20,120,False,0,0,0,0,0:");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x0600EAC6 RID: 60102 RVA: 0x00357974 File Offset: 0x00355B74
		public CustomFigure()
		{
		}

		// Token: 0x0600EAC7 RID: 60103 RVA: 0x0035797C File Offset: 0x00355B7C
		public CustomFigure(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		// Token: 0x0600EAC8 RID: 60104 RVA: 0x00357994 File Offset: 0x00355B94
		internal GraphicsPath GetPath()
		{
			CustomShape customShape = new CustomShape();
			customShape.DeserializeProperties(this.Description);
			return customShape.CreatePath(new Rectangle(0, 0, 10, 10));
		}

		// Token: 0x0600EAC9 RID: 60105 RVA: 0x003579C4 File Offset: 0x00355BC4
		public override string ToString()
		{
			return this.Name;
		}
	}
}
