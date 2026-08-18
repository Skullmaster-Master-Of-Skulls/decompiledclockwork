using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200027D RID: 637
	public sealed class HtmlElementCollection : ICollection, IEnumerable
	{
		// Token: 0x060028E7 RID: 10471 RVA: 0x000BC843 File Offset: 0x000BAA43
		internal HtmlElementCollection(HtmlShimManager shimManager)
		{
			this.htmlElementCollection = null;
			this.elementsArray = null;
			this.shimManager = shimManager;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000BC860 File Offset: 0x000BAA60
		internal HtmlElementCollection(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLElementCollection elements)
		{
			this.htmlElementCollection = elements;
			this.elementsArray = null;
			this.shimManager = shimManager;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000BC87D File Offset: 0x000BAA7D
		internal HtmlElementCollection(HtmlShimManager shimManager, HtmlElement[] array)
		{
			this.htmlElementCollection = null;
			this.elementsArray = array;
			this.shimManager = shimManager;
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000BC89A File Offset: 0x000BAA9A
		private UnsafeNativeMethods.IHTMLElementCollection NativeHtmlElementCollection
		{
			get
			{
				return this.htmlElementCollection;
			}
		}

		// Token: 0x17000989 RID: 2441
		public HtmlElement this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidBoundArgument", new object[]
					{
						"index",
						index,
						0,
						this.Count - 1
					}));
				}
				if (this.NativeHtmlElementCollection != null)
				{
					UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlElementCollection.Item(index, 0) as UnsafeNativeMethods.IHTMLElement;
					if (ihtmlelement == null)
					{
						return null;
					}
					return new HtmlElement(this.shimManager, ihtmlelement);
				}
				else
				{
					if (this.elementsArray != null)
					{
						return this.elementsArray[index];
					}
					return null;
				}
			}
		}

		// Token: 0x1700098A RID: 2442
		public HtmlElement this[string elementId]
		{
			get
			{
				if (this.NativeHtmlElementCollection != null)
				{
					UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlElementCollection.Item(elementId, 0) as UnsafeNativeMethods.IHTMLElement;
					if (ihtmlelement == null)
					{
						return null;
					}
					return new HtmlElement(this.shimManager, ihtmlelement);
				}
				else
				{
					if (this.elementsArray != null)
					{
						int num = this.elementsArray.Length;
						for (int i = 0; i < num; i++)
						{
							HtmlElement htmlElement = this.elementsArray[i];
							if (htmlElement.Id == elementId)
							{
								return htmlElement;
							}
						}
						return null;
					}
					return null;
				}
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		public HtmlElementCollection GetElementsByName(string name)
		{
			int count = this.Count;
			HtmlElement[] array = new HtmlElement[count];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				HtmlElement htmlElement = this[i];
				if (htmlElement.GetAttribute("name") == name)
				{
					array[num] = htmlElement;
					num++;
				}
			}
			if (num == 0)
			{
				return new HtmlElementCollection(this.shimManager);
			}
			HtmlElement[] array2 = new HtmlElement[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[j];
			}
			return new HtmlElementCollection(this.shimManager, array2);
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000BCA50 File Offset: 0x000BAC50
		public int Count
		{
			get
			{
				if (this.NativeHtmlElementCollection != null)
				{
					return this.NativeHtmlElementCollection.GetLength();
				}
				if (this.elementsArray != null)
				{
					return this.elementsArray.Length;
				}
				return 0;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000BCA78 File Offset: 0x000BAC78
		void ICollection.CopyTo(Array dest, int index)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				dest.SetValue(this[i], index++);
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000BCAAC File Offset: 0x000BACAC
		public IEnumerator GetEnumerator()
		{
			HtmlElement[] array = new HtmlElement[this.Count];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x040010CF RID: 4303
		private UnsafeNativeMethods.IHTMLElementCollection htmlElementCollection;

		// Token: 0x040010D0 RID: 4304
		private HtmlElement[] elementsArray;

		// Token: 0x040010D1 RID: 4305
		private HtmlShimManager shimManager;
	}
}
