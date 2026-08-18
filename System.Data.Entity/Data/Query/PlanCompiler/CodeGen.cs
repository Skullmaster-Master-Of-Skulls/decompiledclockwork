using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000045 RID: 69
	internal class CodeGen
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00018CC4 File Offset: 0x00016EC4
		internal static void Process(PlanCompiler compilerState, out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			CodeGen codeGen = new CodeGen(compilerState);
			codeGen.Process(out childCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00018CE1 File Offset: 0x00016EE1
		private CodeGen(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00018CF0 File Offset: 0x00016EF0
		private void Process(out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)this.Command.Root.Op;
			this.m_subCommands = new List<Node>(new Node[]
			{
				this.Command.Root
			});
			childCommands = new List<ProviderCommandInfo>(new ProviderCommandInfo[]
			{
				ProviderCommandInfoUtils.Create(this.Command, this.Command.Root)
			});
			resultColumnMap = this.BuildResultColumnMap(physicalProjectOp);
			columnCount = physicalProjectOp.Outputs.Count;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00018D70 File Offset: 0x00016F70
		private ColumnMap BuildResultColumnMap(PhysicalProjectOp projectOp)
		{
			Dictionary<Var, KeyValuePair<int, int>> varToCommandColumnMap = this.BuildVarMap();
			return ColumnMapTranslator.Translate(projectOp.ColumnMap, varToCommandColumnMap);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00018D94 File Offset: 0x00016F94
		private Dictionary<Var, KeyValuePair<int, int>> BuildVarMap()
		{
			Dictionary<Var, KeyValuePair<int, int>> dictionary = new Dictionary<Var, KeyValuePair<int, int>>();
			int num = 0;
			foreach (Node node in this.m_subCommands)
			{
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)node.Op;
				int num2 = 0;
				foreach (Var key in physicalProjectOp.Outputs)
				{
					KeyValuePair<int, int> value = new KeyValuePair<int, int>(num, num2);
					dictionary[key] = value;
					num2++;
				}
				num++;
			}
			return dictionary;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00018E58 File Offset: 0x00017058
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x0400075C RID: 1884
		private PlanCompiler m_compilerState;

		// Token: 0x0400075D RID: 1885
		private List<Node> m_subCommands;
	}
}
