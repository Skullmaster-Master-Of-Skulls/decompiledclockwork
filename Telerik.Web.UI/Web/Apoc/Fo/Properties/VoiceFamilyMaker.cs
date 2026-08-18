using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015AE RID: 5550
	internal class VoiceFamilyMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8D9 RID: 55513 RVA: 0x002F99C0 File Offset: 0x002F7BC0
		public new static PropertyMaker Maker(string propName)
		{
			return new VoiceFamilyMaker(propName);
		}

		// Token: 0x0600D8DA RID: 55514 RVA: 0x002F99C8 File Offset: 0x002F7BC8
		protected VoiceFamilyMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8DB RID: 55515 RVA: 0x002F99D1 File Offset: 0x002F7BD1
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8DC RID: 55516 RVA: 0x002F99D4 File Offset: 0x002F7BD4
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD1 RID: 15313
		private Property m_defaultProp;
	}
}
