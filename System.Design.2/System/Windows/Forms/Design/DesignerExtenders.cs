using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D5 RID: 725
	internal class DesignerExtenders
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x000ADA38 File Offset: 0x000ABC38
		public DesignerExtenders(IExtenderProviderService ex)
		{
			this.extenderService = ex;
			if (this.providers == null)
			{
				this.providers = new IExtenderProvider[]
				{
					new DesignerExtenders.NameExtenderProvider(),
					new DesignerExtenders.NameInheritedExtenderProvider()
				};
			}
			for (int i = 0; i < this.providers.Length; i++)
			{
				ex.AddExtenderProvider(this.providers[i]);
			}
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000ADA98 File Offset: 0x000ABC98
		public void Dispose()
		{
			if (this.extenderService != null && this.providers != null)
			{
				for (int i = 0; i < this.providers.Length; i++)
				{
					this.extenderService.RemoveExtenderProvider(this.providers[i]);
				}
				this.providers = null;
				this.extenderService = null;
			}
		}

		// Token: 0x04001714 RID: 5908
		private IExtenderProvider[] providers;

		// Token: 0x04001715 RID: 5909
		private IExtenderProviderService extenderService;

		// Token: 0x02000569 RID: 1385
		[ProvideProperty("Name", typeof(IComponent))]
		private class NameExtenderProvider : IExtenderProvider
		{
			// Token: 0x060031B6 RID: 12726 RVA: 0x0000362F File Offset: 0x0000182F
			internal NameExtenderProvider()
			{
			}

			// Token: 0x060031B7 RID: 12727 RVA: 0x0010E094 File Offset: 0x0010C294
			protected IComponent GetBaseComponent(object o)
			{
				if (this.baseComponent == null)
				{
					ISite site = ((IComponent)o).Site;
					if (site != null)
					{
						IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							this.baseComponent = designerHost.RootComponent;
						}
					}
				}
				return this.baseComponent;
			}

			// Token: 0x060031B8 RID: 12728 RVA: 0x0010E0E4 File Offset: 0x0010C2E4
			public virtual bool CanExtend(object o)
			{
				IComponent component = this.GetBaseComponent(o);
				return component == o || TypeDescriptor.GetAttributes(o)[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.NotInherited);
			}

			// Token: 0x060031B9 RID: 12729 RVA: 0x0010E124 File Offset: 0x0010C324
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			[ParenthesizePropertyName(true)]
			[MergableProperty(false)]
			[SRDescription("DesignerPropName")]
			[Category("Design")]
			public virtual string GetName(IComponent comp)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					return site.Name;
				}
				return null;
			}

			// Token: 0x060031BA RID: 12730 RVA: 0x0010E144 File Offset: 0x0010C344
			public void SetName(IComponent comp, string newName)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					site.Name = newName;
				}
			}

			// Token: 0x04002139 RID: 8505
			private IComponent baseComponent;
		}

		// Token: 0x0200056A RID: 1386
		private class NameInheritedExtenderProvider : DesignerExtenders.NameExtenderProvider
		{
			// Token: 0x060031BB RID: 12731 RVA: 0x0010E162 File Offset: 0x0010C362
			internal NameInheritedExtenderProvider()
			{
			}

			// Token: 0x060031BC RID: 12732 RVA: 0x0010E16C File Offset: 0x0010C36C
			public override bool CanExtend(object o)
			{
				IComponent baseComponent = base.GetBaseComponent(o);
				return baseComponent != o && !TypeDescriptor.GetAttributes(o)[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.NotInherited);
			}

			// Token: 0x060031BD RID: 12733 RVA: 0x0010E1AB File Offset: 0x0010C3AB
			[ReadOnly(true)]
			public override string GetName(IComponent comp)
			{
				return base.GetName(comp);
			}
		}
	}
}
