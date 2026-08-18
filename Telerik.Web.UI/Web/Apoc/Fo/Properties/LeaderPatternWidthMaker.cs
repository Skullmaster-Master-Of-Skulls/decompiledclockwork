using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200151D RID: 5405
	internal class LeaderPatternWidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D6D5 RID: 54997 RVA: 0x002F709A File Offset: 0x002F529A
		public new static PropertyMaker Maker(string propName)
		{
			return new LeaderPatternWidthMaker(propName);
		}

		// Token: 0x0600D6D6 RID: 54998 RVA: 0x002F70A2 File Offset: 0x002F52A2
		protected LeaderPatternWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6D7 RID: 54999 RVA: 0x002F70AB File Offset: 0x002F52AB
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6D8 RID: 55000 RVA: 0x002F70AE File Offset: 0x002F52AE
		public override Property Make(PropertyList propertyList)
		{
			return this.Make(propertyList, "use-font-metrics", propertyList.getParentFObj());
		}

		// Token: 0x0600D6D9 RID: 55001 RVA: 0x002F70C2 File Offset: 0x002F52C2
		private static void initKeywords()
		{
			LeaderPatternWidthMaker.s_htKeywords = new Hashtable(1);
			LeaderPatternWidthMaker.s_htKeywords.Add("use-font-metrics", "0pt");
		}

		// Token: 0x0600D6DA RID: 55002 RVA: 0x002F70E4 File Offset: 0x002F52E4
		protected override string CheckValueKeywords(string keyword)
		{
			if (LeaderPatternWidthMaker.s_htKeywords == null)
			{
				LeaderPatternWidthMaker.initKeywords();
			}
			string text = (string)LeaderPatternWidthMaker.s_htKeywords[keyword];
			if (text == null)
			{
				return base.CheckValueKeywords(keyword);
			}
			return text;
		}

		// Token: 0x0600D6DB RID: 55003 RVA: 0x002F711A File Offset: 0x002F531A
		public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
		{
			return new LengthBase(fo, propertyList, 3);
		}

		// Token: 0x04003AE8 RID: 15080
		private static Hashtable s_htKeywords;
	}
}
