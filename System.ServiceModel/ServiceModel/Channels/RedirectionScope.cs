using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000985 RID: 2437
	[Serializable]
	public class RedirectionScope
	{
		// Token: 0x06005E4D RID: 24141 RVA: 0x0015D176 File Offset: 0x0015B376
		private RedirectionScope()
		{
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x0015D180 File Offset: 0x0015B380
		private RedirectionScope(RedirectionScope.InternalRedirectionScope scope)
		{
			this.Namespace = "http://schemas.microsoft.com/ws/2008/06/redirect";
			this.internalScope = scope;
			switch (scope)
			{
			case RedirectionScope.InternalRedirectionScope.Message:
				this.Value = "Message";
				return;
			case RedirectionScope.InternalRedirectionScope.Session:
				this.Value = "Session";
				return;
			case RedirectionScope.InternalRedirectionScope.Endpoint:
				this.Value = "Endpoint";
				return;
			default:
				return;
			}
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x0015D1DD File Offset: 0x0015B3DD
		private RedirectionScope(string value, string ns)
		{
			this.Value = value;
			this.Namespace = ns;
			this.internalScope = RedirectionScope.InternalRedirectionScope.Unknown;
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06005E50 RID: 24144 RVA: 0x0015D1FA File Offset: 0x0015B3FA
		public static RedirectionScope Endpoint
		{
			get
			{
				return RedirectionScope.endpoint;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x0015D201 File Offset: 0x0015B401
		public static RedirectionScope Message
		{
			get
			{
				return RedirectionScope.message;
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06005E52 RID: 24146 RVA: 0x0015D208 File Offset: 0x0015B408
		public static RedirectionScope Session
		{
			get
			{
				return RedirectionScope.session;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x0015D20F File Offset: 0x0015B40F
		// (set) Token: 0x06005E54 RID: 24148 RVA: 0x0015D217 File Offset: 0x0015B417
		public string Namespace { get; private set; }

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x0015D220 File Offset: 0x0015B420
		// (set) Token: 0x06005E56 RID: 24150 RVA: 0x0015D228 File Offset: 0x0015B428
		public string Value { get; private set; }

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x0015D231 File Offset: 0x0015B431
		internal RedirectionScope.InternalRedirectionScope InternalScope
		{
			get
			{
				if (this.internalScope == RedirectionScope.InternalRedirectionScope.Unknown)
				{
					this.DetectScope();
				}
				return this.internalScope;
			}
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x0015D247 File Offset: 0x0015B447
		public static bool operator !=(RedirectionScope left, RedirectionScope right)
		{
			return !(left == right);
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x0015D254 File Offset: 0x0015B454
		public static bool operator ==(RedirectionScope left, RedirectionScope right)
		{
			bool result = false;
			if (left == null && right == null)
			{
				result = true;
			}
			else if (left != null && right != null)
			{
				result = (left.InternalScope == right.InternalScope || RedirectionUtility.IsNamespaceAndValueMatch(left.Value, left.Namespace, right.Value, right.Namespace));
			}
			return result;
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x0015D2A4 File Offset: 0x0015B4A4
		public static RedirectionScope Create(string scope, string ns)
		{
			if (scope == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("scope");
			}
			if (scope.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("scope", SR.GetString("ParameterCannotBeEmpty"));
			}
			return new RedirectionScope(scope, ns);
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x0015D2E4 File Offset: 0x0015B4E4
		public override bool Equals(object obj)
		{
			bool flag = base.Equals(obj);
			if (!flag)
			{
				flag = (obj as RedirectionScope == this);
			}
			return flag;
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x0015D30A File Offset: 0x0015B50A
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(RedirectionUtility.ComputeHashCode(this.Value, this.Namespace));
			}
			return this.hashCode.Value;
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0015D340 File Offset: 0x0015B540
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

		// Token: 0x06005E5E RID: 24158 RVA: 0x0015D3AC File Offset: 0x0015B5AC
		private void DetectScope()
		{
			if (!RedirectionUtility.IsNamespaceMatch(this.Namespace, "http://schemas.microsoft.com/ws/2008/06/redirect"))
			{
				this.internalScope = RedirectionScope.InternalRedirectionScope.Custom;
				return;
			}
			if (string.Equals(this.Value, "Message", StringComparison.Ordinal))
			{
				this.internalScope = RedirectionScope.InternalRedirectionScope.Message;
				return;
			}
			if (string.Equals(this.Value, "Session", StringComparison.Ordinal))
			{
				this.internalScope = RedirectionScope.InternalRedirectionScope.Session;
				return;
			}
			if (string.Equals(this.Value, "Endpoint", StringComparison.Ordinal))
			{
				this.internalScope = RedirectionScope.InternalRedirectionScope.Endpoint;
				return;
			}
			this.internalScope = RedirectionScope.InternalRedirectionScope.Custom;
		}

		// Token: 0x040037EE RID: 14318
		private static RedirectionScope endpoint = new RedirectionScope(RedirectionScope.InternalRedirectionScope.Endpoint);

		// Token: 0x040037EF RID: 14319
		private static RedirectionScope message = new RedirectionScope(RedirectionScope.InternalRedirectionScope.Message);

		// Token: 0x040037F0 RID: 14320
		private static RedirectionScope session = new RedirectionScope(RedirectionScope.InternalRedirectionScope.Session);

		// Token: 0x040037F1 RID: 14321
		private RedirectionScope.InternalRedirectionScope internalScope;

		// Token: 0x040037F2 RID: 14322
		private string toString;

		// Token: 0x040037F3 RID: 14323
		private int? hashCode;

		// Token: 0x02000DF7 RID: 3575
		internal enum InternalRedirectionScope
		{
			// Token: 0x0400499B RID: 18843
			Unknown,
			// Token: 0x0400499C RID: 18844
			Custom,
			// Token: 0x0400499D RID: 18845
			Message,
			// Token: 0x0400499E RID: 18846
			Session,
			// Token: 0x0400499F RID: 18847
			Endpoint
		}
	}
}
