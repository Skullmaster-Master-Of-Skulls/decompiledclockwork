using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000986 RID: 2438
	[Serializable]
	public sealed class RedirectionType
	{
		// Token: 0x06005E60 RID: 24160 RVA: 0x0015D44E File Offset: 0x0015B64E
		private RedirectionType()
		{
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0015D458 File Offset: 0x0015B658
		private RedirectionType(RedirectionType.InternalRedirectionType type)
		{
			this.Namespace = "http://schemas.microsoft.com/ws/2008/06/redirect";
			this.internalType = type;
			switch (type)
			{
			case RedirectionType.InternalRedirectionType.Cache:
				this.Value = "Cache";
				return;
			case RedirectionType.InternalRedirectionType.UseIntermediary:
				this.Value = "UseIntermediary";
				return;
			case RedirectionType.InternalRedirectionType.Resource:
				this.Value = "Resource";
				return;
			default:
				return;
			}
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0015D4B5 File Offset: 0x0015B6B5
		private RedirectionType(string value, string ns)
		{
			this.Value = value;
			this.Namespace = ns;
			this.internalType = RedirectionType.InternalRedirectionType.Unknown;
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x0015D4D2 File Offset: 0x0015B6D2
		public static RedirectionType Cache
		{
			get
			{
				return RedirectionType.cache;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0015D4D9 File Offset: 0x0015B6D9
		public static RedirectionType Resource
		{
			get
			{
				return RedirectionType.resource;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06005E65 RID: 24165 RVA: 0x0015D4E0 File Offset: 0x0015B6E0
		public static RedirectionType UseIntermediary
		{
			get
			{
				return RedirectionType.useIntermediary;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06005E66 RID: 24166 RVA: 0x0015D4E7 File Offset: 0x0015B6E7
		// (set) Token: 0x06005E67 RID: 24167 RVA: 0x0015D4EF File Offset: 0x0015B6EF
		public string Namespace { get; private set; }

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x0015D4F8 File Offset: 0x0015B6F8
		// (set) Token: 0x06005E69 RID: 24169 RVA: 0x0015D500 File Offset: 0x0015B700
		public string Value { get; private set; }

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06005E6A RID: 24170 RVA: 0x0015D509 File Offset: 0x0015B709
		internal RedirectionType.InternalRedirectionType InternalType
		{
			get
			{
				if (this.internalType == RedirectionType.InternalRedirectionType.Unknown)
				{
					this.DetectType();
				}
				return this.internalType;
			}
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0015D51F File Offset: 0x0015B71F
		public static bool operator !=(RedirectionType left, RedirectionType right)
		{
			return !(left == right);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x0015D52C File Offset: 0x0015B72C
		public static bool operator ==(RedirectionType left, RedirectionType right)
		{
			bool result = false;
			if (left == null && right == null)
			{
				result = true;
			}
			else if (left != null && right != null)
			{
				result = (left.InternalType == right.InternalType || RedirectionUtility.IsNamespaceAndValueMatch(left.Value, left.Namespace, right.Value, right.Namespace));
			}
			return result;
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x0015D57C File Offset: 0x0015B77C
		public static RedirectionType Create(string type, string ns)
		{
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("type");
			}
			if (type.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("type", SR.GetString("ParameterCannotBeEmpty"));
			}
			return new RedirectionType(type, ns);
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x0015D5BC File Offset: 0x0015B7BC
		public override bool Equals(object obj)
		{
			bool flag = base.Equals(obj);
			if (!flag)
			{
				flag = (obj as RedirectionType == this);
			}
			return flag;
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x0015D5E2 File Offset: 0x0015B7E2
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(RedirectionUtility.ComputeHashCode(this.Value, this.Namespace));
			}
			return this.hashCode.Value;
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x0015D618 File Offset: 0x0015B818
		public override string ToString()
		{
			if (this.toString == null)
			{
				if (this.Namespace != null)
				{
					this.toString = SR.GetString("RedirectionInfoStringFormatWithNamespace", new object[]
					{
						this.Value,
						this.Namespace
					});
				}
				else
				{
					this.toString = SR.GetString("RedirectionInfoStringFormatNoNamespace", new object[]
					{
						this.Value
					});
				}
			}
			return this.toString;
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x0015D684 File Offset: 0x0015B884
		private void DetectType()
		{
			if (!RedirectionUtility.IsNamespaceMatch(this.Namespace, "http://schemas.microsoft.com/ws/2008/06/redirect"))
			{
				this.internalType = RedirectionType.InternalRedirectionType.Custom;
				return;
			}
			if (string.Equals(this.Value, "Cache", StringComparison.Ordinal))
			{
				this.internalType = RedirectionType.InternalRedirectionType.Cache;
				return;
			}
			if (string.Equals(this.Value, "Resource", StringComparison.Ordinal))
			{
				this.internalType = RedirectionType.InternalRedirectionType.Resource;
				return;
			}
			if (string.Equals(this.Value, "UseIntermediary", StringComparison.Ordinal))
			{
				this.internalType = RedirectionType.InternalRedirectionType.UseIntermediary;
				return;
			}
			this.internalType = RedirectionType.InternalRedirectionType.Custom;
		}

		// Token: 0x040037F6 RID: 14326
		private static RedirectionType cache = new RedirectionType(RedirectionType.InternalRedirectionType.Cache);

		// Token: 0x040037F7 RID: 14327
		private static RedirectionType resource = new RedirectionType(RedirectionType.InternalRedirectionType.Resource);

		// Token: 0x040037F8 RID: 14328
		private static RedirectionType useIntermediary = new RedirectionType(RedirectionType.InternalRedirectionType.UseIntermediary);

		// Token: 0x040037F9 RID: 14329
		private RedirectionType.InternalRedirectionType internalType;

		// Token: 0x040037FA RID: 14330
		private string toString;

		// Token: 0x040037FB RID: 14331
		private int? hashCode;

		// Token: 0x02000DF8 RID: 3576
		internal enum InternalRedirectionType
		{
			// Token: 0x040049A1 RID: 18849
			Unknown,
			// Token: 0x040049A2 RID: 18850
			Custom,
			// Token: 0x040049A3 RID: 18851
			Cache,
			// Token: 0x040049A4 RID: 18852
			UseIntermediary,
			// Token: 0x040049A5 RID: 18853
			Resource
		}
	}
}
