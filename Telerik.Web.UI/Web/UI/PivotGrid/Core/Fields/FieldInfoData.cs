using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB7 RID: 3255
	public class FieldInfoData : IFieldInfoData
	{
		// Token: 0x060079C8 RID: 31176 RVA: 0x001BF460 File Offset: 0x001BD660
		public FieldInfoData(ContainerNode root)
		{
			if (root == null)
			{
				throw new ArgumentNullException("root");
			}
			this.RootFieldInfo = root;
			this.CreateDescriptionDictionary(this.RootFieldInfo.Children);
		}

		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x001BF499 File Offset: 0x001BD699
		// (set) Token: 0x060079CA RID: 31178 RVA: 0x001BF4A1 File Offset: 0x001BD6A1
		public ContainerNode RootFieldInfo { get; private set; }

		// Token: 0x060079CB RID: 31179 RVA: 0x001BF4AA File Offset: 0x001BD6AA
		public IPivotFieldInfo GetFieldDescriptionByMember(string name)
		{
			if (name == null)
			{
				return null;
			}
			return this.GetFieldDescriptionFromCache(name);
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x001BF4B8 File Offset: 0x001BD6B8
		private IPivotFieldInfo GetFieldDescriptionFromCache(string name)
		{
			if (this.cachedFieldDescriptions.ContainsKey(name))
			{
				return this.cachedFieldDescriptions[name];
			}
			return null;
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x001BF4D8 File Offset: 0x001BD6D8
		private void CreateDescriptionDictionary(IEnumerable<ContainerNode> children)
		{
			if (children == null)
			{
				return;
			}
			foreach (ContainerNode containerNode in children)
			{
				this.AddToDescriptorsIfOkay(containerNode);
				this.CreateDescriptionDictionary(containerNode.Children);
			}
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x001BF530 File Offset: 0x001BD730
		private void AddToDescriptorsIfOkay(ContainerNode node)
		{
			FieldInfoNode fieldInfoNode = node as FieldInfoNode;
			if (fieldInfoNode != null)
			{
				this.cachedFieldDescriptions[fieldInfoNode.FieldInfo.Name] = fieldInfoNode.FieldInfo;
			}
		}

		// Token: 0x04002153 RID: 8531
		private Dictionary<string, IPivotFieldInfo> cachedFieldDescriptions = new Dictionary<string, IPivotFieldInfo>();
	}
}
