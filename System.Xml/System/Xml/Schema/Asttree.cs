using System;
using System.Collections;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000182 RID: 386
	internal class Asttree
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005736A File Offset: 0x0005636A
		internal ArrayList SubtreeArray
		{
			get
			{
				return this.fAxisArray;
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00057372 File Offset: 0x00056372
		public Asttree(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			this.xpathexpr = xPath;
			this.isField = isField;
			this.nsmgr = nsmgr;
			this.CompileXPath(xPath, isField, nsmgr);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00057398 File Offset: 0x00056398
		private static bool IsNameTest(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Child && ast.NodeType == XPathNodeType.Element;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000573AE File Offset: 0x000563AE
		internal static bool IsAttribute(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Attribute && ast.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000573C4 File Offset: 0x000563C4
		private static bool IsDescendantOrSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.DescendantOrSelf && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000573E1 File Offset: 0x000563E1
		internal static bool IsSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Self && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00057400 File Offset: 0x00056400
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
				foreach (string xpathExpresion in array)
				{
					Axis value = (Axis)XPathParser.ParseXPathExpresion(xpathExpresion);
					arrayList.Add(value);
				}
			}
			catch
			{
				throw new XmlSchemaException("Sch_ICXpathError", xPath);
			}
			foreach (object obj in arrayList)
			{
				Axis axis = (Axis)obj;
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
						goto IL_14D;
					}
					catch
					{
						throw new XmlSchemaException("Sch_ICXpathError", xPath);
					}
					goto IL_FB;
				}
				IL_14D:
				if (axis2 == null || (!Asttree.IsNameTest(axis2) && !Asttree.IsSelf(axis2)))
				{
					axis3.Input = null;
					if (axis2 == null)
					{
						if (Asttree.IsSelf(axis) && axis.Input != null)
						{
							this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis.Input), false));
							continue;
						}
						this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis), false));
						continue;
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
							continue;
						}
						this.fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis), true));
						continue;
					}
				}
				IL_FB:
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
				goto IL_14D;
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00057718 File Offset: 0x00056718
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

		// Token: 0x04000C66 RID: 3174
		private ArrayList fAxisArray;

		// Token: 0x04000C67 RID: 3175
		private string xpathexpr;

		// Token: 0x04000C68 RID: 3176
		private bool isField;

		// Token: 0x04000C69 RID: 3177
		private XmlNamespaceManager nsmgr;
	}
}
