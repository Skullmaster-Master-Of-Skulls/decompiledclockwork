using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006D9 RID: 1753
	[ConfigurationCollection(typeof(ExpressionBuilder))]
	public sealed class ExpressionBuilderCollection : ConfigurationElementCollection
	{
		// Token: 0x0600545B RID: 21595 RVA: 0x001240D1 File Offset: 0x001222D1
		public ExpressionBuilderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x0600545C RID: 21596 RVA: 0x00127B77 File Offset: 0x00125D77
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilderCollection._properties;
			}
		}

		// Token: 0x1700180B RID: 6155
		public ExpressionBuilder this[string name]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(name);
			}
		}

		// Token: 0x1700180C RID: 6156
		public ExpressionBuilder this[int index]
		{
			get
			{
				return (ExpressionBuilder)base.BaseGet(index);
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

		// Token: 0x06005460 RID: 21600 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ExpressionBuilder buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x00127B9A File Offset: 0x00125D9A
		protected override ConfigurationElement CreateNewElement()
		{
			return new ExpressionBuilder();
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x00127BA1 File Offset: 0x00125DA1
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ExpressionBuilder)element).ExpressionPrefix;
		}

		// Token: 0x04002C4C RID: 11340
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
