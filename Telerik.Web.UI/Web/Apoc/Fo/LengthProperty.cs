using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200141F RID: 5151
	internal class LengthProperty : Property
	{
		// Token: 0x0600D2E8 RID: 53992 RVA: 0x002ED31D File Offset: 0x002EB51D
		public LengthProperty(Length length)
		{
			this.length = length;
		}

		// Token: 0x0600D2E9 RID: 53993 RVA: 0x002ED32C File Offset: 0x002EB52C
		public override Numeric GetNumeric()
		{
			return this.length.AsNumeric();
		}

		// Token: 0x0600D2EA RID: 53994 RVA: 0x002ED339 File Offset: 0x002EB539
		public override Length GetLength()
		{
			return this.length;
		}

		// Token: 0x0600D2EB RID: 53995 RVA: 0x002ED341 File Offset: 0x002EB541
		public override object GetObject()
		{
			return this.length;
		}

		// Token: 0x04003924 RID: 14628
		private Length length;

		// Token: 0x02001420 RID: 5152
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D2EC RID: 53996 RVA: 0x002ED349 File Offset: 0x002EB549
			public Maker(string name) : base(name)
			{
			}

			// Token: 0x0600D2ED RID: 53997 RVA: 0x002ED352 File Offset: 0x002EB552
			protected virtual bool IsAutoLengthAllowed()
			{
				return false;
			}

			// Token: 0x0600D2EE RID: 53998 RVA: 0x002ED358 File Offset: 0x002EB558
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (this.IsAutoLengthAllowed())
				{
					string @string = p.GetString();
					if (@string != null && @string.Equals("auto"))
					{
						return new LengthProperty(new AutoLength());
					}
				}
				if (p is LengthProperty)
				{
					return p;
				}
				Length length = p.GetLength();
				if (length != null)
				{
					return new LengthProperty(length);
				}
				return this.ConvertPropertyDatatype(p, propertyList, fo);
			}
		}
	}
}
