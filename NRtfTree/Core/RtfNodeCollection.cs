using System;
using System.Collections;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x0200000E RID: 14
	public class RtfNodeCollection : CollectionBase
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00005B1A File Offset: 0x00003D1A
		public int Add(RtfTreeNode node)
		{
			base.InnerList.Add(node);
			return base.InnerList.Count - 1;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005B36 File Offset: 0x00003D36
		public void Insert(int index, RtfTreeNode node)
		{
			base.InnerList.Insert(index, node);
		}

		// Token: 0x17000042 RID: 66
		public RtfTreeNode this[int index]
		{
			get
			{
				return (RtfTreeNode)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005B67 File Offset: 0x00003D67
		public int IndexOf(RtfTreeNode node)
		{
			return base.InnerList.IndexOf(node);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005B75 File Offset: 0x00003D75
		public int IndexOf(RtfTreeNode node, int startIndex)
		{
			return base.InnerList.IndexOf(node, startIndex);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005B84 File Offset: 0x00003D84
		public int IndexOf(string key)
		{
			int result = -1;
			if (base.InnerList.Count > 0)
			{
				for (int i = 0; i < base.InnerList.Count; i++)
				{
					if (((RtfTreeNode)base.InnerList[i]).NodeKey == key)
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005BDC File Offset: 0x00003DDC
		public int IndexOf(string key, int startIndex)
		{
			int result = -1;
			if (base.InnerList.Count > 0)
			{
				for (int i = startIndex; i < base.InnerList.Count; i++)
				{
					if (((RtfTreeNode)base.InnerList[i]).NodeKey == key)
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005C32 File Offset: 0x00003E32
		public void AddRange(RtfNodeCollection collection)
		{
			base.InnerList.AddRange(collection);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005C40 File Offset: 0x00003E40
		public void RemoveRange(int index, int count)
		{
			base.InnerList.RemoveRange(index, count);
		}
	}
}
