using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013C5 RID: 5061
	internal class FObjMixed : FObj
	{
		// Token: 0x0600D1AA RID: 53674 RVA: 0x002E6678 File Offset: 0x002E4878
		public new static FObj.Maker GetMaker()
		{
			return new FObjMixed.Maker();
		}

		// Token: 0x0600D1AB RID: 53675 RVA: 0x002E667F File Offset: 0x002E487F
		protected FObjMixed(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D1AC RID: 53676 RVA: 0x002E6689 File Offset: 0x002E4889
		public TextState getTextState()
		{
			return this.ts;
		}

		// Token: 0x0600D1AD RID: 53677 RVA: 0x002E6694 File Offset: 0x002E4894
		protected internal override void AddCharacters(char[] data, int start, int length)
		{
			FOText fotext = new FOText(data, start, length, this);
			fotext.setUnderlined(this.ts.getUnderlined());
			fotext.setOverlined(this.ts.getOverlined());
			fotext.setLineThrough(this.ts.getLineThrough());
			this.AddChild(fotext);
		}

		// Token: 0x0600D1AE RID: 53678 RVA: 0x002E66E8 File Offset: 0x002E48E8
		public override Status Layout(Area area)
		{
			if (this.properties != null)
			{
				Property property = this.properties.GetProperty("id");
				if (property != null)
				{
					string @string = property.GetString();
					if (this.marker == -1000)
					{
						if (area.getIDReferences() != null)
						{
							area.getIDReferences().CreateID(@string);
						}
						this.marker = 0;
					}
					if (this.marker == 0 && area.getIDReferences() != null)
					{
						area.getIDReferences().ConfigureID(@string, area);
					}
				}
			}
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				Status status;
				Status result = status = fonode.Layout(area);
				if (status.isIncomplete())
				{
					this.marker = i;
					return result;
				}
			}
			return new Status(1);
		}

		// Token: 0x04003857 RID: 14423
		protected TextState ts;

		// Token: 0x020013C6 RID: 5062
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1AF RID: 53679 RVA: 0x002E67B3 File Offset: 0x002E49B3
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new FObjMixed(parent, propertyList);
			}
		}
	}
}
