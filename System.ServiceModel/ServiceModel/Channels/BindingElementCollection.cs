using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006F7 RID: 1783
	[__DynamicallyInvokable]
	public class BindingElementCollection : Collection<BindingElement>
	{
		// Token: 0x0600445D RID: 17501 RVA: 0x00101E82 File Offset: 0x00100082
		[__DynamicallyInvokable]
		public BindingElementCollection()
		{
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x00101E8C File Offset: 0x0010008C
		[__DynamicallyInvokable]
		public BindingElementCollection(IEnumerable<BindingElement> elements)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			foreach (BindingElement item in elements)
			{
				base.Add(item);
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x00101EF0 File Offset: 0x001000F0
		[__DynamicallyInvokable]
		public BindingElementCollection(BindingElement[] elements)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			for (int i = 0; i < elements.Length; i++)
			{
				base.Add(elements[i]);
			}
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x00101F30 File Offset: 0x00100130
		internal BindingElementCollection(BindingElementCollection elements)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			for (int i = 0; i < elements.Count; i++)
			{
				base.Add(elements[i]);
			}
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x00101F74 File Offset: 0x00100174
		[__DynamicallyInvokable]
		public BindingElementCollection Clone()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			for (int i = 0; i < base.Count; i++)
			{
				bindingElementCollection.Add(base[i].Clone());
			}
			return bindingElementCollection;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00101FAC File Offset: 0x001001AC
		[__DynamicallyInvokable]
		public void AddRange(params BindingElement[] elements)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			for (int i = 0; i < elements.Length; i++)
			{
				base.Add(elements[i]);
			}
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x00101FE4 File Offset: 0x001001E4
		[__DynamicallyInvokable]
		public bool Contains(Type bindingElementType)
		{
			if (bindingElementType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElementType");
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (bindingElementType.IsInstanceOfType(base[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x0010202D File Offset: 0x0010022D
		[__DynamicallyInvokable]
		public T Find<T>()
		{
			return this.Find<T>(false);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00102036 File Offset: 0x00100236
		[__DynamicallyInvokable]
		public T Remove<T>()
		{
			return this.Find<T>(true);
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x00102040 File Offset: 0x00100240
		private T Find<T>(bool remove)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i] is T)
				{
					T result = (T)((object)base[i]);
					if (remove)
					{
						base.RemoveAt(i);
					}
					return result;
				}
			}
			return default(T);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x0010208E File Offset: 0x0010028E
		[__DynamicallyInvokable]
		public Collection<T> FindAll<T>()
		{
			return this.FindAll<T>(false);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x00102097 File Offset: 0x00100297
		[__DynamicallyInvokable]
		public Collection<T> RemoveAll<T>()
		{
			return this.FindAll<T>(true);
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x001020A0 File Offset: 0x001002A0
		private Collection<T> FindAll<T>(bool remove)
		{
			Collection<T> collection = new Collection<T>();
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i] is T)
				{
					T item = (T)((object)base[i]);
					if (remove)
					{
						base.RemoveAt(i);
						i--;
					}
					collection.Add(item);
				}
			}
			return collection;
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x001020F5 File Offset: 0x001002F5
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, BindingElement item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x00102112 File Offset: 0x00100312
		[__DynamicallyInvokable]
		protected override void SetItem(int index, BindingElement item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
