using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200061C RID: 1564
	internal abstract class RuleProcessingContext
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x0011AF50 File Offset: 0x00119150
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x0011AF58 File Offset: 0x00119158
		internal virtual void PreProcess(Node node)
		{
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x0011AF5A File Offset: 0x0011915A
		internal virtual void PreProcessSubTree(Node node)
		{
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x0011AF5C File Offset: 0x0011915C
		internal virtual void PostProcess(Node node, Rule rule)
		{
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x0011AF5E File Offset: 0x0011915E
		internal virtual void PostProcessSubTree(Node node)
		{
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x0011AF60 File Offset: 0x00119160
		internal virtual int GetHashCode(Node node)
		{
			return node.GetHashCode();
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x0011AF68 File Offset: 0x00119168
		internal RuleProcessingContext(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x04001728 RID: 5928
		private readonly Command m_command;
	}
}
