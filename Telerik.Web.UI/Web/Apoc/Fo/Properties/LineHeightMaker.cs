using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001523 RID: 5411
	internal class LineHeightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D6F0 RID: 55024 RVA: 0x002F7291 File Offset: 0x002F5491
		public new static PropertyMaker Maker(string propName)
		{
			return new LineHeightMaker(propName);
		}

		// Token: 0x0600D6F1 RID: 55025 RVA: 0x002F7299 File Offset: 0x002F5499
		protected LineHeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6F2 RID: 55026 RVA: 0x002F72A2 File Offset: 0x002F54A2
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6F3 RID: 55027 RVA: 0x002F72A5 File Offset: 0x002F54A5
		public override bool InheritsSpecified()
		{
			return true;
		}

		// Token: 0x0600D6F4 RID: 55028 RVA: 0x002F72A8 File Offset: 0x002F54A8
		public override Property Make(PropertyList propertyList)
		{
			return this.Make(propertyList, "normal", propertyList.getParentFObj());
		}

		// Token: 0x0600D6F5 RID: 55029 RVA: 0x002F72BC File Offset: 0x002F54BC
		private static void initKeywords()
		{
			LineHeightMaker.s_htKeywords = new Hashtable(1);
			LineHeightMaker.s_htKeywords.Add("normal", "1.2em");
		}

		// Token: 0x0600D6F6 RID: 55030 RVA: 0x002F72E0 File Offset: 0x002F54E0
		protected override string CheckValueKeywords(string keyword)
		{
			if (LineHeightMaker.s_htKeywords == null)
			{
				LineHeightMaker.initKeywords();
			}
			string text = (string)LineHeightMaker.s_htKeywords[keyword];
			if (text == null)
			{
				return base.CheckValueKeywords(keyword);
			}
			return text;
		}

		// Token: 0x0600D6F7 RID: 55031 RVA: 0x002F7318 File Offset: 0x002F5518
		protected override Property ConvertPropertyDatatype(Property p, PropertyList propertyList, FObj fo)
		{
			Number number = p.GetNumber();
			if (number != null)
			{
				return new LengthProperty(new PercentLength(number.DoubleValue(), this.GetPercentBase(fo, propertyList)));
			}
			return base.ConvertPropertyDatatype(p, propertyList, fo);
		}

		// Token: 0x0600D6F8 RID: 55032 RVA: 0x002F7351 File Offset: 0x002F5551
		public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
		{
			return new LengthBase(fo, propertyList, 1);
		}

		// Token: 0x04003AF3 RID: 15091
		private static Hashtable s_htKeywords;
	}
}
