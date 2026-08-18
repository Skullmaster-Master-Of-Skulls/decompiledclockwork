using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001424 RID: 5156
	internal class ListProperty : Property
	{
		// Token: 0x0600D2F4 RID: 54004 RVA: 0x002ED3E4 File Offset: 0x002EB5E4
		public ListProperty(Property prop)
		{
			this.list = new ArrayList();
			this.list.Add(prop);
		}

		// Token: 0x0600D2F5 RID: 54005 RVA: 0x002ED404 File Offset: 0x002EB604
		public void addProperty(Property prop)
		{
			this.list.Add(prop);
		}

		// Token: 0x0600D2F6 RID: 54006 RVA: 0x002ED413 File Offset: 0x002EB613
		public override ArrayList GetList()
		{
			return this.list;
		}

		// Token: 0x0600D2F7 RID: 54007 RVA: 0x002ED41B File Offset: 0x002EB61B
		public override object GetObject()
		{
			return this.list;
		}

		// Token: 0x04003926 RID: 14630
		protected ArrayList list;

		// Token: 0x02001425 RID: 5157
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D2F8 RID: 54008 RVA: 0x002ED423 File Offset: 0x002EB623
			public Maker(string name) : base(name)
			{
			}

			// Token: 0x0600D2F9 RID: 54009 RVA: 0x002ED42C File Offset: 0x002EB62C
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (p is ListProperty)
				{
					return p;
				}
				return new ListProperty(p);
			}
		}
	}
}
