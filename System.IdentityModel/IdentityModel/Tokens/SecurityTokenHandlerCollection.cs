using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017A RID: 378
	public class SecurityTokenHandlerCollection : Collection<SecurityTokenHandler>
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x000373D5 File Offset: 0x000355D5
		public SecurityTokenHandlerCollection() : this(new SecurityTokenHandlerConfiguration())
		{
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000373E4 File Offset: 0x000355E4
		public SecurityTokenHandlerCollection(SecurityTokenHandlerConfiguration configuration)
		{
			if (configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configuration");
			}
			this.configuration = configuration;
			this.keyInfoSerializer = new KeyInfoSerializer(true);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00037433 File Offset: 0x00035633
		public SecurityTokenHandlerCollection(IEnumerable<SecurityTokenHandler> handlers) : this(handlers, new SecurityTokenHandlerConfiguration())
		{
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00037444 File Offset: 0x00035644
		public SecurityTokenHandlerCollection(IEnumerable<SecurityTokenHandler> handlers, SecurityTokenHandlerConfiguration configuration) : this(configuration)
		{
			if (handlers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("handlers");
			}
			foreach (SecurityTokenHandler item in handlers)
			{
				base.Add(item);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000374A8 File Offset: 0x000356A8
		public SecurityTokenHandlerConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000374B0 File Offset: 0x000356B0
		public IEnumerable<Type> TokenTypes
		{
			get
			{
				return this.handlersByType.Keys;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000374BD File Offset: 0x000356BD
		public IEnumerable<string> TokenTypeIdentifiers
		{
			get
			{
				return this.handlersByIdentifier.Keys;
			}
		}

		// Token: 0x170002F3 RID: 755
		public SecurityTokenHandler this[string tokenTypeIdentifier]
		{
			get
			{
				if (string.IsNullOrEmpty(tokenTypeIdentifier))
				{
					return null;
				}
				SecurityTokenHandler result;
				this.handlersByIdentifier.TryGetValue(tokenTypeIdentifier, out result);
				return result;
			}
		}

		// Token: 0x170002F4 RID: 756
		public SecurityTokenHandler this[SecurityToken token]
		{
			get
			{
				if (token == null)
				{
					return null;
				}
				return this[token.GetType()];
			}
		}

		// Token: 0x170002F5 RID: 757
		public SecurityTokenHandler this[Type tokenType]
		{
			get
			{
				SecurityTokenHandler result = null;
				if (tokenType != null)
				{
					this.handlersByType.TryGetValue(tokenType, out result);
				}
				return result;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00037530 File Offset: 0x00035730
		public static SecurityTokenHandlerCollection CreateDefaultSecurityTokenHandlerCollection()
		{
			return SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection(new SecurityTokenHandlerConfiguration());
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003753C File Offset: 0x0003573C
		public static SecurityTokenHandlerCollection CreateDefaultSecurityTokenHandlerCollection(SecurityTokenHandlerConfiguration configuration)
		{
			SecurityTokenHandlerCollection securityTokenHandlerCollection = new SecurityTokenHandlerCollection(new SecurityTokenHandler[]
			{
				new KerberosSecurityTokenHandler(),
				new RsaSecurityTokenHandler(),
				new SamlSecurityTokenHandler(),
				new Saml2SecurityTokenHandler(),
				new WindowsUserNameSecurityTokenHandler(),
				new X509SecurityTokenHandler(),
				new EncryptedSecurityTokenHandler(),
				new SessionSecurityTokenHandler()
			}, configuration);
			SecurityTokenHandlerCollection.defaultHandlerCollectionCount = securityTokenHandlerCollection.Count;
			return securityTokenHandlerCollection;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000375A2 File Offset: 0x000357A2
		internal SecurityTokenSerializer KeyInfoSerializer
		{
			get
			{
				return this.keyInfoSerializer;
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000375AC File Offset: 0x000357AC
		public void AddOrReplace(SecurityTokenHandler handler)
		{
			if (handler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("handler");
			}
			Type tokenType = handler.TokenType;
			if (tokenType != null && this.handlersByType.ContainsKey(tokenType))
			{
				base.Remove(this[tokenType]);
			}
			else
			{
				string[] tokenTypeIdentifiers = handler.GetTokenTypeIdentifiers();
				if (tokenTypeIdentifiers != null)
				{
					foreach (string text in tokenTypeIdentifiers)
					{
						if (text != null && this.handlersByIdentifier.ContainsKey(text))
						{
							base.Remove(this[text]);
							break;
						}
					}
				}
			}
			base.Add(handler);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00037644 File Offset: 0x00035844
		public bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler != null && securityTokenHandler.CanReadToken(reader))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000376AC File Offset: 0x000358AC
		public bool CanReadToken(string tokenString)
		{
			if (string.IsNullOrEmpty(tokenString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("tokenString");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler != null && securityTokenHandler.CanReadToken(tokenString))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00037718 File Offset: 0x00035918
		public bool CanWriteToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SecurityTokenHandler securityTokenHandler = this[token];
			return securityTokenHandler != null && securityTokenHandler.CanWriteToken;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00037750 File Offset: 0x00035950
		public SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			SecurityTokenHandler securityTokenHandler = this[tokenDescriptor.TokenType];
			if (securityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4020", new object[]
				{
					tokenDescriptor.TokenType
				})));
			}
			return securityTokenHandler.CreateToken(tokenDescriptor);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000377B0 File Offset: 0x000359B0
		public ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SecurityTokenHandler securityTokenHandler = this[token];
			if (securityTokenHandler == null || !securityTokenHandler.CanValidateToken)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4011", new object[]
				{
					token.GetType()
				})));
			}
			return securityTokenHandler.ValidateToken(token);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00037814 File Offset: 0x00035A14
		public SecurityToken ReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler != null && securityTokenHandler.CanReadToken(reader))
				{
					return securityTokenHandler.ReadToken(reader);
				}
			}
			return null;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00037884 File Offset: 0x00035A84
		public SecurityToken ReadToken(string tokenString)
		{
			if (string.IsNullOrEmpty(tokenString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("tokenString");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler != null && securityTokenHandler.CanReadToken(tokenString))
				{
					return securityTokenHandler.ReadToken(tokenString);
				}
			}
			return null;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000378F8 File Offset: 0x00035AF8
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
			SecurityTokenHandler securityTokenHandler = this[token];
			if (securityTokenHandler == null || !securityTokenHandler.CanWriteToken)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4010", new object[]
				{
					token.GetType()
				})));
			}
			securityTokenHandler.WriteToken(writer, token);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00037970 File Offset: 0x00035B70
		public string WriteToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SecurityTokenHandler securityTokenHandler = this[token];
			if (securityTokenHandler == null || !securityTokenHandler.CanWriteToken)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4010", new object[]
				{
					token.GetType()
				})));
			}
			return securityTokenHandler.WriteToken(token);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000379D3 File Offset: 0x00035BD3
		protected override void ClearItems()
		{
			base.ClearItems();
			this.handlersByIdentifier.Clear();
			this.handlersByType.Clear();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000379F4 File Offset: 0x00035BF4
		protected override void InsertItem(int index, SecurityTokenHandler item)
		{
			base.InsertItem(index, item);
			try
			{
				this.AddToDictionaries(item);
			}
			catch
			{
				base.RemoveItem(index);
				throw;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00037A2C File Offset: 0x00035C2C
		protected override void RemoveItem(int index)
		{
			SecurityTokenHandler handler = base.Items[index];
			base.RemoveItem(index);
			this.RemoveFromDictionaries(handler);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00037A54 File Offset: 0x00035C54
		protected override void SetItem(int index, SecurityTokenHandler item)
		{
			SecurityTokenHandler securityTokenHandler = base.Items[index];
			base.SetItem(index, item);
			this.RemoveFromDictionaries(securityTokenHandler);
			try
			{
				this.AddToDictionaries(item);
			}
			catch
			{
				base.SetItem(index, securityTokenHandler);
				this.AddToDictionaries(securityTokenHandler);
				throw;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00037AA8 File Offset: 0x00035CA8
		public bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.CanReadKeyIdentifierClauseCore(reader);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00037AC4 File Offset: 0x00035CC4
		protected virtual bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler.CanReadKeyIdentifierClause(reader))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00037B28 File Offset: 0x00035D28
		public SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.ReadKeyIdentifierClauseCore(reader);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00037B44 File Offset: 0x00035D44
		protected virtual SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler.CanReadKeyIdentifierClause(reader))
				{
					return securityTokenHandler.ReadKeyIdentifierClause(reader);
				}
			}
			return this.keyInfoSerializer.ReadKeyIdentifierClause(reader);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00037BBC File Offset: 0x00035DBC
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

		// Token: 0x06000C07 RID: 3079 RVA: 0x00037BEC File Offset: 0x00035DEC
		protected virtual void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this)
			{
				if (securityTokenHandler.CanWriteKeyIdentifierClause(keyIdentifierClause))
				{
					securityTokenHandler.WriteKeyIdentifierClause(writer, keyIdentifierClause);
					return;
				}
			}
			this.keyInfoSerializer.WriteKeyIdentifierClause(writer, keyIdentifierClause);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00037C74 File Offset: 0x00035E74
		private void AddToDictionaries(SecurityTokenHandler handler)
		{
			if (handler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("handler");
			}
			bool flag = false;
			string[] tokenTypeIdentifiers = handler.GetTokenTypeIdentifiers();
			if (tokenTypeIdentifiers != null)
			{
				foreach (string text in tokenTypeIdentifiers)
				{
					if (text != null)
					{
						this.handlersByIdentifier.Add(text, handler);
						flag = true;
					}
				}
			}
			Type tokenType = handler.TokenType;
			if (handler.TokenType != null)
			{
				try
				{
					this.handlersByType.Add(tokenType, handler);
				}
				catch
				{
					if (flag)
					{
						this.RemoveFromDictionaries(handler);
					}
					throw;
				}
			}
			handler.ContainingCollection = this;
			if (handler.Configuration == null)
			{
				handler.Configuration = this.configuration;
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00037D2C File Offset: 0x00035F2C
		private void RemoveFromDictionaries(SecurityTokenHandler handler)
		{
			string[] tokenTypeIdentifiers = handler.GetTokenTypeIdentifiers();
			if (tokenTypeIdentifiers != null)
			{
				foreach (string text in tokenTypeIdentifiers)
				{
					if (text != null)
					{
						this.handlersByIdentifier.Remove(text);
					}
				}
			}
			Type tokenType = handler.TokenType;
			if (tokenType != null && this.handlersByType.ContainsKey(tokenType))
			{
				this.handlersByType.Remove(tokenType);
			}
			handler.ContainingCollection = null;
			handler.Configuration = null;
		}

		// Token: 0x04000C4F RID: 3151
		internal static int defaultHandlerCollectionCount = 8;

		// Token: 0x04000C50 RID: 3152
		private Dictionary<string, SecurityTokenHandler> handlersByIdentifier = new Dictionary<string, SecurityTokenHandler>();

		// Token: 0x04000C51 RID: 3153
		private Dictionary<Type, SecurityTokenHandler> handlersByType = new Dictionary<Type, SecurityTokenHandler>();

		// Token: 0x04000C52 RID: 3154
		private SecurityTokenHandlerConfiguration configuration;

		// Token: 0x04000C53 RID: 3155
		private KeyInfoSerializer keyInfoSerializer;
	}
}
