using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001575 RID: 5493
	internal class ScoreSpacesMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D821 RID: 55329 RVA: 0x002F8A6E File Offset: 0x002F6C6E
		public new static PropertyMaker Maker(string propName)
		{
			return new ScoreSpacesMaker(propName);
		}

		// Token: 0x0600D822 RID: 55330 RVA: 0x002F8A76 File Offset: 0x002F6C76
		protected ScoreSpacesMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D823 RID: 55331 RVA: 0x002F8A7F File Offset: 0x002F6C7F
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D824 RID: 55332 RVA: 0x002F8A82 File Offset: 0x002F6C82
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "true", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B72 RID: 15218
		private Property m_defaultProp;
	}
}
