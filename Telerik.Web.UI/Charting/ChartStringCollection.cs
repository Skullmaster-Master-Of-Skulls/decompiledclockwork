using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x0200171A RID: 5914
	internal class ChartStringCollection : CollectionBase
	{
		// Token: 0x170045FA RID: 17914
		internal ChartString this[int index]
		{
			get
			{
				return (ChartString)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x170045FB RID: 17915
		// (get) Token: 0x0600E5C4 RID: 58820 RVA: 0x00330503 File Offset: 0x0032E703
		internal ChartString First
		{
			get
			{
				if (base.Count > 0)
				{
					return this[0];
				}
				return null;
			}
		}

		// Token: 0x170045FC RID: 17916
		// (get) Token: 0x0600E5C5 RID: 58821 RVA: 0x00330517 File Offset: 0x0032E717
		internal ChartString Last
		{
			get
			{
				if (base.Count > 0)
				{
					return this[base.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x170045FD RID: 17917
		// (get) Token: 0x0600E5C6 RID: 58822 RVA: 0x00330532 File Offset: 0x0032E732
		// (set) Token: 0x0600E5C7 RID: 58823 RVA: 0x0033053A File Offset: 0x0032E73A
		internal ChartText Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x0600E5C8 RID: 58824 RVA: 0x00330543 File Offset: 0x0032E743
		internal ChartStringCollection(ChartText parent)
		{
			this.parent = parent;
		}

		// Token: 0x0600E5C9 RID: 58825 RVA: 0x00330554 File Offset: 0x0032E754
		internal int Add(ChartString str)
		{
			if (base.Count == 0)
			{
				str.IsFirst = true;
			}
			else
			{
				this.Last.IsLast = false;
			}
			str.IsLast = true;
			str.Parent = this;
			base.List.Add(str);
			return base.Count - 1;
		}

		// Token: 0x0600E5CA RID: 58826 RVA: 0x003305A4 File Offset: 0x0032E7A4
		internal ChartString GetNext(ChartString str)
		{
			int num = base.List.IndexOf(str);
			if (num >= 0)
			{
				if (str.IsLast)
				{
					this.Add(new ChartString(str.Height));
				}
				return this[num + 1];
			}
			return null;
		}

		// Token: 0x0600E5CB RID: 58827 RVA: 0x003305E8 File Offset: 0x0032E7E8
		internal ChartString GetPrevious(ChartString str)
		{
			int num = base.List.IndexOf(str);
			if (num > 0)
			{
				return this[num - 1];
			}
			return null;
		}

		// Token: 0x0600E5CC RID: 58828 RVA: 0x00330614 File Offset: 0x0032E814
		internal ChartStringCollection Clone()
		{
			ChartStringCollection chartStringCollection = new ChartStringCollection(null);
			foreach (object obj in this)
			{
				ChartString chartString = (ChartString)obj;
				chartStringCollection.Add(chartString.Clone());
			}
			return chartStringCollection;
		}

		// Token: 0x04004218 RID: 16920
		private ChartText parent;
	}
}
