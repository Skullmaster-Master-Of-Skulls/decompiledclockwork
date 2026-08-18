using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000028 RID: 40
	internal sealed class LogicalExpr : ValueQuery
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00004A72 File Offset: 0x00002C72
		public LogicalExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			this.op = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004A8F File Offset: 0x00002C8F
		private LogicalExpr(LogicalExpr other) : base(other)
		{
			this.op = other.op;
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004AC6 File Offset: 0x00002CC6
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Operator.Op op = this.op;
			object obj = this.opnd1.Evaluate(nodeIterator);
			object obj2 = this.opnd2.Evaluate(nodeIterator);
			int num = (int)base.GetXPathType(obj);
			int num2 = (int)base.GetXPathType(obj2);
			if (num < num2)
			{
				op = Operator.InvertOperator(op);
				object obj3 = obj;
				obj = obj2;
				obj2 = obj3;
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			if (op == Operator.Op.EQ || op == Operator.Op.NE)
			{
				return LogicalExpr.CompXsltE[num][num2](op, obj, obj2);
			}
			return LogicalExpr.CompXsltO[num][num2](op, obj, obj2);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004B74 File Offset: 0x00002D74
		private static bool cmpQueryQueryE(Operator.Op op, object val1, object val2)
		{
			bool flag = op == Operator.Op.EQ;
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_15:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				string value = nodeSet.Value;
				while (value == nodeSet2.Value != flag)
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_15;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004BD8 File Offset: 0x00002DD8
		private static bool cmpQueryQueryO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_10:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				double n = NumberFunctions.Number(nodeSet.Value);
				while (!LogicalExpr.cmpNumberNumber(op, n, NumberFunctions.Number(nodeSet2.Value)))
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_10;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004C40 File Offset: 0x00002E40
		private static bool cmpQueryNumber(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = (double)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumber(op, NumberFunctions.Number(nodeSet.Value), n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004C80 File Offset: 0x00002E80
		private static bool cmpQueryStringE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			string n = (string)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, nodeSet.Value, n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004CBC File Offset: 0x00002EBC
		private static bool cmpQueryStringO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = NumberFunctions.Number((string)val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(nodeSet.Value), n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004D00 File Offset: 0x00002F00
		private static bool cmpRtfQueryE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, n, nodeSet.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004D3C File Offset: 0x00002F3C
		private static bool cmpRtfQueryO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, n, NumberFunctions.Number(nodeSet.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004D80 File Offset: 0x00002F80
		private static bool cmpQueryBoolE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			bool n = nodeSet.MoveNext();
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004DAC File Offset: 0x00002FAC
		private static bool cmpQueryBoolO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = nodeSet.MoveNext() ? 1.0 : 0.0;
			double n2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004DF3 File Offset: 0x00002FF3
		private static bool cmpBoolBoolE(Operator.Op op, bool n1, bool n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E00 File Offset: 0x00003000
		private static bool cmpBoolBoolE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004E24 File Offset: 0x00003024
		private static bool cmpBoolBoolO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((bool)val1);
			double n2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004E54 File Offset: 0x00003054
		private static bool cmpBoolNumberE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = BooleanFunctions.toBoolean((double)val2);
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004E7C File Offset: 0x0000307C
		private static bool cmpBoolNumberO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((bool)val1);
			double n2 = (double)val2;
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004EA4 File Offset: 0x000030A4
		private static bool cmpBoolStringE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = BooleanFunctions.toBoolean((string)val2);
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004ECC File Offset: 0x000030CC
		private static bool cmpRtfBoolE(Operator.Op op, object val1, object val2)
		{
			bool n = BooleanFunctions.toBoolean(LogicalExpr.Rtf(val1));
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004EF4 File Offset: 0x000030F4
		private static bool cmpBoolStringO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number((bool)val1), NumberFunctions.Number((string)val2));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F12 File Offset: 0x00003112
		private static bool cmpRtfBoolO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(LogicalExpr.Rtf(val1)), NumberFunctions.Number((bool)val2));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004F30 File Offset: 0x00003130
		private static bool cmpNumberNumber(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.EQ:
				return n1 == n2;
			case Operator.Op.NE:
				return n1 != n2;
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004F87 File Offset: 0x00003187
		private static bool cmpNumberNumberO(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004FC0 File Offset: 0x000031C0
		private static bool cmpNumberNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val1;
			double n2 = (double)val2;
			return LogicalExpr.cmpNumberNumber(op, n, n2);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004FE4 File Offset: 0x000031E4
		private static bool cmpStringNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val2;
			double n2 = NumberFunctions.Number((string)val1);
			return LogicalExpr.cmpNumberNumber(op, n2, n);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000500C File Offset: 0x0000320C
		private static bool cmpRtfNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val2;
			double n2 = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			return LogicalExpr.cmpNumberNumber(op, n2, n);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005034 File Offset: 0x00003234
		private static bool cmpStringStringE(Operator.Op op, string n1, string n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005044 File Offset: 0x00003244
		private static bool cmpStringStringE(Operator.Op op, object val1, object val2)
		{
			string n = (string)val1;
			string n2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005068 File Offset: 0x00003268
		private static bool cmpRtfStringE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			string n2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000508C File Offset: 0x0000328C
		private static bool cmpRtfRtfE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			string n2 = LogicalExpr.Rtf(val2);
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000050B0 File Offset: 0x000032B0
		private static bool cmpStringStringO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((string)val1);
			double n2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000050E0 File Offset: 0x000032E0
		private static bool cmpRtfStringO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double n2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005110 File Offset: 0x00003310
		private static bool cmpRtfRtfO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double n2 = NumberFunctions.Number(LogicalExpr.Rtf(val2));
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000513D File Offset: 0x0000333D
		public override XPathNodeIterator Clone()
		{
			return new LogicalExpr(this);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005145 File Offset: 0x00003345
		private static string Rtf(object o)
		{
			return ((XPathNavigator)o).Value;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005152 File Offset: 0x00003352
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005158 File Offset: 0x00003358
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", this.op.ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000051B0 File Offset: 0x000033B0
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalExpr()
		{
			LogicalExpr.cmpXslt[][] array = new LogicalExpr.cmpXslt[5][];
			int num = 0;
			LogicalExpr.cmpXslt[] array2 = new LogicalExpr.cmpXslt[5];
			array2[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array[num] = array2;
			int num2 = 1;
			LogicalExpr.cmpXslt[] array3 = new LogicalExpr.cmpXslt[5];
			array3[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array3[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringE);
			array[num2] = array3;
			int num3 = 2;
			LogicalExpr.cmpXslt[] array4 = new LogicalExpr.cmpXslt[5];
			array4[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberE);
			array4[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringE);
			array4[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolE);
			array[num3] = array4;
			int num4 = 3;
			LogicalExpr.cmpXslt[] array5 = new LogicalExpr.cmpXslt[5];
			array5[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array5[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringE);
			array5[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolE);
			array5[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryE);
			array[num4] = array5;
			array[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfE)
			};
			LogicalExpr.CompXsltE = array;
			LogicalExpr.cmpXslt[][] array6 = new LogicalExpr.cmpXslt[5][];
			int num5 = 0;
			LogicalExpr.cmpXslt[] array7 = new LogicalExpr.cmpXslt[5];
			array7[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array6[num5] = array7;
			int num6 = 1;
			LogicalExpr.cmpXslt[] array8 = new LogicalExpr.cmpXslt[5];
			array8[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array8[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringO);
			array6[num6] = array8;
			int num7 = 2;
			LogicalExpr.cmpXslt[] array9 = new LogicalExpr.cmpXslt[5];
			array9[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberO);
			array9[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringO);
			array9[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolO);
			array6[num7] = array9;
			int num8 = 3;
			LogicalExpr.cmpXslt[] array10 = new LogicalExpr.cmpXslt[5];
			array10[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array10[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringO);
			array10[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolO);
			array10[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryO);
			array6[num8] = array10;
			array6[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfO)
			};
			LogicalExpr.CompXsltO = array6;
		}

		// Token: 0x0400009D RID: 157
		private Operator.Op op;

		// Token: 0x0400009E RID: 158
		private Query opnd1;

		// Token: 0x0400009F RID: 159
		private Query opnd2;

		// Token: 0x040000A0 RID: 160
		private static readonly LogicalExpr.cmpXslt[][] CompXsltE;

		// Token: 0x040000A1 RID: 161
		private static readonly LogicalExpr.cmpXslt[][] CompXsltO;

		// Token: 0x020002FD RID: 765
		// (Invoke) Token: 0x06002D85 RID: 11653
		private delegate bool cmpXslt(Operator.Op op, object val1, object val2);

		// Token: 0x020002FE RID: 766
		private struct NodeSet
		{
			// Token: 0x06002D88 RID: 11656 RVA: 0x000ECA8F File Offset: 0x000EAC8F
			public NodeSet(object opnd)
			{
				this.opnd = (Query)opnd;
				this.current = null;
			}

			// Token: 0x06002D89 RID: 11657 RVA: 0x000ECAA4 File Offset: 0x000EACA4
			public bool MoveNext()
			{
				this.current = this.opnd.Advance();
				return this.current != null;
			}

			// Token: 0x06002D8A RID: 11658 RVA: 0x000ECAC0 File Offset: 0x000EACC0
			public void Reset()
			{
				this.opnd.Reset();
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000ECACD File Offset: 0x000EACCD
			public string Value
			{
				get
				{
					return this.current.Value;
				}
			}

			// Token: 0x040013FB RID: 5115
			private Query opnd;

			// Token: 0x040013FC RID: 5116
			private XPathNavigator current;
		}
	}
}
