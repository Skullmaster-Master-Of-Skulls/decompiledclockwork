using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000047 RID: 71
	internal class XPathMultyIterator : ResetableIterator
	{
		// Token: 0x06000224 RID: 548 RVA: 0x000082A8 File Offset: 0x000064A8
		public XPathMultyIterator(ArrayList inputArray)
		{
			this.arr = new ResetableIterator[inputArray.Count];
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i] = new XPathArrayIterator((ArrayList)inputArray[i]);
			}
			this.Init();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008300 File Offset: 0x00006500
		private void Init()
		{
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.Advance(i);
			}
			int num = this.arr.Length - 2;
			while (this.firstNotEmpty <= num)
			{
				if (this.SiftItem(num))
				{
					num--;
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000834C File Offset: 0x0000654C
		private bool Advance(int pos)
		{
			if (!this.arr[pos].MoveNext())
			{
				if (this.firstNotEmpty != pos)
				{
					ResetableIterator resetableIterator = this.arr[pos];
					Array.Copy(this.arr, this.firstNotEmpty, this.arr, this.firstNotEmpty + 1, pos - this.firstNotEmpty);
					this.arr[this.firstNotEmpty] = resetableIterator;
				}
				this.firstNotEmpty++;
				return false;
			}
			return true;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000083C0 File Offset: 0x000065C0
		private bool SiftItem(int item)
		{
			ResetableIterator resetableIterator = this.arr[item];
			while (item + 1 < this.arr.Length)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(resetableIterator.Current, this.arr[item + 1].Current);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					break;
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					this.arr[item] = this.arr[item + 1];
					item++;
				}
				else
				{
					this.arr[item] = resetableIterator;
					if (!this.Advance(item))
					{
						return false;
					}
					resetableIterator = this.arr[item];
				}
			}
			this.arr[item] = resetableIterator;
			return true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008448 File Offset: 0x00006648
		public override void Reset()
		{
			this.firstNotEmpty = 0;
			this.position = 0;
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i].Reset();
			}
			this.Init();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008489 File Offset: 0x00006689
		public XPathMultyIterator(XPathMultyIterator it)
		{
			this.arr = (ResetableIterator[])it.arr.Clone();
			this.firstNotEmpty = it.firstNotEmpty;
			this.position = it.position;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000084BF File Offset: 0x000066BF
		public override XPathNodeIterator Clone()
		{
			return new XPathMultyIterator(this);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000084C7 File Offset: 0x000066C7
		public override XPathNavigator Current
		{
			get
			{
				return this.arr[this.firstNotEmpty].Current;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000084DB File Offset: 0x000066DB
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000084E4 File Offset: 0x000066E4
		public override bool MoveNext()
		{
			if (this.firstNotEmpty >= this.arr.Length)
			{
				return false;
			}
			if (this.position != 0)
			{
				if (this.Advance(this.firstNotEmpty))
				{
					this.SiftItem(this.firstNotEmpty);
				}
				if (this.firstNotEmpty >= this.arr.Length)
				{
					return false;
				}
			}
			this.position++;
			return true;
		}

		// Token: 0x040000E0 RID: 224
		protected ResetableIterator[] arr;

		// Token: 0x040000E1 RID: 225
		protected int firstNotEmpty;

		// Token: 0x040000E2 RID: 226
		protected int position;
	}
}
