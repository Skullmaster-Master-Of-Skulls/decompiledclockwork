using System;
using System.Collections;

namespace MailBee.Html
{
	// Token: 0x02000006 RID: 6
	public class ElementCollection : CollectionBase
	{
		// Token: 0x1700001C RID: 28
		public Element this[int index]
		{
			get
			{
				return (Element)base.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				base.List[index] = value;
				this.ParentElement.IsRebuildNeeded = true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004E98 File Offset: 0x00003E98
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00004EA0 File Offset: 0x00003EA0
		internal Element ParentElement
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004EA9 File Offset: 0x00003EA9
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00004EB1 File Offset: 0x00003EB1
		internal bool IsInnerTreeProcessLocked
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00004EBA File Offset: 0x00003EBA
		internal bool IsRebuildNeeded
		{
			set
			{
				if (this.ParentElement != null)
				{
					this.ParentElement.IsRebuildNeeded = true;
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004ED0 File Offset: 0x00003ED0
		public ElementCollection()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004EDF File Offset: 0x00003EDF
		internal ElementCollection(Element A_0)
		{
			this.ParentElement = A_0;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004EF8 File Offset: 0x00003EF8
		public void Add(Element elem)
		{
			if (elem == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!this.IsInnerTreeProcessLocked)
			{
				Element element = this[base.List.Count - 1].ParentElement;
				for (int i = 0; i < element.InnerElements.Count; i++)
				{
					if (element.InnerElements[i] == this[base.List.Count - 1])
					{
						element.InnerElements.Add(elem, i + 1);
						break;
					}
				}
			}
			base.List.Add(elem);
			this.IsRebuildNeeded = true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004F8D File Offset: 0x00003F8D
		public void Add(Element elem, int index)
		{
			if (elem == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Insert(index, elem);
			this.IsRebuildNeeded = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004FB0 File Offset: 0x00003FB0
		public void AddRange(ElementCollection elems)
		{
			if (elems == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in elems)
			{
				Element element = (Element)obj;
				if (!this.IsInnerTreeProcessLocked)
				{
					Element element2 = this[base.List.Count - 1].ParentElement;
					for (int i = 0; i < element2.InnerElements.Count; i++)
					{
						if (element2.InnerElements[i] == this[base.List.Count - 1])
						{
							element2.InnerElements.Add(element, i + 1);
							break;
						}
					}
				}
				base.List.Add(element);
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005090 File Offset: 0x00004090
		public void AddRange(ElementCollection elems, int srcIndex, int count, int destIndex)
		{
			if (elems == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			for (int i = 0; i < count; i++)
			{
				base.List.Insert(destIndex + i, elems[srcIndex + i]);
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000050D3 File Offset: 0x000040D3
		public bool Remove(Element elem)
		{
			if (elem == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!this.IsInnerTreeProcessLocked)
			{
				elem.Remove();
			}
			if (base.List.Contains(elem))
			{
				base.List.Remove(elem);
				this.IsRebuildNeeded = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005112 File Offset: 0x00004112
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
			this.IsRebuildNeeded = true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005128 File Offset: 0x00004128
		public void RemoveByName(string tagName)
		{
			bool flag = false;
			string text = tagName.ToLower();
			for (int i = base.List.Count - 1; i >= 0; i--)
			{
				Element element = this[i];
				if ((element.TagName == null && tagName == null) || element.TagName.ToLower() == text)
				{
					base.List.Remove(element);
					flag = true;
				}
			}
			if (flag)
			{
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005194 File Offset: 0x00004194
		public void RemoveAll()
		{
			base.List.Clear();
			this.IsRebuildNeeded = true;
		}

		// Token: 0x04000031 RID: 49
		private Element a;

		// Token: 0x04000032 RID: 50
		private bool b = true;
	}
}
