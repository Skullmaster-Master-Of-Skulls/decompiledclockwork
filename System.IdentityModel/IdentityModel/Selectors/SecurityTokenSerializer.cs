using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AA RID: 426
	public abstract class SecurityTokenSerializer
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003FC46 File Offset: 0x0003DE46
		public bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.CanReadTokenCore(reader);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003FC62 File Offset: 0x0003DE62
		public bool CanWriteToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return this.CanWriteTokenCore(token);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003FC7E File Offset: 0x0003DE7E
		public bool CanReadKeyIdentifier(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.CanReadKeyIdentifierCore(reader);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003FC9A File Offset: 0x0003DE9A
		public bool CanWriteKeyIdentifier(SecurityKeyIdentifier keyIdentifier)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			return this.CanWriteKeyIdentifierCore(keyIdentifier);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003FCB6 File Offset: 0x0003DEB6
		public bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.CanReadKeyIdentifierClauseCore(reader);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003FCD2 File Offset: 0x0003DED2
		public bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			return this.CanWriteKeyIdentifierClauseCore(keyIdentifierClause);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003FCEE File Offset: 0x0003DEEE
		public SecurityToken ReadToken(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.ReadTokenCore(reader, tokenResolver);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003FD0B File Offset: 0x0003DF0B
		public void WriteToken(XmlWriter writer, SecurityToken token)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this.WriteTokenCore(writer, token);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003FD3B File Offset: 0x0003DF3B
		public SecurityKeyIdentifier ReadKeyIdentifier(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.ReadKeyIdentifierCore(reader);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003FD57 File Offset: 0x0003DF57
		public void WriteKeyIdentifier(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			this.WriteKeyIdentifierCore(writer, keyIdentifier);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003FD87 File Offset: 0x0003DF87
		public SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.ReadKeyIdentifierClauseCore(reader);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003FDA3 File Offset: 0x0003DFA3
		public void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			this.WriteKeyIdentifierClauseCore(writer, keyIdentifierClause);
		}

		// Token: 0x06000DF8 RID: 3576
		protected abstract bool CanReadTokenCore(XmlReader reader);

		// Token: 0x06000DF9 RID: 3577
		protected abstract bool CanWriteTokenCore(SecurityToken token);

		// Token: 0x06000DFA RID: 3578
		protected abstract bool CanReadKeyIdentifierCore(XmlReader reader);

		// Token: 0x06000DFB RID: 3579
		protected abstract bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier);

		// Token: 0x06000DFC RID: 3580
		protected abstract bool CanReadKeyIdentifierClauseCore(XmlReader reader);

		// Token: 0x06000DFD RID: 3581
		protected abstract bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause);

		// Token: 0x06000DFE RID: 3582
		protected abstract SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver);

		// Token: 0x06000DFF RID: 3583
		protected abstract void WriteTokenCore(XmlWriter writer, SecurityToken token);

		// Token: 0x06000E00 RID: 3584
		protected abstract SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader);

		// Token: 0x06000E01 RID: 3585
		protected abstract void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier);

		// Token: 0x06000E02 RID: 3586
		protected abstract SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader);

		// Token: 0x06000E03 RID: 3587
		protected abstract void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause);

		// Token: 0x02000292 RID: 658
		internal abstract class KeyIdentifierClauseEntry
		{
			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x06001353 RID: 4947
			protected abstract XmlDictionaryString LocalName { get; }

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x06001354 RID: 4948
			protected abstract XmlDictionaryString NamespaceUri { get; }

			// Token: 0x06001355 RID: 4949 RVA: 0x00052A02 File Offset: 0x00050C02
			public virtual bool CanReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(this.LocalName, this.NamespaceUri);
			}

			// Token: 0x06001356 RID: 4950
			public abstract SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader);

			// Token: 0x06001357 RID: 4951
			public abstract bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause);

			// Token: 0x06001358 RID: 4952
			public abstract void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause);
		}

		// Token: 0x02000293 RID: 659
		internal abstract class StrEntry
		{
			// Token: 0x0600135A RID: 4954
			public abstract string GetTokenTypeUri();

			// Token: 0x0600135B RID: 4955
			public abstract Type GetTokenType(SecurityKeyIdentifierClause clause);

			// Token: 0x0600135C RID: 4956
			public abstract bool CanReadClause(XmlDictionaryReader reader, string tokenType);

			// Token: 0x0600135D RID: 4957
			public abstract SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNonce, int derivationLength, string tokenType);

			// Token: 0x0600135E RID: 4958
			public abstract bool SupportsCore(SecurityKeyIdentifierClause clause);

			// Token: 0x0600135F RID: 4959
			public abstract void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause);
		}

		// Token: 0x02000294 RID: 660
		internal abstract class SerializerEntries
		{
			// Token: 0x06001361 RID: 4961 RVA: 0x000024C1 File Offset: 0x000006C1
			public virtual void PopulateTokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntries)
			{
			}

			// Token: 0x06001362 RID: 4962 RVA: 0x000024C1 File Offset: 0x000006C1
			public virtual void PopulateKeyIdentifierEntries(IList<SecurityTokenSerializer.KeyIdentifierEntry> keyIdentifierEntries)
			{
			}

			// Token: 0x06001363 RID: 4963 RVA: 0x000024C1 File Offset: 0x000006C1
			public virtual void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> keyIdentifierClauseEntries)
			{
			}

			// Token: 0x06001364 RID: 4964 RVA: 0x000024C1 File Offset: 0x000006C1
			public virtual void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
			{
			}
		}

		// Token: 0x02000295 RID: 661
		internal abstract class KeyIdentifierEntry
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x06001366 RID: 4966
			protected abstract XmlDictionaryString LocalName { get; }

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001367 RID: 4967
			protected abstract XmlDictionaryString NamespaceUri { get; }

			// Token: 0x06001368 RID: 4968 RVA: 0x00052A16 File Offset: 0x00050C16
			public virtual bool CanReadKeyIdentifierCore(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(this.LocalName, this.NamespaceUri);
			}

			// Token: 0x06001369 RID: 4969
			public abstract SecurityKeyIdentifier ReadKeyIdentifierCore(XmlDictionaryReader reader);

			// Token: 0x0600136A RID: 4970
			public abstract bool SupportsCore(SecurityKeyIdentifier keyIdentifier);

			// Token: 0x0600136B RID: 4971
			public abstract void WriteKeyIdentifierCore(XmlDictionaryWriter writer, SecurityKeyIdentifier keyIdentifier);
		}

		// Token: 0x02000296 RID: 662
		internal abstract class TokenEntry
		{
			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x0600136D RID: 4973
			protected abstract XmlDictionaryString LocalName { get; }

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x0600136E RID: 4974
			protected abstract XmlDictionaryString NamespaceUri { get; }

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x00052A2A File Offset: 0x00050C2A
			public Type TokenType
			{
				get
				{
					return this.GetTokenTypes()[0];
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x06001370 RID: 4976
			public abstract string TokenTypeUri { get; }

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x06001371 RID: 4977
			protected abstract string ValueTypeUri { get; }

			// Token: 0x06001372 RID: 4978 RVA: 0x00052A34 File Offset: 0x00050C34
			public bool SupportsCore(Type tokenType)
			{
				Type[] array = this.GetTokenTypes();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsAssignableFrom(tokenType))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001373 RID: 4979
			protected abstract Type[] GetTokenTypesCore();

			// Token: 0x06001374 RID: 4980 RVA: 0x00052A64 File Offset: 0x00050C64
			public Type[] GetTokenTypes()
			{
				if (this.tokenTypes == null)
				{
					this.tokenTypes = this.GetTokenTypesCore();
				}
				return this.tokenTypes;
			}

			// Token: 0x06001375 RID: 4981 RVA: 0x00052A80 File Offset: 0x00050C80
			public virtual bool SupportsTokenTypeUri(string tokenTypeUri)
			{
				return this.TokenTypeUri == tokenTypeUri;
			}

			// Token: 0x04001134 RID: 4404
			private Type[] tokenTypes;
		}
	}
}
