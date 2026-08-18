using System;

namespace System.Xml.Linq
{
	// Token: 0x02000021 RID: 33
	internal struct NamespaceResolver
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007CDC File Offset: 0x00005EDC
		public void PushScope()
		{
			this.scope++;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007CEC File Offset: 0x00005EEC
		public void PopScope()
		{
			NamespaceResolver.NamespaceDeclaration prev = this.declaration;
			if (prev != null)
			{
				do
				{
					prev = prev.prev;
					if (prev.scope != this.scope)
					{
						break;
					}
					if (prev == this.declaration)
					{
						this.declaration = null;
					}
					else
					{
						this.declaration.prev = prev.prev;
					}
					this.rover = null;
				}
				while (prev != this.declaration && this.declaration != null);
			}
			this.scope--;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007D64 File Offset: 0x00005F64
		public void Add(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this.scope;
			if (this.declaration == null)
			{
				this.declaration = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this.declaration.prev;
			}
			this.declaration.prev = namespaceDeclaration;
			this.rover = null;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public void AddFirst(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this.scope;
			if (this.declaration == null)
			{
				namespaceDeclaration.prev = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this.declaration.prev;
				this.declaration.prev = namespaceDeclaration;
			}
			this.declaration = namespaceDeclaration;
			this.rover = null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007E34 File Offset: 0x00006034
		public string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			if (this.rover != null && this.rover.ns == ns && (allowDefaultNamespace || this.rover.prefix.Length > 0))
			{
				return this.rover.prefix;
			}
			NamespaceResolver.NamespaceDeclaration prev = this.declaration;
			if (prev != null)
			{
				for (;;)
				{
					prev = prev.prev;
					if (prev.ns == ns)
					{
						NamespaceResolver.NamespaceDeclaration prev2 = this.declaration.prev;
						while (prev2 != prev && prev2.prefix != prev.prefix)
						{
							prev2 = prev2.prev;
						}
						if (prev2 == prev)
						{
							if (allowDefaultNamespace)
							{
								break;
							}
							if (prev.prefix.Length > 0)
							{
								goto Block_8;
							}
						}
					}
					if (prev == this.declaration)
					{
						goto IL_BB;
					}
				}
				this.rover = prev;
				return prev.prefix;
				Block_8:
				return prev.prefix;
			}
			IL_BB:
			return null;
		}

		// Token: 0x04000092 RID: 146
		private int scope;

		// Token: 0x04000093 RID: 147
		private NamespaceResolver.NamespaceDeclaration declaration;

		// Token: 0x04000094 RID: 148
		private NamespaceResolver.NamespaceDeclaration rover;

		// Token: 0x02000050 RID: 80
		private class NamespaceDeclaration
		{
			// Token: 0x0400015E RID: 350
			public string prefix;

			// Token: 0x0400015F RID: 351
			public XNamespace ns;

			// Token: 0x04000160 RID: 352
			public int scope;

			// Token: 0x04000161 RID: 353
			public NamespaceResolver.NamespaceDeclaration prev;
		}
	}
}
