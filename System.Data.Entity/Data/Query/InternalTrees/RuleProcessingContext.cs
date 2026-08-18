using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E9 RID: 233
	internal abstract class RuleProcessingContext
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0003C799 File Offset: 0x0003A999
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void PreProcess(Node node)
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void PreProcessSubTree(Node node)
		{
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void PostProcess(Node node, Rule rule)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void PostProcessSubTree(Node node)
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003C7A1 File Offset: 0x0003A9A1
		internal virtual int GetHashCode(Node node)
		{
			return node.GetHashCode();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003C7A9 File Offset: 0x0003A9A9
		internal RuleProcessingContext(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x04000998 RID: 2456
		private Command m_command;
	}
}
