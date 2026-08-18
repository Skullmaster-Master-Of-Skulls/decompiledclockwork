using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C7 RID: 2247
	internal class SegmentHierarchyNode<TData> where TData : class
	{
		// Token: 0x060055CB RID: 21963 RVA: 0x00139EFF File Offset: 0x001380FF
		public SegmentHierarchyNode(string name, bool useWeakReferences)
		{
			this.name = name;
			this.useWeakReferences = useWeakReferences;
			this.children = new Dictionary<string, SegmentHierarchyNode<TData>>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x00139F28 File Offset: 0x00138128
		public TData Data
		{
			get
			{
				if (!this.useWeakReferences)
				{
					return this.data;
				}
				if (this.weakData == null)
				{
					return default(TData);
				}
				return this.weakData.Target as TData;
			}
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x00139F6B File Offset: 0x0013816B
		public void SetData(TData data, BaseUriWithWildcard path)
		{
			this.path = path;
			if (!this.useWeakReferences)
			{
				this.data = data;
				return;
			}
			if (data == null)
			{
				this.weakData = null;
				return;
			}
			this.weakData = new WeakReference(data);
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x00139FA5 File Offset: 0x001381A5
		public void SetChildNode(string name, SegmentHierarchyNode<TData> node)
		{
			this.children[name] = node;
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x00139FB4 File Offset: 0x001381B4
		public void Collect(List<KeyValuePair<BaseUriWithWildcard, TData>> result)
		{
			TData tdata = this.Data;
			if (tdata != null)
			{
				result.Add(new KeyValuePair<BaseUriWithWildcard, TData>(this.path, tdata));
			}
			foreach (SegmentHierarchyNode<TData> segmentHierarchyNode in this.children.Values)
			{
				segmentHierarchyNode.Collect(result);
			}
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0013A030 File Offset: 0x00138230
		public bool TryGetChild(string segment, out SegmentHierarchyNode<TData> value)
		{
			return this.children.TryGetValue(segment, out value);
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0013A040 File Offset: 0x00138240
		public void RemoveData()
		{
			this.SetData(default(TData), null);
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x0013A060 File Offset: 0x00138260
		public bool RemovePath(string[] path, int seg)
		{
			if (seg == path.Length)
			{
				this.RemoveData();
				return this.children.Count == 0;
			}
			SegmentHierarchyNode<TData> segmentHierarchyNode;
			if (!this.TryGetChild(path[seg], out segmentHierarchyNode))
			{
				return this.children.Count == 0 && this.Data == null;
			}
			if (segmentHierarchyNode.RemovePath(path, seg + 1))
			{
				this.children.Remove(path[seg]);
				return this.children.Count == 0 && this.Data == null;
			}
			return false;
		}

		// Token: 0x04003505 RID: 13573
		private BaseUriWithWildcard path;

		// Token: 0x04003506 RID: 13574
		private TData data;

		// Token: 0x04003507 RID: 13575
		private string name;

		// Token: 0x04003508 RID: 13576
		private Dictionary<string, SegmentHierarchyNode<TData>> children;

		// Token: 0x04003509 RID: 13577
		private WeakReference weakData;

		// Token: 0x0400350A RID: 13578
		private bool useWeakReferences;
	}
}
