using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013BE RID: 5054
	internal class PropertyInfo
	{
		// Token: 0x0600D172 RID: 53618 RVA: 0x002E4FF3 File Offset: 0x002E31F3
		public PropertyInfo(PropertyMaker maker, PropertyList plist, FObj fo)
		{
			this.maker = maker;
			this.plist = plist;
			this.fo = fo;
		}

		// Token: 0x0600D173 RID: 53619 RVA: 0x002E5010 File Offset: 0x002E3210
		public bool inheritsSpecified()
		{
			return this.maker.InheritsSpecified();
		}

		// Token: 0x0600D174 RID: 53620 RVA: 0x002E5020 File Offset: 0x002E3220
		public IPercentBase GetPercentBase()
		{
			IPercentBase functionPercentBase = this.getFunctionPercentBase();
			if (functionPercentBase == null)
			{
				return this.maker.GetPercentBase(this.fo, this.plist);
			}
			return functionPercentBase;
		}

		// Token: 0x0600D175 RID: 53621 RVA: 0x002E5050 File Offset: 0x002E3250
		public int currentFontSize()
		{
			return this.plist.GetProperty("font-size").GetLength().MValue();
		}

		// Token: 0x0600D176 RID: 53622 RVA: 0x002E506C File Offset: 0x002E326C
		public FObj getFO()
		{
			return this.fo;
		}

		// Token: 0x0600D177 RID: 53623 RVA: 0x002E5074 File Offset: 0x002E3274
		public PropertyList getPropertyList()
		{
			return this.plist;
		}

		// Token: 0x0600D178 RID: 53624 RVA: 0x002E507C File Offset: 0x002E327C
		public void pushFunction(IFunction func)
		{
			if (this.stkFunction == null)
			{
				this.stkFunction = new Stack();
			}
			this.stkFunction.Push(func);
		}

		// Token: 0x0600D179 RID: 53625 RVA: 0x002E509D File Offset: 0x002E329D
		public void popFunction()
		{
			if (this.stkFunction != null)
			{
				this.stkFunction.Pop();
			}
		}

		// Token: 0x0600D17A RID: 53626 RVA: 0x002E50B4 File Offset: 0x002E32B4
		private IPercentBase getFunctionPercentBase()
		{
			if (this.stkFunction != null)
			{
				IFunction function = (IFunction)this.stkFunction.Peek();
				if (function != null)
				{
					return function.GetPercentBase();
				}
			}
			return null;
		}

		// Token: 0x0400382B RID: 14379
		private PropertyMaker maker;

		// Token: 0x0400382C RID: 14380
		private PropertyList plist;

		// Token: 0x0400382D RID: 14381
		private FObj fo;

		// Token: 0x0400382E RID: 14382
		private Stack stkFunction;
	}
}
