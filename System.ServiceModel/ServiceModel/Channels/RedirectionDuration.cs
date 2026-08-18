using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000982 RID: 2434
	[Serializable]
	public class RedirectionDuration
	{
		// Token: 0x06005E26 RID: 24102 RVA: 0x0015CADB File Offset: 0x0015ACDB
		private RedirectionDuration()
		{
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x0015CAE3 File Offset: 0x0015ACE3
		private RedirectionDuration(RedirectionDuration.InternalRedirectionDuration duration)
		{
			this.Namespace = "http://schemas.microsoft.com/ws/2008/06/redirect";
			this.internalDuration = duration;
			if (duration == RedirectionDuration.InternalRedirectionDuration.Temporary)
			{
				this.Value = "Temporary";
				return;
			}
			if (duration != RedirectionDuration.InternalRedirectionDuration.Permanent)
			{
				return;
			}
			this.Value = "Permanent";
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x0015CB1D File Offset: 0x0015AD1D
		private RedirectionDuration(string duration, string ns)
		{
			this.Value = duration;
			this.Namespace = ns;
			this.internalDuration = RedirectionDuration.InternalRedirectionDuration.Unknown;
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x0015CB3A File Offset: 0x0015AD3A
		public static RedirectionDuration Permanent
		{
			get
			{
				return RedirectionDuration.permanent;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x0015CB41 File Offset: 0x0015AD41
		public static RedirectionDuration Temporary
		{
			get
			{
				return RedirectionDuration.temporary;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0015CB48 File Offset: 0x0015AD48
		// (set) Token: 0x06005E2C RID: 24108 RVA: 0x0015CB50 File Offset: 0x0015AD50
		public string Namespace { get; private set; }

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x0015CB59 File Offset: 0x0015AD59
		// (set) Token: 0x06005E2E RID: 24110 RVA: 0x0015CB61 File Offset: 0x0015AD61
		public string Value { get; private set; }

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x0015CB6A File Offset: 0x0015AD6A
		internal RedirectionDuration.InternalRedirectionDuration InternalDuration
		{
			get
			{
				if (this.internalDuration == RedirectionDuration.InternalRedirectionDuration.Unknown)
				{
					this.DetectDuration();
				}
				return this.internalDuration;
			}
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0015CB80 File Offset: 0x0015AD80
		public static bool operator !=(RedirectionDuration left, RedirectionDuration right)
		{
			return !(left == right);
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0015CB8C File Offset: 0x0015AD8C
		public static bool operator ==(RedirectionDuration left, RedirectionDuration right)
		{
			bool result = false;
			if (left == null && right == null)
			{
				result = true;
			}
			else if (left != null && right != null)
			{
				result = (left.InternalDuration == right.InternalDuration || RedirectionUtility.IsNamespaceAndValueMatch(left.Value, left.Namespace, right.Value, right.Namespace));
			}
			return result;
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x0015CBDC File Offset: 0x0015ADDC
		public static RedirectionDuration Create(string duration, string ns)
		{
			if (duration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("duration");
			}
			if (duration.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("duration", SR.GetString("ParameterCannotBeEmpty"));
			}
			return new RedirectionDuration(duration, ns);
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x0015CC1C File Offset: 0x0015AE1C
		public override bool Equals(object obj)
		{
			bool flag = base.Equals(obj);
			if (!flag)
			{
				flag = (obj as RedirectionDuration == this);
			}
			return flag;
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0015CC42 File Offset: 0x0015AE42
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				this.hashCode = new int?(RedirectionUtility.ComputeHashCode(this.Value, this.Namespace));
			}
			return this.hashCode.Value;
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0015CC78 File Offset: 0x0015AE78
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

		// Token: 0x06005E36 RID: 24118 RVA: 0x0015CCE4 File Offset: 0x0015AEE4
		private void DetectDuration()
		{
			if (!RedirectionUtility.IsNamespaceMatch(this.Namespace, "http://schemas.microsoft.com/ws/2008/06/redirect"))
			{
				this.internalDuration = RedirectionDuration.InternalRedirectionDuration.Custom;
				return;
			}
			if (string.Equals(this.Value, "Temporary", StringComparison.Ordinal))
			{
				this.internalDuration = RedirectionDuration.InternalRedirectionDuration.Temporary;
				return;
			}
			if (string.Equals(this.Value, "Permanent", StringComparison.Ordinal))
			{
				this.internalDuration = RedirectionDuration.InternalRedirectionDuration.Permanent;
				return;
			}
			this.internalDuration = RedirectionDuration.InternalRedirectionDuration.Custom;
		}

		// Token: 0x040037E2 RID: 14306
		private static RedirectionDuration permanent = new RedirectionDuration(RedirectionDuration.InternalRedirectionDuration.Permanent);

		// Token: 0x040037E3 RID: 14307
		private static RedirectionDuration temporary = new RedirectionDuration(RedirectionDuration.InternalRedirectionDuration.Temporary);

		// Token: 0x040037E4 RID: 14308
		private RedirectionDuration.InternalRedirectionDuration internalDuration;

		// Token: 0x040037E5 RID: 14309
		private string toString;

		// Token: 0x040037E6 RID: 14310
		private int? hashCode;

		// Token: 0x02000DF6 RID: 3574
		internal enum InternalRedirectionDuration
		{
			// Token: 0x04004996 RID: 18838
			Unknown,
			// Token: 0x04004997 RID: 18839
			Custom,
			// Token: 0x04004998 RID: 18840
			Temporary,
			// Token: 0x04004999 RID: 18841
			Permanent
		}
	}
}
