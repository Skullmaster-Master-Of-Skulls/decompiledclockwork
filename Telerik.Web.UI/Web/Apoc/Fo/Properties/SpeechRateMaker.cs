using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158C RID: 5516
	internal class SpeechRateMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D858 RID: 55384 RVA: 0x002F8D76 File Offset: 0x002F6F76
		public new static PropertyMaker Maker(string propName)
		{
			return new SpeechRateMaker(propName);
		}

		// Token: 0x0600D859 RID: 55385 RVA: 0x002F8D7E File Offset: 0x002F6F7E
		protected SpeechRateMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D85A RID: 55386 RVA: 0x002F8D87 File Offset: 0x002F6F87
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D85B RID: 55387 RVA: 0x002F8D8A File Offset: 0x002F6F8A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "medium", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B80 RID: 15232
		private Property m_defaultProp;
	}
}
