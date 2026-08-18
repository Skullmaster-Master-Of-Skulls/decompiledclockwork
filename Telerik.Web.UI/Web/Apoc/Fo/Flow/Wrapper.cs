using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001416 RID: 5142
	internal class Wrapper : FObjMixed
	{
		// Token: 0x0600D2C4 RID: 53956 RVA: 0x002EC6F5 File Offset: 0x002EA8F5
		public new static FObj.Maker GetMaker()
		{
			return new Wrapper.Maker();
		}

		// Token: 0x0600D2C5 RID: 53957 RVA: 0x002EC6FC File Offset: 0x002EA8FC
		public Wrapper(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:wrapper";
		}

		// Token: 0x0600D2C6 RID: 53958 RVA: 0x002EC714 File Offset: 0x002EA914
		protected internal override void AddCharacters(char[] data, int start, int length)
		{
			FOText value = new FOText(data, start, length, this);
			this.children.Add(value);
		}

		// Token: 0x02001417 RID: 5143
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D2C7 RID: 53959 RVA: 0x002EC738 File Offset: 0x002EA938
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Wrapper(parent, propertyList);
			}
		}
	}
}
