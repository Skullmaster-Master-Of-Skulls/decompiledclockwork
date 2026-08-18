using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E2 RID: 482
	internal class CompiledIdentityConstraint
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x000ACAA9 File Offset: 0x000AACA9
		public CompiledIdentityConstraint.ConstraintRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x000ACAB1 File Offset: 0x000AACB1
		public Asttree Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x000ACAB9 File Offset: 0x000AACB9
		public Asttree[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000ACAC1 File Offset: 0x000AACC1
		private CompiledIdentityConstraint()
		{
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000ACAE0 File Offset: 0x000AACE0
		public CompiledIdentityConstraint(XmlSchemaIdentityConstraint constraint, XmlNamespaceManager nsmgr)
		{
			this.name = constraint.QualifiedName;
			try
			{
				this.selector = new Asttree(constraint.Selector.XPath, false, nsmgr);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(constraint.Selector);
				throw ex;
			}
			XmlSchemaObjectCollection xmlSchemaObjectCollection = constraint.Fields;
			this.fields = new Asttree[xmlSchemaObjectCollection.Count];
			for (int i = 0; i < xmlSchemaObjectCollection.Count; i++)
			{
				try
				{
					this.fields[i] = new Asttree(((XmlSchemaXPath)xmlSchemaObjectCollection[i]).XPath, true, nsmgr);
				}
				catch (XmlSchemaException ex2)
				{
					ex2.SetSource(constraint.Fields[i]);
					throw ex2;
				}
			}
			if (constraint is XmlSchemaUnique)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Unique;
				return;
			}
			if (constraint is XmlSchemaKey)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Key;
				return;
			}
			this.role = CompiledIdentityConstraint.ConstraintRole.Keyref;
			this.refer = ((XmlSchemaKeyref)constraint).Refer;
		}

		// Token: 0x04000D82 RID: 3458
		internal XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000D83 RID: 3459
		private CompiledIdentityConstraint.ConstraintRole role;

		// Token: 0x04000D84 RID: 3460
		private Asttree selector;

		// Token: 0x04000D85 RID: 3461
		private Asttree[] fields;

		// Token: 0x04000D86 RID: 3462
		internal XmlQualifiedName refer = XmlQualifiedName.Empty;

		// Token: 0x04000D87 RID: 3463
		public static readonly CompiledIdentityConstraint Empty = new CompiledIdentityConstraint();

		// Token: 0x0200048B RID: 1163
		public enum ConstraintRole
		{
			// Token: 0x04001E0E RID: 7694
			Unique,
			// Token: 0x04001E0F RID: 7695
			Key,
			// Token: 0x04001E10 RID: 7696
			Keyref
		}
	}
}
