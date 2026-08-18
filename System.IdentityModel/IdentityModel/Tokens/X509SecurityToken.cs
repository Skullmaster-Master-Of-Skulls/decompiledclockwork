using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000195 RID: 405
	public class X509SecurityToken : SecurityToken, IDisposable
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003D9A1 File Offset: 0x0003BBA1
		public X509SecurityToken(X509Certificate2 certificate) : this(certificate, SecurityUniqueId.Create().Value)
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
		public X509SecurityToken(X509Certificate2 certificate, string id) : this(certificate, id, true)
		{
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003D9BF File Offset: 0x0003BBBF
		internal X509SecurityToken(X509Certificate2 certificate, bool clone) : this(certificate, SecurityUniqueId.Create().Value, clone)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003D9D3 File Offset: 0x0003BBD3
		internal X509SecurityToken(X509Certificate2 certificate, bool clone, bool disposable) : this(certificate, SecurityUniqueId.Create().Value, clone, disposable)
		{
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
		internal X509SecurityToken(X509Certificate2 certificate, string id, bool clone) : this(certificate, id, clone, true)
		{
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003D9F4 File Offset: 0x0003BBF4
		internal X509SecurityToken(X509Certificate2 certificate, string id, bool clone, bool disposable)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			this.id = id;
			this.certificate = (clone ? new X509Certificate2(certificate) : certificate);
			this.disposable = (clone || disposable);
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003DA66 File Offset: 0x0003BC66
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0003DA70 File Offset: 0x0003BC70
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.securityKeys == null)
				{
					this.securityKeys = new List<SecurityKey>(1)
					{
						new X509AsymmetricSecurityKey(this.certificate)
					}.AsReadOnly();
				}
				return this.securityKeys;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		public override DateTime ValidFrom
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.effectiveTime == SecurityUtils.MaxUtcDateTime)
				{
					this.effectiveTime = this.certificate.NotBefore.ToUniversalTime();
				}
				return this.effectiveTime;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		public override DateTime ValidTo
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.expirationTime == SecurityUtils.MinUtcDateTime)
				{
					this.expirationTime = this.certificate.NotAfter.ToUniversalTime();
				}
				return this.expirationTime;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0003DB40 File Offset: 0x0003BD40
		public X509Certificate2 Certificate
		{
			get
			{
				this.ThrowIfDisposed();
				return this.certificate;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003DB50 File Offset: 0x0003BD50
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			this.ThrowIfDisposed();
			if (typeof(T) == typeof(X509SubjectKeyIdentifierClause))
			{
				return X509SubjectKeyIdentifierClause.CanCreateFrom(this.certificate);
			}
			return typeof(T) == typeof(X509ThumbprintKeyIdentifierClause) || typeof(T) == typeof(X509IssuerSerialKeyIdentifierClause) || typeof(T) == typeof(X509RawDataKeyIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		public override T CreateKeyIdentifierClause<T>()
		{
			this.ThrowIfDisposed();
			if (typeof(T) == typeof(X509SubjectKeyIdentifierClause))
			{
				X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause;
				if (X509SubjectKeyIdentifierClause.TryCreateFrom(this.certificate, out x509SubjectKeyIdentifierClause))
				{
					return x509SubjectKeyIdentifierClause as T;
				}
			}
			else
			{
				if (typeof(T) == typeof(X509ThumbprintKeyIdentifierClause))
				{
					return new X509ThumbprintKeyIdentifierClause(this.certificate) as T;
				}
				if (typeof(T) == typeof(X509IssuerSerialKeyIdentifierClause))
				{
					return new X509IssuerSerialKeyIdentifierClause(this.certificate) as T;
				}
				if (typeof(T) == typeof(X509RawDataKeyIdentifierClause))
				{
					return new X509RawDataKeyIdentifierClause(this.certificate) as T;
				}
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003DCCC File Offset: 0x0003BECC
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			this.ThrowIfDisposed();
			X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause = keyIdentifierClause as X509SubjectKeyIdentifierClause;
			if (x509SubjectKeyIdentifierClause != null)
			{
				return x509SubjectKeyIdentifierClause.Matches(this.certificate);
			}
			X509ThumbprintKeyIdentifierClause x509ThumbprintKeyIdentifierClause = keyIdentifierClause as X509ThumbprintKeyIdentifierClause;
			if (x509ThumbprintKeyIdentifierClause != null)
			{
				return x509ThumbprintKeyIdentifierClause.Matches(this.certificate);
			}
			X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = keyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
			if (x509IssuerSerialKeyIdentifierClause != null)
			{
				return x509IssuerSerialKeyIdentifierClause.Matches(this.certificate);
			}
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = keyIdentifierClause as X509RawDataKeyIdentifierClause;
			if (x509RawDataKeyIdentifierClause != null)
			{
				return x509RawDataKeyIdentifierClause.Matches(this.certificate);
			}
			return base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003DD42 File Offset: 0x0003BF42
		public virtual void Dispose()
		{
			if (this.disposable && !this.disposed)
			{
				this.disposed = true;
				this.certificate.Reset();
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003DD66 File Offset: 0x0003BF66
		protected void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04000CB5 RID: 3253
		private string id;

		// Token: 0x04000CB6 RID: 3254
		private X509Certificate2 certificate;

		// Token: 0x04000CB7 RID: 3255
		private ReadOnlyCollection<SecurityKey> securityKeys;

		// Token: 0x04000CB8 RID: 3256
		private DateTime effectiveTime = SecurityUtils.MaxUtcDateTime;

		// Token: 0x04000CB9 RID: 3257
		private DateTime expirationTime = SecurityUtils.MinUtcDateTime;

		// Token: 0x04000CBA RID: 3258
		private bool disposed;

		// Token: 0x04000CBB RID: 3259
		private bool disposable;
	}
}
