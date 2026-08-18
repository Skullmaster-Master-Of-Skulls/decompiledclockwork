using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000015 RID: 21
	public abstract class ImportExportStatement : AstNode, IEnumerable<AstNode>, IEnumerable, IModuleReference
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003DC4 File Offset: 0x00001FC4
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00003DCC File Offset: 0x00001FCC
		public Context KeywordContext { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003DD5 File Offset: 0x00001FD5
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00003DDD File Offset: 0x00001FDD
		public Context OpenContext { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003DE6 File Offset: 0x00001FE6
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00003DEE File Offset: 0x00001FEE
		public Context CloseContext { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003DF7 File Offset: 0x00001FF7
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00003DFF File Offset: 0x00001FFF
		public Context FromContext { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003E08 File Offset: 0x00002008
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00003E10 File Offset: 0x00002010
		public Context ModuleContext { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003E19 File Offset: 0x00002019
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003E21 File Offset: 0x00002021
		public string ModuleName { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003E2A File Offset: 0x0000202A
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003E32 File Offset: 0x00002032
		public ModuleScope ReferencedModule { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003E3B File Offset: 0x0000203B
		public override bool IsDeclaration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003E3E File Offset: 0x0000203E
		protected ImportExportStatement(Context context) : base(context)
		{
			this.m_list = new List<AstNode>();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003E52 File Offset: 0x00002052
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003E5F File Offset: 0x0000205F
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes<AstNode>(this.m_list);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003E94 File Offset: 0x00002094
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (this.m_list[i] == oldNode)
				{
					oldNode.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
					if (newNode == null)
					{
						this.m_list.RemoveAt(i);
					}
					else
					{
						this.m_list[i] = newNode;
						newNode.Parent = this;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003F08 File Offset: 0x00002108
		public ImportExportStatement Append(AstNode node)
		{
			ImportExportStatement importExportStatement = node as ImportExportStatement;
			if (importExportStatement != null)
			{
				for (int i = 0; i < importExportStatement.Count; i++)
				{
					this.Append(importExportStatement[i]);
				}
			}
			else if (node != null)
			{
				node.Parent = this;
				this.m_list.Add(node);
				base.Context.UpdateWith(node.Context);
			}
			return this;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003F6C File Offset: 0x0000216C
		public ImportExportStatement Insert(int position, AstNode node)
		{
			ImportExportStatement importExportStatement = node as ImportExportStatement;
			if (importExportStatement != null)
			{
				for (int i = 0; i < importExportStatement.Count; i++)
				{
					this.Insert(position + i, importExportStatement[i]);
				}
			}
			else if (node != null)
			{
				node.Parent = this;
				this.m_list.Insert(position, node);
				base.Context.UpdateWith(node.Context);
			}
			return this;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003FFC File Offset: 0x000021FC
		public void RemoveAt(int position)
		{
			this.m_list[position].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
			this.m_list.RemoveAt(position);
		}

		// Token: 0x17000053 RID: 83
		public AstNode this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				this.m_list[index].IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				if (value != null)
				{
					this.m_list[index] = value;
					this.m_list[index].Parent = this;
					return;
				}
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000040BC File Offset: 0x000022BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_list.Count > 0)
			{
				stringBuilder.Append(this.m_list[0].ToString());
				for (int i = 1; i < this.m_list.Count; i++)
				{
					stringBuilder.Append(" , ");
					stringBuilder.Append(this.m_list[i].ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004135 File Offset: 0x00002335
		public IEnumerator<AstNode> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004147 File Offset: 0x00002347
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0400003A RID: 58
		private List<AstNode> m_list;
	}
}
