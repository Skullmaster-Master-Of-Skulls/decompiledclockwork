using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000250 RID: 592
	internal class NamespaceList
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x000B9EF9 File Offset: 0x000B80F9
		public NamespaceList()
		{
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000B9F04 File Offset: 0x000B8104
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
			string[] array = XmlConvert.SplitString(namespaces);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "##local")
				{
					this.set[string.Empty] = string.Empty;
				}
				else if (array[i] == "##targetNamespace")
				{
					this.set[targetNamespace] = targetNamespace;
				}
				else
				{
					XmlConvert.ToUri(array[i]);
					this.set[array[i]] = array[i];
				}
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000B9FE0 File Offset: 0x000B81E0
		public NamespaceList Clone()
		{
			NamespaceList namespaceList = (NamespaceList)base.MemberwiseClone();
			if (this.type == NamespaceList.ListType.Set)
			{
				namespaceList.set = (Hashtable)this.set.Clone();
			}
			return namespaceList;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000BA019 File Offset: 0x000B8219
		public NamespaceList.ListType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000BA021 File Offset: 0x000B8221
		public string Excluded
		{
			get
			{
				return this.targetNamespace;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000BA02C File Offset: 0x000B822C
		public ICollection Enumerate
		{
			get
			{
				NamespaceList.ListType listType = this.type;
				if (listType > NamespaceList.ListType.Other && listType == NamespaceList.ListType.Set)
				{
					return this.set.Keys;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000BA05C File Offset: 0x000B825C
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

		// Token: 0x06002316 RID: 8982 RVA: 0x000BA0B1 File Offset: 0x000B82B1
		public bool Allows(XmlQualifiedName qname)
		{
			return this.Allows(qname.Namespace);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000BA0C0 File Offset: 0x000B82C0
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

		// Token: 0x06002318 RID: 8984 RVA: 0x000BA1AC File Offset: 0x000B83AC
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

		// Token: 0x06002319 RID: 8985 RVA: 0x000BA270 File Offset: 0x000B8470
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

		// Token: 0x0600231A RID: 8986 RVA: 0x000BA470 File Offset: 0x000B8670
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

		// Token: 0x0600231B RID: 8987 RVA: 0x000BA4DC File Offset: 0x000B86DC
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

		// Token: 0x0600231C RID: 8988 RVA: 0x000BA688 File Offset: 0x000B8888
		private void RemoveNamespace(string tns)
		{
			if (this.set[tns] != null)
			{
				this.set.Remove(tns);
			}
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000BA6A4 File Offset: 0x000B88A4
		public bool IsEmpty()
		{
			return this.type == NamespaceList.ListType.Set && (this.set == null || this.set.Count == 0);
		}

		// Token: 0x04000EB2 RID: 3762
		private NamespaceList.ListType type;

		// Token: 0x04000EB3 RID: 3763
		private Hashtable set;

		// Token: 0x04000EB4 RID: 3764
		private string targetNamespace;

		// Token: 0x02000491 RID: 1169
		public enum ListType
		{
			// Token: 0x04001E28 RID: 7720
			Any,
			// Token: 0x04001E29 RID: 7721
			Other,
			// Token: 0x04001E2A RID: 7722
			Set
		}
	}
}
