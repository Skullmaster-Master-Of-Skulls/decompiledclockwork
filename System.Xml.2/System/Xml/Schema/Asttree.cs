using System;
using System.Collections;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020001DC RID: 476
	internal class Asttree
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000AB5DE File Offset: 0x000A97DE
		internal ArrayList SubtreeArray
		{
			get
			{
				return this.fAxisArray;
			}
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000AB5E6 File Offset: 0x000A97E6
		public Asttree(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			this.xpathexpr = xPath;
			this.isField = isField;
			this.nsmgr = nsmgr;
			this.CompileXPath(xPath, isField, nsmgr);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000AB60C File Offset: 0x000A980C
		private static bool IsNameTest(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Child && ast.NodeType == XPathNodeType.Element;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000AB622 File Offset: 0x000A9822
		internal static bool IsAttribute(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Attribute && ast.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000AB638 File Offset: 0x000A9838
		private static bool IsDescendantOrSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.DescendantOrSelf && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000AB655 File Offset: 0x000A9855
		internal static bool IsSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Self && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000AB674 File Offset: 0x000A9874
		public void CompileXPath(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			if (xPath == null || xPath.Length == 0)
			{
				throw new XmlSchemaException("Sch_EmptyXPath", string.Empty);
			}
			string[] array = xPath.Split(new char[]
			{
				'|'
			});
			ArrayList arrayList = new ArrayList(array.Length);
			this.fAxisArray = new ArrayList(array.Length);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Axis value = (Axis)XPathParser.ParseXPathExpresion(array[i]);
					arrayList.Add(value);
				}
			}
			catch
			{
				throw new XmlSchemaException("Sch_ICXpathError", xPath);
			}
			int j = 0;
			while (j < arrayList.Count)
			{
				Axis axis = (Axis)arrayList[j];
				Axis axis2;
				if ((axis2 = axis) == null)
				{
					throw new XmlSchemaException("Sch_ICXpathError", xPath);
				}
				Axis axis3 = axis2;
				if (Asttree.IsAttribute(axis2))
				{
					if (!isField)
					{
						throw new XmlSchemaException("Sch_SelectorAttr", xPath);
					}
					this.SetURN(axis2, nsmgr);
					try
					{
						axis2 = (Axis)axis2.Input;
						goto IL_12A;
					}
					catch
					{
						throw new XmlSchemaException("Sch_ICXpathError", xPath);
					}
					goto IL_DF;
				}
				IL_12A:
				if (axis2 == null || (!Asttree.IsNameTest(axis2) && !Asttree.IsSelf(axis2)))
				{
					axis3.Input = null;
					if (axis2 == null)
					{
						if (Asttree.IsSelf(axis) && axis.Input != null)
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis.Input), false));
						}
						else
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis), false));
						}
					}
					else
					{
						if (!Asttree.IsDescendantOrSelf(axis2))
						{
							throw new XmlSchemaException("Sch_ICXpathError", xPath);
						}
						try
						{
							axis2 = (Axis)axis2.Input;
						}
						catch
						{
							throw new XmlSchemaException("Sch_ICXpathError", xPath);
						}
						if (axis2 == null || !Asttree.IsSelf(axis2) || axis2.Input != null)
						{
							throw new XmlSchemaException("Sch_ICXpathError", xPath);
						}
						if (Asttree.IsSelf(axis) && axis.Input != null)
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis.Input), true));
						}
						else
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis), true));
						}
					}
					j++;
					continue;
				}
				IL_DF:
				if (Asttree.IsSelf(axis2) && axis != axis2)
				{
					axis3.Input = axis2.Input;
				}
				else
				{
					axis3 = axis2;
					if (Asttree.IsNameTest(axis2))
					{
						this.SetURN(axis2, nsmgr);
					}
				}
				try
				{
					axis2 = (Axis)axis2.Input;
				}
				catch
				{
					throw new XmlSchemaException("Sch_ICXpathError", xPath);
				}
				goto IL_12A;
			}
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000AB908 File Offset: 0x000A9B08
		private void SetURN(Axis axis, XmlNamespaceManager nsmgr)
		{
			if (axis.Prefix.Length != 0)
			{
				axis.Urn = nsmgr.LookupNamespace(axis.Prefix);
				if (axis.Urn == null)
				{
					throw new XmlSchemaException("Sch_UnresolvedPrefix", axis.Prefix);
				}
			}
			else
			{
				if (axis.Name.Length != 0)
				{
					axis.Urn = null;
					return;
				}
				axis.Urn = "";
			}
		}

		// Token: 0x04000D60 RID: 3424
		private ArrayList fAxisArray;

		// Token: 0x04000D61 RID: 3425
		private string xpathexpr;

		// Token: 0x04000D62 RID: 3426
		private bool isField;

		// Token: 0x04000D63 RID: 3427
		private XmlNamespaceManager nsmgr;
	}
}
