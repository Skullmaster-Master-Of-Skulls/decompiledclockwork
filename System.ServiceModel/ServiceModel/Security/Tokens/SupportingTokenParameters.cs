using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel.Diagnostics;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A6 RID: 934
	[__DynamicallyInvokable]
	public class SupportingTokenParameters
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x0007FDB4 File Offset: 0x0007DFB4
		private SupportingTokenParameters(SupportingTokenParameters other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			foreach (SecurityTokenParameters securityTokenParameters in other.signed)
			{
				this.signed.Add(securityTokenParameters.Clone());
			}
			foreach (SecurityTokenParameters securityTokenParameters2 in other.signedEncrypted)
			{
				this.signedEncrypted.Add(securityTokenParameters2.Clone());
			}
			foreach (SecurityTokenParameters securityTokenParameters3 in other.endorsing)
			{
				this.endorsing.Add(securityTokenParameters3.Clone());
			}
			foreach (SecurityTokenParameters securityTokenParameters4 in other.signedEndorsing)
			{
				this.signedEndorsing.Add(securityTokenParameters4.Clone());
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0007FF30 File Offset: 0x0007E130
		[__DynamicallyInvokable]
		public SupportingTokenParameters()
		{
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007FF64 File Offset: 0x0007E164
		[__DynamicallyInvokable]
		public Collection<SecurityTokenParameters> Endorsing
		{
			[__DynamicallyInvokable]
			get
			{
				return this.endorsing;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0007FF6C File Offset: 0x0007E16C
		public Collection<SecurityTokenParameters> SignedEndorsing
		{
			get
			{
				return this.signedEndorsing;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0007FF74 File Offset: 0x0007E174
		public Collection<SecurityTokenParameters> Signed
		{
			get
			{
				return this.signed;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0007FF7C File Offset: 0x0007E17C
		[__DynamicallyInvokable]
		public Collection<SecurityTokenParameters> SignedEncrypted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.signedEncrypted;
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0007FF84 File Offset: 0x0007E184
		public void SetKeyDerivation(bool requireDerivedKeys)
		{
			foreach (SecurityTokenParameters securityTokenParameters in this.endorsing)
			{
				if (securityTokenParameters.HasAsymmetricKey)
				{
					securityTokenParameters.RequireDerivedKeys = false;
				}
				else
				{
					securityTokenParameters.RequireDerivedKeys = requireDerivedKeys;
				}
			}
			foreach (SecurityTokenParameters securityTokenParameters2 in this.signedEndorsing)
			{
				if (securityTokenParameters2.HasAsymmetricKey)
				{
					securityTokenParameters2.RequireDerivedKeys = false;
				}
				else
				{
					securityTokenParameters2.RequireDerivedKeys = requireDerivedKeys;
				}
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00080030 File Offset: 0x0007E230
		internal bool IsSetKeyDerivation(bool requireDerivedKeys)
		{
			foreach (SecurityTokenParameters securityTokenParameters in this.endorsing)
			{
				if (securityTokenParameters.RequireDerivedKeys != requireDerivedKeys)
				{
					return false;
				}
			}
			foreach (SecurityTokenParameters securityTokenParameters2 in this.signedEndorsing)
			{
				if (securityTokenParameters2.RequireDerivedKeys != requireDerivedKeys)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000800CC File Offset: 0x0007E2CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.endorsing.Count == 0)
			{
				stringBuilder.AppendLine("No endorsing tokens.");
			}
			else
			{
				for (int i = 0; i < this.endorsing.Count; i++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "Endorsing[{0}]", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture)
					}));
					stringBuilder.AppendLine("  " + this.endorsing[i].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			if (this.signed.Count == 0)
			{
				stringBuilder.AppendLine("No signed tokens.");
			}
			else
			{
				for (int i = 0; i < this.signed.Count; i++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "Signed[{0}]", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture)
					}));
					stringBuilder.AppendLine("  " + this.signed[i].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			if (this.signedEncrypted.Count == 0)
			{
				stringBuilder.AppendLine("No signed encrypted tokens.");
			}
			else
			{
				for (int i = 0; i < this.signedEncrypted.Count; i++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "SignedEncrypted[{0}]", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture)
					}));
					stringBuilder.AppendLine("  " + this.signedEncrypted[i].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			if (this.signedEndorsing.Count == 0)
			{
				stringBuilder.AppendLine("No signed endorsing tokens.");
			}
			else
			{
				for (int i = 0; i < this.signedEndorsing.Count; i++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "SignedEndorsing[{0}]", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture)
					}));
					stringBuilder.AppendLine("  " + this.signedEndorsing[i].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00080334 File Offset: 0x0007E534
		[__DynamicallyInvokable]
		public SupportingTokenParameters Clone()
		{
			SupportingTokenParameters supportingTokenParameters = this.CloneCore();
			if (supportingTokenParameters == null || supportingTokenParameters.GetType() != base.GetType())
			{
				TraceUtility.TraceEvent(TraceEventType.Error, 458752, SR.GetString("CloneNotImplementedCorrectly", new object[]
				{
					base.GetType(),
					(supportingTokenParameters != null) ? supportingTokenParameters.ToString() : "null"
				}));
			}
			return supportingTokenParameters;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00080396 File Offset: 0x0007E596
		protected virtual SupportingTokenParameters CloneCore()
		{
			return new SupportingTokenParameters(this);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0008039E File Offset: 0x0007E59E
		internal bool IsEmpty()
		{
			return this.signed.Count == 0 && this.signedEncrypted.Count == 0 && this.endorsing.Count == 0 && this.signedEndorsing.Count == 0;
		}

		// Token: 0x04001FD2 RID: 8146
		private Collection<SecurityTokenParameters> signed = new Collection<SecurityTokenParameters>();

		// Token: 0x04001FD3 RID: 8147
		private Collection<SecurityTokenParameters> signedEncrypted = new Collection<SecurityTokenParameters>();

		// Token: 0x04001FD4 RID: 8148
		private Collection<SecurityTokenParameters> endorsing = new Collection<SecurityTokenParameters>();

		// Token: 0x04001FD5 RID: 8149
		private Collection<SecurityTokenParameters> signedEndorsing = new Collection<SecurityTokenParameters>();
	}
}
