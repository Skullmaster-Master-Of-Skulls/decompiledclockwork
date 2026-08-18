using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144F RID: 5199
	internal class BackgroundAttachmentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3CF RID: 54223 RVA: 0x002EFBE4 File Offset: 0x002EDDE4
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundAttachmentMaker(propName);
		}

		// Token: 0x0600D3D0 RID: 54224 RVA: 0x002EFBEC File Offset: 0x002EDDEC
		protected BackgroundAttachmentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3D1 RID: 54225 RVA: 0x002EFBF5 File Offset: 0x002EDDF5
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3D2 RID: 54226 RVA: 0x002EFBF8 File Offset: 0x002EDDF8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "scroll", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003984 RID: 14724
		private Property m_defaultProp;
	}
}
