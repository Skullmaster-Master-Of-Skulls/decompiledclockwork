using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000202 RID: 514
	internal class NamespaceList
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0006CE38 File Offset: 0x0006BE38
		public NamespaceList()
		{
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0006CE40 File Offset: 0x0006BE40
		public NamespaceList(string namespaces, string targetNamespace)
		{
			this.targetNamespace = targetNamespace;
			namespaces = namespaces.Trim();
			if (namespaces == "##any" || namespaces.Length == 0)
			{
				this.type = NamespaceList.ListType.Any;
				return;
			}
			if (namespaces == "##other")
			{
				this.type = NamespaceList.ListType.Other;
				return;
			}
			this.type = NamespaceList.ListType.Set;
			this.set = new Hashtable();
			foreach (string text in XmlConvert.SplitString(namespaces))
			{
				if (text == "##local")
				{
					this.set[string.Empty] = string.Empty;
				}
				else if (text == "##targetNamespace")
				{
					this.set[targetNamespace] = targetNamespace;
				}
				else
				{
					XmlConvert.ToUri(text);
					this.set[text] = text;
				}
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0006CF14 File Offset: 0x0006BF14
		public NamespaceList Clone()
		{
			NamespaceList namespaceList = (NamespaceList)base.MemberwiseClone();
			if (this.type == NamespaceList.ListType.Set)
			{
				namespaceList.set = (Hashtable)this.set.Clone();
			}
			return namespaceList;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0006CF4D File Offset: 0x0006BF4D
		public NamespaceList.ListType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0006CF55 File Offset: 0x0006BF55
		public string Excluded
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0006CF60 File Offset: 0x0006BF60
		public ICollection Enumerate
		{
			get
			{
				switch (this.type)
				{
				case NamespaceList.ListType.Set:
					return this.set.Keys;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0006CF9C File Offset: 0x0006BF9C
		public virtual bool Allows(string ns)
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return true;
			case NamespaceList.ListType.Other:
				return ns != this.targetNamespace && ns.Length != 0;
			case NamespaceList.ListType.Set:
				return this.set[ns] != null;
			default:
				return false;
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0006CFF7 File Offset: 0x0006BFF7
		public bool Allows(XmlQualifiedName qname)
		{
			return this.Allows(qname.Namespace);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0006D008 File Offset: 0x0006C008
		public override string ToString()
		{
			switch (this.type)
			{
			case NamespaceList.ListType.Any:
				return "##any";
			case NamespaceList.ListType.Other:
				return "##other";
			case NamespaceList.ListType.Set:
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj in this.set.Keys)
				{
					string text = (string)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					if (text == this.targetNamespace)
					{
						stringBuilder.Append("##targetNamespace");
					}
					else if (text.Length == 0)
					{
						stringBuilder.Append("##local");
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			default:
				return string.Empty;
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0006D0F4 File Offset: 0x0006C0F4
		public static bool IsSubset(NamespaceList sub, NamespaceList super)
		{
			if (super.type == NamespaceList.ListType.Any)
			{
				return true;
			}
			if (sub.type == NamespaceList.ListType.Other && super.type == NamespaceList.ListType.Other)
			{
				return super.targetNamespace == sub.targetNamespace;
			}
			if (sub.type != NamespaceList.ListType.Set)
			{
				return false;
			}
			if (super.type == NamespaceList.ListType.Other)
			{
				return !sub.set.Contains(super.targetNamespace);
			}
			foreach (object obj in sub.set.Keys)
			{
				string key = (string)obj;
				if (!super.set.Contains(key))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0006D1B8 File Offset: 0x0006C1B8
		public static NamespaceList Union(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = new NamespaceList();
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					using (IEnumerator enumerator = o2.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							namespaceList.set[text] = text;
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o1.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o2.Clone();
						}
					}
					else if (o2.targetNamespace != string.Empty)
					{
						namespaceList = o1.CompareSetToOther(o2);
					}
					else if (o1.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
				else if (o2.type == NamespaceList.ListType.Set && o1.type == NamespaceList.ListType.Other)
				{
					if (v1Compat)
					{
						if (o2.set.Contains(o2.targetNamespace))
						{
							namespaceList = new NamespaceList();
						}
						else
						{
							namespaceList = o1.Clone();
						}
					}
					else if (o1.targetNamespace != string.Empty)
					{
						namespaceList = o2.CompareSetToOther(o1);
					}
					else if (o2.set.Contains(string.Empty))
					{
						namespaceList = new NamespaceList();
					}
					else
					{
						namespaceList = new NamespaceList("##other", string.Empty);
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0006D3B8 File Offset: 0x0006C3B8
		private NamespaceList CompareSetToOther(NamespaceList other)
		{
			NamespaceList result;
			if (this.set.Contains(other.targetNamespace))
			{
				if (this.set.Contains(string.Empty))
				{
					result = new NamespaceList();
				}
				else
				{
					result = new NamespaceList("##other", string.Empty);
				}
			}
			else if (this.set.Contains(string.Empty))
			{
				result = null;
			}
			else
			{
				result = other.Clone();
			}
			return result;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0006D424 File Offset: 0x0006C424
		public static NamespaceList Intersection(NamespaceList o1, NamespaceList o2, bool v1Compat)
		{
			NamespaceList namespaceList = null;
			if (o1.type == NamespaceList.ListType.Any)
			{
				namespaceList = o2.Clone();
			}
			else if (o2.type == NamespaceList.ListType.Any)
			{
				namespaceList = o1.Clone();
			}
			else if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Other)
			{
				namespaceList = o1.Clone();
				namespaceList.RemoveNamespace(o2.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Set)
			{
				namespaceList = o2.Clone();
				namespaceList.RemoveNamespace(o1.targetNamespace);
				if (!v1Compat)
				{
					namespaceList.RemoveNamespace(string.Empty);
				}
			}
			else
			{
				if (o1.type == NamespaceList.ListType.Set && o2.type == NamespaceList.ListType.Set)
				{
					namespaceList = o1.Clone();
					namespaceList = new NamespaceList();
					namespaceList.type = NamespaceList.ListType.Set;
					namespaceList.set = new Hashtable();
					using (IEnumerator enumerator = o1.set.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string text = (string)obj;
							if (o2.set.Contains(text))
							{
								namespaceList.set.Add(text, text);
							}
						}
						return namespaceList;
					}
				}
				if (o1.type == NamespaceList.ListType.Other && o2.type == NamespaceList.ListType.Other)
				{
					if (o1.targetNamespace == o2.targetNamespace)
					{
						namespaceList = o1.Clone();
						return namespaceList;
					}
					if (!v1Compat)
					{
						if (o1.targetNamespace == string.Empty)
						{
							namespaceList = o2.Clone();
						}
						else if (o2.targetNamespace == string.Empty)
						{
							namespaceList = o1.Clone();
						}
					}
				}
			}
			return namespaceList;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006D5D0 File Offset: 0x0006C5D0
		private void RemoveNamespace(string tns)
		{
			if (this.set[tns] != null)
			{
				this.set.Remove(tns);
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0006D5EC File Offset: 0x0006C5EC
		public bool IsEmpty()
		{
			return this.type == NamespaceList.ListType.Set && (this.set == null || this.set.Count == 0);
		}

		// Token: 0x04000E51 RID: 3665
		private NamespaceList.ListType type;

		// Token: 0x04000E52 RID: 3666
		private Hashtable set;

		// Token: 0x04000E53 RID: 3667
		private string targetNamespace;

		// Token: 0x02000203 RID: 515
		public enum ListType
		{
			// Token: 0x04000E55 RID: 3669
			Any,
			// Token: 0x04000E56 RID: 3670
			Other,
			// Token: 0x04000E57 RID: 3671
			Set
		}
	}
}
