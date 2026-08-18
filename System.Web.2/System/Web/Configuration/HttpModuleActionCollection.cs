using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006FD RID: 1789
	[ConfigurationCollection(typeof(HttpModuleAction))]
	public sealed class HttpModuleActionCollection : ConfigurationElementCollection
	{
		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x0600565E RID: 22110 RVA: 0x0012E8FE File Offset: 0x0012CAFE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleActionCollection._properties;
			}
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x001240D1 File Offset: 0x001222D1
		public HttpModuleActionCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170018F5 RID: 6389
		public HttpModuleAction this[int index]
		{
			get
			{
				return (HttpModuleAction)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0012E49C File Offset: 0x0012C69C
		public int IndexOf(HttpModuleAction action)
		{
			return base.BaseIndexOf(action);
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(HttpModuleAction httpModule)
		{
			this.BaseAdd(httpModule);
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x0012E913 File Offset: 0x0012CB13
		public void Remove(HttpModuleAction action)
		{
			base.BaseRemove(action.Key);
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0012E921 File Offset: 0x0012CB21
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpModuleAction();
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0012E928 File Offset: 0x0012CB28
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HttpModuleAction)element).Key;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x0012E938 File Offset: 0x0012CB38
		protected override bool IsElementRemovable(ConfigurationElement element)
		{
			HttpModuleAction httpModuleAction = (HttpModuleAction)element;
			if (base.BaseIndexOf(httpModuleAction) != -1)
			{
				return true;
			}
			if (HttpModuleAction.IsSpecialModuleName(httpModuleAction.Name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Special_module_cannot_be_removed_manually", new object[]
				{
					httpModuleAction.Name
				}), httpModuleAction.FileName, httpModuleAction.LineNumber);
			}
			throw new ConfigurationErrorsException(SR.GetString("Module_not_in_app", new object[]
			{
				httpModuleAction.Name
			}), httpModuleAction.FileName, httpModuleAction.LineNumber);
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x04002DDB RID: 11739
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
