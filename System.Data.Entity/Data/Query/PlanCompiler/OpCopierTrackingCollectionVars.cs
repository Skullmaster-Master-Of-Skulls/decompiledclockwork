using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000058 RID: 88
	internal class OpCopierTrackingCollectionVars : OpCopier
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x000250B5 File Offset: 0x000232B5
		private OpCopierTrackingCollectionVars(Command cmd) : base(cmd)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000250CC File Offset: 0x000232CC
		internal static Node Copy(Command cmd, Node n, out VarMap varMap, out Dictionary<Var, Node> newCollectionVarDefinitions)
		{
			OpCopierTrackingCollectionVars opCopierTrackingCollectionVars = new OpCopierTrackingCollectionVars(cmd);
			Node result = opCopierTrackingCollectionVars.CopyNode(n);
			varMap = opCopierTrackingCollectionVars.m_varMap;
			newCollectionVarDefinitions = opCopierTrackingCollectionVars.m_newCollectionVarDefinitions;
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000250FC File Offset: 0x000232FC
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			Node node = base.Visit(op, n);
			MultiStreamNestOp multiStreamNestOp = (MultiStreamNestOp)node.Op;
			for (int i = 0; i < multiStreamNestOp.CollectionInfo.Count; i++)
			{
				this.m_newCollectionVarDefinitions.Add(multiStreamNestOp.CollectionInfo[i].CollectionVar, node.Children[i + 1]);
			}
			return node;
		}

		// Token: 0x040007C1 RID: 1985
		private Dictionary<Var, Node> m_newCollectionVarDefinitions = new Dictionary<Var, Node>();
	}
}
