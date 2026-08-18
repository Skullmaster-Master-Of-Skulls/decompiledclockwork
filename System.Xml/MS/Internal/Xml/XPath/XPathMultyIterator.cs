using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200016A RID: 362
	internal class XPathMultyIterator : ResetableIterator
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x00053810 File Offset: 0x00052810
		public XPathMultyIterator(ArrayList inputArray)
		{
			this.arr = new ResetableIterator[inputArray.Count];
			for (int i = 0; i < this.arr.Length; i++)
			{
				this.arr[i] = new XPathArrayIterator((ArrayList)inputArray[i]);
			}
			this.Init();
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00053868 File Offset: 0x00052868
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

		// Token: 0x0600135F RID: 4959 RVA: 0x000538B4 File Offset: 0x000528B4
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

		// Token: 0x06001360 RID: 4960 RVA: 0x00053928 File Offset: 0x00052928
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

		// Token: 0x06001361 RID: 4961 RVA: 0x000539B0 File Offset: 0x000529B0
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

		// Token: 0x06001362 RID: 4962 RVA: 0x000539F1 File Offset: 0x000529F1
		public XPathMultyIterator(XPathMultyIterator it)
		{
			this.arr = (ResetableIterator[])it.arr.Clone();
			this.firstNotEmpty = it.firstNotEmpty;
			this.position = it.position;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00053A27 File Offset: 0x00052A27
		public override XPathNodeIterator Clone()
		{
			return new XPathMultyIterator(this);
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00053A2F File Offset: 0x00052A2F
		public override XPathNavigator Current
		{
			get
			{
				return this.arr[this.firstNotEmpty].Current;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00053A43 File Offset: 0x00052A43
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00053A4C File Offset: 0x00052A4C
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

		// Token: 0x04000BF1 RID: 3057
		protected ResetableIterator[] arr;

		// Token: 0x04000BF2 RID: 3058
		protected int firstNotEmpty;

		// Token: 0x04000BF3 RID: 3059
		protected int position;
	}
}
