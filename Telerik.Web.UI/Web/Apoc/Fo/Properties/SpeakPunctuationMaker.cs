using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158B RID: 5515
	internal class SpeakPunctuationMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D854 RID: 55380 RVA: 0x002F8D3A File Offset: 0x002F6F3A
		public new static PropertyMaker Maker(string propName)
		{
			return new SpeakPunctuationMaker(propName);
		}

		// Token: 0x0600D855 RID: 55381 RVA: 0x002F8D42 File Offset: 0x002F6F42
		protected SpeakPunctuationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D856 RID: 55382 RVA: 0x002F8D4B File Offset: 0x002F6F4B
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D857 RID: 55383 RVA: 0x002F8D4E File Offset: 0x002F6F4E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B7F RID: 15231
		private Property m_defaultProp;
	}
}
