using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.Design
{
	// Token: 0x02000033 RID: 51
	public abstract class DesignerObject : IServiceProvider
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		protected DesignerObject(ControlDesigner designer, string name)
		{
			if (designer == null)
			{
				throw new ArgumentNullException("designer");
			}
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			this._designer = designer;
			this._name = name;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000CC85 File Offset: 0x0000AE85
		public ControlDesigner Designer
		{
			get
			{
				return this._designer;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000CC8D File Offset: 0x0000AE8D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000CC95 File Offset: 0x0000AE95
		public IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new HybridDictionary();
				}
				return this._properties;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		protected object GetService(Type serviceType)
		{
			IServiceProvider site = this._designer.Component.Site;
			if (site != null)
			{
				return site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CCDA File Offset: 0x0000AEDA
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x04000120 RID: 288
		private ControlDesigner _designer;

		// Token: 0x04000121 RID: 289
		private string _name;

		// Token: 0x04000122 RID: 290
		private IDictionary _properties;
	}
}
