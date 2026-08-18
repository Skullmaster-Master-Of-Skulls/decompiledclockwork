using System;

namespace System.Xml.Schema
{
	// Token: 0x02000188 RID: 392
	internal class CompiledIdentityConstraint
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0005882E File Offset: 0x0005782E
		public CompiledIdentityConstraint.ConstraintRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00058836 File Offset: 0x00057836
		public Asttree Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0005883E File Offset: 0x0005783E
		public Asttree[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00058846 File Offset: 0x00057846
		private CompiledIdentityConstraint()
		{
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00058864 File Offset: 0x00057864
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

		// Token: 0x04000C87 RID: 3207
		internal XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000C88 RID: 3208
		private CompiledIdentityConstraint.ConstraintRole role;

		// Token: 0x04000C89 RID: 3209
		private Asttree selector;

		// Token: 0x04000C8A RID: 3210
		private Asttree[] fields;

		// Token: 0x04000C8B RID: 3211
		internal XmlQualifiedName refer = XmlQualifiedName.Empty;

		// Token: 0x04000C8C RID: 3212
		public static readonly CompiledIdentityConstraint Empty = new CompiledIdentityConstraint();

		// Token: 0x02000189 RID: 393
		public enum ConstraintRole
		{
			// Token: 0x04000C8E RID: 3214
			Unique,
			// Token: 0x04000C8F RID: 3215
			Key,
			// Token: 0x04000C90 RID: 3216
			Keyref
		}
	}
}
